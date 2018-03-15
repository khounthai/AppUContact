using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.entity
{
    public class Donnee
    {
        [JsonProperty("ididdonnee")]
        private long iddonnee;
        [JsonProperty("idchamp")]
        private long idchamp;
        [JsonProperty("idcontact")]
        private long idcontact;
        [JsonProperty("valeur")]
        private String valeur;
        [JsonProperty("ordre")]
        private long ordre;
        [JsonProperty("dtenregistrement")]
        private long dtenregistrement;
        [JsonProperty("accueil")]
        private bool accueil;
        [JsonProperty("libellechamp")]
        private string libellechamp;

        public Donnee() { }

        public Donnee(long iddonnee, long idchamp, long idcontact, String valeur, DateTime dtenregistrement, long ordre, bool accueil,string libellechamp)
        {
            this.iddonnee = iddonnee;
            this.idchamp = idchamp;
            this.idcontact = idcontact;
            this.valeur = valeur;
            this.dtenregistrement = dtenregistrement.Ticks;
            this.ordre = ordre;
            this.accueil = accueil;
            this.libellechamp = libellechamp;
        }

        public long getIddonnee()
        {
            return iddonnee;
        }

        public void setIddonnee(long iddonnee)
        {
            this.iddonnee = iddonnee;
        }

        public long getIdchamp()
        {
            return idchamp;
        }

        public void setIdchamp(long idchamp)
        {
            this.idchamp = idchamp;
        }

        public long getIdcontact()
        {
            return idcontact;
        }

        public void setIdcontact(long idcontact)
        {
            this.idcontact = idcontact;
        }

        public String getValeur()
        {
            return valeur;
        }

        public void setValeur(String valeur)
        {
            this.valeur = valeur;
        }

        public long getDtenregistrement()
        {
            return dtenregistrement;
        }

        public void setDtenregistrement(long dtenregistrement)
        {
            this.dtenregistrement = dtenregistrement;
        }

        public long getOrdre()
        {
            return ordre;
        }

        public void setOrdre(long ordre)
        {
            this.ordre = ordre;
        }

        public bool isAccueil()
        {
            return accueil;
        }

        public void setAccueil(bool accueil)
        {
            this.accueil = accueil;
        }

        public string getLibellechamp()
        {
            return libellechamp;
        }

        public void setLibellechamp(string libellechamp)
        {
            this.libellechamp = libellechamp;
        }

        public override string ToString()
        {
            return "Donnee [iddonnee=" + iddonnee + ", idchamp=" + idchamp + ", idcontact=" + idcontact + ", valeur="
                    + valeur + ", ordre=" + ordre + ", dtenregistrement=" + dtenregistrement + ", accueil=" + accueil +", libellechamp=" +libellechamp + "]";
        }
    }

}
