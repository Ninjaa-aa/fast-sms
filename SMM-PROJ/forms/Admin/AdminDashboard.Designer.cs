namespace SMM_PROJ.Forms.Admin
{
    partial class AdminDashboard
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
            panelStats = new Panel();
            lblTotalUsers = new Label();
            lblTotalSocieties = new Label();
            lblPendingEvents = new Label();
            panelNav = new Panel();
            btnManageUsers = new Button();
            btnManageSocieties = new Button();
            btnApproveEvents = new Button();
            btnReports = new Button();
            btnLogout = new Button();

            panelStats.SuspendLayout();
            panelNav.SuspendLayout();
            SuspendLayout();

            // lblTitle
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitle.Location = new Point(200, 15);
            lblTitle.Text = "Admin Panel";

            // panelStats
            panelStats.BorderStyle = BorderStyle.FixedSingle;
            panelStats.Controls.Add(lblTotalUsers);
            panelStats.Controls.Add(lblTotalSocieties);
            panelStats.Controls.Add(lblPendingEvents);
            panelStats.Location = new Point(200, 60);
            panelStats.Size = new Size(570, 50);

            // lblTotalUsers
            lblTotalUsers.AutoSize = true;
            lblTotalUsers.Font = new Font("Segoe UI", 10F);
            lblTotalUsers.Location = new Point(15, 15);
            lblTotalUsers.Text = "Total Users: 0";

            // lblTotalSocieties
            lblTotalSocieties.AutoSize = true;
            lblTotalSocieties.Font = new Font("Segoe UI", 10F);
            lblTotalSocieties.Location = new Point(200, 15);
            lblTotalSocieties.Text = "Active Societies: 0";

            // lblPendingEvents
            lblPendingEvents.AutoSize = true;
            lblPendingEvents.Font = new Font("Segoe UI", 10F);
            lblPendingEvents.Location = new Point(400, 15);
            lblPendingEvents.Text = "Pending Events: 0";

            // panelNav
            panelNav.BorderStyle = BorderStyle.FixedSingle;
            panelNav.Controls.Add(btnManageUsers);
            panelNav.Controls.Add(btnManageSocieties);
            panelNav.Controls.Add(btnApproveEvents);
            panelNav.Controls.Add(btnReports);
            panelNav.Controls.Add(btnLogout);
            panelNav.Location = new Point(12, 60);
            panelNav.Size = new Size(170, 360);

            // btnManageUsers
            btnManageUsers.Location = new Point(10, 15);
            btnManageUsers.Size = new Size(148, 40);
            btnManageUsers.Text = "Manage Users";
            btnManageUsers.Click += BtnManageUsers_Click;

            // btnManageSocieties
            btnManageSocieties.Location = new Point(10, 70);
            btnManageSocieties.Size = new Size(148, 40);
            btnManageSocieties.Text = "Manage Societies";
            btnManageSocieties.Click += BtnManageSocieties_Click;

            // btnApproveEvents
            btnApproveEvents.Location = new Point(10, 125);
            btnApproveEvents.Size = new Size(148, 40);
            btnApproveEvents.Text = "Approve Events";
            btnApproveEvents.Click += BtnApproveEvents_Click;

            // btnReports
            btnReports.Location = new Point(10, 180);
            btnReports.Size = new Size(148, 40);
            btnReports.Text = "University Reports";
            btnReports.Click += BtnReports_Click;

            // btnLogout
            btnLogout.Location = new Point(10, 305);
            btnLogout.Size = new Size(148, 40);
            btnLogout.Text = "Logout";
            btnLogout.Click += BtnLogout_Click;

            // AdminDashboard
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(lblTitle);
            Controls.Add(panelStats);
            Controls.Add(panelNav);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "AdminDashboard";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Admin Dashboard";
            Load += AdminDashboard_Load;
            panelStats.ResumeLayout(false);
            panelStats.PerformLayout();
            panelNav.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private Panel panelStats;
        private Label lblTotalUsers;
        private Label lblTotalSocieties;
        private Label lblPendingEvents;
        private Panel panelNav;
        private Button btnManageUsers;
        private Button btnManageSocieties;
        private Button btnApproveEvents;
        private Button btnReports;
        private Button btnLogout;
    }
}
