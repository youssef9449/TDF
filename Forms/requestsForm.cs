using Bunifu.UI.WinForms;
using Microsoft.Office.Interop.Excel;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using TDF.Net.Classes;
using static TDF.Net.Forms.addRequestForm;
using static TDF.Net.loginForm;
using static TDF.Net.mainForm;
using DataTable = System.Data.DataTable;
using Excel = Microsoft.Office.Interop.Excel;
using Point = System.Drawing.Point;
using Rectangle = System.Drawing.Rectangle;

namespace TDF.Net.Forms
{
    public partial class requestsForm : Form
    {
        public requestsForm(bool isModern)
        {
            InitializeComponent();

            Program.applyTheme(this);
            StartPosition = FormStartPosition.CenterScreen;

            if (isModern)
            {
                controlBox.Visible = !isModern;
                panel.MouseDown += new MouseEventHandler(panel_MouseDown);
                panel.Paint += panel_Paint;
                //Width += 500;
            }
            else
            {
                panel.Visible = isModern;
            }
        }

        private Timer requestsRefreshTimer;
        private bool requestNoteEdited = false;
        public static Request selectedRequest = null;

        #region Events
        private void requestsForm_Load(object sender, EventArgs e)
        {
            applyButton.Visible = hasManagerRole || hasAdminRole || hasHRRole;

            refreshRequestsTable();

            requestsRefreshTimer = new Timer();
            requestsRefreshTimer.Interval = 15000; // 15 seconds
            requestsRefreshTimer.Tick += requestsRefreshTimer_Tick;
            requestsRefreshTimer.Start();
        }
        private void requestsDataGridView_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string columnName = requestsDataGridView.Columns[e.ColumnIndex].Name;

