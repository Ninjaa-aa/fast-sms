using System.Data;
using SMM_PROJ.DAL;
using SMM_PROJ.Helpers;
using SMM_PROJ.Forms.Auth;

namespace SMM_PROJ.Forms.Society
{
    /// <summary>
    /// Main hub for Society Heads. Resolves the head's society on load
    /// and provides navigation to member, event, task, and report management.
    /// </summary>
    public partial class SocietyDashboard : Form
    {
        public SocietyDashboard()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Fetches the society managed by this head and updates the UI accordingly.
        /// </summary>
        private void SocietyDashboard_Load(object? sender, EventArgs e)
        {
            try
            {
                DataTable dt = SocietyDAL.GetByHead(Session.UserID);

                DataRow? activeRow = null;
                foreach (DataRow row in dt.Rows)
                {
                    if (row["Status"].ToString() == "Active")
                    {
                        activeRow = row;
                        break;
                    }
                }

                if (activeRow != null)
                {
                    Session.SocietyID = Convert.ToInt32(activeRow["SocietyID"]);
                    string societyName = activeRow["Name"].ToString()!;
                    lblWelcome.Text = $"Welcome, {Session.FullName} — {societyName}";
                    EnableNavButtons(true);
                }
                else
                {
                    lblWelcome.Text = $"Welcome, {Session.FullName}";
                    lblSocietyStatus.Text = "Your society is pending admin approval.";
                    EnableNavButtons(false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading society: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EnableNavButtons(bool enabled)
        {
            btnManageMembers.Enabled = enabled;
            btnManageEvents.Enabled = enabled;
            btnManageTasks.Enabled = enabled;
            btnReports.Enabled = enabled;
        }

        private void BtnManageMembers_Click(object? sender, EventArgs e)
        {
            new ManageMembers().Show();
            this.Hide();
        }

        private void BtnManageEvents_Click(object? sender, EventArgs e)
        {
            new ManageEvents().Show();
            this.Hide();
        }

        private void BtnManageTasks_Click(object? sender, EventArgs e)
        {
            new ManageTasks().Show();
            this.Hide();
        }

        private void BtnReports_Click(object? sender, EventArgs e)
        {
            new SocietyReports().Show();
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
