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
    public partial class FormLaporanSisaStok : Form
    {
        public LaporanSisaStokY pb = new LaporanSisaStokY();
        static WebHelper webHelper = new WebHelper();

        public FormLaporanSisaStok(string year,string tipe)
        {
            InitializeComponent();
            //Uri url = new Uri(string.Format("http://127.0.0.1:8000/api/sisa_stok_desktop/2019/'Sparepart%20Roda'"));
            Uri url = new Uri(string.Format("http://127.0.0.1:8000/api/sisa_stok_desktop/" +year+ "/"+ "'" +tipe+ "'"));
            string response = webHelper.Get(url);

            JObject jobject = new JObject();
            jobject = webHelper.jsonParse(response);
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            DataTable dt3 = new DataTable();
            dt1 = JsonConvert.DeserializeObject<DataTable>(jobject.GetValue("datas").ToString());

            DataColumn col = new DataColumn();
            DataColumn col1 = new DataColumn();
            col.DataType = System.Type.GetType("System.String");
            col1.DataType = System.Type.GetType("System.String");
            col.ColumnName = "year";
            col1.ColumnName = "tipe";
            dt2.Columns.Add(col);
            dt3.Columns.Add(col1);

            DataRow row;
            DataRow row1;
            row = dt2.NewRow();
            row1 = dt3.NewRow();
            row["year"] = year;
            row1["tipe"] = tipe;
            dt2.Rows.Add(row);
            dt3.Rows.Add(row1);

            pb.Database.Tables["sisa_stok"].SetDataSource(dt1);
            pb.Database.Tables["year"].SetDataSource(dt2);
            pb.Database.Tables["tipe"].SetDataSource(dt3);
            crystalReportViewer1.ReportSource = pb;
        }
    }
}
