using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using Microsoft.VisualStudio.Services.Common;
using System.Security.Claims;
using Tsi.AspNetCore.Identity.AzureAD;
using Tsi.Erp.TestTracker.Domain.Stores;
using Tsi.Extensions.Identity.Stores;


namespace Tsi.Erp.TestTracker.Api.Security;

public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    private UserAzureADManager<ApplicationUser, TsiIdentityPermission> _userAzureAdManager;

    public PermissionAuthorizationHandler(UserAzureADManager<ApplicationUser, TsiIdentityPermission> userAzureAD)
    {
        _userAzureAdManager = userAzureAD;

    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {

        if (HasPermission(context.User, requirement.RequiredPermission).Result)
        {

            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }



    private async Task<bool> HasPermission(ClaimsPrincipal claim, Permissions permission)
    {
        var user = await _userAzureAdManager.GetUserAsync(claim);
        if (user is null)
        {
            return false;
        }
        if (user.IsAdmin!.Value)
        {
            return true;
        }
        
        return await _userAzureAdManager.HasPermissionAsync(user, permission.ToString());
    }

}


