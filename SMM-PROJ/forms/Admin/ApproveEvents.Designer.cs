namespace SMM_PROJ.Forms.Admin
{
    partial class ApproveEvents
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
            btnApprove = new Button();
            btnReject = new Button();
            btnBack = new Button();

            ((System.ComponentModel.ISupportInitialize)dgvEvents).BeginInit();
            SuspendLayout();

            // lblTitle
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.Location = new Point(20, 15);
            lblTitle.Text = "Approve Events";

            // dgvEvents
            dgvEvents.AllowUserToAddRows = false;
            dgvEvents.AllowUserToDeleteRows = false;
            dgvEvents.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvEvents.Location = new Point(20, 55);
            dgvEvents.MultiSelect = false;
            dgvEvents.ReadOnly = true;
            dgvEvents.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvEvents.Size = new Size(740, 280);

            // btnApprove
            btnApprove.Location = new Point(20, 350);
            btnApprove.Size = new Size(120, 35);
            btnApprove.Text = "Approve";
            btnApprove.Click += BtnApprove_Click;

            // btnReject
            btnReject.Location = new Point(160, 350);
            btnReject.Size = new Size(120, 35);
            btnReject.Text = "Reject";
            btnReject.Click += BtnReject_Click;

            // btnBack
            btnBack.Location = new Point(660, 350);
            btnBack.Size = new Size(100, 35);
            btnBack.Text = "Back";
            btnBack.Click += BtnBack_Click;

            // ApproveEvents
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(780, 400);
            Controls.Add(lblTitle);
            Controls.Add(dgvEvents);
            Controls.Add(btnApprove);
            Controls.Add(btnReject);
            Controls.Add(btnBack);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "ApproveEvents";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Approve Events";
            Load += ApproveEvents_Load;
            ((System.ComponentModel.ISupportInitialize)dgvEvents).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private DataGridView dgvEvents;
        private Button btnApprove;
        private Button btnReject;
        private Button btnBack;
    }
}
