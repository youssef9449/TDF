using Bunifu.UI.WinForms;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;
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

        int numberOfDaysRequested, availableBalance = 0;
        public static bool requestAddedOrUpdated;

        #region Methods
        private void populateFieldsWithRequestData()
        {

            switch (RequestToEdit.RequestType)
            {
                case "Work From Home":
                    SetRadioButtonStates(workFromHome: true, externalAssignment: false, dayOff: false, exit: false, annual: false, casual: false);
                    break;
                case "Permission":
                    SetRadioButtonStates(workFromHome: false, externalAssignment: false, dayOff: false, exit: true, annual: false, casual: false);
                    break;
                case "External Assignment":
                    SetRadioButtonStates(workFromHome: false, externalAssignment: true, dayOff: false, exit: false, annual: false, casual: false);
                    break;
                default:
                    bool isAnnual = RequestToEdit.RequestType == "Annual";
                    bool isCasual = RequestToEdit.RequestType == "Emergency";

                    SetRadioButtonStates(workFromHome: false, externalAssignment: false, dayOff: true, exit: false, isAnnual, isCasual);
                    break;
            }
            // Set radio button states based on the request type
            void SetRadioButtonStates(bool workFromHome, bool externalAssignment, bool dayOff, bool exit, bool annual, bool casual)
            {
                workFromHomeRadioButton.Checked = workFromHome;
                externalAssignmentRadioButton.Checked = externalAssignment;
                dayoffRadioButton.Checked = dayOff;
                exitRadioButton.Checked = exit;

                if (annual)
                {
                    annualRadioButton.Checked = true;
                    casualRadioButton.Checked = false;
                    unpaidRadioButton.Checked = false;
                }
                else if (casual)
                {
                    casualRadioButton.Checked = true;
                    annualRadioButton.Checked = false;
                    unpaidRadioButton.Checked = false;
                }
                else
                {
                    casualRadioButton.Checked = false;
                    annualRadioButton.Checked = false;
                    unpaidRadioButton.Checked = true;
                }
            }

            // Populate other fields
            reasonTextBox.Text = RequestToEdit.RequestReason;
            fromDayDatePicker.Value = RequestToEdit.RequestFromDay;
            toDayDatePicker.Value = DateTime.TryParse(RequestToEdit.RequestToDay?.ToString(), out var toDate) ? toDate : DateTime.Now;

            bool isDayOff = dayoffRadioButton.Checked;
            fromTimeTextBox.Text = isDayOff ? string.Empty : RequestToEdit.RequestBeginningTime?.TimeOfDay.ToString(@"hh\:mm");
            toTimeTextBox.Text = isDayOff ? string.Empty : RequestToEdit.RequestEndingTime?.TimeOfDay.ToString(@"hh\:mm");

            // Adjust control states based on user role
            bool isEditable = !(hasManagerRole || hasAdminRole);
            reasonTextBox.ReadOnly = !isEditable;
            workFromHomeRadioButton.Enabled = isEditable;
            externalAssignmentRadioButton.Enabled = isEditable;
            dayoffRadioButton.Enabled = isEditable;
            exitRadioButton.Enabled = isEditable;
            annualRadioButton.Enabled = isEditable;
            casualRadioButton.Enabled = isEditable;
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
        public static int getLeaveDays(string leaveType, int? userID = null, string userName = null)
        {
            int days = 0;

            try
            {
                using (SqlConnection conn = Database.getConnection())
                {
                    conn.Open();

                    // Determine the query based on the provided parameters
                    string query;
                    if (userID.HasValue)
                    {
                        query = $"SELECT {leaveType} FROM AnnualLeave WHERE UserID = @UserID";
                    }
                    else if (!string.IsNullOrEmpty(userName))
                    {
                        query = $"SELECT {leaveType} FROM AnnualLeave WHERE FullName = @FullName";
                    }
                    else
                    {
                        throw new ArgumentException("Either userID or FullName must be provided.");
                    }

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Add the appropriate parameter
                        if (userID.HasValue)
                        {
                            cmd.Parameters.AddWithValue("@UserID", userID.Value);
                        }
                        else if (!string.IsNullOrEmpty(userName))
                        {
                            cmd.Parameters.AddWithValue("@FullName", userName);
                        }

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

        private void updateBalanceLabels(string leaveType, int userId, int daysRequested)
        {
            availableBalance = getLeaveDays(leaveType, userId);
            availableBalanceLabel.Text = availableBalance.ToString();
            remainingBalanceLabel.Text = (availableBalance - daysRequested).ToString();
            balanceLabel.Visible = true;
            availableBalanceLabel.Visible = true;
            remainingLabel.Visible = true;
            remainingBalanceLabel.Visible = true;

        }
        private void updateDays(DateTime toDate, DateTime fromDate)
        {
            numberOfDaysRequested = getWorkingDays(fromDate, toDate);
            daysRequestedLabel.Text = numberOfDaysRequested.ToString();
        }
        private void updateLeaveBalanceLabel()
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
                updateDays(toDate, fromDate);

                if (dayoffRadioButton.Checked)
                {
                    updateDays(toDate, fromDate);

                    if (annualRadioButton.Checked)
                    {
                        updateBalanceLabels("AnnualBalance", loggedInUser.userID, numberOfDaysRequested);

                    }
                    else if (casualRadioButton.Checked)
                    {
                        updateBalanceLabels("CasualBalance", loggedInUser.userID, numberOfDaysRequested);
                    }
                    else
                    {
                        balanceLabel.Visible = false;
                        availableBalanceLabel.Visible = false;
                        remainingLabel.Visible = false;
                        remainingBalanceLabel.Visible = false;
                    }
                }
            }
        }

        private void setTimeControlsVisibility(bool isVisible)
        {
            fromTimeTextBox.Visible = isVisible;
            toTimeTextBox.Visible = isVisible;
            fromLabel.Visible = isVisible;
            toLabel.Visible = isVisible;
        }
        private void setDateControlsVisibility(bool isVisible)
        {
            toDateLabel.Visible = isVisible;
            toDayDatePicker.Visible = isVisible;
        }
        private void setBalanceControlsVisibility(bool isVisible)
        {
            daysLabel.Visible = isVisible;
            balanceLabel.Visible = isVisible;
            remainingLabel.Visible = isVisible;
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
            Program.applyTheme(this);

            availableBalanceLabel.ForeColor = ThemeColor.darkColor;
            daysRequestedLabel.ForeColor = ThemeColor.darkColor;
            remainingBalanceLabel.ForeColor = ThemeColor.darkColor;
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
            ControlPaint.DrawBorder(e.Graphics, ClientRectangle, ThemeColor.darkColor, ButtonBorderStyle.Solid);
        }
        private void panel_Paint(object sender, PaintEventArgs e)
        {
            base.OnPaint(e);
            ControlPaint.DrawBorder(e.Graphics, ClientRectangle, ThemeColor.darkColor, ButtonBorderStyle.Solid);
        }
        private void dayoffRadioButton_CheckedChanged(object sender, BunifuRadioButton.CheckedChangedEventArgs e)
        {
            if (dayoffRadioButton.Checked)
            {
                // Hide time-related controls and show date-related controls
                setTimeControlsVisibility(false);
                setDateControlsVisibility(true);
                setBalanceControlsVisibility(true);
                updateLeaveBalanceLabel();
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
                daysLabel.Visible = true;
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
            updateLeaveBalanceLabel();

        }
        private void casualRadioButton_CheckedChanged(object sender, BunifuRadioButton.CheckedChangedEventArgs e)
        {
            updateLeaveBalanceLabel();
        }
        private void unpaidRadioButton_CheckedChanged(object sender, BunifuRadioButton.CheckedChangedEventArgs e)
        {
            if (unpaidRadioButton.Checked)
            {
                if (getLeaveDays("AnnualBalance", loggedInUser.userID) + getLeaveDays("CasualBalance", loggedInUser.userID) > 0 && RequestToEdit == null)
                {
                    MessageBox.Show("You have Annual or Emergency balance. You can use it instead.");
                }

                updateLeaveBalanceLabel();
            }
        }
        private void toDayDatePicker_ValueChanged(object sender, EventArgs e)
        {
            updateLeaveBalanceLabel();
        }
        private void fromDayDatePicker_ValueChanged(object sender, EventArgs e)
        {
            updateLeaveBalanceLabel();
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
            }

            if (annualRadioButton.Checked)
            {
                if (numberOfDaysRequested > availableBalance)
                {
                    MessageBox.Show("You don't have enough Annual balance. You can adjust your dates accordingly and request the remaining days as unpaid.");
                    return;
                }
            }
            if (casualRadioButton.Checked)
            {
                if (numberOfDaysRequested > availableBalance)
                {
                    MessageBox.Show("You don't have enough Emergency balance. You can adjust your dates accordingly and request the remaining days as unpaid.");
                    return;
                }
            }



            // Determine request type
            string requestType = exitRadioButton.Checked
                ? "Permission"
                : workFromHomeRadioButton.Checked
                ? "Work From Home"
                : dayoffRadioButton.Checked
                ? (annualRadioButton.Checked ? "Annual" : casualRadioButton.Checked ? "Emergency" : "Unpaid")
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
