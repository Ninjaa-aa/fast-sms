using System.Data;
using SMM_PROJ.DAL;
using SMM_PROJ.Helpers;

namespace SMM_PROJ.Forms.Society
{
    /// <summary>
    /// Lets the society head assign tasks to approved members and mark tasks as completed.
    /// </summary>
    public partial class ManageTasks : Form
    {
        public ManageTasks()
        {
            InitializeComponent();
        }

        private void ManageTasks_Load(object? sender, EventArgs e)
        {
            LoadTasks();
        }

        private void LoadTasks()
        {
            try
            {
                if (Session.SocietyID == null) return;

                dgvTasks.DataSource = TaskDAL.GetBySociety(Session.SocietyID.Value);
                if (dgvTasks.Columns.Contains("TaskID"))
                    dgvTasks.Columns["TaskID"]!.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading tasks: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Opens a modal dialog to assign a new task to an approved member.
        /// </summary>
        private void BtnAssign_Click(object? sender, EventArgs e)
        {
            if (Session.SocietyID == null) return;

            using var dialog = new AssignTaskDialog(Session.SocietyID.Value);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    TaskDAL.Create(
                        Session.SocietyID.Value,
                        dialog.AssignedToUserId,
                        Session.UserID,
                        dialog.TaskTitle,
                        dialog.TaskDescription,
                        dialog.DueDate);

                    LoadTasks();
                    MessageBox.Show("Task assigned successfully.", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Marks the selected task as completed.
        /// </summary>
        private void BtnComplete_Click(object? sender, EventArgs e)
        {
            if (dgvTasks.SelectedRows.Count == 0) return;

            try
            {
                int taskId = Convert.ToInt32(dgvTasks.SelectedRows[0].Cells["TaskID"].Value);
                TaskDAL.MarkComplete(taskId);
                LoadTasks();
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

    /// <summary>
    /// Modal dialog for assigning a task to an approved society member.
    /// </summary>
    internal class AssignTaskDialog : Form
    {
        public int AssignedToUserId { get; private set; }
        public string TaskTitle { get; private set; } = string.Empty;
        public string TaskDescription { get; private set; } = string.Empty;
        public DateTime DueDate { get; private set; }

        private readonly ComboBox cmbMember;
        private readonly TextBox txtTitle;
        private readonly TextBox txtDescription;
        private readonly DateTimePicker dtpDueDate;

        public AssignTaskDialog(int societyId)
        {
            Text = "Assign New Task";
            ClientSize = new Size(400, 340);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            MaximizeBox = false;
            MinimizeBox = false;

            var lblMember = new Label { Text = "Assign To:", Location = new Point(20, 20), AutoSize = true };
            cmbMember = new ComboBox { Location = new Point(20, 40), Size = new Size(350, 23), DropDownStyle = ComboBoxStyle.DropDownList };

            var lblTitle = new Label { Text = "Title:", Location = new Point(20, 75), AutoSize = true };
            txtTitle = new TextBox { Location = new Point(20, 95), Size = new Size(350, 23) };

            var lblDesc = new Label { Text = "Description:", Location = new Point(20, 130), AutoSize = true };
            txtDescription = new TextBox { Location = new Point(20, 150), Size = new Size(350, 60), Multiline = true };

            var lblDue = new Label { Text = "Due Date:", Location = new Point(20, 220), AutoSize = true };
            dtpDueDate = new DateTimePicker { Location = new Point(20, 240), Size = new Size(350, 23), Format = DateTimePickerFormat.Short };

            var btnOk = new Button { Text = "Assign", Location = new Point(190, 285), Size = new Size(80, 30), DialogResult = DialogResult.OK };
            var btnDialogCancel = new Button { Text = "Cancel", Location = new Point(290, 285), Size = new Size(80, 30), DialogResult = DialogResult.Cancel };

            DataTable members = MembershipDAL.GetApprovedMembers(societyId);
            cmbMember.DisplayMember = "FullName";
            cmbMember.ValueMember = "UserID";
            cmbMember.DataSource = members;

            btnOk.Click += (s, e) =>
            {
                if (cmbMember.SelectedValue == null || string.IsNullOrWhiteSpace(txtTitle.Text))
                {
                    MessageBox.Show("Member and Title are required.", "Validation",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    DialogResult = DialogResult.None;
                    return;
                }
                AssignedToUserId = Convert.ToInt32(cmbMember.SelectedValue);
                TaskTitle = txtTitle.Text.Trim();
                TaskDescription = txtDescription.Text.Trim();
                DueDate = dtpDueDate.Value;
            };

            Controls.AddRange(new Control[] { lblMember, cmbMember, lblTitle, txtTitle, lblDesc, txtDescription, lblDue, dtpDueDate, btnOk, btnDialogCancel });
            AcceptButton = btnOk;
            CancelButton = btnDialogCancel;
        }
    }
}
