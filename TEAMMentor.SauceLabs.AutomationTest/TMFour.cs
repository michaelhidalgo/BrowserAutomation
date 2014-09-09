using System;
using MbUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TEAMMentor.SauceLabs.AutomationTest
{
    [TestFixture, Parallelizable]
    public class TMFour : BaseTest
    {
        [MbUnit.Framework.Test, Parallelizable]
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

        [MbUnit.Framework.Test, Parallelizable]
        public void Login(string browser, string version, string platform)
        {
            base._Setup(browser, version, platform);
            base._Driver.Navigate().GoToUrl("http://tm-node.azurewebsites.net/home/main-app-view.html");
            var wait = new WebDriverWait(this._Driver, TimeSpan.FromSeconds(30));
            wait.Until(x => x.Title == "TEAM Mentor 4.0 (Html version)");
            Assert.IsTrue(_Driver.Title == "TEAM Mentor 4.0 (Html version)");

            var link = this._Driver.FindElement(By.LinkText("Login"));
            Assert.IsTrue(link.Text.Length > 0);
            link.Click();
            wait.Until(x => x.Url == "http://tm-node.azurewebsites.net/user/login/returning-user-login.html");
            _Driver.FindElement(By.Id("new-user-username")).Clear();
            _Driver.FindElement(By.Id("new-user-username")).SendKeys("tm");
            _Driver.FindElement(By.Id("new-user-password")).SendKeys("tm");

            _Driver.FindElement(By.Id("btn-login")).Click();
            wait.Until(x => x.Url == "http://tm-node.azurewebsites.net/home/main-app-view.html");

            Assert.IsTrue(_Driver.Url == "http://tm-node.azurewebsites.net/home/main-app-view.html");

        }
    }
}
