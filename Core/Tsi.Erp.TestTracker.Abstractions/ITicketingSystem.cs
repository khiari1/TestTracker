using Microsoft.TeamFoundation.Work.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;


namespace Tsi.Erp.TestTracker.Abstractions
{
    public interface ITicketingSystem
    {
        Task<WorkItem> CreateWorkItemAsync(string title, string description, string areaPath, string type, string assignedTo, string projectName);
        Task<WorkItem> CreateWorkItems(WorkItemModel workItems, string type, string projectName);
        Task<WorkItem> CreateTestCase(string title, string projectName, string description, string type);
        Task<bool> UpdateWorkItemState(string projectName, int workItemId, string newState);
        Task<bool> UpdateWorkItemDescription(string projectName, string title, string newDescription);
        Task<bool> GetWorkItems(string projectName, string title);
        Task<WorkItem> GetWorkItemAsync(int id);
        Task<bool> DeleteWorkItemAsync(int id);
        Task<List<WorkItemModel>> GetWorkItemsByState(string projectName, string state);
        Task<List<Microsoft.TeamFoundation.Core.WebApi.WebApiTeam>> GetTeamsAsync(string projectName);
        Task<IEnumerable<Guid>> GetProjectIds(string projectName);
        Task<List<Member>> GetTeamMembersAsync(string projectId, string teamId);
        Task<IEnumerable<Guid>> GetTeamIds(string projectName);

        Task<List<string>> GetAllTeamMembersAsync(string projectName);
    }

}
public class WorkItemModel
{

    public int Id { get; set; }

    public string? AssignedToDisplayName { get; set; }
    public string? WorkItemTitle { get; set; }
}
