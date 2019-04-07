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
using atmauto.Entity;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;

namespace atmauto.Boundary
{
    public partial class ServiceForm : Form
    {
        public ServiceForm()
        {
            InitializeComponent();
        }

        private void pegawaiButton_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Hide(); EmployeeForm hm = new EmployeeForm();
            hm.ShowDialog();
        }

        private void homeButton_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Hide();
            HomeForm1 hm = new HomeForm1();
            hm.ShowDialog();
        }

        private void cabangButton_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Hide(); BranchForm br = new BranchForm();
            br.ShowDialog();
        }
        
        private void buttonLogOut_Click_1(object sender, EventArgs e)
        {
            slidePanel.Height = buttonReport.Height;
            slidePanel.Top = buttonReport.Top;
            Application.Restart();
            LoginForm lg = new LoginForm();
            lg.ShowDialog();
        }

        private void buttonReport_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Hide(); ReportForm rf = new ReportForm();
            rf.ShowDialog();
        }
        
    }
}
