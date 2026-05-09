namespace SMM_PROJ.Forms.Student
{
    partial class StudentDashboard
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
            panelNav = new Panel();
            btnBrowseSocieties = new Button();
            btnMyMemberships = new Button();
            btnBrowseEvents = new Button();
            btnMyTickets = new Button();
            btnLogout = new Button();

            panelNav.SuspendLayout();
            SuspendLayout();

            // lblWelcome
            lblWelcome.AutoSize = true;
            lblWelcome.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblWelcome.Location = new Point(200, 20);
            lblWelcome.Text = "Welcome, Student";

            // panelNav
            panelNav.BorderStyle = BorderStyle.FixedSingle;
            panelNav.Controls.Add(btnBrowseSocieties);
            panelNav.Controls.Add(btnMyMemberships);
            panelNav.Controls.Add(btnBrowseEvents);
            panelNav.Controls.Add(btnMyTickets);
            panelNav.Controls.Add(btnLogout);
            panelNav.Location = new Point(12, 70);
            panelNav.Size = new Size(170, 330);

            // btnBrowseSocieties
            btnBrowseSocieties.Location = new Point(10, 15);
            btnBrowseSocieties.Size = new Size(148, 40);
            btnBrowseSocieties.Text = "Browse Societies";
            btnBrowseSocieties.Click += BtnBrowseSocieties_Click;

            // btnMyMemberships
            btnMyMemberships.Location = new Point(10, 70);
            btnMyMemberships.Size = new Size(148, 40);
            btnMyMemberships.Text = "My Memberships";
            btnMyMemberships.Click += BtnMyMemberships_Click;

            // btnBrowseEvents
            btnBrowseEvents.Location = new Point(10, 125);
            btnBrowseEvents.Size = new Size(148, 40);
            btnBrowseEvents.Text = "Browse Events";
            btnBrowseEvents.Click += BtnBrowseEvents_Click;

            // btnMyTickets
            btnMyTickets.Location = new Point(10, 180);
            btnMyTickets.Size = new Size(148, 40);
            btnMyTickets.Text = "My Tickets";
            btnMyTickets.Click += BtnMyTickets_Click;

            // btnLogout
            btnLogout.Location = new Point(10, 275);
            btnLogout.Size = new Size(148, 40);
            btnLogout.Text = "Logout";
            btnLogout.Click += BtnLogout_Click;

            // StudentDashboard
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(lblWelcome);
            Controls.Add(panelNav);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "StudentDashboard";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Student Dashboard";
            panelNav.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblWelcome;
        private Panel panelNav;
        private Button btnBrowseSocieties;
        private Button btnMyMemberships;
        private Button btnBrowseEvents;
        private Button btnMyTickets;
        private Button btnLogout;
    }
}
