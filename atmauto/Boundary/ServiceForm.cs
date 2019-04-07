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

        private void sendButton_Click(object sender, EventArgs e)
        {
            WebHelper webHelper = new WebHelper();

            Service sv = new Service();
            sv.Nama_Jasa = txtName.Text;
            sv.Harga_Jasa = double.Parse(txtPrice.Text);

            string request = JsonConvert.SerializeObject(sv);

            Uri url = new Uri(string.Format("http://atmauto.jasonfw.com/api/jasas/store"));

            string response = webHelper.Post(url, request);

            Clear();

            if (response != null)
            {
                //Handle your reponse here
                string message = "Service Success";
                string title = "Message";
                MessageBox.Show(message, title);
            }
            else
            {
                //No Response from the server
                string message = "Error Service";
                string title = "Message";
                MessageBox.Show(message, title);
            }
            loadData();
        }

        private void Clear()
        {
            txtName.Clear();
            txtPrice.Clear();
        }

        private void loadData()
        {
            WebHelper webHelper = new WebHelper();

            Uri url = new Uri(string.Format("http://atmauto.jasonfw.com/api/jasas"));

            string response = webHelper.Get(url);

            DataTable dt = new DataTable();
            dt = webHelper.json_convert(response);
            //Debug.WriteLine("data count = " + dt.Rows.Count);

            dataGridViewService.DataSource = dt;
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            string id = txtSearch.Text;
            WebHelper webHelper = new WebHelper();

            Service sv = new Service();
            // pg.Id_Role = comboBoxRole.Text;
            // pg.Id_Cabang = comboBoxBranch.Text;
            sv.Id_Jasa = txtSearch.Text;
            sv.Nama_Jasa = txtName.Text;
            sv.Harga_Jasa = double.Parse(txtPrice.Text);

            string request = JsonConvert.SerializeObject(sv);

            Uri url = new Uri(string.Format("http://atmauto.jasonfw.com/api/jasas/update/" + id));

            string response = webHelper.Update(url, request);

            Clear();

            if (response != null)
            {
                //Handle your reponse here
                string message = "Update Success";
                string title = "Message";
                MessageBox.Show(message, title);
            }
            else
            {
                //No Response from the server
                string message = "Error Update";
                string title = "Message";
                MessageBox.Show(message, title);
            }
            loadData();
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            string id = txtSearch.Text;
            loadData();
            Clear();

            WebHelper webHelper = new WebHelper();
            Uri url = new Uri(string.Format("http://atmauto.jasonfw.com/api/jasas/" + id));
            string response = webHelper.Get(url);

            dynamic data = JObject.Parse(response);

            if (data.Id_Jasa != null)
            {
                txtName.Text = data.Nama_Jasa;
                txtPrice.Text = data.Harga_Jasa;
            }
            else if (data.Id_Pegawai == null)
            {
                string message = "Jasa Not Found";
                string title = "Message";
                MessageBox.Show(message, title);
            }
            txtSearch.Clear();
        }

        private async void deleteButton_Click(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://atmauto.jasonfw.com/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                 new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                DialogResult res = MessageBox.Show("Are you sure want to delete this data?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (res == DialogResult.OK)
                {
                    HttpResponseMessage response = await client.DeleteAsync(
                        $"api/jasas/delete/{dataGridViewService.SelectedRows[0].Cells["Id_Jasa"].Value.ToString()}");
                    response.EnsureSuccessStatusCode();
                    loadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ServiceForm_Load(object sender, EventArgs e)
        {
            loadData();
        }
    }
}
