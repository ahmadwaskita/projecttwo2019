using OpenQA.Selenium;

namespace SmProjectTwo.PageObject.Linkedin
{
    public class Common
    {
        public static By IconHome = By.Id("feed-tab-icon");
        public static By IconMyNetwork = By.Id("mynetwork-tab-icon");

        public static By IconProfile = By.Id("nav-settings__dropdown-trigger");
        public static By BtnLogout = By.CssSelector("a[href*=logout]");

        private readonly IWebDriver driver;

        public Common(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void ClickIconHome()
        {
            driver.FindElement(IconHome).Click();
        }

        public void ClickIconMyNetwork()
        {
            driver.FindElement(IconMyNetwork).Click();
        }

        public void ClickIconProfile()
        {
            driver.FindElement(IconProfile).Click();
        }

        public void ClickBtnLogout()
        {
            driver.FindElement(BtnLogout).Click();
        }
    }
}