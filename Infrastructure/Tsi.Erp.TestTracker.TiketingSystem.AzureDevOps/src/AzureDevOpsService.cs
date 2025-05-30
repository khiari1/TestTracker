using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;
using Microsoft.VisualStudio.Services.WebApi.Patch.Json;
using Microsoft.VisualStudio.Services.WebApi.Patch;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.TeamFoundation.Work.WebApi;
using Tsi.Erp.TestTracker.Abstractions;

namespace Tsi.Erp.TestTracker.TiketingSystem.AzureDevOps
{

    public class AzureDevOpsService : ITicketingSystem
    {
        private readonly Uri _organizationUrl;
        private readonly VssCredentials _credentials;

        public AzureDevOpsService(string organizationUrl, string pat)
        {
            _organizationUrl = new Uri(organizationUrl);
            _credentials = new VssBasicCredential(string.Empty, pat);


        }
        public async Task<WorkItem> CreateTestCase(string title, string projectName, string description, string type)
        {
            using (var connection = new VssConnection(_organizationUrl, _credentials))
            {

                var witClient = connection.GetClient<WorkItemTrackingHttpClient>();

                JsonPatchDocument patchDocument = new JsonPatchDocument();
                patchDocument.Add(new JsonPatchOperation()
                {
                    Operation = Operation.Add,
                    Path = "/fields/System.Title",
                    Value = title
                });
                patchDocument.Add(new JsonPatchOperation()
                {
                    Operation = Operation.Add,
                    Path = "/fields/System.AssignedTo",
                    Value = ""
                });
                patchDocument.Add(new JsonPatchOperation()
                {
                    Operation = Operation.Add,
                    Path = "/fields/System.Description",
                    Value = description
                });
                patchDocument.Add(new JsonPatchOperation()
                {
                    Operation = Operation.Add,
                    Path = "/fields/Microsoft.VSTS.TCM.ReproSteps",
                    Value = description
                });
                var createdWorkItem = await witClient.CreateWorkItemAsync(patchDocument, projectName, type);
                return createdWorkItem;
            }
        }
        public async Task<WorkItem> CreateWorkItemAsync(string title, string description, string areaPath, string type, string assignedTo, string projectName)
        {
            using (var connection = new VssConnection(_organizationUrl, _credentials))
            {

                var witClient = connection.GetClient<WorkItemTrackingHttpClient>();

                JsonPatchDocument patchDocument = new JsonPatchDocument();
                patchDocument.Add(new JsonPatchOperation()
                {
                    Operation = Operation.Add,
                    Path = "/fields/System.Title",
                    Value = title
                });
                patchDocument.Add(new JsonPatchOperation()
                {
                    Operation = Operation.Add,
                    Path = "/fields/System.Description",
                    Value = description
                });
                patchDocument.Add(new JsonPatchOperation()
                {
                    Operation = Operation.Add,
                    Path = "/fields/System.AreaPath",
                    Value = areaPath
                });
                patchDocument.Add(new JsonPatchOperation()
                {
                    Operation = Operation.Add,
                    Path = "/fields/System.AssignedTo",
                    Value = assignedTo
                });

                var createdWorkItem = await witClient.CreateWorkItemAsync(patchDocument, projectName, type);
                return createdWorkItem;
            }
        }

        public async Task<WorkItem> CreateWorkItems(WorkItemModel workItems, string type, string projectName)
        {
            using (var connection = new VssConnection(_organizationUrl, _credentials))
            {

                var witClient = connection.GetClient<WorkItemTrackingHttpClient>();

                var patchRequests = new List<JsonPatchDocument>();

                JsonPatchDocument patchDocument = new JsonPatchDocument();


                patchDocument.Add(new JsonPatchOperation()
                {
                    Operation = Operation.Add,
                    Path = "/fields/System.Title",
                    Value = workItems.WorkItemTitle
                });
                patchDocument.Add(new JsonPatchOperation()
                {
                    Operation = Operation.Add,
                    Path = "/fields/System.Description",
                    Value = workItems.AssignedToDisplayName
                });

                patchDocument.Add(new JsonPatchOperation()
                {
                    Operation = Operation.Add,
                    Path = "/fields/System.AssignedTo",
                    Value = " "
                });
                var createdWorkItem = await witClient.CreateWorkItemAsync(patchDocument, projectName, type);
                return createdWorkItem;
            }
        }

