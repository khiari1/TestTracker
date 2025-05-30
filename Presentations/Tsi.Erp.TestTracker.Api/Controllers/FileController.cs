using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using Tsi.AspNetCore.Identity.AzureAD;
using Tsi.Erp.TestTracker.Core.Services;
using Tsi.Erp.TestTracker.Domain.Stores;
using Tsi.Extensions.Identity.Stores;

namespace Tsi.Erp.TestTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly FileManager _fileManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserAzureADManager<ApplicationUser, TsiIdentityPermission> _userAzureAdManager;

        public FilesController(FileManager fileService,
                               IWebHostEnvironment webHostEnvironment,
                               UserAzureADManager<ApplicationUser, TsiIdentityPermission> userAzureAdManager)
        {
            _fileManager = fileService;
            _webHostEnvironment = webHostEnvironment;
            _userAzureAdManager = userAzureAdManager;
        }

        [HttpGet("Download")]
        [TsiAuthorize(Permissions.File_ReadWrite)]
        public IActionResult DownloadFile(string folder, int objectId,string fileName)
        {
            var file = _fileManager.GetFile(folder, objectId,fileName);

            if (file is not null)
            {
                return File(new MemoryStream(file.Data), "application/octet-stream", file.FileName);
            }

            return NotFound();
        }

        [HttpGet]
        [TsiAuthorize(Permissions.File_Read)]
        public IActionResult Get(string folder, int objectId)
        {
            return Ok( _fileManager.GetFiles( folder, objectId));

        }

        [HttpPost]
        [TsiAuthorize(Permissions.File_ReadWrite)]
        public IActionResult Post(IFormFile file, string folder, int objectId)
        {
            string? userId = _userAzureAdManager.GetUserId(HttpContext.User) ;

            MemoryStream memoryStream = new MemoryStream();

            file.CopyTo(memoryStream);

            //var data = memoryStream.ToArray();

            _fileManager.Save(_webHostEnvironment.WebRootPath,
                folder,
                objectId,
                file.FileName,
                userId,
                memoryStream);

            return Ok();
        }

        [HttpDelete]
        [TsiAuthorize(Permissions.File_ReadWrite)]
        public IActionResult Delete(string folder, int objectId, string fileName)
        {
            using MemoryStream memoryStream = new MemoryStream();

            _fileManager.Delete(folder,objectId, fileName);

            return Ok();
        }
    }
}
