using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MbUnit.Framework;

namespace TEAMMentor.BrowserAutomation.Test
{
    [TestFixture]
    public class LandingPageTest : BaseTest
    {
        [Test]
        public void TitleIsCorrect(string browser, string version, string platform)
        {
            _Setup(browser, version, platform);
            _Driver.Navigate().GoToUrl("https://tm-4-design.herokuapp.com/default.html");
            Assert.IsTrue(_Driver.Title.Length>0);
            Assert.IsTrue(_Driver.Title == "TEAM Mentor 4.0 (Html version)");
        }

        [Test]
        public void TeamMentorLandingButtonIsAvailable(string browser, string version, string platform)
        {
            _Setup(browser, version, platform);
            var url = @"https://tm-4-design.herokuapp.com/landing-pages/index.html";
            _Driver.Navigate().GoToUrl(url);

            var button = _Driver.FindElementEx(OpenQA.Selenium.By.ClassName("btn-landing-page"));
            Console.WriteLine("This is the text => " + button.Text);
            Assert.IsTrue(button.Text.Equals("Start your free trial today",StringComparison.OrdinalIgnoreCase));

        }
        [Test]
        public void NavigationLinksAreEnabled(string browser, string version, string platform)
        {
            _Setup(browser, version, platform);
            var url = @"https://tm-4-design.herokuapp.com/landing-pages/index.html";
            _Driver.Navigate().GoToUrl(url);
            var navBar = _Driver.FindElementEx(OpenQA.Selenium.By.ClassName("nav"));
            var links = navBar.FindElements(OpenQA.Selenium.By.TagName("a"));
            Assert.IsTrue(links.Count > 0);
            Assert.IsTrue(String.Equals("About", links[0].Text, StringComparison.OrdinalIgnoreCase));
            Assert.IsTrue(String.Equals("Features", links[1].Text, StringComparison.OrdinalIgnoreCase));
            Assert.IsTrue(String.Equals("Help", links[2].Text, StringComparison.OrdinalIgnoreCase));
            Assert.IsTrue(String.Equals("|", links[3].Text, StringComparison.OrdinalIgnoreCase));
            Assert.IsTrue(String.Equals("Sign Up", links[4].Text, StringComparison.OrdinalIgnoreCase));
            Assert.IsTrue(String.Equals("Login", links[5].Text, StringComparison.OrdinalIgnoreCase));
        }

    }
}
