using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Contacts.entity;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Contacts.classes;

namespace Contacts
{
    public partial class ListeContactsForm : Form
    {
        private HttpClient client = new HttpClient();
        private User user;

        public ListeContactsForm()
        {
            InitializeComponent();
            toolStripLabelInfo.Text = "";
            user = null;
        }

        private void buttonConnexion_Click(object sender, EventArgs e)
        {
            toolStripLabelInfo.Text = "";
            dataGridView1.DataSource = null;
            user = null;

            try
            {
                string strJson = ApiContact.GetStringJSonUser(textBoxLogin.Text, textBoxPassword.Text);
                user = GestionContacts.GetUser(strJson);

                if (user==null)
                {
                    toolStripLabelInfo.Text = "Utilisateur non valide.";                   
                }

                strJson = ApiContact.GetStringJSonContacts(user);

                List<Contact> contacts = GestionContacts.GetContacts(strJson);
                
                SetDataGridView(GetDataTableFromListContacts(contacts));
            }
            catch(Exception ex)
            {
                toolStripLabelInfo.Text = "Erreur de connexion: " + ex.Message;
            }
        }
     
        private void SetDataGridView(DataTable table)
        {
            this.dataGridView1.DataSource = table;

            //cache la colonne des idcontact
            if (table!=null)
                this.dataGridView1.Columns["idcontact"].Visible = false;
        }

        private DataTable GetDataTableFromListContacts(List<Contact> contacts)
        {
            if (contacts == null || contacts.Count == 0)
            {
                return null;
            }

            DataTable table = new DataTable();

            //génère les columns
            Contact c = contacts[0];
            List<Donnee> donnees = c.getDonnees().OrderBy(x => x.getOrdre()).ToList();
            DataColumn col = null;

            //ajoute la colonne des idcontact
            col = new DataColumn("idcontact");
            table.Columns.Add(col);

            //ajoute les colonnes dont les champs sont configurés pour être visible dans la grille: propriété isAccueil=true de l'objet Donnee
            foreach (Donnee d in donnees)
            {
                if (d.isAccueil())
                {
                    col = new DataColumn(d.getLibellechamp());
                    table.Columns.Add(col);
                }
            }

            //génère les lignes du tableau
            DataRow r = null;
            
            foreach (Contact item in contacts)
            {
                r = table.NewRow();

                //trie les données par ordre d'affichage
                donnees = item.getDonnees().OrderBy(x => x.getOrdre()).ToList();

                //ajoute l'idcontact
                r["idcontact"] = item.getIdcontact();

                //ajoute les données du contact à une ligne du tableau

                foreach (Donnee d in donnees)
                {       
                    if (d.isAccueil() )
                    {
                        r[d.getLibellechamp()] = d.getValeur();                        
                    }
                }

                table.Rows.Add(r);                
            }
            
            return table;
        }

        private void buttonAjouter_Click(object sender, EventArgs e)
        {
            if (user == null)
                return;

            Template template = GestionContacts.GetTemplate(ApiContact.GetStringJSonTemplate(user.getIduser()));

            if (template!=null)
            {
                FicheContactForm w = new FicheContactForm(template);
                w.ShowDialog();
            }
        }
    }
}
