using atmauto.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace atmauto.UI
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
        }
        
        private void RegisterForm_Load(object sender, EventArgs e)
        {
            panel1.Location =
                new Point(ClientSize.Width / 2 - panel1.Size.Width / 2,
                          ClientSize.Height / 2 - panel1.Size.Height / 2);
            panel1.Anchor = AnchorStyles.None;

            panel2.Location =
                new Point(ClientSize.Width - panel2.Size.Width);
            panel2.Anchor = AnchorStyles.None;

            panel3.Location =
                new Point(ClientSize.Width / 8 - panel3.Size.Width / 8,
                          ClientSize.Height - panel3.Size.Height);
            panel3.Anchor = AnchorStyles.None;

        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
        }

        private void signUpLabel_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
            LoginForm lg = new LoginForm();
            lg.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WebHelper webHelper = new WebHelper();

            Pegawai pg = new Pegawai();
            pg.Id_Role = "1";
            pg.Id_Cabang = "1";
            pg.Nama_Pegawai = txtNama.Text;
            pg.Alamat_Pegawai = txtAlamat.Text;
            pg.Telepon_Pegawai = txtTelephone.Text;
            pg.Gaji_Pegawai = 100000;
            pg.Username = txtUsername.Text;
            pg.Password = txtPassword.Text;

            string request = JsonConvert.SerializeObject(pg);

            Uri url = new Uri(string.Format("http://atmauto.jasonfw.com/api/pegawais/store"));
            string response = webHelper.Post(url, request);

            if (response != null)
            {
                MessageBox.Show("Admin Account Created", "Message");

                this.Hide();
                this.Close();
                LoginForm lg = new LoginForm();
                lg.ShowDialog();

                disableInput();
            }
            else
            {
                MessageBox.Show("Error Create Admin Account", "Message");
            }
            Clear();
        }

        private void disableInput()
        {
            txtNama.ReadOnly = true;
            txtAlamat.ReadOnly = true;
            txtTelephone.ReadOnly = true;
            txtUsername.ReadOnly = true;
            txtPassword.ReadOnly = true;
        }

        private void Clear()
        {
            txtNama.Clear();
            txtTelephone.Clear();
            txtAlamat.Clear();
            txtUsername.Clear();
            txtPassword.Clear();
        }
    }
}
