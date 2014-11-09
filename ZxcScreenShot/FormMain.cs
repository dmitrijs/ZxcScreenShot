using System;
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.Win32;
using ZxcScreenShot.Properties;
using ZxcScreenShot.tools;

namespace ZxcScreenShot
{
    public partial class FormMain : Form
    {
        private const string APP_REG_KEY = "ZxcScreenShot";

        private enum NotifyIconBaloonAction
        {
            None,
            CheckForUpdates
        }
        NotifyIconBaloonAction _baloonAction = NotifyIconBaloonAction.None;

        private readonly Updater _updater = Updater.GetInstance();

        public FormMain()
        {
            InitializeComponent();

            comboBox1.Items.Add(new ComboBoxItem { Text = "", Value = null });
            comboBox1.Items.Add(new ComboBoxItem {Text = "Local Environment", Value = "http://shots.local/"});
            comboBox1.Items.Add(new ComboBoxItem {Text = "shots.zxc.lv", Value = "http://shots.zxc.lv/"});

            timerCheckForUpdates.Interval = (int) new TimeSpan(hours: 0, minutes: 30, seconds: 0).TotalMilliseconds;
            timerCheckForUpdates.Enabled = true;

            var currentVersion = Updater.GetInstance().GetCurrentVersion();
            if (currentVersion != null)
            {
                notifyIcon.Text += string.Format(" v{0}", currentVersion);
            }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            UpdateUi();
        }

        private void UpdateUi()
        {
            cbDoSaveFile.Checked = Settings.Default.DoSaveFile;
            tbSaveFileFolder.Text = Settings.Default.SaveFileFolder;
            cbAutoStart.Checked = Settings.Default.AutoStart;

            tbSaveFileFolder.Enabled = Settings.Default.DoSaveFile;
            button2.Enabled = Settings.Default.DoSaveFile;

            comboBox1.SelectedIndex = 0;
            foreach (var item in comboBox1.Items)
            {
                if (((ComboBoxItem) item).Value == Settings.Default.ShotsServiceBaseUrl)
                {
                    comboBox1.SelectedItem = item;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() != DialogResult.OK) return;

            Settings.Default.SaveFileFolder = folderBrowserDialog.SelectedPath;
            Settings.Default.Save();
            UpdateUi();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AppContext.Instance().Exit();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Show();
        }

        private void cbDoSaveFile_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.DoSaveFile = cbDoSaveFile.Checked;
            Settings.Default.Save();
            UpdateUi();
        }
        
        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void notifyIcon_Click(object sender, EventArgs e)
        {
        }

        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                AppContext.Instance().GetOverlay(createNew: true).Show();
            }
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Settings.Default.ShotsServiceBaseUrl = ((ComboBoxItem)comboBox1.SelectedItem).Value;
            Settings.Default.Save();
        }

        private void cbAutoStart_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.AutoStart = cbAutoStart.Checked;
            
            // register start up entry
            var registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            Debug.Assert(registryKey != null);
            if (Settings.Default.AutoStart)
            {
                registryKey.SetValue(APP_REG_KEY, "\"" + Application.ExecutablePath + "\"");
            }
            else
            {
                registryKey.DeleteValue(APP_REG_KEY);
            }

            Settings.Default.Save();
            UpdateUi();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            var overlay = AppContext.Instance().GetOverlay(createNew: false);
            if (overlay == null)
            {
                MessageBox.Show("This feature allows you to edit and retake the last made screen shot.\n" +
                                "Please first make a screen shot to be able to use the feature.",
                                "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            overlay.Show();
        }

        public void ShowNotifyMessage(string title, string msg)
        {
            notifyIcon.ShowBalloonTip(1000, title, msg, ToolTipIcon.Info);
        }
        
        private void checkForUpdatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_updater.IsAnyUpdateAvailable())
            {
                _updater.ShowApplicationUpdatePrompt();
            }
            else
            {
                MessageBox.Show("No updates were found.");
            }
        }

        private void timerCheckForUpdates_Tick(object sender, EventArgs e)
        {
            if (_updater.IsImportantUpdateAvailable())
            {
                _baloonAction = NotifyIconBaloonAction.CheckForUpdates;
                notifyIcon.ShowBalloonTip(10000, "Update is available", "A new version of ZxcScreenShot is available!", ToolTipIcon.Info);
            }
        }

        private void notifyIcon_BalloonTipClicked(object sender, EventArgs e)
        {
            switch (_baloonAction)
            {
                case NotifyIconBaloonAction.CheckForUpdates:
                    _updater.ShowApplicationUpdatePrompt();

                    timerCheckForUpdates.Enabled = false;
                    break;
            }
            _baloonAction = NotifyIconBaloonAction.None;
        }

        private void notifyIcon_BalloonTipClosed(object sender, EventArgs e)
        {
            _baloonAction = NotifyIconBaloonAction.None;
        }
    }

    class ComboBoxItem
    {
        public string Text { get; set; }
        public string Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}
