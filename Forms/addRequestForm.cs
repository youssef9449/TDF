using Bunifu.UI.WinForms;
using TDF.Net.Classes;
using System;
using System.Windows.Forms;
using TDF.Classes;

namespace TDF.Net.Forms
{
    public partial class addRequestForm : Form
    {
        Request RequestToEdit;

        public addRequestForm()
        {
            InitializeComponent();
            Program.loadForm(this);

        }
        public static bool requestAddedOrUpdated;

        public addRequestForm(Request request)
        {
            InitializeComponent();
            RequestToEdit = request;

            // Populate the form fields with the request data
            PopulateFieldsWithRequestData();
            Program.loadForm(this);
        }


        #region Methods
        private void PopulateFieldsWithRequestData()
        {
            if (RequestToEdit != null)
            {
                dayoffRadioButton.Checked = RequestToEdit.RequestType == "Dayoff" ? true : false;
                exitRadioButton.Checked = dayoffRadioButton.Checked ? false : true;
                reasonTextBox.Text = RequestToEdit.RequestReason;
                fromDayDatePicker.Value = RequestToEdit.RequestFromDay;
                toDayDatePicker.Value = (DateTime)(DateTime.TryParse(RequestToEdit.RequestToDay.ToString(), out DateTime to) ? RequestToEdit.RequestToDay : null);
                fromTimeTextBox.Text = dayoffRadioButton.Checked ? "" : RequestToEdit.RequestBeginningTime.Value.TimeOfDay.ToString(@"hh\:mm");
                toTimeTextBox.Text = dayoffRadioButton.Checked ? "" : RequestToEdit.RequestEndingTime.Value.TimeOfDay.ToString(@"hh\:mm");
            }
        }
        #endregion

        #region Events
        private void panel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                mainForm.ReleaseCapture();
                mainForm.SendMessage(Handle, 0x112, 0xf012, 0);
            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            ControlPaint.DrawBorder(e.Graphics, ClientRectangle, ThemeColor.SecondaryColor, ButtonBorderStyle.Solid);
        }
        private void submitButton_Click(object sender, EventArgs e)
        {
            if (exitRadioButton.Checked)
            {
                if (!DateTime.TryParse(fromTimeTextBox.Text, out DateTime from))
                {
                    MessageBox.Show("Beginning time isn't correct");
                    fromTimeTextBox.Focus();
                    return;
                }

                if (!DateTime.TryParse(toTimeTextBox.Text, out DateTime to))
                {
                    MessageBox.Show("Ending time isn't correct");
                    toTimeTextBox.Focus();
                    return;
                }

                if (from >= to)
                {
                    MessageBox.Show("Beginning time can't be equal to or earlier than the ending time");
                    return;
                }
            }
            else
            {

                if (!DateTime.TryParse(fromDayDatePicker.Text, out DateTime fromDay))
                {
                    MessageBox.Show("Invalid selected Beginning Day");
                    fromDayDatePicker.Focus();
                    return;
                }

                if (!DateTime.TryParse(toDayDatePicker.Text, out DateTime toDay))
                {
                    MessageBox.Show("Invalid selected end Day");
                    toDayDatePicker.Focus();
                    return;
                }

                if (fromDay > toDay)
                {
                    MessageBox.Show("Beginning date can't be earlier than the ending date");
                    return;
                }
            }

            if (RequestToEdit != null)
            {
                // Update existing Request
                RequestToEdit.RequestType = dayoffRadioButton.Checked ? "Dayoff" : "Permission";
                RequestToEdit.RequestReason = reasonTextBox.Text;
                RequestToEdit.RequestFromDay = fromDayDatePicker.Value;
                RequestToEdit.RequestToDay = dayoffRadioButton.Checked ? Convert.ToDateTime(toDayDatePicker.Text) : (DateTime?)null;
                RequestToEdit.RequestBeginningTime = dayoffRadioButton.Checked ? (DateTime?)null : Convert.ToDateTime(fromTimeTextBox.Text);
                RequestToEdit.RequestEndingTime = dayoffRadioButton.Checked ? (DateTime?)null : Convert.ToDateTime(toTimeTextBox.Text);

                // Call a method to update the Request in the database
                RequestToEdit.update();
            }
            else
            {

                Request Request = dayoffRadioButton.Checked
                    ? new Request(dayoffRadioButton.Checked ? "Dayoff" : "Permission", reasonTextBox.Text,loginForm.loggedInUser.FullName ,Convert.ToDateTime(fromDayDatePicker.Text), Convert.ToDateTime(toDayDatePicker.Text), loginForm.loggedInUser.userID, loginForm.loggedInUser.Department)
                    : new Request(dayoffRadioButton.Checked ? "Dayoff" : "Permission", reasonTextBox.Text, loginForm.loggedInUser.FullName, Convert.ToDateTime(fromDayDatePicker.Text), Convert.ToDateTime(fromTimeTextBox.Text), Convert.ToDateTime(toTimeTextBox.Text), loginForm.loggedInUser.userID, loginForm.loggedInUser.Department);
                Request.add();
            }
        }
        private void dayoffRadioButton_CheckedChanged(object sender, BunifuRadioButton.CheckedChangedEventArgs e)
        {
            if (dayoffRadioButton.Checked)
            {
                fromTimeTextBox.Visible = false;
                toTimeTextBox.Visible = false;
                fromLabel.Visible = false;
                toLabel.Visible = false;
                toDateLabel.Visible = true;
                toDayDatePicker.Visible = true;
            }
            else
            {
                fromTimeTextBox.Visible = true;
                toTimeTextBox.Visible = true;
                fromLabel.Visible = true;
                toLabel.Visible = true;
                toDateLabel.Visible = false;
                toDayDatePicker.Visible = false;
            }
        }
        #endregion

    }
}
