//using NUnit.Framework;
//using NUnit.Framework.Interfaces;
//using NUnit.Framework.Internal;
//using OpenQA.Selenium;
//using OpenQA.Selenium.Chrome;
//using System.Reflection;

//namespace Tsi.Erp.TestTracker.Api.Services
//{
//    public class TestRunnerManager : ITestRunner<ChromeDriverService>
//    {
//        private Assembly? _assembly;
      
//        IWebDriver chromeDriver;

//        public string ChromeDriverPropertyName = "WebDriver";

//        private Dictionary<Type, MethodInfo[]> _dataTypeDefinition;

//        public TestRunnerManager()
//        {
//            _dataTypeDefinition = new Dictionary<Type, MethodInfo[]>();

//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="assembly"></param>
//        /// <param name="cancellation"></param>
//        public void InitializeAssembly(Assembly assembly)
//        {
//            ArgumentNullException.ThrowIfNull(assembly, nameof(assembly));

//            _assembly = assembly;

//            var types = _assembly.GetTypes();

//            foreach (var type in types)
//            {
//                var attributre = type
//                    .GetCustomAttributes(false);

//                var attributeType = attributre?
//                    .FirstOrDefault(a => a.GetType() == typeof(TestFixtureAttribute))?
//                    .GetType();

//                if (attributeType is not null)
//                {
//                    if (!_dataTypeDefinition.Any(d => d.Key == type))
//                        _dataTypeDefinition.Add(type, PopulateTestMethode(type));
//                }
//            }
//        }
//        public void InitializeAssemblyFromFile(string assemblyFilePath)
//        {
//            Assembly assembly = Assembly.LoadFrom(assemblyFilePath);
//            InitializeAssembly(assembly);
//        }


//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="type"></param>
//        /// <returns></returns>
//        private MethodInfo[] PopulateTestMethode(Type type)
//        {
//            var methodeInfos = new List<MethodInfo>();

//            var methods = type.GetMethods();

//            foreach (var method in methods)
//            {
//                var attributs = method
//                    .GetCustomAttributes(false)
//                    .Where(a => a.GetType() == typeof(TestAttribute) || a.GetType() == typeof(TearDownAttribute))
//                    .FirstOrDefault();

//                if (attributs is not null)
//                {
//                    methodeInfos.Add(method);
//                }
//            }
//            return methodeInfos.ToArray();
//        }


//        private IWebDriver CreateChromeDriver()
//        {
//            ChromeOptions chromeOptions = new ChromeOptions();
//            chromeOptions.AddArgument("--disable-extensions");
//            chromeOptions.AddArgument("--disable-gpu");
            
//            IWebDriver chromeDriver = new ChromeDriver(chromeOptions);

//            return chromeDriver;
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="testMethodeName"></param>
//        public TestResult RunTest(string testMethodeName)
//        {
//            var dateBegin = DateTime.UtcNow;
            
//            lock (TestExecutionContext.CurrentContext)
//            {
//                var currentResult = TestExecutionContext.CurrentContext.CurrentResult;
//                Exception exception = null;
//                Type? classType = _dataTypeDefinition
//                .Where(pair => pair.Value.Any(methodInfo => methodInfo.Name == testMethodeName))
//                   .Select(pair => pair.Key)
//                     .FirstOrDefault();

//                if (classType == null)
//                {
//                    var testresult = TestExecutionContext.CurrentContext;
//                    var message = $"No class containing method '{testMethodeName}' found in the assembly.";
//                    testresult.CurrentResult.SetResult(ResultState.Skipped);
//                    testresult.CurrentResult.RecordAssertion(new AssertionResult(AssertionStatus.Error, message: message, null));

//                    return new TestResult(TestStatus.Skipped, message, "", new TimeSpan(0), "",null);
//                }

//                var instance = Activator.CreateInstance(classType);

//                var methode = _dataTypeDefinition[classType] 
//                    .Where(m => m.Name == testMethodeName)
//                    .FirstOrDefault();  


//                if (methode is null)
//                {
//                    var testresult = TestExecutionContext.CurrentContext;
//                    var message = $"{testMethodeName} : does not existe in assembly {_assembly?.FullName}";
//                    testresult.CurrentResult.SetResult(ResultState.Skipped);
//                    testresult.CurrentResult.RecordAssertion(new AssertionResult(AssertionStatus.Error, message: message, null));

//                    return new TestResult(TestStatus.Skipped,message,"",new TimeSpan(0), classType.FullName,null);
//                }

//                string className = classType.Name;

               
//                var propertyInfo = classType.GetProperty(ChromeDriverPropertyName);
//                var stackTrace =string.Empty;
//                try
//                {

//                    chromeDriver = CreateChromeDriver();

//                    if (propertyInfo is not null)
//                    propertyInfo.SetValue(instance, chromeDriver);

//                    methode.Invoke(instance, null);
//                    currentResult.SetResult(ResultState.Success);
                    

