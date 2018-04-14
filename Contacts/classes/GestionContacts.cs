using Contacts.entity;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.classes
{
    public class GestionContacts
    {
        private static JsonSerializerSettings setting = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            MissingMemberHandling = MissingMemberHandling.Ignore
        };

        public static List<Contact> GetContacts(string strJson)
        {
            List<Contact> contacts = null;
            var dateTimeConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd" };
            contacts = JsonConvert.DeserializeObject<List<Contact>>(strJson);
            return contacts;
        }

        public static Template GetTemplate(string strJson)
        {
            Template template= JsonConvert.DeserializeObject<Template>(strJson,setting);
            return template;
        }

        public static User GetUser(string strJson)
        {
            User user = JsonConvert.DeserializeObject<User>(strJson, setting);
            return user;
        }

    }
}
