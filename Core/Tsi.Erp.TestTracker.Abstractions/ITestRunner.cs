using System.Reflection;

namespace Tsi.Erp.TestTracker.Abstractions
{
    public interface ITestRunner<T>
    {
        public void Run(string testMethodeName, CancellationToken cancellation);
        public void Stop(CancellationToken cancellation);
        public void Start(CancellationToken cancellation);
        public void InitializeAssembly(Assembly assembly);
    }
}