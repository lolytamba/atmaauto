using atmauto.Entity;
using atmauto.Laporan;
using atmauto.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace atmauto.Boundary
{
    public partial class CustomerServiceForm : Form
    {
        static WebHelper webHelper = new WebHelper();

        public CustomerServiceForm()
        {
            InitializeComponent();
        }

        private void buttonLogOut_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void dataGridViewTransaction_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string id = dataGridViewTransaction.SelectedRows[0].Cells["Id_Transaksi"].Value.ToString();
            FormCetakSuratPerintahKerja fm = new FormCetakSuratPerintahKerja(id);
            fm.Show();
            loadData();
        }

        private void CustomerForm_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void loadData()
        {
            Uri url = new Uri(string.Format("http://192.168.19.140/8708/api/transaksi_penjualans/unprocessed"));
            string response = webHelper.Get(url);
            DataTable dt = new DataTable();
            dt = webHelper.jsonConvert(response);
            dataGridViewTransaction.DataSource = dt;
        }

        private void homeButton_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Hide();
            HomeForm1 hm = new HomeForm1();
            hm.ShowDialog();
        }

        private void buttonTransaction_Click(object sender, EventArgs e)
        {
            slidePanel.Height = buttonTransaction.Height;
            slidePanel.Top = buttonTransaction.Top;
            Application.Restart();
            LoginForm lg = new LoginForm();
            lg.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            slidePanel.Height = button1.Height;
            slidePanel.Top = button1.Top;
            Application.Restart();
            LoginForm lg = new LoginForm();
            lg.ShowDialog();
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            Uri url = new Uri(string.Format("http://192.168.19.140/8708/api/transaksi_penjualans/unprocessed"));
            string response = webHelper.Get(url);
            DataTable dt = new DataTable();
            dt = webHelper.jsonConvert(response);
            dataGridViewTransaction.DataSource = dt;
            dataGridViewTransaction.Columns[0].Visible = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Uri url = new Uri(string.Format("http://192.168.19.140/8708/api/transaksi_penjualans/processed"));
            string response = webHelper.Get(url);
            DataTable dt = new DataTable();
            dt = webHelper.jsonConvert(response);
            dataGridViewTransaction.DataSource = dt;
            dataGridViewTransaction.Columns[0].Visible = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Uri url = new Uri(string.Format("http://192.168.19.140/8708/api/transaksi_penjualans/finished"));
            string response = webHelper.Get(url);
            DataTable dt = new DataTable();
            dt = webHelper.jsonConvert(response);
            dataGridViewTransaction.DataSource = dt;
            dataGridViewTransaction.Columns[0].Visible = false;
        }
    }
}
