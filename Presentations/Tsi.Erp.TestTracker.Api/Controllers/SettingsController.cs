using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Diagnostics.CodeAnalysis;
using Tsi.Erp.TestTracker.Api.Controllers.Dto.Request;
using Tsi.Erp.TestTracker.Api.Controllers.Dto.Response;
using Tsi.Erp.TestTracker.Api.Services;
using Tsi.Erp.TestTracker.Core.Services;

namespace Tsi.Erp.TestTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController: ControllerBase
    {
        private readonly SettingsService _settingsService;
        public SettingsController(SettingsService settingsService) {
            _settingsService = settingsService;
        }

        [HttpPost("AzureDevops")]
        [TsiAuthorize(Permissions.Settings_ReadWrite)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<IActionResult> SaveAsync([FromBody] AzureDevops azuredevops)
        {
           await _settingsService.SaveAsync(azuredevops);
            return Ok();

        }
        [HttpPost("Project")]
        [TsiAuthorize(Permissions.Settings_ReadWrite)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<IActionResult> SaveAsync([FromBody] Project project)
        {
            await _settingsService.SaveAsync(project);
            return Ok();

        }
        [HttpPost("Team")]
        [TsiAuthorize(Permissions.Settings_ReadWrite)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<IActionResult> SaveAsync([FromBody] Team team)
        {
            await _settingsService.SaveAsync(team);
            return Ok();

        }
        [HttpGet("Project")]
        [TsiAuthorize(Permissions.Settings_Read)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<IActionResult> GetAsync()
        {
            
            var Result = await _settingsService.GetSetting<Project>();
            return Ok(Result);
        }
        [HttpGet("AzureDevops")]
        [TsiAuthorize(Permissions.Settings_Read)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<IActionResult> GetADAsync()
        {

            var Result = await _settingsService.GetSetting<AzureDevops>();
            return Ok(Result);
        }
        [HttpGet("Team")]
        [TsiAuthorize(Permissions.Settings_Read)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<IActionResult> GetTeamAsync()
        {

            var Result = await _settingsService.GetSetting<Team>();
            return Ok(Result);
        }

        [HttpGet("TestProjectInfo")]
        [TsiAuthorize(Permissions.Settings_Read)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<IActionResult> TestProjectInfoAsync()
        {

            var Result = await _settingsService.GetSetting<TestProject>();
            return Ok(new {Result.FileName , Result.ProjectName});
        }

        [HttpPost("UploadAssemblyTestProject")]
        [TsiAuthorize(Permissions.Settings_Read)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<IActionResult> UploadProjectAsync([FromForm] IFormFile file)
        {
            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            var data = memoryStream.ToArray();
            var assemblyFile = new TestProject { FileName = file.FileName, Data = data,ProjectName = "Test",Size = 0 };
            await _settingsService.SaveAsync(assemblyFile);
            return Ok();
        }
    }
}
