using AutoMapper;
using Hangfire.Storage;
using Hangfire.Storage.Monitoring;
using Tsi.Erp.TestTracker.Abstractions.Models.Job;

namespace Tsi.Erp.TestTracker.Hangfire
{
    public class MappingConfiguration : Profile
    {
        public MappingConfiguration()
        {


            CreateMap<RecurringJobDto, RecurringJobResponse>()
                .ForMember(target => target.JobName, source => source.MapFrom(s => s.Job.Type.Name + "." + s.Job.Method.Name));
            CreateMap<ScheduledJobDto, ScheduledJobResponse>();
            CreateMap<SucceededJobDto, SucceededJobResponse>();
            CreateMap<DeletedJobDto, DeletedJobResponse>();
            CreateMap<FailedJobDto, FailedJobResponse>();
            //CreateMap<RecurringJobDto, RecurringJobResponse>();
        }


    }
}
