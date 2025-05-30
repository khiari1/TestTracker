using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Tsi.Erp.TestTracker.Api.Controllers.Dto.Request;
using Tsi.AspNetCore.Identity.AzureAD;
using Tsi.Erp.TestTracker.Api.Controllers.Dto.Response;
using Tsi.Erp.TestTracker.Domain.Stores;
using Tsi.Extensions.Identity.Stores;
using System.Linq.Extensions;
using Tsi.Erp.TestTracker.Core.Services;
using System.IO;


namespace Tsi.Erp.TestTracker.Api.Controllers.v1
{


    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class UsersController : ControllerBase
    {
        private readonly IMapper _mapper;        
        private readonly UserAzureADManager<ApplicationUser, TsiIdentityPermission> _userAzureAdManager;
        private readonly FileManager _fileManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UsersController(IMapper mapper,
            UserAzureADManager<ApplicationUser, TsiIdentityPermission> userAzureAD,
            FileManager fileManager,
            IWebHostEnvironment webHostEnvironment
            )
        {
            _mapper = mapper;
            _userAzureAdManager = userAzureAD;
            _fileManager = fileManager;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        [TsiAuthorize(Permissions.User_Read)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<IActionResult> GetAsync()
        {
            var users = await _userAzureAdManager.GetAsync();
            var userResponse = _mapper.Map<List<UserResponse>>(users);
            return Ok(userResponse);
        }

        [HttpPost("Query")]
        [TsiAuthorize(Permissions.User_Read)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<IActionResult> PostAsync(Query filter)
        {
            var users = await _userAzureAdManager.GetAsync(filter);
            var userResponse = _mapper.Map<List<UserResponse>>(users);
            return Ok(userResponse);
        }

        [HttpPost]
        [TsiAuthorize(Permissions.User_ReadWrite)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<IActionResult> PostAsync([FromBody] UserRequest user)
        {
            var userToCreate = _mapper.Map<ApplicationUser>(user);
            await _userAzureAdManager.CreateAsync(userToCreate);
            return CreatedAtAction(nameof(GetAsync), new { id = userToCreate.Id }, userToCreate);
        }


        [HttpGet("me")]
        [TsiAuthorize]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<IActionResult> MeAsync()
        {
           var user= await _userAzureAdManager.GetUserAsync(HttpContext.User);
            return Ok(_mapper.Map<UserResponse>(user));
        }

        [HttpPost("InviteUser")]
        [TsiAuthorize(Permissions.User_ReadWrite)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<IActionResult> InviteUserAsync([FromBody] InvitationUserModel request)
        {            
            var response = await _userAzureAdManager.InviteUser(request);
            return Ok(response);
        }

        [HttpGet("{id}")]
        [TsiAuthorize(Permissions.User_Read)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<IActionResult> GetAsync(string id)
        {
            var user = await _userAzureAdManager.FindbyIdAsync(id);
            var userResponse = _mapper.Map<UserResponse>(user);
            return Ok(userResponse);
        }

        [HttpPut("{id}")]
        [TsiAuthorize(Permissions.User_ReadWrite)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        public async Task<IActionResult> PutAsync(string id, [FromBody] UserRequest userModel)
        {
            var user = await _userAzureAdManager.FindbyIdAsync(id);
            if (user is null)
            {
                return NotFound();
            }

            _mapper.Map(userModel, user);

            await _userAzureAdManager.UpdateAsync(id,user);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [TsiAuthorize(Permissions.User_ReadWrite)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Delete))]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            var user = await _userAzureAdManager.FindbyIdAsync(id);
            if (user is null)
            {
                return NotFound();
            }
            await _userAzureAdManager.DeleteAsync(id);

            return Ok();
        }

        [HttpGet("SyncStore")]
        [TsiAuthorize(Permissions.User_Read)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Delete))]
        public async Task<IActionResult> SyncStoreAsync()
        {
            await _userAzureAdManager.SyncStoreAsync();

            var photos = await _userAzureAdManager.UsersPhotoAsync();

            foreach (var photo in photos)
            {
                using MemoryStream stream = new MemoryStream();
                photo.Value.CopyTo(stream);
                _fileManager.SaveAvatar(_webHostEnvironment.WebRootPath, photo.Key, stream) ;

                photo.Value.Close();
                photo.Value.Dispose();

            }
            
            return Ok();
        }
    }
}
