using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace SmProjectTwo.Script
{
    public class Util
    {
        private readonly IWebDriver driver;

        public Util(IWebDriver driver)
        {
            this.driver = driver;
        }

        public string GetBasePath()
        {
            string path = Directory.GetCurrentDirectory();
            return Path.GetFullPath(Path.Combine(path, "..", "..", ".."));
        }

        public string GetDataPath()
        {
            return Path.GetFullPath(Path.Combine(GetBasePath(), "Data"));
        }

        public string GetData(string dataPath)
        {
            return Path.GetFullPath(Path.Combine(GetDataPath(), dataPath));
        }

        public string GetDownloadPath()
        {
            var downloadPath = Path.GetFullPath(Path.Combine(GetBasePath(), "Downloads/"));

            DirectoryInfo dirInfo = new DirectoryInfo(downloadPath);

            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }

            return downloadPath;
        }

        public string GetFileDownload(string fileDownload)
        {
            return Path.GetFullPath(Path.Combine(GetDownloadPath(), fileDownload));
        }

        public void ActionLastTab(Action action)
        {
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            action();
            driver.Close();
            driver.SwitchTo().Window(driver.WindowHandles.First());
        }

        public void ScrollToElement(IWebElement element)
        {
            IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)driver;
            jsExecutor.ExecuteScript("arguments[0].scrollIntoView(false);", element);
            //jsExecutor.("arguments[0].scrollIntoView(true); window.scrollBy(0, -window.innerHeight / 4);", element);
            //jsExecutor.ExecuteScript("arguments[0].scrollIntoView(true); window.scrollBy(0, -arguments[1].offsetHeight);", element, header);
        }

        public void ScrollToBottom()
        {
            long scrollHeight = 0;

            do
            {
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                var newScrollHeight = (long)js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight); return document.body.scrollHeight;");

                if (newScrollHeight == scrollHeight)
                {
                    break;
                }
                else
                {
                    scrollHeight = newScrollHeight;
                    Thread.Sleep(2000);
                }
            } while (true);
        }

        public void RenameFile(string fileName, string newName)
        {
            try
            {
                // Check if file exists with its full path
                if (File.Exists(GetFileDownload(fileName)))
                {
                    File.Move(GetFileDownload(fileName), GetFileDownload(newName));
                }
                //else Console.WriteLine("File not found");
            }
            catch (IOException ioExp)
            {
                Console.WriteLine(ioExp.Message);
            }
        }

        public async void WaitDownloadComplete(string fileDownload, int timeoutMs, int pollingMs)
        {
            using (var cancellationTokenSource = new CancellationTokenSource(timeoutMs))
            {
                await WaitForFileToFinishChangingContentAsync(GetFileDownload(fileDownload), pollingMs, cancellationTokenSource.Token);
            }
        }

        internal async Task WaitForFileToFinishChangingContentAsync(string filePath, int pollingIntervalMs, CancellationToken cancellationToken)
        {
            await WaitForFileToExistAsync(filePath, pollingIntervalMs, cancellationToken);

            var fileSize = new FileInfo(filePath).Length;

            while (true)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    throw new TaskCanceledException();
                }

                await Task.Delay(pollingIntervalMs, cancellationToken);

                var newFileSize = new FileInfo(filePath).Length;

                if (newFileSize == fileSize)
                {
                    break;
                }

                fileSize = newFileSize;
            }
        }

        /// <exception cref="TaskCanceledException" />
        internal async Task WaitForFileToExistAsync(string filePath, int pollingIntervalMs, CancellationToken cancellationToken)
        {
            while (true)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    throw new TaskCanceledException();
                    //return;
                }

                if (File.Exists(filePath))
                {
                    break;
                }

                await Task.Delay(pollingIntervalMs, cancellationToken);
            }
        }
    }
}