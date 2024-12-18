using Bunifu.UI.WinForms;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using TDF.Classes;
using TDF.Net.Classes;
using static TDF.Net.loginForm;

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

        int numberOfDaysRequested, usedBalance, availableBalance = 0;

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
        private int getAnnualBalance(int userID)
        {
            int annualLeave = 0;

            try
            {
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();

                    string query = "SELECT Annual FROM AnnualLeave WHERE UserID = @UserID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", userID);

                        object result = cmd.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int leaveValue))
                        {
                            annualLeave = leaveValue;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("A database error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return annualLeave;
        }
        public int getApprovedDays(int userID)
        {
            int totalApprovedDays = 0;

            try
            {
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();

                    string query = "SELECT SUM(NumberOfDays) AS TotalApprovedDays FROM Requests WHERE RequestStatus = 'Approved' AND RequestUserID = @UserID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", userID);

                        object result = cmd.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int sum))
                        {
                            totalApprovedDays = sum;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("A database error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return totalApprovedDays;
        }
        private void updateLeaveBalance()
        {
            usedBalance = getApprovedDays(loggedInUser.userID);
            availableBalance = getAnnualBalance(loggedInUser.userID) - usedBalance;
            availableBalanceLabel.Text = availableBalance.ToString();

            numberOfDaysRequested = (Convert.ToDateTime(toDayDatePicker.Text) - Convert.ToDateTime(toDayDatePicker.Text)).Days + 1;
            daysRequestedLabel.Text = numberOfDaysRequested.ToString();
            remainingBalanceLabel.Text = (availableBalance - numberOfDaysRequested).ToString();
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
        private void panel_Paint(object sender, PaintEventArgs e)
        {
            base.OnPaint(e);
            ControlPaint.DrawBorder(e.Graphics, ClientRectangle, ThemeColor.SecondaryColor, ButtonBorderStyle.Solid);
        }
        private void toDayDatePicker_ValueChanged(object sender, EventArgs e)
        {
            updateLeaveBalance();
        }

        private void fromDayDatePicker_ValueChanged(object sender, EventArgs e)
        {
            updateLeaveBalance();
        }

        private void dayoffRadioButton_CheckedChanged(object sender, BunifuRadioButton.CheckedChangedEventArgs e)
        {
            if (dayoffRadioButton.Checked || workFromHomeRadioButton.Checked)
            {
                // Hide time-related controls and show date-related controls
                SetTimeControlsVisibility(false);
                SetDateControlsVisibility(true);

                if (dayoffRadioButton.Checked)
                {
                    updateLeaveBalance();
                }
            }
            else
            {
                // Show time-related controls and hide date-related controls
                SetTimeControlsVisibility(true);
                SetDateControlsVisibility(false);
            }

            // Helper methods for setting visibility
            void SetTimeControlsVisibility(bool isVisible)
            {
                fromTimeTextBox.Visible = isVisible;
                toTimeTextBox.Visible = isVisible;
                fromLabel.Visible = isVisible;
                toLabel.Visible = isVisible;
            }

            void SetDateControlsVisibility(bool isVisible)
            {
                toDateLabel.Visible = isVisible;
                toDayDatePicker.Visible = isVisible;
            }
        }
        #endregion

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

                numberOfDaysRequested = (toDay - fromDay).Days + 1;
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
                    ? new Request(dayoffRadioButton.Checked ? "Dayoff" : "Permission", reasonTextBox.Text, loginForm.loggedInUser.FullName, Convert.ToDateTime(fromDayDatePicker.Text), Convert.ToDateTime(toDayDatePicker.Text), loginForm.loggedInUser.userID, loginForm.loggedInUser.Department, numberOfDaysRequested)
                    : new Request(dayoffRadioButton.Checked ? "Dayoff" : "Permission", reasonTextBox.Text, loginForm.loggedInUser.FullName, Convert.ToDateTime(fromDayDatePicker.Text), Convert.ToDateTime(fromTimeTextBox.Text), Convert.ToDateTime(toTimeTextBox.Text), loginForm.loggedInUser.userID, loginForm.loggedInUser.Department);
                Request.add();
            }
        }

    }
}
