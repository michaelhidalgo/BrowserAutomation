using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using  OpenQA.Selenium.Support.UI;

namespace TEAMMentor.BrowserAutomation.Test
{
    public static  class ExtensionMethods
    {
        static readonly int timeoutInSeconds = 20;
        public static  IWebElement FindElementEx(this IWebDriver driver , By by)
        {
            if (timeoutInSeconds > 0)
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                return wait.Until(drv => drv.FindElement(by));
            }
            return driver.FindElement(by);
        }
    }
}
