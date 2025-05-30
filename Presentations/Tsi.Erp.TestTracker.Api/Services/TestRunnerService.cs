using Microsoft.VisualStudio.Services.WebApi;
using Tsi.AutomatedTestRunner;
using Tsi.Erp.TestTracker.Core;
using Tsi.Erp.TestTracker.Core.Services;
using Tsi.Erp.TestTracker.Domain.Repositories;
using Tsi.Erp.TestTracker.Domain.Stores;

namespace Tsi.Erp.TestTracker.Api.Services
{
    public class TestRunnerService : ITestRunnerService
    {
        private readonly IMonitoringRepository _monitoringRepository;
        private readonly IAssemblyRepository _assemblyRepository;
        private readonly IPushNotification _pushNotification;
        private readonly IHtmlRendrer _htmlRendrer;
        private readonly TestRunnerManager _testRunnerManager;
        private readonly MailService _mailService;
        private static string lockRun = "lock";
        public TestRunnerService(
            IMonitoringRepository monitoringRepository,
            IAssemblyRepository assemblyRepository,
            TestRunnerManager testRunnerManager,
            IPushNotification pushNotification,
            IHtmlRendrer htmlRendrer,
            MailService mailService,
            SettingsService settingsService)
        {
            _monitoringRepository = monitoringRepository;
            _testRunnerManager = testRunnerManager;
            _assemblyRepository = assemblyRepository;
            _pushNotification = pushNotification;
            _htmlRendrer = htmlRendrer;
            _mailService = mailService;
        }


        public void RunTest(int id)
        {
            RunTests(new int[] { id });
        }

        public void RunTests(int[] ids)
        {
            var testResults = new List<MonitoringDetail>();
            var testProject = _assemblyRepository.GetAsync().Result.FirstOrDefault();
            if (testProject is not null)
            {
                var projectPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "project");
                var filePath = Path.Combine(projectPath, testProject.FileName);

                if (!Directory.Exists(projectPath)) Directory.CreateDirectory(projectPath);

                if (!File.Exists(filePath))
                {
                    using var ms = new MemoryStream(testProject.Data);
                    using (var file = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite, testProject.Data.Length))
                    {
                        ms.CopyTo(file);
                    };

                    System.IO.Compression.ZipFile.ExtractToDirectory(filePath, AppDomain.CurrentDomain.BaseDirectory, true);
                }

                _testRunnerManager.InitializeAssemblyFromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "project", $"{testProject.ProjectName}.dll"));
            }

            lock (lockRun)
            {
                foreach (var id in ids)
                {
                    _pushNotification
                        .PushAsync(new Notification() { Area = "Monitoring", ObjectId = id, Value = ((int)TaskState.Pending).ToString(), Severity = Severity.Information.ToString() })
                        .Wait();
                }

                foreach (var id in ids)
                {
                    var monitoring = _monitoringRepository.Find(id);

                    _pushNotification
                         .PushAsync(new Notification() { Area = "Monitoring", ObjectId = monitoring.Id, Value = ((int)TaskState.Inprogress).ToString(), Severity = Severity.Information.ToString() })
                         .Wait();

                    var monitoringDetail = new MonitoringDetail()
                    {
                        Date = DateTime.Now.Date,
                        Status = Status.Success,
                        BuildVersion = "07.101",
                        Ticket = "0",
                        Monitoring = monitoring,
                    };

                    testResults.Add(monitoringDetail);

                    var result = _testRunnerManager.RunTest(monitoring.NameMethodTest);

                    monitoringDetail.Message = result.Message;
                    monitoringDetail.StackTrace = result.StackTrace;
                    monitoringDetail.Duration = result.Duration;
                    monitoringDetail.ExceptionType = result.Exception?.GetType().FullName;
                    monitoringDetail.ExceptionMessage = result.Exception?.Message;

                    if (result.TestState == TestStatus.Skipped)
                    {
                        monitoringDetail.Status = Status.Skipped;
                    }
                    else if (result.TestState == TestStatus.Success)
                    {
                        monitoringDetail.Status = Status.Success;
                    }
                    else if (result.TestState == TestStatus.Warning)
                    {
                        monitoringDetail.Status = Status.Warning;
                    }
                    else if (result.TestState == TestStatus.Failed)
                    {
                        monitoringDetail.Status = Status.Failed;
                    }
                    else
                    {
                        monitoringDetail.Status = Status.Error;
                    }

                    if (monitoring.MonitoringDetails is null) monitoring.MonitoringDetails = new List<MonitoringDetail>() { monitoringDetail };
                    else
                    {
                        monitoring.MonitoringDetails.Add(monitoringDetail);
                    }

                    _monitoringRepository.Update(monitoring);


                    _monitoringRepository.SaveAsync().SyncResult();

                    _pushNotification.
                        PushAsync(new Notification() { Area = "Monitoring", ObjectId = monitoring.Id, Value = ((int)TaskState.Completed).ToString(), Severity = Severity.Information.ToString() })
                        .Wait();
                }
            }

            var html = _htmlRendrer.RenderAsync("MonitoringDetails", testResults).Result;

            _mailService.Send($"Test Result in {DateTime.Now.Date.ToShortDateString()}", html, new string[]
            {
                "louhichi.marwen@gmail.com"
            });

            try
            {
                _pushNotification.PushAsync(new Notification()
                {
                    Subject = "Send email",
                    Message = $"You have a new email for test result at {DateTime.Now.ToShortDateString()}",
                    Severity = Severity.Error.ToString(),
                    Value = "Notification",
                    Area = "Notification",
                }).Wait();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void SendEmail()
        {

        }

    }
}
