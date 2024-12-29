using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using TDF.Classes;
using TDF.Net.Classes;
using static TDF.Net.loginForm;
using static TDF.Net.mainForm;
using static TDF.Net.Forms.addRequestForm;


namespace TDF.Net.Forms
{
    public partial class requestsForm : Form
    {
        public requestsForm()
        {
            InitializeComponent();
            Program.loadForm(this);
            controlBox.BackColor = Color.White;
            controlBox.CloseBoxOptions.HoverColor = Color.White;
            controlBox.CloseBoxOptions.IconHoverColor = ThemeColor.SecondaryColor;
            controlBox.CloseBoxOptions.IconPressedColor = ThemeColor.PrimaryColor;
            controlBox.CloseBoxOptions.PressedColor = Color.White;
        }

        #region Events
        private void requestsForm_Resize(object sender, EventArgs e)
        {
            Invalidate();
            /*sizeChangecount++;
            requestsDataGridView.Columns["RequestReason"].AutoSizeMode = sizeChangecount > 1 ? DataGridViewAutoSizeColumnMode.Fill : DataGridViewAutoSizeColumnMode.ColumnHeader;*/
        }
        private void Requests_Load(object sender, EventArgs e)
        {
            applyButton.Visible = hasManagerRole || hasAdminRole;

            pendingLabel.Visible = true;
            closedLabel.Visible = true;

            closedRadioButton.Visible = true;
            pendingRadioButton.Visible = true;

            refreshRequestsTable();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            ControlPaint.DrawBorder(e.Graphics, ClientRectangle, ThemeColor.SecondaryColor, ButtonBorderStyle.Solid);
        }
        private void requestsDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 & e.RowIndex >= 0)
            {
                if (requestsDataGridView.Columns[e.ColumnIndex].Name == "Edit" && requestsDataGridView.Rows[e.RowIndex].Cells["RequestStatus"].Value.ToString() == "Pending"|| hasAdminRole || hasManagerRole)
                {
                    if (requestsDataGridView.Rows[e.RowIndex].Cells["RequestStatus"].Value.ToString() == "Pending")
                    {
                        if (requestsDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex] is DataGridViewButtonCell)
                        {
                            openRequestToEdit(e);
                        }
                    }
                    else
                    {
                        MessageBox.Show("The request is locked. You cannot perform this action.");

                    }
                }

