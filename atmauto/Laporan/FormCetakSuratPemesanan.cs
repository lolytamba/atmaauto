using atmauto.Entity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace atmauto.Laporan
{
    public partial class FormCetakSuratPemesanan : Form
    {
        public CetakSuratPemesanan SP = new CetakSuratPemesanan();
        static WebHelper webHelper = new WebHelper();

        public FormCetakSuratPemesanan(string id)
        {
            InitializeComponent();
            Uri url = new Uri(string.Format("http://127.0.0.1:8000/api/cetak_surat_pemesanan_desktop/" + id));
            string response = webHelper.Get(url);

            JObject jobject = new JObject();
            jobject = webHelper.jsonParse(response);
            DataTable dt1 = new DataTable();
           
            dt1 = JsonConvert.DeserializeObject<DataTable>(jobject.GetValue("datas").ToString());
            SP.Database.Tables["surat_pemesanan"].SetDataSource(dt1);
           
            crystalReportViewer1.ReportSource = SP;
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}
