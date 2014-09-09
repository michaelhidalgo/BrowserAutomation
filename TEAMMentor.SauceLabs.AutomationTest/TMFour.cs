using System;
using MbUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TEAMMentor.SauceLabs.AutomationTest
{
    [TestFixture, Parallelizable]
    public class TMFour : BaseTest
    {
        [Test, Parallelizable]
        public void MainPageAskForLogin(string browser, string version, string platform)
        {
            base._Setup(browser, version, platform);
            base._Driver.Navigate().GoToUrl("http://tm-node.azurewebsites.net/home/main-app-view.html");
            var wait = new WebDriverWait(this._Driver, TimeSpan.FromSeconds(30));
            wait.Until(x => x.Title == "TEAM Mentor 4.0 (Html version)");
            Assert.IsTrue(_Driver.Title == "TEAM Mentor 4.0 (Html version)");

            var link = this._Driver.FindElement(By.LinkText("Login"));
            Assert.IsTrue(link.Text.Length > 0);
          

        }
    }
}
