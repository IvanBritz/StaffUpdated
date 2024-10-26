using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Staff
{
    public class StaffDetailsPopup
    {
        public static StaffDetailsPopup instance;

        private StaffDetailsPopup() { }

        public static StaffDetailsPopup GetInstance() 
        {
            if (instance == null)
            {
                instance = new StaffDetailsPopup(); 
            }
            return instance; 
        
        }

        public void ShowStaffDetailsPopup(int staffID)
        {
            string query = @"
        SELECT s.StaffID, s.StaffName, r.RoleName, s.Username, s.Password
        FROM Staff s
        INNER JOIN Role r ON s.RoleID = r.RoleID
        WHERE s.StaffID = @StaffID";
            DatabaseConn b = DatabaseConn.getInstance();

            try
            {
                using (SqlCommand cmd = new SqlCommand(query, b.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@StaffID", staffID);

                    if (b.GetConnection().State == System.Data.ConnectionState.Closed)
                    {
                        b.GetConnection().Open();
                    }

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {

                            int id = Convert.ToInt32(reader["StaffID"]);
                            string name = reader["StaffName"].ToString();
                            string role = reader["RoleName"].ToString();
                            string username = reader["Username"]?.ToString();
                            string password = reader["Password"]?.ToString();


                            StaffDetailsForm detailsForm = new StaffDetailsForm(id, name, role, username, password);
                            detailsForm.ShowDialog();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving staff details: " + ex.Message);
            }
            finally
            {
                b.CloseConnection();
            }
        }

    }
}
