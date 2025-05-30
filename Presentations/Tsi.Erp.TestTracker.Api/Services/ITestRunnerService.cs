
namespace Tsi.Erp.TestTracker.Api.Services
{
    public interface ITestRunnerService
    {
        void RunTest(int id);
        void RunTests(int[] ids);
    }
}
