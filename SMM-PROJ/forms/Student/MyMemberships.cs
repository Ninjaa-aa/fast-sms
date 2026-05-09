using SMM_PROJ.DAL;
using SMM_PROJ.Helpers;

namespace SMM_PROJ.Forms.Student
{
    /// <summary>
    /// Shows the student's society membership statuses.
    /// </summary>
    public partial class MyMemberships : Form
    {
        public MyMemberships()
        {
            InitializeComponent();
        }

        private void MyMemberships_Load(object? sender, EventArgs e)
        {
            try
            {
                dgvMemberships.DataSource = MembershipDAL.GetByUser(Session.UserID);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading memberships: {ex.Message}", "Error",
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
