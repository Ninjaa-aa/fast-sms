namespace SMM_PROJ.Forms.Admin
{
    partial class ManageUsers
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
            txtSearch = new TextBox();
            btnSearch = new Button();
            dgvUsers = new DataGridView();
            btnDelete = new Button();
            btnBack = new Button();

            ((System.ComponentModel.ISupportInitialize)dgvUsers).BeginInit();
            SuspendLayout();

            // lblTitle
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.Location = new Point(20, 15);
            lblTitle.Text = "Manage Users";

            // txtSearch
            txtSearch.Location = new Point(440, 15);
            txtSearch.Size = new Size(220, 23);
            txtSearch.PlaceholderText = "Search by name or email...";

            // btnSearch
            btnSearch.Location = new Point(670, 13);
            btnSearch.Size = new Size(90, 27);
            btnSearch.Text = "Search";
            btnSearch.Click += BtnSearch_Click;

            // dgvUsers
            dgvUsers.AllowUserToAddRows = false;
            dgvUsers.AllowUserToDeleteRows = false;
            dgvUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvUsers.Location = new Point(20, 55);
            dgvUsers.MultiSelect = false;
            dgvUsers.ReadOnly = true;
            dgvUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsers.Size = new Size(740, 300);

            // btnDelete
            btnDelete.Location = new Point(20, 370);
            btnDelete.Size = new Size(120, 35);
            btnDelete.Text = "Delete User";
            btnDelete.Click += BtnDelete_Click;

            // btnBack
            btnBack.Location = new Point(660, 370);
            btnBack.Size = new Size(100, 35);
            btnBack.Text = "Back";
            btnBack.Click += BtnBack_Click;

            // ManageUsers
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(780, 420);
            Controls.Add(lblTitle);
            Controls.Add(txtSearch);
            Controls.Add(btnSearch);
            Controls.Add(dgvUsers);
            Controls.Add(btnDelete);
            Controls.Add(btnBack);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "ManageUsers";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Manage Users";
            Load += ManageUsers_Load;
            ((System.ComponentModel.ISupportInitialize)dgvUsers).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private TextBox txtSearch;
        private Button btnSearch;
        private DataGridView dgvUsers;
        private Button btnDelete;
        private Button btnBack;
    }
}
