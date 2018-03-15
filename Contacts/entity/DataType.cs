using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.entity 
{
    public class DataType
    {
        [JsonProperty("id")]
        private long id;
        [JsonProperty("libelle")]
        private string libelle;
        [JsonProperty("regex")]
        private String regex;
           


        public DataType() { }

        public DataType(long id, String libelle, String regex)
        {
            this.id = id;
            this.libelle = libelle;
            this.regex = regex;
        }

        public long getId()
        {
            return id;
        }

        public void setId(long id)
        {
            this.id = id;
        }

        public String getLibelle()
        {
            return libelle;
        }

        public void setLibelle(String libelle)
        {
            this.libelle = libelle;
        }

        public String getRegex()
        {
            return regex;
        }

        public void setRegex(String regex)
        {
            this.regex = regex;
        }


        public override string ToString()
        {
            return "DataType [id=" + id + ", libelle=" + libelle + ", regex=" + regex + "]";
        }

    }
}