//                }
//                catch (Exception ex)
//                {
//                    exception = ex.InnerException;
//                    var execption = ex;

//                    while (ex.InnerException is not null)
//                    {
//                        ex= ex.InnerException;                        
//                        stackTrace += "<br/>" + $"<span class='textconsolas font-bold text-100'>{ex.GetType().FullName}: </span><span class='textconsolas font-bold text-400'>{ex.Message}</span><br/>" + " " + new StackTraceBeautify.StackTraceBeautify(new StackTraceBeautify.Options()
//                        {
//                            FrameCssClass = "textconsolas text-100",
//                            MethodCssClass = "textconsolas font-bold text-blue-600",
//                            ParamTypeCssClass = "textconsolas text-green-800",
//                            ParamNameCssClass = "textconsolas text-600",
//                            TypeCssClass = "textconsolas text-100",
//                            LineCssClass = "textconsolas text-purple-500",
//                            FileCssClass = "textconsolas text-100",
//                            ParamsListCssClass = "text-100",                            
//                            PrettyPrint = true,                            
//                        }).Beautify(ex.StackTrace);

//                        if(ex is SuccessException)
//                        {
//                            currentResult.SetResult(ResultState.Success);
//                            currentResult.AssertionResults.Add(new AssertionResult(AssertionStatus.Passed,ex.Message, stackTrace));
//                        }else if(ex is not AssertionException 
//                            && ex is not IgnoreException 
//                            && ex is not InconclusiveException
//                            && ex is not MultipleAssertException)
//                        {
//                            currentResult.SetResult(ResultState.Error);
//                            currentResult.AssertionResults.Add(new AssertionResult(AssertionStatus.Error, ex.Message, stackTrace));
//                        }
//                    }                 
//                }
//                finally
//                {
//                    if (chromeDriver != null) {
//                        chromeDriver.Quit();
//                        // Close the browser window
//                        chromeDriver.Dispose(); // Release resources
//                      }


//                }
//                var status = TestExecutionContext.CurrentContext.CurrentResult.ResultState.Status;
//                var result = TestExecutionContext.CurrentContext.CurrentResult.AssertionResults.LastOrDefault();
//                var timespan = DateTime.UtcNow - dateBegin;
//                var Area = classType.FullName;
//                timespan = new TimeSpan(timespan.Hours, timespan.Minutes, timespan.Seconds);
//                if (status == NUnit.Framework.Interfaces.TestStatus.Skipped)
//                {                   
//                    return new TestResult(TestStatus.Skipped, result?.Message, stackTrace, timespan, Area, exception);
//                }
//                else if (status == NUnit.Framework.Interfaces.TestStatus.Passed ||
//                    status == NUnit.Framework.Interfaces.TestStatus.Inconclusive)
//               {
                    
//                    return new TestResult(result?.Status== AssertionStatus.Warning ? TestStatus.Warning : TestStatus.Success,
//                        result?.Message ?? $"Test succeded with {status.ToString()}",
//                        stackTrace, timespan, Area, exception);
//                }
//                else if (status == NUnit.Framework.Interfaces.TestStatus.Warning)
//                {
//                    return new TestResult(TestStatus.Warning, result?.Message, stackTrace, timespan, Area, exception);
//                }else if(status == NUnit.Framework.Interfaces.TestStatus.Failed)
//                {
//                    return new TestResult(result?.Status == AssertionStatus.Failed ? TestStatus.Failed : TestStatus.Error, result?.Message, stackTrace, timespan, Area, exception);
//                }
//                else
//                {
//                    return new TestResult(TestStatus.Error, result?.Message, stackTrace, timespan, Area, exception);
//                }
               
//            }
            
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="testMethodeName"></param>
//        /// <param name="cancellation"></param>
//        public void Run(string testMethodeName, CancellationToken cancellation)
//        {
//            RunTest(testMethodeName);
//        }

//        public void Stop(CancellationToken cancellation)
//        {
//            throw new NotImplementedException();
//        }

//        public void Start(CancellationToken cancellation)
//        {          
//            throw new NotImplementedException();
//        }

//    }
//    public enum TestStatus
//    {
//        Success = 0,
//        Warning = 1,
//        Failed = 2,
//        Error = 3,
//        Skipped = 4,
//    }

//    public class TestResult
//    {
//        public TestResult(TestStatus testState, string? message, string? stackTrack, TimeSpan duration,string? area, Exception? exception)
//        {
//            Message = message;
//            TestState = testState;
//            StackTrace = stackTrack;
//            Duration = duration;
//            Area = area;
//            Exception = exception;
//        }
//        public string? Message { get; set; }
//        public TestStatus TestState { get; set; }

//        public string? StackTrace { get; set; }
//        public TimeSpan Duration { get; set; }
//        public string? Area { get; set; }
//        public Exception? Exception { get; set; }

//    }
//}


