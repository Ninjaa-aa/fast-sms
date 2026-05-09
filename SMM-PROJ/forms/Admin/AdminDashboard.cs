using SMM_PROJ.DAL;
using SMM_PROJ.Helpers;
using SMM_PROJ.Forms.Auth;

namespace SMM_PROJ.Forms.Admin
{
    /// <summary>
    /// Main hub for the Admin role. Shows summary stats and navigation to admin features.
    /// </summary>
    public partial class AdminDashboard : Form
    {
        public AdminDashboard()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Loads summary statistics on form load.
        /// </summary>
        private void AdminDashboard_Load(object? sender, EventArgs e)
        {
            try
            {
                lblTotalUsers.Text = $"Total Users: {UserDAL.GetCount()}";
                lblTotalSocieties.Text = $"Active Societies: {SocietyDAL.GetActiveCount()}";
                lblPendingEvents.Text = $"Pending Events: {EventDAL.GetPendingCount()}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading stats: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnManageUsers_Click(object? sender, EventArgs e)
        {
            new ManageUsers().Show();
            this.Hide();
        }

        private void BtnManageSocieties_Click(object? sender, EventArgs e)
        {
            new ManageSocieties().Show();
            this.Hide();
        }

        private void BtnApproveEvents_Click(object? sender, EventArgs e)
        {
            new ApproveEvents().Show();
            this.Hide();
        }

        private void BtnReports_Click(object? sender, EventArgs e)
        {
            new AdminReports().Show();
            this.Hide();
        }

        private void BtnLogout_Click(object? sender, EventArgs e)
        {
            Session.Clear();
            new LoginForm().Show();
            this.Close();
        }
    }
}
