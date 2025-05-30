using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using OpenQA.Selenium.Chrome;

namespace TestProject1
{
    [TestFixture]
    public class EdgeDriverTest
    {
        // In order to run the below test(s), 
        // please follow the instructions from http://go.microsoft.com/fwlink/?LinkId=619687
        // to install Microsoft WebDriver.

        public ChromeDriver WebDriver { get; set; }

        [SetUp]
        public void SetUp()
        {
            WebDriver = new ChromeDriver(new ChromeOptions()
            {

            });
        }

        [Test]
        public void VerifyPageTitle()
        {
            // Replace with your own test logic
            WebDriver.Url = "https://www.bing.com";
            WebDriver.Navigate();
            Assert.AreEqual("Bing", WebDriver.Title, "");

        }

        [Test]
        public void VerifyPageTitle1()
        {
            // Replace with your own test logic
            WebDriver.Url = "https://www.google.com";
            WebDriver.Navigate();
            Assert.AreEqual("Bing", WebDriver.Title, "WebDriver.Url = \"https://www.google.com\";\r\n            WebDriver.Navigate();");
        }

        [Test]
        public void ThrowException()
        {
            // Replace with your own test logic
            WebDriver.Url = "https://www.google.com";
            WebDriver.Navigate();
            throw new Exception("this methode has throw an exception and will be with state error");
        }
        [Test]
        public void WarningException()
        {
            // Replace with your own test logic
            WebDriver.Url = "https://www.google.com";
            WebDriver.Navigate();
            Assert.Warn("This is a warning");
        }

        [Test]
        public void SuccessException()
        {
            // Replace with your own test logic
            WebDriver.Url = "https://www.google.com";
            WebDriver.Navigate();
            Assert.Pass("This is a sucess message");
        }

        [TearDown]
        public void TearDown()
        {            
        }

        public ResultState GetCurrentTestResult()
        {
            TestExecutionContext context = TestExecutionContext.CurrentContext;
            var result = context.CurrentResult;
            return result.ResultState;
        }
        public string GetCurrentTestName()
        {
            TestExecutionContext context = TestExecutionContext.CurrentContext;
            var result = context.CurrentTest.Name;
            return result;
        }
    }
}