using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Graph;
using Microsoft.Identity.Web;
using Tsi.Erp.TestTracker.Domain.Repositories;

namespace Tsi.Erp.TestTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IMonitoringRepository _monitoringRepository;
        private readonly GraphServiceClient _graphServiceClient;
        private readonly ITokenAcquisition _tokenAcquisition;
        private readonly IOptions<MicrosoftGraphOptions> _graphOptions;

        public StatisticsController(ITokenAcquisition  tokenAcquisition,GraphServiceClient graphServiceClient,IMonitoringRepository monitoringRepository ,IOptions<MicrosoftGraphOptions> graphOptions)
        {
            _monitoringRepository= monitoringRepository;
            _graphServiceClient= graphServiceClient;
            _tokenAcquisition= tokenAcquisition;
            _graphOptions= graphOptions;

        }



        
        [HttpGet("Monitoring/Count")]
        public ActionResult GetCount(DateTime? date)
        {
            return Ok();
        }

    }
}
