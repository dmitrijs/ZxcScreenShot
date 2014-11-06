using System.Deployment.Application;
using System.Windows.Forms;

namespace ZxcScreenShot.tools
{
    class Updater
    {
        private UpdateCheckInfo _updateInfo;

        public Updater()
        {
            CheckForUpdates();
        }

        private void CheckForUpdates()
        {
            if (!ApplicationDeployment.IsNetworkDeployed) return;

            _updateInfo = ApplicationDeployment.CurrentDeployment.CheckForDetailedUpdate();
        }

        public bool IsUpdateAvailable()
        {
            CheckForUpdates();
            return _updateInfo != null && _updateInfo.UpdateAvailable;
        }

        public void ShowApplicationUpdatePrompt()
        {
            var message =
                string.Format(
                    "New version of ZxcScreenShot is available!\n\nYour version: {0}\nNew version:{1}\n\nInstall the update?",
                    ApplicationDeployment.CurrentDeployment.CurrentVersion,
                    _updateInfo.AvailableVersion
                    );

            if (MessageBox.Show(message, @"Update found", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                ApplicationDeployment.CurrentDeployment.Update();
                Application.Restart();
            }
        }
    }
}
