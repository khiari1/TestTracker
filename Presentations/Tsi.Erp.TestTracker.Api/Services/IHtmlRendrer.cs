
namespace Tsi.Erp.TestTracker.Api.Services
{
    public interface IHtmlRendrer
    {
        public Task<string> RenderAsync(string path, object model);
    }
}