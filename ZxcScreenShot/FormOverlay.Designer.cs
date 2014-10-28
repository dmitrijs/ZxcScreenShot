namespace ZxcScreenShot
{
    partial class FormOverlay
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormOverlay));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panelTools = new System.Windows.Forms.Panel();
            this.buttonColor8 = new System.Windows.Forms.Button();
            this.buttonColor7 = new System.Windows.Forms.Button();
            this.buttonColor6 = new System.Windows.Forms.Button();
            this.buttonColor5 = new System.Windows.Forms.Button();
            this.buttonColor4 = new System.Windows.Forms.Button();
            this.buttonColor3 = new System.Windows.Forms.Button();
            this.buttonColor2 = new System.Windows.Forms.Button();
            this.buttonColor1 = new System.Windows.Forms.Button();
            this.buttonDrawUndo = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.buttonDrawResize = new System.Windows.Forms.Button();
            this.buttonDrawColor = new System.Windows.Forms.Button();
            this.buttonDrawArrow = new System.Windows.Forms.Button();
            this.buttonDrawLine = new System.Windows.Forms.Button();
            this.buttonDrawRect = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.panelOutput = new System.Windows.Forms.Panel();
            this.buttonJustSave = new System.Windows.Forms.Button();
            this.buttonCopy = new System.Windows.Forms.Button();
            this.buttonEditInPaint = new System.Windows.Forms.Button();
            this.buttonUrl = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonPath = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.contextMenuStripUrl = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyURLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewInBrowserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.longPressTimer = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStripPath = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyFilePathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewInExplorerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelTools.SuspendLayout();
            this.panelOutput.SuspendLayout();
            this.contextMenuStripUrl.SuspendLayout();
            this.contextMenuStripPath.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(686, 415);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            // 
            // panelTools
            // 
            this.panelTools.BackColor = System.Drawing.Color.Transparent;
            this.panelTools.Controls.Add(this.buttonColor8);
            this.panelTools.Controls.Add(this.buttonColor7);
            this.panelTools.Controls.Add(this.buttonColor6);
            this.panelTools.Controls.Add(this.buttonColor5);
            this.panelTools.Controls.Add(this.buttonColor4);
            this.panelTools.Controls.Add(this.buttonColor3);
            this.panelTools.Controls.Add(this.buttonColor2);
            this.panelTools.Controls.Add(this.buttonColor1);
            this.panelTools.Controls.Add(this.buttonDrawUndo);
            this.panelTools.Controls.Add(this.buttonDrawResize);
            this.panelTools.Controls.Add(this.buttonDrawColor);
            this.panelTools.Controls.Add(this.buttonDrawArrow);
            this.panelTools.Controls.Add(this.buttonDrawLine);
            this.panelTools.Controls.Add(this.buttonDrawRect);
            this.panelTools.Location = new System.Drawing.Point(388, 104);
            this.panelTools.Name = "panelTools";
            this.panelTools.Size = new System.Drawing.Size(36, 197);
            this.panelTools.TabIndex = 2;
            this.panelTools.Visible = false;
            // 
            // buttonColor8
            // 
            this.buttonColor8.BackColor = System.Drawing.Color.Blue;
            this.buttonColor8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonColor8.Location = new System.Drawing.Point(36, 124);
            this.buttonColor8.Name = "buttonColor8";
            this.buttonColor8.Size = new System.Drawing.Size(20, 20);
            this.buttonColor8.TabIndex = 17;
            this.buttonColor8.UseVisualStyleBackColor = false;
            // 
            // buttonColor7
            // 
            this.buttonColor7.BackColor = System.Drawing.Color.Yellow;
            this.buttonColor7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonColor7.Location = new System.Drawing.Point(36, 52);
            this.buttonColor7.Name = "buttonColor7";
            this.buttonColor7.Size = new System.Drawing.Size(20, 20);
            this.buttonColor7.TabIndex = 16;
            this.buttonColor7.UseVisualStyleBackColor = false;
            // 
            // buttonColor6
            // 
            this.buttonColor6.BackColor = System.Drawing.Color.White;
            this.buttonColor6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonColor6.Location = new System.Drawing.Point(36, 3);
            this.buttonColor6.Name = "buttonColor6";
            this.buttonColor6.Size = new System.Drawing.Size(20, 21);
            this.buttonColor6.TabIndex = 15;
            this.buttonColor6.UseVisualStyleBackColor = false;
            // 
            // buttonColor5
            // 
            this.buttonColor5.BackColor = System.Drawing.Color.Red;
            this.buttonColor5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonColor5.Location = new System.Drawing.Point(36, 28);
            this.buttonColor5.Name = "buttonColor5";
            this.buttonColor5.Size = new System.Drawing.Size(20, 20);
            this.buttonColor5.TabIndex = 14;
            this.buttonColor5.UseVisualStyleBackColor = false;
            // 
            // buttonColor4
            // 
            this.buttonColor4.BackColor = System.Drawing.Color.Purple;
            this.buttonColor4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonColor4.Location = new System.Drawing.Point(36, 148);
            this.buttonColor4.Name = "buttonColor4";
            this.buttonColor4.Size = new System.Drawing.Size(20, 20);
            this.buttonColor4.TabIndex = 13;
            this.buttonColor4.UseVisualStyleBackColor = false;
            // 
            // buttonColor3
            // 
            this.buttonColor3.BackColor = System.Drawing.Color.Cyan;
            this.buttonColor3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonColor3.Location = new System.Drawing.Point(36, 100);
            this.buttonColor3.Name = "buttonColor3";
            this.buttonColor3.Size = new System.Drawing.Size(20, 20);
            this.buttonColor3.TabIndex = 12;
            this.buttonColor3.UseVisualStyleBackColor = false;
            // 
            // buttonColor2
            // 
            this.buttonColor2.BackColor = System.Drawing.Color.Lime;
            this.buttonColor2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonColor2.Location = new System.Drawing.Point(36, 76);
            this.buttonColor2.Name = "buttonColor2";
            this.buttonColor2.Size = new System.Drawing.Size(20, 20);
            this.buttonColor2.TabIndex = 11;
            this.buttonColor2.UseVisualStyleBackColor = false;
            // 
            // buttonColor1
            // 
            this.buttonColor1.BackColor = System.Drawing.Color.Black;
            this.buttonColor1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonColor1.Location = new System.Drawing.Point(36, 172);
            this.buttonColor1.Name = "buttonColor1";
            this.buttonColor1.Size = new System.Drawing.Size(20, 21);
            this.buttonColor1.TabIndex = 10;
            this.buttonColor1.UseVisualStyleBackColor = false;
            // 
            // buttonDrawUndo
            // 
            this.buttonDrawUndo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonDrawUndo.ImageKey = "backward.png";
            this.buttonDrawUndo.ImageList = this.imageList1;
            this.buttonDrawUndo.Location = new System.Drawing.Point(2, 163);
            this.buttonDrawUndo.Name = "buttonDrawUndo";
            this.buttonDrawUndo.Size = new System.Drawing.Size(32, 32);
            this.buttonDrawUndo.TabIndex = 9;
            this.buttonDrawUndo.TabStop = false;
            this.toolTip1.SetToolTip(this.buttonDrawUndo, "Undo (Ctrl+Z)");
            this.buttonDrawUndo.UseVisualStyleBackColor = true;
            this.buttonDrawUndo.Click += new System.EventHandler(this.buttonDrawUndo_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "anchor.png");
            this.imageList1.Images.SetKeyName(1, "folder.png");
            this.imageList1.Images.SetKeyName(2, "link.png");
            this.imageList1.Images.SetKeyName(3, "browser.png");
            this.imageList1.Images.SetKeyName(4, "pen.png");
            this.imageList1.Images.SetKeyName(5, "copy-2.png");
            this.imageList1.Images.SetKeyName(6, "download-files.png");
            this.imageList1.Images.SetKeyName(7, "cross.png");
            this.imageList1.Images.SetKeyName(8, "rectangle.png");
            this.imageList1.Images.SetKeyName(9, "line.png");
            this.imageList1.Images.SetKeyName(10, "left.png");
            this.imageList1.Images.SetKeyName(11, "backward.png");
            this.imageList1.Images.SetKeyName(12, "target.png");
            // 
            // buttonDrawResize
            // 
            this.buttonDrawResize.ImageKey = "target.png";
            this.buttonDrawResize.ImageList = this.imageList1;
            this.buttonDrawResize.Location = new System.Drawing.Point(2, 131);
            this.buttonDrawResize.Name = "buttonDrawResize";
            this.buttonDrawResize.Size = new System.Drawing.Size(32, 32);
            this.buttonDrawResize.TabIndex = 8;
            this.toolTip1.SetToolTip(this.buttonDrawResize, "Resize/move window");
            this.buttonDrawResize.UseVisualStyleBackColor = true;
            this.buttonDrawResize.Click += new System.EventHandler(this.buttonDone_Click);
            // 
            // buttonDrawColor
            // 
            this.buttonDrawColor.BackColor = System.Drawing.Color.Red;
            this.buttonDrawColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDrawColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonDrawColor.ForeColor = System.Drawing.Color.Black;
            this.buttonDrawColor.ImageList = this.imageList1;
            this.buttonDrawColor.Location = new System.Drawing.Point(3, 99);
            this.buttonDrawColor.Name = "buttonDrawColor";
            this.buttonDrawColor.Size = new System.Drawing.Size(30, 30);
            this.buttonDrawColor.TabIndex = 7;
            this.toolTip1.SetToolTip(this.buttonDrawColor, "Change color");
            this.buttonDrawColor.UseVisualStyleBackColor = false;
            this.buttonDrawColor.Click += new System.EventHandler(this.buttonDrawColor_Click);
            this.buttonDrawColor.MouseDown += new System.Windows.Forms.MouseEventHandler(this.buttonDrawColor_MouseDown);
            // 
            // buttonDrawArrow
            // 
            this.buttonDrawArrow.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonDrawArrow.ImageKey = "left.png";
            this.buttonDrawArrow.ImageList = this.imageList1;
            this.buttonDrawArrow.Location = new System.Drawing.Point(2, 66);
            this.buttonDrawArrow.Name = "buttonDrawArrow";
            this.buttonDrawArrow.Size = new System.Drawing.Size(32, 32);
            this.buttonDrawArrow.TabIndex = 4;
            this.buttonDrawArrow.TabStop = false;
            this.toolTip1.SetToolTip(this.buttonDrawArrow, "Draw an arrow");
            this.buttonDrawArrow.UseVisualStyleBackColor = true;
            this.buttonDrawArrow.Click += new System.EventHandler(this.buttonDrawArrow_Click);
            // 
            // buttonDrawLine
            // 
            this.buttonDrawLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonDrawLine.ImageKey = "line.png";
            this.buttonDrawLine.ImageList = this.imageList1;
            this.buttonDrawLine.Location = new System.Drawing.Point(2, 34);
            this.buttonDrawLine.Name = "buttonDrawLine";
            this.buttonDrawLine.Size = new System.Drawing.Size(32, 32);
            this.buttonDrawLine.TabIndex = 3;
            this.toolTip1.SetToolTip(this.buttonDrawLine, "Draw a line");
            this.buttonDrawLine.UseVisualStyleBackColor = true;
            this.buttonDrawLine.Click += new System.EventHandler(this.buttonDrawLine_Click);
            // 
            // buttonDrawRect
            // 
            this.buttonDrawRect.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonDrawRect.ImageKey = "rectangle.png";
            this.buttonDrawRect.ImageList = this.imageList1;
            this.buttonDrawRect.Location = new System.Drawing.Point(2, 2);
            this.buttonDrawRect.Name = "buttonDrawRect";
            this.buttonDrawRect.Size = new System.Drawing.Size(32, 32);
            this.buttonDrawRect.TabIndex = 2;
            this.buttonDrawRect.TabStop = false;
            this.toolTip1.SetToolTip(this.buttonDrawRect, "Draw a rectangle");
            this.buttonDrawRect.UseVisualStyleBackColor = true;
            this.buttonDrawRect.Click += new System.EventHandler(this.buttonDrawRect_Click);
            // 
            // panelOutput
            // 
            this.panelOutput.Controls.Add(this.buttonJustSave);
            this.panelOutput.Controls.Add(this.buttonCopy);
            this.panelOutput.Controls.Add(this.buttonEditInPaint);
            this.panelOutput.Controls.Add(this.buttonUrl);
            this.panelOutput.Controls.Add(this.buttonCancel);
            this.panelOutput.Controls.Add(this.buttonPath);
            this.panelOutput.Location = new System.Drawing.Point(173, 304);
            this.panelOutput.Name = "panelOutput";
            this.panelOutput.Size = new System.Drawing.Size(204, 36);
            this.panelOutput.TabIndex = 3;
            // 
            // buttonJustSave
            // 
            this.buttonJustSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonJustSave.ImageKey = "download-files.png";
            this.buttonJustSave.ImageList = this.imageList1;
            this.buttonJustSave.Location = new System.Drawing.Point(134, 2);
            this.buttonJustSave.Name = "buttonJustSave";
            this.buttonJustSave.Size = new System.Drawing.Size(32, 32);
            this.buttonJustSave.TabIndex = 11;
            this.toolTip1.SetToolTip(this.buttonJustSave, "Save at default location");
            this.buttonJustSave.UseVisualStyleBackColor = true;
            this.buttonJustSave.Click += new System.EventHandler(this.buttonJustSave_Click);
            // 
            // buttonCopy
            // 
            this.buttonCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCopy.ImageKey = "copy-2.png";
            this.buttonCopy.ImageList = this.imageList1;
            this.buttonCopy.Location = new System.Drawing.Point(101, 2);
            this.buttonCopy.Name = "buttonCopy";
            this.buttonCopy.Size = new System.Drawing.Size(32, 32);
            this.buttonCopy.TabIndex = 10;
            this.toolTip1.SetToolTip(this.buttonCopy, "Copy image (Ctrl+C) / Drag image file");
            this.buttonCopy.UseVisualStyleBackColor = true;
            this.buttonCopy.Click += new System.EventHandler(this.buttonCopy_Click);
            this.buttonCopy.MouseDown += new System.Windows.Forms.MouseEventHandler(this.buttonCopy_MouseDown);
            this.buttonCopy.MouseMove += new System.Windows.Forms.MouseEventHandler(this.buttonCopy_MouseMove);
            // 
            // buttonEditInPaint
            // 
            this.buttonEditInPaint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonEditInPaint.ImageKey = "pen.png";
            this.buttonEditInPaint.ImageList = this.imageList1;
            this.buttonEditInPaint.Location = new System.Drawing.Point(68, 2);
            this.buttonEditInPaint.Name = "buttonEditInPaint";
            this.buttonEditInPaint.Size = new System.Drawing.Size(32, 32);
            this.buttonEditInPaint.TabIndex = 9;
            this.toolTip1.SetToolTip(this.buttonEditInPaint, "Edit (Paint)");
            this.buttonEditInPaint.UseVisualStyleBackColor = true;
            this.buttonEditInPaint.Click += new System.EventHandler(this.buttonEditInPaint_Click);
            // 
            // buttonUrl
            // 
            this.buttonUrl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonUrl.ImageKey = "link.png";
            this.buttonUrl.ImageList = this.imageList1;
            this.buttonUrl.Location = new System.Drawing.Point(35, 2);
            this.buttonUrl.Name = "buttonUrl";
            this.buttonUrl.Size = new System.Drawing.Size(32, 32);
            this.buttonUrl.TabIndex = 7;
            this.toolTip1.SetToolTip(this.buttonUrl, "Copy URL");
            this.buttonUrl.UseVisualStyleBackColor = true;
            this.buttonUrl.Click += new System.EventHandler(this.buttonUrl_Click);
            this.buttonUrl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.buttonUrl_MouseDown);
            this.buttonUrl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonUrl_MouseUp);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonCancel.ImageKey = "cross.png";
            this.buttonCancel.ImageList = this.imageList1;
            this.buttonCancel.Location = new System.Drawing.Point(170, 2);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(32, 32);
            this.buttonCancel.TabIndex = 6;
            this.toolTip1.SetToolTip(this.buttonCancel, "Cancel");
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonPath
            // 
            this.buttonPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPath.ImageKey = "anchor.png";
            this.buttonPath.ImageList = this.imageList1;
            this.buttonPath.Location = new System.Drawing.Point(2, 2);
            this.buttonPath.Name = "buttonPath";
            this.buttonPath.Size = new System.Drawing.Size(32, 32);
            this.buttonPath.TabIndex = 1;
            this.toolTip1.SetToolTip(this.buttonPath, "Copy file path");
            this.buttonPath.UseVisualStyleBackColor = true;
            this.buttonPath.Click += new System.EventHandler(this.buttonPath_Click);
            this.buttonPath.MouseDown += new System.Windows.Forms.MouseEventHandler(this.buttonPath_MouseDown);
            this.buttonPath.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonPath_MouseUp);
            // 
            // contextMenuStripUrl
            // 
            this.contextMenuStripUrl.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyURLToolStripMenuItem,
            this.viewInBrowserToolStripMenuItem});
            this.contextMenuStripUrl.Name = "contextMenuStripUrl";
            this.contextMenuStripUrl.Size = new System.Drawing.Size(158, 48);
            // 
            // copyURLToolStripMenuItem
            // 
            this.copyURLToolStripMenuItem.Image = global::ZxcScreenShot.Properties.Resources.link;
            this.copyURLToolStripMenuItem.Name = "copyURLToolStripMenuItem";
            this.copyURLToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.copyURLToolStripMenuItem.Text = "Copy URL";
            this.copyURLToolStripMenuItem.Click += new System.EventHandler(this.copyURLToolStripMenuItem_Click);
            // 
            // viewInBrowserToolStripMenuItem
            // 
            this.viewInBrowserToolStripMenuItem.Image = global::ZxcScreenShot.Properties.Resources.browser;
            this.viewInBrowserToolStripMenuItem.Name = "viewInBrowserToolStripMenuItem";
            this.viewInBrowserToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.viewInBrowserToolStripMenuItem.Text = "View in Browser";
            this.viewInBrowserToolStripMenuItem.Click += new System.EventHandler(this.viewInBrowserToolStripMenuItem_Click);
            // 
            // longPressTimer
            // 
            this.longPressTimer.Interval = 400;
            // 
            // contextMenuStripPath
            // 
            this.contextMenuStripPath.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyFilePathToolStripMenuItem,
            this.viewInExplorerToolStripMenuItem});
            this.contextMenuStripPath.Name = "contextMenuStripPath";
            this.contextMenuStripPath.Size = new System.Drawing.Size(158, 48);
            // 
            // copyFilePathToolStripMenuItem
            // 
            this.copyFilePathToolStripMenuItem.Image = global::ZxcScreenShot.Properties.Resources.anchor;
            this.copyFilePathToolStripMenuItem.Name = "copyFilePathToolStripMenuItem";
            this.copyFilePathToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.copyFilePathToolStripMenuItem.Text = "Copy file path";
            this.copyFilePathToolStripMenuItem.Click += new System.EventHandler(this.copyFilePathToolStripMenuItem_Click);
            // 
            // viewInExplorerToolStripMenuItem
            // 
            this.viewInExplorerToolStripMenuItem.Image = global::ZxcScreenShot.Properties.Resources.folder;
            this.viewInExplorerToolStripMenuItem.Name = "viewInExplorerToolStripMenuItem";
            this.viewInExplorerToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.viewInExplorerToolStripMenuItem.Text = "View in Explorer";
            this.viewInExplorerToolStripMenuItem.Click += new System.EventHandler(this.viewInExplorerToolStripMenuItem_Click);
            // 
            // FormOverlay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(686, 415);
            this.Controls.Add(this.panelOutput);
            this.Controls.Add(this.panelTools);
            this.Controls.Add(this.pictureBox1);
            this.KeyPreview = true;
            this.Name = "FormOverlay";
            this.Text = "Overlay";
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Shown += new System.EventHandler(this.FormOverlay_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panelTools.ResumeLayout(false);
            this.panelOutput.ResumeLayout(false);
            this.contextMenuStripUrl.ResumeLayout(false);
            this.contextMenuStripPath.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panelTools;
        private System.Windows.Forms.Button buttonDrawRect;
        private System.Windows.Forms.Button buttonDrawArrow;
        private System.Windows.Forms.Button buttonDrawLine;
        private System.Windows.Forms.Button buttonDrawColor;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button buttonDrawResize;
        private System.Windows.Forms.Panel panelOutput;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonPath;
        private System.Windows.Forms.Button buttonUrl;
        private System.Windows.Forms.Button buttonEditInPaint;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button buttonDrawUndo;
        private System.Windows.Forms.Button buttonCopy;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripUrl;
        private System.Windows.Forms.ToolStripMenuItem copyURLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewInBrowserToolStripMenuItem;
        private System.Windows.Forms.Timer longPressTimer;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripPath;
        private System.Windows.Forms.ToolStripMenuItem copyFilePathToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewInExplorerToolStripMenuItem;
        private System.Windows.Forms.Button buttonColor8;
        private System.Windows.Forms.Button buttonColor7;
        private System.Windows.Forms.Button buttonColor6;
        private System.Windows.Forms.Button buttonColor5;
        private System.Windows.Forms.Button buttonColor4;
        private System.Windows.Forms.Button buttonColor3;
        private System.Windows.Forms.Button buttonColor2;
        private System.Windows.Forms.Button buttonColor1;
        private System.Windows.Forms.Button buttonJustSave;
    }
}

