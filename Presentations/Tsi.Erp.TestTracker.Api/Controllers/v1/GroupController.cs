using Microsoft.AspNetCore.Mvc;
using System.Drawing.Imaging;
using Tsi.AspNetCore.Identity.AzureAD;
using Tsi.Erp.TestTracker.Api.Controllers.Dto.Request;
using Tsi.Erp.TestTracker.Api.Controllers.Dto.Response;
using Tsi.Erp.TestTracker.Api.Helpers;
using Tsi.Erp.TestTracker.Core.Services;
using Tsi.Erp.TestTracker.Domain.Stores;
using Tsi.Extensions.Identity.Core;
using Tsi.Extensions.Identity.Stores;

namespace Tsi.Erp.TestTracker.Api.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly TsiGroupManager<TsiIdentityGroup, TsiIdentityPermission> _groupManager;
        private readonly UserAzureADManager<ApplicationUser, TsiIdentityPermission> _userAzureAdManager;
        private readonly FileManager _fileManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public GroupsController(
            TsiGroupManager<TsiIdentityGroup, TsiIdentityPermission> groupManager,
            UserAzureADManager<ApplicationUser, TsiIdentityPermission> userAzureAdManager,
            FileManager fileManager,
            IWebHostEnvironment webHostEnvironment
            )
        {
            _groupManager = groupManager;
            _userAzureAdManager = userAzureAdManager;
            this._fileManager = fileManager;
            this._webHostEnvironment = webHostEnvironment;
        }

        // GET: api/<GroupController>
        [HttpGet]
        //  [TsiAuthorize(Rights.Group_Read)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<IActionResult> GetAsync(CancellationToken cancellation)
        {

            var group = await _groupManager.GetAsync();

            return Ok(group);
        }
        // GET: api/<GroupController>/5
        [HttpGet("{id}")]
        //  [TsiAuthorize(Rights.Group_Read)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<IActionResult> GetAsync(string id)
        {
            var group = await _groupManager.FindByIdAsync(id);
            if (group is null)
            {
                return NotFound();
            }
            return Ok(group);
        }

        // POST: api/<GroupController>
        [HttpPost]
        //  [TsiAuthorize(Rights.Group_ReadWrite)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<IActionResult> PostAsync([FromBody] GroupRequest model)
        {
            var group = new TsiIdentityGroup()
            {
                Name = model.Name,
                Description = model.Description,
            };

            await _groupManager.CreateAsync(group);

            if (!string.IsNullOrWhiteSpace(model.Name))
            {
                var bitmap = AvatarGenerator.GenerateAvatar(model.Name.FirstOrDefault().ToString());
                using MemoryStream stream = new MemoryStream();

                bitmap.Save(stream, ImageFormat.Png);

                stream.Seek(0, SeekOrigin.Begin);

                _fileManager.SaveAvatar(_webHostEnvironment.WebRootPath, $"{group.Id}.png", stream);
            }
                
            return CreatedAtAction(nameof(GetAsync),
                new { id = group.Id }, group);
        }

        // PUT: api/<GroupController>/5
        [HttpPut("{id}")]
        //[TsiAuthorize(Rights.Group_ReadWrite)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        public async Task<IActionResult> PutAsync(string id, [FromBody] GroupRequest model)
        {
            var group = await _groupManager.FindByIdAsync(id);
            if (group is null)
            {
                return NotFound();
            }
            group.Name = model.Name;
            group.Description = model.Description;
            await _groupManager.UpdateAsync(group);

            return NoContent();
        }

        // DELETE: api/<GroupController>/5
        [HttpDelete("{id}")]
        //   [TsiAuthorize(Rights.Group_ReadWrite)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Delete))]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            var group = await _groupManager.FindByIdAsync(id);
            if (group is null)
            {
                return NotFound();
            }
            await _groupManager.DeleteAsync(group);
            return Ok();
        }

        //ADD Users : api/<GroupController>
        [HttpPut("AddUser")]
        // [TsiAuthorize(Rights.Group_ReadWrite)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        public async Task<IActionResult> AddUserAsync(UserToGroupRquest model)
        {
            var group = await _groupManager.FindByIdAsync(model.GroupId);
            if (group is null)
            {
                return NotFound();
            }
            var user = await _userAzureAdManager.FindbyIdAsync(model.UserId);
            if (user is null)
            {
                return NotFound("user not found");
            }

            await _userAzureAdManager.AddToGroupAsync(user, group.Id);

            return NoContent();
        }

        // Remove Users : api/<GroupController>
        [HttpPut("RemoveUser")]
        [TsiAuthorize(Permissions.Group_ReadWrite)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Delete))]
        public async Task<IActionResult> RemoveUserAsync(UserToGroupRquest model)
        {
            var user = await _userAzureAdManager.FindbyIdAsync(model.UserId);
            if (user is null)
            {
                return NotFound("user not found");
            }

            await _userAzureAdManager.RemoveFromGroupAsync(user, model.GroupId);

            return Ok();
        }

        [HttpGet("GetUserNotInGroup/{idGroup}")]
        // [TsiAuthorize(Rights.Group_Read)]
        public async Task<IActionResult> GetUserNotInGroup(string idGroup)
        {
            var group = await _groupManager.FindByIdAsync(idGroup);
            if (group is null)
            {
                return NotFound();
            }
            var users = await _userAzureAdManager.GetUsersNotInGroupAsync(idGroup);

            return Ok(users);
        }

        [HttpGet("GetUsersInGroup/{idGroup}")]
        // [TsiAuthorize(Rights.Group_Read)]
        public async Task<IActionResult> GetUsersInGroupAsync(string idGroup)
        {
            var group = await _groupManager.FindByIdAsync(idGroup);
            if (group is null)
            {
                return NotFound();
            }
            var users = await _userAzureAdManager.GetUsersInGroupAsync(idGroup);

            return Ok(users);
        }

        //PUT: api/<GroupController>/5
        [HttpPut("{groupId}/Permissions")]
        //[TsiAuthorize(Rights.Group_ReadWrite)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        public async Task<IActionResult> UpdatePermissionAsync(string groupId, [FromBody] string[] permissions)
        {
            var group = await _groupManager.FindByIdAsync(groupId);

            if (group is null)
                return NotFound();

            var groupPermissions = await _groupManager.GetPermissionAsync(group);


            var permissionToDeletes = groupPermissions.Where(gp => !permissions.Contains(gp.Key))
                .ToList();


            foreach (var permissionToDelete in permissionToDeletes)
            {
                await _groupManager.RemovePermissionAsync(group, permissionToDelete);
            }

            var permissionEnums = EnumHelper.ParseValues<Permissions>().Where(e => permissions.Contains(e.Key) && !groupPermissions.Select(gp => gp.Key).Contains(e.Key));

            foreach (var permission in permissionEnums)
            {
                await _groupManager.AddPermissionAsync(group, new TsiIdentityPermission()
                {
                    Key = permission.Key,
                    Name = permission.Key,
                    Description = permission.Value,
                });

            }
            await _groupManager.UpdateAsync(group);

            return NoContent();
        }
        [HttpDelete("{groupId}/DeletePermissions")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Delete))]
        public async Task<IActionResult> DeletePermissions(string groupId, [FromBody] string[] keys)
        {
            var group = await _groupManager.FindByIdAsync(groupId);

            if (group is null)
                return NotFound();
            var groupPermissions = await _groupManager.GetPermissionAsync(group);

            foreach (var item in groupPermissions)
            {
                if (keys.Contains(item.Key))
                    await _groupManager.RemovePermissionAsync(group, item);
            }

            await _groupManager.UpdateAsync(group);

            return NoContent();
        }

        [HttpGet("{groupId}/Permissions")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<IActionResult> GetPermissions(string groupId)
        {
            var group = await _groupManager.FindByIdAsync(groupId);

            if (group is null)
                return NotFound();
            var result = EnumHelper.ParseValues<Permissions>();
            var groupPermissions = await _groupManager.GetPermissionAsync(group);

            var result1 = from r in result
                          join gp in groupPermissions on r.Key equals gp.Key into rgp
                          from gp in rgp.DefaultIfEmpty()
                          select new PermissionResponse { Key = r.Key, Value = r.Value, Selected = rgp.Any() };

            return Ok(result1.ToList());
            //  return Ok(groupPermissions);

        }


    }
}
