
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
            this.stripLblDuration = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.stripTxtExamName = new System.Windows.Forms.ToolStripTextBox();
            this.stripLblExamName = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.stripBtnActivate = new System.Windows.Forms.ToolStripButton();
            this.chkIsRandom = new System.Windows.Forms.CheckBox();
            this.nudDuration = new System.Windows.Forms.NumericUpDown();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDuration)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stripBtnRdb,
            this.stripBtnChk,
            this.stripBtnTxt,
            this.stripLblDuration,
            this.toolStripSeparator1,
            this.stripTxtExamName,
            this.stripLblExamName,
            this.toolStripSeparator2,
            this.stripBtnActivate});
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.Name = "toolStrip1";
            // 
            // stripBtnRdb
            // 
            this.stripBtnRdb.Image = global::OnExam.Properties.Resources.img_stripBtnRdb;
            resources.ApplyResources(this.stripBtnRdb, "stripBtnRdb");
            this.stripBtnRdb.Name = "stripBtnRdb";
            this.stripBtnRdb.Click += new System.EventHandler(this.stripBtnRdb_Click);
            // 
            // stripBtnChk
            // 
            this.stripBtnChk.Image = global::OnExam.Properties.Resources.img_stripBtnChk;
            resources.ApplyResources(this.stripBtnChk, "stripBtnChk");
            this.stripBtnChk.Name = "stripBtnChk";
            this.stripBtnChk.Click += new System.EventHandler(this.stripBtnChk_Click);
            // 
            // stripBtnTxt
            // 
            this.stripBtnTxt.Image = global::OnExam.Properties.Resources.img_stripBtnTxt;
            resources.ApplyResources(this.stripBtnTxt, "stripBtnTxt");
            this.stripBtnTxt.Name = "stripBtnTxt";
            this.stripBtnTxt.Click += new System.EventHandler(this.stripBtnTxt_Click);
            // 
            // stripLblDuration
            // 
            this.stripLblDuration.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.stripLblDuration.Name = "stripLblDuration";
            resources.ApplyResources(this.stripLblDuration, "stripLblDuration");
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // stripTxtExamName
            // 
            this.stripTxtExamName.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            resources.ApplyResources(this.stripTxtExamName, "stripTxtExamName");
            this.stripTxtExamName.Name = "stripTxtExamName";
            // 
            // stripLblExamName
            // 
            this.stripLblExamName.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.stripLblExamName.Name = "stripLblExamName";
            resources.ApplyResources(this.stripLblExamName, "stripLblExamName");
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // stripBtnActivate
            // 
            this.stripBtnActivate.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.stripBtnActivate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(this.stripBtnActivate, "stripBtnActivate");
            this.stripBtnActivate.Name = "stripBtnActivate";
            this.stripBtnActivate.Click += new System.EventHandler(this.stripBtnActivate_Click);
            // 
            // chkIsRandom
            // 
            resources.ApplyResources(this.chkIsRandom, "chkIsRandom");
            this.chkIsRandom.BackColor = System.Drawing.Color.Transparent;
            this.chkIsRandom.Name = "chkIsRandom";
            this.chkIsRandom.UseVisualStyleBackColor = false;
            // 
            // nudDuration
            // 
            resources.ApplyResources(this.nudDuration, "nudDuration");
            this.nudDuration.Maximum = new decimal(new int[] {
            240,
            0,
            0,
            0});
            this.nudDuration.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudDuration.Name = "nudDuration";
            this.nudDuration.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // frmExam
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.nudDuration);
            this.Controls.Add(this.chkIsRandom);
            this.Controls.Add(this.toolStrip1);
            this.IsMdiContainer = true;
            this.Name = "frmExam";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmExam_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmExam_FormClosed);
            this.Load += new System.EventHandler(this.frmExam_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDuration)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton stripBtnRdb;
        private System.Windows.Forms.ToolStripButton stripBtnChk;
        private System.Windows.Forms.CheckBox chkIsRandom;
        private System.Windows.Forms.ToolStripButton stripBtnTxt;
        private System.Windows.Forms.ToolStripLabel stripLblDuration;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripTextBox stripTxtExamName;
        private System.Windows.Forms.ToolStripLabel stripLblExamName;
        private System.Windows.Forms.NumericUpDown nudDuration;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton stripBtnActivate;
    }
}