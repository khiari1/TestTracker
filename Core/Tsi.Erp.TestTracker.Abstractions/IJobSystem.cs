using System.Linq.Extensions;
using Tsi.Erp.TestTracker.Abstractions.Models.Job;

namespace Tsi.Erp.TestTracker.Abstractions
{
    public interface IJobSystem
    {
        Task<IEnumerable<RecurringJobResponse>> ReccuringJobsAsync(Query filter);
        Task<IEnumerable<SucceededJobResponse>> SucceededJosbAsync(Query filter);
        Task<IEnumerable<FailedJobResponse>> FailedJobsAsync(Query filter);
        Task<IEnumerable<EnqueuedJobResponse>> EnqueuedJobsAsync(Query filter);
        Task<IEnumerable<ProcessingJobResponse>> ProcessingJobsAsync(Query filter);
        Task<IEnumerable<ScheduledJobResponse>> ScheduledJobsAsync(Query filter);
        void RequeuJobs(string[] ids);
        void TriggerReccuringJobs(string[] recurringids);
        void RescheduleJobs(string id, DateTimeOffset enqueueAt);
        public void DeleteJobs(string[] ids);
        public void DeleteReccuringJobs(string[] ids);

    }
}
