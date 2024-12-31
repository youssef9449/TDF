using Bunifu.UI.WinForms;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using TDF.Classes;
using TDF.Net.Classes;
using static TDF.Net.loginForm;
using static TDF.Net.mainForm;


namespace TDF.Net.Forms
{
    public partial class addRequestForm : Form
    {
        Request RequestToEdit;

        public addRequestForm()
        {
            InitializeComponent();
            externalAssignmentRadioButton.Checked = false;
            casualRadioButton.Checked = false;
            dayoffRadioButton.Checked = true;
            annualRadioButton.Checked = true;
        }

        public addRequestForm(Request request)
        {
            InitializeComponent();

            RequestToEdit = request;

            populateFieldsWithRequestData();
        }

        int numberOfDaysRequested, availableAnnualBalance, availableCasualBalance = 0;
        public static bool requestAddedOrUpdated;

        #region Methods
        private void populateFieldsWithRequestData()
        {
            switch (RequestToEdit.RequestType)
            {
                case "Work From Home":
                    workFromHomeRadioButton.Checked = true;
                    externalAssignmentRadioButton.Checked = false;
                    dayoffRadioButton.Checked = false;
                    exitRadioButton.Checked = false;
                    break;
                case "Permission":
                    exitRadioButton.Checked = true;
                    workFromHomeRadioButton.Checked = false;
                    externalAssignmentRadioButton.Checked = false;
                    dayoffRadioButton.Checked = false;
                    break;
                case "External Assignment":
                    externalAssignmentRadioButton.Checked = true;
                    exitRadioButton.Checked = false;
                    workFromHomeRadioButton.Checked = false;
                    dayoffRadioButton.Checked = false;
                    break;
                default:
                    dayoffRadioButton.Checked = true;
                    externalAssignmentRadioButton.Checked = false;
                    exitRadioButton.Checked = false;
                    workFromHomeRadioButton.Checked = false;

                    if (RequestToEdit.RequestType == "Annual")
                    {
                        casualRadioButton.Checked = false;
                        annualRadioButton.Checked = true;
                    }
                    else
                    {
                        annualRadioButton.Checked = false;
                        casualRadioButton.Checked = true;
                    }
                    break;
            }

            reasonTextBox.Text = RequestToEdit.RequestReason;
            fromDayDatePicker.Value = RequestToEdit.RequestFromDay;
            toDayDatePicker.Value = (DateTime)(DateTime.TryParse(RequestToEdit.RequestToDay.ToString(), out DateTime to) ? RequestToEdit.RequestToDay : null);
            fromTimeTextBox.Text = dayoffRadioButton.Checked ? "" : RequestToEdit.RequestBeginningTime.Value.TimeOfDay.ToString(@"hh\:mm");
            toTimeTextBox.Text = dayoffRadioButton.Checked ? "" : RequestToEdit.RequestEndingTime.Value.TimeOfDay.ToString(@"hh\:mm");

            reasonTextBox.ReadOnly = hasManagerRole || hasAdminRole;
            workFromHomeRadioButton.Enabled = !(hasManagerRole || hasAdminRole);
            externalAssignmentRadioButton.Enabled = !(hasManagerRole || hasAdminRole);
            dayoffRadioButton.Enabled = !(hasManagerRole || hasAdminRole);
            exitRadioButton.Enabled = !(hasManagerRole || hasAdminRole);
            annualRadioButton.Enabled = !(hasManagerRole || hasAdminRole);
            casualRadioButton.Enabled = !(hasManagerRole || hasAdminRole);
        }
        private int getWorkingDays(DateTime start, DateTime end)
        {
            int workingDays = 0;
            for (DateTime date = start; date <= end; date = date.AddDays(1))
            {
                // Exclude Fridays and Saturdays
                if (date.DayOfWeek != DayOfWeek.Friday && date.DayOfWeek != DayOfWeek.Saturday)
                {
                    workingDays++;
                }
            }
            return workingDays;
        }
        private int getLeaveDays(string leaveType, int userID)
        {
            int days = 0;

            try
            {
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();

                    string query = $"SELECT {leaveType} FROM AnnualLeave WHERE UserID = @UserID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", userID);

                        object result = cmd.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int sum))
                        {
                            days = sum;
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

