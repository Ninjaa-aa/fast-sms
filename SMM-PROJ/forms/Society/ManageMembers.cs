using SMM_PROJ.DAL;
using SMM_PROJ.Helpers;

namespace SMM_PROJ.Forms.Society
{
    /// <summary>
    /// Lets the society head approve or reject membership requests and view all members.
    /// </summary>
    public partial class ManageMembers : Form
    {
        public ManageMembers()
        {
            InitializeComponent();
        }

        private void ManageMembers_Load(object? sender, EventArgs e)
        {
            LoadMembers();
        }

        private void LoadMembers()
        {
            try
            {
                if (Session.SocietyID == null) return;

                string filter = cmbFilter.SelectedItem?.ToString() ?? "All";

                dgvMembers.DataSource = filter == "All"
                    ? MembershipDAL.GetBySociety(Session.SocietyID.Value)
                    : MembershipDAL.GetBySocietyFiltered(Session.SocietyID.Value, filter);

                if (dgvMembers.Columns.Contains("MembershipID"))
                    dgvMembers.Columns["MembershipID"]!.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading members: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CmbFilter_SelectedIndexChanged(object? sender, EventArgs e)
        {
            LoadMembers();
        }

        /// <summary>
        /// Approves the selected membership request.
        /// </summary>
        private void BtnApprove_Click(object? sender, EventArgs e)
        {
            if (dgvMembers.SelectedRows.Count == 0) return;

            try
            {
                int membershipId = Convert.ToInt32(dgvMembers.SelectedRows[0].Cells["MembershipID"].Value);
                MembershipDAL.Approve(membershipId);
                LoadMembers();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Rejects the selected membership request.
        /// </summary>
        private void BtnReject_Click(object? sender, EventArgs e)
        {
            if (dgvMembers.SelectedRows.Count == 0) return;

            try
            {
                int membershipId = Convert.ToInt32(dgvMembers.SelectedRows[0].Cells["MembershipID"].Value);
                MembershipDAL.Reject(membershipId);
                LoadMembers();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnBack_Click(object? sender, EventArgs e)
        {
            new SocietyDashboard().Show();
            this.Close();
        }
    }
}
