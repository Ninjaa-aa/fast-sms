namespace SMM_PROJ.Forms.Society
{
    partial class ManageEvents
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
            btnCreate = new Button();
            btnCancel = new Button();
            btnBack = new Button();

            ((System.ComponentModel.ISupportInitialize)dgvEvents).BeginInit();
            SuspendLayout();

            // lblTitle
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.Location = new Point(20, 15);
            lblTitle.Text = "Manage Events";

            // dgvEvents
            dgvEvents.AllowUserToAddRows = false;
            dgvEvents.AllowUserToDeleteRows = false;
            dgvEvents.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvEvents.Location = new Point(20, 55);
            dgvEvents.MultiSelect = false;
            dgvEvents.ReadOnly = true;
            dgvEvents.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvEvents.Size = new Size(740, 280);

            // btnCreate
            btnCreate.Location = new Point(20, 350);
            btnCreate.Size = new Size(140, 35);
            btnCreate.Text = "Create Event";
            btnCreate.Click += BtnCreate_Click;

            // btnCancel
            btnCancel.Location = new Point(180, 350);
            btnCancel.Size = new Size(140, 35);
            btnCancel.Text = "Cancel Event";
            btnCancel.Click += BtnCancelEvent_Click;

            // btnBack
            btnBack.Location = new Point(660, 350);
            btnBack.Size = new Size(100, 35);
            btnBack.Text = "Back";
            btnBack.Click += BtnBack_Click;

            // ManageEvents
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(780, 400);
            Controls.Add(lblTitle);
            Controls.Add(dgvEvents);
            Controls.Add(btnCreate);
            Controls.Add(btnCancel);
            Controls.Add(btnBack);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "ManageEvents";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Manage Events";
            Load += ManageEvents_Load;
            ((System.ComponentModel.ISupportInitialize)dgvEvents).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private DataGridView dgvEvents;
        private Button btnCreate;
        private Button btnCancel;
        private Button btnBack;
    }
}
