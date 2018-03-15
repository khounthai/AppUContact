using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.entity
{
    public class User
    {
        [JsonProperty("iduser")]
        private long iduser;
        [JsonProperty("login")]
        private String login;

        private byte[] encryptedkey;
        private String validationkey; //clé de validation de compte	
        private Boolean validaccount = false;
        private byte[] hashedPassword;
        private DateTime dtcreation;
        private String role;
        private bool actif;
        private long timestampModifPwd;
        private byte[] encryptedkeypwd;

        public User()
        {
        }
        public User(long iduser, String login, byte[] encryptedkey, String validationkey, Boolean validaccount,
                byte[] hashedPassword, String role, bool actif, long timestampModifPwd,
                byte[] encryptedkeypwd, DateTime dtcreation)
        {

            this.iduser = iduser;
            this.login = login;
            this.encryptedkey = encryptedkey;
            this.validationkey = validationkey;
            this.validaccount = validaccount;
            this.hashedPassword = hashedPassword;
            this.dtcreation = dtcreation;
            this.role = role;
            this.actif = actif;
            this.timestampModifPwd = timestampModifPwd;
            this.encryptedkeypwd = encryptedkeypwd;
        }

        public void setLogin(String login)
        {
            this.login = login;
        }

        public void setHashedPassword(byte[] hashedPassword)
        {
            this.hashedPassword = hashedPassword;
        }

        public byte[] getHashedPassword()
        {
            return hashedPassword;
        }

        public DateTime getDtcreation()
        {
            return dtcreation;
        }

        public void setDtcreation(DateTime dtcreation)
        {
            this.dtcreation = dtcreation;
        }

        public String getRole()
        {
            return role;
        }

        public void setRole(String role)
        {
            this.role = role;
        }

        public String getValidationkey()
        {
            return validationkey;
        }

        public void setValidationkey(String validationkey)
        {
            this.validationkey = validationkey;
        }

        public Boolean getValidaccount()
        {
            return validaccount;
        }

        public void setValidaccount(Boolean validaccount)
        {
            this.validaccount = validaccount;

        }

        public byte[] getEncryptedkey()
        {
            return encryptedkey;
        }

        public void setEncryptedkey(byte[] encryptedkey)
        {
            this.encryptedkey = encryptedkey;
        }

        public long getIduser()
        {
            return iduser;
        }

        public void setIduser(long iduser)
        {
            this.iduser = iduser;
        }

        public String getLogin()
        {
            return login;
        }

        public bool isActif()
        {
            return actif;
        }

        public void setActif(bool actif)
        {
            this.actif = actif;
        }


        public long getTimestampModifPwd()
        {
            return timestampModifPwd;
        }

        public void setTimestampModifPwd(long timestampModifPwd)
        {
            this.timestampModifPwd = timestampModifPwd;
        }

        public byte[] getEncryptedkeypwd()
        {
            return encryptedkeypwd;
        }

        public void setEncryptedkeypwd(byte[] encryptedkeypwd)
        {
            this.encryptedkeypwd = encryptedkeypwd;
        }


        public override string ToString()
        {
            return "User [iduser=" + iduser + ", login=" + login + "]";
        }
    }
}