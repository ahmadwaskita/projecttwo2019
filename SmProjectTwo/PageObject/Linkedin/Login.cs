using OpenQA.Selenium;

namespace SmProjectTwo.PageObject.Linkedin
{
    public class Login
    {
        public static By TxtfieldUsername = By.Id("username");
        public static By TxtfieldPassword = By.Id("password");
        public static By BtnLogin = By.CssSelector("button[type=submit]");

        private readonly IWebDriver driver;

        public Login(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void EnterUsername(string user)
        {
            driver.FindElement(TxtfieldUsername).Clear();
            driver.FindElement(TxtfieldUsername).SendKeys(user);
        }

        public void EnterPassword(string pass)
        {
            driver.FindElement(TxtfieldPassword).Clear();
            driver.FindElement(TxtfieldPassword).SendKeys(pass);
        }

        public void ClickBtnLogin()
        {
            driver.FindElement(BtnLogin).Click();
        }
    }
}