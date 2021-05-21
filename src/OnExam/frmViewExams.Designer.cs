﻿
namespace OnExam
{
    partial class frmViewExams
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmViewExams));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dataGridExams = new System.Windows.Forms.DataGridView();
            this.ExamID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ExamName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Duration = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isRandom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.State = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnProfile = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridExams)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.dataGridExams, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnProfile, 2, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // dataGridExams
            // 
            this.dataGridExams.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridExams.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ExamID,
            this.ExamName,
            this.Duration,
            this.isRandom,
            this.State});
            resources.ApplyResources(this.dataGridExams, "dataGridExams");
            this.dataGridExams.Name = "dataGridExams";
            this.dataGridExams.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // ExamID
            // 
            this.ExamID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ExamID.DataPropertyName = "ExamID";
            resources.ApplyResources(this.ExamID, "ExamID");
            this.ExamID.Name = "ExamID";
            this.ExamID.ReadOnly = true;
            // 
            // ExamName
            // 
            this.ExamName.DataPropertyName = "ExamName";
            resources.ApplyResources(this.ExamName, "ExamName");
            this.ExamName.Name = "ExamName";
            this.ExamName.ReadOnly = true;
            // 
            // Duration
            // 
            this.Duration.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Duration.DataPropertyName = "Duration";
            resources.ApplyResources(this.Duration, "Duration");
            this.Duration.Name = "Duration";
            this.Duration.ReadOnly = true;
            // 
            // isRandom
            // 
            this.isRandom.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.isRandom.DataPropertyName = "isRandom";
            resources.ApplyResources(this.isRandom, "isRandom");
            this.isRandom.Name = "isRandom";
            this.isRandom.ReadOnly = true;
            // 
            // State
            // 
            this.State.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.State.DataPropertyName = "State";
            resources.ApplyResources(this.State, "State");
            this.State.Name = "State";
            this.State.ReadOnly = true;
            // 
            // tableLayoutPanel2
            // 
            resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
            this.tableLayoutPanel2.Controls.Add(this.btnNew, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnDelete, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnOpen, 4, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // btnNew
            // 
            resources.ApplyResources(this.btnNew, "btnNew");
            this.btnNew.Name = "btnNew";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnDelete
            // 
            resources.ApplyResources(this.btnDelete, "btnDelete");
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnOpen
            // 
            resources.ApplyResources(this.btnOpen, "btnOpen");
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnProfile
            // 
            resources.ApplyResources(this.btnProfile, "btnProfile");
            this.btnProfile.Name = "btnProfile";
            this.btnProfile.UseVisualStyleBackColor = true;
            this.btnProfile.Click += new System.EventHandler(this.btnProfile_Click);
            // 
            // ViewExams
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ViewExams";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Exams_FormClosed);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridExams)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView dataGridExams;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.DataGridViewTextBoxColumn ExamID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ExamName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Duration;
        private System.Windows.Forms.DataGridViewTextBoxColumn isRandom;
        private System.Windows.Forms.DataGridViewTextBoxColumn State;
        private System.Windows.Forms.Button btnProfile;
    }
}