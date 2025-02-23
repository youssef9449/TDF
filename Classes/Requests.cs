using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
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

        public bool add()
        {
            string[] requestTypesToCheck = { "Annual", "Work From Home", "Unpaid", "Emergency" };
            if (Array.IndexOf(requestTypesToCheck, RequestType) >= 0)
            {
                if (hasConflictingRequests())
                {
                    MessageBox.Show("This request conflicts with existing requests. Please check your dates.", "Request Conflict", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            return insertRequest(); // Return success of insertion
        }

        private bool hasConflictingRequests()
        {
            // First check if the dates are valid

        string query = @"
        SELECT COUNT(*)
        FROM Requests
        WHERE RequestUserID = @RequestUserID
        AND RequestID != @RequestID  -- Exclude current request when updating
        AND (RequestType IN ('Annual', 'Work From Home', 'Unpaid', 'Emergency'))
        AND RequestStatus != 'Rejected'  -- Ignore rejected requests
        AND (
            (RequestFromDay <= @RequestToDay AND RequestToDay >= @RequestFromDay)  -- Overlapping date ranges
        )";

            try
            {
                using (SqlConnection conn = Database.getConnection())
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@RequestID", RequestID);  // Will be 0 for new requests
                    cmd.Parameters.AddWithValue("@RequestUserID", RequestUserID);
                    cmd.Parameters.AddWithValue("@RequestFromDay", RequestFromDay);
                    cmd.Parameters.AddWithValue("@RequestToDay", RequestToDay);

                    int conflictCount = (int)cmd.ExecuteScalar();
                    if (conflictCount > 0)
                    {
                        MessageBox.Show("You already have a request for these dates.", "Date Conflict", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return true;
                    }
                    return false;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"A database error occurred: {ex.Message}");
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}");
                return true;
            }
        }

        private bool insertRequest()
        {
            string query = @"
            INSERT INTO Requests (
                RequestUserFullName, RequestType, RequestReason, RequestFromDay, RequestToDay,
                RequestUserID, RequestBeginningTime, RequestEndingTime, RequestDepartment, RequestNumberOfDays, RequestHRStatus
            )
            OUTPUT INSERTED.RequestID
            VALUES (
                @RequestUserFullName, @RequestType, @RequestReason, @RequestFromDay, @RequestToDay,
                @RequestUserID, @RequestBeginningTime, @RequestEndingTime, @RequestDepartment, @RequestNumberOfDays, @RequestHRStatus
            )";

            try
            {
                using (SqlConnection conn = Database.getConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Add common parameters
                        cmd.Parameters.AddWithValue("@RequestUserFullName", RequestUserFullName);
                        cmd.Parameters.AddWithValue("@RequestType", RequestType);
                        cmd.Parameters.AddWithValue("@RequestReason", RequestReason ?? (object)DBNull.Value);
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

                        // Execute query and get the inserted RequestID
                        RequestID = (int)cmd.ExecuteScalar(); // Sets RequestID
                        bool success = RequestID > 0;
                        Forms.addRequestForm.requestAddedOrUpdated = success;
                        MessageBox.Show(success ? "Request added successfully." : "Failed to add request.", "Add Request", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return success;
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"A database error occurred: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}");
                return false;
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

        public void InsertNotificationsForNewRequest()
        {
            using (SqlConnection conn = Database.getConnection())
            {
                conn.Open();
                // Find HR users and users in the Human Resources department
                string hrRolesQuery = $"SELECT UserID FROM Users WHERE Role IN ('Human Resources')";
                List<int> hrUserIds = new List<int>();
                using (SqlCommand cmd = new SqlCommand(hrRolesQuery, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            hrUserIds.Add(reader.GetInt32(0));
                        }
                    }
                }

                // Find managers and users in the Human Resources department
                string department = RequestDepartment;
                string queryManagers = "SELECT UserID FROM Users WHERE (Role IN ('Manager', 'Team Leader') OR Department = @Department)";
                List<int> managerUserIds = new List<int>();
                using (SqlCommand cmd = new SqlCommand(queryManagers, conn))
                {
                    cmd.Parameters.AddWithValue("@Department", "Human Resources");
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            managerUserIds.Add(reader.GetInt32(0));
                        }
                    }
                }

                // Insert notifications for unique users (HR and managers in the department)
                string insertQuery = "INSERT INTO RequestNotifications (RequestId, UserId, IsRead, CreatedAt) VALUES (@RequestId, @UserId, 0, @CreatedAt)";
                var affectedUsers = hrUserIds.Union(managerUserIds).Distinct().ToList();
                foreach (int userId in affectedUsers)
                {
                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@RequestId", RequestID);
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        cmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
                        cmd.ExecuteNonQuery();
                    }
                }
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
    }

    #endregion
}


