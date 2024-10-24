using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Staff
{
    public interface IStaffRepository
    {
        DataTable GetAllStaff();
        DataTable FilterStaff(string searchValue);
        void UpdateStaff(int staffID, string staffName, decimal baseSalary);
        void DeleteStaff(int staffID);
        bool AddStaff(string staffName, int roleID, decimal baseSalary, string username, string password);
    }
}
