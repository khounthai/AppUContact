using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.entity
{
    public class ContactWrapper : Contact
    {
        [JsonProperty("contact")]
        private Contact contact;
        [JsonProperty("idtemplate")]
        private long idtemplate;

        public ContactWrapper() { }

        public long getIdtemplate()
        {
            return idtemplate;
        }

        public void setIdtemplate(long idtemplate)
        {
            this.idtemplate = idtemplate;
        }

        public Contact getContact()
        {
            return contact;
        }

        public void setContact(Contact contact)
        {
            this.contact = contact;
        }
    }
}
