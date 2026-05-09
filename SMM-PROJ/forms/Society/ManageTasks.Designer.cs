namespace SMM_PROJ.Forms.Society
{
    partial class ManageTasks
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
            dgvTasks = new DataGridView();
            btnAssign = new Button();
            btnComplete = new Button();
            btnBack = new Button();

            ((System.ComponentModel.ISupportInitialize)dgvTasks).BeginInit();
            SuspendLayout();

            // lblTitle
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.Location = new Point(20, 15);
            lblTitle.Text = "Manage Tasks";

            // dgvTasks
            dgvTasks.AllowUserToAddRows = false;
            dgvTasks.AllowUserToDeleteRows = false;
            dgvTasks.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTasks.Location = new Point(20, 55);
            dgvTasks.MultiSelect = false;
            dgvTasks.ReadOnly = true;
            dgvTasks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTasks.Size = new Size(740, 280);

            // btnAssign
            btnAssign.Location = new Point(20, 350);
            btnAssign.Size = new Size(140, 35);
            btnAssign.Text = "Assign New Task";
            btnAssign.Click += BtnAssign_Click;

            // btnComplete
            btnComplete.Location = new Point(180, 350);
            btnComplete.Size = new Size(150, 35);
            btnComplete.Text = "Mark as Completed";
            btnComplete.Click += BtnComplete_Click;

            // btnBack
            btnBack.Location = new Point(660, 350);
            btnBack.Size = new Size(100, 35);
            btnBack.Text = "Back";
            btnBack.Click += BtnBack_Click;

            // ManageTasks
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(780, 400);
            Controls.Add(lblTitle);
            Controls.Add(dgvTasks);
            Controls.Add(btnAssign);
            Controls.Add(btnComplete);
            Controls.Add(btnBack);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "ManageTasks";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Manage Tasks";
            Load += ManageTasks_Load;
            ((System.ComponentModel.ISupportInitialize)dgvTasks).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private DataGridView dgvTasks;
        private Button btnAssign;
        private Button btnComplete;
        private Button btnBack;
    }
}
