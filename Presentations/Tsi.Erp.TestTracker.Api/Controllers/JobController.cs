
using Microsoft.AspNetCore.Mvc;
using System.Linq.Extensions;
using Tsi.Erp.TestTracker.Core.Services;

namespace Tsi.Erp.TestTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {

        private readonly JobManager _jobManager;

        public JobsController(JobManager jobManager)
        {
            _jobManager = jobManager;
        }

        #region Get actions

        [HttpPost("Reccuring/Query")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<IActionResult> GetReccuringJobsAsync(Query filter)
        {
            return Ok(await _jobManager.GetReccuringJobsAsync(filter));
        }

        [HttpPost("Succeeded/Query")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<IActionResult> GetSucceededJosbAsync(Query filter)
        {

            return Ok(await _jobManager.GetSucceededJosbAsync(filter));
        }

        [HttpPost("Failed/Query")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<IActionResult> GetFailedJobsAsync(Query filter)
        {            
            return Ok(await _jobManager.GetFailedJobsAsync(filter));
        }
        [HttpPost("Enqueued/Query")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<IActionResult> GetEnqueuedJobsAsync(Query filter)
        {
            return Ok(await _jobManager.GetEnqueuedJobsAsync(filter));
        }
        [HttpPost("Processing/Query")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<IActionResult> GetProcessingJobsAsync(Query filter)
        {           
            return Ok(await Task.FromResult(await _jobManager.GetProcessingJobsAsync(filter)));
        }

        [HttpPost("Scheduled/Query")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<IActionResult> GetScheduledJobsAsync(Query filter)
        {
            return Ok(await _jobManager.GetScheduledJobsAsync(filter));
        }

        #endregion

        #region Post actions
        [HttpPost("Requeue")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<IActionResult> RequeuJobsAsync([FromBody]string[] ids)
        {
            await _jobManager.RequeuJobsAsync(ids);

            return Ok();
        }
        [HttpPost("Reccuring/Trigger")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<IActionResult> TriggerReccuringJobsAsync([FromBody] string[] recurringids)
        {
            await _jobManager.TriggerReccuringJobsAsync(recurringids);            
            return Ok();
        }

        [HttpPost("Reschedule")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<IActionResult> RescheduleJobsAsync(string id, DateTimeOffset enqueueAt)
        {
            await _jobManager.RescheduleJobsAsync(id, enqueueAt: enqueueAt);

            return Ok();
        }
        #endregion

        #region Delete actions
        [HttpDelete("{DeleteJobs}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Delete))]
        public async Task<IActionResult> DeleteJobsAsync(string[] ids)
        {
            await _jobManager.DeleteJobsAsync(ids); 
            return Ok();
        }


        [HttpDelete("Reccuring")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Delete))]
        public async Task<IActionResult> DeleteReccuringJobsAsync(string[] ids)
        {
            await _jobManager.DeleteReccuringJobsAsync(ids);
            return Ok();
        }
        #endregion
    }
}
