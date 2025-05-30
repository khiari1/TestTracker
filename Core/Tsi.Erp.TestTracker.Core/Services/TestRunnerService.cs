//using Microsoft.AspNetCore.SignalR;
//using Microsoft.VisualStudio.Services.WebApi;
//using Tsi.Erp.TestTracker.Core.Abstractions;
//using Tsi.Erp.TestTracker.Domain.Repositories;

//namespace Tsi.Erp.TestTracker.Core
//{
//    public class TestRunnerService : ITestRunnerService
//    {
//        private readonly ITicketingSystem _azureDevOpsService;
//        private readonly IMonitoringRepository _monitoringRepository;
//        private readonly IAssemblyRepository _assemblyRepository;
//        private readonly IHubContext<TsiHub, ISignalrDemoHub> _hubContext;
//        private readonly TestRunnerManager _testRunnerManager;
//        public TestRunnerService(ITicketingSystem azureDevOpsService,
//            IMonitoringRepository monitoringRepository,
//            IAssemblyRepository assemblyRepository,
//            IHubContext<TsiHub, ISignalrDemoHub> hubContext,
//            TestRunnerManager testRunnerManager)
//        {
//            //_azureDevOpsService = azureDevOpsService;
//            _monitoringRepository = monitoringRepository;
//            _hubContext = hubContext;
//            _testRunnerManager = testRunnerManager;
//            _assemblyRepository = assemblyRepository;
//        }


//        public void RunTest(int id)
//        {
//            var monitoring = _monitoringRepository.GetById(id);

//            _hubContext.Clients.All.TaskState(new TaskStateResponce() { Id = monitoring.Id, Name = monitoring.NameMethodTest, State = TaskState.Pending });
//            _hubContext.Clients.All.TaskState(new TaskStateResponce() { Id = monitoring.Id, Name = monitoring.NameMethodTest, State = TaskState.Inprogress });

//            var monitoringDetail = new MonitoringDetail()
//            {
//                Date = DateTime.Now,
//                State = State.Success,
//                BuildVersion = "07.101",
//                Ticket = "0"
//            };

//            var assemblies = _assemblyRepository.GetAllAsync().Result;
//            var assembly = assemblies.FirstOrDefault();
//            if (assembly is not null)
//            {
//                if (!File.Exists(assembly.Name))
//                {
//                    using var ms = new MemoryStream(assembly.AssemblyBytes);
//                    using var file = new FileStream(assembly.Name, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite, assembly.AssemblyBytes.Length);
//                    ms.CopyTo(file);
//                }
//                _testRunnerManager.InitializeAssemblyFromFile(assembly.Name);
//            }

//            var result = _testRunnerManager.RunTest(monitoring.NameMethodTest);

//            monitoringDetail.Message = result.Message;

//            monitoringDetail.StackTrace = result.StackTrace;
//            monitoringDetail.Duration = result.Duration;
//            monitoringDetail.ExceptionType = result.Exception?.GetType().FullName;
//            monitoringDetail.ExceptionMessage = result.Exception?.Message;

//            if (result.TestState == TestState.Skipped)
//            {
//                monitoringDetail.State = State.Skipped;
//            }
//            else if (result.TestState == TestState.Success)
//            {
//                monitoringDetail.State = State.Success;
//            }
//            else if (result.TestState == TestState.Warning)
//            {
//                monitoringDetail.State = State.Warning;
//            }
//            else if (result.TestState == TestState.Failed)
//            {
//                monitoringDetail.State = State.Failed;
//            }
//            else
//            {
//                //var type = "Bug";
//                //var projectName = "TSI_ERP_TEST_TRACKER";
//                //var title = result.Area+" "+monitoring.NameMethodTest;
//                //var message = monitoringDetail.StackTrace;
//                //var Area = result.Area;
//                //if (message!=null) {
//                //    var exist = await _azureDevOpsService.GetWorkItems(projectName, title);
//                //    if (exist==false) {
//                //        await _azureDevOpsService.CreateTestCase(title, projectName, message, type);
//                //    }
//                //    else
//                //    {
//                //        await _azureDevOpsService.UpdateWorkItemDescription(title, message, type);
//                //    }
//                //}

//                monitoringDetail.State = State.Error;
//            }

//            if (monitoring.MonitoringDetails is null)
//            {
//                monitoring.MonitoringDetails = new List<MonitoringDetail>() {
//                    monitoringDetail
//            };
//            }


