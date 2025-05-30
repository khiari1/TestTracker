namespace Tsi.Erp.TestTracker.Abstractions
{
    public interface ITestRunnerService
    {
        void RunTest(int id);
        void RunTests(int[] ids);
        void StartAll();
    }
}