            return days;
        }
        private void updateLeaveBalance()
        {
            DateTime toDate = Convert.ToDateTime(toDayDatePicker.Value).Date;
            DateTime fromDate = Convert.ToDateTime(fromDayDatePicker.Value).Date;

            if (toDate < fromDate)
            {
                daysRequestedLabel.Text = "------";
                remainingBalanceLabel.Text = "------";
            }
            else
            {
                if (dayoffRadioButton.Checked)
                {
                    if (annualRadioButton.Checked)
                    {
                        availableAnnualBalance = getLeaveDays("AnnualBalance", loggedInUser.userID);
                        availableBalanceLabel.Text = availableAnnualBalance.ToString();

                        numberOfDaysRequested = getWorkingDays(fromDate, toDate);
                        daysRequestedLabel.Text = numberOfDaysRequested.ToString();
                        remainingBalanceLabel.Text = (availableAnnualBalance - numberOfDaysRequested).ToString();
                    }
                    else
                    {
                        availableCasualBalance = getLeaveDays("CasualBalance", loggedInUser.userID);
                        availableBalanceLabel.Text = availableCasualBalance.ToString();

                        numberOfDaysRequested = getWorkingDays(fromDate, toDate);
                        daysRequestedLabel.Text = numberOfDaysRequested.ToString();
                        remainingBalanceLabel.Text = (availableCasualBalance - numberOfDaysRequested).ToString();
                    }
                }
                else
                {
                    numberOfDaysRequested = getWorkingDays(fromDate, toDate);
                    daysRequestedLabel.Text = numberOfDaysRequested.ToString();
                }
            }
        }
        void setTimeControlsVisibility(bool isVisible)
        {
            fromTimeTextBox.Visible = isVisible;
            toTimeTextBox.Visible = isVisible;
            fromLabel.Visible = isVisible;
            toLabel.Visible = isVisible;
        }
        void setDateControlsVisibility(bool isVisible)
        {
            toDateLabel.Visible = isVisible;
            toDayDatePicker.Visible = isVisible;
        }
        void setBalanceControlsVisibility(bool isVisible)
        {
            bunifuLabel3.Visible = isVisible;
            bunifuLabel4.Visible = isVisible;
            bunifuLabel5.Visible = isVisible;
            availableBalanceLabel.Visible = isVisible;
            daysRequestedLabel.Visible = isVisible;
            remainingBalanceLabel.Visible = isVisible;
        }
        private bool validateTimeInputs(string fromText, string toText, out DateTime from, out DateTime to)
        {
            from = default;
            to = default;

            if (!DateTime.TryParse(fromText, out from))
            {
                MessageBox.Show("Beginning time isn't correct.");
                fromTimeTextBox.Focus();
                return false;
            }

            if (!DateTime.TryParse(toText, out to))
            {
                MessageBox.Show("Ending time isn't correct.");
                toTimeTextBox.Focus();
                return false;
            }

            return true;
        }
        private bool validateDayInputs(string fromText, string toText, out DateTime fromDay, out DateTime toDay)
        {
            fromDay = default;
            toDay = default;

            if (!DateTime.TryParse(fromText, out fromDay))
            {
                MessageBox.Show("Invalid selected beginning day.");
                fromDayDatePicker.Focus();
                return false;
            }

            if (!DateTime.TryParse(toText, out toDay))
            {
                MessageBox.Show("Invalid selected end day.");
                toDayDatePicker.Focus();
                return false;
            }

            return true;
        }
        private void addNewRequest(string requestType)
        {
            DateTime? fromTime = (requestType == "Permission" || requestType == "External Assignment")
                ? Convert.ToDateTime(fromTimeTextBox.Text)
                : (DateTime?)null;

            DateTime? toTime = (requestType == "Permission" || requestType == "External Assignment")
                ? Convert.ToDateTime(toTimeTextBox.Text)
                : (DateTime?)null;

            numberOfDaysRequested = (requestType == "Permission" || requestType == "External Assignment") ? 0 : numberOfDaysRequested;

            Request newRequest = new Request(
                requestType,
                reasonTextBox.Text,
                loggedInUser.FullName,
                fromDayDatePicker.Value,
                toDayDatePicker.Value,
                loggedInUser.userID,
                loggedInUser.Department,
                numberOfDaysRequested,
                fromTime,
                toTime
            );

            newRequest.add();
        }
        private void updateExistingRequest(string requestType)
        {
            RequestToEdit.RequestType = requestType;
            RequestToEdit.RequestReason = reasonTextBox.Text;
            RequestToEdit.RequestFromDay = fromDayDatePicker.Value;
            RequestToEdit.RequestToDay = dayoffRadioButton.Checked || workFromHomeRadioButton.Checked ? toDayDatePicker.Value : (DateTime?)null;
            RequestToEdit.RequestBeginningTime = requestType == "Permission" || requestType == "External Assignment" ? Convert.ToDateTime(fromTimeTextBox.Text) : (DateTime?)null;
            RequestToEdit.RequestEndingTime = requestType == "Permission" || requestType == "External Assignment" ? Convert.ToDateTime(toTimeTextBox.Text) : (DateTime?)null;
            RequestToEdit.RequestNumberOfDays = requestType == "Permission" || requestType == "External Assignment" ? 0 : numberOfDaysRequested;
            RequestToEdit.update();
        }
        #endregion

