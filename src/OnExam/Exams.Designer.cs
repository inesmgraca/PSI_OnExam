
namespace OnExam
{
    partial class Exams
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dataGridExams = new System.Windows.Forms.DataGridView();
            this.ExameID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Duracao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsRandom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnOpen = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridExams)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.Controls.Add(this.dataGridExams, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(734, 441);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // dataGridExams
            // 
            this.dataGridExams.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridExams.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ExameID,
            this.Duracao,
            this.IsRandom,
            this.Estado});
            this.dataGridExams.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridExams.Location = new System.Drawing.Point(76, 47);
            this.dataGridExams.Name = "dataGridExams";
            this.dataGridExams.Size = new System.Drawing.Size(581, 302);
            this.dataGridExams.TabIndex = 0;
            // 
            // ExameID
            // 
            this.ExameID.HeaderText = "ID";
            this.ExameID.Name = "ExameID";
            // 
            // Duracao
            // 
            this.Duracao.HeaderText = "Duração";
            this.Duracao.Name = "Duracao";
            // 
            // IsRandom
            // 
            this.IsRandom.HeaderText = "Aleatório";
            this.IsRandom.Name = "IsRandom";
            // 
            // Estado
            // 
            this.Estado.HeaderText = "Estado";
            this.Estado.Name = "Estado";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 5;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.Controls.Add(this.btnNew, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnDelete, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnOpen, 4, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(76, 355);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 69F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(581, 60);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // btnNew
            // 
            this.btnNew.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnNew.Location = new System.Drawing.Point(3, 3);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(110, 54);
            this.btnNew.TabIndex = 0;
            this.btnNew.Text = "Novo";
            this.btnNew.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            this.btnDelete.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDelete.Location = new System.Drawing.Point(235, 3);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(110, 54);
            this.btnDelete.TabIndex = 1;
            this.btnDelete.Text = "Eliminar";
            this.btnDelete.UseVisualStyleBackColor = true;
            // 
            // btnOpen
            // 
            this.btnOpen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnOpen.Location = new System.Drawing.Point(467, 3);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(111, 54);
            this.btnOpen.TabIndex = 2;
            this.btnOpen.Text = "Abrir";
            this.btnOpen.UseVisualStyleBackColor = true;
            // 
            // Exams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(734, 441);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Arial", 9F);
            this.Name = "Exams";
            this.Text = "Exams";
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridExams)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView dataGridExams;
        private System.Windows.Forms.DataGridViewTextBoxColumn ExameID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Duracao;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsRandom;
        private System.Windows.Forms.DataGridViewTextBoxColumn Estado;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnOpen;
    }
}