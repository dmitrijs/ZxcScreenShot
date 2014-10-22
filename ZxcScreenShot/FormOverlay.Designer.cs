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
            this.buttonDrawUndo = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.buttonDrawResize = new System.Windows.Forms.Button();
            this.buttonDrawColor = new System.Windows.Forms.Button();
            this.buttonDrawArrow = new System.Windows.Forms.Button();
            this.buttonDrawLine = new System.Windows.Forms.Button();
            this.buttonDrawRect = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.panelOutput = new System.Windows.Forms.Panel();
            this.buttonCopy = new System.Windows.Forms.Button();
            this.buttonEditInPaint = new System.Windows.Forms.Button();
            this.buttonUrl = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonPath = new System.Windows.Forms.Button();
            this.buttonFolder = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelTools.SuspendLayout();
            this.panelOutput.SuspendLayout();
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
            this.imageList1.Images.SetKeyName(0, "folder.png");
            this.imageList1.Images.SetKeyName(1, "anchor.png");
            this.imageList1.Images.SetKeyName(2, "link.png");
            this.imageList1.Images.SetKeyName(3, "pen.png");
            this.imageList1.Images.SetKeyName(4, "copy-2.png");
            this.imageList1.Images.SetKeyName(5, "cross.png");
            this.imageList1.Images.SetKeyName(6, "rectangle.png");
            this.imageList1.Images.SetKeyName(7, "line.png");
            this.imageList1.Images.SetKeyName(8, "left.png");
            this.imageList1.Images.SetKeyName(9, "backward.png");
            this.imageList1.Images.SetKeyName(10, "target.png");
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
            this.panelOutput.Controls.Add(this.buttonCopy);
            this.panelOutput.Controls.Add(this.buttonEditInPaint);
            this.panelOutput.Controls.Add(this.buttonUrl);
            this.panelOutput.Controls.Add(this.buttonCancel);
            this.panelOutput.Controls.Add(this.buttonPath);
            this.panelOutput.Controls.Add(this.buttonFolder);
            this.panelOutput.Location = new System.Drawing.Point(166, 304);
            this.panelOutput.Name = "panelOutput";
            this.panelOutput.Size = new System.Drawing.Size(211, 36);
            this.panelOutput.TabIndex = 3;
            this.panelOutput.Visible = false;
            // 
            // buttonCopy
            // 
            this.buttonCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCopy.ImageKey = "copy-2.png";
            this.buttonCopy.ImageList = this.imageList1;
            this.buttonCopy.Location = new System.Drawing.Point(139, 2);
            this.buttonCopy.Name = "buttonCopy";
            this.buttonCopy.Size = new System.Drawing.Size(32, 32);
            this.buttonCopy.TabIndex = 10;
            this.toolTip1.SetToolTip(this.buttonCopy, "Edit (Paint)");
            this.buttonCopy.UseVisualStyleBackColor = true;
            this.buttonCopy.Click += new System.EventHandler(this.buttonCopy_Click);
            // 
            // buttonEditInPaint
            // 
            this.buttonEditInPaint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonEditInPaint.ImageKey = "pen.png";
            this.buttonEditInPaint.ImageList = this.imageList1;
            this.buttonEditInPaint.Location = new System.Drawing.Point(105, 2);
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
            this.buttonUrl.Location = new System.Drawing.Point(71, 2);
            this.buttonUrl.Name = "buttonUrl";
            this.buttonUrl.Size = new System.Drawing.Size(32, 32);
            this.buttonUrl.TabIndex = 7;
            this.toolTip1.SetToolTip(this.buttonUrl, "Copy URL");
            this.buttonUrl.UseVisualStyleBackColor = true;
            this.buttonUrl.Click += new System.EventHandler(this.buttonUrl_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonCancel.ImageKey = "cross.png";
            this.buttonCancel.ImageList = this.imageList1;
            this.buttonCancel.Location = new System.Drawing.Point(177, 2);
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
            this.buttonPath.Location = new System.Drawing.Point(37, 2);
            this.buttonPath.Name = "buttonPath";
            this.buttonPath.Size = new System.Drawing.Size(32, 32);
            this.buttonPath.TabIndex = 1;
            this.toolTip1.SetToolTip(this.buttonPath, "Copy file path");
            this.buttonPath.UseVisualStyleBackColor = true;
            this.buttonPath.Click += new System.EventHandler(this.buttonPath_Click);
            // 
            // buttonFolder
            // 
            this.buttonFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonFolder.ImageKey = "folder.png";
            this.buttonFolder.ImageList = this.imageList1;
            this.buttonFolder.Location = new System.Drawing.Point(3, 2);
            this.buttonFolder.Name = "buttonFolder";
            this.buttonFolder.Size = new System.Drawing.Size(32, 32);
            this.buttonFolder.TabIndex = 0;
            this.toolTip1.SetToolTip(this.buttonFolder, "Show in Explorer");
            this.buttonFolder.UseVisualStyleBackColor = true;
            this.buttonFolder.Click += new System.EventHandler(this.buttonFolder_Click);
            // 
            // FormOverlay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(686, 415);
            this.ControlBox = false;
            this.Controls.Add(this.panelOutput);
            this.Controls.Add(this.panelTools);
            this.Controls.Add(this.pictureBox1);
            this.KeyPreview = true;
            this.Name = "FormOverlay";
            this.Text = "Overlay";
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panelTools.ResumeLayout(false);
            this.panelOutput.ResumeLayout(false);
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
        private System.Windows.Forms.Button buttonFolder;
        private System.Windows.Forms.Button buttonUrl;
        private System.Windows.Forms.Button buttonEditInPaint;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button buttonDrawUndo;
        private System.Windows.Forms.Button buttonCopy;
    }
}

