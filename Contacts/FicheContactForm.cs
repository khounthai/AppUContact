using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Contacts.classes;
using Contacts.entity;

namespace Contacts
{
    public partial class FicheContactForm : Form
    {
        Template template;
        long iduser;
        private Delegate ChargerListeContact;
        private Contact contact;

        public FicheContactForm(Template template, long iduser, Delegate ChargerListeContact, Contact contact = null)
        {
            InitializeComponent();
            this.template = template;
            this.contact = contact;
            this.InitialiseChamp();
            toolStripLabelInfo.Text = "";
            this.Text = "Fiche contact";
            this.iduser = iduser;
            this.ChargerListeContact = ChargerListeContact;
        }

        private void InitialiseChamp()
        {
            if (template == null)
                return;

            Control ctrl = null;
            int x = 20, y = 20;

            foreach (Champ c in template.getChamps())
            {
                c.isValide = true;

                Label l = new Label();
                l.Text = c.getLibelle();
                l.AutoSize = true;
                l.BackColor = Color.Transparent;
                l.Location = new Point(x, y);
                l.Parent = this.panel1;

                Donnee d = null;

                if (contact != null)
                {
                    d = contact.getDonnees().Where(dd => dd.getIdchamp() == c.getIdchamp()).FirstOrDefault();
                }


                if (c.getDatatype().getLibelle() == "DATE")
                {
                    ctrl = new DateTimePicker();
                    if (d != null && !string.IsNullOrEmpty(d.getValeur()))
                        ((DateTimePicker)ctrl).Value = DateTime.Parse(d.getValeur());
                }
                else if (c.getPreselectionsize() > 0)
                {
                    ctrl = new ComboBox();

                    foreach (string s in c.getPreselection())
                    {
                        ((ComboBox)ctrl).Items.Add(s);
                        if (d != null)
                            ((ComboBox)ctrl).SelectedItem = d.getValeur();
                    }
                }
                else
                {
                    ctrl = new TextBox();
                    ctrl.KeyPress += Ctrl_KeyPress;

                    if (!String.IsNullOrEmpty(c.getDatatype().getRegex()))
                    {

                        ctrl.Leave += Ctrl_Leave;
                        ctrl.LostFocus += Ctrl_Leave;
                    }

                    if (d != null)
                        ((TextBox)ctrl).Text = d.getValeur();
                }

                ctrl.Tag = c;
                ctrl.Width = 200;
                ctrl.Location = new Point(120, y);
                ctrl.Parent = this.panel1;

                y = y + ctrl.Height + 10;
            }

            Button btnAjouter = new Button();
            btnAjouter.Name = "BtnAjouter";
            btnAjouter.Text =(contact==null? "Ajouter":"Modifier");
            btnAjouter.Width = 100;
            btnAjouter.Location = new Point(x, y);
            btnAjouter.Parent = this.panel1;
            btnAjouter.Click += BtnAjouter_Click;           

            Button btnQuitter = new Button();
            btnQuitter.Text = "Quitter";
            btnQuitter.Width = 100;
            btnQuitter.Location = new Point(btnAjouter.Width + 40, y);
            btnQuitter.Parent = this.panel1;
            btnQuitter.Click += BtnQuitter_Click;

            if (ctrl != null)
            {
                x = ctrl.Location.X + ctrl.Width + 40;
                y = toolStrip1.Location.Y-20;
            }

            this.panel1.Size = new Size(x, y);

        }

        private void Ctrl_KeyPress(object sender, KeyPressEventArgs e)
        {
            toolStripLabelInfo.Text = "";
        }

        private void Ctrl_Leave(object sender, EventArgs e)
        {
            TextBox txtb = (TextBox)sender;

            if (txtb.Tag != null && txtb.Tag.GetType() == typeof(Champ))
            {
                Champ c = (Champ)txtb.Tag;
                if (!String.IsNullOrEmpty(c.getDatatype().getRegex()))
                {
                    Regex r = new Regex(c.getDatatype().getRegex());

                    if (!r.IsMatch(txtb.Text) && !string.IsNullOrEmpty(txtb.Text))
                    {
                        c.isValide = false;
                        toolStripLabelInfo.Text = "Erreur sur la saisie sur le champ: " + c.getLibelle();
                    }
                    else
                        c.isValide = true;
                }
            }
        }

        private void BtnQuitter_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnAjouter_Click(object sender, EventArgs e)
        {
            if (FormIsValid())
            {
                contact = NouveauContact(); 
                contact.setIduser(iduser);
                ContactWrapper cw = new ContactWrapper();
                cw.setContact(contact);
                cw.setIdtemplate(template.getIdtemplate());
                ApiContact.SetContact(cw);
                ChargerListeContact.DynamicInvoke();

                if (this.panel1.Controls["BtnAjouter"]!=null && this.panel1.Controls["BtnAjouter"].Text=="Ajouter")                
                {
                    contact = null;
                    this.panel1.Controls.Clear();
                    this.InitialiseChamp();
                }
            }
        }

        private bool FormIsValid()
        {
            bool b = true;
            int i = 0;
            while (b && i < this.Controls.Count)
            {
                Control ctrl = this.panel1.Controls[i];

                if (ctrl.Tag != null && ctrl.Tag.GetType() == typeof(Champ))
                {
                    Champ c = (Champ)ctrl.Tag;
                    b = c.isValide;
                    
                }
                i++;
            }

            return b;
        }
        
        private Contact NouveauContact()
        {

            if (this.contact == null)
                this.contact = new Contact();

            List<Donnee> donnees;
            
            contact.setIduser(template.getIduser());
            contact.setDtcreation(DateTime.Now);
            contact.setFavoris(false);
            contact.setActif(true);

            donnees = new List<Donnee>();

            foreach (Control ctrl in this.panel1.Controls)
            {
                if (ctrl.Tag != null && ctrl.Tag.GetType() == typeof(Champ))
                {
                    
                    Champ c = (Champ)ctrl.Tag;

                    Donnee d = new Donnee();
                    d.setIdchamp(c.getIdchamp());

                    if (ctrl.GetType() == typeof(DateTimePicker))
                    {
                        if ((((DateTimePicker)ctrl).Value.ToShortDateString() != DateTime.Now.ToShortDateString()))
                            d.setValeur(((DateTimePicker)ctrl).Value.ToString("yyyy-MM-dd"));
                        else
                            d.setValeur(null);
                    }
                    else
                        d.setValeur(ctrl.Text);

                    donnees.Add(d);
                }
            }

            contact.setDonnees(donnees);
            return contact;
        }
    }
}
