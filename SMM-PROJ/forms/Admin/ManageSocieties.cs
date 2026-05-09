using System.Data;
using SMM_PROJ.DAL;

namespace SMM_PROJ.Forms.Admin
{
    /// <summary>
    /// Admin form for creating, approving, suspending, and deleting societies.
    /// </summary>
    public partial class ManageSocieties : Form
    {
        public ManageSocieties()
        {
            InitializeComponent();
        }

        private void ManageSocieties_Load(object? sender, EventArgs e)
        {
            LoadSocieties();
        }

        private void LoadSocieties()
        {
            try
            {
                dgvSocieties.DataSource = SocietyDAL.GetAll();
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
        /// Opens a modal dialog to create a new society.
        /// </summary>
        private void BtnCreate_Click(object? sender, EventArgs e)
        {
            using var dialog = new CreateSocietyDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    SocietyDAL.Create(dialog.SocietyName, dialog.SocietyDescription, dialog.HeadUserId);
                    LoadSocieties();
                    MessageBox.Show("Society created (pending approval).", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnApprove_Click(object? sender, EventArgs e)
        {
            if (dgvSocieties.SelectedRows.Count == 0) return;
            try
            {
                int id = Convert.ToInt32(dgvSocieties.SelectedRows[0].Cells["SocietyID"].Value);
                SocietyDAL.Approve(id);
                LoadSocieties();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSuspend_Click(object? sender, EventArgs e)
        {
            if (dgvSocieties.SelectedRows.Count == 0) return;
            try
            {
                int id = Convert.ToInt32(dgvSocieties.SelectedRows[0].Cells["SocietyID"].Value);
                SocietyDAL.Suspend(id);
                LoadSocieties();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDelete_Click(object? sender, EventArgs e)
        {
            if (dgvSocieties.SelectedRows.Count == 0) return;

            var result = MessageBox.Show("Are you sure you want to delete this society?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes) return;

            try
            {
                int id = Convert.ToInt32(dgvSocieties.SelectedRows[0].Cells["SocietyID"].Value);
                SocietyDAL.Delete(id);
                LoadSocieties();
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

    /// <summary>
    /// Modal dialog for creating a new society. Head is selected from SocietyHead users.
    /// </summary>
    internal class CreateSocietyDialog : Form
    {
        public string SocietyName { get; private set; } = string.Empty;
        public string SocietyDescription { get; private set; } = string.Empty;
        public int HeadUserId { get; private set; }

        private readonly TextBox txtName;
        private readonly TextBox txtDescription;
        private readonly ComboBox cmbHead;

        public CreateSocietyDialog()
        {
            Text = "Create New Society";
            ClientSize = new Size(400, 280);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            MaximizeBox = false;
            MinimizeBox = false;

            var lblName = new Label { Text = "Name:", Location = new Point(20, 20), AutoSize = true };
            txtName = new TextBox { Location = new Point(20, 40), Size = new Size(350, 23) };

            var lblDesc = new Label { Text = "Description:", Location = new Point(20, 75), AutoSize = true };
            txtDescription = new TextBox { Location = new Point(20, 95), Size = new Size(350, 60), Multiline = true };

            var lblHead = new Label { Text = "Head (SocietyHead user):", Location = new Point(20, 165), AutoSize = true };
            cmbHead = new ComboBox { Location = new Point(20, 185), Size = new Size(350, 23), DropDownStyle = ComboBoxStyle.DropDownList };

            DataTable heads = UserDAL.GetSocietyHeads();
            cmbHead.DisplayMember = "FullName";
            cmbHead.ValueMember = "UserID";
            cmbHead.DataSource = heads;

            var btnOk = new Button { Text = "Create", Location = new Point(190, 230), Size = new Size(80, 30), DialogResult = DialogResult.OK };
            var btnDialogCancel = new Button { Text = "Cancel", Location = new Point(290, 230), Size = new Size(80, 30), DialogResult = DialogResult.Cancel };

            btnOk.Click += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtName.Text) || cmbHead.SelectedValue == null)
                {
                    MessageBox.Show("Name and Head are required.", "Validation",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    DialogResult = DialogResult.None;
                    return;
                }
                SocietyName = txtName.Text.Trim();
                SocietyDescription = txtDescription.Text.Trim();
                HeadUserId = Convert.ToInt32(cmbHead.SelectedValue);
            };

            Controls.AddRange(new Control[] { lblName, txtName, lblDesc, txtDescription, lblHead, cmbHead, btnOk, btnDialogCancel });
            AcceptButton = btnOk;
            CancelButton = btnDialogCancel;
        }
    }
}
