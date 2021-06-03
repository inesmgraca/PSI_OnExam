
namespace OnExam
{
    partial class frmExamText
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmExamText));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.txtQuestion = new System.Windows.Forms.TextBox();
            this.txtNotesAnswer = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.txtQuestion, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtNotesAnswer, 1, 1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // txtQuestion
            // 
            resources.ApplyResources(this.txtQuestion, "txtQuestion");
            this.txtQuestion.Name = "txtQuestion";
            // 
            // txtNotesAnswer
            // 
            resources.ApplyResources(this.txtNotesAnswer, "txtNotesAnswer");
            this.txtNotesAnswer.Name = "txtNotesAnswer";
            // 
            // frmExamText
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "frmExamText";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox txtQuestion;
        private System.Windows.Forms.TextBox txtNotesAnswer;
    }
}