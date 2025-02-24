using Bunifu.UI.WinForms;
using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using TDF.Net.Classes;
using static TDF.Net.Forms.requestsForm;
using static TDF.Net.loginForm;
using static TDF.Net.mainForm;


namespace TDF.Net.Forms
{
    public partial class addRequestForm : Form
    {
        Request requestToEdit;

        public addRequestForm()
        {
            InitializeComponent();
            externalAssignmentRadioButton.Checked = false;
            casualRadioButton.Checked = false;
            dayoffRadioButton.Checked = true;
            annualRadioButton.Checked = true;
            requiredUserID = loggedInUser.userID;
        }

        public addRequestForm(Request request)
        {
            InitializeComponent();
            requestToEdit = request;
            requiredUserID = request.RequestUserID;
            populateFieldsWithRequestData();
        }

        int numberOfDaysRequested, availableBalance, pendingDays, pendingPermissions = 0;
        public static bool requestAddedOrUpdated = false;
        public event Action requestAddedOrUpdatedEvent;
        int requiredUserID = loggedInUser.userID;

        #region Events
        private void addRequestForm_Load(object sender, EventArgs e)
        {
            Program.applyTheme(this);

            availableBalanceLabel.ForeColor = ThemeColor.darkColor;
            daysRequestedLabel.ForeColor = ThemeColor.darkColor;
            remainingBalanceLabel.ForeColor = ThemeColor.darkColor;
            pendingDaysLabel.ForeColor = ThemeColor.darkColor;
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
                exitRadioButton.Checked = false;
                // Hide time-related controls and show date-related controls
                setTimeControlsVisibility(false);
                setDateControlsVisibility(true);

                setBalanceControlsVisibility(true);
                updateLeaveBalanceLabel();
                leaveGroupBox.Visible = true;
                setDaysLabelLocation();
            }
        }
        private void exitRadioButton_CheckedChanged(object sender, BunifuRadioButton.CheckedChangedEventArgs e)
        {
            if (exitRadioButton.Checked)
            {
                // Show time-related controls and hide date-related controls
                setTimeControlsVisibility(true);
                setDateControlsVisibility(false);
                setBalanceControlsVisibility(true);
                leaveGroupBox.Visible = false;
                updateLeaveBalanceLabel();
                setDaysLabelLocation();
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
                setDaysLabelLocation();
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
                if (getLeaveDays("AnnualBalance", requiredUserID) + getLeaveDays("CasualBalance", requiredUserID) > 0 && requestToEdit == null)
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

            if (exitRadioButton.Checked)
            {
                updateLabelsForPermissionBalance();
            }

        }
        private void addRequestForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            selectedRequest = null;
        }
        #endregion

