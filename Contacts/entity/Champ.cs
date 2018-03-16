using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.entity
{

    public class Champ
    {
        [JsonProperty("idchamp")]
        private long idchamp;
        [JsonProperty("libelle")]
        private String libelle;
        [JsonProperty("multivaleur")]
        private bool multivaleur;
        [JsonProperty("iddatatype")]
        private long iddatatype;
        [JsonProperty("donnee")]
        private Donnee donnee;
        [JsonProperty("datatype")]
        private DataType datatype;
        [JsonProperty("preselection")]
        private List<String> preselection;
        [JsonProperty("preselectionsize")]
        private int preselectionsize;
        [JsonProperty("accueil")]
        private bool accueil;
        [JsonIgnore]
        public bool isValide { set; get; }

        public Champ() { }

        public Champ(long idchamp, String libelle, bool multivaleur, long iddatatype, Donnee donnee, DataType datatype,
                List<String> preselection, bool accueil)
        {

            this.idchamp = idchamp;
            this.libelle = libelle;
            this.multivaleur = multivaleur;
            this.iddatatype = iddatatype;
            this.donnee = donnee;
            this.datatype = datatype;
            this.preselection = preselection;
            this.accueil = accueil;
        }

        public long getIdchamp()
        {
            return idchamp;
        }

        public void setIdchamp(long idchamp)
        {
            this.idchamp = idchamp;
        }

        public String getLibelle()
        {
            return libelle;
        }

        public void setLibelle(String libelle)
        {
            this.libelle = libelle;
        }

        public bool getMultivaleur()
        {
            return multivaleur;
        }

        public void setMultivaleur(bool multivaleur)
        {
            this.multivaleur = multivaleur;
        }

        public long getIddatatype()
        {
            return iddatatype;
        }

        public void setIddatatype(long iddatatype)
        {
            this.iddatatype = iddatatype;
        }

        public Donnee getDonnee()
        {
            if (this.donnee == null)
            {
                this.donnee = new Donnee();
                this.donnee.setIdchamp(idchamp);
            }

            return donnee;
        }

        public void setDonnee(Donnee donnee)
        {
            this.donnee = donnee;
        }

        public DataType getDatatype()
        {
            return datatype;
        }

        public void setDatatype(DataType datatype)
        {
            this.datatype = datatype;
        }

        public List<String> getPreselection()
        {
            return preselection;
        }

        public void setPreselection(List<String> preselection)
        {
            this.preselection = preselection;
        }


        public int getPreselectionsize()
        {
            return preselection.Count;
        }

        public bool isAccueil()
        {
            return accueil;
        }

        public void setAccueil(bool accueil)
        {
            this.accueil = accueil;
        }

        
        public override string ToString()
        {
            return "Champ [idchamp=" + idchamp + ", libelle=" + libelle + ", multivaleur=" + multivaleur + ", iddatatype="
                    + iddatatype + ", donnee=" + donnee + ", datatype=" + datatype + ", preselection=" + preselection
                    + ", preselectionsize=" + preselectionsize + ", accueil=" + accueil + "]";
        }
    }
}