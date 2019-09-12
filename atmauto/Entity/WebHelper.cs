using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;

namespace atmauto.Entity
{
    class WebHelper
    {
        public string Post(Uri url, string value)
        {
            Debug.WriteLine("web_api url_request = " + url);
            Debug.WriteLine("web_api post_request = " + value);

            var request = HttpWebRequest.Create(url);
            var byteData = Encoding.ASCII.GetBytes(value);
            request.ContentType = "application/json";
            request.Method = "POST";

            try
            {
                using (var stream = request.GetRequestStream())
                {
                    stream.Write(byteData, 0, byteData.Length);
                }
                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                Debug.WriteLine("web_api response = " + responseString);

                response.Close();

                return responseString;
            }
            catch (WebException e)
            {
                return null;
            }
        }

        public string Get(Uri url)
        {
            var request = HttpWebRequest.Create(url);
            request.ContentType = "application/json";
            request.Method = "GET";

            try
            {
                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                Debug.WriteLine("web_api response = " + responseString);
                Debug.WriteLine("web_api url_request = " + url);

                return responseString;
                
            }
            catch (WebException e)
            {
                return null;
            }
        }

        public DataTable json_convert(string json)
        {
           JObject json_object = JObject.Parse(json);
           DataTable dt = new DataTable();
           JArray json_array = JArray.Parse(json_object["data"].ToString());
           dt = JsonConvert.DeserializeObject<DataTable>(json_array.ToString());

           return dt;
        }

        public DataTable jsonConvert(string json)
        {
            JObject json_object = JObject.Parse(json);
            DataTable dt = new DataTable();
            JArray json_array = JArray.Parse(json_object["data"].ToString());
            foreach (JObject jobject in json_array.ToList())
            {
                jobject.Remove("detail_jasa");
                jobject.Remove("detail_sparepart");
                jobject.Remove("pegawai_on_duty");
            }
            dt = JsonConvert.DeserializeObject<DataTable>(json_array.ToString());

            return dt;
        }

        public DataTable jsonConvertProcurements(string json)
        {
            JObject json_object = JObject.Parse(json);
            DataTable dt = new DataTable();
            JArray json_array = JArray.Parse(json_object["data"].ToString());
            foreach (JObject jobject in json_array.ToList())
            {
                jobject.Remove("detail_pengadaan");
            }
            dt = JsonConvert.DeserializeObject<DataTable>(json_array.ToString());

            return dt;
        }

        public DataTable jsonConvertSpareparts(string json)
        {
            JObject json_object = JObject.Parse(json);
            DataTable dt = new DataTable();
            JArray json_array = JArray.Parse(json_object["data"].ToString());
            foreach (JObject jobject in json_array.ToList())
            {
                jobject.Remove("compatibility");
            }
            dt = JsonConvert.DeserializeObject<DataTable>(json_array.ToString());

            return dt;
        }

        public JObject jsonParse(string json)
        {
            JObject json_object = JObject.Parse(json);
            return json_object;
        }


        public string Update(Uri url, string value)
        {
            Debug.WriteLine("web_api url_request = " + url);
            Debug.WriteLine("web_api post_request = " + value);

            var request = HttpWebRequest.Create(url);
            var byteData = Encoding.ASCII.GetBytes(value);
            request.ContentType = "application/json";
            request.Method = "PATCH";

            try
            {
                using (var stream = request.GetRequestStream())
                {
                    stream.Write(byteData, 0, byteData.Length);
                }
                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                Debug.WriteLine("web_api response = " + responseString);

                response.Close();

                return responseString;
            }
            catch (WebException e)
            {
                return null;
            }
        }

        public string Delete(Uri url, string value)
        {
            Debug.WriteLine("web_api url_request = " + url);
            Debug.WriteLine("web_api post_request = " + value);

            var request = HttpWebRequest.Create(url);
            var byteData = Encoding.ASCII.GetBytes(value);
            request.ContentType = "application/json";
            request.Method = "DELETE";

            try
            {
                using (var stream = request.GetRequestStream())
                {
                    stream.Write(byteData, 0, byteData.Length);
                }
                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                Debug.WriteLine("web_api response = " + responseString);

                response.Close();

                return responseString;
            }
            catch (WebException e)
            {
                return null;
            }
        }
    }
}
