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
    public partial class FormCetakNotaLunas : Form
    {
        public CetakNotaLunas NL = new CetakNotaLunas();
        static WebHelper webHelper = new WebHelper();

        public FormCetakNotaLunas(string id)
        {
            InitializeComponent();
            Uri url = new Uri(string.Format("http://127.0.0.1:8000/api/cetakNotaLunas/" + id));
            string response = webHelper.Get(url);

            JObject jobject = new JObject();
            jobject = webHelper.jsonParse(response);

            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            DataTable dt3 = new DataTable();
            DataTable dt4 = new DataTable();
            DataTable dt5 = new DataTable();
            DataTable dt6 = new DataTable();
            DataTable dt7 = new DataTable();
            DataTable dt8 = new DataTable();
            DataTable dt9 = new DataTable();
            DataTable dt10 = new DataTable();

            dt1 = JsonConvert.DeserializeObject<DataTable>(jobject.GetValue("data1").ToString());
            dt2 = JsonConvert.DeserializeObject<DataTable>(jobject.GetValue("data2").ToString());
            dt3 = JsonConvert.DeserializeObject<DataTable>(jobject.GetValue("data3").ToString());
            dt4 = JsonConvert.DeserializeObject<DataTable>(jobject.GetValue("data4").ToString());
            dt5 = JsonConvert.DeserializeObject<DataTable>(jobject.GetValue("data5").ToString());
            dt6 = JsonConvert.DeserializeObject<DataTable>(jobject.GetValue("data6").ToString());
            dt7 = JsonConvert.DeserializeObject<DataTable>(jobject.GetValue("data7").ToString());
            dt8 = JsonConvert.DeserializeObject<DataTable>(jobject.GetValue("data8").ToString());
            dt9 = JsonConvert.DeserializeObject<DataTable>(jobject.GetValue("data9").ToString());
            dt10 = JsonConvert.DeserializeObject<DataTable>(jobject.GetValue("data10").ToString());

            NL.Database.Tables["detail_spareparts_2"].SetDataSource(dt1);
            NL.Database.Tables["detail_jasas_new"].SetDataSource(dt2);
            NL.Database.Tables["konsumens"].SetDataSource(dt3);
            NL.Database.Tables["pegawai_on_duties_2"].SetDataSource(dt4);
            NL.Database.Tables["montirs"].SetDataSource(dt5);
            NL.Database.Tables["montirs"].SetDataSource(dt6);
            NL.Database.Tables["transaksi_penjualans"].SetDataSource(dt7);
            NL.Database.Tables["total_transaksi"].SetDataSource(dt8);
            NL.Database.Tables["motor_konsumens"].SetDataSource(dt9);
            NL.Database.Tables["kasir"].SetDataSource(dt10);

            crystalReportViewer1.ReportSource = NL;
        }
    }
}
