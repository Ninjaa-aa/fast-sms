using SMM_PROJ.DAL;

namespace SMM_PROJ.Forms.Admin
{
    /// <summary>
    /// University-wide reporting: all members, all events, and society performance summary.
    /// </summary>
    public partial class AdminReports : Form
    {
        public AdminReports()
        {
            InitializeComponent();
        }

        private void AdminReports_Load(object? sender, EventArgs e)
        {
            LoadReports();
        }

        private void LoadReports()
        {
            try
            {
                dgvMembers.DataSource = MembershipDAL.GetAll();
                dgvEvents.DataSource = EventDAL.GetAll();
                dgvPerformance.DataSource = SocietyDAL.GetPerformanceSummary();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading reports: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnRefresh_Click(object? sender, EventArgs e)
        {
            LoadReports();
        }

        private void BtnBack_Click(object? sender, EventArgs e)
        {
            new AdminDashboard().Show();
            this.Close();
        }
    }
}
