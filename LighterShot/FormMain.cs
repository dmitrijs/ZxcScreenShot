using System;
using System.Windows.Forms;
using LighterShot.Properties;

namespace LighterShot
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            var formOverlay = new FormOverlay {InstanceRef = this};
            formOverlay.Show();
        }
        
        private void FormMain_Load(object sender, EventArgs e)
        {
            UpdateUi();
        }

        private void UpdateUi()
        {
            cbDoSaveFile.Checked = Settings.Default.DoSaveFile;
            tbSaveFileFolder.Text = Settings.Default.SaveFileFolder;

            tbSaveFileFolder.Enabled = Settings.Default.DoSaveFile;
            button2.Enabled = Settings.Default.DoSaveFile;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() != DialogResult.OK) return;

            Settings.Default.SaveFileFolder = folderBrowserDialog1.SelectedPath;
            UpdateUi();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            new FormMain().Show();
        }

        private void cbDoSaveFile_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.DoSaveFile = cbDoSaveFile.Checked;
            Settings.Default.Save();
            UpdateUi();
        }

        private void tbSaveFileFolder_TextChanged(object sender, EventArgs e)
        {
            Settings.Default.Save();
        }
    }
}
