using Microsoft.AspNetCore.Mvc;
using Tsi.Erp.TestTracker.Domain.Repositories;

namespace Tsi.Erp.TestTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourcesController : ControllerBase
    {
        private readonly IResourceRepository _resourceRepository;
        public ResourcesController(IResourceRepository resourceRepository)
        {
            _resourceRepository = resourceRepository;
        }

        [HttpGet("GetModules/{searchPattern?}")]
        [TsiAuthorize]
        public async Task<IActionResult> GetModules(string? searchPattern)=>
            new ObjectResult(await _resourceRepository.GetModules(searchPattern)) ;
    
        [HttpGet("Users/{searchPattern?}")]
        [TsiAuthorize]
        public async Task<IActionResult> GetUsersAsync(string? searchPattern) =>
            new ObjectResult(await _resourceRepository.GetUsersAsync(searchPattern));
    }
}
