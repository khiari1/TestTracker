using System.Linq.Extensions;
using Tsi.Erp.TestTracker.Abstractions;
using Tsi.Erp.TestTracker.Abstractions.Models.Job;

namespace Tsi.Erp.TestTracker.Core.Services
{
    public class JobManager
    {

        private readonly IJobSystem _jobSystem;

        public JobManager(IJobSystem jobSystem)
        {
            _jobSystem = jobSystem;
        }

        public async Task<IEnumerable<RecurringJobResponse>> GetReccuringJobsAsync(Query filter) =>
            await _jobSystem.ReccuringJobsAsync(filter);
        public async Task<IEnumerable<SucceededJobResponse>> GetSucceededJosbAsync(Query filter) =>
            await _jobSystem.SucceededJosbAsync(filter);
        public async Task<IEnumerable<FailedJobResponse>> GetFailedJobsAsync(Query filter) =>
            await _jobSystem.FailedJobsAsync(filter);

        public async Task<IEnumerable<EnqueuedJobResponse>> GetEnqueuedJobsAsync(Query filter) =>
            await _jobSystem.EnqueuedJobsAsync(filter);

        public async Task<IEnumerable<ProcessingJobResponse>> GetProcessingJobsAsync(Query filter) =>
            await _jobSystem.ProcessingJobsAsync(filter);

        public async Task<IEnumerable<ScheduledJobResponse>> GetScheduledJobsAsync(Query filter) =>
            await _jobSystem.ScheduledJobsAsync(filter);


        public async Task RequeuJobsAsync(string[] ids) =>
            await Task.Run(() => _jobSystem.RequeuJobs(ids));

        public async Task TriggerReccuringJobsAsync(string[] recurringids) =>
            await Task.Run(() => _jobSystem.TriggerReccuringJobs(recurringids));

        public async Task RescheduleJobsAsync(string id, DateTimeOffset enqueueAt) =>
            await Task.Run(() => _jobSystem.RescheduleJobs(id, enqueueAt));

        public async Task DeleteJobsAsync(string[] ids) =>
            await Task.Run(() => _jobSystem.DeleteJobs(ids));
        public async Task DeleteReccuringJobsAsync(string[] ids) =>
            await Task.Run(() => _jobSystem.DeleteReccuringJobs(ids));
    }
}
