using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Staff
{
    public class TextBoxFilter
    {
        public static TextBoxFilter instance;
        private TextBoxFilter() { }

        public static TextBoxFilter GetInstance()
        {
            if (instance == null)
            {
                instance = new TextBoxFilter();
            }
            return instance;
        }
        public void Perform(TextBox tbSearch,DataGridView dataGridView1, StaffRepository _staffRepo)
        {
            string searchValue = tbSearch.Text.Trim();

            if (string.IsNullOrEmpty(searchValue))
            {
                dataGridView1.DataSource = _staffRepo.GetAllStaff();
                FormatDataGridView f = FormatDataGridView.GetInstance();
                f.Perform(dataGridView1);
            }
            else
            {
                dataGridView1.DataSource = _staffRepo.FilterStaff(searchValue);
            }
        }
    }
}
