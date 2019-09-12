using atmauto.Entity;
using atmauto.Laporan;
using atmauto.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace atmauto.Boundary
{
    public partial class NotaLunasForm : Form
    {
        static WebHelper webHelper = new WebHelper();

        public
            NotaLunasForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridViewPayment_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string id = dataGridViewPayment.SelectedRows[0].Cells["Id_Transaksi"].Value.ToString();
            Debug.WriteLine(" " +id);
            FormCetakNotaLunas fm = new FormCetakNotaLunas(id);
            fm.Show();
        }

        private void NotaLunasForm_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void loadData()
        {
            Uri url = new Uri(string.Format("http://192.168.19.140/8708/api/transaksi_penjualans/transaksikeluar"));
            string response = webHelper.Get(url);
            DataTable dt = new DataTable();
            dt = webHelper.jsonConvert(response);
            dataGridViewPayment.DataSource = dt;
            dataGridViewPayment.Columns.Remove("Id_Konsumen");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
            NotaLunasForm p = new NotaLunasForm();
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
            this.Hide();
            this.Close();
            CashierForm p = new CashierForm();
            p.ShowDialog();
        }
    }
}
