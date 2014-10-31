using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MbUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TEAMMentor.BrowserAutomation.Test
{
    [TestFixture, Parallelizable]
    public class ArticleDelete : BaseTest
    {
        [Test, Parallelizable]
        public void MainPageAskForLogin(string browser, string version, string platform)
        {
            _Setup(browser, version, platform);
            base._Driver.Navigate().GoToUrl("http://tm-dev-01.teammentor.net/Teammentor");
            Login();
        }

        private void Login()
        {
            var wait = new WebDriverWait(this._Driver, TimeSpan.FromSeconds(30));
            wait.Until(x => x.Title == "TEAM Mentor");
            Assert.IsTrue(_Driver.Title == "TEAM Mentor");

            var link = this._Driver.FindElement(By.LinkText("Login"));
            Assert.IsTrue(link.Text.Length > 0);
        }
    }
}
