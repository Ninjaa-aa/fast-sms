namespace SMM_PROJ.Forms.Student
{
    partial class BrowseEvents
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
            dgvEvents = new DataGridView();
            btnRegister = new Button();
            lblStatus = new Label();
            btnBack = new Button();

            ((System.ComponentModel.ISupportInitialize)dgvEvents).BeginInit();
            SuspendLayout();

            // lblTitle
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.Location = new Point(20, 15);
            lblTitle.Text = "Upcoming Events";

            // dgvEvents
            dgvEvents.AllowUserToAddRows = false;
            dgvEvents.AllowUserToDeleteRows = false;
            dgvEvents.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvEvents.Location = new Point(20, 55);
            dgvEvents.MultiSelect = false;
            dgvEvents.ReadOnly = true;
            dgvEvents.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvEvents.Size = new Size(740, 280);

            // btnRegister
            btnRegister.Location = new Point(20, 350);
            btnRegister.Size = new Size(160, 35);
            btnRegister.Text = "Register for Event";
            btnRegister.Click += BtnRegister_Click;

            // lblStatus
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(200, 358);
            lblStatus.ForeColor = Color.Green;
            lblStatus.Text = "";

            // btnBack
            btnBack.Location = new Point(660, 350);
            btnBack.Size = new Size(100, 35);
            btnBack.Text = "Back";
            btnBack.Click += BtnBack_Click;

            // BrowseEvents
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(780, 400);
            Controls.Add(lblTitle);
            Controls.Add(dgvEvents);
            Controls.Add(btnRegister);
            Controls.Add(lblStatus);
            Controls.Add(btnBack);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "BrowseEvents";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Browse Events";
            Load += BrowseEvents_Load;
            ((System.ComponentModel.ISupportInitialize)dgvEvents).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private DataGridView dgvEvents;
        private Button btnRegister;
        private Label lblStatus;
        private Button btnBack;
    }
}
