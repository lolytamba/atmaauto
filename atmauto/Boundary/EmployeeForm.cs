using atmauto.UI;
using atmauto.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Net;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace atmauto.Boundary
{
    public partial class EmployeeForm : Form
    {
        public EmployeeForm()
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

        }

        private void buttonLogOut_Click(object sender, EventArgs e)
        {
            slidePanel.Height = buttonReport.Height;
            slidePanel.Top = buttonReport.Top;
            this.Close();
            this.Hide();
            LoginForm lg = new LoginForm();
            lg.ShowDialog();
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

        private void buttonReport_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Hide();
            ReportForm rf = new ReportForm();
            rf.ShowDialog();
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            string id = txtSearch.Text;
            WebHelper webHelper = new WebHelper();

            Pegawai pg = new Pegawai();
            // pg.Id_Role = comboBoxRole.Text;
            // pg.Id_Cabang = comboBoxBranch.Text;
            pg.Id_Pegawai = txtSearch.Text;
            pg.Nama_Pegawai = txtNama.Text;
            pg.Alamat_Pegawai = txtAlamat.Text;
            pg.Telepon_Pegawai = txtTelephone.Text;
            pg.Gaji_Pegawai = double.Parse(txtSalary.Text);
            

            string request = JsonConvert.SerializeObject(pg);

            Uri url = new Uri(string.Format("http://atmauto.jasonfw.com/api/pegawais/update/" + id));

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
            txtUsername.ReadOnly = false;
            txtPassword.ReadOnly = false;
            comboBoxRole.Enabled = true;
            comboBoxBranch.Enabled = true;
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            WebHelper webHelper = new WebHelper();
            
            Pegawai pg = new Pegawai();
            pg.Id_Role = comboBoxRole.Text;
            pg.Id_Cabang = comboBoxBranch.Text;
            pg.Nama_Pegawai = txtNama.Text;
            pg.Alamat_Pegawai = txtAlamat.Text;
            pg.Telepon_Pegawai = txtTelephone.Text;
            pg.Gaji_Pegawai = double.Parse(txtSalary.Text);
            pg.Username = txtUsername.Text;
            pg.Password = txtPassword.Text;

            string request = JsonConvert.SerializeObject(pg);

            Uri url = new Uri(string.Format("http://atmauto.jasonfw.com/api/pegawais/store"));
            string response = webHelper.Post(url, request);

            Clear();
            loadData();

            if (response != null)
            {
                //Handle your reponse here
                string message = "Pegawai Success";
                string title = "Message";
                MessageBox.Show(message, title);
            }
            else
            {
                //No Response from the server
                string message = "Error Pegawai";
                string title = "Close Window";
                MessageBox.Show(message, title);
            }
        }
        
        private void buttonLogOut_Click_1(object sender, EventArgs e)
        {
            slidePanel.Height = buttonLogOut.Height;
            slidePanel.Top = buttonLogOut.Top;
            Application.Restart();
            LoginForm lg = new LoginForm();
            lg.ShowDialog();
        }
        
        private void EmployeeForm_Load(object sender, EventArgs e)
        {
            loadData();
            getRole();
            getBranch();
        }

        private void getRole()
        {
            WebHelper webHelper = new WebHelper();

            Uri url = new Uri(string.Format("http://atmauto.jasonfw.com/api/roles"));

            string response = webHelper.Get(url);
            DataTable dt = new DataTable();
            dt = webHelper.json_convert(response);
            comboBoxRole.DataSource = dt;
            //comboBoxRole.DisplayMember = "Nama_Role";
            //comboBoxRole.ValueMember = "Id_Role";
            comboBoxRole.DisplayMember = "Id_Role";
        }

        private void getBranch()
        {
            WebHelper webHelper = new WebHelper();

            Uri url = new Uri(string.Format("http://atmauto.jasonfw.com/api/cabangs"));

            string response = webHelper.Get(url);
            DataTable dt = new DataTable();
            dt = webHelper.json_convert(response);
            comboBoxBranch.DataSource = dt;
            //comboBoxBranch.DisplayMember = "Nama_Cabang";
            //comboBoxBranch.ValueMember = "Id_Cabang";
            comboBoxBranch.DisplayMember = "Id_Cabang";
        }

        private void Clear()
        {
            //comboBoxBranch.DisplayMember = "";
            txtNama.Clear();
            txtAlamat.Clear();
            txtTelephone.Clear();
            txtUsername.Clear();
            txtPassword.Clear();
            txtSalary.Clear();
            txtTelephone.Clear();
        }
        
        private void searchButton_Click(object sender, EventArgs e)
        {
            string id = txtSearch.Text;
            comboBoxRole.Enabled = false;
            comboBoxBranch.Enabled = false;
            txtUsername.ReadOnly = true;
            txtPassword.ReadOnly = true;
            loadData();
            Clear();

            WebHelper webHelper = new WebHelper();
            Uri url = new Uri(string.Format("http://atmauto.jasonfw.com/api/pegawais/" + id));
            string response = webHelper.Get(url);

            dynamic data = JObject.Parse(response);
            
            if (data.Id_Pegawai != null)
            {
                comboBoxBranch.Text = data.Id_Cabang;
                comboBoxRole.Text = data.Id_Role;
                txtNama.Text = data.Nama_Pegawai;
                txtAlamat.Text = data.Alamat_Pegawai;
                txtTelephone.Text = data.Telepon_Pegawai;
                txtSalary.Text = data.Gaji_Pegawai;
                txtUsername.Text = data.Username;
                txtPassword.Text = data.Password;
            }
            else if(data.Id_Pegawai == null)
            {
                string message = "Pegawai Not Found";
                string title = "Message";
                MessageBox.Show(message, title);
            }
        }

        private void loadData()
        {
            WebHelper webHelper = new WebHelper();

            Uri url = new Uri(string.Format("http://atmauto.jasonfw.com/api/pegawais"));
           // Uri url = new Uri(string.Format("http://192.168.1.77:8000/api/pegawais/"));

            string response = webHelper.Get(url);

            DataTable dt = new DataTable();
            dt = webHelper.json_convert(response);
            //Debug.WriteLine("data count = " + dt.Rows.Count);

            dataGridViewTable.DataSource = dt;
        }

        private void disableInput()
        {
            comboBoxRole.Enabled = false;
            comboBoxBranch.Enabled = false;
            txtNama.ReadOnly = true;
            txtAlamat.ReadOnly = true;
            txtTelephone.ReadOnly = true;
            txtUsername.ReadOnly = true;
            txtPassword.ReadOnly = true;
            txtSalary.ReadOnly = true;
            txtTelephone.ReadOnly = true;
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
                        $"api/pegawais/delete/{dataGridViewTable.SelectedRows[0].Cells["Id_Pegawai"].Value.ToString()}");
                    response.EnsureSuccessStatusCode();
                    loadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
