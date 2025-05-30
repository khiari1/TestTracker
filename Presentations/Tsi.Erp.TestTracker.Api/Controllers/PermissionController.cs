using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tsi.AspNetCore.Identity.AzureAD;
using Tsi.Erp.TestTracker.Api.Controllers.Dto.Response;
using Tsi.Erp.TestTracker.Api.Helpers;
using Tsi.Erp.TestTracker.Domain.Stores;
using Tsi.Extensions.Identity.Stores;


namespace Tsi.Erp.TestTracker.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PermissionController : ControllerBase
{
    private readonly IMapper _mapper;

    private readonly UserAzureADManager<ApplicationUser, TsiIdentityPermission> _userAzureAdManager;


    public PermissionController(IMapper mapper,
                                UserAzureADManager<ApplicationUser, TsiIdentityPermission> userAzureAdManager)
    {
        _mapper = mapper;
        _userAzureAdManager = userAzureAdManager;
    }

    [HttpGet("alluserpermission")]
    [Authorize]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
    public async Task<IActionResult> GetUserPermissionsAsync()
    {
        var httpContext = HttpContext;
        var claimsPrincipal = httpContext?.User;

        if (claimsPrincipal is null)
        {
            return Unauthorized();
        }
        // var isAdmin = _userAzureAdManager.IsUserAdmin(claimsPrincipal);

        var user = await _userAzureAdManager.GetUserAsync(claimsPrincipal);
        if (user is null)
        {
            return NotFound();
        }
        if (user.IsAdmin == true)
        {
            var Adminpermission = await _userAzureAdManager.GetUserPermissionsAsync(user);
            return Ok(Adminpermission);
        }

        var result = EnumHelper.ParseValues<Permissions>();
        var permissions = await _userAzureAdManager.GetUserPermissionsAsync(user);
        var rights = from r in result
                     join gp in permissions on r.Key equals gp.Key into rgp
                     from gp in rgp.DefaultIfEmpty()

                     where rgp.Any() && gp.Name == gp.Key
                     select new PermissionResponse
                     {
                         Key = r.Key,
                         Value = r.Value,

                     };
        var permissionResponses = _mapper.Map<List<PermissionResponse>>(rights);

        return Ok(permissionResponses);
    }


}

