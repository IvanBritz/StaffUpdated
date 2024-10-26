using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Staff
{
    public class CellContentClick
    {
        public static CellContentClick Instance;

        private CellContentClick() { }

        public static CellContentClick GetInstance()
        {
            if (Instance == null)
            {
                Instance = new CellContentClick();  
            }

        return Instance; 
        }

        public void Perform(DataGridView dataGridView1, StaffRepository _staffRepo, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Delete")
            {
                int staffID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["StaffID"].Value);
                _staffRepo.DeleteStaff(staffID);
            }
        }
    }
}
