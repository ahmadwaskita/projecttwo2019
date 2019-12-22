using SmProjectTwo.Script;
using Xunit;
using Xunit.Abstractions;

namespace SmProjectTwo
{
    public class Linkedin : InitDriver
    {
        public Robot Robot;

        public Linkedin(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            Robot = new Robot(driver);
        }

        [Fact]
        public void TestAlpha()
        {
            Robot.NavigateToLinkedin();
            Robot.LoginToLinkedin();
            Robot.GoToMyNetwork();
            Robot.GoToMyNetworkConnections();
            //Robot.ClickConnections(40);
            Robot.DownloadProfilePDFBatch(186);
        }
    }
}