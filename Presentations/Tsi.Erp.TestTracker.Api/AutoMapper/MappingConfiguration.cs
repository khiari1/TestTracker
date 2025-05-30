using AutoMapper;
using Hangfire.Storage;
using Hangfire.Storage.Monitoring;
using Microsoft.TeamFoundation.TestManagement.WebApi;
using Newtonsoft.Json;
using System.Linq.Extensions;
using Tsi.Erp.TestTracker.Api.Controllers.Dto.Request;
using Tsi.Erp.TestTracker.Api.Controllers.Dto.Response;
using Tsi.Erp.TestTracker.Domain.Stores;
using Tsi.Extensions.Identity.Stores;

namespace Tsi.Erp.TestTracker.Api.AutoMapper
{
    public class MappingConfiguration : Profile
    {
        public MappingConfiguration()
        {
            
            CreateMap<TsiIdentityUser, UserRequest>();
            CreateMap<UserRequest, TsiIdentityUser>();
            CreateMap<UserRequest, ApplicationUser>();
            CreateMap<PermissionResponse, Permissions>();
            CreateMap<Permissions,PermissionResponse>();
            CreateMap<ApplicationUser, UserResponse>();
            CreateMap<ProjectFile, ProjectFileModel>()
                .ReverseMap();           
            CreateMap<Comment, CommentDto>();
                //.ForMember(target => target.IsCurrentUser,source => source.Ignore());
               CreateMap<CommentModel,Comment>();
            CreateMap<Label, LabelsDto>();
            CreateMap<LabelsDto, Label>();

                         
            CreateMap<Module, ModuleDto>();
            CreateMap<Monitoring, MonitoringDto>()
                .ForMember(target => target.TesterName, source => source.MapFrom(user => user.Tester.DisplayName))
                .ForMember(target => target.ResponsibleName, source => source.MapFrom(user => user.Responsible.DisplayName))
              
                .IncludeMembers();



            CreateMap<MonitoringDetail, MonitoringDetailDto>();        
            CreateMap<JobDto, Job>();
            CreateMap<Job, JobDto>();
            CreateMap<ModuleDto, Module>();
            CreateMap<MonitoringModel, Monitoring>();
            CreateMap<MonitoringDetailDto, MonitoringDetail>();

            CreateMap<RecurringJobDto, RecurringJobResponse>()
                .ForMember(target => target.JobName,source => source.MapFrom(s => s.Job.Type.Name + "."+ s.Job.Method.Name ));
            CreateMap<ScheduledJobDto, ScheduledJobResponse>();
            CreateMap<SucceededJobDto, SucceededJobResponse>();
            CreateMap<DeletedJobDto, DeletedJobResponse>();
            CreateMap<FailedJobDto, FailedJobResponse>();

            CreateMap<QueryStore, QueryStoreModel>()
                .ForMember(target => target.Query, source => source.MapFrom(s => JsonConvert.DeserializeObject<Query>(s.SerializedQuery)))
                .ForMember(target => target.UserName, source => source.MapFrom(s => s.User.UserName))
                .IncludeMembers();

            CreateMap<QueryStoreModel, QueryStore>()
    .ForMember(target => target.SerializedQuery, source => source.MapFrom(s => JsonConvert.SerializeObject(s.Query)))
    .IncludeMembers();
            CreateMap<Feature, FeatureDto>();
            CreateMap<FeatureDto, Feature>();
            //CreateMap<RecurringJobDto, RecurringJobResponse>();
        }
        

    }
}
