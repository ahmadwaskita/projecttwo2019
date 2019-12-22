using OpenQA.Selenium;

namespace SmProjectTwo.PageObject.Linkedin
{
    public class MyNetwork
    {
        public static By SidebarConnections = By.CssSelector("div a[href*=connections]");

        private readonly IWebDriver driver;

        public MyNetwork(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void ClickConnections()
        {
            driver.FindElement(SidebarConnections).Click();
        }
    }
}