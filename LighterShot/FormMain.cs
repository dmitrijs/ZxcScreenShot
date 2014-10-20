﻿using System;
using System.Windows.Forms;
using LighterShot.Properties;

namespace LighterShot
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();

            comboBox1.Items.Add(new ComboBoxItem { Text = "", Value = null });
            comboBox1.Items.Add(new ComboBoxItem {Text = "Local Environment", Value = "http://shots.local/"});
            comboBox1.Items.Add(new ComboBoxItem {Text = "shots.zxc.lv", Value = "http://shots.zxc.lv/"});
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
            UpdateUi();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
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

        private void tbSaveFileFolder_TextChanged(object sender, EventArgs e)
        {
            Settings.Default.Save();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
            else
            {
                notifyIcon.Visible = false;
            }
        }

        private void notifyIcon_Click(object sender, EventArgs e)
        {
        }

        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                new FormOverlay().Show();
            }
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Settings.Default.ShotsServiceBaseUrl = ((ComboBoxItem)comboBox1.SelectedItem).Value;
            Settings.Default.Save();
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
