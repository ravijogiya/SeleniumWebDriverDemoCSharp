using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using RelevantCodes.ExtentReports;
using SeleniumWebDriverInCSharp.BasePage;
namespace SeleniumWebDriverInCSharp.LoginTC
{
    class LoginTC : BaseTest
    {


        [Test]
        public void DemoReportPass()
        {
            
        


            log = extent.StartTest("DemoReportPass");
            Assert.IsTrue(true);
            log.Log(LogStatus.Pass, "1.Assert Pass as condition is True");

            log.Log(LogStatus.Pass, "2.Title is verifyed");

            log.Log(LogStatus.Pass, "3.Title is verifyed");

            log.Log(LogStatus.Pass, "4.Title is verifyed");
        }

        [Test]
        public void DemoReportFail()
        {
            log = extent.StartTest("DemoReportFail");
            Assert.IsTrue(false);
            log.Log(LogStatus.Pass, "Assert Fail as condition is False");
        }
    }
}
