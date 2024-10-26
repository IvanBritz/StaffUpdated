using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Staff
{
    public class CellendEdit
    {
        public static CellendEdit instance;

        private CellendEdit() { }

        public static CellendEdit GetInstance() {

            if (instance == null)
            {
                instance = new CellendEdit();   
            }
            return instance;
        }

        public void Perform(DataGridView dataGridView1, StaffRepository _staffRepo, DataGridViewCellEventArgs e)
        {
            int staffID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["StaffID"].Value);
            string staffName = dataGridView1.Rows[e.RowIndex].Cells["StaffName"].Value.ToString();
            decimal baseSalary = Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells["BaseSalary"].Value);

            _staffRepo.UpdateStaff(staffID, staffName, baseSalary);
        }
    }
}
