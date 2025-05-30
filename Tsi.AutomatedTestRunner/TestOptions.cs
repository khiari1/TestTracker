
namespace Tsi.AutomatedTestRunner
{
    public class TestOptions
    {
        public static TestOptions Default => 
            new TestOptions();
        public TestOptions() { }

        public string TestMethodAttribute = "TestAttribute";

        public string TearDownAttribute = "TearDownAttribute";

        public string TestClassAttribute = "TestFixtureAttribute";

        public string ChromeDriverPropertyName = "WebDriver";
    }
}
