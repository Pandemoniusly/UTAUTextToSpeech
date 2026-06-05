namespace utautexttospeech
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            button1 = new Button();
            openFileDialog1 = new OpenFileDialog();
            button2 = new Button();
            textBox3 = new TextBox();
            pictureBox1 = new PictureBox();
            button3 = new Button();
            textBox4 = new TextBox();
            folderBrowserDialog1 = new FolderBrowserDialog();
            progressBar1 = new ProgressBar();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.BackColor = SystemColors.HotTrack;
            textBox1.Location = new Point(12, 12);
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.Size = new Size(492, 23);
            textBox1.TabIndex = 0;
            textBox1.Text = "Input Text Below";
            textBox1.TextAlign = HorizontalAlignment.Center;
            // 
            // textBox2
            // 
            textBox2.BackColor = SystemColors.HotTrack;
            textBox2.Location = new Point(12, 41);
            textBox2.Multiline = true;
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(492, 174);
            textBox2.TabIndex = 1;
            textBox2.TextChanged += textBox2_TextChanged;
            // 
            // button1
            // 
            button1.Location = new Point(12, 221);
            button1.Name = "button1";
            button1.Size = new Size(492, 23);
            button1.TabIndex = 2;
            button1.Text = "Run my vbirus";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "ustx only";
            // 
            // button2
            // 
            button2.Location = new Point(12, 250);
            button2.Name = "button2";
            button2.Size = new Size(130, 23);
            button2.TabIndex = 3;
            button2.Text = "File Path";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // textBox3
            // 
            textBox3.BackColor = SystemColors.HotTrack;
            textBox3.Location = new Point(148, 250);
            textBox3.Name = "textBox3";
            textBox3.ReadOnly = true;
            textBox3.Size = new Size(356, 23);
            textBox3.TabIndex = 4;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(512, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(130, 129);
            pictureBox1.TabIndex = 5;
            pictureBox1.TabStop = false;
            // 
            // button3
            // 
            button3.Location = new Point(12, 279);
            button3.Name = "button3";
            button3.Size = new Size(130, 23);
            button3.TabIndex = 6;
            button3.Text = "Cache/Output Path";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // textBox4
            // 
            textBox4.BackColor = SystemColors.HotTrack;
            textBox4.Location = new Point(148, 279);
            textBox4.Name = "textBox4";
            textBox4.ReadOnly = true;
            textBox4.Size = new Size(356, 23);
            textBox4.TabIndex = 7;
            // 
            // progressBar1
            // 
            progressBar1.BackColor = SystemColors.Control;
            progressBar1.Location = new Point(510, 147);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(132, 17);
            progressBar1.Step = 1;
            progressBar1.Style = ProgressBarStyle.Continuous;
            progressBar1.TabIndex = 8;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(654, 308);
            Controls.Add(progressBar1);
            Controls.Add(textBox4);
            Controls.Add(button3);
            Controls.Add(pictureBox1);
            Controls.Add(textBox3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Name = "Form1";
            Text = "the creature feature";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
        #endregion

        private TextBox textBox1;
        private TextBox textBox2;
        private Button button1;
        private OpenFileDialog openFileDialog1;
        private Button button2;
        private TextBox textBox3;
        private PictureBox pictureBox1;
        private Button button3;
        private TextBox textBox4;
        private FolderBrowserDialog folderBrowserDialog1;
        private ProgressBar progressBar1;
    }
}
