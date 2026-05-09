using System.Data;
using SMM_PROJ.DAL;
using SMM_PROJ.Helpers;

namespace SMM_PROJ.Forms.Society
{
    /// <summary>
    /// Displays member and event reports for the society head's society.
    /// </summary>
    public partial class SocietyReports : Form
    {
        public SocietyReports()
        {
            InitializeComponent();
        }

        private void SocietyReports_Load(object? sender, EventArgs e)
        {
            LoadReports();
        }

        private void LoadReports()
        {
            try
            {
                if (Session.SocietyID == null) return;

                DataTable members = MembershipDAL.GetApprovedMembers(Session.SocietyID.Value);
                dgvMembers.DataSource = members;
                if (dgvMembers.Columns.Contains("UserID"))
                    dgvMembers.Columns["UserID"]!.Visible = false;
                lblMemberCount.Text = $"Total Members: {members.Rows.Count}";

                DataTable events = EventDAL.GetBySociety(Session.SocietyID.Value);
                dgvEvents.DataSource = events;
                if (dgvEvents.Columns.Contains("EventID"))
                    dgvEvents.Columns["EventID"]!.Visible = false;
                lblEventCount.Text = $"Total Events: {events.Rows.Count}";
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
            new SocietyDashboard().Show();
            this.Close();
        }
    }
}