                if (columnName == "Edit" || columnName == "Remove" || columnName == "Report")
                {
                    requestsDataGridView.Cursor = Cursors.Hand;
                }
            }
        }
        private void requestsDataGridView_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            requestsDataGridView.Cursor = Cursors.Default;
        }
        private void requestsForm_Resize(object sender, EventArgs e)
        {
            Invalidate();
        }
        private void requestsDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 & e.RowIndex >= 0)
            {
                if (requestsDataGridView.Columns[e.ColumnIndex].Name == "Edit")
                {
                    string requestStatus = requestsDataGridView.Rows[e.RowIndex].Cells["RequestStatus"].Value.ToString();
                    string requestHRStatus = requestsDataGridView.Rows[e.RowIndex].Cells["RequestHRStatus"].Value.ToString();
                    string requestUserFullName = requestsDataGridView.Rows[e.RowIndex].Cells["RequestUserFullName"].Value.ToString();
                    bool isRequestOwner = requestUserFullName == loggedInUser.FullName;

                    // Allow editing if the request is "Pending" or the user has an elevated role
                    if (requestStatus == "Pending" || requestHRStatus == "Pending" || hasAdminRole || hasManagerRole || hasHRRole)
                    {
                        // Prevent HR users from editing another user's request unless they have Admin/Manager roles
                        if (!isRequestOwner && hasHRRole && !hasAdminRole && !hasManagerRole)
                        {
                            MessageBox.Show("You are not allowed to edit another user's request.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        if ((requestStatus != "Pending" || requestHRStatus != "Pending") && !hasAdminRole)
                        {
                            MessageBox.Show("You are not allowed to edit an approved request.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        // Check if the clicked cell is an image button before opening the edit form
                        if (requestsDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex] is DataGridViewImageCell)
                        {
                            openRequestToEdit(e);
                        }
                    }
                }

                if (requestsDataGridView.Columns[e.ColumnIndex].Name == "Remove")
                {
                    if (requestsDataGridView.Rows[e.RowIndex].Cells["RequestStatus"].Value.ToString() == "Pending")
                    {
                        if (requestsDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex] is DataGridViewImageCell imageCell)
                        {
                            if (requestsDataGridView.Rows[e.RowIndex].Cells["RequestUserFullName"].Value.ToString() == loggedInUser.FullName)
                            {
                                DialogResult confirmResult = MessageBox.Show("Are you sure you want to delete this request?",
                                                                             "Confirm Delete",
                                                                              MessageBoxButtons.YesNo);

                                if (confirmResult == DialogResult.Yes)
                                {
                                    Request RequestToDelete = new Request
                                    {
                                        RequestID = Convert.ToInt32(requestsDataGridView.Rows[e.RowIndex].Cells["RequestID"].Value)
                                    };

                                    RequestToDelete.delete();

                                    requestsDataGridView.Rows.RemoveAt(e.RowIndex);
                                }
                            }
                            else
                            {
                                MessageBox.Show("You are not allowed to remove another user's request.");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("The request is locked. You cannot perform this action.");
                    }
                }

                if (requestsDataGridView.Columns[e.ColumnIndex].Name == "Report")
                {
                    DataGridViewRow currentRow = requestsDataGridView.Rows[e.RowIndex];

                    string requestType = currentRow.Cells["RequestType"].Value?.ToString();

                    DateTime? beginningDate = DateTime.TryParse(currentRow.Cells["RequestFromDay"].Value?.ToString(), out DateTime parsedBeginningDate)
                        ? parsedBeginningDate
                        : (DateTime?)null;

                    DateTime? endingDate = DateTime.TryParse(currentRow.Cells["RequestToDay"].Value?.ToString(), out DateTime parsedEndingDate)
                        ? parsedEndingDate
                        : (DateTime?)null;

                    int numberOfDays = int.TryParse(currentRow.Cells["NumberOfDays"].Value?.ToString(), out int result) ? result : 0;
                    int availableBalance = int.TryParse(currentRow.Cells["RemainingBalance"].Value?.ToString(), out int balance) ? balance : 0;
                    string reason = currentRow.Cells["RequestReason"].Value?.ToString() ?? string.Empty;
                    string beginningTime = currentRow.Cells["RequestBeginningTime"].Value?.ToString() ?? string.Empty;
                    string endingTime = currentRow.Cells["RequestEndingTime"].Value?.ToString() ?? string.Empty;
                    string managerApprovalStatus = currentRow.Cells["RequestStatus"].Value?.ToString() ?? string.Empty;
                    string hrApprovalStatus = currentRow.Cells["RequestHRStatus"].Value?.ToString() ?? string.Empty;

                    if (managerApprovalStatus == "Rejected")
                    {
                        MessageBox.Show("Generating a report for a rejected request is not allowed.");
                        return;
                    }

                    if (currentRow.Cells["RequestUserFullName"].Value?.ToString() != loggedInUser.FullName)
                    {
                        MessageBox.Show("Generating a report for another user's request is not allowed.");
                        return;
                    }
                    else
                    {
                        createPDF(requestType, beginningDate, endingDate, numberOfDays, availableBalance, reason, beginningTime, endingTime, managerApprovalStatus, hrApprovalStatus);
                    }
                }
            }
        }
        private void requestsDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (requestsDataGridView.Columns[e.ColumnIndex].Name == "RequestStatus" || 
                requestsDataGridView.Columns[e.ColumnIndex].Name == "RequestHRStatus")
            {
                if (e.Value != null && e.Value.ToString() == "Approved")
                {
                    e.CellStyle.ForeColor = Color.Green;
                }
                else if (e.Value != null && e.Value.ToString() == "Rejected")
                {
                    e.CellStyle.ForeColor = Color.Red;
                }
                else
                {
                    e.CellStyle.ForeColor = Color.Blue;
                }
            }
            if (requestsDataGridView.Columns[e.ColumnIndex].Name == "RequestBeginningTime" ||
                requestsDataGridView.Columns[e.ColumnIndex].Name == "RequestEndingTime")
            {
                if (e.Value != DBNull.Value && e.Value != null || !string.IsNullOrEmpty(e.Value.ToString()))
                {
                    // Convert TimeSpan to DateTime to apply AM/PM format
                    TimeSpan timeValue = (TimeSpan)e.Value;
                    DateTime dateTime = DateTime.Today.Add(timeValue); // Use today's date with the TimeSpan
                    e.Value = dateTime.ToString("hh:mm tt"); // Format in 12-hour format with AM/PM
                }
            }
            if (requestsDataGridView.Columns[e.ColumnIndex].Name == "RequestReason")
            {
                if (e.Value == null || string.IsNullOrEmpty(e.Value.ToString()))
                {
                    e.Value = "-";
                    e.FormattingApplied = true;
                }
            }
            if (requestsDataGridView.Columns[e.ColumnIndex].Name == "NumberOfDays")
            {
                if ((int)e.Value == 0 || string.IsNullOrEmpty(e.Value.ToString()))
                {
                    e.Value = "-";
                    e.FormattingApplied = true;
                }
            }
        }
        private void pendingRadioButton_CheckedChanged(object sender, BunifuRadioButton.CheckedChangedEventArgs e)
        {
            refreshRequestsTable();
        }
        private void requestsDataGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            // Ensure the column is either 'Approve' or 'Reject' and the row is valid
            if (requestsDataGridView.Columns[e.ColumnIndex].Name == "Approve" || requestsDataGridView.Columns[e.ColumnIndex].Name == "Reject")
            {
                DataGridViewRow currentRow = requestsDataGridView.Rows[e.RowIndex];
                string requestUserFullName = currentRow.Cells["RequestUserFullName"].Value?.ToString();

                // If the logged-in user tries to approve or reject their own request
                if (requestUserFullName == loggedInUser.FullName)
                {
                    // Cancel the edit action immediately
                    e.Cancel = true;
                    MessageBox.Show("You can't approve or reject your own request.");
                }
                else
                {
                    // If not the logged-in user, handle the logic normally
                    if (requestsDataGridView.Columns[e.ColumnIndex].Name == "Approve")
                    {
                        currentRow.Cells["Reject"].Value = false;  // Uncheck 'Reject'
                    }
                    else if (requestsDataGridView.Columns[e.ColumnIndex].Name == "Reject")
                    {
                        currentRow.Cells["Approve"].Value = false;  // Uncheck 'Approve'
                    }
                }
            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Get the form's scroll position
           Point scrollPos = AutoScrollPosition;

            // Adjust for scroll position when drawing the border
            Rectangle rect = new Rectangle(ClientRectangle.X - scrollPos.X, ClientRectangle.Y - scrollPos.Y, ClientRectangle.Width, ClientRectangle.Height);

            ControlPaint.DrawBorder(e.Graphics, rect, ThemeColor.darkColor, ButtonBorderStyle.Solid);
        }
        private void panel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                ReleaseCapture();
                SendMessage(Handle, 0x112, 0xf012, 0);
            }
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Invalidate();  // Forces the form to repaint when resized
        }
        private void panel_Paint(object sender, PaintEventArgs e)
        {
            base.OnPaint(e);

            // Get the form's scroll position
            Point scrollPos = AutoScrollPosition;

            // Adjust for scroll position when drawing the border
            Rectangle rect = new Rectangle(ClientRectangle.X - scrollPos.X, ClientRectangle.Y - scrollPos.Y, ClientRectangle.Width, ClientRectangle.Height);

            ControlPaint.DrawBorder(e.Graphics, rect, ThemeColor.darkColor, ButtonBorderStyle.Solid);
        }
        private void requestsRefreshTimer_Tick(object sender, EventArgs e)
        {
            bool anyChecked = false;

            // Loop through each row to see if any "Approve" or "Reject" checkbox is checked.
            foreach (DataGridViewRow row in requestsDataGridView.Rows)
            {
                // Check the "Approve" checkbox.
                if (row.Cells["Approve"].Value is bool approveValue && approveValue)
                {
                    anyChecked = true;
                    break;
                }

                // Check the "Reject" checkbox.
                if (row.Cells["Reject"].Value is bool rejectValue && rejectValue)
                {
                    anyChecked = true;
                    break;
                }
            }

            // Check if the current cell is in edit mode in the "RequestRejectReason" column.
            bool editingNotes = false;
            if (requestsDataGridView.IsCurrentCellInEditMode &&
                requestsDataGridView.CurrentCell != null &&
                requestsDataGridView.CurrentCell.OwningColumn.Name == "RequestRejectReason")
            {
                editingNotes = true;
            }

            // Only refresh if:
            // 1. No "Approve" or "Reject" checkboxes are checked.
            // 2. The user is not currently editing the "RequestRejectReason" column.
            // 3. No "RequestRejectReason" cell has been edited (and remains unsaved) as per our flag.
            if (!anyChecked && !editingNotes && !requestNoteEdited)
            {
                refreshRequestsTablePreserveState();
            }
        }
        private void requestsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (requestsRefreshTimer != null)
            {
                requestsRefreshTimer.Stop();
                requestsRefreshTimer.Dispose();
            }
        }
        private void requestsDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // Check if the changed cell is in the "RequestRejectReason" column.
            if (requestsDataGridView.Columns[e.ColumnIndex].Name == "RequestRejectReason")
            {
                requestNoteEdited = true;
            }
        }
        #endregion

        #region Methods
        public void refreshRequestsTable()
        {
            DataTable requestsTable = new DataTable();

            try
            {
                if (hasManagerRole || hasAdminRole || hasHRRole)
                {
                    loadRequestsForManagerOrAdminOrHR(requestsTable);
                    configureDataGridViewForManagerOrAdminOrHR();
                }
                else
                {
                    loadRequestsForUser(requestsTable);
                    configureDataGridViewForUser();
                }

                requestsDataGridView.DataSource = requestsTable;
                adjustRemainingBalanceForPermissions();
                reorderDataGridViewColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void loadRequestsForManagerOrAdminOrHR(DataTable requestsTable)
        {
            string query = buildQueryForManagerOrAdminORHR();
            executeQuery(query, requestsTable);
        }
        private void loadRequestsForUser(DataTable requestsTable)
        {
            string query = buildQueryForUser();
            executeQuery(query, requestsTable, cmd =>
            {
                cmd.Parameters.AddWithValue("@UserID", loggedInUser.userID);
            });
        }
        private string buildQueryForManagerOrAdminORHR()
        {
            string baseQuery = @"SELECT 
            r.RequestID, 
            r.RequestUserFullName, 
            r.RequestType, 
            r.RequestReason, 
            r.RequestFromDay, 
            r.RequestToDay, 
            r.RequestBeginningTime, 
            r.RequestEndingTime, 
            r.RequestStatus, 
            r.RequestRejectReason, 
            r.RequestNumberOfDays,
            r.RequestHRStatus,
            CASE 
                WHEN r.RequestType = 'Annual' THEN al.AnnualBalance
                WHEN r.RequestType = 'Emergency' THEN al.CasualBalance
                WHEN r.RequestType = 'Permission' THEN al.PermissionsBalance
                ELSE NULL
            END AS remainingBalance
        FROM 
            Requests r
        LEFT JOIN 
            AnnualLeave al ON r.RequestUserID = al.UserID ";

            string condition="";

            if (hasHRRole)
            {
                 condition = pendingRadioButton.Checked
                    ? "WHERE r.RequestHRStatus = 'Pending'"
                    : "WHERE NOT r.RequestHRStatus = 'Pending'";
            }
            if (hasManagerRole)
            {
                 condition = pendingRadioButton.Checked
                    ? "WHERE r.RequestStatus = 'Pending'"
                    : "WHERE NOT r.RequestStatus = 'Pending'";
            }
            if (hasAdminRole)
            {
                condition = pendingRadioButton.Checked
                   ? "WHERE r.RequestStatus = 'Pending' OR r.RequestHRStatus = 'Pending'"
                   : "WHERE NOT r.RequestStatus = 'Pending' And NOT r.RequestHRStatus = 'Pending'";
            }
            if (!hasAdminRole && !hasHRRole)
            {
                // Split the manager's departments and build conditions dynamically
                string[] departments = loggedInUser.Department.Split(new string[] { " - " }, StringSplitOptions.RemoveEmptyEntries);
                string departmentCondition = string.Join(" OR ", departments.Select(d => $"r.RequestDepartment LIKE '%{d.Trim()}%'"));
                condition += $" AND ({departmentCondition})";
                //condition += $" AND NOT RequestUserFullName = '{loggedInUser.FullName}'";
            }

            return baseQuery + condition;
        }
        private string buildQueryForUser()
        {
            return @"
        SELECT 
            r.RequestID, 
            r.RequestUserFullName, 
            r.RequestType, 
            r.RequestReason, 
            r.RequestFromDay, 
            r.RequestToDay, 
            r.RequestBeginningTime, 
            r.RequestEndingTime, 
            r.RequestStatus, 
            r.RequestRejectReason, 
            r.RequestNumberOfDays,
            r.RequestHRStatus,
            CASE 
                WHEN r.RequestType = 'Annual' THEN al.AnnualBalance
                WHEN r.RequestType = 'Emergency' THEN al.CasualBalance
                WHEN r.RequestType = 'Permission' THEN al.PermissionsBalance
                ELSE NULL
            END AS remainingBalance
        FROM 
            Requests r
        LEFT JOIN 
            AnnualLeave al ON r.RequestUserID = al.UserID
        WHERE 
            r.RequestUserID = @UserID 
            AND (" + (pendingRadioButton.Checked ? " (r.RequestStatus = 'Pending' OR r.RequestHRStatus = 'Pending'))" : " NOT (r.RequestStatus = 'Pending' OR r.RequestHRStatus = 'Pending'))");
        }
        private void executeQuery(string query, DataTable requestsTable, Action<SqlCommand> parameterizeCommand = null)
        {
            using (SqlConnection conn = Database.getConnection())
            {
                conn.Open();
                string orderByClause = " ORDER BY RequestUserFullName ASC, RequestFromDay ASC";

                using (SqlCommand cmd = new SqlCommand(query + orderByClause, conn))
                {
                    parameterizeCommand?.Invoke(cmd);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(requestsTable);
                    }
                }
            }
        }
        private void configureDataGridViewForManagerOrAdminOrHR()
        {
            requestsDataGridView.Columns["RequestUserFullName"].Visible = true;

            requestsDataGridView.Columns["Edit"].Visible = pendingRadioButton.Checked;
            //requestsDataGridView.Columns["RequestUserFullName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet;
          //  requestsDataGridView.Columns["RequestUserFullName"].MinimumWidth += 25;
           // requestsDataGridView.Columns["RequestUserFullName"].Width += 25;

            /*requestsDataGridView.Columns["Edit"].Visible = !hasAdminRole && pendingRadioButton.Checked;
              requestsDataGridView.Columns["Remove"].Visible = false;

             if (requestsDataGridView.Columns["Report"] != null)
             {
                 requestsDataGridView.Columns["Report"].Visible = false;
             }*/
            requestsDataGridView.Columns["RequestRejectReason"].ReadOnly = false;
        }
        private void configureDataGridViewForUser()
        {
            bool isPending = pendingRadioButton.Checked;
            requestsDataGridView.Columns["Approve"].Visible = false;
            requestsDataGridView.Columns["Reject"].Visible = false;
            requestsDataGridView.Columns["Edit"].Visible = isPending;
            requestsDataGridView.Columns["Remove"].Visible = isPending;
            requestsDataGridView.Columns["RequestRejectReason"].Visible = !isPending;
            requestsDataGridView.Columns["RequestRejectReason"].ReadOnly = true;
        }
        private void openRequestToEdit(DataGridViewCellEventArgs e)
        {
            // Get the underlying data row from the DataGridView
            DataRow row = ((DataRowView)requestsDataGridView.Rows[e.RowIndex].DataBoundItem).Row;

            // Retrieve the request data from the DataRow
            DateTime requestFromDay = Convert.ToDateTime(row["RequestFromDay"]);

            selectedRequest = new Request();

            selectedRequest.RequestUserFullName = row["RequestUserFullName"] != DBNull.Value ? row["RequestUserFullName"].ToString() : string.Empty;
            selectedRequest.RequestID = row["RequestID"] != DBNull.Value ? Convert.ToInt32(row["RequestID"]) : 0;
            selectedRequest.RequestUserID = row["RequestID"] != DBNull.Value ? getSelectedRequestUserID() : 0;
            selectedRequest.RequestType = row["RequestType"] != DBNull.Value ? row["RequestType"].ToString() : string.Empty;
            selectedRequest.RequestReason = row["RequestReason"] != DBNull.Value ? row["RequestReason"].ToString() : string.Empty;
            selectedRequest.RequestFromDay = requestFromDay;
            selectedRequest.RequestToDay = row["RequestToDay"] != DBNull.Value ? Convert.ToDateTime(row["RequestToDay"]) : requestFromDay;

            // Converting TimeSpan to DateTime by adding the TimeSpan to the requestFromDay
            selectedRequest.RequestBeginningTime = row["RequestBeginningTime"] != DBNull.Value ?
                                                   requestFromDay.Add((TimeSpan)row["RequestBeginningTime"]) : requestFromDay;

            selectedRequest.RequestEndingTime = row["RequestEndingTime"] != DBNull.Value ?
                                                requestFromDay.Add((TimeSpan)row["RequestEndingTime"]) : requestFromDay;

            selectedRequest.RequestStatus = row["RequestStatus"] != DBNull.Value ? row["RequestStatus"].ToString() : string.Empty;


            // Open the AddRequestForm with the selected request data for editing
            addRequestForm addRequestForm = new addRequestForm(selectedRequest); 
            addRequestForm.ShowDialog();

            if (requestAddedOrUpdated)
            {
                refreshRequestsTable();
            }
        }
        private void reorderDataGridViewColumns()
        {
            requestsDataGridView.Columns["RequestHRStatus"].DisplayIndex = requestsDataGridView.Columns.Count - 1;
            requestsDataGridView.Columns["Edit"].DisplayIndex = requestsDataGridView.Columns.Count - 1;
            requestsDataGridView.Columns["Remove"].DisplayIndex = requestsDataGridView.Columns.Count - 1;
            requestsDataGridView.Columns["Report"].DisplayIndex = requestsDataGridView.Columns.Count - 1;
            requestsDataGridView.Columns["RequestStatus"].DisplayIndex = requestsDataGridView.Columns.Count - 4;
            requestsDataGridView.Columns["RequestRejectReason"].DisplayIndex = requestsDataGridView.Columns.Count - 4;
            requestsDataGridView.Columns["Approve"].DisplayIndex = requestsDataGridView.Columns.Count - 1;
            requestsDataGridView.Columns["Reject"].DisplayIndex = requestsDataGridView.Columns.Count - 1;
            requestsDataGridView.Columns["RequestReason"].DisplayIndex = 7;
        }
        private int getMonthlyPermissionsCount(int userID, DateTime requestDate)
        {
            try
            {
                int month = requestDate.Month;
                int year = requestDate.Year;

                using (SqlConnection conn = Database.getConnection())
                {
                    conn.Open();

                    string query = @"SELECT COUNT(*) FROM Requests 
                             WHERE RequestType = @RequestType 
                             AND RequestUserID = @RequestUserID 
                             AND MONTH(RequestFromDay) = @Month 
                             AND YEAR(RequestFromDay) = @Year AND RequestStatus = 'Approved' AND RequestHRStatus = 'Approved'";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@RequestType", "Permission");
                        cmd.Parameters.AddWithValue("@RequestUserID", userID);
                        cmd.Parameters.AddWithValue("@Month", month);
                        cmd.Parameters.AddWithValue("@Year", year);

                        object result = cmd.ExecuteScalar();
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

            return 0;
        }
        private void adjustRemainingBalanceForPermissions()
        {
            foreach (DataGridViewRow row in requestsDataGridView.Rows)
            {
                if (row.Cells["RequestType"].Value?.ToString() == "Permission")
                {
                    int userID = loggedInUser.userID;
                    DateTime requestDate = Convert.ToDateTime(row.Cells["RequestFromDay"].Value);
                    int monthlyCount = getMonthlyPermissionsCount(userID, requestDate);
                    int adjustedBalance = 2 - monthlyCount;

                    row.Cells["remainingBalance"].Value = adjustedBalance;
                }
            }
        }


        private (string managerName, string managerDepartment) getManagerName()
        {
            string managerName = string.Empty;
            string managerDepartment = string.Empty;

            string query = "SELECT TOP 1 FullName, Department FROM Users WHERE Department LIKE @Department AND Role = @Role";

            try
            {
                using (SqlConnection connection = Database.getConnection())
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Department", "%" + loggedInUser.Department + "%");
                        command.Parameters.AddWithValue("@Role", "Manager");

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read()) // Only take the first result
                            {
                                managerName = reader["FullName"].ToString();
                                managerDepartment = reader["Department"].ToString();
                                return (managerName, managerDepartment);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }

            return (managerName, managerDepartment);
        }
        private string getHRDirectorName()
        {
            string hrDirectorName = string.Empty;

            string query = "SELECT TOP 1 FullName FROM Users WHERE Title = @Title";

            try
            {
                using (SqlConnection connection = Database.getConnection())
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Title", "HR Director");

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read()) // Only take the first result
                            {
                                hrDirectorName = reader["FullName"].ToString();
                                return hrDirectorName;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }

            return hrDirectorName;
        }
        public (int Annual, int Casual, int AnnualUsed, int CasualUsed) getLeaveBalances()
        {
            string query = @"SELECT Annual, CasualLeave, AnnualUsed, CasualUsed 
                             FROM AnnualLeave 
                             WHERE UserID = @UserID";

            using (SqlConnection conn = Database.getConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", loggedInUser.userID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int annual = reader.GetInt32(reader.GetOrdinal("Annual"));
                            int casual = reader.GetInt32(reader.GetOrdinal("CasualLeave"));
                            int annualUsed = reader.GetInt32(reader.GetOrdinal("AnnualUsed"));
                            int casualUsed = reader.GetInt32(reader.GetOrdinal("CasualUsed"));

                            return (annual, casual, annualUsed, casualUsed);
                        }
                    }
                }
            }

            // If no record found, return defaults
            return (0, 0, 0, 0);
        }
        public int getPermissionUsed()
        {
            string query = "SELECT PermissionsUsed FROM AnnualLeave WHERE UserID = @UserID";

            using (var conn = Database.getConnection())
            using (var cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@UserID", loggedInUser.userID);
                conn.Open();

                return (int?)cmd.ExecuteScalar() ?? 0;
            }
        }
        public int getSelectedRequestUserID()
        {
            string query = "SELECT RequestUserID FROM Requests WHERE RequestID = @RequestID";

            using (var conn = Database.getConnection())
            using (var cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@RequestID", selectedRequest.RequestID);
                conn.Open();

                return (int?)cmd.ExecuteScalar() ?? 0;
            }
        }
        private void createPDF(string requestType, DateTime? beginningDate, DateTime? endingDate, int numberOfDays, int availableBalance, string reason, string beginningTime, string endingTime, string status, string hrStatus)
        {
            string filePath = string.Empty;

            if (requestType == "Annual" || requestType == "Emergency" || requestType == "Unpaid")
            {
                filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Forms", "Leave.xlsx");
            }
            else if (requestType == "Permission")
            {
                filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Forms", "Permission.xlsx");
            }
            else
            {
                filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Forms", "Work From Home_Business Trip.xlsx");
            }

            Excel.Application excelApp = null;
            Workbook workbook = null;
            Worksheet worksheet = null;

            try
            {
                excelApp = new Excel.Application
                {
                    Visible = false,
                    DisplayAlerts = false
                };

                workbook = excelApp.Workbooks.Open(filePath, ReadOnly: false, IgnoreReadOnlyRecommended: true);
                worksheet = workbook.Worksheets[1]; // Assuming data will be written to the first worksheet

                (string managerName, string managerDepartment) = getManagerName();
                string hrDirectorName = getHRDirectorName();

                // Write the common data for all request types
                worksheet.Cells[2, 3].Value = beginningDate;
                worksheet.Cells[3, 3].Value = loggedInUser.FullName;
                worksheet.Cells[5, 3].Value = loggedInUser.Department;
                worksheet.Cells[4, 3].Value = loggedInUser.Title;
                worksheet.Cells[6, 3].Value = hasManagerRole || hasAdminRole ? "Hala Ibrahim" : managerName;
                worksheet.Cells[7, 3].Value = hasManagerRole || hasAdminRole ? "Founder & CEO" : managerDepartment + " Department";

                if (requestType == "Annual" || requestType == "Emergency" || requestType == "Unpaid")
                {
                    (int Annual, int Casual, int AnnualUsed, int CasualUsed) = getLeaveBalances();

                    worksheet.Columns[10].ClearContents();
                    worksheet.Cells[16, 3].Value = beginningDate;
                    worksheet.Cells[17, 3].Value = endingDate;


                    if (requestType == "Annual")
                    {
                        /*if (AnnualUsed + numberOfDays >= Annual)
                        {
                            worksheet.Cells[13, 10].Value = "TRUE";
                        }
                        else
                        {
                            worksheet.Cells[10, 10].Value = "TRUE";
                        }*/
                        worksheet.Cells[10, 10].Value = "TRUE";
                        worksheet.Cells[23, 2].Value = Annual;
                        worksheet.Cells[23, 3].Value = AnnualUsed;
                    }
                    else if (requestType == "Emergency")
                    {
                        worksheet.Cells[12, 10].Value = "TRUE";
                        worksheet.Cells[23, 2].Value = Casual;
                        worksheet.Cells[23, 3].Value = CasualUsed;
                    }
                    else
                    {
                        worksheet.Cells[13, 10].Value = "TRUE";

                    }

                    worksheet.Cells[8, 10].Value = numberOfDays;
                    worksheet.Cells[26, 4].Value = status == "Approved" ? worksheet.Cells[6, 3].Value : "";
                    worksheet.Cells[27, 4].Value = hrStatus == "Approved" ? hrDirectorName : "";

                }
                else if (requestType == "Permission")
                {
                    worksheet.Columns[9].ClearContents();
                    worksheet.Cells[11, 3].Value = beginningTime;
                    worksheet.Cells[11, 5].Value = endingTime;
                    worksheet.Cells[13, 3].Value = beginningDate;
                    worksheet.Cells[16, 2].Value = reason;
                    worksheet.Cells[19, 9].Value = getPermissionUsed();

                    worksheet.Cells[22, 4].Value = status == "Approved" ? worksheet.Cells[6, 3].Value : "";
                    worksheet.Cells[23, 4].Value = hrStatus == "Approved" ? hrDirectorName : "";

                }
                else if (requestType == "Work From Home" || requestType == "External Assignment") // "Work From Home_Business Trip"
                {
                    worksheet.Columns[9].ClearContents();
                    worksheet.Cells[19, 2].Value = reason;

                    if (requestType == "External Assignment")
                    {
                        worksheet.Cells[10, 9].Value = "TRUE";
                        worksheet.Cells[11, 3].Value = beginningTime;
                        worksheet.Cells[11, 5].Value = endingTime;
                        worksheet.Cells[12, 3].Value = beginningDate;
                    }
                    else
                    {
                        worksheet.Cells[13, 9].Value = "TRUE";
                        worksheet.Cells[14, 3].Value = beginningDate;
                        worksheet.Cells[15, 3].Value = endingDate;
                    }

                    worksheet.Cells[23, 4].Value = status == "Approved" ? worksheet.Cells[6, 3].Value : "";
                    worksheet.Cells[24, 4].Value = hrStatus == "Approved" ? hrDirectorName : "";
                }

                saveAsPdf(workbook, requestType, status);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error writing to the Excel file or saving as PDF: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cleanupExcel(excelApp, workbook, worksheet);
            }
        }
        private void saveAsPdf(Workbook workbook, string requestType, string status)
        {
            try
            {
                // Get the desktop path
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                // Get the current timestamp
                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss"); // Format: YYYYMMDD_HHmmss

                // Construct the PDF file name and path
                string pdfFileName = $"{loggedInUser.FullName}_{status}_{requestType}_{timestamp}.pdf";
                string pdfFilePath = Path.Combine(desktopPath, pdfFileName);

                // Export the worksheet to PDF
                workbook.ExportAsFixedFormat(
                    XlFixedFormatType.xlTypePDF,
                    pdfFilePath,
                    XlFixedFormatQuality.xlQualityStandard,
                    IncludeDocProperties: true,
                    IgnorePrintAreas: false,
                    OpenAfterPublish: false
                );

                MessageBox.Show($"File successfully saved on the Desktop as PDF: {pdfFilePath}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving the file as PDF: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void cleanupExcel(Excel.Application excelApp, Workbook workbook, Worksheet worksheet)
        {
            if (workbook != null)
            {
                workbook.Close(false);  // Close the workbook without saving changes
                Marshal.ReleaseComObject(workbook);
            }
            if (worksheet != null)
            {
                Marshal.ReleaseComObject(worksheet);
            }
            if (excelApp != null)
            {
                excelApp.Quit();  // Quit the Excel application
                Marshal.ReleaseComObject(excelApp);
            }
        }
        private void refreshRequestsTablePreserveState()
        {
            // 1. Save the current scroll position.
            int firstDisplayedIndex = requestsDataGridView.FirstDisplayedScrollingRowIndex;

            // 2. Save sorting information.
            DataGridViewColumn sortedColumn = requestsDataGridView.SortedColumn;
            System.ComponentModel.ListSortDirection sortDirection = ListSortDirection.Ascending;
            string sortColumnName = string.Empty;
            if (sortedColumn != null)
            {
                sortDirection = (requestsDataGridView.SortOrder == System.Windows.Forms.SortOrder.Ascending)
                                    ? ListSortDirection.Ascending
                                    : ListSortDirection.Descending;
                // Save the column's Name.
                sortColumnName = sortedColumn.Name;
            }

            // If using a BindingSource, prepare the sort string.
            BindingSource bs = requestsDataGridView.DataSource as BindingSource;
            string sortString = string.Empty;
            if (bs != null && !string.IsNullOrEmpty(sortColumnName))
            {
                // Assuming column Name equals its DataPropertyName.
                sortString = $"{sortColumnName} {(sortDirection == ListSortDirection.Ascending ? "ASC" : "DESC")}";
            }

            // 3. Save the currently selected row index.
            int selectedRowIndex = -1;
            if (requestsDataGridView.CurrentRow != null)
            {
                selectedRowIndex = requestsDataGridView.CurrentRow.Index;
            }

            // 4. Refresh the grid's data.
            refreshRequestsTable();

            // 5. Reapply the sort order.
            if (!string.IsNullOrEmpty(sortColumnName))
            {
                // Retrieve the updated column by its name.
                DataGridViewColumn newSortedColumn = requestsDataGridView.Columns[sortColumnName];
                if (newSortedColumn != null)
                {
                    if (bs != null)
                    {
                        bs.Sort = sortString;
                    }
                    else
                    {
                        requestsDataGridView.Sort(newSortedColumn, sortDirection);
                    }
                }
            }

            // 6. Restore the scroll position if it's still valid.
            if (firstDisplayedIndex >= 0 && firstDisplayedIndex < requestsDataGridView.Rows.Count)
            {
                requestsDataGridView.FirstDisplayedScrollingRowIndex = firstDisplayedIndex;
            }

            // 7. Restore the selected row.
            if (selectedRowIndex >= 0 && selectedRowIndex < requestsDataGridView.Rows.Count)
            {
                // Find a visible row starting from the saved index.
                int newIndex = selectedRowIndex;
                while (newIndex < requestsDataGridView.Rows.Count && !requestsDataGridView.Rows[newIndex].Visible)
                {
                    newIndex++;
                }
                if (newIndex < requestsDataGridView.Rows.Count)
                {
                    DataGridViewRow row = requestsDataGridView.Rows[newIndex];
                    DataGridViewCell cellToSelect = null;
                    // Look for the first visible cell in that row.
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (requestsDataGridView.Columns[cell.ColumnIndex].Visible)
                        {
                            cellToSelect = cell;
                            break;
                        }
                    }
                    if (cellToSelect != null)
                    {
                        try
                        {
                            requestsDataGridView.CurrentCell = cellToSelect;
                            row.Selected = true;
                        }
                        catch (Exception ex)
                        {
                            // Log or handle this error as needed.
                            MessageBox.Show("Error restoring selected cell: " + ex.Message);
                        }
                    }
                }
            }
        }
        private bool isHRDepartmentUser(SqlConnection conn, string userFullName)
        {
            string query = "SELECT Department FROM Users WHERE FullName = @FullName";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@FullName", userFullName);
                object result = cmd.ExecuteScalar();
                return result?.ToString() == "Human Resources";
            }
        }
        private void handleBalanceUpdate(SqlConnection conn, string currentHRStatus, string currentManagerStatus, string newStatus, string role, string requestType, int numberOfDays, string userFullName)
        {
            bool isHRRequestUser = isHRDepartmentUser(conn, userFullName);

            if (newStatus == "Approved")
            {
                if (isHRRequestUser)
                {
                    // For HR department users, HR Director directly updates the balance
                    if (role == "HR Director" && currentHRStatus != "Approved")
                    {
                        adjustLeaveBalance(conn, requestType, numberOfDays, userFullName, isAdding: true);
                    }
                }
                else
                {
                    // For normal users, balance is updated only when both Manager and HR have approved
                    if (role == "Manager" && currentHRStatus == "Approved" && currentManagerStatus != "Approved")
                    {
                        // HR approved first, and now Manager approves
                        adjustLeaveBalance(conn, requestType, numberOfDays, userFullName, isAdding: true);
                    }
                    else if ((role == "HR" || role == "HR Director") && currentManagerStatus == "Approved" && currentHRStatus != "Approved")
                    {
                        // Manager approved first, and now HR (or HR Director) approves
                        adjustLeaveBalance(conn, requestType, numberOfDays, userFullName, isAdding: true);
                    }
                }
            }
            else if (newStatus == "Rejected")
            {
                // Revert balances only if the request was fully approved before rejection
                if (isHRRequestUser)
                {
                    if (role == "HR Director" && currentHRStatus == "Approved")
                    {
                        adjustLeaveBalance(conn, requestType, numberOfDays, userFullName, isAdding: false);
                    }
                }
                else
                {
                    if (currentHRStatus == "Approved" && currentManagerStatus == "Approved")
                    {
                        adjustLeaveBalance(conn, requestType, numberOfDays, userFullName, isAdding: false);
                    }
                }
            }
        }
        private void adjustLeaveBalance(SqlConnection conn, string requestType, int numberOfDays, string userFullName, bool isAdding)
        {
            string updateLeaveQuery = "";

            if (requestType == "Annual")
            {
                updateLeaveQuery = @"UPDATE AnnualLeave
                     SET AnnualUsed = AnnualUsed + @NumberOfDays
                     WHERE FullName = @FullName";
            }
            else if (requestType == "Emergency")
            {
                updateLeaveQuery = @"UPDATE AnnualLeave
                     SET CasualUsed = CasualUsed + @NumberOfDays
                     WHERE FullName = @FullName";
            }
            else if (requestType == "Unpaid")
            {
                updateLeaveQuery = @"UPDATE AnnualLeave
                     SET UnpaidUsed = UnpaidUsed + @NumberOfDays
                     WHERE FullName = @FullName";
            }
            else if (requestType == "Permission")
            {
                updateLeaveQuery = @"UPDATE AnnualLeave
                     SET PermissionsUsed = PermissionsUsed + 1
                     WHERE FullName = @FullName";
            }

            if (!isAdding)
            {
                updateLeaveQuery = updateLeaveQuery.Replace("+", "-");
            }


            // Only execute the query if it's not empty
            if (!string.IsNullOrEmpty(updateLeaveQuery))
            {
                using (SqlCommand cmd = new SqlCommand(updateLeaveQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@NumberOfDays", numberOfDays);
                    cmd.Parameters.AddWithValue("@FullName", userFullName);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        #endregion

        #region Buttons
        private void addRequestButton_Click(object sender, EventArgs e)
        {
            addRequestForm addRequestForm = new addRequestForm();
            addRequestForm.requestAddedOrUpdatedEvent += refreshRequestsTable;
            addRequestForm.ShowDialog();

            if (requestAddedOrUpdated)
            {
                refreshRequestsTable();
            }

        }
        private void refreshButton_Click(object sender, EventArgs e)
        {
            refreshRequestsTable();
        }
        private void applyButton_Click(object sender, EventArgs e)
        {
            if (hasHRRole || hasManagerRole)
            {
                using (SqlConnection conn = Database.getConnection())
                {
                    conn.Open();

                    foreach (DataGridViewRow row in requestsDataGridView.Rows)
                    {
                        if (row.IsNewRow) continue;

                        bool isApproved = Convert.ToBoolean(row.Cells["Approve"].Value);
                        bool isRejected = Convert.ToBoolean(row.Cells["Reject"].Value);
                        string rejectReason = row.Cells["RequestRejectReason"].Value?.ToString() ?? string.Empty;

                        string newStatus = null;
                        if (isApproved)
                        {
                            newStatus = "Approved";
                        }
                        else if (isRejected)
                        {
                            newStatus = "Rejected";
                        }

                        if (newStatus != null)
                        {
                            int requestId = Convert.ToInt32(row.Cells["RequestID"].Value);
                            string requestType = row.Cells["RequestType"].Value?.ToString();
                            int numberOfDays = Convert.ToInt32(row.Cells["NumberOfDays"].Value);
                            string userFullName = row.Cells["RequestUserFullName"].Value?.ToString();
                            string currentHRStatus = row.Cells["RequestHRStatus"].Value?.ToString();
                            string currentManagerStatus = row.Cells["RequestStatus"].Value?.ToString();

                            // Check if the user is "HR Director" and the request user is in the "HR" department
                            bool isHRDirector = loggedInUser.Role == "HR Director";
                            bool isHRRequestUser = isHRDepartmentUser(conn, userFullName);

                            // Construct update query
                            string query = null;
                            if (hasHRRole)
                            {
                                query = isHRDirector && isHRRequestUser
                                    ? @"UPDATE Requests 
                                 SET RequestHRStatus = @RequestStatus, 
                                     RequestStatus = @RequestStatus, 
                                     RequestRejectReason = @RequestRejectReason, 
                                     RequestHRCloser = @RequestCloser, 
                                     RequestCloser = @RequestCloser 
                                 WHERE RequestID = @RequestID"
                                    : @"UPDATE Requests 
                                 SET RequestHRStatus = @RequestStatus, 
                                     RequestRejectReason = @RequestRejectReason, 
                                     RequestHRCloser = @RequestCloser 
                                 WHERE RequestID = @RequestID";
                            }
                            else if (hasManagerRole)
                            {
                                query = @"UPDATE Requests 
                                 SET RequestStatus = @RequestStatus, 
                                     RequestRejectReason = @RequestRejectReason, 
                                     RequestCloser = @RequestCloser 
                                 WHERE RequestID = @RequestID";
                            }

                            // Execute update query
                            if (!string.IsNullOrEmpty(query))
                            {
                                using (SqlCommand cmd = new SqlCommand(query, conn))
                                {
                                    cmd.Parameters.AddWithValue("@RequestStatus", newStatus);
                                    cmd.Parameters.AddWithValue("@RequestRejectReason", rejectReason);
                                    cmd.Parameters.AddWithValue("@RequestCloser", loggedInUser.FullName);
                                    cmd.Parameters.AddWithValue("@RequestID", requestId);

                                    cmd.ExecuteNonQuery();
                                }
                            }

                            // Call HandleBalanceUpdate only once per request
                            string effectiveRole = isHRDirector ? "HR Director" : hasHRRole ? "HR" : "Manager";
                            handleBalanceUpdate(conn, currentHRStatus, currentManagerStatus, newStatus, effectiveRole, requestType, numberOfDays, userFullName);
                        }
                    }
                    requestNoteEdited = false;
                    MessageBox.Show("Requests updated successfully.");
                    refreshRequestsTable();
                }
            }
        }
        #endregion

    }
}
