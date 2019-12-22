using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;

namespace SmProjectTwo.PageObject.Linkedin.Network
{
    public class Connections
    {
        public static By NameList = By.CssSelector("li div a span:nth-child(2)");
        public static By NameLinkList = By.CssSelector("li div.mn-connection-card__details a[href*=in]");

        private readonly IWebDriver driver;

        public Connections(IWebDriver driver)
        {
            this.driver = driver;
        }

        public List<IWebElement> NameElements()
        {
            return driver.FindElements(NameList).ToList();
        }

        public List<IWebElement> NameLinkElements()
        {
            return driver.FindElements(NameLinkList).ToList();
        }
    }
}