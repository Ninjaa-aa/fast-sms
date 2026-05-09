namespace SMM_PROJ.Forms.Auth
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            lblTitle = new Label();
            lblEmail = new Label();
            lblPassword = new Label();
            txtEmail = new TextBox();
            txtPassword = new TextBox();
            btnLogin = new Button();
            lnkRegister = new LinkLabel();

            SuspendLayout();

            // lblTitle
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitle.Location = new Point(100, 30);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(350, 32);
            lblTitle.Text = "Societies Management System";

            // lblEmail
            lblEmail.AutoSize = true;
            lblEmail.Location = new Point(100, 100);
            lblEmail.Text = "Email:";

            // txtEmail
            txtEmail.Location = new Point(100, 125);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(300, 23);

            // lblPassword
            lblPassword.AutoSize = true;
            lblPassword.Location = new Point(100, 165);
            lblPassword.Text = "Password:";

            // txtPassword
            txtPassword.Location = new Point(100, 190);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.Size = new Size(300, 23);

            // btnLogin
            btnLogin.Location = new Point(100, 240);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(300, 35);
            btnLogin.Text = "Login";
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += BtnLogin_Click;

            // lnkRegister
            lnkRegister.AutoSize = true;
            lnkRegister.Location = new Point(140, 290);
            lnkRegister.Name = "lnkRegister";
            lnkRegister.Size = new Size(200, 15);
            lnkRegister.Text = "Don't have an account? Register";
            lnkRegister.LinkClicked += LnkRegister_LinkClicked;

            // LoginForm
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(500, 350);
            Controls.Add(lblTitle);
            Controls.Add(lblEmail);
            Controls.Add(txtEmail);
            Controls.Add(lblPassword);
            Controls.Add(txtPassword);
            Controls.Add(btnLogin);
            Controls.Add(lnkRegister);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Login - Societies Management System";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private Label lblEmail;
        private Label lblPassword;
        private TextBox txtEmail;
        private TextBox txtPassword;
        private Button btnLogin;
        private LinkLabel lnkRegister;
    }
}
