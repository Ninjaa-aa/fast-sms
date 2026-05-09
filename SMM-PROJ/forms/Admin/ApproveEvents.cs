using SMM_PROJ.DAL;

namespace SMM_PROJ.Forms.Admin
{
    /// <summary>
    /// Admin form for approving or rejecting pending event requests from societies.
    /// </summary>
    public partial class ApproveEvents : Form
    {
        public ApproveEvents()
        {
            InitializeComponent();
        }

        private void ApproveEvents_Load(object? sender, EventArgs e)
        {
            LoadPendingEvents();
        }

        private void LoadPendingEvents()
        {
            try
            {
                dgvEvents.DataSource = EventDAL.GetPending();
                if (dgvEvents.Columns.Contains("EventID"))
                    dgvEvents.Columns["EventID"]!.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading events: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Approves the selected event.
        /// </summary>
        private void BtnApprove_Click(object? sender, EventArgs e)
        {
            if (dgvEvents.SelectedRows.Count == 0) return;

            try
            {
                int eventId = Convert.ToInt32(dgvEvents.SelectedRows[0].Cells["EventID"].Value);
                EventDAL.Approve(eventId);
                LoadPendingEvents();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Rejects the selected event (sets status to Cancelled).
        /// </summary>
        private void BtnReject_Click(object? sender, EventArgs e)
        {
            if (dgvEvents.SelectedRows.Count == 0) return;

            try
            {
                int eventId = Convert.ToInt32(dgvEvents.SelectedRows[0].Cells["EventID"].Value);
                EventDAL.Reject(eventId);
                LoadPendingEvents();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnBack_Click(object? sender, EventArgs e)
        {
            new AdminDashboard().Show();
            this.Close();
        }
    }
}
