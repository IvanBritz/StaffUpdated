using System.Data;
using System.Data.SqlClient;
namespace Staff
{
    public partial class Form1 : Form
    {
        private StaffRepository _staffRepo = new StaffRepository();
        public Form1()
        {
            InitializeComponent();
            _staffRepo.DataChanged += (s, e) => LoadData();
            LoadData();
            dataGridView1.CellEndEdit += dataGridView1_CellEndEdit;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            dataGridView1.CellDoubleClick += dataGridView1_CellDoubleClick;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            tbSearch.TextChanged += tbSearch_TextChanged;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Form addForm = FormFactory.CreateAddForm(this);
            addForm.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CellContentClick c = CellContentClick.GetInstance();
            c.Perform(dataGridView1,_staffRepo,e);
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            CellDoubleClick c = CellDoubleClick.GetInstance();
            c.Perform(dataGridView1, _staffRepo, e);
        }

        
        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            TextBoxFilter t = TextBoxFilter.GetInstance();
            t.Perform(tbSearch, dataGridView1, _staffRepo);
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            CellendEdit c = CellendEdit.GetInstance();
            c.Perform(dataGridView1, _staffRepo,e);
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void LoadData()
        {
            _staffRepo.LoadData(dataGridView1, _staffRepo);
        }

    }
    }

