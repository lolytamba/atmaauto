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
    public partial class FormLaporanPenjualanJasa : Form
    {
        public LaporanPenjualanJasaY pb = new LaporanPenjualanJasaY();
        static WebHelper webHelper = new WebHelper();

        public FormLaporanPenjualanJasa(string year, string month)
        {
            InitializeComponent();
            
            Uri url = new Uri(string.Format("http://127.0.0.1:8000/api/penjualan_jasa_desktop/"+year +"/" +"'" +month + "'"));
            //Uri url = new Uri(string.Format("http://10.53.11.59:8000/api/penjualan_jasa_desktop/2019/'May'"));
            string response = webHelper.Get(url);

            JObject jobject = new JObject();
            jobject = webHelper.jsonParse(response);
            DataTable dt1 = new DataTable();
            dt1 = JsonConvert.DeserializeObject<DataTable>(jobject.GetValue("datas").ToString());

            pb.Database.Tables["penjualan_jasa"].SetDataSource(dt1);
            crystalReportViewer1.ReportSource = pb;
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}
