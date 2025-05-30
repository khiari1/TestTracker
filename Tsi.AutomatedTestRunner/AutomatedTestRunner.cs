using System.Reflection;

namespace Tsi.AutomatedTestRunner
{
    public class TestRunnerManager
    {
        private readonly TestOptions _testOption;
        private AssemblyManager AssemblyManager;
        public TestRunnerManager()
        {
        }
        public TestRunnerManager(TestOptions testOptions)
        {
            _testOption = testOptions;
        }

        public void InitializeAssembly(Assembly assembly)
        {
            AssemblyManager = new AssemblyManager(new[] { assembly }, _testOption);
        }

        public void InitializeAssemblyFromFile(string assemblyFilePath)
        {
            Assembly assembly = Assembly.LoadFrom(assemblyFilePath);
            InitializeAssembly(assembly);
        }

        public TestResult RunTest(string testMethodName)
        {
            
            var dateBegin = DateTime.UtcNow;

            var classDefinition = AssemblyManager.FindTestClassDefinition(testMethodName);

            TimeSpan timespan;
            try
            {
                classDefinition.InvokeMethod(testMethodName);

                timespan = DateTime.UtcNow - dateBegin;
                return TestResult.Success($"Test {testMethodName} succeeded", "", timespan);
            }
            catch (Exception ex)
            {
                timespan = DateTime.UtcNow - dateBegin;
                return TestResult.Failed(ex.Message, timespan, ex.StackTrace, ex);
            }
        }
    }
}


