namespace SMM_PROJ.Forms.Society
{
    partial class SocietyDashboard
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
            lblWelcome = new Label();
            lblSocietyStatus = new Label();
            panelNav = new Panel();
            btnManageMembers = new Button();
            btnManageEvents = new Button();
            btnManageTasks = new Button();
            btnReports = new Button();
            btnLogout = new Button();

            panelNav.SuspendLayout();
            SuspendLayout();

            // lblWelcome
            lblWelcome.AutoSize = true;
            lblWelcome.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblWelcome.Location = new Point(200, 15);
            lblWelcome.Text = "Welcome, Society Head";

            // lblSocietyStatus
            lblSocietyStatus.AutoSize = true;
            lblSocietyStatus.ForeColor = Color.Gray;
            lblSocietyStatus.Location = new Point(200, 50);
            lblSocietyStatus.Text = "";

            // panelNav
            panelNav.BorderStyle = BorderStyle.FixedSingle;
            panelNav.Controls.Add(btnManageMembers);
            panelNav.Controls.Add(btnManageEvents);
            panelNav.Controls.Add(btnManageTasks);
            panelNav.Controls.Add(btnReports);
            panelNav.Controls.Add(btnLogout);
            panelNav.Location = new Point(12, 80);
            panelNav.Size = new Size(170, 340);

            // btnManageMembers
            btnManageMembers.Location = new Point(10, 15);
            btnManageMembers.Size = new Size(148, 40);
            btnManageMembers.Text = "Manage Members";
            btnManageMembers.Click += BtnManageMembers_Click;

            // btnManageEvents
            btnManageEvents.Location = new Point(10, 70);
            btnManageEvents.Size = new Size(148, 40);
            btnManageEvents.Text = "Manage Events";
            btnManageEvents.Click += BtnManageEvents_Click;

            // btnManageTasks
            btnManageTasks.Location = new Point(10, 125);
            btnManageTasks.Size = new Size(148, 40);
            btnManageTasks.Text = "Manage Tasks";
            btnManageTasks.Click += BtnManageTasks_Click;

            // btnReports
            btnReports.Location = new Point(10, 180);
            btnReports.Size = new Size(148, 40);
            btnReports.Text = "Reports";
            btnReports.Click += BtnReports_Click;

            // btnLogout
            btnLogout.Location = new Point(10, 285);
            btnLogout.Size = new Size(148, 40);
            btnLogout.Text = "Logout";
            btnLogout.Click += BtnLogout_Click;

            // SocietyDashboard
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(lblWelcome);
            Controls.Add(lblSocietyStatus);
            Controls.Add(panelNav);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "SocietyDashboard";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Society Dashboard";
            Load += SocietyDashboard_Load;
            panelNav.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblWelcome;
        private Label lblSocietyStatus;
        private Panel panelNav;
        private Button btnManageMembers;
        private Button btnManageEvents;
        private Button btnManageTasks;
        private Button btnReports;
        private Button btnLogout;
    }
}
