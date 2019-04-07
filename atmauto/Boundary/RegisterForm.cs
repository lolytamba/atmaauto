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
    }
}
