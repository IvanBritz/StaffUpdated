﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Staff
{
    public class StaffRepository : IStaffRepository
    {
        private readonly DatabaseConn _databaseConn = DatabaseConn.getInstance();

        public event EventHandler DataChanged;

        protected virtual void OnDataChanged()
        {
            DataChanged?.Invoke(this, EventArgs.Empty);
        }

        public DataTable GetAllStaff()
        {
            string query = "SELECT StaffID, StaffName, BaseSalary FROM Staff";
            using (SqlCommand cmd = new SqlCommand(query, _databaseConn.GetConnection()))
            {
                if (_databaseConn.GetConnection().State == ConnectionState.Closed)
                {
                    _databaseConn.GetConnection().Open();
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public DataTable FilterStaff(string searchValue)
        {
            string query = "SELECT StaffID, StaffName, BaseSalary FROM Staff WHERE StaffName LIKE @SearchValue";
            using (SqlCommand cmd = new SqlCommand(query, _databaseConn.GetConnection()))
            {
                cmd.Parameters.AddWithValue("@SearchValue", "%" + searchValue + "%");

                if (_databaseConn.GetConnection().State == ConnectionState.Closed)
                {
                    _databaseConn.GetConnection().Open();
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public void UpdateStaff(int staffID, string staffName, decimal baseSalary)
        {
            string query = "UPDATE Staff SET StaffName = @StaffName, BaseSalary = @BaseSalary WHERE StaffID = @StaffID";
            using (SqlCommand cmd = new SqlCommand(query, _databaseConn.GetConnection()))
            {
                cmd.Parameters.AddWithValue("@StaffName", staffName);
                cmd.Parameters.AddWithValue("@BaseSalary", baseSalary);
                cmd.Parameters.AddWithValue("@StaffID", staffID);

                if (_databaseConn.GetConnection().State == ConnectionState.Closed)
                {
                    _databaseConn.GetConnection().Open();
                }

                cmd.ExecuteNonQuery();
                OnDataChanged();
                MessageBox.Show("Staff updated successfully.");
            }
        }

        public void DeleteStaff(int staffID)
        {
            string query = "DELETE FROM Staff WHERE StaffID = @StaffID";
            using (SqlCommand cmd = new SqlCommand(query, _databaseConn.GetConnection()))
            {
                cmd.Parameters.AddWithValue("@StaffID", staffID);

                if (_databaseConn.GetConnection().State == ConnectionState.Closed)
                {
                    _databaseConn.GetConnection().Open();
                }

                cmd.ExecuteNonQuery();
                MessageBox.Show("Staff deleted successfully.");
                OnDataChanged();
            }
        }

        public bool AddStaff(string staffName, int roleID, decimal baseSalary, string username, string password)
        {
            string query = "INSERT INTO Staff (StaffName, RoleID, BaseSalary, Username, Password) VALUES (@StaffName, @RoleID, @BaseSalary, @Username, @Password)";
            try
            {
                using (SqlCommand cmd = new SqlCommand(query, _databaseConn.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@StaffName", staffName);
                    cmd.Parameters.AddWithValue("@RoleID", roleID);
                    cmd.Parameters.AddWithValue("@BaseSalary", baseSalary);
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", password);

                    if (_databaseConn.GetConnection().State == ConnectionState.Closed)
                    {
                        _databaseConn.GetConnection().Open();
                    }

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding staff: " + ex.Message);
                return false;
            }
            finally
            {
                _databaseConn.CloseConnection();
            }
        }
    }

}