//            else
//            {
//                monitoring.MonitoringDetails.Add(monitoringDetail);
//            }

//            _monitoringRepository.Update(monitoring);


//            _monitoringRepository.SaveAsync().SyncResult();

//            _hubContext.Clients.All.TaskState(new TaskStateResponce() { Id = monitoring.Id, Name = monitoring.NameMethodTest, State = TaskState.Completed }).SyncResult();
//        }

//        public void RunTests(int[] ids)
//        {
//            foreach (var id in ids)
//            {
//                RunTest(id);
//            }
//        }

//        public void StartAll()
//        {
//            var monitorings = _monitoringRepository.GetAllAsync().SyncResult();
//            foreach (var monitoring in monitorings)
//            {
//                _hubContext.Clients.All.TaskState(new TaskStateResponce() { Id = monitoring.Id, Name = monitoring.NameMethodTest, State = TaskState.Pending }).SyncResult();
//            }
//            lock (_testRunnerManager)
//            {
//                foreach (var monitoring in monitorings)
//                {

//                    //}
//                    //foreach (var monitoring in monitorings)
//                    //{
//                    _hubContext.Clients.All.TaskState(new TaskStateResponce() { Id = monitoring.Id, Name = monitoring.NameMethodTest, State = TaskState.Inprogress }).SyncResult();

//                    var monitoringDetail = new MonitoringDetail()
//                    {
//                        Date = DateTime.Now,
//                        State = State.Success,
//                        BuildVersion = "07.101",
//                        Ticket = "0"
//                    };

//                    var assemblies = _assemblyRepository.GetAllAsync().Result;
//                    var assembly = assemblies.FirstOrDefault();
//                    if (assembly is not null)
//                    {
//                        if (!File.Exists(assembly.Name))
//                        {
//                            using var ms = new MemoryStream(assembly.AssemblyBytes);
//                            using var file = new FileStream(assembly.Name, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite, assembly.AssemblyBytes.Length);
//                            ms.CopyTo(file);
//                        }
//                        _testRunnerManager.InitializeAssemblyFromFile(assembly.Name);
//                    }

//                    var result = _testRunnerManager.RunTest(monitoring.NameMethodTest);

//                    monitoringDetail.Message = result.Message;
//                    monitoringDetail.StackTrace = result.StackTrace;
//                    monitoringDetail.Duration = result.Duration;
//                    monitoringDetail.ExceptionType = result.Exception?.GetType().FullName;
//                    monitoringDetail.ExceptionMessage = result.Exception?.Message;

//                    if (result.TestState == TestState.Skipped)
//                    {
//                        monitoringDetail.State = State.Skipped;
//                    }
//                    else if (result.TestState == TestState.Success)
//                    {
//                        monitoringDetail.State = State.Success;
//                    }
//                    else if (result.TestState == TestState.Warning)
//                    {
//                        monitoringDetail.State = State.Warning;
//                    }
//                    else if (result.TestState == TestState.Failed)
//                    {
//                        monitoringDetail.State = State.Failed;
//                    }
//                    else
//                    {
//                        var type = "Bug";
//                        var projectName = "TSI_ERP_TEST_TRACKER";
//                        var title = result.Area + ": " + monitoring.NameMethodTest;
//                        var message = monitoringDetail.StackTrace;
//                        var Area = result.Area;
//                        //if (message != null)
//                        //{
//                        //    var exist = await _azureDevOpsService.GetWorkItems(projectName, title);
//                        //    if (exist == false)
//                        //    {
//                        //        await _azureDevOpsService.CreateTestCase(title, projectName, message, type);
//                        //    }
//                        //    else
//                        //    {
//                        //        await _azureDevOpsService.UpdateWorkItemDescription(projectName,title, message);
//                        //    }
//                        //}
//                        monitoringDetail.State = State.Error;
//                    }
//                    if (monitoring.MonitoringDetails is null)
//                    {
//                        monitoring.MonitoringDetails = new List<MonitoringDetail>();
//                    }

//                    monitoring.MonitoringDetails.Add(monitoringDetail);

//                    _monitoringRepository.Update(monitoring);

//                    _monitoringRepository.Save();

//                    _hubContext.Clients.All.TaskState(new TaskStateResponce() { Id = monitoring.Id, Name = monitoring.NameMethodTest, State = TaskState.Completed }).SyncResult();
//                }

//            }

//        }

//    }
//}
