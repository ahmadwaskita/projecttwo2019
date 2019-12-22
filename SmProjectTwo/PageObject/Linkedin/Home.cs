using OpenQA.Selenium;

namespace SmProjectTwo.PageObject.Linkedin
{
    public class Home
    {
        private readonly IWebDriver driver;

        public Home(IWebDriver driver)
        {
            this.driver = driver;
        }
    }
}