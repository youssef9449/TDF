using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace TDF.Net.Classes
{

    public class Request
    {
        public int RequestID { get; set; }
        public int RequestUserID { get; set; }
        public string RequestUserFullName { get; set; }
        public string RequestType { get; set; }
        public string RequestReason { get; set; }
        public string RequestStatus { get; set; }
        public string RequestCloser { get; set; }
        public DateTime RequestFromDay { get; set; }
        public DateTime? RequestToDay { get; set; }
        public DateTime? RequestBeginningTime { get; set; }
        public DateTime? RequestEndingTime { get; set; }
        public string RequestRejectReason { get; set; }
        public string RequestDepartment { get; set; }
        public int RequestNumberOfDays { get; set; }


        public Request()
        {

        }

        public Request(string requestType, string requestReason, string requestUserFullName, DateTime requestFromDay, DateTime requestToDay, int requestUserID, string requestDepartment, int requestNumberOfDays, DateTime? requestBeginningTime, DateTime? requestEndingTime)
        {
            RequestType = requestType;
            RequestReason = requestReason;
            RequestFromDay = requestFromDay;
            RequestUserFullName = requestUserFullName;
            RequestToDay = requestToDay;
            RequestUserID = requestUserID;
            RequestBeginningTime = null;
            RequestEndingTime = null;
            RequestDepartment = requestDepartment;
            RequestNumberOfDays = requestNumberOfDays;
            RequestBeginningTime = requestBeginningTime;
            RequestEndingTime = requestEndingTime;
        }

        #region Methods

        public void add()
        {
            string query = @"
        INSERT INTO Requests (
            RequestUserFullName, RequestType, RequestReason, RequestFromDay, RequestToDay,
            RequestUserID, RequestBeginningTime, RequestEndingTime, RequestDepartment, RequestNumberOfDays, RequestHRStatus
        )
        VALUES (
            @RequestUserFullName, @RequestType, @RequestReason, @RequestFromDay, @RequestToDay,
            @RequestUserID, @RequestBeginningTime, @RequestEndingTime, @RequestDepartment, @RequestNumberOfDays, @RequestHRStatus
        )";

            try
            {
                using (SqlConnection conn = Database.getConnection())
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();

                    // Add common parameters
                    cmd.Parameters.AddWithValue("@RequestUserFullName", RequestUserFullName);
                    cmd.Parameters.AddWithValue("@RequestType", RequestType);
                    cmd.Parameters.AddWithValue("@RequestReason", RequestReason);
                    cmd.Parameters.AddWithValue("@RequestFromDay", RequestFromDay);
                    cmd.Parameters.AddWithValue("@RequestToDay", RequestType == "Permission" || RequestType == "External Assignment" ? (object)DBNull.Value : RequestToDay);
                    cmd.Parameters.AddWithValue("@RequestUserID", RequestUserID);
                    cmd.Parameters.AddWithValue("@RequestDepartment", RequestDepartment);
                    cmd.Parameters.AddWithValue("@RequestNumberOfDays", RequestNumberOfDays);
                    cmd.Parameters.AddWithValue("@RequestHRStatus", "Pending");

                    if (RequestType == "Permission" || RequestType == "External Assignment")
                    {
                        cmd.Parameters.AddWithValue("@RequestBeginningTime", RequestBeginningTime ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@RequestEndingTime", RequestEndingTime ?? (object)DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@RequestBeginningTime", DBNull.Value);
                        cmd.Parameters.AddWithValue("@RequestEndingTime", DBNull.Value);
                    }

                    // Execute query and show result
                    bool success = cmd.ExecuteNonQuery() > 0;
                    Forms.addRequestForm.requestAddedOrUpdated = success;
                    MessageBox.Show(success ? "Request added successfully." : "Failed to add request.", "Add Request", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"A database error occurred: {ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}");
            }
        }

        public void update()
        {
            try
            {
                using (SqlConnection conn = Database.getConnection())
                {
                    conn.Open();

                    // SQL query to update the request, allowing RequestBeginningTime and RequestEndingTime to be NULL
                    string query = @"UPDATE Requests
                             SET 
                                 RequestType = @RequestType,
                                 RequestReason = @RequestReason,
                                 RequestFromDay = @RequestFromDay,
                                 RequestToDay = @RequestToDay,
                                 RequestBeginningTime = @RequestBeginningTime,
                                 RequestEndingTime = @RequestEndingTime,
                                 RequestStatus = @RequestStatus,
                                 RequestNumberOfDays = @RequestNumberOfDays
                                 WHERE RequestID = @RequestID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@RequestID", RequestID);
                        cmd.Parameters.AddWithValue("@RequestType", RequestType ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@RequestReason", RequestReason ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@RequestFromDay", RequestFromDay != DateTime.MinValue ? (object)RequestFromDay : DBNull.Value);
                        cmd.Parameters.AddWithValue("@RequestToDay", RequestToDay != null ? (object)RequestToDay : DBNull.Value);

                        // Handling nullable RequestBeginningTime and RequestEndingTime
                        cmd.Parameters.AddWithValue("@RequestBeginningTime", RequestBeginningTime.HasValue ?
                                                                (object)RequestBeginningTime.Value : DBNull.Value);
                        cmd.Parameters.AddWithValue("@RequestEndingTime", RequestEndingTime.HasValue ?
                                                                (object)RequestEndingTime.Value : DBNull.Value);

                        cmd.Parameters.AddWithValue("@RequestStatus", RequestStatus ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@RequestNumberOfDays", RequestType == "Permission" || RequestType == "External Assignment" ? 0 : RequestNumberOfDays);

                        // Execute the update query and check if it was successful
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Request updated successfully.", "Update Request", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Forms.addRequestForm.requestAddedOrUpdated = true;
                        }
                        else
                        {
                            MessageBox.Show("No request was updated.", "Update Request", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Forms.addRequestForm.requestAddedOrUpdated = false;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                // Handle database-related exceptions
                MessageBox.Show("An error occurred while updating the request: " + ex.Message);
            }
            catch (Exception ex)
            {
                // Handle other potential exceptions
                MessageBox.Show("An unexpected error occurred: " + ex.Message);
            }
        }

        public void delete()
        {
            using (SqlConnection conn = Database.getConnection())
            {
                try
                {
                    conn.Open();

                    string query = "DELETE FROM Requests WHERE RequestID = @RequestID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@RequestID", RequestID);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Request deleted successfully.");
                            Forms.addRequestForm.requestAddedOrUpdated = true;
                        }
                        else
                        {
                            MessageBox.Show("No request found with the given ID.");
                            Forms.addRequestForm.requestAddedOrUpdated = false;
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
        }

        #endregion

    }
}


