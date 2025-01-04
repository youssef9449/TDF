using Bunifu.UI.WinForms;
using Microsoft.Office.Interop.Excel;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using TDF.Classes;
using TDF.Net.Classes;
using static TDF.Net.Forms.addRequestForm;
using static TDF.Net.loginForm;
using static TDF.Net.mainForm;
using DataTable = System.Data.DataTable;
using Excel = Microsoft.Office.Interop.Excel;

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
        private void Requests_Load(object sender, EventArgs e)
        {
            applyButton.Visible = hasManagerRole || hasAdminRole;

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
                if (requestsDataGridView.Columns[e.ColumnIndex].Name == "Edit" && (requestsDataGridView.Rows[e.RowIndex].Cells["RequestStatus"].Value.ToString() == "Pending" || hasAdminRole || hasManagerRole))
                {
                    if (requestsDataGridView.Rows[e.RowIndex].Cells["RequestStatus"].Value.ToString() == "Pending")
                    {
                        if (requestsDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex] is DataGridViewImageCell)
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
                    if (requestsDataGridView.Rows[e.RowIndex].Cells["RequestStatus"].Value.ToString() == "Pending")
                    {
                        if (requestsDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex] is DataGridViewImageCell imageCell)
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
                    }
                    else
                    {
                        MessageBox.Show("The request is locked. You cannot perform this action.");
                    }
                }

                if (requestsDataGridView.Columns[e.ColumnIndex].Name == "Approve" || requestsDataGridView.Columns[e.ColumnIndex].Name == "Reject")
                {
                    DataGridViewRow currentRow = requestsDataGridView.Rows[e.RowIndex];

                    if (requestsDataGridView.Columns[e.ColumnIndex].Name == "Approve")
                    {
                        currentRow.Cells["Reject"].Value = false;
                    }
                    else if (requestsDataGridView.Columns[e.ColumnIndex].Name == "Reject")
                    {
                        currentRow.Cells["Approve"].Value = false;
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
                    string status = currentRow.Cells["RequestStatus"].Value?.ToString() ?? string.Empty;

                    if (status == "Rejected")
                    {
                        MessageBox.Show("Generating a report for a rejected request is not possible.");
                        return;
                    }

                    createPDF(requestType, beginningDate, endingDate, numberOfDays, availableBalance, reason, beginningTime, endingTime, status);
                }
            }
        }
        private void requestsDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (requestsDataGridView.Columns[e.ColumnIndex].Name == "RequestStatus")
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
        #endregion

        #region Methods
        public void refreshRequestsTable()
        {
            DataTable requestsTable = new DataTable();

            try
            {
                if (hasManagerRole || hasAdminRole)
                {
                    loadRequestsForManagerOrAdmin(requestsTable);
                    configureDataGridViewForManagerOrAdmin();
                }
                else
                {
                    loadRequestsForUser(requestsTable);
                    configureDataGridViewForUser();
                }

                requestsDataGridView.DataSource = requestsTable;
                reorderDataGridViewColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void loadRequestsForManagerOrAdmin(DataTable requestsTable)
        {
            string query = buildQueryForManagerOrAdmin();
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
        private string buildQueryForManagerOrAdmin()
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
                WHEN r.RequestType = 'Emergency' THEN al.CasualBalance
                WHEN r.RequestType = 'Permission' THEN al.PermissionsBalance
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
                // Split the manager's departments and build conditions dynamically
                string[] departments = loggedInUser.Department.Split(new string[] { " - " }, StringSplitOptions.RemoveEmptyEntries);
                string departmentCondition = string.Join(" OR ", departments.Select(d => $"r.RequestDepartment LIKE '%{d.Trim()}%'"));
                condition += $" AND ({departmentCondition})";
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
            r.RequestUserID = @UserID AND 
            " + (pendingRadioButton.Checked ? "r.RequestStatus = 'Pending'" : "NOT r.RequestStatus = 'Pending'");
        }
        private void executeQuery(string query, DataTable requestsTable, Action<SqlCommand> parameterizeCommand = null)
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
        private void configureDataGridViewForManagerOrAdmin()
        {
            requestsDataGridView.Columns["RequestUserFullName"].Visible = true;
            requestsDataGridView.Columns["Approve"].Visible = true;
            requestsDataGridView.Columns["Reject"].Visible = true;
            requestsDataGridView.Columns["Edit"].Visible = pendingRadioButton.Checked;
            requestsDataGridView.Columns["Remove"].Visible = false;
            if (requestsDataGridView.Columns["Report"] != null)
            {
                requestsDataGridView.Columns["Report"].Visible = false;
            }
            requestsDataGridView.Columns["RequestRejectReason"].ReadOnly = false;
        }
        private void configureDataGridViewForUser()
        {
            bool isPending = pendingRadioButton.Checked;
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
        private void reorderDataGridViewColumns()
        {
            requestsDataGridView.Columns["Edit"].DisplayIndex = requestsDataGridView.Columns.Count - 1;
            requestsDataGridView.Columns["Remove"].DisplayIndex = requestsDataGridView.Columns.Count - 1;
            if (requestsDataGridView.Columns["Report"] != null)
            {
                requestsDataGridView.Columns["Report"].DisplayIndex = requestsDataGridView.Columns.Count - 1;
            }
            requestsDataGridView.Columns["RequestStatus"].DisplayIndex = requestsDataGridView.Columns.Count - 4;
            requestsDataGridView.Columns["RequestRejectReason"].DisplayIndex = requestsDataGridView.Columns.Count - 4;
            requestsDataGridView.Columns["Reject"].DisplayIndex = requestsDataGridView.Columns.Count - 1;
            requestsDataGridView.Columns["Approve"].DisplayIndex = requestsDataGridView.Columns.Count - 1;
        }
        private (string managerName, string managerDepartment) getManagerName()
        {
            string managerName = string.Empty;
            string managerDepartment = string.Empty;

            string query = "SELECT TOP 1 FullName, Department FROM Users WHERE Department LIKE @Department AND Role = @Role";

            try
            {
                using (SqlConnection connection = Database.GetConnection())
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
        public (int Annual, int Casual, int AnnualUsed, int CasualUsed) getLeaveBalances()
        {
            string query = @"SELECT Annual, CasualLeave, AnnualUsed, CasualUsed 
                             FROM AnnualLeave 
                             WHERE UserID = @UserID";

            using (SqlConnection conn = Database.GetConnection())
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

            using (var conn = Database.GetConnection())
            using (var cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@UserID", loggedInUser.userID);
                conn.Open();

                return (int?)cmd.ExecuteScalar() ?? 0;
            }
        }
        private void createPDF(string requestType, DateTime? beginningDate, DateTime? endingDate, int numberOfDays, int availableBalance, string reason, string beginningTime, string endingTime, string status)
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

                // Write the common data for all request types
                worksheet.Cells[2, 3].Value = DateTime.Now.Date;
                worksheet.Cells[3, 3].Value = loggedInUser.FullName;
                worksheet.Cells[5, 3].Value = loggedInUser.Department;
                worksheet.Cells[4, 3].Value = loggedInUser.Title;
                worksheet.Cells[6, 3].Value = managerName;
                worksheet.Cells[7, 3].Value = managerDepartment + " Department";

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
                        string currentStatus = row.Cells["RequestStatus"].Value?.ToString();

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
                        if (newStatus == "Approved" && currentStatus != "Approved" && (requestType == "Annual" || requestType == "Emergency" || requestType == "Unpaid" || requestType == "Permission"))
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
                            else
                            {
                                updateLeaveQuery = @"UPDATE AnnualLeave
                                             SET PermissionsUsed = PermissionsUsed + 1
                                             WHERE FullName = @FullName";
                            }

                            using (SqlCommand cmd = new SqlCommand(updateLeaveQuery, conn))
                            {
                                cmd.Parameters.AddWithValue("@NumberOfDays", numberOfDays);
                                cmd.Parameters.AddWithValue("@FullName", userFullName);

                                cmd.ExecuteNonQuery();
                            }
                        }
                        else if (newStatus == "Rejected" && currentStatus == "Approved" && (requestType == "Annual" || requestType == "Emergency" || requestType == "Unpaid" || requestType == "Permission"))
                        {
                            string updateLeaveQuery = "";

                            if (requestType == "Annual")
                            {
                                updateLeaveQuery = @"UPDATE AnnualLeave
                                             SET AnnualUsed = AnnualUsed - @NumberOfDays
                                             WHERE FullName = @FullName";
                            }
                            else if (requestType == "Emergency")
                            {
                                updateLeaveQuery = @"UPDATE AnnualLeave
                                             SET CasualUsed = CasualUsed - @NumberOfDays
                                             WHERE FullName = @FullName";
                            }
                            else if (requestType == "Unpaid")
                            {
                                updateLeaveQuery = @"UPDATE AnnualLeave
                                             SET UnpaidUsed = UnpaidUsed - @NumberOfDays
                                             WHERE FullName = @FullName";
                            }
                            else
                            {
                                updateLeaveQuery = @"UPDATE AnnualLeave
                                             SET PermissionsUsed = PermissionsUsed - 1
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
