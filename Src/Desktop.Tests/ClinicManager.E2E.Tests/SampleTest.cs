namespace ClinicManager.E2E.Tests
{
    
    public class EmptyTests : FlaUITestBase
    {
    
        [Fact]
        public void TestMethodName()
        {
            var loginWindow = GetLoginWindow();
            loginWindow.LoginButton.Click();
            
            //shellWindow.SendButton.Click();
           Thread.Sleep(TimeSpan.FromSeconds(20));
            var path = TakeScreenShot("hello1");
            TestContext.Current.AddAttachment(
                           name: "hello1.png",
                           value: File.ReadAllBytes(path),
                           mediaType: "image/png");
            throw new NotImplementedException();
        }

        
        
    }
}
