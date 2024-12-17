using TDF.Net.Classes;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using TDF.Classes;


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
            applyButton.Visible = mainForm.hasManagerRole;

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
        private void panel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                mainForm.ReleaseCapture();
                mainForm.SendMessage(Handle, 0x112, 0xf012, 0);
            }
        }
        private void requestsDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 & e.RowIndex >= 0)
            {
                if (requestsDataGridView.Columns[e.ColumnIndex].Name == "Edit")
                {
                    if (requestsDataGridView.Rows[e.RowIndex].Cells["RequestStatus"].Value.ToString() == "Pending")
                    {
                        if (requestsDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex] is DataGridViewButtonCell)
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

                            if (addRequestForm.requestAddedOrUpdated)
                            {
                                refreshRequestsTable();
                            }
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

            if (mainForm.hasManagerRole)
            {
                string query;
                using (SqlConnection conn = Database.GetConnection())
                {
                    try
                    {
                        conn.Open();

                        if (loginForm.loggedInUser.Department == "All" || (loginForm.loggedInUser.Department == "Founder & CEO"))
                        {

                            query = pendingRadioButton.Checked ? "SELECT RequestID, RequestUserFullName, RequestType, RequestReason, RequestFromDay, RequestToDay, RequestBeginningTime, RequestEndingTime, RequestStatus, RequestRejectReason " +
                                               $"FROM Requests WHERE RequestStatus = 'Pending'" : "SELECT RequestID, RequestUserFullName, RequestType, RequestReason, RequestFromDay, RequestToDay, RequestBeginningTime, RequestEndingTime, RequestStatus, RequestRejectReason " +
                                               $"FROM Requests WHERE Not RequestStatus = 'Pending'";
                        }
                        else
                        {
                            query = pendingRadioButton.Checked ? "SELECT RequestID, RequestUserFullName, RequestType, RequestReason, RequestFromDay, RequestToDay, RequestBeginningTime, RequestEndingTime, RequestStatus, RequestRejectReason " +
                                           $"FROM Requests WHERE RequestStatus = 'Pending' And RequestDepartment = '{loginForm.loggedInUser.Department}'" : "SELECT RequestID, RequestUserFullName, RequestType, RequestReason, RequestFromDay, RequestToDay, RequestBeginningTime, RequestEndingTime, RequestStatus, RequestRejectReason " +
                                           $"FROM Requests WHERE Not RequestStatus = 'Pending' And RequestDepartment = '{loginForm.loggedInUser.Department}'";
                        }
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                            {
                                // Fill the DataTable with the result of the query
                                adapter.Fill(requestsTable);
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("A database error occurred: " + ex.Message);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An unexpected error occurred: " + ex.Message);
                    }
                }

                requestsDataGridView.Columns["RequestUserFullName"].Visible = true;
                requestsDataGridView.Columns["Approve"].Visible = true;
                requestsDataGridView.Columns["Reject"].Visible = true;
                requestsDataGridView.Columns["Edit"].Visible = false;
                requestsDataGridView.Columns["Remove"].Visible = false;
                requestsDataGridView.Columns["RequestRejectReason"].ReadOnly = false;

            }
            else
            {
                using (SqlConnection conn = Database.GetConnection())
                {
                    try
                    {
                        conn.Open();

                        // Query to get all requests of the logged-in user
                        string query = pendingRadioButton.Checked ? "SELECT RequestID, RequestUserFullName, RequestType, RequestReason, RequestFromDay, RequestToDay, RequestBeginningTime, RequestEndingTime, RequestStatus, RequestRejectReason " +
                                       "FROM Requests WHERE RequestUserID = @UserID and RequestStatus = 'Pending'" :
                                       "SELECT RequestID, RequestUserFullName, RequestType, RequestReason, RequestFromDay, RequestToDay, RequestBeginningTime, RequestEndingTime, RequestStatus, RequestRejectReason " +
                                       "FROM Requests WHERE RequestUserID = @UserID And Not RequestStatus = 'Pending'";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@UserID", loginForm.loggedInUser.userID);

                            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                            {
                                // Fill the DataTable with the result of the query
                                adapter.Fill(requestsTable);
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("A database error occurred: " + ex.Message);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An unexpected error occurred: " + ex.Message);
                    }
                }

                requestsDataGridView.Columns["RequestReason"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                requestsDataGridView.Columns["RequestReason"].Width = 145;
                requestsDataGridView.Columns["Edit"].Visible = pendingRadioButton.Checked;
                requestsDataGridView.Columns["Remove"].Visible = pendingRadioButton.Checked;
            }

            requestsDataGridView.DataSource = requestsTable;

            foreach (DataGridViewRow row in requestsDataGridView.Rows)
            {
                // Check if both RequestFromDay and RequestToDay cells have valid values
                if (row.Cells["RequestFromDay"].Value != null && row.Cells["RequestToDay"].Value != null)
                {
                    DateTime beginningDate, endingDate;

                    // Try parsing both dates to ensure they are valid
                    bool isBeginningDateValid = DateTime.TryParse(row.Cells["RequestFromDay"].Value.ToString(), out beginningDate);
                    bool isEndingDateValid = DateTime.TryParse(row.Cells["RequestToDay"].Value.ToString(), out endingDate);

                    if (isBeginningDateValid && isEndingDateValid)
                    {
                        // Calculate the difference in days
                        int numberOfDays = (endingDate - beginningDate).Days +1;

                        // Set the calculated value in the NumberOfDays column
                        row.Cells["NumberOfDays"].Value = numberOfDays;
                    }
                    else
                    {
                        // Handle invalid date cases (optional message or indication)
                        row.Cells["NumberOfDays"].Value = "-";
                    }
                }
                else
                {
                    // Handle missing dates (optional message or indication)
                    row.Cells["NumberOfDays"].Value = "-";
                }
            }

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

            if (addRequestForm.requestAddedOrUpdated)
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

                        string query = @"
                    UPDATE Requests 
                    SET RequestStatus = @RequestStatus, 
                        RequestRejectReason = @RequestRejectReason, 
                        RequestCloser = @RequestCloser 
                    WHERE RequestID = @RequestID";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@RequestStatus", newStatus);
                            cmd.Parameters.AddWithValue("@RequestRejectReason", rejectReason);
                            cmd.Parameters.AddWithValue("@RequestCloser", loginForm.loggedInUser.FullName);
                            cmd.Parameters.AddWithValue("@RequestID", requestId);

                            cmd.ExecuteNonQuery();
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
