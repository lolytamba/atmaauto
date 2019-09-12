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
    public partial class FormLaporanPengeluaranBulanan : Form
    {
        public LaporanPengeluaranBulananY pb = new LaporanPengeluaranBulananY();
        static WebHelper webHelper = new WebHelper();

        public FormLaporanPengeluaranBulanan(string tahun)
        {
            InitializeComponent();
            Uri url = new Uri(string.Format("http://127.0.0.1:8000/api/pengeluaran_bulanan_desktop/" + tahun));
            string response = webHelper.Get(url);

            JObject jobject = new JObject();

            jobject = webHelper.jsonParse(response);
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            dt1 = JsonConvert.DeserializeObject<DataTable>(jobject.GetValue("datas").ToString());

            DataColumn col = new DataColumn();
            col.DataType = System.Type.GetType("System.String");
            col.ColumnName = "year";
            dt2.Columns.Add(col);

            DataRow row;
            row = dt2.NewRow();
            row["year"] = tahun;
            dt2.Rows.Add(row);

            pb.Database.Tables["pengeluaran_bulanan"].SetDataSource(dt1);
            pb.Database.Tables["year"].SetDataSource(dt2);
            crystalReportViewer1.ReportSource = pb;
        }
    }
}