        #region Methods
        private void populateFieldsWithRequestData()
        {
            switch (requestToEdit.RequestType)
            {
                case "Work From Home":
                    SetRadioButtonStates(workFromHome: true, externalAssignment: false, dayOff: false, exit: false, annual: true, casual: false);
                    break;
                case "Permission":
                    SetRadioButtonStates(workFromHome: false, externalAssignment: false, dayOff: false, exit: true, annual: true, casual: false);
                    break;
                case "External Assignment":
                    SetRadioButtonStates(workFromHome: false, externalAssignment: true, dayOff: false, exit: false, annual: true, casual: false);
                    break;
                default:
                    bool isAnnual = requestToEdit.RequestType == "Annual";
                    bool isCasual = requestToEdit.RequestType == "Emergency";

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
            reasonTextBox.Text = requestToEdit.RequestReason;
            fromDayDatePicker.Value = requestToEdit.RequestFromDay;
            toDayDatePicker.Value = DateTime.TryParse(requestToEdit.RequestToDay?.ToString(), out var toDate) ? toDate : DateTime.Now;

            bool isDayOff = dayoffRadioButton.Checked;
            fromTimeTextBox.Text = isDayOff ? string.Empty : requestToEdit.RequestBeginningTime?.TimeOfDay.ToString(@"hh\:mm");
            toTimeTextBox.Text = isDayOff ? string.Empty : requestToEdit.RequestEndingTime?.TimeOfDay.ToString(@"hh\:mm");

            // Adjust control states based on user role
            bool isEditable = requestToEdit.RequestUserFullName == loggedInUser.FullName || hasAdminRole;
            reasonTextBox.ReadOnly = !isEditable;
            workFromHomeRadioButton.Enabled = isEditable;
            externalAssignmentRadioButton.Enabled = isEditable;
            dayoffRadioButton.Enabled = isEditable;
            exitRadioButton.Enabled = isEditable;
            annualRadioButton.Enabled = isEditable;
            casualRadioButton.Enabled = isEditable;
            unpaidRadioButton.Enabled = isEditable;
        }
        public static int getWorkingDays(DateTime start, DateTime end)
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
        public int getPermissionUsed(int userID)
        {
            try
            {
                DateTime fromTime = Convert.ToDateTime(fromDayDatePicker.Value); // Get the selected date

                int month = fromTime.Month;
                int year = fromTime.Year;

                using (SqlConnection conn = Database.getConnection())
                {
                    conn.Open();

                    string query = @"
    SELECT COUNT(*) 
    FROM Requests 
    WHERE RequestType = @RequestType 
      AND RequestUserID = @RequestUserID 
      AND MONTH(RequestFromDay) = @Month 
      AND YEAR(RequestFromDay) = @Year 
      AND RequestStatus = 'Approved' 
      AND RequestHRStatus = 'Approved'";

                    // Ensure it's from the same month & year

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@RequestType", "Permission");
                        cmd.Parameters.AddWithValue("@RequestUserID", userID);
                        cmd.Parameters.AddWithValue("@Month", month);
                        cmd.Parameters.AddWithValue("@Year", year);

                        object result = cmd.ExecuteScalar();

                        // Correct DBNull check
                        return (result != DBNull.Value && result != null) ? Convert.ToInt32(result) : 0;
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

            return 0; // Default return if an exception occurs.
        }
        public static int getPendingDaysCount(int userID, string requestType)
        {
            try
            {
                using (SqlConnection conn = Database.getConnection())
                {
                    conn.Open();

                    string query = "SELECT SUM (RequestNumberOfDays) FROM Requests " +
                                   "WHERE RequestType = @RequestType " +
                                   "AND (RequestStatus = 'Pending' OR RequestHRStatus = 'Pending') " +
                                   "AND RequestUserID = @RequestUserID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@RequestType", requestType);
                        cmd.Parameters.AddWithValue("@RequestUserID", userID);

                        object result = cmd.ExecuteScalar();

                        // Directly return the result if it's not null, otherwise return 0.
                        return (result != DBNull.Value && result != null) ? Convert.ToInt32(result) : 0;
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

            return 0; // Default return if an exception occurs.
        }
        private int getPendingMonthlyPermissionsCount(int userID)
        {
            try
            {
                DateTime fromTime = Convert.ToDateTime(fromDayDatePicker.Value); // Get the selected date
                int month = fromTime.Month;
                int year = fromTime.Year;

                using (SqlConnection conn = Database.getConnection())
                {
                    conn.Open();

                    string query = @"SELECT COUNT(*) 
                                    FROM Requests 
                                    WHERE RequestType = @RequestType 
                                    AND RequestUserID = @RequestUserID 
                                    AND MONTH(RequestFromDay) = @Month 
                                    AND YEAR(RequestFromDay) = @Year 
                                    AND (RequestStatus = 'Pending' OR RequestHRStatus = 'Pending')";

                    // Ensure it's from the same month & year

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@RequestType", "Permission");
                        cmd.Parameters.AddWithValue("@RequestUserID", userID);
                        cmd.Parameters.AddWithValue("@Month", month);
                        cmd.Parameters.AddWithValue("@Year", year);

                        object result = cmd.ExecuteScalar();

                        // Correct DBNull check
                        return (result != DBNull.Value && result != null) ? Convert.ToInt32(result) : 0;
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

            return 0; // Default return if an exception occurs.
        }
        private void updateLabelsForDayOffBalance(string leaveType, int userId, int daysRequested)
        {
            // Retrieve the available leave balance.
            availableBalance = getLeaveDays(leaveType, userId);
            availableBalanceLabel.Text = availableBalance.ToString();

            // Determine the expected request type based on the selected radio button.
            string expectedRequestType = annualRadioButton.Checked ? "Annual" :
                                         casualRadioButton.Checked ? "Emergency" : null;

            if (expectedRequestType != null)
            {
                // Calculate adjusted pending days if we're editing an existing request.

                if (requestToEdit != null && requestToEdit.RequestType == expectedRequestType)
                {
                    pendingDays -= requestToEdit.RequestNumberOfDays;
                }
                pendingDaysLabel.Text = pendingDays.ToString();

                // Calculate the remaining balance.
                int remaining = availableBalance - pendingDays;

                remainingBalanceLabel.Text = remaining.ToString();
            }

            // Ensure the balance-related labels are visible.
            balanceLabel.Visible = true;
            availableBalanceLabel.Visible = true;
            remainingLabel.Visible = true;
            remainingBalanceLabel.Visible = true;
        }
        private void updateLabelsForPermissionBalance()
        {
            // Define constants for clarity.
            const int maxMonthlyPermissions = 2;
            const int requestedPermissions = 1;

            // Retrieve pending permissions count.
            pendingPermissions = getPendingMonthlyPermissionsCount(requiredUserID);

            // If editing an existing permission request, adjust the pending count.
            if (requestToEdit != null && requestToEdit.RequestType == "Permission")
            {
                pendingPermissions--;
            }

            // Calculate available permissions.
            int usedPermissions = getPermissionUsed(requiredUserID);
            int availablePermissions = maxMonthlyPermissions - usedPermissions;
            availableBalanceLabel.Text = availablePermissions.ToString();

            // Set the requested days label.
            daysRequestedLabel.Text = requestedPermissions.ToString();

            // Compute remaining balance.
            int remainingBalance = availablePermissions - pendingPermissions;
            remainingBalanceLabel.Text = remainingBalance.ToString();

            // Update pending labels.
            pendingLabel.Visible = true;
            pendingDaysLabel.Text = pendingPermissions.ToString();
            pendingDaysLabel.Visible = true;
        }
        private void updateLeaveBalanceLabel()
        {
            DateTime toDate = Convert.ToDateTime(toDayDatePicker.Value).Date;
            DateTime fromDate = Convert.ToDateTime(fromDayDatePicker.Value).Date;

            updateRequestedDaysLabel(toDate, fromDate);

            if (dayoffRadioButton.Checked)
            {
                updateRequestedDaysLabel(toDate, fromDate);

                if (annualRadioButton.Checked)
                {
                    pendingDays = getPendingDaysCount(requiredUserID, "Annual");
                    updateLabelsForDayOffBalance("AnnualBalance", requiredUserID, numberOfDaysRequested);

                    pendingLabel.Visible = true;
                    pendingDaysLabel.Visible = true;
                }
                else if (casualRadioButton.Checked)
                {
                    pendingDays = getPendingDaysCount(requiredUserID, "Emergency");
                    updateLabelsForDayOffBalance("CasualBalance", requiredUserID, numberOfDaysRequested);
                    pendingLabel.Visible = true;
                    pendingDaysLabel.Visible = true;
                }
                else
                {
                    balanceLabel.Visible = false;
                    availableBalanceLabel.Visible = false;
                    remainingLabel.Visible = false;
                    remainingBalanceLabel.Visible = false;
                    pendingLabel.Visible = false;
                    pendingDaysLabel.Visible = false;
                }
            }
            else if (exitRadioButton.Checked)
            {

                updateLabelsForPermissionBalance();
            }
        }
        private void updateRequestedDaysLabel(DateTime toDate, DateTime fromDate)
        {
            numberOfDaysRequested = getWorkingDays(fromDate, toDate);
            daysRequestedLabel.Text = numberOfDaysRequested.ToString();
        }
        private void setDaysLabelLocation()
        {
            bool isExit = exitRadioButton.Checked;

            daysLabel.Text = isExit ? "Permissions requested:" : "Days requested:";
            daysLabel.Location = isExit ? new Point(5, 350) : new Point(40, 350);

            pendingLabel.Text = isExit ? "Pending Permissions:" : "Pending Days:";
            pendingLabel.Location = isExit ? new Point(10, 304) : new Point(48, 304);
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
            pendingLabel.Visible = isVisible;
            pendingDaysLabel.Visible = isVisible;
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
        private async void addNewRequestAsync(string requestType)
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
                fromDayDatePicker.Value.Date,
                toDayDatePicker.Value.Date,
                loggedInUser.userID,
                loggedInUser.Department,
                numberOfDaysRequested,
                fromTime,
                toTime
            );

            newRequest.add();

            //await SignalRManager.HubProxy.Invoke("NotifyNewRequestWithDetails", newRequest.RequestDepartment, newRequest.RequestID, newRequest.RequestUserFullName, newRequest.RequestType, newRequest.RequestFromDay.ToString("yyyy-MM-dd"), newRequest.RequestStatus);

            // Notify via SignalR
        }
        private void updateExistingRequest(string requestType)
        {
            requestToEdit.RequestType = requestType;
            requestToEdit.RequestReason = reasonTextBox.Text;
            requestToEdit.RequestFromDay = fromDayDatePicker.Value;
            requestToEdit.RequestToDay = dayoffRadioButton.Checked || workFromHomeRadioButton.Checked ? toDayDatePicker.Value : (DateTime?)null;
            requestToEdit.RequestBeginningTime = requestType == "Permission" || requestType == "External Assignment" ? Convert.ToDateTime(fromTimeTextBox.Text) : (DateTime?)null;
            requestToEdit.RequestEndingTime = requestType == "Permission" || requestType == "External Assignment" ? Convert.ToDateTime(toTimeTextBox.Text) : (DateTime?)null;
            requestToEdit.RequestNumberOfDays = requestType == "Permission" || requestType == "External Assignment" ? 0 : numberOfDaysRequested;
            requestToEdit.update();
        }
        private string determineRequestType()
        {
            return exitRadioButton.Checked
                ? "Permission"
                : workFromHomeRadioButton.Checked
                ? "Work From Home"
                : dayoffRadioButton.Checked
                ? (annualRadioButton.Checked ? "Annual" : casualRadioButton.Checked ? "Emergency" : "Unpaid")
                : "External Assignment";
        }
        #endregion

        #region Buttons
        private async void submitButton_Click(object sender, EventArgs e)
        {

            int remainingBalance = int.TryParse(remainingBalanceLabel.Text, out int rBalance) ? rBalance : 0;
            int daysRequested = int.TryParse(daysRequestedLabel.Text, out int requested) ? requested : 0;

            // Validate inputs for time-based requests
            if (exitRadioButton.Checked || externalAssignmentRadioButton.Checked)
            {
                if (!validateTimeInputs(fromTimeTextBox.Text, toTimeTextBox.Text, out DateTime from, out DateTime to))
                    return;

                if (from >= to)
                {
                    MessageBox.Show("Ending time can't be earlier than or equal to the beginning time.", "Invalid Request");
                    return;
                }

                if (exitRadioButton.Checked)
                {

                    if (pendingPermissions >= 2)
                    {
                        MessageBox.Show("You have already applied for 2 Permissions this month.", "Insufficient Balance");
                        return;
                    }

                    if (DateTime.TryParse(fromTimeTextBox.Text, out DateTime fromTime) &&
                        DateTime.TryParse(toTimeTextBox.Text, out DateTime toTime))
                    {
                        // Check if the time difference exceeds 2 hours
                        if (toTime - fromTime > TimeSpan.FromHours(2))
                        {
                            MessageBox.Show("You can't apply for permission for more than 2 hours.", "Not Allowed");
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please enter valid time values in both fields.", "Invalid Request");
                        return;
                    }
                }

            }
            // Validate inputs for day-based requests
            else
            {
                if (!validateDayInputs(fromDayDatePicker.Text, toDayDatePicker.Text, out DateTime fromDay, out DateTime toDay))
                    return;

                if (fromDay > toDay)
                {
                    MessageBox.Show("Ending date can't be earlier than the beginning date.", "Invalid Request");
                    return;
                }
               else if (daysRequested <= 0)
                {
                    MessageBox.Show("You cannot apply for leave on Friday or Saturday.", "Invalid Request");
                    return;
                }
            }
            if (dayoffRadioButton.Checked)
            {
                if (annualRadioButton.Checked)
                {
                    if (requested > rBalance)
                    {
                        MessageBox.Show("You don't have enough Annual leave balance.", "Insufficient Balance");
                        return;
                    }
                }

                if (casualRadioButton.Checked)
                {
                    if (requested > rBalance)
                    {
                        MessageBox.Show("You don't have enough Emergency leave balance.", "Insufficient Balance");
                        return;
                    }
                }
            }
            // Determine request type
            string requestType = determineRequestType();

            // Create or update request
            if (requestToEdit == null)
            {
                addNewRequestAsync(requestType);

            }
            else
            {
                updateExistingRequest(requestType);
            }

            if (requestAddedOrUpdated && (requestType == "Annual" || requestType == "Emergency"))
            {
                
                pendingDays += numberOfDaysRequested;
                pendingDaysLabel.Text = pendingDays.ToString();
                remainingBalanceLabel.Text = (remainingBalance - numberOfDaysRequested).ToString();
            }
            else if (requestAddedOrUpdated && requestType == "Permission")
            {
                pendingPermissions++;
                pendingDaysLabel.Text = pendingPermissions.ToString();
                remainingBalanceLabel.Text = (remainingBalance - 1).ToString();
            }
            requestAddedOrUpdatedEvent?.Invoke();
            requestAddedOrUpdated = false;

        }
        #endregion
    }
}
