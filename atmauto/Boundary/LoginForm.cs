using atmauto.Boundary;
using atmauto.Entity;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace atmauto.UI
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }


        private void signUpLabel_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            RegisterForm rg = new RegisterForm();
            rg.ShowDialog();
            this.Close();
        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
        }

        private void buttonLogin_Click_1(object sender, EventArgs e)
        {
            if (txtUsername.Text.Trim() != "" && txtPassword.Text.Trim() != "")
            {
                getLogin(txtUsername.Text.ToString(), txtPassword.Text.ToString());
            }
        }

        static async void getLogin(string user, string pass)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://atmauto.jasonfw.com/");
            Login lg = new Login { Username = user, Password = pass };
            var response = client.PostAsJsonAsync("api/pegawais/mobileauthenticate", lg).Result;
            Debug.WriteLine(" " + user);

            if (response.IsSuccessStatusCode)
            {
                var a = await response.Content.ReadAsStringAsync();
                Debug.WriteLine($"{a}");
                Login.Data data = new Login.Data(a);
                Debug.WriteLine($"{data.name}");
                LoginForm.ActiveForm.Hide();
                HomeForm1 hm = new HomeForm1();
                hm.Show();
            }
            else
            {
                MessageBox.Show("Login Failed", "Message");
            }
        }
        private void LoginForm_Load(object sender, EventArgs e)
        {
            panel1.Location =
                new Point(ClientSize.Width / 2 - panel1.Size.Width / 2,
                          ClientSize.Height / 2 - panel1.Size.Height / 2);
            panel1.Anchor = AnchorStyles.None;

            panel3.Location =
                new Point(ClientSize.Width /8   - panel3.Size.Width /8 ,
                          ClientSize.Height  - panel3.Size.Height );
            panel3.Anchor = AnchorStyles.None;

            panel2.Location =
                new Point(ClientSize.Width  - panel2.Size.Width);
            panel2.Anchor = AnchorStyles.None;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
