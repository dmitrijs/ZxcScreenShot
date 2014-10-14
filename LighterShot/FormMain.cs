using System;
using System.Windows.Forms;

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

        private void button2_Click(object sender, EventArgs e)
        {
            var msg = Uploader.Upload("http://shots.local/", "20141014", "yo123", @"d:\WBJNH\zalenieku18_plans\images\stories\korterid\zalenieki18\k-1\krt-1.png");
            MessageBox.Show(msg);
        }
    }
}
