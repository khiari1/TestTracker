

//using Microsoft.AspNetCore.Mvc;
//using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
//using Microsoft.VisualStudio.Services.WebApi;
//using System.Net;
//using Tsi.Erp.TestTracker.Core.Abstractions;

//namespace Tsi.Erp.TestTracker.Api.Controllers
//{

//    [Route("api/workitems")]
//    [ApiController]
//    public class WorkItemController : ControllerBase
//    {
//        private readonly IAzureDevOpsService _azureDevOpsService;
//        private readonly IMemberManagement _memberManagemntService;
//        public WorkItemController(IAzureDevOpsService azureDevOpsService, IMemberManagement memberManagemntService)
//        {
//            _azureDevOpsService = azureDevOpsService;
//            _memberManagemntService = memberManagemntService;
//        }

//        [HttpPost]
//        public async Task<IActionResult> CreateWorkItem( string title, string description, string areaPath, string type,string assignedTo, string projectName)
//        {       
//            WorkItem workItem = await _azureDevOpsService.CreateWorkItemAsync(title, description, areaPath,  type, assignedTo, projectName);
//            return Ok(workItem);
//        }

//        [HttpGet]
//        public async Task<ActionResult<WorkItem>> GetWorkItem(int workItemId)
//        {
//            WorkItem workItem = await _azureDevOpsService.GetWorkItemAsync(workItemId);
//            if (workItem != null)
//            {
//                return Ok(workItem);
//            }
//            else
//            {
//                return NotFound();
//            }
//        }

//        [HttpPut("{id}/state")]
//        public async Task<IActionResult> UpdateWorkItemState(string projectName,int id, string newState)
//        {
//            var result = await _azureDevOpsService.UpdateWorkItemState(projectName,id, newState);

//                return Ok(result);
           
//        }

//        [HttpDelete("{workItemId}")]
//        public async Task<IActionResult> DeleteWorkItem(int workItemId)
//        {
//            bool result = await _azureDevOpsService.DeleteWorkItemAsync(workItemId);
//            if (result)
//            {
//                return Ok("Work Item Deleted successfully");
//            }
//            else
//            {
//                return NotFound();
//            }
//        }

//        [HttpGet("workitems/{state}")]
//        public async Task<ActionResult<List<WorkItemModel>>> GetWorkItemsByState(string projectName ,string state)
//        {
         
//            try
//            {
//                List<WorkItemModel> workItems = await _azureDevOpsService.GetWorkItemsByState(projectName, state);
//                return Ok(workItems);
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, $"An error occurred: {ex.Message}");
//            }
//        }



//        [HttpPost("CreateTestCases")]
//        public async Task<IActionResult> CreateTestCases(string ProjectName, string state)
//        {
//            List<WorkItemModel> workItems = await _azureDevOpsService.GetWorkItemsByState(ProjectName, state);
//            foreach (WorkItemModel workItemR in workItems)
//            {
//                var type = "Test Case";
//                var projectName = "TSI_ERP_TEST_TRACKER";
//                WorkItem workItem = await _azureDevOpsService.CreateWorkItems(  workItemR, type, projectName);
//            }
//            return Ok(workItems.Count());
//        }
//    }
    

//}

    