using System;
using MbUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
namespace TEAMMentor.SauceLabs.AutomationTest
{
    [TestFixture,Parallelizable]
    public class TeamMentor : BaseTest
    {
        /// <summary>tests the title of the page</summary>
        [MbUnit.Framework.Test, Parallelizable,Ignore] // denotes that this method is a test and can be run in parallel
        public void TeamMentorPageTitleIsCorrect(string browser, string version, string platform)
        {
            // start the remote webdriver session with sauce labs
            _Setup(browser, version, platform);

            // verify the page title is correct
            Assert.Contains(_Driver.Title, "TeamMentor");
        }
        /// <summary>tests the title of the page</summary>
        [Test,Parallelizable,Ignore] // denotes that this method is a test and can be run in parallel
        public void DotNetLibrariesLoaded(string browser, string version, string platform)
        {
            // start the remote webdriver session with sauce labs
            _Setup(browser, version, platform);

            // verify the page title is correct
            Assert.Contains(_Driver.Title, "TeamMentor");
            var wait = new WebDriverWait(_Driver, TimeSpan.FromSeconds(100));
            wait.Until((x) => x.FindElement(By.XPath(".//*[@id='nowShowingText']")).Text.Length > 0);
            var nowShowingLabel = _Driver.FindElement(By.XPath(".//*[@id='nowShowingText']")).Text;
            Assert.IsTrue(nowShowingLabel.Length>0);
            Assert.IsTrue(nowShowingLabel== "Loaded 534 out of 534");
        }

        [Test,Parallelizable,Ignore]
        public void Login(String browser, string version, string platform)
        {
            _Setup(browser, version, platform);
            var panel = _Driver.FindElement(By.Id("TopMenuLinks"));
            Assert.IsTrue(panel != null);
            var links = panel.FindElement(By.Id("topRightMenu"));
            var loginLink = links.FindElement(By.LinkText("Login"));
            Assert.IsTrue(loginLink.Text == "Login");
            loginLink.Click();
            var wait = new WebDriverWait(_Driver, TimeSpan.FromSeconds(30));
            wait.Until((d) => d.FindElement(By.Id("ui-dialog-title-LoginDialog")).Text.Contains("Login into TeamMentor"));


            _Driver.FindElement(By.Id("UsernameBox")).SendKeys("mhidalgo");
            _Driver.FindElement(By.Id("PasswordBox")).SendKeys("!!tmadmin");
            _Driver.FindElement(By.Id("loginButton")).Click();

            wait = new WebDriverWait(_Driver, TimeSpan.FromSeconds(30));
            wait.Until((d) => d.FindElement(By.Id("topRightMenu")).Text.Contains("Logged"));
            var logPanel = _Driver.FindElement(By.Id("topRightMenu")).Text;
            Assert.IsTrue(logPanel.Contains("mhidalgo"));

        }
        [Test,Parallelizable,Ignore]
        public void TbotisAvailable(string browser, string version, string platform)
        {
            _Setup(browser, version, platform);
            var panel = _Driver.FindElement(By.Id("TopMenuLinks"));
            Assert.IsTrue(panel != null);
            var links = panel.FindElement(By.Id("topRightMenu"));
            var loginLink = links.FindElement(By.LinkText("Login"));
            Assert.IsTrue(loginLink.Text == "Login");
            loginLink.Click();
            var wait = new WebDriverWait(_Driver, TimeSpan.FromSeconds(30));
            wait.Until((d) => d.FindElement(By.Id("ui-dialog-title-LoginDialog")).Text.Contains("Login into TeamMentor"));


            _Driver.FindElement(By.Id("UsernameBox")).SendKeys("mhidalgo");
            _Driver.FindElement(By.Id("PasswordBox")).SendKeys("!!tmadmin");
            _Driver.FindElement(By.Id("loginButton")).Click();

            wait = new WebDriverWait(_Driver, TimeSpan.FromSeconds(30));
            wait.Until((d) => d.FindElement(By.Id("topRightMenu")).Text.Contains("Logged"));
            var logPanel = _Driver.FindElement(By.Id("topRightMenu")).Text;
            Assert.IsTrue(logPanel.Contains("TBot"));

        }
        [Test, Parallelizable,Ignore]
        public void SonyCustomizationWorksFine(string browser, string version, string platform)
        {
            _Setup(browser, version, platform);
            _Driver.Navigate().GoToUrl(@"https://sony-qa.teammentor.net/tbot");
            var wait = new WebDriverWait(_Driver, TimeSpan.FromSeconds(30));
            wait.Until((d) => d.Url.Contains("Pages/login.html?LoginReferer=/tbot"));
            _Driver.FindElement(By.Id("username")).SendKeys("mhidalgo");
            _Driver.FindElement(By.Id("password")).SendKeys("!!tmadmin");
            _Driver.FindElement(By.Id("loginButton")).Click();
            wait = new WebDriverWait(_Driver, TimeSpan.FromSeconds(40));
            wait.Until((d) => d.Url.Contains("/rest/tbot/run/Commands"));
            var tmlink = _Driver.FindElement(By.LinkText("TeamMentor UI"));
            Assert.IsTrue(tmlink.Text == "TeamMentor UI");
            tmlink.Click();
            wait = new WebDriverWait(_Driver, TimeSpan.FromSeconds(30));
            wait.Until((x) => x.FindElement(By.Id("logoText")).Text.Length > 0);
            var headerText = _Driver.FindElement(By.Id("logoText")).Text;
            Assert.IsTrue(headerText.Length>0);
            Assert.IsTrue(headerText.Contains("TeamMentor Secure Development KnowledgeBase"));
        }


    
    }
}