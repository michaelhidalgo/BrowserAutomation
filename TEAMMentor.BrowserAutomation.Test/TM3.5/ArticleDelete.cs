using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gallio.Framework;
using MbUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using TEAMMentor.SauceLabs.AutomationTest;

namespace TEAMMentor.BrowserAutomation.Test
{
    [MbUnit.Framework.TestFixture]
    public class ArticleDelete : BaseTest
    {
        [MbUnit.Framework.Test]
        public void CreateAndDeleteArticles(string browser, string version, string platform)
        {
            _Setup(browser, version, platform);
            base._Driver.Navigate().GoToUrl("http://tm-dev-01.teammentor.net/Teammentor");
            Login();
            var articleCount = _Driver.FindElementWhenHasText(By.Id("nowShowingText"));
            Assert.IsTrue(articleCount.Text== "Showing 370 items (out of 370)");

            CreateAnArticle();
            

            var topText = _Driver.FindElementWhenHasText(By.Id("topRightMenu"));
            LogOut();
        }
        [Test]
        public void TMVersionChanged(string browser, string version, string platform)
        {
            _Setup(browser, version, platform);
            base._Driver.Navigate().GoToUrl("http://tm-dev-01.teammentor.net/Teammentor");
            var topText = _Driver.FindElementWhenHasText(By.Id("topRightMenu"));
            Assert.IsTrue(topText.Text.Contains("TM 3.5.2"));

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

        private void CreateAnArticle()
        {
            var topText = _Driver.FindElementWhenHasText(By.Id("topRightMenu"));
            Assert.IsTrue(topText.Text.Contains("Edit Mode"));
            _Driver.FindElementWhenHasText(By.LinkText("Edit Mode")).Click();
            _Driver.FindElementEx(By.XPath(".//*[@id='c53ab3f8-ec69-4856-8a20-3aa3c68ce053']/ins")).Click();
            var item = _Driver.FindElementEx(By.XPath(".//*[@id='90bbf77c-c2fb-402e-9511-b35903b04030']/a"));
            Actions actions = new Actions(_Driver);
            var action = actions.ContextClick(item).Build(); //pass WebElement as an argument
            action.Perform();
            var addItem = _Driver.FindElementEx(By.XPath(".//*[@id='vakata-contextmenu']/ul/li[1]/a"));
            addItem.Click();
            _Driver.Navigate().Refresh();
            var articleCount = _Driver.FindElementWhenHasText(By.Id("nowShowingText"));
            Assert.IsTrue(articleCount.Text == "Showing 371 items (out of 371)");
            var seachBox = _Driver.FindElementEx(By.Id("SearchTextBox"));
            seachBox.SendKeys("New Guidance Item");
            _Driver.FindElementEx(By.Id("ctl00_ContentPlaceHolder1_SearchControl1_SearchButton")).Click();
            articleCount = _Driver.FindElementWhenHasText(By.Id("nowShowingText"));
            Assert.IsTrue(articleCount.Text == "Showing 1 items (out of 1)");
            _Driver.FindElementWhenHasText(By.LinkText("Edit Mode")).Click();
            _Driver.FindElementEx(By.Id("button_selectAll")).Click();
            _Driver.FindElementEx(By.Id("button_DeleteGuidanceItemsFromLibrary")).Click();
            _Driver.SwitchTo().ActiveElement();
            IWebElement modalDialog = _Driver.FindElementEx(By.ClassName("ui-dialog"));
            IWebElement title = _Driver.FindElementEx(By.XPath(".//*[@id='ui-dialog-title-1']"));
            Assert.IsTrue(title.Text.Contains("Delete confirmation"));
            var buttons = modalDialog.FindElements(By.ClassName("ui-button"));
            Assert.IsTrue(buttons.Count ==2);
            buttons[1].Click();
            _Driver.Navigate().Refresh();
             articleCount = _Driver.FindElementWhenHasText(By.Id("nowShowingText"));
           // Assert.IsTrue(articleCount.Text == "Showing 371 items (out of 371)");

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
