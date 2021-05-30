
namespace OnExam
{
    partial class frmExam
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmExam));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.stripBtnRdb = new System.Windows.Forms.ToolStripButton();
            this.stripBtnChk = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stripBtnRdb,
            this.stripBtnChk});
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.Name = "toolStrip1";
            // 
            // stripBtnRdb
            // 
            resources.ApplyResources(this.stripBtnRdb, "stripBtnRdb");
            this.stripBtnRdb.Name = "stripBtnRdb";
            this.stripBtnRdb.Click += new System.EventHandler(this.stripBtnRdb_Click);
            // 
            // stripBtnChk
            // 
            resources.ApplyResources(this.stripBtnChk, "stripBtnChk");
            this.stripBtnChk.Name = "stripBtnChk";
            this.stripBtnChk.Click += new System.EventHandler(this.stripBtnChk_Click);
            // 
            // frmExam
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStrip1);
            this.IsMdiContainer = true;
            this.Name = "frmExam";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton stripBtnRdb;
        private System.Windows.Forms.ToolStripButton stripBtnChk;
    }
}