using SMM_PROJ.DAL;
using SMM_PROJ.Helpers;

namespace SMM_PROJ.Forms.Student
{
    /// <summary>
    /// Displays the student's registered event tickets/passes.
    /// </summary>
    public partial class MyTickets : Form
    {
        public MyTickets()
        {
            InitializeComponent();
        }

        private void MyTickets_Load(object? sender, EventArgs e)
        {
            try
            {
                dgvTickets.DataSource = EventDAL.GetTicketsByUser(Session.UserID);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading tickets: {ex.Message}", "Error",
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
