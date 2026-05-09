namespace SMM_PROJ.Forms.Auth
{
    partial class RegisterForm
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
            lblFullName = new Label();
            lblEmail = new Label();
            lblPassword = new Label();
            lblConfirmPassword = new Label();
            lblRole = new Label();
            txtFullName = new TextBox();
            txtEmail = new TextBox();
            txtPassword = new TextBox();
            txtConfirmPassword = new TextBox();
            cmbRole = new ComboBox();
            btnRegister = new Button();
            lnkLogin = new LinkLabel();

            SuspendLayout();

            // lblTitle
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.Location = new Point(140, 20);
            lblTitle.Text = "Create New Account";

            // lblFullName
            lblFullName.AutoSize = true;
            lblFullName.Location = new Point(100, 75);
            lblFullName.Text = "Full Name:";

            // txtFullName
            txtFullName.Location = new Point(100, 95);
            txtFullName.Size = new Size(300, 23);

            // lblEmail
            lblEmail.AutoSize = true;
            lblEmail.Location = new Point(100, 130);
            lblEmail.Text = "Email:";

            // txtEmail
            txtEmail.Location = new Point(100, 150);
            txtEmail.Size = new Size(300, 23);

            // lblPassword
            lblPassword.AutoSize = true;
            lblPassword.Location = new Point(100, 185);
            lblPassword.Text = "Password:";

            // txtPassword
            txtPassword.Location = new Point(100, 205);
            txtPassword.PasswordChar = '*';
            txtPassword.Size = new Size(300, 23);

            // lblConfirmPassword
            lblConfirmPassword.AutoSize = true;
            lblConfirmPassword.Location = new Point(100, 240);
            lblConfirmPassword.Text = "Confirm Password:";

            // txtConfirmPassword
            txtConfirmPassword.Location = new Point(100, 260);
            txtConfirmPassword.PasswordChar = '*';
            txtConfirmPassword.Size = new Size(300, 23);

            // lblRole
            lblRole.AutoSize = true;
            lblRole.Location = new Point(100, 295);
            lblRole.Text = "Role:";

            // cmbRole
            cmbRole.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRole.Items.AddRange(new object[] { "Student", "SocietyHead" });
            cmbRole.Location = new Point(100, 315);
            cmbRole.Size = new Size(300, 23);
            cmbRole.SelectedIndex = 0;

            // btnRegister
            btnRegister.Location = new Point(100, 360);
            btnRegister.Size = new Size(300, 35);
            btnRegister.Text = "Register";
            btnRegister.UseVisualStyleBackColor = true;
            btnRegister.Click += BtnRegister_Click;

            // lnkLogin
            lnkLogin.AutoSize = true;
            lnkLogin.Location = new Point(170, 410);
            lnkLogin.Text = "Already have an account? Login";
            lnkLogin.LinkClicked += LnkLogin_LinkClicked;

            // RegisterForm
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(500, 460);
            Controls.Add(lblTitle);
            Controls.Add(lblFullName);
            Controls.Add(txtFullName);
            Controls.Add(lblEmail);
            Controls.Add(txtEmail);
            Controls.Add(lblPassword);
            Controls.Add(txtPassword);
            Controls.Add(lblConfirmPassword);
            Controls.Add(txtConfirmPassword);
            Controls.Add(lblRole);
            Controls.Add(cmbRole);
            Controls.Add(btnRegister);
            Controls.Add(lnkLogin);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "RegisterForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Register - Societies Management System";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private Label lblFullName;
        private Label lblEmail;
        private Label lblPassword;
        private Label lblConfirmPassword;
        private Label lblRole;
        private TextBox txtFullName;
        private TextBox txtEmail;
        private TextBox txtPassword;
        private TextBox txtConfirmPassword;
        private ComboBox cmbRole;
        private Button btnRegister;
        private LinkLabel lnkLogin;
    }
}
