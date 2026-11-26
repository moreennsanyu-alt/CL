using System;
using System.IO;
using System.Threading.Tasks;
using FlaUI.Core;
using FlaUI.Core.Capturing;
using FlaUI.Core.Logging;
using FlaUI.Core.Tools;
using FlaUI.TestUtilities;

namespace ClinicManager.E2E.Tests.Core
{
    public abstract class UITestBase : FlaUITestBase
    {
        
        public  string MediaPath => @"c:\temp\testsmedia";

        public UITestBase()
        {
            NativeMethods.SetProcessDPIAware();
        }

        protected override  AutomationBase GetAutomation()
        {
            return new UIA3Automation();
        }

        public static string ApplicationPath = BuildInfo.ApplicationPath;
    
        protected override   FlaUI.Core.Application StartApplication()
        { 
            var startInfo = new System.Diagnostics.ProcessStartInfo
            {
                FileName = ApplicationPath,
                WorkingDirectory = System.IO.Path.GetDirectoryName(ApplicationPath),
                UseShellExecute = false
            };

            return FlaUI.Core.Application.Launch(startInfo);
        }

        public TWindow GetWindow<TWindow>(string automationId) where TWindow : FlaUI.Core.AutomationElements.Window
        {
            var window = WindowFinder.FindWindowById(
                                        Automation,
                                        automationId,
                                        timeout: TimeSpan.FromSeconds(15),
                                        pollInterval: TimeSpan.FromMilliseconds(300));     
            return window.As<TWindow>();
        }

        public LoginWindow GetLoginWindow()
                   => GetWindow<LoginWindow>("LoginWindow"); 

        public ShellWindow GetShellWindow()
                   => GetWindow<ShellWindow>("ShellWindow"); 

   
        private static class NativeMethods
        {
            [DllImport("user32.dll", SetLastError = true)]
            public static extern bool SetProcessDPIAware();
        }
    }

}
