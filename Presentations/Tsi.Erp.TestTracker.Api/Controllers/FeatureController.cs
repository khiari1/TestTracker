using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Extensions;
using Tsi.Erp.TestTracker.Api.Controllers.Dto.Response;
using Tsi.Erp.TestTracker.Domain.Repositories;
using Tsi.Erp.TestTracker.Domain.Stores;
namespace Tsi.Erp.TestTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeatureController : ControllerBase
    {
        private readonly IGenericRepository<Feature> _featureRepository;
        private readonly IMapper _mapper;
        public FeatureController(IGenericRepository<Feature> featuresRepository, IMapper mapper)
        {
            _featureRepository = featuresRepository;
            _mapper = mapper;
        }
        // GET: api/<FeatureController>
        [HttpGet()]
        [TsiAuthorize(Permissions.Feature_Read)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<IActionResult> GetAsync()
        {
            var feature = (await _featureRepository.GetAsync()).ToList();
            var featureResults = _mapper.Map<List<Feature>, List<FeatureDto>>(feature);
            return Ok(featureResults);
        }

        [HttpPost()]
        [TsiAuthorize(Permissions.Feature_ReadWrite)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<IActionResult> PostAsync([FromBody] FeatureDto feature)
        {
            var featureResult = _mapper.Map<FeatureDto, Feature>(feature);

            _featureRepository.Create(featureResult);

            await _featureRepository.SaveAsync();

            return CreatedAtAction(nameof(GetAsync),
                new { id = featureResult.Id },
                _mapper.Map(featureResult, feature));
        }
        [HttpPost("Query")]
        [TsiAuthorize(Permissions.Feature_ReadWrite)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<IActionResult> PostAsync([FromBody] Query filter)
        {
            var feature = await _featureRepository.GetAsync(filter);
            var featureResults = _mapper.Map<List<FeatureDto>>(feature);
            return Ok(featureResults);
        }

        [HttpGet("{id}")]
        [TsiAuthorize(Permissions.Feature_Read)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<IActionResult> GetAsync(int id)
        {
            var feature = await Task.FromResult(_featureRepository.Find(id));
            if (feature is null)
            {
                return NotFound();
            }
            var featureResults = _mapper.Map<Feature, FeatureDto>(feature);
            return Ok(featureResults);

        }
        [HttpPut("{id}")]
        [TsiAuthorize(Permissions.Feature_ReadWrite)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        public async Task<IActionResult> PutAsync(int id, [FromBody] FeatureDto FeatureDto)
        {
            var feature = _featureRepository.Find(id);
            if (feature is null)
            {
                return NotFound();
            }
            var functionalityResult = _mapper.Map(FeatureDto, feature);

            _featureRepository.Update(functionalityResult);
            await _featureRepository.SaveAsync();

            return NoContent();
        }
        [HttpDelete("{id}")]
        [TsiAuthorize(Permissions.Feature_ReadWrite)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Delete))]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var feature = _featureRepository.Find(id);
            if (feature is null)
            {
                return NotFound();
            }
            _featureRepository.Delete(id);
            await _featureRepository.SaveAsync();
            return Ok();
        }

        [HttpDelete()]
        [TsiAuthorize(Permissions.Feature_ReadWrite)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Delete))]
        public async Task<IActionResult> DeleteAsync(int[] id)
        {
            await _featureRepository.DeleteRangeAsync(id);
            await _featureRepository.SaveAsync();
            return Ok();
        }

    }
}
