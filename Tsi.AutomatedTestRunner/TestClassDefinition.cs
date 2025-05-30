
using System.Reflection;
using Xunit;


namespace Tsi.AutomatedTestRunner
{
    public class TestClassDefinition
    {
        public TestClassDefinition()
        {
        }

        public TestClassDefinition(Type type, IEnumerable<MethodInfo> methodInfos)
        {
            Type = type;
            MethodInfos = methodInfos;
        }

        public Type Type { get; set; }
        public object? Instance { get; set; }
        public IEnumerable<MethodInfo> MethodInfos { get; set; }

        public void CreateInstance()
        {
            Instance = Activator.CreateInstance(Type);
            var lifeTime = Instance as IAsyncLifetime;

            if (lifeTime is not null)
            {
                lifeTime.InitializeAsync().Wait();
            }
        }

        public void InvokeMethod(string methodName)
        {
            CreateInstance();

            var method = GetMethod(methodName);

            method?.Invoke(Instance, null);
        }

        public MethodInfo? GetMethod(string methodName)
        {
            return MethodInfos
                .Where(m => m.Name == methodName)
                .FirstOrDefault();
        }
    }
}
