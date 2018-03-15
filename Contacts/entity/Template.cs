using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.entity
{
    public class Template
    {

        [JsonProperty("idtemplate")]
        private long idtemplate;
        [JsonProperty("libelle")]
        private String libelle;
        [JsonProperty("iduser")]
        private long iduser;
        [JsonProperty("champs")]
        private List<Champ> champs;
        [JsonProperty("check")]
        private bool check;

        public Template()
        {
        }

        public Template(long idtemplate, String libelle, long iduser, List<Champ> champs)
        {
            this.idtemplate = idtemplate;
            this.libelle = libelle;
            this.iduser = iduser;
            this.champs = champs;
        }

        public long getIdtemplate()
        {
            return idtemplate;
        }

        public void setIdtemplate(long idtemplate)
        {
            this.idtemplate = idtemplate;
        }

        public String getLibelle()
        {
            return libelle;
        }

        public void setLibelle(String libelle)
        {
            this.libelle = libelle;
        }

        public long getIduser()
        {
            return iduser;
        }

        public void setIduser(long iduser)
        {
            this.iduser = iduser;
        }

        public List<Champ> getChamps()
        {
            return champs;
        }

        public void setChamps(List<Champ> champs)
        {
            this.champs = champs;
        }


        public bool isCheck()
        {
            return check;
        }

        public void setCheck(bool check)
        {
            this.check = check;
        }


        public override string ToString()
        {
            return "Template [idtemplate=" + idtemplate + ", libelle=" + libelle + ", iduser=" + iduser + ", champs="
                    + champs + ", isCheck=" + check + "]";
        }

    }
}
