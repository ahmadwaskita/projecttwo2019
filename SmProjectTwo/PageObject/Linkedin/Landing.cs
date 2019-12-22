using OpenQA.Selenium;

namespace SmProjectTwo.PageObject.Linkedin
{
    public class Landing
    {
        public static By BtnLogin = By.CssSelector("nav a[href*=login]");

        private readonly IWebDriver driver;

        public Landing(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void ClickBtnLogin()
        {
            driver.FindElement(BtnLogin).Click();
        }
    }
}