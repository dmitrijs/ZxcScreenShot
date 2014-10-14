namespace LighterShot
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panelTools = new System.Windows.Forms.Panel();
            this.buttonDrawColor = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonDrawArrow = new System.Windows.Forms.Button();
            this.buttonDrawLine = new System.Windows.Forms.Button();
            this.buttonDrawRect = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.buttonDone = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelTools.SuspendLayout();
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
            this.panelTools.Controls.Add(this.buttonDone);
            this.panelTools.Controls.Add(this.buttonDrawColor);
            this.panelTools.Controls.Add(this.buttonCancel);
            this.panelTools.Controls.Add(this.buttonDrawArrow);
            this.panelTools.Controls.Add(this.buttonDrawLine);
            this.panelTools.Controls.Add(this.buttonDrawRect);
            this.panelTools.Location = new System.Drawing.Point(400, 46);
            this.panelTools.Name = "panelTools";
            this.panelTools.Size = new System.Drawing.Size(53, 252);
            this.panelTools.TabIndex = 2;
            this.panelTools.Visible = false;
            // 
            // buttonDrawColor
            // 
            this.buttonDrawColor.BackColor = System.Drawing.Color.Red;
            this.buttonDrawColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDrawColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonDrawColor.ForeColor = System.Drawing.Color.Black;
            this.buttonDrawColor.Location = new System.Drawing.Point(4, 158);
            this.buttonDrawColor.Name = "buttonDrawColor";
            this.buttonDrawColor.Size = new System.Drawing.Size(45, 36);
            this.buttonDrawColor.TabIndex = 7;
            this.buttonDrawColor.UseVisualStyleBackColor = false;
            this.buttonDrawColor.Click += new System.EventHandler(this.buttonDrawColor_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonCancel.Location = new System.Drawing.Point(3, 207);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(47, 42);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "X";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonDrawArrow
            // 
            this.buttonDrawArrow.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonDrawArrow.Location = new System.Drawing.Point(3, 81);
            this.buttonDrawArrow.Name = "buttonDrawArrow";
            this.buttonDrawArrow.Size = new System.Drawing.Size(47, 39);
            this.buttonDrawArrow.TabIndex = 4;
            this.buttonDrawArrow.TabStop = false;
            this.buttonDrawArrow.Text = "arrow";
            this.buttonDrawArrow.UseVisualStyleBackColor = true;
            this.buttonDrawArrow.Click += new System.EventHandler(this.buttonDrawArrow_Click);
            this.buttonDrawArrow.KeyDown += new System.Windows.Forms.KeyEventHandler(this.buttonDrawArrow_KeyDown);
            this.buttonDrawArrow.KeyUp += new System.Windows.Forms.KeyEventHandler(this.buttonDrawArrow_KeyUp);
            // 
            // buttonDrawLine
            // 
            this.buttonDrawLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonDrawLine.Location = new System.Drawing.Point(3, 42);
            this.buttonDrawLine.Name = "buttonDrawLine";
            this.buttonDrawLine.Size = new System.Drawing.Size(47, 37);
            this.buttonDrawLine.TabIndex = 3;
            this.buttonDrawLine.Text = "line";
            this.buttonDrawLine.UseVisualStyleBackColor = true;
            this.buttonDrawLine.Click += new System.EventHandler(this.buttonDrawLine_Click);
            // 
            // buttonDrawRect
            // 
            this.buttonDrawRect.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonDrawRect.Location = new System.Drawing.Point(3, 3);
            this.buttonDrawRect.Name = "buttonDrawRect";
            this.buttonDrawRect.Size = new System.Drawing.Size(47, 37);
            this.buttonDrawRect.TabIndex = 2;
            this.buttonDrawRect.TabStop = false;
            this.buttonDrawRect.Text = "rect";
            this.buttonDrawRect.UseVisualStyleBackColor = true;
            this.buttonDrawRect.Click += new System.EventHandler(this.buttonDrawRect_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // buttonDone
            // 
            this.buttonDone.Location = new System.Drawing.Point(4, 121);
            this.buttonDone.Name = "buttonDone";
            this.buttonDone.Size = new System.Drawing.Size(45, 36);
            this.buttonDone.TabIndex = 8;
            this.buttonDone.Text = "done";
            this.buttonDone.UseVisualStyleBackColor = true;
            this.buttonDone.Click += new System.EventHandler(this.buttonDone_Click);
            // 
            // FormOverlay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(686, 415);
            this.Controls.Add(this.panelTools);
            this.Controls.Add(this.pictureBox1);
            this.Name = "FormOverlay";
            this.Text = "Overlay";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormOverlay_FormClosed);
            this.Load += new System.EventHandler(this.FormOverlay_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panelTools.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panelTools;
        private System.Windows.Forms.Button buttonDrawRect;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonDrawArrow;
        private System.Windows.Forms.Button buttonDrawLine;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button buttonDrawColor;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button buttonDone;
    }
}

