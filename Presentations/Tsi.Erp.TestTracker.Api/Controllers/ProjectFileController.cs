using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Tsi.Erp.TestTracker.Api.Controllers.Dto.Response;
using Tsi.Erp.TestTracker.Api.Services;
using Tsi.Erp.TestTracker.Core.Services;
using Tsi.Erp.TestTracker.Domain.Stores;

namespace Tsi.Erp.TestTracker.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProjectFileController : ControllerBase
    {
        private readonly ProjectFileService _projectFileService;
        private readonly IMapper _mapper;

        public ProjectFileController(ProjectFileService assemblyService, IMapper mapper)
        {
            _projectFileService = assemblyService;
            _mapper = mapper;
        }

        [HttpGet]
        [TsiAuthorize]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<IActionResult> GetAsync()
        {
            var assemblyFiles = (await _projectFileService.GetAsync());
            return Ok(new { assemblyFiles.FileName,ProjectName = "" });
        }

        
        [HttpDelete()]
        [TsiAuthorize]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Delete))]
        public async Task<IActionResult> DeleteAsync()
        {
            await _projectFileService.DeleteAsync();
            return Ok();
        }

        [HttpPost()]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<IActionResult> ImportAsync([FromForm] IFormFile file,string ProjectName)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            var assemblyBytes = memoryStream.ToArray();

            var assemblyFile = new ProjectFile { FileName = file.FileName, Data = assemblyBytes,ProjectName = ProjectName };
            await _projectFileService.CreateAsync(assemblyFile);

            return Ok();
        }

    }
}
