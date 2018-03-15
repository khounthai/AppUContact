using Contacts.entity;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.classes
{
    public class ApiContact
    {
        public static string GetStringJSonContacts(User user)
        {
            string url = @"http://localhost:8080//api-liste-contacts/";

            string postData = "iduser=" + user;

            return PostHttpReponse(url, user);            
        }

        public static string GetStringJSonTemplate(long iduser)
        {            
            string url = String.Format(@"http://localhost:8080/api-template/{0}", iduser);
            return GetHttpReponse(url);
        }

        public static string GetStringJSonUser(string login,string password)
        {
            string url = @"http://localhost:8080/api-get-user/"+login+"/"+password;       
            return GetHttpReponse(url);
        }

        private static string GetHttpReponse(string url)
        {
            String strResponse = string.Empty;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    strResponse = reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return strResponse;
        }


        private static string PostHttpReponse(string url,object obj)
        {
            String strResponse = string.Empty;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            try
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Converters.Add(new JavaScriptDateTimeConverter());
                serializer.NullValueHandling = NullValueHandling.Ignore;
           
                request.Method = "POST";
                request.ContentType = "application/json; charset=utf-8";

                string json = JsonConvert.SerializeObject(obj, Formatting.Indented);

                StreamWriter streamwriter = new StreamWriter(request.GetRequestStream());

                streamwriter.Write(json);
                streamwriter.Flush();

                var response = (HttpWebResponse)request.GetResponse();

                strResponse = new StreamReader(response.GetResponseStream()).ReadToEnd();

                streamwriter.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return strResponse;
        }
    }
}
