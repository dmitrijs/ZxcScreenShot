using System;
using System.Windows.Forms;
using ZxcScreenShot.tools;

namespace ZxcScreenShot
{
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            if (Environment.OSVersion.Version.Major >= 6) User32.SetProcessDPIAware();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var appContext = AppContext.Instance();
            if (appContext.Ok())
            {
                Application.Run(new FormOverlay(false));
            }
        }
    }
}