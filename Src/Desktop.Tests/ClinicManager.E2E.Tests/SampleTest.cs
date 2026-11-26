namespace ClinicManager.E2E.Tests
{
    
    public class EmptyTests : UITestBase
    {
    
        [Test]
        public void TestMethodName()
        {
            var loginWindow = GetLoginWindow();
            loginWindow.LoginButton.Click();
        }

        
        
    }
}
