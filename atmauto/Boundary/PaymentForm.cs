using atmauto.Entity;
using atmauto.Laporan;
using atmauto.UI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
    public partial class PaymentForm : Form
    {
        static WebHelper webHelper = new WebHelper();

        public PaymentForm()
        {
            InitializeComponent();
        }

        private void PaymentForm_Load(object sender, EventArgs e)
        {
            Uri url = new Uri(string.Format("http://192.168.19.140/8708/api/transaksi_penjualans/finished"));
            string response = webHelper.Get(url);
            DataTable dt = new DataTable();
            dt = webHelper.jsonConvert(response);
            dataGridViewPayment.DataSource = dt;
            dataGridViewPayment.Columns.Remove("Id_Konsumen");
        }

        private void buttonTransaction_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
            CashierForm p = new CashierForm();
            p.ShowDialog();
        }

        private void homeButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
            HomeForm1 p = new HomeForm1();
            p.ShowDialog();
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            WebHelper webHelper = new WebHelper();

            string id = txtSearch.Text;
            loadData();
            Uri url = new Uri(string.Format("http://192.168.19.140/8708/api/transaksi_penjualans/kasir_desktop/" + id));
            string response1 = webHelper.Get(url);

            dynamic data = JObject.Parse(response1);

            if (data.Id_Pegawai != null)
            {
                Debug.WriteLine("cek");
            }
            
            string request = JsonConvert.SerializeObject(data);
            Uri url1 = new Uri(string.Format("http://192.168.19.140/8708/api/transaksi_penjualans/pembayaran/" + id));
            string response = webHelper.Update(url1, request);

            MessageBox.Show("Pembayaran Berhasil", "Message");
            txtDiskon.Clear();
            txtSearch.Clear();
            txtTotal.Clear();
            loadData();


        }

        private void button4_Click(object sender, EventArgs e)
        {
            slidePanel.Height = button4.Height;
            slidePanel.Top = button4.Top;
            Application.Restart();
            LoginForm lg = new LoginForm();
            lg.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
        private void loadData()
        {
            Uri url = new Uri(string.Format("http://192.168.19.140/8708/api/transaksi_penjualans/finished"));
            string response = webHelper.Get(url);
            DataTable dt = new DataTable();
            dt = webHelper.jsonConvert(response);
            dataGridViewPayment.DataSource = dt;
            dataGridViewPayment.Columns.Remove("Id_Konsumen");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            /*string id = txtSearch.Text;
            loadData();

            try
            {
                if (txtSearch.Text.Trim() != "")
                {
                    foreach (DataGridViewRow row in dataGridViewPayment.Rows)
                    {
                        if (row.Cells[0].Value.ToString().Equals(id))
                        {
                            id = row.Cells[0].Value.ToString();
                            Debug.WriteLine("bind :" + id);

                            row.Selected = true;
                            //((DataTable)dataGridViewPayment.DataSource).DefaultView.RowFilter = string.Format("Id_Transaksi like '%{1}%'", txtSearch.Text.Trim().Replace("'", "''"));
                            break;
                        }
                    }
                    WebHelper webHelper = new WebHelper();
                    Uri url = new Uri(string.Format("http://127.0.0.1:8000/api/transaksi_penjualans/show" + id));
                    string response = webHelper.Get(url);

                    //dynamic data = JObject.Parse(response);

                    //if (data.Id_Transaksi != null)
                    //{
                    //  txtTotal.Text = data.Total;
                    //}
                    MessageBox.Show("Cek", "Message");
                }
                else
                {
                    loadData();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Transaction Not Found", "Message");

            }
        }*/
            try
            {
                string id = txtSearch.Text;
                loadData();

                WebHelper webHelper = new WebHelper();
                Uri url = new Uri(string.Format("http://192.168.19.140/8708/api/transaksi_penjualans/show/" + id));
                string response = webHelper.Get(url);

                dynamic data = JObject.Parse(response);

                if (data.Id_Transaksi != null)
                {
                    txtDiskon.Text = data.Diskon;
                    txtSubTotal.Text = data.Subtotal;
                    txtTotal.Text = data.Total;
                    txtTotal.Enabled = false;
                    txtDiskon.Enabled = false;
                    txtSubTotal.Enabled = false;
                }
                else if (data.Id_Pegawai == null)
                {
                    MessageBox.Show("Transaction Found", "Message");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Transaction Not Found", "Message");
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Uri url = new Uri(string.Format("http://192.168.19.140/8708/api/transaksi_penjualans/finished"));
            string response = webHelper.Get(url);
            DataTable dt = new DataTable();
            dt = webHelper.jsonConvert(response);
            dataGridViewPayment.DataSource = dt;
            dataGridViewPayment.Columns.Remove("Id_Konsumen");
        }

        private void buttonPaid_Click(object sender, EventArgs e)
        {
            Uri url = new Uri(string.Format("http://192.168.19.140/8708/api/transaksi_penjualans/transaksikeluar"));
            string response = webHelper.Get(url);
            DataTable dt = new DataTable();
            dt = webHelper.jsonConvert(response);
            dataGridViewPayment.DataSource = dt;
            dataGridViewPayment.Columns.Remove("Id_Konsumen");
        }

        private void dataGridViewPayment_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
            NotaLunasForm p = new NotaLunasForm();
            p.ShowDialog();
        }
    }
}
