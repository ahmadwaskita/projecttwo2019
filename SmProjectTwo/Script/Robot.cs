using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using SmProjectTwo.PageObject.Linkedin;
using SmProjectTwo.PageObject.Linkedin.Network;

namespace SmProjectTwo.Script
{
    public class Robot
    {
        public Util Util;

        public Landing Landing;
        public Login Login;

        public Common Common;
        public Home Home;

        public MyNetwork MyNetwork;
        public Connections Connections;

        public Profile Profile;

        private readonly IWebDriver driver;

        public Robot(IWebDriver driver)
        {
            this.driver = driver;
            Util = new Util(driver);

            Landing = new Landing(driver);
            Login = new Login(driver);

            Common = new Common(driver);
            Home = new Home(driver);

            MyNetwork = new MyNetwork(driver);
            Connections = new Connections(driver);

            Profile = new Profile(driver);
        }

        public void NavigateToLinkedin()
        {
            driver.Navigate().GoToUrl(Data.Links.Linkedin);
        }

        public void LoginToLinkedin()
        {
            Landing.ClickBtnLogin();

            Login.EnterUsername(Data.Credential.Username);
            Login.EnterPassword(Data.Credential.Password);
            Login.ClickBtnLogin();
        }

        public void GoToMyNetwork()
        {
            Common.ClickIconMyNetwork();
        }

        public void GoToMyNetworkConnections()
        {
            MyNetwork.ClickConnections();
        }

        public List<IWebElement> ScrollPopulate(int count)
        {
            var connections = Connections.NameLinkElements();

            try
            {
                int exit = (int)Math.Ceiling(count / 40.0);

                int limit = 0;
                while (connections.Count() <= count)
                {
                    foreach (var connection in connections)
                    {
                        Util.ScrollToElement(connection);
                    }
                    Thread.Sleep(3000);
                    connections = Connections.NameLinkElements();
                    limit++;
                    if (limit > exit)
                    {
                        break;
                    }
                }
                return connections;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void SequenceAction(IWebElement element)
        {
            Actions action = new Actions(driver);
            action.KeyDown(Keys.Control).MoveToElement(element).Click().KeyUp(Keys.Control).Build().Perform();
        }

        public void ClickConnections(int count)
        {
            var connections = ScrollPopulate(count);

            Actions action = new Actions(driver);
            Actions build = new Actions(driver);

            SequenceAction(connections[0]);
            Util.ActionLastTab(DownloadProfilePDF);

            SequenceAction(connections[2]);
            Util.ActionLastTab(DownloadProfilePDF);

            SequenceAction(connections[4]);
            Util.ActionLastTab(DownloadProfilePDF);

            //string selectLinkOpeninNewTab = (Keys.Control + Keys.Return);
            //connections[0].SendKeys(selectLinkOpeninNewTab);
            //connections[0].Click();
        }

        public void DownloadProfilePDF()
        {
            Profile.ClickBtnMore();
            Thread.Sleep(200);
            Profile.ClickDownloadPDF();
            Thread.Sleep(1000);
            Util.WaitDownloadComplete("Profile.pdf", 10000, 500);
            Thread.Sleep(2000);
            Util.RenameFile("Profile.pdf", Profile.TextProfileName() + ".pdf");
            Thread.Sleep(2000);
        }

        public void DownloadProfilePDFBatch(int count)
        {
            var connections = ScrollPopulate(count);

            Actions action = new Actions(driver);
            for (int i = 0; i < connections.Count(); i++)
            {
                SequenceAction(connections[i]);
                Util.ActionLastTab(DownloadProfilePDF);
            }
        }
    }
}