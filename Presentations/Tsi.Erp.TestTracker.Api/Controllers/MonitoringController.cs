using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using Hangfire;
using Tsi.Erp.TestTracker.Api.Controllers.Dto.Response;
using Tsi.Erp.TestTracker.Api.Controllers.Dto.Request;
using System.Globalization;
using Tsi.Erp.TestTracker.Domain.Repositories;
using Tsi.Erp.TestTracker.Domain.Stores;
using System.Linq.Extensions;
using ITestRunnerService = Tsi.Erp.TestTracker.Api.Services.ITestRunnerService;
using Tsi.AutomatedTestRunner;


namespace Tsi.Erp.TestTracker.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MonitoringsController : ControllerBase
{
    private readonly IMonitoringRepository _monitoringRepository;
    private readonly IMapper _mapper;    
    private readonly IBackgroundJobClient _backgroundJUobClient;
    private readonly IRecurringJobManager _recurringJobManager;
    public MonitoringsController(IMonitoringRepository monitoringRepository,
                                 IMapper mapper,
                                 TestRunnerManager testRunnerManager,
                                 IBackgroundJobClient backgroundJUobClient,
                                 IRecurringJobManager recurringJobManager)
    {
        _monitoringRepository = monitoringRepository;
        _mapper = mapper;        
        _backgroundJUobClient = backgroundJUobClient;
        _recurringJobManager = recurringJobManager;
    }

    // GET api/<MonitoringController>/5
    [HttpGet("{id}")]
    [TsiAuthorize(Permissions.Monitoring_Read)]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
    public async Task<IActionResult> GetAsync(int id)
    {
        var monitoring = await Task.FromResult(_monitoringRepository.Find(id));
        if (monitoring is null)
        {
            return NotFound("can not find module");
        }
        var monitoringResult = _mapper.Map<Monitoring, MonitoringDto>(monitoring);
        return Ok(monitoringResult);
    }

    // POST api/<MonitoringController>
    [HttpPost]
    [TsiAuthorize(Permissions.Monitoring_ReadWrite)]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
    public async Task<IActionResult> PostAsync([FromBody] MonitoringModel Monitoring)
    {
        var monitoringResult = _mapper.Map<Monitoring>(Monitoring);
        _monitoringRepository.Create(monitoringResult);
        await _monitoringRepository.SaveAsync();

        return CreatedAtAction(nameof(GetAsync),
              new { id = monitoringResult.Id },
              _mapper.Map<MonitoringDto>(monitoringResult));
    }

    // POST: api/<MonitoringController>
    [HttpPost("Query")]
    [TsiAuthorize(Permissions.Monitoring_Read)]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
    public async Task<IActionResult> PostAsync(Query filter)
    {
        var monitorings = (await _monitoringRepository.GetMonitoringsAsync(filter)).ToList();
        return Ok(monitorings);
    }

    [HttpPost("Details/Query")]
    [TsiAuthorize(Permissions.Monitoring_Read)]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
    public async Task<IActionResult> PostDetailsAsync(Query filter)
    {
        var monitorings = (await _monitoringRepository.GetMonitoringDetailAsync(filter)).ToList();
        return Ok(monitorings);
    }

    // PUT api/<MonitoringController>/5
    [HttpPut("{id}")]
    [TsiAuthorize(Permissions.Monitoring_ReadWrite)]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
    public async Task<IActionResult> PutAsync(int id, [FromBody] MonitoringModel monitoringDto)
    {
        var monitoring = _monitoringRepository.Find(id);
        if (monitoring is null)
        {
            return NotFound("module does  not exist ");
        }
        var monitoringResult = _mapper.Map(monitoringDto, monitoring);
        _monitoringRepository.Update(monitoringResult);
        await _monitoringRepository.SaveAsync();
        return NoContent();

    }

