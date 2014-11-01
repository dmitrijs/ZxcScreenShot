using System;
using System.Windows.Forms;
using ZxcScreenShot.lib;
using ZxcScreenShot.Properties;

namespace ZxcScreenShot
{
    class AppContext : ApplicationContext
    {
        private static AppContext _instance;
        private FormMain _main;
        private FormOverlay _overlay;
        private KeyboardHook _hook;

        private AppContext()
        {
            Application.ApplicationExit += OnApplicationExit;

            if (RegisterTheHotKey())
            {
                _main = new FormMain(); // creates notifyIcon

                CheckIfFirstRun();
            }
            else
            {
                MessageBox.Show(
                    "Could not register PrintScreen shortcut key.\n" +
                    "Please make sure no other screen capture application is running.\n" +
                    "\n" +
                    "Application will now exit...");
            }
        }

        private bool RegisterTheHotKey()
        {
            // register hotkey
            _hook = new KeyboardHook();
            _hook.KeyPressed += Hook_KeyPressed;

            return _hook.RegisterHotKey(0, Keys.PrintScreen);
        }

        private void CheckIfFirstRun()
        {
            if (!Settings.Default.IsFirstRun)
            {
                return;
            }

            Settings.Default.IsFirstRun = false;
            Settings.Default.Save();

            MessageBox.Show(
                @"Thank you for downloading ZxcScreenShot!" +
                "\n\n" +
                @"Please take a minute to configure the settings."
                );

            _main.Show();
        }

        private void OnApplicationExit(object sender, EventArgs e)
        {
            _main = null;
        }

        private static void Hook_KeyPressed(object sender, KeyPressedEventArgs e)
        {
            Instance().GetOverlay(createNew: true).Show();
        }

        public static AppContext Instance()
        {
            return _instance ?? (_instance = new AppContext());
        }

        public FormOverlay GetOverlay(bool createNew)
        {
            if (createNew)
            {
                _overlay = new FormOverlay();
            }
            return _overlay;
        }

        public bool Ok()
        {
            return _main != null;
        }

        public void ShowNotifyMessage(string title, string msg)
        {
            _main.ShowNotifyMessage(title, msg);
        }
    }
}
