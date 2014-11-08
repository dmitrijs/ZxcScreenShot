using System;
using System.Collections.Generic;
using System.Deployment.Application;
using System.Windows.Forms;
using ZxcScreenShot.Properties;

namespace ZxcScreenShot.tools
{
    abstract class Updater
    {
        private static Updater _instance;

        public static Updater GetInstance()
        {
            if (_instance == null)
            {
                _instance = ApplicationDeployment.IsNetworkDeployed
                    ? (Updater) new NetworkDeployedUpdater()
                    : new NonUpdater();
            }
            return _instance;
        }

        public abstract bool IsUpdateAvailable();
        public abstract void ShowApplicationUpdatePrompt();
        public abstract void ShowUpdateChangeLog();
    }

    class NonUpdater : Updater
    {
        public override bool IsUpdateAvailable()
        {
            return false;
        }

        public override void ShowApplicationUpdatePrompt()
        {
        }

        public override void ShowUpdateChangeLog()
        {
        }
    }

    class NetworkDeployedUpdater : Updater
    {
        private UpdateCheckInfo _updateInfo;

        private void CheckForUpdates()
        {
            if (!ApplicationDeployment.IsNetworkDeployed) return;

            _updateInfo = ApplicationDeployment.CurrentDeployment.CheckForDetailedUpdate();
        }

        public override bool IsUpdateAvailable()
        {
            if (!ApplicationDeployment.IsNetworkDeployed) return false;

            CheckForUpdates();
            return _updateInfo != null && _updateInfo.UpdateAvailable;
        }

        public override void ShowApplicationUpdatePrompt()
        {
            if (_updateInfo == null) return;

            var message =
                string.Format(
                    "New version of ZxcScreenShot is available!\n\nYour version: {0}\nNew version: {1}\n\nInstall the update?",
                    ApplicationDeployment.CurrentDeployment.CurrentVersion,
                    _updateInfo.AvailableVersion
                    );

            if (MessageBox.Show(message, @"Update found", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                ApplicationDeployment.CurrentDeployment.Update();
                Application.Restart();
            }
        }

        public override void ShowUpdateChangeLog()
        {
            if (!ApplicationDeployment.IsNetworkDeployed) return;

            var previousVersion = GetAndUpdateLastUsedVersion();
            if (previousVersion == null)
            {
                return;
            }
            var currentVersion = ApplicationDeployment.CurrentDeployment.CurrentVersion;

            ShowChangeLog(currentVersion, previousVersion);
        }

        private static Version GetAndUpdateLastUsedVersion()
        {
            var previousVersionStr = Settings.Default.LastUsedVersion;

            Settings.Default.LastUsedVersion = ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
            Settings.Default.Save();
            
            if (previousVersionStr.Length == 0)
            {
                return null;
            }
            return new Version(previousVersionStr);
        }

        private static void ShowChangeLog(Version currentVersion, Version previousVersion)
        {
            var versionToChanges = new Dictionary<Version, List<string>>
            {
                {new Version("1.4"), new List<string>
                {
                    "- New tool: Filled rectangle. It can be used to erase sensitive information",
                    "- Added color picker: Click with middle mouse button to pick a color from the screen shot"
                }},
                {new Version("1.5"), new List<string>
                {
                    "- Application now checks for updates automatically every hour",
                    "- Added option \"Check for Updates\" to check for update manually",
                    "- After update, display the list of changes (you reading it now!)"
                }},
            };

            var changeLog = "";
            foreach (var versionToChange in versionToChanges)
            {
                if (versionToChange.Key > previousVersion && versionToChange.Key <= currentVersion)
                {
                    changeLog += "** Version " + versionToChange.Key + "\n\n";
                    changeLog += string.Join("\n", versionToChange.Value) + "\n\n";
                }
            }

            if (changeLog.Length == 0)
            {
                MessageBox.Show(@"You are now using the latest version!", @"Update was successful");
            }
            else
            {
                MessageBox.Show(string.Format("Changes since last installed version:\n\n{0}", changeLog), @"Update was successful");
            }
        }
    }
}