    // DELETE api/<MonitoringController>/5
    [HttpDelete("{id}")]
    [TsiAuthorize(Permissions.Monitoring_ReadWrite)]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Delete))]
    public async Task<IActionResult> DeleteAsync(int id)

    {
        var module = _monitoringRepository.Find(id);
        if (module is null)
        {
            return NotFound("id not exist");
        }
        _monitoringRepository.Delete(id);
        await _monitoringRepository.SaveAsync();

        return Ok();
    }

    [HttpDelete()]
    [TsiAuthorize(Permissions.Functionality_ReadWrite)]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Delete))]
    public async Task<IActionResult> DeleteAsync(int[] id)
    {
        await _monitoringRepository.DeleteRangeAsync(id);
        await _monitoringRepository.SaveAsync();
        return Ok();
    }

    [HttpDelete("Details")]
    [TsiAuthorize(Permissions.Monitoring_ReadWrite)]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Delete))]
    public async Task<IActionResult> DeleteDetailsAsync(int[] ids)
    {
        await _monitoringRepository.DeleteMonitoringDetailsAsync(ids);
        await _monitoringRepository.SaveAsync();
        return Ok();
    }

    [HttpGet("RunTest/{id}")]
    [TsiAuthorize(Permissions.Monitoring_ReadWrite)]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
    public IActionResult RunTestAsync(int id, CancellationToken cancellationToken)
    {
        _backgroundJUobClient.Enqueue<ITestRunnerService>(repository => repository.RunTest(id));
        return Ok();
    }


    [HttpPost("RunTest")]
    [TsiAuthorize(Permissions.Monitoring_Read)]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
    public IActionResult EnqueueJobAsync(int[] ids,CancellationToken cancellationToken)
    {
        _backgroundJUobClient.Enqueue<ITestRunnerService>(service => service.RunTests(ids));
        return Ok();
    }

    [HttpPost("RecurringJob")]
    [TsiAuthorize(Permissions.Monitoring_Read)]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
    public IActionResult RecurringJobAsync(RecurringJobModel jobModel)
    {   
        
        var cron = string.Empty;
        switch (jobModel.RecuringMode)
        {
            case RecuringMode.Hourly:
                cron = Cron.Hourly(jobModel.Minute);
                break;
            case RecuringMode.Daily:
                cron = Cron.Daily(jobModel.Hour,jobModel.Minute);
                break;
            case RecuringMode.Weekly:
                cron = Cron.Weekly(System.DayOfWeek.Tuesday,jobModel.Hour, jobModel.Minute);
                break;
            case RecuringMode.Monthly:
                cron = Cron.Monthly(jobModel.DayOfMonth, jobModel.Minute, jobModel.Minute);
                break;
        }
        _recurringJobManager
            .AddOrUpdate<ITestRunnerService>(jobModel.JobId,
                                            _testRunner => _testRunner.RunTests(jobModel.MonitoringIds),
                                            cron);
        return Ok();
    }

    [HttpPost("ScheduleJob")]
    [TsiAuthorize(Permissions.Monitoring_Read)]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
    public IActionResult ScheduleJobAsync(ScheduleJobModel jobModel)
    {

        _backgroundJUobClient.Schedule<ITestRunnerService>(
                                            _testRunner => _testRunner.RunTests(jobModel.MonitoringIds),
                                            jobModel.Date);
        return Ok();
    }



    [HttpPost("export")]
    [TsiAuthorize(Permissions.Monitoring_Read)]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
    public async Task<IActionResult> Export(Query filter)
    {
        using (var package = new ExcelPackage())
        {
            var worksheet = package.Workbook.Worksheets.Add("Monitoring Details");

            var monitorings = (await _monitoringRepository
                .GetMonitoringDetailAsync(filter))
                .ToList();

            worksheet.Cells[1, 1].Value = "Module";
            worksheet.Cells[1, 5].Value = "Methode";
            worksheet.Cells[1, 6].Value = "Message";
            worksheet.Cells[1, 7].Value = "Date";
            worksheet.Cells[1, 8].Value = "Duration";
            worksheet.Cells[1, 9].Value = "Failing Since";

            foreach (var item in monitorings)
            {
                var index = monitorings.IndexOf(item);
                worksheet.Cells[index + 2, 1].Value = item.ModuleName;
                worksheet.Cells[index + 2, 5].Value = item.NameMethodeTest;
                worksheet.Cells[index + 2, 6].Value = item.ErrorMesage;
                worksheet.Cells[index + 2, 7].Value = item.Date;
                worksheet.Cells[index + 2, 7].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                worksheet.Cells[index + 2, 8].Value = item.Duration;
                worksheet.Cells[index + 2, 8].Style.Numberformat.Format = "hh:mm:ss";
                worksheet.Cells[index + 2, 9].Value = item.FailingSince;
                worksheet.Cells[index + 2, 9].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;

            }

            package.SaveAs(new FileInfo("MonitoringDetails.xlsx"));
            var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            return File(package.GetAsByteArray(), contentType, "monitoringDetails.xlsx");
        }
    }

}