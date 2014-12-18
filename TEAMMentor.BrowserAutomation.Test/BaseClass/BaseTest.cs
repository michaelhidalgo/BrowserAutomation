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
            if (!IsRunningFromTeamCity())
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
            if (this.ShouldSaveScreenShoots)
                SaveScreenShot();
        }

        private static bool IsRunningFromTeamCity()
        {
            var assemblyLocation = Assembly.GetCallingAssembly().CodeBase;
            var asemblyFullPath = Uri.EscapeUriString(assemblyLocation);
            var assemblyDirectory = Path.GetDirectoryName(asemblyFullPath);
            return assemblyDirectory!=null && assemblyDirectory.ToLowerInvariant().Contains(@"buildagent\work");
        }

        private string GetJsonResponse(string url)
        {
            using (var client = new WebClient())
            {
                string credentials =
                    Convert.ToBase64String(
                        Encoding.ASCII.GetBytes(Constants.SAUCE_LABS_ACCOUNT_NAME +":"+Constants.SAUCE_LABS_ACCOUNT_KEY));
                client.Headers[HttpRequestHeader.Authorization] = "Basic " + credentials;
                var response = client.DownloadString(url);
                return response;
            }
        }

        private void SavePngImage(string fileName, string url)
        {
            using (var client = new WebClient())
            {
                string credentials =
                    Convert.ToBase64String(
                        Encoding.ASCII.GetBytes(Constants.SAUCE_LABS_ACCOUNT_NAME + ":" + Constants.SAUCE_LABS_ACCOUNT_KEY));
                client.Headers[HttpRequestHeader.Authorization] = "Basic " + credentials;
                byte[] imageResponse = client.DownloadData(url);

                System.IO.File.WriteAllBytes(fileName, imageResponse);
            }
        }
        private void SaveScreenShot()
        {
            var jobUrl = String.Format(@"https://saucelabs.com/rest/v1/{0}/jobs?limit=1", Constants.SAUCE_LABS_ACCOUNT_NAME);
            var jobId = GetJsonResponse(jobUrl);
            var response = jobId.json_Deserialize();
         
            var record = ((Object[])response).FirstOrDefault();
            var dic = record as Dictionary<string, object>;
            var id= dic.Values.FirstOrDefault();
            Assert.IsTrue(id.ToString().isGuid());
            Thread.Sleep(5000);
            //Getting the job
            jobUrl = String.Format(@"https://saucelabs.com/rest/v1/{0}/jobs/{1}/assets", Constants.SAUCE_LABS_ACCOUNT_NAME, id);
            var jobDetails = GetJsonResponse(jobUrl);
            var images = jobDetails.json_Deserialize() as Dictionary<string, object>;
            var list = images.value("screenshots") as Object [];
            int index = 0;
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Screnshots");
            if (!System.IO.Directory.Exists(path))
                Directory.CreateDirectory(path);

            foreach (var value in list)
            {
                TestLog.WriteLine(value);
                var imageUrl = String.Format(@"https://saucelabs.com/rest/v1/{0}/jobs/{1}/assets/{2}", Constants.SAUCE_LABS_ACCOUNT_NAME, id,value);
                string fileName = Path.Combine(path, string.Format(@"{0}_{1}_{2}_{3}_{4}.png", _browser, _version, _platform, TestContext.CurrentContext.Test.Name, index).ToString());
                SavePngImage(fileName, imageUrl);
                TestLog.WriteLine(fileName);
                index ++;
            }


        }
    }
}