        public async Task<bool> UpdateWorkItemState(string projectName, int workItemId, string newState)
        {
            using (var connection = new VssConnection(_organizationUrl, _credentials))
            {

                var witClient = connection.GetClient<WorkItemTrackingHttpClient>();

                var patchDocument = new JsonPatchDocument();
                patchDocument.Add(new JsonPatchOperation()
                {
                    Operation = Operation.Add,
                    Path = "/fields/System.State",
                    Value = newState
                });
                try
                {
                    await witClient.UpdateWorkItemAsync(patchDocument, workItemId);
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
        public async Task<bool> UpdateWorkItemDescription(string projectName, string title, string newDescription)
        {
            using (var connection = new VssConnection(_organizationUrl, _credentials))
            {
                var witClient = connection.GetClient<WorkItemTrackingHttpClient>();

                Wiql wiql = new Wiql
                {
                    Query = $"SELECT [System.Id] " +
                            $"FROM WorkItems WHERE [System.TeamProject] = '{projectName}' " +
                            $"AND [System.Title] = '{title}'"
                };

                WorkItemQueryResult queryResult = await witClient.QueryByWiqlAsync(wiql, projectName);

                if (queryResult.WorkItems.Count() == 0)

                {
                    return false;
                }

                int workItemId = queryResult.WorkItems.FirstOrDefault()?.Id ?? 0;


                JsonPatchDocument patchDocument = new JsonPatchDocument();
                patchDocument.Add(new JsonPatchOperation()
                {
                    Operation = Operation.Add,
                    Path = "/fields/System.Description",
                    Value = newDescription
                });
                patchDocument.Add(new JsonPatchOperation()
                {
                    Operation = Operation.Add,
                    Path = "/fields/Microsoft.VSTS.TCM.ReproSteps",
                    Value = newDescription
                });
                try
                {
                    await witClient.UpdateWorkItemAsync(patchDocument, workItemId);
                    return true;
                }
                catch (Exception ex)
                {
                    // Handle any exceptions here
                    return false;
                }
            }
        }


        public async Task<WorkItem> GetWorkItemAsync(int id)
        {
            using (var connection = new VssConnection(_organizationUrl, _credentials))
            {
                var witClient = connection.GetClient<WorkItemTrackingHttpClient>();
                return await witClient.GetWorkItemAsync(id);
            }
        }

        public async Task<bool> DeleteWorkItemAsync(int id)
        {
            using (var connection = new VssConnection(_organizationUrl, _credentials))
            {
                var witClient = connection.GetClient<WorkItemTrackingHttpClient>();
                await witClient.DeleteWorkItemAsync(id);
                return true;
            }
        }
        public async Task<List<WorkItemModel>> GetWorkItemsByState(string projectName, string state)
        {
            using (var connection = new VssConnection(_organizationUrl, _credentials))
            {
                WorkItemTrackingHttpClient witClient = connection.GetClient<WorkItemTrackingHttpClient>();

                Wiql wiql = new Wiql
                {
                    Query = $"SELECT [System.Id], [System.Title], [System.AssignedTo] " +
                            $"FROM WorkItems WHERE [System.TeamProject] = '{projectName}' " +
                             $" AND[System.State] = '{state}'" +
                            $"ORDER BY [System.ChangedDate] DESC"
                };

                WorkItemQueryResult queryResult = await witClient.QueryByWiqlAsync(wiql, project: projectName);
                List<WorkItemModel> workItems = new List<WorkItemModel>();
                foreach (WorkItemReference workItemReference in queryResult.WorkItems)
                {
                    WorkItem workItem = await witClient.GetWorkItemAsync(workItemReference.Id, expand: WorkItemExpand.Relations);
                    string assignedToDisplayName = ((IdentityRef)workItem.Fields["System.AssignedTo"]).DisplayName.ToString();
                    string workItemTitle = workItem.Fields["System.Title"].ToString();
                    workItems.Add(new WorkItemModel
                    {
                        Id = workItem.Id ?? 0,
                        AssignedToDisplayName = assignedToDisplayName,
                        WorkItemTitle = workItemTitle
                    });
                }

                return workItems;

            }
        }

        public async Task<bool> GetWorkItems(string projectName, string title)
        {
            using (var connection = new VssConnection(_organizationUrl, _credentials))
            {
                var witClient = connection.GetClient<WorkItemTrackingHttpClient>();

                Wiql wiql = new Wiql
                {
                    Query = $"SELECT [System.Id] " +
                            $"FROM WorkItems WHERE [System.TeamProject] = '{projectName}' " +
                            $"AND [System.Title] = '{title}'"
                };

                WorkItemQueryResult queryResult = await witClient.QueryByWiqlAsync(wiql, project: projectName);

                if (queryResult.WorkItems.Count() == 0)
                {
                    return false;
                }
                return true;
            }
        }

        public Task<List<WebApiTeam>> GetTeamsAsync(string projectName)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Guid>> GetProjectIds(string projectName)
        {
            throw new NotImplementedException();
        }

        public Task<List<Member>> GetTeamMembersAsync(string projectId, string teamId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Guid>> GetTeamIds(string projectName)
        {
            throw new NotImplementedException();
        }

        public Task<List<string>> GetAllTeamMembersAsync(string projectName)
        {
            throw new NotImplementedException();
        }
    }
}