                if (requestsDataGridView.Columns[e.ColumnIndex].Name == "Remove")
                {
                    if (requestsDataGridView.Rows[e.RowIndex].Cells["RequestStatus"].Value.ToString() != "Pending")
                    {
                        if (requestsDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex] is DataGridViewButtonCell buttonCell)
                        {
                            var confirmResult = MessageBox.Show("Are you sure you want to delete this request?",
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
                    }
                    else
                    {
                        MessageBox.Show("The request is locked. You cannot perform this action.");
                    }
                }

                if (e.RowIndex >= 0 &&
                 (requestsDataGridView.Columns[e.ColumnIndex].Name == "Approve" ||
                  requestsDataGridView.Columns[e.ColumnIndex].Name == "Reject"))
                {
                    // Get the current row
                    DataGridViewRow currentRow = requestsDataGridView.Rows[e.RowIndex];

                    // Toggle checkboxes based on current selection
                    if (requestsDataGridView.Columns[e.ColumnIndex].Name == "Approve")
                    {
                        // If "Approve" was clicked, uncheck "Reject"
                        currentRow.Cells["Reject"].Value = false;
                    }
                    else if (requestsDataGridView.Columns[e.ColumnIndex].Name == "Reject")
                    {
                        // If "Reject" was clicked, uncheck "Approve"
                        currentRow.Cells["Approve"].Value = false;
                    }
                }
            }
        }
        private void requestsDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Check if the current cell is in the column that holds the status
            if (requestsDataGridView.Columns[e.ColumnIndex].Name == "RequestStatus") // Change to your actual column name
            {
                // Check the value of the cell
                if (e.Value != null && e.Value.ToString() == "Approved")
                {
                    // Set the font color to green
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
                requestsDataGridView.Columns[e.ColumnIndex].Name == "RequestEndingTime" ||
                requestsDataGridView.Columns[e.ColumnIndex].Name == "RequestToDay" ||
                requestsDataGridView.Columns[e.ColumnIndex].Name == "RequestRejectReason")
            {
                // Check if the cell value is null or empty
                if (e.Value == null || string.IsNullOrEmpty(e.Value.ToString()))
                {
                    e.Value = "-"; // Set the display value to "-"
                    e.FormattingApplied = true; // Indicate that formatting is applied
                }
            }
            if (requestsDataGridView.Columns[e.ColumnIndex].Name == "NumberOfDays")
            {
                // Check if the cell value is null or empty
                if ((int)e.Value == 0 || string.IsNullOrEmpty(e.Value.ToString()))
                {
                    e.Value = "-"; // Set the display value to "-"
                    e.FormattingApplied = true; // Indicate that formatting is applied
                }
            }
        }
        private void pendingRadioButton_CheckedChanged(object sender, Bunifu.UI.WinForms.BunifuRadioButton.CheckedChangedEventArgs e)
        {
            refreshRequestsTable();
        }
        #endregion

        #region Methods
        public void refreshRequestsTable()
        {
            DataTable requestsTable = new DataTable();

            try
            {
                if (hasManagerRole || hasAdminRole)
                {
                    LoadRequestsForManagerOrAdmin(requestsTable);
                    ConfigureDataGridViewForManagerOrAdmin();
                }
                else
                {
                    LoadRequestsForUser(requestsTable);
                    ConfigureDataGridViewForUser();
                }

                requestsDataGridView.DataSource = requestsTable;
                //CalculateNumberOfDaysForRequests();
                ReorderDataGridViewColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadRequestsForManagerOrAdmin(DataTable requestsTable)
        {
            string query = BuildQueryForManagerOrAdmin();
            ExecuteQuery(query, requestsTable);
        }
        private void LoadRequestsForUser(DataTable requestsTable)
        {
            string query = BuildQueryForUser();
            ExecuteQuery(query, requestsTable, cmd =>
            {
                cmd.Parameters.AddWithValue("@UserID", loggedInUser.userID);
            });
        }
        private string BuildQueryForManagerOrAdmin()
        {
            string baseQuery = @"
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
            CASE 
                WHEN r.RequestType = 'Annual' THEN al.AnnualBalance
                WHEN r.RequestType = 'Casual' THEN al.CasualBalance
                ELSE NULL
            END AS remainingBalance
        FROM 
            Requests r
        LEFT JOIN 
            AnnualLeave al ON r.RequestUserID = al.UserID ";

            string condition = pendingRadioButton.Checked
                ? "WHERE r.RequestStatus = 'Pending'"
                : "WHERE NOT r.RequestStatus = 'Pending'";

            if (!hasAdminRole)
            {
                condition += $" AND r.RequestDepartment = '{loggedInUser.Department}'";
            }

            return baseQuery + condition;
        }
        private string BuildQueryForUser()
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
            CASE 
                WHEN r.RequestType = 'Annual' THEN al.AnnualBalance
                WHEN r.RequestType = 'Casual' THEN al.CasualBalance
                ELSE NULL
            END AS remainingBalance
        FROM 
            Requests r
        LEFT JOIN 
            AnnualLeave al ON r.RequestUserID = al.UserID
        WHERE 
            r.RequestUserID = @UserID AND 
            " + (pendingRadioButton.Checked ? "r.RequestStatus = 'Pending'" : "NOT r.RequestStatus = 'Pending'");
        }
        private void ExecuteQuery(string query, DataTable requestsTable, Action<SqlCommand> parameterizeCommand = null)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    parameterizeCommand?.Invoke(cmd);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(requestsTable);
                    }
                }
            }
        }
        private void ConfigureDataGridViewForManagerOrAdmin()
        {
            requestsDataGridView.Columns["RequestUserFullName"].Visible = true;
            requestsDataGridView.Columns["Approve"].Visible = true;
            requestsDataGridView.Columns["Reject"].Visible = true;
            //requestsDataGridView.Columns["Edit"].Visible = false;
            requestsDataGridView.Columns["Remove"].Visible = false;
            requestsDataGridView.Columns["RequestRejectReason"].ReadOnly = false;
        }
        private void ConfigureDataGridViewForUser()
        {
            //requestsDataGridView.Columns["RequestReason"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            //requestsDataGridView.Columns["RequestRejectReason"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            //  requestsDataGridView.Columns["RequestReason"].Width = 145;
            bool isPending = pendingRadioButton.Checked;
            requestsDataGridView.Columns["Edit"].Visible = isPending;
            requestsDataGridView.Columns["Remove"].Visible = isPending;
        }
        private void openRequestToEdit(DataGridViewCellEventArgs e)
        {
            // Get the underlying data row from the DataGridView
            DataRow row = ((DataRowView)requestsDataGridView.Rows[e.RowIndex].DataBoundItem).Row;

            // Retrieve the request data from the DataRow
            DateTime requestFromDay = Convert.ToDateTime(row["RequestFromDay"]);

            Request selectedRequest = new Request
            {
                RequestID = row["RequestID"] != DBNull.Value ? Convert.ToInt32(row["RequestID"]) : 0,
                RequestType = row["RequestType"] != DBNull.Value ? row["RequestType"].ToString() : string.Empty,
                RequestReason = row["RequestReason"] != DBNull.Value ? row["RequestReason"].ToString() : string.Empty,
                RequestFromDay = requestFromDay,
                RequestToDay = row["RequestToDay"] != DBNull.Value ? Convert.ToDateTime(row["RequestToDay"]) : requestFromDay,

                // Converting TimeSpan to DateTime by adding the TimeSpan to the requestFromDay
                RequestBeginningTime = row["RequestBeginningTime"] != DBNull.Value ?
                                       requestFromDay.Add((TimeSpan)row["RequestBeginningTime"]) : requestFromDay,
                RequestEndingTime = row["RequestEndingTime"] != DBNull.Value ?
                                    requestFromDay.Add((TimeSpan)row["RequestEndingTime"]) : requestFromDay,

                RequestStatus = row["RequestStatus"] != DBNull.Value ? row["RequestStatus"].ToString() : string.Empty
            };

            // Open the AddRequestForm with the selected request data for editing
            addRequestForm addRequestForm = new addRequestForm(selectedRequest); // Assuming AddRequestForm has a constructor that takes a Request object
            addRequestForm.ShowDialog();

            // Optionally, refresh the DataGridView after editing (if changes are saved)
            //refreshRequestsTable();

            if (requestAddedOrUpdated)
            {
                refreshRequestsTable();
            }
        }
        /* private void CalculateNumberOfDaysForRequests()
         {
             foreach (DataGridViewRow row in requestsDataGridView.Rows)
             {
                 if (row.Cells["RequestFromDay"].Value != null && row.Cells["RequestToDay"].Value != null &&
                     DateTime.TryParse(row.Cells["RequestFromDay"].Value.ToString(), out var beginningDate) &&
                     DateTime.TryParse(row.Cells["RequestToDay"].Value.ToString(), out var endingDate))
                 {
                     row.Cells["NumberOfDays"].Value = (endingDate - beginningDate).Days + 1;
                 }
                 else
                 {
                     row.Cells["NumberOfDays"].Value = "-";
                 }
             }
         }*/
        private void ReorderDataGridViewColumns()
        {
            requestsDataGridView.Columns["Edit"].DisplayIndex = requestsDataGridView.Columns.Count - 1;
            requestsDataGridView.Columns["Remove"].DisplayIndex = requestsDataGridView.Columns.Count - 1;
            requestsDataGridView.Columns["RequestStatus"].DisplayIndex = requestsDataGridView.Columns.Count - 3;
            requestsDataGridView.Columns["RequestRejectReason"].DisplayIndex = requestsDataGridView.Columns.Count - 3;
            requestsDataGridView.Columns["Reject"].DisplayIndex = requestsDataGridView.Columns.Count - 1;
            requestsDataGridView.Columns["Approve"].DisplayIndex = requestsDataGridView.Columns.Count - 1;
        }
        #endregion

        #region Buttons
        private void addRequestButton_Click(object sender, EventArgs e)
        {
            addRequestForm addRequestForm = new addRequestForm();
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
            using (SqlConnection conn = Database.GetConnection())
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
                        string currentStatus = row.Cells["RequestStatus"].Value?.ToString(); // Current status from the DataGridView

                        // Update request status and rejection reason
                        string query = @"UPDATE Requests 
                                 SET RequestStatus = @RequestStatus, 
                                     RequestRejectReason = @RequestRejectReason, 
                                     RequestCloser = @RequestCloser 
                                 WHERE RequestID = @RequestID";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@RequestStatus", newStatus);
                            cmd.Parameters.AddWithValue("@RequestRejectReason", rejectReason);
                            cmd.Parameters.AddWithValue("@RequestCloser", loggedInUser.FullName);
                            cmd.Parameters.AddWithValue("@RequestID", requestId);

                            cmd.ExecuteNonQuery();
                        }

                        // Handle balance adjustment if the status is changing between "Approved" and "Rejected"
                        if (newStatus == "Approved" && currentStatus != "Approved" && (requestType == "Annual" || requestType == "Casual"))
                        {
                            string updateLeaveQuery = "";

                            if (requestType == "Annual")
                            {
                                updateLeaveQuery = @"UPDATE AnnualLeave
                                             SET AnnualUsed = AnnualUsed + @NumberOfDays
                                             WHERE FullName = @FullName";
                            }
                            else if (requestType == "Casual")
                            {
                                updateLeaveQuery = @"UPDATE AnnualLeave
                                             SET CasualUsed = CasualUsed + @NumberOfDays
                                             WHERE FullName = @FullName";
                            }

                            using (SqlCommand cmd = new SqlCommand(updateLeaveQuery, conn))
                            {
                                cmd.Parameters.AddWithValue("@NumberOfDays", numberOfDays);
                                cmd.Parameters.AddWithValue("@FullName", userFullName);

                                cmd.ExecuteNonQuery();
                            }
                        }
                        else if (newStatus == "Rejected" && currentStatus == "Approved" && (requestType == "Annual" || requestType == "Casual"))
                        {
                            string updateLeaveQuery = "";

                            if (requestType == "Annual")
                            {
                                updateLeaveQuery = @"UPDATE AnnualLeave
                                             SET AnnualUsed = AnnualUsed - @NumberOfDays
                                             WHERE FullName = @FullName";
                            }
                            else if (requestType == "Casual")
                            {
                                updateLeaveQuery = @"UPDATE AnnualLeave
                                             SET CasualUsed = CasualUsed - @NumberOfDays
                                             WHERE FullName = @FullName";
                            }

                            using (SqlCommand cmd = new SqlCommand(updateLeaveQuery, conn))
                            {
                                cmd.Parameters.AddWithValue("@NumberOfDays", numberOfDays);
                                cmd.Parameters.AddWithValue("@FullName", userFullName);

                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                }

                MessageBox.Show("Requests updated successfully.");
                refreshRequestsTable();
            }
        }
        #endregion

    }
}