        #region Events
        private void addRequestForm_Load(object sender, EventArgs e)
        {
            Program.loadForm(this);

            availableBalanceLabel.ForeColor = ThemeColor.SecondaryColor;
            daysRequestedLabel.ForeColor = ThemeColor.SecondaryColor;
            remainingBalanceLabel.ForeColor = ThemeColor.SecondaryColor;
        }
        private void panel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                ReleaseCapture();
                SendMessage(Handle, 0x112, 0xf012, 0);
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
        private void dayoffRadioButton_CheckedChanged(object sender, BunifuRadioButton.CheckedChangedEventArgs e)
        {
            if (dayoffRadioButton.Checked)
            {
                // Hide time-related controls and show date-related controls
                setTimeControlsVisibility(false);
                setDateControlsVisibility(true);
                setBalanceControlsVisibility(true);
                updateLeaveBalance();
                leaveGroupBox.Visible = true;

            }
        }
        private void exitRadioButton_CheckedChanged(object sender, BunifuRadioButton.CheckedChangedEventArgs e)
        {
            if (exitRadioButton.Checked)
            {
                // Show time-related controls and hide date-related controls
                setTimeControlsVisibility(true);
                setDateControlsVisibility(false);
                setBalanceControlsVisibility(false);
                leaveGroupBox.Visible = false;
            }
        }
        private void workFromHomeRadioButton_CheckedChanged(object sender, BunifuRadioButton.CheckedChangedEventArgs e)
        {
            if (workFromHomeRadioButton.Checked)
            {
                // Hide time-related controls and show date-related controls
                setTimeControlsVisibility(false);
                setDateControlsVisibility(true);
                setBalanceControlsVisibility(false);
                leaveGroupBox.Visible = false;
                daysRequestedLabel.Visible = true;
                bunifuLabel3.Visible = true;
            }
        }
        private void externalAssignmentRadioButton_CheckedChanged(object sender, BunifuRadioButton.CheckedChangedEventArgs e)
        {
            if (externalAssignmentRadioButton.Checked)
            {
                // Show time-related controls and hide date-related controls
                setTimeControlsVisibility(true);
                setDateControlsVisibility(false);
                setBalanceControlsVisibility(false);
                leaveGroupBox.Visible = false;
            }
        }
        private void annualRadioButton_CheckedChanged(object sender, BunifuRadioButton.CheckedChangedEventArgs e)
        {
            updateLeaveBalance();

        }
        private void casualRadioButton_CheckedChanged(object sender, BunifuRadioButton.CheckedChangedEventArgs e)
        {
            updateLeaveBalance();
        }
        private void toDayDatePicker_ValueChanged(object sender, EventArgs e)
        {
            updateLeaveBalance();
        }
        private void fromDayDatePicker_ValueChanged(object sender, EventArgs e)
        {
            updateLeaveBalance();

        }
        #endregion

        #region Buttons
        private void submitButton_Click(object sender, EventArgs e)
        {
            // Validate inputs for time-based requests

            if (exitRadioButton.Checked || externalAssignmentRadioButton.Checked)
            {
                if (!validateTimeInputs(fromTimeTextBox.Text, toTimeTextBox.Text, out DateTime from, out DateTime to))
                    return;

                if (from >= to)
                {
                    MessageBox.Show("Ending time can't be earlier than or equal to the beginning time.");
                    return;
                }
            }

            // Validate inputs for day-based requests
            else
            {
                if (!validateDayInputs(fromDayDatePicker.Text, toDayDatePicker.Text, out DateTime fromDay, out DateTime toDay))
                    return;

                if (fromDay > toDay)
                {
                    MessageBox.Show("Ending date can't be earlier than the beginning date.");
                    return;
                }

                //numberOfDaysRequested = (toDay - fromDay).Days + 1;
            }

            // Determine request type
            string requestType = exitRadioButton.Checked
                ? "Permission"
                : workFromHomeRadioButton.Checked
                ? "Work From Home"
                : dayoffRadioButton.Checked
                ? (annualRadioButton.Checked ? "Annual" : "Casual")
                : "External Assignment";

            // Create or update request
            if (RequestToEdit == null)
            {
                addNewRequest(requestType);
            }
            else
            {
                updateExistingRequest(requestType);
            }
        }
        #endregion
    }
}
