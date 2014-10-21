using System;
using System.Windows.Forms;
using LighterShot.lib;

namespace LighterShot
{
    class AppContext : ApplicationContext
    {
        private FormMain _main;
        private KeyboardHook _hook;

        public AppContext()
        {
            Application.ApplicationExit += OnApplicationExit;

            _main = new FormMain(); // creates notifyIcon

            // register hotkey
            _hook = new KeyboardHook();
            _hook.KeyPressed += Hook_KeyPressed;

            if (!_hook.RegisterHotKey(0, Keys.PrintScreen))
            {
                MessageBox.Show(
                    "Could not register PrintScreen shortcut key.\n" +
                    "Please make sure no other screen capture application is running.");
            }
        }

        private void OnApplicationExit(object sender, EventArgs e)
        {
            _main = null;
        }

        static void Hook_KeyPressed(object sender, KeyPressedEventArgs e)
        {
            new FormOverlay().Show();
        }
    }
}
