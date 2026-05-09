namespace SMM_PROJ.Forms.Admin
{
    partial class ManageSocieties
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
            dgvSocieties = new DataGridView();
            btnCreate = new Button();
            btnApprove = new Button();
            btnSuspend = new Button();
            btnDelete = new Button();
            btnBack = new Button();

            ((System.ComponentModel.ISupportInitialize)dgvSocieties).BeginInit();
            SuspendLayout();

            // lblTitle
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.Location = new Point(20, 15);
            lblTitle.Text = "Manage Societies";

            // dgvSocieties
            dgvSocieties.AllowUserToAddRows = false;
            dgvSocieties.AllowUserToDeleteRows = false;
            dgvSocieties.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvSocieties.Location = new Point(20, 55);
            dgvSocieties.MultiSelect = false;
            dgvSocieties.ReadOnly = true;
            dgvSocieties.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSocieties.Size = new Size(740, 280);

            // btnCreate
            btnCreate.Location = new Point(20, 350);
            btnCreate.Size = new Size(130, 35);
            btnCreate.Text = "Create Society";
            btnCreate.Click += BtnCreate_Click;

            // btnApprove
            btnApprove.Location = new Point(165, 350);
            btnApprove.Size = new Size(100, 35);
            btnApprove.Text = "Approve";
            btnApprove.Click += BtnApprove_Click;

            // btnSuspend
            btnSuspend.Location = new Point(280, 350);
            btnSuspend.Size = new Size(100, 35);
            btnSuspend.Text = "Suspend";
            btnSuspend.Click += BtnSuspend_Click;

            // btnDelete
            btnDelete.Location = new Point(395, 350);
            btnDelete.Size = new Size(100, 35);
            btnDelete.Text = "Delete";
            btnDelete.Click += BtnDelete_Click;

            // btnBack
            btnBack.Location = new Point(660, 350);
            btnBack.Size = new Size(100, 35);
            btnBack.Text = "Back";
            btnBack.Click += BtnBack_Click;

            // ManageSocieties
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(780, 400);
            Controls.Add(lblTitle);
            Controls.Add(dgvSocieties);
            Controls.Add(btnCreate);
            Controls.Add(btnApprove);
            Controls.Add(btnSuspend);
            Controls.Add(btnDelete);
            Controls.Add(btnBack);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "ManageSocieties";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Manage Societies";
            Load += ManageSocieties_Load;
            ((System.ComponentModel.ISupportInitialize)dgvSocieties).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private DataGridView dgvSocieties;
        private Button btnCreate;
        private Button btnApprove;
        private Button btnSuspend;
        private Button btnDelete;
        private Button btnBack;
    }
}
