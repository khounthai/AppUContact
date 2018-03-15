using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.classes
{
    public class ApiContact
    {
        public static string GetStringJSonContacts(string login, string pwd)
        {
            String strJson = string.Empty;
            string url = @"http://localhost:8080/api-liste-contacts/{0}/{1}";

            url = string.Format(url, login, pwd);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    strJson = reader.ReadToEnd();
                }
            }
            catch ( Exception ex)
            {
                throw ex;
            }

            return strJson;
        }

        public static string GetStringJSonTemplate(long iduser)
        {
            String strJson = string.Empty;
            string url = @"http://localhost:8080/api-template/{0}";

            url = string.Format(url, iduser);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    strJson = reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return strJson;
        }

        public static string GetStringJSonUser(string login,string pwd)
        {
            String strJson = string.Empty;
            string url = @"http://localhost:8080/api-get_user/{0}/[1}";

            url = string.Format(url, iduser);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    strJson = reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return strJson;
        }
    }
}
