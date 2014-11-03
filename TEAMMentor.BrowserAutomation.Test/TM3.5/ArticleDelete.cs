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
    [MbUnit.Framework.TestFixture]
    public class ArticleDelete : BaseTest
    {
        [MbUnit.Framework.Test]
        public void MainPageAskForLogin(string browser, string version, string platform)
        {
            _Setup(browser, version, platform);
            base._Driver.Navigate().GoToUrl("http://tm-dev-01.teammentor.net/Teammentor");
            Login();
            LogOut();
        }

        private void Login()
        {
            
            Assert.IsTrue(_Driver.Title == "TEAM Mentor");
            var link = this._Driver.FindElementEx(By.LinkText("Login"));
            link.Click();
            //Caja de texto ha cargo.
            var usrName = _Driver.FindElementEx(By.Id("UsernameBox"));
            var password= _Driver.FindElementEx(By.Id("PasswordBox"));
            usrName.SendKeys("");
            usrName.SendKeys("admin");
            password.SendKeys("");
            password.SendKeys("!!tmadmin");
            _Driver.FindElementEx(By.Id("loginButton")).Click();
            Assert.IsTrue(link.Text.Length > 0);
        }

        private void LogOut()
        {
            
            var v = new WebDriverWait(_Driver, TimeSpan.FromSeconds(30));
            v.Until(x=> x.FindElementEx(By.Id("topRightMenu")).Text.Contains("Logout"));
            var topMenu = _Driver.FindElementEx(By.Id("topRightMenu"));
            Assert.IsTrue(topMenu.Text.Contains("Logout"));
        }
    }
}
