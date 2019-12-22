using OpenQA.Selenium;

namespace SmProjectTwo.PageObject.Linkedin
{
    public class Profile
    {
        public static By BtnMore = By.CssSelector("artdeco-dropdown button[class*=toggle]");
        public static By IconPDF = By.CssSelector("li-icon[type=download-icon]");
        public static By BtnDownloadPDF = By.CssSelector("artdeco-dropdown-item[class*=save-to-pdf]");

        public static By TxtProfileName = By.CssSelector("li.break-words");

        private readonly IWebDriver driver;

        public Profile(IWebDriver driver)
        {
            this.driver = driver;
        }

        public string TextProfileName()
        {
            return driver.FindElement(TxtProfileName).Text.Trim();
        }

        public void ClickBtnMore()
        {
            driver.FindElement(BtnMore).Click();
        }

        public void ClickIconPDF()
        {
            driver.FindElement(IconPDF).Click();
        }

        public void ClickDownloadPDF()
        {
            driver.FindElement(BtnDownloadPDF).Click();
        }
    }
}