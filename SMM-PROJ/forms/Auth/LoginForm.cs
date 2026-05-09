using SMM_PROJ.DAL;
using SMM_PROJ.Helpers;
using SMM_PROJ.Forms.Student;
using SMM_PROJ.Forms.Society;
using SMM_PROJ.Forms.Admin;

namespace SMM_PROJ.Forms.Auth
{
    /// <summary>
    /// Entry-point form. Authenticates the user and redirects to the role-specific dashboard.
    /// </summary>
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Validates credentials against the database and opens the appropriate dashboard.
        /// </summary>
        private void BtnLogin_Click(object? sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter both email and password.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var user = UserDAL.GetByEmail(email);

                if (user == null || !PasswordHasher.Verify(password, user.PasswordHash))
                {
                    MessageBox.Show("Invalid email or password.", "Login Failed",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Session.UserID = user.UserID;
                Session.FullName = user.FullName;
                Session.Email = user.Email;
                Session.Role = user.Role;

                Form dashboard = user.Role switch
                {
                    "Student" => new StudentDashboard(),
                    "SocietyHead" => new SocietyDashboard(),
                    "Admin" => new AdminDashboard(),
                    _ => throw new InvalidOperationException($"Unknown role: {user.Role}")
                };

                dashboard.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Navigates to the registration form.
        /// </summary>
        private void LnkRegister_LinkClicked(object? sender, LinkLabelLinkClickedEventArgs e)
        {
            var registerForm = new RegisterForm();
            registerForm.Show();
            this.Hide();
        }
    }
}
