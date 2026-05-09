namespace SMM_PROJ.Forms.Student
{
    partial class MyMemberships
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
            dgvMemberships = new DataGridView();
            btnBack = new Button();

            ((System.ComponentModel.ISupportInitialize)dgvMemberships).BeginInit();
            SuspendLayout();

            // lblTitle
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.Location = new Point(20, 15);
            lblTitle.Text = "Your Memberships";

            // dgvMemberships
            dgvMemberships.AllowUserToAddRows = false;
            dgvMemberships.AllowUserToDeleteRows = false;
            dgvMemberships.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMemberships.Location = new Point(20, 55);
            dgvMemberships.ReadOnly = true;
            dgvMemberships.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMemberships.Size = new Size(740, 300);

            // btnBack
            btnBack.Location = new Point(660, 370);
            btnBack.Size = new Size(100, 35);
            btnBack.Text = "Back";
            btnBack.Click += BtnBack_Click;

            // MyMemberships
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(780, 420);
            Controls.Add(lblTitle);
            Controls.Add(dgvMemberships);
            Controls.Add(btnBack);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "MyMemberships";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "My Memberships";
            Load += MyMemberships_Load;
            ((System.ComponentModel.ISupportInitialize)dgvMemberships).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private DataGridView dgvMemberships;
        private Button btnBack;
    }
}
