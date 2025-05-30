using System.Reflection;
using Xunit;


namespace Tsi.AutomatedTestRunner
{
    public class AssemblyManager
    {
        public AssemblyManager(Assembly[] assemblies,TestOptions testOptions)
        {
            ArgumentNullException.ThrowIfNull(assemblies, nameof(assemblies));
            _assemblies = assemblies;
            _testOptions = testOptions;
            InitializeClassDefinition();
        }

        private readonly Assembly[] _assemblies;

        private readonly TestOptions _testOptions;

        private List<TestClassDefinition> TestClassDefinitions { get; set; } = new List<TestClassDefinition>();

        private void InitializeClassDefinition()
        {           
            foreach (var assembly in _assemblies)
            {
                InisializeTestClassDefinitions(assembly);
            }            
        }
        private void InisializeTestClassDefinitions(Assembly assembly)
        {
            var types = assembly.ExportedTypes;

            foreach (var type in types)
            {
                var isClassTest = type.Name.Contains("Test") && type.GetInterfaces().Any(i => i == typeof(IAsyncLifetime));

                if (!isClassTest) continue;
                
                    var methodInfos = SelectTestMethodes(type);

                    if (!TestClassDefinitions.Any(d => d.Type == type))
                        TestClassDefinitions.Add(new (type, methodInfos));
                
            }
        }
        private static List<MethodInfo> SelectTestMethodes(Type type)
        {
            var methodeInfos = new List<MethodInfo>();

            var methods = type.GetMethods();

            foreach (var method in methods)
            {
                var isTestMethode = method
                    .CustomAttributes
                    .Any(a => a.AttributeType.Equals(typeof(FactAttribute)));

                if (isTestMethode)
                {
                    methodeInfos.Add(method);
                }
            }
            return methodeInfos;
        }

        public TestClassDefinition FindTestClassDefinition(string methodName)
        {
            return TestClassDefinitions
                .Where(t => t.MethodInfos.Any(mi => mi.Name == methodName))
                .FirstOrDefault();
        }   

    }
}
