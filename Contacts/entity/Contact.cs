using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Contacts.entity
{
    public class Contact
    {
        [JsonProperty("idcontact")]
        private long idcontact;
        [JsonProperty("dtcreation")]
        private DateTime dtcreation;
        [JsonProperty("favoris")]
        private Boolean favoris;
        [JsonProperty("iduser")]
        private long iduser;
        [JsonProperty("actif")]
        private bool actif;
        [JsonProperty("donnees")]
        private List<Donnee> donnees;

        public Contact()
        {
        }

        public Contact(long idcontact, DateTime dtcreation, Boolean favoris, long iduser, bool actif, List<Donnee> donnees)
        {
            this.idcontact = idcontact;
            this.dtcreation = dtcreation;
            this.favoris = favoris;
            this.iduser = iduser;
            this.donnees = donnees;
            this.actif = actif;
        }

        public long getIdcontact()
        {
            return idcontact;
        }

        public void setIdcontact(long idcontact)
        {
            this.idcontact = idcontact;
        }

        public long getIduser()
        {
            return iduser;
        }

        public void setIduser(long iduser)
        {
            this.iduser = iduser;
        }

        public Boolean getFavoris()
        {
            return favoris;
        }

        public void setFavoris(Boolean favoris)
        {
            this.favoris = favoris;
        }

        public List<Donnee> getDonnees()
        {
            return donnees;
        }

        public void setDonnees(List<Donnee> donnees)
        {
            this.donnees = donnees;
        }

        public bool getActif()
        {
            return actif;
        }

        public void setActif(bool actif)
        {
            this.actif = actif;
        }


        public DateTime getDtcreation()
        {
            return dtcreation;
        }

        public void setDtcreation(DateTime dtcreation)
        {
            this.dtcreation = dtcreation;
        }

        
        public override string ToString()
        {
            return "Contact [idcontact=" + idcontact + ", dtcreation=" + dtcreation + ", favoris=" + favoris + ", iduser="
                    + iduser + ", donnees=" + donnees + "]";
        }
    }
}
