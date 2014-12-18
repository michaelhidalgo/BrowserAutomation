using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.Remoting.Channels;
using Gallio.Framework;
using Gallio.Runner.Reports.Schema;
using MbUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
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

        [MbUnit.Framework.Test, Parallelizable]
        public void LoginPage_BrowserResize_600X600(string browser, string version, string platform)
        {
            base.ShouldSaveScreenShoots = true;
            base._Setup(browser, version, platform);
            base._Driver.Navigate().GoToUrl("http://tm-dev-01.teammentor.net:1337/user/login/returning-user-login.html");
            var p = new Point {X= 600, Y =600};
            _Driver.Manage().Window.Size = new Size(p);

            var wait = new WebDriverWait(this._Driver, TimeSpan.FromSeconds(30));
            wait.Until(x => x.Title == "TEAM Mentor 4.0 (Html version)");
            Assert.IsTrue(_Driver.Title == "TEAM Mentor 4.0 (Html version)");

            wait = new WebDriverWait(this._Driver, TimeSpan.FromSeconds(30));
            wait.Until(x => x.FindElement(By.Id("login")).Text.Length > 0);

        }
        [MbUnit.Framework.Test, Parallelizable]
        public void LoginPage_BrowserResize500X500(string browser, string version, string platform)
        {
            base.ShouldSaveScreenShoots = true;
            base._Setup(browser, version, platform);
            base._Driver.Navigate().GoToUrl("http://tm-dev-01.teammentor.net:1337/user/login/returning-user-login.html");
            var p = new Point { X = 500, Y = 500 };
            _Driver.Manage().Window.Size = new Size(p);
            var wait = new WebDriverWait(this._Driver, TimeSpan.FromSeconds(30));
            wait.Until(x => x.Title == "TEAM Mentor 4.0 (Html version)");
            Assert.IsTrue(_Driver.Title == "TEAM Mentor 4.0 (Html version)");

            wait = new WebDriverWait(this._Driver, TimeSpan.FromSeconds(30));
            wait.Until(x => x.FindElement(By.Id("login")).Text.Length > 0);

        }
        [MbUnit.Framework.Test, Parallelizable]
        public void LoginPage_BrowserResize300X400(string browser, string version, string platform)
        {
            base.ShouldSaveScreenShoots = true;
            base._Setup(browser, version, platform);
            base._Driver.Navigate().GoToUrl("http://tm-dev-01.teammentor.net:1337/user/login/returning-user-login.html");
            var p = new Point { X = 300, Y = 400 };
            _Driver.Manage().Window.Size = new Size(p);
            var wait = new WebDriverWait(this._Driver, TimeSpan.FromSeconds(30));
            wait.Until(x => x.Title == "TEAM Mentor 4.0 (Html version)");
            Assert.IsTrue(_Driver.Title == "TEAM Mentor 4.0 (Html version)");

            wait = new WebDriverWait(this._Driver, TimeSpan.FromSeconds(30));
            wait.Until(x => x.FindElement(By.Id("login")).Text.Length > 0);

        }

        [MbUnit.Framework.Test, Parallelizable]
        public void LoginPage_BrowserResize300X500(string browser, string version, string platform)
        {
            base.ShouldSaveScreenShoots = true;
            base._Setup(browser, version, platform);
            base._Driver.Navigate().GoToUrl("http://tm-dev-01.teammentor.net:1337/user/login/returning-user-login.html");
            var p = new Point { X = 300, Y = 500 };
            _Driver.Manage().Window.Size = new Size(p);
            var wait = new WebDriverWait(this._Driver, TimeSpan.FromSeconds(30));
            wait.Until(x => x.Title == "TEAM Mentor 4.0 (Html version)");
            Assert.IsTrue(_Driver.Title == "TEAM Mentor 4.0 (Html version)");

            wait = new WebDriverWait(this._Driver, TimeSpan.FromSeconds(30));
            wait.Until(x => x.FindElement(By.Id("login")).Text.Length > 0);

       
        }

        [MbUnit.Framework.Test, Parallelizable]
        public void LoginPage_BrowserResize200X200(string browser, string version, string platform)
        {
            base.ShouldSaveScreenShoots = true;
            base._Setup(browser, version, platform);
            base._Driver.Navigate().GoToUrl("http://tm-dev-01.teammentor.net:1337/user/login/returning-user-login.html");
            var p = new Point { X = 200, Y = 200 };
            _Driver.Manage().Window.Size = new Size(p);
            var wait = new WebDriverWait(this._Driver, TimeSpan.FromSeconds(30));
            wait.Until(x => x.Title == "TEAM Mentor 4.0 (Html version)");
            Assert.IsTrue(_Driver.Title == "TEAM Mentor 4.0 (Html version)");

            wait = new WebDriverWait(this._Driver, TimeSpan.FromSeconds(30));
            wait.Until(x => x.FindElement(By.Id("login")).Text.Length > 0);


        }
        [MbUnit.Framework.Test, Parallelizable]
        public void TMMainView_BrowserResize200X200(string browser, string version, string platform)
        {
            base.ShouldSaveScreenShoots = true;
            base._Setup(browser, version, platform);
            base._Driver.Navigate().GoToUrl("http://tm-dev-01.teammentor.net:1337/home/main-app-view.html");
            var p = new Point { X = 200, Y = 200 };
            _Driver.Manage().Window.Size = new Size(p);
            var wait = new WebDriverWait(this._Driver, TimeSpan.FromSeconds(30));
            wait.Until(x => x.Title == "TEAM Mentor 4.0 (Html version)");
            Assert.IsTrue(_Driver.Title == "TEAM Mentor 4.0 (Html version)");

        }
    }
}
