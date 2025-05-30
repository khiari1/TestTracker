using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq.Extensions;
using Tsi.AspNetCore.Identity.AzureAD;
using Tsi.Erp.TestTracker.Api.Controllers.Dto.Request;
using Tsi.Erp.TestTracker.Api.Controllers.Dto.Response;
using Tsi.Erp.TestTracker.Domain.Repositories;
using Tsi.Erp.TestTracker.Domain.Stores;
using Tsi.Extensions.Identity.Core;
using Tsi.Extensions.Identity.Stores;

namespace Tsi.Erp.TestTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueryStoresController : ControllerBase

    {
        private readonly IGenericRepository<QueryStore> _queryStoreRepository;
        private readonly IMapper _mapper;
        private readonly UserAzureADManager<ApplicationUser, TsiIdentityPermission> _userAzureAdManager;
        public QueryStoresController(IGenericRepository<QueryStore> QueryStoreRepository, IMapper mapper, UserAzureADManager<ApplicationUser, TsiIdentityPermission> userAzureAdManager)
        {
            _queryStoreRepository = QueryStoreRepository;
            _mapper = mapper;
            _userAzureAdManager = userAzureAdManager;
        }
        // GET: api/<QueryStoreController>    
        [HttpGet()]
        [TsiAuthorize(Permissions.QueryStore_Read)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<IActionResult> GetAsync()
        {
            var QueryStore = (await _queryStoreRepository.GetAsync()).ToList();
            var QueryStoreResults = _mapper.Map<List<QueryStore>, List<QueryStoreModel>>(QueryStore);
            return Ok(QueryStoreResults);
        }
        // GET api/<QueryStoreController>/5

        [HttpGet("{id}")]
        [TsiAuthorize(Permissions.QueryStore_Read)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<IActionResult> GetAsync(int id)
        {
            var queryStore = await Task.FromResult(_queryStoreRepository.Find(id));
            if (queryStore is null)
            {
                return NotFound();
            }
            var QueryStoreResult = _mapper.Map<QueryStore, QueryStoreModel>(queryStore);

            QueryStoreResult.Query = JsonConvert.DeserializeObject<Query>(queryStore.SerializedQuery);

            return Ok(QueryStoreResult);

        }

        // POST api/<QueryStoreController>
        [HttpPost()]
        [TsiAuthorize(Permissions.QueryStore_ReadWrite)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<IActionResult> PostAsync([FromBody] QueryStoreModel QueryStore)
        {
            var QueryStoreResult = _mapper.Map<QueryStoreModel, QueryStore>(QueryStore);
            QueryStoreResult.UserId = _userAzureAdManager.GetUserId(HttpContext.User);
            _queryStoreRepository.Create(QueryStoreResult);

            await _queryStoreRepository.SaveAsync();

            return CreatedAtAction(nameof(GetAsync),
                new { id = QueryStoreResult.Id },
                _mapper.Map(QueryStoreResult, QueryStore));
        }

        // GET: api/<QueryStoreController>    
        [HttpPost("Query")]
        [TsiAuthorize(Permissions.QueryStore_Read)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<IActionResult> GetAsync(Query filter)
        {
            var QueryStore = (await _queryStoreRepository.GetAsync(filter)).ToList();
            var QueryStoreResults = _mapper.Map<List<QueryStore>, List<QueryStoreModel>>(QueryStore);
            return Ok(QueryStoreResults);
        }

        // PUT api/<QueryStoreController>/5
        [HttpPut("{id}")]
        [TsiAuthorize(Permissions.QueryStore_ReadWrite)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        public async Task<IActionResult> PutAsync(int id, [FromBody] QueryStoreModel QueryStoreDto)
        {
            var QueryStore = _queryStoreRepository.Find(id);
            if (QueryStore is null)
            {
                return NotFound();
            }
            var QueryStoreResult = _mapper.Map(QueryStoreDto, QueryStore);

            _queryStoreRepository.Update(QueryStoreResult);
            await _queryStoreRepository.SaveAsync();

            return NoContent();
        }

        // DELETE api/<QueryStoreController>/5
        [HttpDelete("{id}")]
        [TsiAuthorize(Permissions.QueryStore_ReadWrite)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Delete))]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var QueryStore = _queryStoreRepository.Find(id);
            if (QueryStore is null)
            {
                return NotFound();
            }
            _queryStoreRepository.Delete(id);
            await _queryStoreRepository.SaveAsync();
            return Ok();
        }

        [HttpDelete()]
        [TsiAuthorize(Permissions.Functionality_ReadWrite)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Delete))]
        public async Task<IActionResult> DeleteAsync(int[] id)
        {
            await _queryStoreRepository.DeleteRangeAsync(id);
            await _queryStoreRepository.SaveAsync();
            return Ok();
        }


    }
}
