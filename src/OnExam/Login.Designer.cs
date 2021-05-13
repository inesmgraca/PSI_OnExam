
namespace OnExam
{
    partial class Login
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblUsernameEmail = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnLogin = new System.Windows.Forms.Button();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.btnRegister = new System.Windows.Forms.Button();
            this.lblNoAccount = new System.Windows.Forms.Label();
            this.btnBack = new System.Windows.Forms.Button();
            this.providerError = new System.Windows.Forms.ErrorProvider(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.providerError)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.lblUsernameEmail, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtUsername, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblPassword, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtPassword, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.lblNoAccount, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.btnBack, 0, 0);
            this.providerError.SetError(this.tableLayoutPanel1, resources.GetString("tableLayoutPanel1.Error"));
            this.providerError.SetIconAlignment(this.tableLayoutPanel1, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("tableLayoutPanel1.IconAlignment"))));
            this.providerError.SetIconPadding(this.tableLayoutPanel1, ((int)(resources.GetObject("tableLayoutPanel1.IconPadding"))));
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // lblUsernameEmail
            // 
            resources.ApplyResources(this.lblUsernameEmail, "lblUsernameEmail");
            this.providerError.SetError(this.lblUsernameEmail, resources.GetString("lblUsernameEmail.Error"));
            this.providerError.SetIconAlignment(this.lblUsernameEmail, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblUsernameEmail.IconAlignment"))));
            this.providerError.SetIconPadding(this.lblUsernameEmail, ((int)(resources.GetObject("lblUsernameEmail.IconPadding"))));
            this.lblUsernameEmail.Name = "lblUsernameEmail";
            // 
            // txtUsername
            // 
            resources.ApplyResources(this.txtUsername, "txtUsername");
            this.providerError.SetError(this.txtUsername, resources.GetString("txtUsername.Error"));
            this.providerError.SetIconAlignment(this.txtUsername, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtUsername.IconAlignment"))));
            this.providerError.SetIconPadding(this.txtUsername, ((int)(resources.GetObject("txtUsername.IconPadding"))));
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUsername_KeyDown);
            // 
            // lblPassword
            // 
            resources.ApplyResources(this.lblPassword, "lblPassword");
            this.providerError.SetError(this.lblPassword, resources.GetString("lblPassword.Error"));
            this.providerError.SetIconAlignment(this.lblPassword, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblPassword.IconAlignment"))));
            this.providerError.SetIconPadding(this.lblPassword, ((int)(resources.GetObject("lblPassword.IconPadding"))));
            this.lblPassword.Name = "lblPassword";
            // 
            // txtPassword
            // 
            resources.ApplyResources(this.txtPassword, "txtPassword");
            this.providerError.SetError(this.txtPassword, resources.GetString("txtPassword.Error"));
            this.providerError.SetIconAlignment(this.txtPassword, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtPassword.IconAlignment"))));
            this.providerError.SetIconPadding(this.txtPassword, ((int)(resources.GetObject("txtPassword.IconPadding"))));
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.UseSystemPasswordChar = true;
            this.txtPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPassword_KeyDown);
            // 
            // tableLayoutPanel2
            // 
            resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
            this.tableLayoutPanel2.Controls.Add(this.btnLogin, 1, 0);
            this.providerError.SetError(this.tableLayoutPanel2, resources.GetString("tableLayoutPanel2.Error"));
            this.providerError.SetIconAlignment(this.tableLayoutPanel2, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("tableLayoutPanel2.IconAlignment"))));
            this.providerError.SetIconPadding(this.tableLayoutPanel2, ((int)(resources.GetObject("tableLayoutPanel2.IconPadding"))));
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // btnLogin
            // 
            resources.ApplyResources(this.btnLogin, "btnLogin");
            this.providerError.SetError(this.btnLogin, resources.GetString("btnLogin.Error"));
            this.providerError.SetIconAlignment(this.btnLogin, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("btnLogin.IconAlignment"))));
            this.providerError.SetIconPadding(this.btnLogin, ((int)(resources.GetObject("btnLogin.IconPadding"))));
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // tableLayoutPanel3
            // 
            resources.ApplyResources(this.tableLayoutPanel3, "tableLayoutPanel3");
            this.tableLayoutPanel3.Controls.Add(this.btnRegister, 1, 0);
            this.providerError.SetError(this.tableLayoutPanel3, resources.GetString("tableLayoutPanel3.Error"));
            this.providerError.SetIconAlignment(this.tableLayoutPanel3, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("tableLayoutPanel3.IconAlignment"))));
            this.providerError.SetIconPadding(this.tableLayoutPanel3, ((int)(resources.GetObject("tableLayoutPanel3.IconPadding"))));
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            // 
            // btnRegister
            // 
            resources.ApplyResources(this.btnRegister, "btnRegister");
            this.providerError.SetError(this.btnRegister, resources.GetString("btnRegister.Error"));
            this.providerError.SetIconAlignment(this.btnRegister, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("btnRegister.IconAlignment"))));
            this.providerError.SetIconPadding(this.btnRegister, ((int)(resources.GetObject("btnRegister.IconPadding"))));
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.UseVisualStyleBackColor = true;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // lblNoAccount
            // 
            resources.ApplyResources(this.lblNoAccount, "lblNoAccount");
            this.providerError.SetError(this.lblNoAccount, resources.GetString("lblNoAccount.Error"));
            this.providerError.SetIconAlignment(this.lblNoAccount, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblNoAccount.IconAlignment"))));
            this.providerError.SetIconPadding(this.lblNoAccount, ((int)(resources.GetObject("lblNoAccount.IconPadding"))));
            this.lblNoAccount.Name = "lblNoAccount";
            // 
            // btnBack
            // 
            resources.ApplyResources(this.btnBack, "btnBack");
            this.providerError.SetError(this.btnBack, resources.GetString("btnBack.Error"));
            this.providerError.SetIconAlignment(this.btnBack, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("btnBack.IconAlignment"))));
            this.providerError.SetIconPadding(this.btnBack, ((int)(resources.GetObject("btnBack.IconPadding"))));
            this.btnBack.Name = "btnBack";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // providerError
            // 
            this.providerError.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.providerError.ContainerControl = this;
            resources.ApplyResources(this.providerError, "providerError");
            // 
            // Login
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Login";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Login_FormClosed);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.providerError)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblUsernameEmail;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.Label lblNoAccount;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.ErrorProvider providerError;
    }
}