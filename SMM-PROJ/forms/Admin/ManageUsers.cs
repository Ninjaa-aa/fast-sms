using SMM_PROJ.DAL;

namespace SMM_PROJ.Forms.Admin
{
    /// <summary>
    /// Admin form for viewing, searching, and deleting non-admin user accounts.
    /// </summary>
    public partial class ManageUsers : Form
    {
        public ManageUsers()
        {
            InitializeComponent();
        }

        private void ManageUsers_Load(object? sender, EventArgs e)
        {
            LoadUsers();
        }

        private void LoadUsers()
        {
            try
            {
                dgvUsers.DataSource = UserDAL.GetAll();
                if (dgvUsers.Columns.Contains("UserID"))
                    dgvUsers.Columns["UserID"]!.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading users: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Filters users by name or email.
        /// </summary>
        private void BtnSearch_Click(object? sender, EventArgs e)
        {
            try
            {
                string term = txtSearch.Text.Trim();
                dgvUsers.DataSource = string.IsNullOrWhiteSpace(term)
                    ? UserDAL.GetAll()
                    : UserDAL.Search(term);

                if (dgvUsers.Columns.Contains("UserID"))
                    dgvUsers.Columns["UserID"]!.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Deletes the selected user after confirmation.
        /// </summary>
        private void BtnDelete_Click(object? sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count == 0) return;

            var result = MessageBox.Show("Are you sure you want to delete this user?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result != DialogResult.Yes) return;

            try
            {
                int userId = Convert.ToInt32(dgvUsers.SelectedRows[0].Cells["UserID"].Value);
                UserDAL.Delete(userId);
                LoadUsers();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting user: {ex.Message}", "Error",
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
