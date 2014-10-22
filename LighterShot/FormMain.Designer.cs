using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace LighterShot
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new Container();
            ComponentResourceManager resources = new ComponentResourceManager(typeof(FormMain));
            this.tbSaveFileFolder = new TextBox();
            this.cbDoSaveFile = new CheckBox();
            this.button2 = new Button();
            this.folderBrowserDialog = new FolderBrowserDialog();
            this.notifyMenuStrip = new ContextMenuStrip(this.components);
            this.toolStripMenuItem2 = new ToolStripMenuItem();
            this.toolStripSeparator1 = new ToolStripSeparator();
            this.toolStripMenuItem1 = new ToolStripMenuItem();
            this.notifyIcon = new NotifyIcon(this.components);
            this.label1 = new Label();
            this.comboBox1 = new ComboBox();
            this.cbAutoStart = new CheckBox();
            this.notifyMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbSaveFileFolder
            // 
            this.tbSaveFileFolder.Location = new Point(12, 31);
            this.tbSaveFileFolder.Name = "tbSaveFileFolder";
            this.tbSaveFileFolder.Size = new Size(452, 20);
            this.tbSaveFileFolder.TabIndex = 2;
            this.tbSaveFileFolder.TextChanged += new EventHandler(this.tbSaveFileFolder_TextChanged);
            // 
            // cbDoSaveFile
            // 
            this.cbDoSaveFile.AutoSize = true;
            this.cbDoSaveFile.Location = new Point(12, 8);
            this.cbDoSaveFile.Name = "cbDoSaveFile";
            this.cbDoSaveFile.Size = new Size(162, 17);
            this.cbDoSaveFile.TabIndex = 3;
            this.cbDoSaveFile.Text = "Save every screenshot here:";
            this.cbDoSaveFile.UseVisualStyleBackColor = true;
            this.cbDoSaveFile.CheckedChanged += new EventHandler(this.cbDoSaveFile_CheckedChanged);
            // 
            // button2
            // 
            this.button2.Anchor = ((AnchorStyles)((AnchorStyles.Top | AnchorStyles.Right)));
            this.button2.Location = new Point(470, 29);
            this.button2.Name = "button2";
            this.button2.Size = new Size(37, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "...";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new EventHandler(this.button2_Click);
            // 
            // notifyMenuStrip
            // 
            this.notifyMenuStrip.Items.AddRange(new ToolStripItem[] {
            this.toolStripMenuItem2,
            this.toolStripSeparator1,
            this.toolStripMenuItem1});
            this.notifyMenuStrip.Name = "notifyMenuStrip";
            this.notifyMenuStrip.Size = new Size(117, 54);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new Size(116, 22);
            this.toolStripMenuItem2.Text = "Options";
            this.toolStripMenuItem2.Click += new EventHandler(this.toolStripMenuItem2_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new Size(113, 6);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new Size(116, 22);
            this.toolStripMenuItem1.Text = "Exit";
            this.toolStripMenuItem1.Click += new EventHandler(this.toolStripMenuItem1_Click);
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.notifyMenuStrip;
            this.notifyIcon.Icon = ((Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "LighterShot v1.0";
            this.notifyIcon.Visible = true;
            this.notifyIcon.Click += new EventHandler(this.notifyIcon_Click);
            this.notifyIcon.MouseClick += new MouseEventHandler(this.notifyIcon_MouseClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new Point(12, 68);
            this.label1.Name = "label1";
            this.label1.Size = new Size(84, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Remote service:";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.ItemHeight = 13;
            this.comboBox1.Location = new Point(102, 65);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new Size(405, 21);
            this.comboBox1.TabIndex = 6;
            this.comboBox1.SelectionChangeCommitted += new EventHandler(this.comboBox1_SelectionChangeCommitted);
            // 
            // cbAutoStart
            // 
            this.cbAutoStart.AutoSize = true;
            this.cbAutoStart.Location = new Point(15, 94);
            this.cbAutoStart.Name = "cbAutoStart";
            this.cbAutoStart.Size = new Size(71, 17);
            this.cbAutoStart.TabIndex = 7;
            this.cbAutoStart.Text = "Auto start";
            this.cbAutoStart.UseVisualStyleBackColor = true;
            this.cbAutoStart.CheckedChanged += new EventHandler(this.cbAutoStart_CheckedChanged);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.ClientSize = new Size(519, 137);
            this.Controls.Add(this.cbAutoStart);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.cbDoSaveFile);
            this.Controls.Add(this.tbSaveFileFolder);
            this.Name = "FormMain";
            this.ShowIcon = false;
            this.Text = "LighterShot Options";
            this.FormClosing += new FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new EventHandler(this.FormMain_Load);
            this.notifyMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox tbSaveFileFolder;
        private CheckBox cbDoSaveFile;
        private Button button2;
        private FolderBrowserDialog folderBrowserDialog;
        private ContextMenuStrip notifyMenuStrip;
        private ToolStripMenuItem toolStripMenuItem2;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem toolStripMenuItem1;
        private NotifyIcon notifyIcon;
        private Label label1;
        private ComboBox comboBox1;
        private CheckBox cbAutoStart;
    }
}