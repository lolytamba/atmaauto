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
    public partial class ProcurementsForm : Form
    {
        static WebHelper webHelper = new WebHelper();

        public ProcurementsForm()
        {
            InitializeComponent();
        }

        private void homeButton_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Hide();
            HomeForm1 hm = new HomeForm1();
            hm.ShowDialog();
        }

        private void pegawaiButton_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Hide();
            EmployeeForm hm = new EmployeeForm();
            hm.ShowDialog();
        }

        private void buttonSparepart_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Hide();
            SparepartForm hm = new SparepartForm();
            hm.ShowDialog();
        }

        private void serviceButton_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Hide();
            ServiceForm hm = new ServiceForm();
            hm.ShowDialog();

        }

        private void cabangButton_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Hide();
            BranchForm hm = new BranchForm();
            hm.ShowDialog();
        }

        private void buttonReport_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Hide();
            ReportForm hm = new ReportForm();
            hm.ShowDialog();

        }

        private void buttonLogOut_Click(object sender, EventArgs e)
        {
            slidePanel.Height = buttonLogOut.Height;
            slidePanel.Top = buttonLogOut.Top;
            Application.Restart();
            LoginForm lg = new LoginForm();
            lg.ShowDialog();
        }

        private void loadData()
        {
            Uri url = new Uri(string.Format("http://192.168.19.140/8708/api/transaksi_pengadaans/ordered"));
            string response = webHelper.Get(url);
            DataTable dt = new DataTable();
            dt = webHelper.jsonConvertProcurements(response);
            dataGridViewProcurements.DataSource = dt;
            dataGridViewProcurements.Columns[0].Visible = true;
        }

        private void menuPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridViewProcurements_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string id = dataGridViewProcurements.SelectedRows[0].Cells["Id_Pengadaan"].Value.ToString();
            FormCetakSuratPemesanan fm = new FormCetakSuratPemesanan(id);
            fm.Show();
            loadData();
        }

        private void ProcurementsForm_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Uri url = new Uri(string.Format("http://192.168.19.140/8708/api/transaksi_pengadaans/printed"));
            string response = webHelper.Get(url);
            DataTable dt = new DataTable();
            dt = webHelper.jsonConvertProcurements(response);
            dataGridViewProcurements.DataSource = dt;
            dataGridViewProcurements.Columns[0].Visible = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Uri url = new Uri(string.Format("http://192.168.19.140/8708/api/transaksi_pengadaans/transaksimasuk"));
            string response = webHelper.Get(url);
            DataTable dt = new DataTable();
            dt = webHelper.jsonConvertProcurements(response);
            dataGridViewProcurements.DataSource = dt;
            dataGridViewProcurements.Columns[0].Visible = false;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            loadData();
        }
    }
}
