using System;
using System.Windows.Forms;

namespace LighterShot
{
    class AppContext : ApplicationContext
    {
        private FormMain _main;

        public AppContext()
        {
            Application.ApplicationExit += OnApplicationExit;

            _main = new FormMain(); // creates notifyIcon
        }

        private void OnApplicationExit(object sender, EventArgs e)
        {
            _main = null;
        }
    }
}
