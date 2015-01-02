using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using FluentSharp.CoreLib;
using FluentSharp.Web;
using Gallio.Framework;
using Gallio.Model;
using MbUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using System;
using TEAMMentor.BrowserAutomation.Test;

namespace TEAMMentor.SauceLabs.AutomationTest
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
        /// 
        public string _browser { get; set; }
        public string _version { get; set; }
        public string _platform { get; set; }
        public bool  ShouldSaveScreenShoots { get; set; }

        public BaseTest()
        {
            this._browser = string.Empty;
            this._platform = String.Empty;
            this._version = String.Empty;
            this.ShouldSaveScreenShoots = false;
        }
        protected void _Setup(string browser, string version, string platform)
        {
            this._browser = browser;
            this._platform = platform;
            this._version = version;
            var fullPath = @"C:\temp\access\access.txt";
            var text = System.IO.File.ReadAllText(fullPath);
            var plainText = text.Split(':').ToList();
            var userName = plainText.First();
            var accesKey = plainText.LastOrDefault();
            //if (!IsRunningFromTeamCity())
            //{
            var commandExecutorUri = new Uri("http://ondemand.saucelabs.com/wd/hub");

                var desiredCapabilites = new DesiredCapabilities(browser, version, Platform.CurrentPlatform);
                desiredCapabilites.SetCapability("platform", platform); // operating system to use
                desiredCapabilites.SetCapability("username", userName);
                desiredCapabilites.SetCapability("accessKey",accesKey);
                desiredCapabilites.SetCapability("name", "TEAM Mentor Build -" + TestContext.CurrentContext.Test.Name);
                _Driver = new RemoteWebDriver(commandExecutorUri, desiredCapabilites);
            //}
            //else
            //{
                //_Driver = new FirefoxDriver();
            //}
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
                    // log the result to sauce labs
                    ((IJavaScriptExecutor) _Driver).ExecuteScript("sauce:job-result=" + (passed ? "passed" : "failed"));
            }
            finally
            {
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
