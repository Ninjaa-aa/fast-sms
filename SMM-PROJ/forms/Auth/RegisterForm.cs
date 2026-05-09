using SMM_PROJ.DAL;
using SMM_PROJ.Helpers;
using SMM_PROJ.Models;

namespace SMM_PROJ.Forms.Auth
{
    /// <summary>
    /// Allows new users to register as Student or SocietyHead.
    /// Admin accounts cannot be self-registered.
    /// </summary>
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Validates input, hashes the password, and inserts the new user.
        /// </summary>
        private void BtnRegister_Click(object? sender, EventArgs e)
        {
            string fullName = txtFullName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;
            string role = cmbRole.SelectedItem?.ToString() ?? "Student";

            if (string.IsNullOrWhiteSpace(fullName) || string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("All fields are required.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Passwords do not match.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (UserDAL.EmailExists(email))
                {
                    MessageBox.Show("An account with this email already exists.", "Duplicate",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var user = new User
                {
                    FullName = fullName,
                    Email = email,
                    PasswordHash = PasswordHasher.Hash(password),
                    Role = role
                };

                if (UserDAL.Register(user))
                {
                    MessageBox.Show("Registration successful! You can now log in.", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    NavigateToLogin();
                }
                else
                {
                    MessageBox.Show("Registration failed. Please try again.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Navigates back to the login form.
        /// </summary>
        private void LnkLogin_LinkClicked(object? sender, LinkLabelLinkClickedEventArgs e)
        {
            NavigateToLogin();
        }

        private void NavigateToLogin()
        {
            var loginForm = new LoginForm();
            loginForm.Show();
            this.Close();
        }
    }
}
