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
        static WebHelper webHelper = new WebHelper();
        
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
            foreach (DataGridViewRow row in dataGridViewTable.Rows)
            {
                if (row.Cells[3].Value.ToString().Equals(id))
                {
                    id = row.Cells[2].Value.ToString();
                    Debug.WriteLine("bind 1:" + id);

                    Pegawai pg = new Pegawai();
                    pg.Id_Pegawai = txtSearch.Text;
                    pg.Nama_Pegawai = txtNama.Text;
                    pg.Alamat_Pegawai = txtAlamat.Text;
                    pg.Telepon_Pegawai = txtTelephone.Text;
                    pg.Gaji_Pegawai = double.Parse(txtSalary.Text);

                    try
                    {
                        DialogResult res = MessageBox.Show("Are you sure want to update this data?", "Confirmation", 
                                                            MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                        if (res == DialogResult.OK)
                        {
                            string request = JsonConvert.SerializeObject(pg);
                            Uri url = new Uri(string.Format("http://192.168.19.140/8708/api/pegawais/update/" + id));
                            string response = webHelper.Update(url, request);
                            Clear();
                            loadData();
                            MessageBox.Show("Update Success", "Message");
                            txtUsername.ReadOnly = false;
                            txtPassword.ReadOnly = false;
                            comboBoxRole.Enabled = true;
                            comboBoxBranch.Enabled = true;
                            txtSearch.Clear();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Update Error", "Message");
                    }
                }
                break;
            }
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            try
            {
                Pegawai pg = new Pegawai();
                pg.Id_Role = comboBoxRole.SelectedValue.ToString();
                pg.Id_Cabang = comboBoxBranch.SelectedValue.ToString();
                pg.Nama_Pegawai = txtNama.Text;
                pg.Alamat_Pegawai = txtAlamat.Text;
                pg.Telepon_Pegawai = txtTelephone.Text;
                pg.Gaji_Pegawai = double.Parse(txtSalary.Text);
                pg.Username = txtUsername.Text;
                pg.Password = txtPassword.Text;

                string request = JsonConvert.SerializeObject(pg);
                Uri url = new Uri(string.Format("http://192.168.19.140/8708/api/pegawais/store"));
                //Uri url = new Uri(string.Format("http://10.53.4.85:8000/api/pegawais/store"));
                string response = webHelper.Post(url, request);

                Clear();
                loadData();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Data Not Valid", "Message");
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
            //Uri url = new Uri(string.Format("http://atmauto.jasonfw.com/api/roles"));
            Uri url = new Uri(string.Format("http://192.168.19.140/8708/api/roles"));

            string response = webHelper.Get(url);

            DataTable dt = new DataTable();
            dt = webHelper.json_convert(response);
            comboBoxRole.DataSource = dt;
            comboBoxRole.DisplayMember = "Nama_Role";
            comboBoxRole.ValueMember = "Id_Role";
        }

        private void getBranch()
        {
            //Uri url = new Uri(string.Format("http://atmauto.jasonfw.com/api/cabangs"));
            Uri url = new Uri(string.Format("http://192.168.19.140/8708/api/cabangs"));
            string response = webHelper.Get(url);
           
            DataTable dt = new DataTable();
            dt = webHelper.json_convert(response);
            comboBoxBranch.DataSource = dt;
            comboBoxBranch.ValueMember = "Id_Cabang";
            comboBoxBranch.DisplayMember = "Nama_Cabang";
        }
        
        private void searchButton_Click(object sender, EventArgs e)
        {
            loadData();
            Clear();
            
            string id = txtSearch.Text;
            //loadData();

            try
            {
                if (txtSearch.Text.Trim() != "")
                {
                    foreach (DataGridViewRow row in dataGridViewTable.Rows)
                    {
                        if (row.Cells[3].Value.ToString().Equals(id))
                        {
                            id = row.Cells[2].Value.ToString();
                            Debug.WriteLine("bind :" + id);

                            row.Selected = true;
                            ((DataTable)dataGridViewTable.DataSource).DefaultView.RowFilter = string.Format("Nama_Pegawai like '%{0}%'", txtSearch.Text.Trim().Replace("'", "''"));
                            break;
                        }
                    }
                    Uri url = new Uri(string.Format("http://192.168.19.140/8708/api/pegawais/" + id));
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
                        comboBoxRole.Enabled = false;
                        comboBoxBranch.Enabled = false;
                        txtUsername.ReadOnly = true;
                        txtPassword.ReadOnly = true;
                    }
                }
                else
                {
                    loadData();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Employee Not Found", "Message");
                txtSearch.Clear();
            }
        }

        private void loadData()
        {
            Uri url = new Uri(string.Format("http://192.168.19.140/8708/api/pegawais"));
            string response = webHelper.Get(url);

            DataTable dt = new DataTable();
            dt = webHelper.json_convert(response);
            dataGridViewTable.DataSource = dt;
            dataGridViewTable.Columns.Remove("Id_Role");
            dataGridViewTable.Columns.Remove("Password");
            dataGridViewTable.Columns.Remove("Username");
            dataGridViewTable.Columns.Remove("Id_Cabang");
        }
        
        private async void deleteButton_Click(object sender, EventArgs e)
        {
             HttpClient client = new HttpClient();
            //client.BaseAddress = new Uri("http://192.168.19.140/8708/");
            client.BaseAddress = new Uri("http://192.168.19.140/8708");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                 new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                DialogResult res = MessageBox.Show("Are you sure want to delete this data?", "Confirmation",
                                                    MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
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

        private void Clear()
        {
            comboBoxBranch.SelectedIndex = -1;
            comboBoxRole.SelectedIndex = -1;
            txtNama.Clear();
            txtAlamat.Clear();
            txtTelephone.Clear();
            txtUsername.Clear();
            txtPassword.Clear();
            txtSalary.Clear();
            txtTelephone.Clear();
        }

        private void dataGridViewTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void buttonSparepart_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Hide();
            SparepartForm br = new SparepartForm();
            br.ShowDialog();
        }

        private void buttonProcurements_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Hide();
            ProcurementsForm br = new ProcurementsForm();
            br.ShowDialog();
        }

        private void menuPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pegawaiButton_Click(object sender, EventArgs e)
        {

        }
    }
}
