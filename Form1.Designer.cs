namespace ImageProcessing
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            loadToolStripMenuItem = new ToolStripMenuItem();
            imageToolStripMenuItem = new ToolStripMenuItem();
            webcamToolStripMenuItem = new ToolStripMenuItem();
            processToolStripMenuItem = new ToolStripMenuItem();
            basicCopyToolStripMenuItem = new ToolStripMenuItem();
            greyscaleToolStripMenuItem = new ToolStripMenuItem();
            colorInversionToolStripMenuItem = new ToolStripMenuItem();
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            pictureBox3 = new PictureBox();
            histogramToolStripMenuItem = new ToolStripMenuItem();
            sepiaToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { loadToolStripMenuItem, processToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(881, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // loadToolStripMenuItem
            // 
            loadToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { imageToolStripMenuItem, webcamToolStripMenuItem });
            loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            loadToolStripMenuItem.Size = new Size(45, 20);
            loadToolStripMenuItem.Text = "Load";
            // 
            // imageToolStripMenuItem
            // 
            imageToolStripMenuItem.Name = "imageToolStripMenuItem";
            imageToolStripMenuItem.Size = new Size(121, 22);
            imageToolStripMenuItem.Text = "Image";
            imageToolStripMenuItem.Click += imageToolStripMenuItem_Click;
            // 
            // webcamToolStripMenuItem
            // 
            webcamToolStripMenuItem.Name = "webcamToolStripMenuItem";
            webcamToolStripMenuItem.Size = new Size(121, 22);
            webcamToolStripMenuItem.Text = "Webcam";
            // 
            // processToolStripMenuItem
            // 
            processToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { basicCopyToolStripMenuItem, greyscaleToolStripMenuItem, colorInversionToolStripMenuItem, histogramToolStripMenuItem, sepiaToolStripMenuItem });
            processToolStripMenuItem.Name = "processToolStripMenuItem";
            processToolStripMenuItem.Size = new Size(59, 20);
            processToolStripMenuItem.Text = "Process";
            // 
            // basicCopyToolStripMenuItem
            // 
            basicCopyToolStripMenuItem.Name = "basicCopyToolStripMenuItem";
            basicCopyToolStripMenuItem.Size = new Size(180, 22);
            basicCopyToolStripMenuItem.Text = "Basic Copy";
            basicCopyToolStripMenuItem.Click += basicCopyToolStripMenuItem_Click;
            // 
            // greyscaleToolStripMenuItem
            // 
            greyscaleToolStripMenuItem.Name = "greyscaleToolStripMenuItem";
            greyscaleToolStripMenuItem.Size = new Size(180, 22);
            greyscaleToolStripMenuItem.Text = "Greyscale";
            greyscaleToolStripMenuItem.Click += greyscaleToolStripMenuItem_Click;
            // 
            // colorInversionToolStripMenuItem
            // 
            colorInversionToolStripMenuItem.Name = "colorInversionToolStripMenuItem";
            colorInversionToolStripMenuItem.Size = new Size(180, 22);
            colorInversionToolStripMenuItem.Text = "Color Inversion";
            colorInversionToolStripMenuItem.Click += colorInversionToolStripMenuItem_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(12, 27);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(280, 279);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.Location = new Point(298, 27);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(280, 279);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 2;
            pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            pictureBox3.Location = new Point(584, 27);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(280, 279);
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.TabIndex = 3;
            pictureBox3.TabStop = false;
            // 
            // histogramToolStripMenuItem
            // 
            histogramToolStripMenuItem.Name = "histogramToolStripMenuItem";
            histogramToolStripMenuItem.Size = new Size(180, 22);
            histogramToolStripMenuItem.Text = "Histogram";
            // 
            // sepiaToolStripMenuItem
            // 
            sepiaToolStripMenuItem.Name = "sepiaToolStripMenuItem";
            sepiaToolStripMenuItem.Size = new Size(180, 22);
            sepiaToolStripMenuItem.Text = "Sepia";
            sepiaToolStripMenuItem.Click += sepiaToolStripMenuItem_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(881, 450);
            Controls.Add(pictureBox3);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "Form1";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem loadToolStripMenuItem;
        private ToolStripMenuItem imageToolStripMenuItem;
        private ToolStripMenuItem webcamToolStripMenuItem;
        private ToolStripMenuItem processToolStripMenuItem;
        private ToolStripMenuItem basicCopyToolStripMenuItem;
        private ToolStripMenuItem greyscaleToolStripMenuItem;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private PictureBox pictureBox3;
        private ToolStripMenuItem colorInversionToolStripMenuItem;
        private ToolStripMenuItem histogramToolStripMenuItem;
        private ToolStripMenuItem sepiaToolStripMenuItem;
    }
}
