namespace MissionPlanner.GCSViews
{
    partial class Speech1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Speech1));
            this.myLabel2 = new MissionPlanner.Controls.MyLabel();
            this.myLabel3 = new MissionPlanner.Controls.MyLabel();
            this.SuspendLayout();
            // 
            // myLabel2
            // 
            this.myLabel2.ForeColor = System.Drawing.Color.Red;
            resources.ApplyResources(this.myLabel2, "myLabel2");
            this.myLabel2.Name = "myLabel2";
            this.myLabel2.resize = false;
            // 
            // myLabel3
            // 
            resources.ApplyResources(this.myLabel3, "myLabel3");
            this.myLabel3.Name = "myLabel3";
            this.myLabel3.resize = false;
            // 
            // Speech1
            // 
            this.Controls.Add(this.myLabel3);
            this.Controls.Add(this.myLabel2);
            this.Name = "Speech1";
            resources.ApplyResources(this, "$this");
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.MyButton myButton1;
        private Controls.MyLabel myLabel1;
        private Controls.MyLabel myLabel2;
        private Controls.MyLabel myLabel3;

    }
}
