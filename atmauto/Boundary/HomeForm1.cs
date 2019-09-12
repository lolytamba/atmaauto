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
    public partial class HomeForm1 : Form
    {
        public HomeForm1()
        {
            InitializeComponent();
        }

        private void pegawaiButton_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Hide();
            EmployeeForm hm = new EmployeeForm();
            hm.ShowDialog();
        }

        private void serviceButton_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Hide();
            ServiceForm sv = new ServiceForm();
            sv.ShowDialog();
        }

        private void cabangButton_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Hide(); 
            BranchForm br = new BranchForm();
            br.ShowDialog();
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

        private void buttonLogOut_Click(object sender, EventArgs e)
        {
            slidePanel.Height = buttonLogOut.Height;
            slidePanel.Top = buttonLogOut.Top;
            Application.Restart();
            LoginForm lg = new LoginForm();
            lg.ShowDialog();
        }
        

        private void buttonProcurements_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Hide();
            ProcurementsForm tf = new ProcurementsForm();
            tf.ShowDialog();
        }

        private void buttonSparepart_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Hide();
            SparepartForm tf = new SparepartForm();
            tf.ShowDialog();
        }

        private void homeButton_Click(object sender, EventArgs e)
        {

        }
    }
}

