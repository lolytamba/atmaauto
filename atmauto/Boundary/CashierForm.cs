using atmauto.Entity;
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
    public partial class CashierForm : Form
    {
        static WebHelper webHelper = new WebHelper();

        public CashierForm()
        {
            InitializeComponent();
        }
        private void loadData()
        {
            Uri url = new Uri(string.Format("http://192.168.19.140/8708/api/transaksi_penjualans"));
            string response = webHelper.Get(url);
            DataTable dt = new DataTable();
            dt = webHelper.jsonConvert(response);
            dataGridViewTransaction.DataSource = dt;
            dataGridViewTransaction.Columns.Remove("Id_Konsumen");
        }

        private void menuPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void CashierForm_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            Uri url = new Uri(string.Format("http://192.168.19.140/8708/api/transaksi_penjualans/unprocessed"));
            string response = webHelper.Get(url);
            DataTable dt = new DataTable();
            dt = webHelper.jsonConvert(response);
            dataGridViewTransaction.DataSource = dt;
            dataGridViewTransaction.Columns.Remove("Id_Konsumen");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Uri url = new Uri(string.Format("http://192.168.19.140/8708/api/transaksi_penjualans/processed"));
            string response = webHelper.Get(url);
            DataTable dt = new DataTable();
            dt = webHelper.jsonConvert(response);
            dataGridViewTransaction.DataSource = dt;
            dataGridViewTransaction.Columns.Remove("Id_Konsumen");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Uri url = new Uri(string.Format("http://192.168.19.140/8708/api/transaksi_penjualans/finished"));
            string response = webHelper.Get(url);
            DataTable dt = new DataTable();
            dt = webHelper.jsonConvert(response);
            dataGridViewTransaction.DataSource = dt;
            dataGridViewTransaction.Columns.Remove("Id_Konsumen");
        }

        private void buttonPaid_Click(object sender, EventArgs e)
        {
            Uri url = new Uri(string.Format("http://127.0.0.1:8000/api/transaksi_penjualans/transaksikeluar"));
            string response = webHelper.Get(url);
            DataTable dt = new DataTable();
            dt = webHelper.jsonConvert(response);
            dataGridViewTransaction.DataSource = dt;
            dataGridViewTransaction.Columns.Remove("Id_Konsumen");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
            PaymentForm p = new PaymentForm();
            p.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            slidePanel.Height = button4.Height;
            slidePanel.Top = button4.Top;
            Application.Restart();
            LoginForm lg = new LoginForm();
            lg.ShowDialog();
        }

        private void buttonTransaction_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
            NotaLunasForm p = new NotaLunasForm();
            p.ShowDialog();
        }
    }
   
}
