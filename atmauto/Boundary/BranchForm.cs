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
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.IO;
using atmauto.Entity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Web.Script.Serialization;


namespace atmauto.Boundary
{
    public partial class BranchForm : Form
    {
        public BranchForm()
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

        }

        private void homeButton_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Hide();
            HomeForm1 hm = new HomeForm1();
            hm.ShowDialog();
        }

        private void buttonReport_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Hide();
            ReportForm rf = new ReportForm();
            rf.ShowDialog();
        }

        private void buttonLogOut_Click_1(object sender, EventArgs e)
        {
            slidePanel.Height = buttonReport.Height;
            slidePanel.Top = buttonReport.Top;
            Application.Restart();
            LoginForm lg = new LoginForm();
            lg.ShowDialog();
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            WebHelper webHelper = new WebHelper();

            Cabang cb = new Cabang();
            cb.Nama_Cabang = txtName.Text;
            cb.Alamat_Cabang = txtAddress.Text;
            cb.Telepon_Cabang = txtTelephone.Text;

            string request = JsonConvert.SerializeObject(cb);

            // Uri url = new Uri(string.Format("http://atmauto.jasonfw.com/api/cabangs/store"));
            Uri url = new Uri(string.Format("http://10.53.12.16:8080/api/cabangs/store"));
        
            string response = webHelper.Post(url, request);

            Clear();

            if (response != null)
            {
                //Handle your reponse here
                string message = "Cabang Success";
                string title = "Message";
                MessageBox.Show(message, title);
            }
            else
            {
                //No Response from the server
                string message = "Error Cabang";
                string title = "Message";
                MessageBox.Show(message, title);
            }
            loadData();
        }

        public void Clear()
        {
            txtSearch.Clear();
            txtName.Clear();
            txtAddress.Clear();
            txtTelephone.Clear();
        }

        public void loadData()
        {
            WebHelper webHelper = new WebHelper();

            //Uri url = new Uri(string.Format("http://atmauto.jasonfw.com/api/cabangs"));
            Uri url = new Uri(string.Format("http://10.53.12.16:8080/api/cabangs"));

            string response = webHelper.Get(url);

            DataTable dt = new DataTable();
            dt = webHelper.json_convert(response);

            dataGridViewTable.DataSource = dt;
        }

        private void BranchForm_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            string id = txtSearch.Text;
            loadData();

            WebHelper webHelper = new WebHelper();
            //Uri url = new Uri(string.Format("http://atmauto.jasonfw.com/api/cabangs/" + id));
        
            Uri url = new Uri(string.Format("http://10.53.12.16:8080/api/cabangs/" + id));

            string response = webHelper.Get(url);

            dynamic data = JObject.Parse(response);

            if (data.Id_Cabang != null)
            {
                txtName.Text = data.Nama_Cabang;
                txtAddress.Text = data.Alamat_Cabang;
                txtTelephone.Text = data.Telepon_Cabang;
            }
            else if (data.Id_Cabang == null)
            {
                string message = "Cabang Not Found";
                string title = "Message";
                MessageBox.Show(message, title);
            }
        }

        public void disableInput()
        {
            txtName.ReadOnly = true;
            txtAddress.ReadOnly = true;
            txtTelephone.ReadOnly = true;
        }

        public void enableInput()
        {
            txtName.ReadOnly = false;
            txtAddress.ReadOnly = false;
            txtTelephone.ReadOnly = false;
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            loadData();
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            //enableInput();    
            string id = txtSearch.Text;
            WebHelper webHelper = new WebHelper();

            Cabang cb = new Cabang();
            cb.Id_Cabang = txtSearch.Text;
            cb.Nama_Cabang = txtName.Text;
            cb.Alamat_Cabang = txtAddress.Text;
            cb.Telepon_Cabang = txtTelephone.Text;

            string request = JsonConvert.SerializeObject(cb);

            // Uri url = new Uri(string.Format("http://atmauto.jasonfw.com/api/cabangs/update/" + id));
            Uri url = new Uri(string.Format("http://10.53.12.16:8080/api/cabangs/update/" + id));

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

        private async void deleteButton_Click(object sender, EventArgs e)
        {
           HttpClient client = new HttpClient();
            //client.BaseAddress = new Uri("http://atmauto.jasonfw.com/");
           client.BaseAddress = new Uri("http://10.53.12.16:8080");
           client.DefaultRequestHeaders.Accept.Clear();
           client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                DialogResult res = MessageBox.Show("Are you sure want to delete this data?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (res == DialogResult.OK)
                {
                    HttpResponseMessage response = await client.DeleteAsync(
                        $"api/cabangs/delete/{dataGridViewTable.SelectedRows[0].Cells["Id_Cabang"].Value.ToString()}");
                    response.EnsureSuccessStatusCode();
                    loadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
