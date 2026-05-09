namespace SMM_PROJ.Forms.Admin
{
    partial class AdminReports
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
            tabControl = new TabControl();
            tabMembers = new TabPage();
            dgvMembers = new DataGridView();
            tabEvents = new TabPage();
            dgvEvents = new DataGridView();
            tabPerformance = new TabPage();
            dgvPerformance = new DataGridView();
            btnRefresh = new Button();
            btnBack = new Button();

            tabControl.SuspendLayout();
            tabMembers.SuspendLayout();
            tabEvents.SuspendLayout();
            tabPerformance.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMembers).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvEvents).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvPerformance).BeginInit();
            SuspendLayout();

            // lblTitle
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.Location = new Point(20, 10);
            lblTitle.Text = "University Reports";

            // tabControl
            tabControl.Controls.Add(tabMembers);
            tabControl.Controls.Add(tabEvents);
            tabControl.Controls.Add(tabPerformance);
            tabControl.Location = new Point(20, 45);
            tabControl.Size = new Size(740, 340);

            // tabMembers
            tabMembers.Controls.Add(dgvMembers);
            tabMembers.Text = "All Members";
            tabMembers.Padding = new Padding(3);

            // dgvMembers
            dgvMembers.AllowUserToAddRows = false;
            dgvMembers.AllowUserToDeleteRows = false;
            dgvMembers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMembers.Dock = DockStyle.Fill;
            dgvMembers.ReadOnly = true;
            dgvMembers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // tabEvents
            tabEvents.Controls.Add(dgvEvents);
            tabEvents.Text = "All Events";
            tabEvents.Padding = new Padding(3);

            // dgvEvents
            dgvEvents.AllowUserToAddRows = false;
            dgvEvents.AllowUserToDeleteRows = false;
            dgvEvents.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvEvents.Dock = DockStyle.Fill;
            dgvEvents.ReadOnly = true;
            dgvEvents.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // tabPerformance
            tabPerformance.Controls.Add(dgvPerformance);
            tabPerformance.Text = "Society Performance";
            tabPerformance.Padding = new Padding(3);

            // dgvPerformance
            dgvPerformance.AllowUserToAddRows = false;
            dgvPerformance.AllowUserToDeleteRows = false;
            dgvPerformance.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPerformance.Dock = DockStyle.Fill;
            dgvPerformance.ReadOnly = true;
            dgvPerformance.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // btnRefresh
            btnRefresh.Location = new Point(20, 395);
            btnRefresh.Size = new Size(100, 35);
            btnRefresh.Text = "Refresh";
            btnRefresh.Click += BtnRefresh_Click;

            // btnBack
            btnBack.Location = new Point(660, 395);
            btnBack.Size = new Size(100, 35);
            btnBack.Text = "Back";
            btnBack.Click += BtnBack_Click;

            // AdminReports
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(780, 445);
            Controls.Add(lblTitle);
            Controls.Add(tabControl);
            Controls.Add(btnRefresh);
            Controls.Add(btnBack);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "AdminReports";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "University Reports";
            Load += AdminReports_Load;
            tabControl.ResumeLayout(false);
            tabMembers.ResumeLayout(false);
            tabEvents.ResumeLayout(false);
            tabPerformance.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvMembers).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvEvents).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvPerformance).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private TabControl tabControl;
        private TabPage tabMembers;
        private DataGridView dgvMembers;
        private TabPage tabEvents;
        private DataGridView dgvEvents;
        private TabPage tabPerformance;
        private DataGridView dgvPerformance;
        private Button btnRefresh;
        private Button btnBack;
    }
}
