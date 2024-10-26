using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Staff
{
    public class CellDoubleClick
    {
        public static CellDoubleClick instance;

        private CellDoubleClick() { }   

        public static CellDoubleClick GetInstance()
        {
            if(instance == null)
            {
                instance = new CellDoubleClick();
            }
        return instance; 
        }  

        public void Perform(DataGridView dataGridView1, StaffDetailsPopup popup, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int staffID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["StaffID"].Value);
                popup.ShowStaffDetailsPopup(staffID);
            }
        }
    }
}
