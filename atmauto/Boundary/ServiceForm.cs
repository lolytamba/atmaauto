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
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Diagnostics;

namespace atmauto.Boundary
{
    public partial class ServiceForm : Form
    {
        static WebHelper webHelper = new WebHelper();
         
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
            try
            {
                Service sv = new Service();
                sv.Nama_Jasa = txtName.Text;
                sv.Harga_Jasa = double.Parse(txtPrice.Text);

                string request = JsonConvert.SerializeObject(sv);
                Uri url = new Uri(string.Format("http://192.168.19.140/8708/api/jasas/store"));
                //Uri url = new Uri(string.Format("http://10.53.4.85:8000/api/jasas/store"));
                string response = webHelper.Post(url, request);

                Clear();
                loadData();

                MessageBox.Show("Service Success", "Message");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Data Not Valid","Message");
            }
        }

        private async void deleteButton_Click(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();

            //client.BaseAddress = new Uri("http://10.53.4.85:8000");
            client.BaseAddress = new Uri("http://192.168.19.140/8708");
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

        private void editButton_Click(object sender, EventArgs e)
        {
            string id = txtSearch.Text;
            foreach (DataGridViewRow row in dataGridViewService.Rows)
            {
                if (row.Cells[1].Value.ToString().Equals(id))
                {
                    id = row.Cells[0].Value.ToString();
                    Debug.WriteLine("bind :" + id);

                    try
                    {
                        DialogResult res = MessageBox.Show("Are you sure want to update this data?", "Confirmation",
                                                            MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                        if (res == DialogResult.OK)
                        {

                            Service sv = new Service();
                            sv.Id_Jasa = txtSearch.Text;
                            sv.Nama_Jasa = txtName.Text;
                            sv.Harga_Jasa = double.Parse(txtPrice.Text);

                            string request = JsonConvert.SerializeObject(sv);
                            Uri url = new Uri(string.Format("http://192.168.19.140/8708/api/jasas/update/" + id));
                            string response = webHelper.Update(url, request);

                            Clear();
                            loadData();
                            txtSearch.Clear();
                            MessageBox.Show("Update Success", "Message");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Update Error", "Message");
                    }
                }
                break;
            }
            
            txtSearch.Clear();
            sendButton.Enabled = true;
            deleteButton.Enabled = true;
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            string id = txtSearch.Text;
            loadData();

            try
            {
                if (txtSearch.Text.Trim() != "")
                {
                    foreach (DataGridViewRow row in dataGridViewService.Rows)
                    {
                        if (row.Cells[1].Value.ToString().Equals(id))
                        {
                            id = row.Cells[0].Value.ToString();
                            Debug.WriteLine("bind :" + id);

                            row.Selected = true;
                            ((DataTable)dataGridViewService.DataSource).DefaultView.RowFilter = string.Format("Nama_Jasa like '%{0}%'", txtSearch.Text.Trim().Replace("'", "''"));
                            break;
                        }
                    }
                    WebHelper webHelper = new WebHelper();
                    Uri url = new Uri(string.Format("http://192.168.19.140/8708/api/jasas/" + id));
                    string response = webHelper.Get(url);

                    dynamic data = JObject.Parse(response);

                    if (data.Id_Jasa != null)
                    {
                        txtName.Text = data.Nama_Jasa;
                        txtPrice.Text = data.Harga_Jasa;
                    }
                }
                else
                {
                    loadData();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Service Not Found", "Message");

            }
        }

        private void loadData()
        {
            WebHelper webHelper = new WebHelper();
            Uri url = new Uri(string.Format("http://192.168.19.140/8708/api/jasas"));
            string response = webHelper.Get(url);
            DataTable dt = new DataTable();
            dt = webHelper.json_convert(response);
            dataGridViewService.DataSource = dt;
        }

        private void Clear()
        {
            txtName.Clear();
            txtPrice.Clear();
        }

        private void ServiceForm_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void buttonSparepart_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Hide(); SparepartForm hm = new SparepartForm();
            hm.ShowDialog();
        }

        private void buttonProcurements_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Hide(); ProcurementsForm br = new ProcurementsForm();
            br.ShowDialog();
        }
        
    }
}
