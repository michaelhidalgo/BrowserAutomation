using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TEAMMentor.BrowserAutomation.Test
{
    public static  class ExtensionMethods
    {
        private const int TimeoutInSeconds = 30;

        public static  IWebElement FindElementEx(this IWebDriver driver , By by)
        {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(TimeoutInSeconds));
                return wait.Until(drv => drv.FindElement(@by));
        }

        public static IWebElement FindElementWhenHasText(this IWebDriver driver, By by)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(TimeoutInSeconds));
            wait.Until(drv => drv.FindElement(@by).Text.Length>0);
            return driver.FindElement(by);
        }
    }
}
