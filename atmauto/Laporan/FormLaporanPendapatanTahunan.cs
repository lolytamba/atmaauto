using atmauto.Entity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace atmauto.Laporan
{
    public partial class FormLaporanPendapatanTahunan : Form
    {
        public LaporanPendapatanTahunanY pb = new LaporanPendapatanTahunanY();
        static WebHelper webHelper = new WebHelper();

        public FormLaporanPendapatanTahunan()
        {
            InitializeComponent();
            Debug.WriteLine("Masuk sini lho");
            Uri url = new Uri(string.Format("http://127.0.0.1:8000/api/pendapatan_tahunan_desktop/"));
            string response = webHelper.Get(url);

            JObject jobject = new JObject();
            jobject = webHelper.jsonParse(response);
            DataTable dt1 = new DataTable();
            dt1 = JsonConvert.DeserializeObject<DataTable>(jobject.GetValue("datas").ToString());

            pb.Database.Tables["pendapatan_tahunan"].SetDataSource(dt1);
            crystalReportViewer1.ReportSource = pb;
        }
    }
}
