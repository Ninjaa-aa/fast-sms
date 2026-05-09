using SMM_PROJ.DAL;
using SMM_PROJ.Helpers;

namespace SMM_PROJ.Forms.Student
{
    /// <summary>
    /// Displays all active societies and lets students apply for membership.
    /// </summary>
    public partial class BrowseSocieties : Form
    {
        public BrowseSocieties()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Loads active societies into the grid on form load.
        /// </summary>
        private void BrowseSocieties_Load(object? sender, EventArgs e)
        {
            LoadSocieties();
        }

        private void LoadSocieties()
        {
            try
            {
                dgvSocieties.DataSource = SocietyDAL.GetActive();
                if (dgvSocieties.Columns.Contains("SocietyID"))
                    dgvSocieties.Columns["SocietyID"]!.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading societies: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Applies for membership in the selected society.
        /// </summary>
        private void BtnApply_Click(object? sender, EventArgs e)
        {
            if (dgvSocieties.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a society.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int societyId = Convert.ToInt32(dgvSocieties.SelectedRows[0].Cells["SocietyID"].Value);

                if (MembershipDAL.HasApplied(Session.UserID, societyId))
                {
                    lblStatus.ForeColor = Color.Orange;
                    lblStatus.Text = "You have already applied for this society.";
                    return;
                }

                if (MembershipDAL.Apply(Session.UserID, societyId))
                {
                    lblStatus.ForeColor = Color.Green;
                    lblStatus.Text = "Membership request sent!";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnBack_Click(object? sender, EventArgs e)
        {
            new StudentDashboard().Show();
            this.Close();
        }
    }
}
