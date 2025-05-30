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
        public class LabelsController : ControllerBase
        {
            private readonly IGenericRepository<Label> _labelsRepository;
            private readonly IMapper _mapper;

            public LabelsController(IGenericRepository<Label> labelsRepository, IMapper mapper)
            {
                _labelsRepository = labelsRepository;
                _mapper = mapper;
            }

            // GET: api/Labels
            [HttpGet]
            [TsiAuthorize(Permissions.Labels_Read)]
            [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
            public async Task<IActionResult> GetAsync()
            {
                var labels = (await _labelsRepository.GetAsync()).ToList();
                var labelResults = _mapper.Map<List<LabelsDto>>(labels);
                return Ok(labelResults);
            }

            // GET: api/Labels/5
            [HttpGet("{id}")]
            [TsiAuthorize(Permissions.Labels_Read)]
            [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
            public async Task<IActionResult> GetAsync(int id)
            {
                var label = _labelsRepository.Find(id);
                if (label is null)
                    return NotFound();

                var labelResult = _mapper.Map<LabelsDto>(label);
                return Ok(labelResult);
            }

            // POST: api/Labels
            [HttpPost]
            [TsiAuthorize(Permissions.Labels_ReadWrite)]
            [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
            public async Task<IActionResult> PostAsync([FromBody] LabelsDto dto)
            {
                var label = _mapper.Map<Label>(dto);
                _labelsRepository.Create(label);
                await _labelsRepository.SaveAsync();

                return CreatedAtAction(nameof(GetAsync), new { id = label.Id }, _mapper.Map<LabelsDto>(label));
            }

            // POST: api/Labels/Query
            [HttpPost("Query")]
            [TsiAuthorize(Permissions.Labels_ReadWrite)]
            [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
            public async Task<IActionResult> PostAsync([FromBody] Query filter)
            {
                var labels = await _labelsRepository.GetAsync(filter);
                var labelResults = _mapper.Map<List<LabelsDto>>(labels);
                return Ok(labelResults);
            }
          
            [HttpPut("{id}")]
            [TsiAuthorize(Permissions.Labels_ReadWrite)]
            [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
            public async Task<IActionResult> PutAsync(int id, [FromBody] LabelsDto dto)
            {
                var label = _labelsRepository.Find(id);
                if (label is null)
                    return NotFound();

                _mapper.Map(dto, label);
                _labelsRepository.Update(label);
                await _labelsRepository.SaveAsync();

                return NoContent();
            }

            [HttpDelete("{id}")]
            [TsiAuthorize(Permissions.Labels_ReadWrite)]
            [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Delete))]
            public async Task<IActionResult> DeleteAsync(int id)
            {
                var label = _labelsRepository.Find(id);
                if (label is null)
                    return NotFound();

                _labelsRepository.Delete(id);
                await _labelsRepository.SaveAsync();

                return Ok();
            }

            // DELETE: api/Labels
            [HttpDelete]
            [TsiAuthorize(Permissions.Labels_ReadWrite)]
            [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Delete))]
            public async Task<IActionResult> DeleteRangeAsync([FromBody] int[] ids)
            {
                await _labelsRepository.DeleteRangeAsync(ids);
                await _labelsRepository.SaveAsync();

                return Ok();
            }
        }
    }

