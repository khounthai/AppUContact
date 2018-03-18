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

    public partial class ListeContactsForm : Form, INotifyPropertyChanged
    {
        private HttpClient client = new HttpClient();
        private User user;
        private bool _connecteVisible;
        List<Contact> ListeContacts;
        public event PropertyChangedEventHandler PropertyChanged;

        private delegate void DelegateChargerListeContact();

        public ListeContactsForm()
        {
            InitializeComponent();
            toolStripLabelInfo.Text = "";
            user = null;
            this.Text = "Ucontact";
            this.dataGridView1.ReadOnly = true;

            Binding b = new Binding("Visible", this, "ConnecteVisible", true, DataSourceUpdateMode.OnPropertyChanged);
            label3.DataBindings.Clear();
            label3.DataBindings.Add(b);

            Binding b1 = new Binding("Visible", this, "ConnecteVisible", true, DataSourceUpdateMode.OnPropertyChanged);
            buttonActualiser.DataBindings.Clear();
            buttonActualiser.DataBindings.Add(b1);

        }

        public bool ConnecteVisible
        {
            get { return _connecteVisible; }
            set
            {
                if (value == _connecteVisible)
                    return;
                _connecteVisible = value;
                OnPropertyChanged("ConnecteVisible");
            }
        }

        protected virtual void OnPropertyChanged(string PropertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
        }

        private void buttonConnexion_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxLogin.Text.Trim()) || string.IsNullOrEmpty(textBoxPassword.Text.Trim()))
                return;


            if (buttonConnexion.Text == "Connexion")
                this.ChargerListeContacts();
            else
            {
                textBoxLogin.Enabled = true;
                textBoxPassword.Enabled = true;
                textBoxLogin.Text = "";
                textBoxPassword.Text = "";
                buttonConnexion.Text = "Connexion";
                user = null;
                ConnecteVisible = false;
                dataGridView1.DataSource = null;
                toolStripLabelInfo.Text = "";
            }
        }

        private void ChargerListeContacts()
        {
            this.Cursor = Cursors.WaitCursor;

            toolStripLabelInfo.Text = "";
            dataGridView1.DataSource = null;
            user = null;

            try
            {
                string strJson = ApiContact.GetStringJSonUser(textBoxLogin.Text, textBoxPassword.Text);
                user = GestionContacts.GetUser(strJson);

                if (user == null)
                {
                    toolStripLabelInfo.Text = "Utilisateur non valide.";
                }
                else
                {
                    textBoxLogin.Enabled = false;
                    textBoxPassword.Enabled = false;
                    buttonConnexion.Text = "Déconnexion";
                }

                strJson = ApiContact.GetStringJSonContacts(user);

                ListeContacts = GestionContacts.GetContacts(strJson);

                SetDataGridView(GetDataTableFromListContacts(ListeContacts));
            }
            catch (Exception ex)
            {
                toolStripLabelInfo.Text = "Erreur de connexion: " + ex.Message;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

            ConnecteVisible = (user != null);
        }

        private void SetDataGridView(DataTable table)
        {
            this.dataGridView1.DataSource = table;
            toolStripLabelInfo.Text = "";

            //cache la colonne des idcontact
            if (table != null)
            {
                this.dataGridView1.Columns["idcontact"].Visible = false;

                int x = 0;

                foreach (DataGridViewColumn c in dataGridView1.Columns)
                {
                    x = x + c.Width;
                }

                dataGridView1.Size = new Size(x + 10, dataGridView1.Height);

                toolStripLabelInfo.Text = "Nombre de contacts: " + table.Rows.Count;
            }
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
                    if (d.isAccueil())
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

            Template template = GestionContacts.GetTemplate(ApiContact.GetStringJSonTemplate(user));

            if (template != null)
            {
                DelegateChargerListeContact d = ChargerListeContacts;


                FicheContactForm w = new FicheContactForm(template, user.getIduser(), d);
                w.ShowDialog();
            }
        }

        private void buttonSupprimer_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                Console.WriteLine(dataGridView1.CurrentRow.Cells[0]);

                long idcontact = long.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                if (ListeContacts != null)
                {
                    Contact c = ListeContacts.Where(x => x.getIdcontact() == idcontact).FirstOrDefault();
                    if (c != null)
                    {
                        ApiContact.DeleteContact(c);
                        ChargerListeContacts();
                    }

                }
            }
        }

        private void buttonActualiser_Click(object sender, EventArgs e)
        {
            ChargerListeContacts();
        }

        private void buttonModifier_Click(object sender, EventArgs e)
        {
            if (user == null || dataGridView1.CurrentRow==null)
                return;

            Template template = GestionContacts.GetTemplate(ApiContact.GetStringJSonTemplate(user));

            if (template != null && ListeContacts!=null)
            {
                DelegateChargerListeContact d = ChargerListeContacts;


                long idcontact = long.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                if (ListeContacts != null)
                {
                    Contact c = ListeContacts.Where(x => x.getIdcontact() == idcontact).FirstOrDefault();
                    if (c != null)
                    {
                        FicheContactForm w = new FicheContactForm(template, user.getIduser(), d,c);
                        w.ShowDialog();
                    }
                }
            }
        }
    }
}
