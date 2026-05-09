using SMM_PROJ.DAL;
using SMM_PROJ.Helpers;

namespace SMM_PROJ.Forms.Student
{
    /// <summary>
    /// Displays upcoming approved events and lets students register.
    /// </summary>
    public partial class BrowseEvents : Form
    {
        public BrowseEvents()
        {
            InitializeComponent();
        }

        private void BrowseEvents_Load(object? sender, EventArgs e)
        {
            LoadEvents();
        }

        private void LoadEvents()
        {
            try
            {
                dgvEvents.DataSource = EventDAL.GetUpcomingApproved();
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
        /// Registers the student for the selected event.
        /// </summary>
        private void BtnRegister_Click(object? sender, EventArgs e)
        {
            if (dgvEvents.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an event.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int eventId = Convert.ToInt32(dgvEvents.SelectedRows[0].Cells["EventID"].Value);

                if (EventDAL.IsRegistered(eventId, Session.UserID))
                {
                    lblStatus.ForeColor = Color.Orange;
                    lblStatus.Text = "You are already registered for this event.";
                    return;
                }

                if (EventDAL.Register(eventId, Session.UserID))
                {
                    lblStatus.ForeColor = Color.Green;
                    lblStatus.Text = "Registered successfully! Your ticket has been saved.";
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
