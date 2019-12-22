using System;
using System.IO;
using System.Reflection;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit.Abstractions;

namespace SmProjectTwo.Script
{
    public class InitDriver : IDisposable
    {
        protected IWebDriver driver;
        protected ITestOutputHelper testOutputHelper;

        public InitDriver(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
            Util util = new Util(driver);

            try
            {
                ChromeOptions options = new ChromeOptions();

                options.AddArguments("start-maximized");
                options.AddArguments("disable-infobars");
                options.AddArguments("--disable-extensions");

                options.AddUserProfilePreference("download.default_directory", util.GetDownloadPath());
                options.AddUserProfilePreference("download.prompt_for_download", false);
                options.AddUserProfilePreference("download.directory_upgrade", true);

                string chromeBinary = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                driver = new ChromeDriver(chromeBinary, options, TimeSpan.FromSeconds(400));
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(400);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception when starting Chrome! {e}", e);
                throw;
            }
        }

        public void Dispose()
        {
            driver.Dispose();
        }
    }
}