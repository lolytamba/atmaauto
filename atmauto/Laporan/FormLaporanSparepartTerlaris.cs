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
    public partial class FormLaporanSparepartTerlaris : Form
    {
        public LaporanSparepartTerlaris pb = new LaporanSparepartTerlaris();
        static WebHelper webHelper = new WebHelper();

        public FormLaporanSparepartTerlaris(string year)
        {
            InitializeComponent();
            Uri url = new Uri(string.Format("http://127.0.0.1:8000/api/sparepart_terlaris_desktop/" + year));
            string response = webHelper.Get(url);

            JObject jobject = new JObject();
            jobject = webHelper.jsonParse(response);
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            dt1 = JsonConvert.DeserializeObject<DataTable>(jobject.GetValue("data").ToString());

            DataColumn col = new DataColumn();
            col.DataType = System.Type.GetType("System.String");
            col.ColumnName = "year";
            dt2.Columns.Add(col);

            DataRow row;
            row = dt2.NewRow();
            row["year"] = year;
            dt2.Rows.Add(row);

            pb.Database.Tables["sparepart_terlaris"].SetDataSource(dt1);
            pb.Database.Tables["year"].SetDataSource(dt2);
            crystalReportViewer1.ReportSource = pb;
        }
        
    }
}
