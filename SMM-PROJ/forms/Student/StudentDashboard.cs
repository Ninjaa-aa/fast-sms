using SMM_PROJ.Helpers;
using SMM_PROJ.Forms.Auth;

namespace SMM_PROJ.Forms.Student
{
    /// <summary>
    /// Main hub for logged-in students. Provides navigation to all student features.
    /// </summary>
    public partial class StudentDashboard : Form
    {
        public StudentDashboard()
        {
            InitializeComponent();
            lblWelcome.Text = $"Welcome, {Session.FullName}";
        }

        private void BtnBrowseSocieties_Click(object? sender, EventArgs e)
        {
            new BrowseSocieties().Show();
            this.Hide();
        }

        private void BtnMyMemberships_Click(object? sender, EventArgs e)
        {
            new MyMemberships().Show();
            this.Hide();
        }

        private void BtnBrowseEvents_Click(object? sender, EventArgs e)
        {
            new BrowseEvents().Show();
            this.Hide();
        }

        private void BtnMyTickets_Click(object? sender, EventArgs e)
        {
            new MyTickets().Show();
            this.Hide();
        }

        /// <summary>
        /// Clears the session and returns to the login screen.
        /// </summary>
        private void BtnLogout_Click(object? sender, EventArgs e)
        {
            Session.Clear();
            new LoginForm().Show();
            this.Close();
        }
    }
}
