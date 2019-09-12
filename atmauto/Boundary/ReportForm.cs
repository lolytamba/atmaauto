using System;
using atmauto.UI;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using atmauto.Laporan;
using System.Diagnostics;

namespace atmauto.Boundary
{
    public partial class ReportForm : Form
    {
        public ReportForm()
        {
            InitializeComponent();
        }

        private void buttonReport_Click(object sender, EventArgs e)
        {
            slidePanel.Height = buttonReport.Height;
            slidePanel.Top = buttonReport.Top;
            this.Close();
            this.Hide();
            ReportForm rf = new ReportForm();
            rf.ShowDialog();
        }

        private void cabangButton_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Hide();
            BranchForm br = new BranchForm();
            br.ShowDialog();
        }

        private void serviceButton_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Hide();
            ServiceForm sv = new ServiceForm();
            sv.ShowDialog();
        }

        private void pegawaiButton_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Hide();
            EmployeeForm hm = new EmployeeForm();
            hm.ShowDialog();
        }

        private void homeButton_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Hide();
            HomeForm1 hm = new HomeForm1();
            hm.ShowDialog();
        }

        private void buttonLogOut_Click(object sender, EventArgs e)
        {
            slidePanel.Height = buttonReport.Height;
            slidePanel.Top = buttonReport.Top;
            Application.Restart();
            LoginForm lg = new LoginForm();
            lg.ShowDialog();
        }

        private void buttonPenjualanJasa_Click(object sender, EventArgs e)
        {
            string year = txtYear.Text;
            string month = txtMonth.Text;

            FormLaporanPenjualanJasa FLPJ = new FormLaporanPenjualanJasa(year,month);
            FLPJ.ShowDialog();
            Clear();
            txtMonth.Enabled = false;
        }

        private void Clear()
        {
            txtYear.Clear();
            txtMonth.Clear();
            txtType.Clear();
        }
          
        private void button3_Click(object sender, EventArgs e)
        {
            string year = txtYear.Text;
            FormLaporanPendapatanBulanan FLPJ = new FormLaporanPendapatanBulanan(year);
            FLPJ.Show();
            Clear();
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            string tahun = txtYear.Text;
            FormLaporanPengeluaranBulanan LP = new FormLaporanPengeluaranBulanan(tahun);
            LP.ShowDialog();
            Clear();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string year = txtYear.Text;
            string type = txtType.Text;
            FormLaporanSisaStok LP = new FormLaporanSisaStok(year,type);
            LP.ShowDialog();
            Clear();
            txtYear.Enabled = false;
            txtType.Enabled = false;
        }

        private void buttonST_Click(object sender, EventArgs e)
        {
            string year = txtYear.Text;
            FormLaporanSparepartTerlaris LP = new FormLaporanSparepartTerlaris(year);
            LP.ShowDialog();
            Clear();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            txtYear.Enabled = true;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void buttonProcurements_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Hide();
            ProcurementsForm br = new ProcurementsForm();
            br.ShowDialog();
        }

        private void buttonSparepart_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Hide();
            SparepartForm br = new SparepartForm();
            br.ShowDialog();
        }

        private void ReportForm_Load(object sender, EventArgs e)
        {
            txtYear.ReadOnly = true;
            txtMonth.ReadOnly = true;
            txtType.ReadOnly = true;
        }

        private void label3_Click(object sender, EventArgs e)
        {
            txtYear.ReadOnly = false;
        }

        private void label12_Click(object sender, EventArgs e)
        {
            txtYear.ReadOnly = false;
        }

        private void label11_Click(object sender, EventArgs e)
        {
            txtYear.ReadOnly = false;
            txtMonth.ReadOnly = false;
        }

        private void label8_Click(object sender, EventArgs e)
        {
            txtYear.ReadOnly = false;
            txtType.ReadOnly = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FormLaporanPendapatanTahunan FP = new FormLaporanPendapatanTahunan();
            FP.ShowDialog();
        }
    }
}

