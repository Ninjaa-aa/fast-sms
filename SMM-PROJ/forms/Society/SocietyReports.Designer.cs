namespace SMM_PROJ.Forms.Society
{
    partial class SocietyReports
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
            grpMembers = new GroupBox();
            dgvMembers = new DataGridView();
            lblMemberCount = new Label();
            grpEvents = new GroupBox();
            dgvEvents = new DataGridView();
            lblEventCount = new Label();
            btnRefresh = new Button();
            btnBack = new Button();

            grpMembers.SuspendLayout();
            grpEvents.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMembers).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvEvents).BeginInit();
            SuspendLayout();

            // lblTitle
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.Location = new Point(20, 10);
            lblTitle.Text = "Society Reports";

            // grpMembers
            grpMembers.Controls.Add(dgvMembers);
            grpMembers.Controls.Add(lblMemberCount);
            grpMembers.Location = new Point(20, 45);
            grpMembers.Size = new Size(370, 330);
            grpMembers.Text = "Member Report";

            // dgvMembers
            dgvMembers.AllowUserToAddRows = false;
            dgvMembers.AllowUserToDeleteRows = false;
            dgvMembers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMembers.Location = new Point(10, 25);
            dgvMembers.ReadOnly = true;
            dgvMembers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMembers.Size = new Size(350, 270);

            // lblMemberCount
            lblMemberCount.AutoSize = true;
            lblMemberCount.Location = new Point(10, 302);
            lblMemberCount.Text = "Total Members: 0";

            // grpEvents
            grpEvents.Controls.Add(dgvEvents);
            grpEvents.Controls.Add(lblEventCount);
            grpEvents.Location = new Point(400, 45);
            grpEvents.Size = new Size(370, 330);
            grpEvents.Text = "Event Report";

            // dgvEvents
            dgvEvents.AllowUserToAddRows = false;
            dgvEvents.AllowUserToDeleteRows = false;
            dgvEvents.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvEvents.Location = new Point(10, 25);
            dgvEvents.ReadOnly = true;
            dgvEvents.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvEvents.Size = new Size(350, 270);

            // lblEventCount
            lblEventCount.AutoSize = true;
            lblEventCount.Location = new Point(10, 302);
            lblEventCount.Text = "Total Events: 0";

            // btnRefresh
            btnRefresh.Location = new Point(20, 390);
            btnRefresh.Size = new Size(100, 35);
            btnRefresh.Text = "Refresh";
            btnRefresh.Click += BtnRefresh_Click;

            // btnBack
            btnBack.Location = new Point(670, 390);
            btnBack.Size = new Size(100, 35);
            btnBack.Text = "Back";
            btnBack.Click += BtnBack_Click;

            // SocietyReports
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(790, 440);
            Controls.Add(lblTitle);
            Controls.Add(grpMembers);
            Controls.Add(grpEvents);
            Controls.Add(btnRefresh);
            Controls.Add(btnBack);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "SocietyReports";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Society Reports";
            Load += SocietyReports_Load;
            grpMembers.ResumeLayout(false);
            grpMembers.PerformLayout();
            grpEvents.ResumeLayout(false);
            grpEvents.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMembers).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvEvents).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private GroupBox grpMembers;
        private DataGridView dgvMembers;
        private Label lblMemberCount;
        private GroupBox grpEvents;
        private DataGridView dgvEvents;
        private Label lblEventCount;
        private Button btnRefresh;
        private Button btnBack;
    }
}
