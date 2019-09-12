using atmauto.Entity;
using atmauto.UI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace atmauto.Boundary
{
    public partial class SparepartForm : Form
    {
        OpenFileDialog open = new OpenFileDialog();
        static WebHelper webHelper = new WebHelper();
        static HttpClient client = new HttpClient();

        public SparepartForm()
        {
            InitializeComponent();
            client.BaseAddress = new Uri("http://192.168.19.140/8708");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            open.Filter = "Image Files(*.jpg;*,.jpeg;)|*.jpg;*.jpeg:";
            if(open.ShowDialog() == DialogResult.OK)
            {
                gambarSparepart.Image = new Bitmap(open.FileName);
                txtGambar.Text = open.FileName;
            }
        }
        

        private void loadData()
        {
            WebHelper webHelper = new WebHelper();

            Uri url = new Uri(string.Format("http://192.168.19.140/8708/api/spareparts"));
            string response = webHelper.Get(url);

            DataTable dt = new DataTable();
            dt = webHelper.jsonConvertSpareparts(response);

            dataGridViewSparepart.DataSource = dt;
            //bool upload = true;
        }


        private void SparepartForm_Load(object sender, EventArgs e)
        {
            loadData();
            //bool upload = false;
        }

        private void dataGridViewSparepart_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void homeButton_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Hide();
            HomeForm1 tf = new HomeForm1();
            tf.ShowDialog();
        }

        private void buttonSparepart_Click(object sender, EventArgs e)
        {

        }

        private void serviceButton_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Hide();
            ServiceForm tf = new ServiceForm();
            tf.ShowDialog();
        }

        private void cabangButton_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Hide();
            BranchForm tf = new BranchForm();
            tf.ShowDialog();
        }

        private void buttonProcurements_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Hide();
            ProcurementsForm tf = new ProcurementsForm();
            tf.ShowDialog();
        }

        private void buttonReport_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Hide();
            ReportForm tf = new ReportForm();
            tf.ShowDialog();
        }

        private void buttonLogOut_Click(object sender, EventArgs e)
        {
            slidePanel.Height = buttonReport.Height;
            slidePanel.Top = buttonReport.Top;
            Application.Restart();
            LoginForm lg = new LoginForm();
            lg.ShowDialog();
        }


        private async void sendButton_Click(object sender, EventArgs e)
        {
            MultipartFormDataContent form = new MultipartFormDataContent();

            try
            {
                form.Add(new StringContent(txtKode.Text), "Kode_Sparepart");
                form.Add(new StringContent(txtName.Text), "Nama_Sparepart");
                form.Add(new StringContent(txtBrand.Text), "Merk_Sparepart");
                form.Add(new StringContent(txtType.Text), "Tipe_Barang");
                form.Add(new StringContent(txtType.Text), "Rak_Sparepart");
                form.Add(new StringContent(txtAmount.Text), "Jumlah_Sparepart");
                form.Add(new StringContent(txtStock.Text), "Stok_Minimum_Sparepart");
                form.Add(new StringContent(txtPrice.Text), "Harga_Beli");
                form.Add(new StringContent(txtSell.Text), "Harga_Jual");

                MemoryStream ms = new MemoryStream();
                gambarSparepart.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

                byte[] buff = ms.GetBuffer();
                form.Add(new ByteArrayContent(buff, 0, buff.Length), "Gambar", open.SafeFileName);

                Debug.WriteLine(form);

                var response = client.PostAsync("api/spareparts/storemobile", form).Result;

                var a = await response.Content.ReadAsStringAsync();
                Debug.WriteLine(a);
                loadData();
                Clear();

                MessageBox.Show("Sparepart Success", "Message");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Data Not Valid", "Message");
            }
        }

        private void Clear()
        {
            txtAmount.Clear();
            txtKode.Clear();
            txtName.Clear();
            txtBrand.Clear();
            txtPrice.Clear();
            txtSell.Clear();
            txtStock.Clear();
            txtGambar.Clear();
            txtType.Clear();
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            string id = txtSearch.Text;
            loadData();

            try
            {
                if (txtSearch.Text.Trim() != "")
                {
                    foreach (DataGridViewRow row in dataGridViewSparepart.Rows)
                    {
                        if (row.Cells[1].Value.ToString().Equals(id))
                        {
                            id = row.Cells[0].Value.ToString();
                            Debug.WriteLine("bind :" + id);

                            row.Selected = true;
                            ((DataTable)dataGridViewSparepart.DataSource).DefaultView.RowFilter = string.Format("Nama_Sparepart like '%{0}%'", txtSearch.Text.Trim().Replace("'", "''"));
                            break;
                        }
                    }
                    WebHelper webHelper = new WebHelper();
                    Uri url = new Uri(string.Format("http://192.168.19.140/8708/api/spareparts/" + id));
                    string response = webHelper.Get(url);

                    dynamic data = JObject.Parse(response);

                    if (data.Kode_Sparepart != null)
                    {
                        txtName.Text = data.Nama_Sparepart;
                        txtKode.Text = data.Kode_Sparepart;
                        txtPrice.Text = data.Harga_Beli;
                        txtType.Text = data.Tipe_Barang;
                        txtStock.Text = data.Stok_Minimum_Sparepart;
                        txtSell.Text = data.Harga_Jual;
                        txtBrand.Text = data.Harga_Beli;
                        txtGambar.Text = data.Gambar;
                        txtAmount.Text = data.Jumlah_Sparepart;
                    }
                    loadData();
                }
                else
                {
                    loadData();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Sparepart Not Found", "Message");

            }
        }

        private void pegawaiButton_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Hide();
            EmployeeForm tf = new EmployeeForm();
            tf.ShowDialog();
        }

        private async void deleteButton_Click(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();
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
                        $"api/spareparts/delete/{dataGridViewSparepart.SelectedRows[0].Cells["Kode_Sparepart"].Value.ToString()}");
                    response.EnsureSuccessStatusCode();
                    loadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void editButton_Click(object sender, EventArgs e)
        {
            MultipartFormDataContent form = new MultipartFormDataContent();
            string id = txtSearch.Text;
            foreach (DataGridViewRow row in dataGridViewSparepart.Rows)
            {
                if (row.Cells[1].Value.ToString().Equals(id))
                {
                    id = row.Cells[0].Value.ToString();
                    Debug.WriteLine("bind ini:" + id);

                    form.Add(new StringContent(txtKode.Text), "Kode_Sparepart");
                    form.Add(new StringContent(txtName.Text), "Nama_Sparepart");
                    form.Add(new StringContent(txtBrand.Text), "Merk_Sparepart");
                    form.Add(new StringContent(txtType.Text), "Tipe_Barang");
                    form.Add(new StringContent(txtType.Text), "Rak_Sparepart");
                    form.Add(new StringContent(txtAmount.Text), "Jumlah_Sparepart");
                    form.Add(new StringContent(txtStock.Text), "Stok_Minimum_Sparepart");
                    form.Add(new StringContent(txtPrice.Text), "Harga_Beli");
                    form.Add(new StringContent(txtSell.Text), "Harga_Jual");

                    MemoryStream ms = new MemoryStream();
                    gambarSparepart.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

                    byte[] buff = ms.GetBuffer();
                    form.Add(new ByteArrayContent(buff, 0, buff.Length), "Gambar", open.SafeFileName);

                    try
                    {
                        DialogResult res = MessageBox.Show("Are you sure want to update this data?", "Confirmation",
                                                            MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                        if (res == DialogResult.OK)
                        {
                            var response = client.PostAsync($"api/spareparts/updatemobile/{txtSearch.Text.ToString()}", form).Result;
                            var a = await response.Content.ReadAsStringAsync();
                            Debug.WriteLine(a);

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
        }
    }
}
