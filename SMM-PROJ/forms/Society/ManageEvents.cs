using SMM_PROJ.DAL;
using SMM_PROJ.Helpers;

namespace SMM_PROJ.Forms.Society
{
    /// <summary>
    /// Lets the society head create events and cancel existing ones.
    /// New events are created with Pending status (admin must approve).
    /// </summary>
    public partial class ManageEvents : Form
    {
        public ManageEvents()
        {
            InitializeComponent();
        }

        private void ManageEvents_Load(object? sender, EventArgs e)
        {
            LoadEvents();
        }

        private void LoadEvents()
        {
            try
            {
                if (Session.SocietyID == null) return;

                dgvEvents.DataSource = EventDAL.GetBySociety(Session.SocietyID.Value);
                if (dgvEvents.Columns.Contains("EventID"))
                    dgvEvents.Columns["EventID"]!.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading events: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Opens a modal dialog to create a new event.
        /// </summary>
        private void BtnCreate_Click(object? sender, EventArgs e)
        {
            using var dialog = new CreateEventDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    EventDAL.Create(
                        Session.SocietyID!.Value,
                        dialog.EventTitle,
                        dialog.EventDescription,
                        dialog.EventDate,
                        dialog.EventVenue);

                    LoadEvents();
                    MessageBox.Show("Event created (pending admin approval).", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Cancels the selected event.
        /// </summary>
        private void BtnCancelEvent_Click(object? sender, EventArgs e)
        {
            if (dgvEvents.SelectedRows.Count == 0) return;

            var result = MessageBox.Show("Are you sure you want to cancel this event?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result != DialogResult.Yes) return;

            try
            {
                int eventId = Convert.ToInt32(dgvEvents.SelectedRows[0].Cells["EventID"].Value);
                EventDAL.Cancel(eventId);
                LoadEvents();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnBack_Click(object? sender, EventArgs e)
        {
            new SocietyDashboard().Show();
            this.Close();
        }
    }

    /// <summary>
    /// Modal dialog for creating a new event. Returns data via public properties.
    /// </summary>
    internal class CreateEventDialog : Form
    {
        public string EventTitle { get; private set; } = string.Empty;
        public string EventDescription { get; private set; } = string.Empty;
        public DateTime EventDate { get; private set; }
        public string EventVenue { get; private set; } = string.Empty;

        private readonly TextBox txtTitle;
        private readonly TextBox txtDescription;
        private readonly DateTimePicker dtpDate;
        private readonly TextBox txtVenue;

        public CreateEventDialog()
        {
            Text = "Create New Event";
            ClientSize = new Size(400, 330);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            MaximizeBox = false;
            MinimizeBox = false;

            var lblTitle = new Label { Text = "Title:", Location = new Point(20, 20), AutoSize = true };
            txtTitle = new TextBox { Location = new Point(20, 40), Size = new Size(350, 23) };

            var lblDesc = new Label { Text = "Description:", Location = new Point(20, 75), AutoSize = true };
            txtDescription = new TextBox { Location = new Point(20, 95), Size = new Size(350, 60), Multiline = true };

            var lblDate = new Label { Text = "Event Date:", Location = new Point(20, 165), AutoSize = true };
            dtpDate = new DateTimePicker { Location = new Point(20, 185), Size = new Size(350, 23), Format = DateTimePickerFormat.Short };

            var lblVenue = new Label { Text = "Venue:", Location = new Point(20, 220), AutoSize = true };
            txtVenue = new TextBox { Location = new Point(20, 240), Size = new Size(350, 23) };

            var btnOk = new Button { Text = "Create", Location = new Point(190, 280), Size = new Size(80, 30), DialogResult = DialogResult.OK };
            var btnDialogCancel = new Button { Text = "Cancel", Location = new Point(290, 280), Size = new Size(80, 30), DialogResult = DialogResult.Cancel };

            btnOk.Click += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtTitle.Text) || string.IsNullOrWhiteSpace(txtVenue.Text))
                {
                    MessageBox.Show("Title and Venue are required.", "Validation",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    DialogResult = DialogResult.None;
                    return;
                }
                EventTitle = txtTitle.Text.Trim();
                EventDescription = txtDescription.Text.Trim();
                EventDate = dtpDate.Value;
                EventVenue = txtVenue.Text.Trim();
            };

            Controls.AddRange(new Control[] { lblTitle, txtTitle, lblDesc, txtDescription, lblDate, dtpDate, lblVenue, txtVenue, btnOk, btnDialogCancel });
            AcceptButton = btnOk;
            CancelButton = btnDialogCancel;
        }
    }
}
