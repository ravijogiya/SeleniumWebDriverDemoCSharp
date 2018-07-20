using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using RelevantCodes.ExtentReports;

namespace SeleniumWebDriverInCSharp.BasePage
{
    [TestFixture]
    class BaseTest
    {
        public ExtentReports extent;
        public ExtentTest log;
        public IWebDriver driver;

        public BaseTest() { }

        [OneTimeSetUp]
        public void StartReport()
        {
            driver = new ChromeDriver("C:\\Program Files");
            driver.Manage().Window.Maximize();
            driver.Url = "https://www.gmail.com";

            string path = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            string actualPath = path.Substring(0, path.LastIndexOf("bin"));
            string projectPath = new Uri(actualPath).LocalPath;
            string reportPath = projectPath + "Reports\\TestReport.html";

            extent = new ExtentReports(reportPath, true);
            extent
            .AddSystemInfo("Host Name", "Automation")
            .AddSystemInfo("Environment", "QA")
            .AddSystemInfo("User Name", "Ravi Jogiya");
            extent.LoadConfig(projectPath + "extent-config.xml");
        }

        [TearDown]
        public void GetResult()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stackTrace = "<pre>" + TestContext.CurrentContext.Result.StackTrace + "</pre>";
            var errorMessage = TestContext.CurrentContext.Result.Message;

            if (status == TestStatus.Failed)
            {
                ITakesScreenshot ts = (ITakesScreenshot)driver;
                Screenshot screenshot = ts.GetScreenshot();
                string pth = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
                string finalpth = pth.Substring(0, pth.LastIndexOf("bin")) + "Reports\\" + new DateTime().ToString("yyyyMMddHHmmss") + ".png";
                string localpath = new Uri(finalpth).LocalPath;
                screenshot.SaveAsFile(localpath, ScreenshotImageFormat.Png);
                log.Log(LogStatus.Fail, log.AddScreenCapture(localpath));
                log.Log(LogStatus.Fail, stackTrace + errorMessage);
            }
            extent.EndTest(log);
        }

        [OneTimeTearDown]
        public void EndReport()
        {
            driver.Close();
            extent.Flush();
            extent.Close();

        }
    }
}
