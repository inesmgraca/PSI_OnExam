
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
            this.stripBtnTxt = new System.Windows.Forms.ToolStripButton();
            this.lblExamName = new System.Windows.Forms.Label();
            this.lblDuration = new System.Windows.Forms.Label();
            this.chkIsRandom = new System.Windows.Forms.CheckBox();
            this.txtExamName = new System.Windows.Forms.TextBox();
            this.txtDuration = new System.Windows.Forms.TextBox();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stripBtnRdb,
            this.stripBtnChk,
            this.stripBtnTxt});
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
            // stripBtnTxt
            // 
            resources.ApplyResources(this.stripBtnTxt, "stripBtnTxt");
            this.stripBtnTxt.Name = "stripBtnTxt";
            this.stripBtnTxt.Click += new System.EventHandler(this.stripBtnTxt_Click);
            // 
            // lblExamName
            // 
            resources.ApplyResources(this.lblExamName, "lblExamName");
            this.lblExamName.BackColor = System.Drawing.Color.Transparent;
            this.lblExamName.Name = "lblExamName";
            // 
            // lblDuration
            // 
            resources.ApplyResources(this.lblDuration, "lblDuration");
            this.lblDuration.BackColor = System.Drawing.Color.Transparent;
            this.lblDuration.Name = "lblDuration";
            // 
            // chkIsRandom
            // 
            resources.ApplyResources(this.chkIsRandom, "chkIsRandom");
            this.chkIsRandom.BackColor = System.Drawing.Color.Transparent;
            this.chkIsRandom.Name = "chkIsRandom";
            this.chkIsRandom.UseVisualStyleBackColor = false;
            // 
            // txtExamName
            // 
            resources.ApplyResources(this.txtExamName, "txtExamName");
            this.txtExamName.Name = "txtExamName";
            // 
            // txtDuration
            // 
            resources.ApplyResources(this.txtDuration, "txtDuration");
            this.txtDuration.Name = "txtDuration";
            // 
            // frmExam
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtDuration);
            this.Controls.Add(this.txtExamName);
            this.Controls.Add(this.chkIsRandom);
            this.Controls.Add(this.lblDuration);
            this.Controls.Add(this.lblExamName);
            this.Controls.Add(this.toolStrip1);
            this.IsMdiContainer = true;
            this.Name = "frmExam";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmExam_FormClosing);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton stripBtnRdb;
        private System.Windows.Forms.ToolStripButton stripBtnChk;
        private System.Windows.Forms.Label lblExamName;
        private System.Windows.Forms.Label lblDuration;
        private System.Windows.Forms.CheckBox chkIsRandom;
        private System.Windows.Forms.TextBox txtExamName;
        private System.Windows.Forms.TextBox txtDuration;
        private System.Windows.Forms.ToolStripButton stripBtnTxt;
    }
}