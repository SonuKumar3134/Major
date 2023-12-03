namespace MissionPlanner.GCSViews
{
    partial class Speech
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.myButton1 = new MissionPlanner.Controls.MyButton();
            this.myLabel1 = new MissionPlanner.Controls.MyLabel();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(310, 215);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(271, 167);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            this.richTextBox1.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // myButton1
            // 
            this.myButton1.Location = new System.Drawing.Point(583, 146);
            this.myButton1.Name = "myButton1";
            this.myButton1.Size = new System.Drawing.Size(147, 46);
            this.myButton1.TabIndex = 1;
            this.myButton1.Text = "myButton1";
            this.myButton1.UseVisualStyleBackColor = true;
            this.myButton1.Click += new System.EventHandler(this.myButton1_Click);
            // 
            // myLabel1
            // 
            this.myLabel1.Location = new System.Drawing.Point(294, 136);
            this.myLabel1.Name = "myLabel1";
            this.myLabel1.resize = false;
            this.myLabel1.Size = new System.Drawing.Size(187, 55);
            this.myLabel1.TabIndex = 2;
            this.myLabel1.Text = "myLabel1";
            // 
            // Speech
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.myLabel1);
            this.Controls.Add(this.myButton1);
            this.Controls.Add(this.richTextBox1);
            this.Name = "Speech";
            this.Size = new System.Drawing.Size(844, 496);
            this.Load += new System.EventHandler(this.Speech_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private Controls.MyButton myButton1;
        private Controls.MyLabel myLabel1;
    }
}
