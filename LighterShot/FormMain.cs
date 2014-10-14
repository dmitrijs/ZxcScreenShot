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
        
        private void FormMain_Load(object sender, EventArgs e)
        {

        }
    }
}
