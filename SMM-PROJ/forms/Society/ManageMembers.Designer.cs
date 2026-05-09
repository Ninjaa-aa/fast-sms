namespace SMM_PROJ.Forms.Society
{
    partial class ManageMembers
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            lblTitle = new Label();
            cmbFilter = new ComboBox();
            dgvMembers = new DataGridView();
            btnApprove = new Button();
            btnReject = new Button();
            btnBack = new Button();

            ((System.ComponentModel.ISupportInitialize)dgvMembers).BeginInit();
            SuspendLayout();

            // lblTitle
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.Location = new Point(20, 15);
            lblTitle.Text = "Manage Members";

            // cmbFilter
            cmbFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFilter.Items.AddRange(new object[] { "All", "Pending", "Approved", "Rejected" });
            cmbFilter.Location = new Point(620, 15);
            cmbFilter.Size = new Size(140, 23);
            cmbFilter.SelectedIndex = 0;
            cmbFilter.SelectedIndexChanged += CmbFilter_SelectedIndexChanged;

            // dgvMembers
            dgvMembers.AllowUserToAddRows = false;
            dgvMembers.AllowUserToDeleteRows = false;
            dgvMembers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMembers.Location = new Point(20, 55);
            dgvMembers.MultiSelect = false;
            dgvMembers.ReadOnly = true;
            dgvMembers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMembers.Size = new Size(740, 300);

            // btnApprove
            btnApprove.Location = new Point(20, 370);
            btnApprove.Size = new Size(120, 35);
            btnApprove.Text = "Approve";
            btnApprove.Click += BtnApprove_Click;

            // btnReject
            btnReject.Location = new Point(160, 370);
            btnReject.Size = new Size(120, 35);
            btnReject.Text = "Reject";
            btnReject.Click += BtnReject_Click;

            // btnBack
            btnBack.Location = new Point(660, 370);
            btnBack.Size = new Size(100, 35);
            btnBack.Text = "Back";
            btnBack.Click += BtnBack_Click;

            // ManageMembers
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(780, 420);
            Controls.Add(lblTitle);
            Controls.Add(cmbFilter);
            Controls.Add(dgvMembers);
            Controls.Add(btnApprove);
            Controls.Add(btnReject);
            Controls.Add(btnBack);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "ManageMembers";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Manage Members";
            Load += ManageMembers_Load;
            ((System.ComponentModel.ISupportInitialize)dgvMembers).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private ComboBox cmbFilter;
        private DataGridView dgvMembers;
        private Button btnApprove;
        private Button btnReject;
        private Button btnBack;
    }
}
