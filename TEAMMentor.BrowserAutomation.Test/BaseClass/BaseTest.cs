using System.IO;
using System.Reflection;
using Gallio.Framework;
using Gallio.Model;
using MbUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using System;

namespace TEAMMentor.BrowserAutomation.Test
{

    [Header("browser", "version", "platform")] // name of the parameters in the rows
    [Row("firefox", "25", "Windows 7")] // run all tests in the fixture against firefox 25 for windows 7
    [Row("chrome", "31", "Windows 7")] // run all tests in the fixture against chrome 31 for windows 7
    [Row("firefox", "25", "Windows 8.1")] // run all tests in the fixture against firefox 25 for windows 7
    public class BaseTest
    {
        protected IWebDriver _Driver;
        /// <summary>starts a sauce labs sessions</summary>
        /// <param name="browser">name of the browser to request</param>
        /// <param name="version">version of the browser to request</param>
        /// <param name="platform">operating system to request</param>
        protected void _Setup(string browser, string version, string platform)
        {

            if (IsRunningFromTeamCity())
            {
                var commandExecutorUri = new Uri("http://ondemand.saucelabs.com/wd/hub");

                var desiredCapabilites = new DesiredCapabilities(browser, version, Platform.CurrentPlatform);
                desiredCapabilites.SetCapability("platform", platform); // operating system to use
                desiredCapabilites.SetCapability("username", Constants.SAUCE_LABS_ACCOUNT_NAME);
                desiredCapabilites.SetCapability("accessKey", Constants.SAUCE_LABS_ACCOUNT_KEY);
                desiredCapabilites.SetCapability("name", "TEAM Mentor Build -" + TestContext.CurrentContext.Test.Name);
                _Driver = new RemoteWebDriver(commandExecutorUri, desiredCapabilites);
            }
            else
            {
                _Driver = new FirefoxDriver();
            }
            _Driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(30));
            _Driver.Manage().Timeouts().SetPageLoadTimeout(TimeSpan.FromSeconds(30));

            // navigate to the page under test
            //_Driver.Navigate().GoToUrl("http://tmqa-dev.teammentor.net/teamMentor");
        }


        /// <summary>called at the end of each test to tear it down</summary>
        [TearDown] // denotes that this will be called at the end of each test method
        public void CleanUp()
        {
            // get the status of the current test
            bool passed = TestContext.CurrentContext.Outcome.Status == TestStatus.Passed;
            try
            {
                if (IsRunningFromTeamCity())
                {
                    // log the result to sauce labs
                    ((IJavaScriptExecutor) _Driver).ExecuteScript("sauce:job-result=" + (passed ? "passed" : "failed"));
                }
            }
            finally
            {
                // terminate the remote webdriver session
                _Driver.Quit();
            }
        }

        private static bool IsRunningFromTeamCity()
        {
            var assemblyLocation = Assembly.GetCallingAssembly().CodeBase;
            var asemblyFullPath = Uri.EscapeUriString(assemblyLocation);
            var assemblyDirectory = Path.GetDirectoryName(asemblyFullPath);
            return assemblyDirectory!=null && assemblyDirectory.ToLowerInvariant().Contains(@"buildagent\work");
        }

    }
}
