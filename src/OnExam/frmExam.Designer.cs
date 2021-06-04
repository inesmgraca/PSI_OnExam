
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
            this.stripTxtDuration = new System.Windows.Forms.ToolStripTextBox();
            this.stripLblDuration = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.stripTxtExamName = new System.Windows.Forms.ToolStripTextBox();
            this.stripLblExamName = new System.Windows.Forms.ToolStripLabel();
            this.chkIsRandom = new System.Windows.Forms.CheckBox();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stripBtnRdb,
            this.stripBtnChk,
            this.stripBtnTxt,
            this.stripTxtDuration,
            this.stripLblDuration,
            this.toolStripSeparator1,
            this.stripTxtExamName,
            this.stripLblExamName});
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
            // stripTxtDuration
            // 
            resources.ApplyResources(this.stripTxtDuration, "stripTxtDuration");
            this.stripTxtDuration.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.stripTxtDuration.Name = "stripTxtDuration";
            // 
            // stripLblDuration
            // 
            resources.ApplyResources(this.stripLblDuration, "stripLblDuration");
            this.stripLblDuration.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.stripLblDuration.Name = "stripLblDuration";
            // 
            // toolStripSeparator1
            // 
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            this.toolStripSeparator1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            // 
            // stripTxtExamName
            // 
            resources.ApplyResources(this.stripTxtExamName, "stripTxtExamName");
            this.stripTxtExamName.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.stripTxtExamName.Name = "stripTxtExamName";
            // 
            // stripLblExamName
            // 
            resources.ApplyResources(this.stripLblExamName, "stripLblExamName");
            this.stripLblExamName.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.stripLblExamName.Name = "stripLblExamName";
            // 
            // chkIsRandom
            // 
            resources.ApplyResources(this.chkIsRandom, "chkIsRandom");
            this.chkIsRandom.BackColor = System.Drawing.Color.Transparent;
            this.chkIsRandom.Name = "chkIsRandom";
            this.chkIsRandom.UseVisualStyleBackColor = false;
            // 
            // frmExam
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkIsRandom);
            this.Controls.Add(this.toolStrip1);
            this.IsMdiContainer = true;
            this.Name = "frmExam";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmExam_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmExam_FormClosed);
            this.Load += new System.EventHandler(this.frmExam_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton stripBtnRdb;
        private System.Windows.Forms.ToolStripButton stripBtnChk;
        private System.Windows.Forms.CheckBox chkIsRandom;
        private System.Windows.Forms.ToolStripButton stripBtnTxt;
        private System.Windows.Forms.ToolStripTextBox stripTxtDuration;
        private System.Windows.Forms.ToolStripLabel stripLblDuration;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripTextBox stripTxtExamName;
        private System.Windows.Forms.ToolStripLabel stripLblExamName;
    }
}