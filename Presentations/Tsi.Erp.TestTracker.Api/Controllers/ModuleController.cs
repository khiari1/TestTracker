using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Extensions;
using Tsi.Erp.TestTracker.Api.Controllers.Dto.Response;
using Tsi.Erp.TestTracker.Domain.Repositories;
using Tsi.Erp.TestTracker.Domain.Stores;
using Tsi.Erp.TestTracker.EntityFrameworkCore.Repositories;

namespace Tsi.Erp.TestTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModulesController : ControllerBase

    {
        private readonly IGenericRepository<Module> _moduleRepository;
        private readonly IMapper _mapper;
        public ModulesController(IGenericRepository<Module> moduleRepository, IMapper mapper)
        {
            _moduleRepository = moduleRepository;
            _mapper = mapper;
        }
        // GET: api/<ModuleController>    
        [HttpGet()]
        [TsiAuthorize(Permissions.Module_Read)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<IActionResult> GetAsync()
        {
            var module = (await _moduleRepository.GetAsync()).ToList();
            var moduleResults = _mapper.Map<List<Module>, List<ModuleDto>>(module);
            return Ok(moduleResults);
        }
        // GET api/<ModuleController>/5

        [HttpGet("{id}")]
        [TsiAuthorize(Permissions.Module_Read)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<IActionResult> GetAsync(int id)
        {
            var module = await Task.FromResult(_moduleRepository.Find(id));
            if (module is null)
            {
                return NotFound();
            }
            var moduleResult = _mapper.Map<Module, ModuleDto>(module);
            return Ok(moduleResult);

        }

        // POST api/<ModuleController>
        [HttpPost()]
        [TsiAuthorize(Permissions.Module_ReadWrite)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<IActionResult> PostAsync([FromBody] ModuleDto module)
        {
            var moduleResult = _mapper.Map<ModuleDto, Module>(module);

            _moduleRepository.Create(moduleResult);

            await _moduleRepository.SaveAsync();

            return CreatedAtAction(nameof(GetAsync),
                new { id = moduleResult.Id },
                _mapper.Map(moduleResult, module));
        }

        // GET: api/<ModuleController>    
        [HttpPost("Query")]
        [TsiAuthorize(Permissions.Module_Read)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<IActionResult> GetAsync(Query filter)
        {
            var module = (await _moduleRepository.GetAsync(filter)).ToList();
            var moduleResults = _mapper.Map<List<Module>, List<ModuleDto>>(module);
            return Ok(moduleResults);
        }

 

        // DELETE api/<ModuleController>/5
        [HttpDelete("{id}")]
        [TsiAuthorize(Permissions.Module_ReadWrite)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Delete))]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var module = _moduleRepository.Find(id);
            if (module is null)
            {
                return NotFound();
            }
            _moduleRepository.Delete(id);
            await _moduleRepository.SaveAsync();
            return Ok();
        }



    }
}
