using AutoMapper;
using Hangfire;
using Hangfire.Storage;
using System.Linq.Extensions;
using Tsi.Erp.TestTracker.Abstractions;
using Tsi.Erp.TestTracker.Abstractions.Models.Job;

namespace Tsi.Erp.TestTracker.Hangfire
{
    public class HangfireJob : IJobSystem
    {
        private readonly IMapper _mapper;
        private readonly IRecurringJobManager _recurringJobManager;
        private readonly JobStorage _jobStorage;
        private readonly IBackgroundJobClient _backgroundJobClient;

        public HangfireJob(JobStorage jobStorage,
                              IMapper mapper,
                              IRecurringJobManager recurringJobManager,
                              IBackgroundJobClient backgroundJobClient)
        {
            _recurringJobManager = recurringJobManager;
            _mapper = mapper;
            _jobStorage = jobStorage;
            _backgroundJobClient = backgroundJobClient;

        }

        public async Task<IEnumerable<RecurringJobResponse>> ReccuringJobsAsync(Query filter)
        {
            var jobs = _jobStorage
                .GetConnection()
                .GetRecurringJobs()
                .Where(filter);

            var jobResults = _mapper.Map<List<RecurringJobResponse>>(jobs);
            return await Task.FromResult(jobResults);
        }

        public async Task<IEnumerable<SucceededJobResponse>> SucceededJosbAsync(Query filter)
        {
            var jobs = _jobStorage
                .GetMonitoringApi()
                .SucceededJobs(0, int.MaxValue)
                .Where(filter)
                .Select(s => new SucceededJobResponse()
                {
                    InvocationData = new Abstractions.Models.Job.InvocationData(s.Value.InvocationData.Type, s.Value.InvocationData.Method, s.Value.InvocationData.ParameterTypes, s.Value.InvocationData.Arguments, s.Value.InvocationData.Queue),
                    InSucceededState = s.Value.InSucceededState,
                    LoadException = s.Value.LoadException,
                    SucceededAt = s.Value.SucceededAt,
                    Id = s.Key,
                    Result = s.Value.Result,
                    StateData = s.Value.StateData,
                    TotalDuration = s.Value.TotalDuration,
                });

            return await Task.FromResult(jobs);
        }

        public async Task<IEnumerable<FailedJobResponse>> FailedJobsAsync(Query filter)
        {
            var jobs = _jobStorage
                .GetMonitoringApi()
                .FailedJobs(0, int.MaxValue)
                .Where(filter)
                .Select(s => new FailedJobResponse()
                {
                    InvocationData = new Abstractions.Models.Job.InvocationData(s.Value.InvocationData.Type,
                                                                                s.Value.InvocationData.Method,
                                                                                s.Value.InvocationData.ParameterTypes,
                                                                                s.Value.InvocationData.Arguments,
                                                                                s.Value.InvocationData.Queue),
                    StateData = s.Value.StateData,
                    Id = s.Key,
                    LoadException = s.Value.LoadException,
                    ExceptionDetails = s.Value.ExceptionDetails,
                    ExceptionMessage = s.Value.ExceptionMessage,
                    ExceptionType = s.Value.ExceptionType,
                    FailedAt = s.Value.FailedAt,
                    InFailedState = s.Value.InFailedState,
                    Reason = s.Value.Reason
                });
            return await Task.FromResult(jobs);
        }

        public async Task<IEnumerable<EnqueuedJobResponse>> EnqueuedJobsAsync(Query filter)
        {
            var jobs = _jobStorage
                .GetMonitoringApi()
                .EnqueuedJobs("", 0, 0)
                .Where(filter)
                .Select(s => new EnqueuedJobResponse()
                {
                    LoadException = s.Value.LoadException,
                    EnqueuedAt = s.Value.EnqueuedAt,
                    Id = s.Key,
                    InEnqueuedState = s.Value.InEnqueuedState,
                    InvocationData = new Abstractions.Models.Job.InvocationData(s.Value.InvocationData.Type,
                                                                                s.Value.InvocationData.Method,
                                                                                s.Value.InvocationData.ParameterTypes,
                                                                                s.Value.InvocationData.Arguments,
                                                                                s.Value.InvocationData.Queue),
                    State = s.Value.State,
                    StateData = s.Value.StateData,
                });
            return await Task.FromResult(jobs);
        }

        public async Task<IEnumerable<ProcessingJobResponse>> ProcessingJobsAsync(Query filter)
        {
            var jobs = _jobStorage
                .GetMonitoringApi()
                .ProcessingJobs(0, int.MaxValue)
                .Where(filter)
                .Select(s => new ProcessingJobResponse()
                {
                    LoadException = s.Value.LoadException,
                    Id = s.Key,
                    InProcessingState = s.Value.InProcessingState,
                    InvocationData = new Abstractions.Models.Job.InvocationData(s.Value.InvocationData.Type,
                                                                                s.Value.InvocationData.Method,
                                                                                s.Value.InvocationData.ParameterTypes,
                                                                                s.Value.InvocationData.Arguments,
                                                                                s.Value.InvocationData.Queue),
                    StartedAt = s.Value.StartedAt,
                    StateData = s.Value.StateData,
                });
            return await Task.FromResult(jobs);
        }

        public async Task<IEnumerable<ScheduledJobResponse>> ScheduledJobsAsync(Query filter)
        {
            var jobs = _jobStorage
                .GetMonitoringApi()
                .ScheduledJobs(0, int.MaxValue)
                .Where(filter)
                .Select(s => new ScheduledJobResponse()
                {
                    EnqueueAt = s.Value.EnqueueAt,
                    Id = s.Key,
                    InScheduledState = s.Value.InScheduledState,
                    InvocationData = new Abstractions.Models.Job.InvocationData(s.Value.InvocationData.Type,
                                                                                s.Value.InvocationData.Method,
                                                                                s.Value.InvocationData.ParameterTypes,
                                                                                s.Value.InvocationData.Arguments,
                                                                                s.Value.InvocationData.Queue),
                    LoadException = s.Value.LoadException,
                    ScheduledAt = s.Value.ScheduledAt,
                    StateData = s.Value.StateData,
                });

            return await Task.FromResult(jobs);
        }

        public void RequeuJobs(string[] ids)
        {
            foreach (var id in ids)
            {
                _backgroundJobClient.Requeue(id);
            }
        }

        public void TriggerReccuringJobs(string[] recurringids)
        {
            foreach (var id in recurringids)
            {
                  _recurringJobManager.Trigger(id);
            }

        }

        public void RescheduleJobs(string id, DateTimeOffset enqueueAt)
        {
            _backgroundJobClient.Reschedule(id, enqueueAt: enqueueAt);

        }

        public void DeleteJobs(string[] ids)
        {
            foreach (var id in ids)
            {
                _backgroundJobClient.Delete(id);
            }
        }

        public void DeleteReccuringJobs(string[] ids)
        {
            foreach (var id in ids)
            {
                _recurringJobManager.RemoveIfExists(id);
            }
        }
    }
}
