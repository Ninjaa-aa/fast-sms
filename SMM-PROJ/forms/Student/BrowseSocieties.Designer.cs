namespace SMM_PROJ.Forms.Student
{
    partial class BrowseSocieties
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
            btnApply = new Button();
            lblStatus = new Label();
            btnBack = new Button();

            ((System.ComponentModel.ISupportInitialize)dgvSocieties).BeginInit();
            SuspendLayout();

            // lblTitle
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.Location = new Point(20, 15);
            lblTitle.Text = "Browse Active Societies";

            // dgvSocieties
            dgvSocieties.AllowUserToAddRows = false;
            dgvSocieties.AllowUserToDeleteRows = false;
            dgvSocieties.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvSocieties.Location = new Point(20, 55);
            dgvSocieties.MultiSelect = false;
            dgvSocieties.ReadOnly = true;
            dgvSocieties.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSocieties.Size = new Size(740, 280);

            // btnApply
            btnApply.Location = new Point(20, 350);
            btnApply.Size = new Size(180, 35);
            btnApply.Text = "Apply for Membership";
            btnApply.Click += BtnApply_Click;

            // lblStatus
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(220, 358);
            lblStatus.ForeColor = Color.Green;
            lblStatus.Text = "";

            // btnBack
            btnBack.Location = new Point(660, 350);
            btnBack.Size = new Size(100, 35);
            btnBack.Text = "Back";
            btnBack.Click += BtnBack_Click;

            // BrowseSocieties
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(780, 400);
            Controls.Add(lblTitle);
            Controls.Add(dgvSocieties);
            Controls.Add(btnApply);
            Controls.Add(lblStatus);
            Controls.Add(btnBack);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "BrowseSocieties";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Browse Societies";
            Load += BrowseSocieties_Load;
            ((System.ComponentModel.ISupportInitialize)dgvSocieties).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private DataGridView dgvSocieties;
        private Button btnApply;
        private Label lblStatus;
        private Button btnBack;
    }
}
