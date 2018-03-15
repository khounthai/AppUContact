using Contacts.entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Contacts
{
    public partial class FicheContactForm : Form
    {
        Template template;
        
        System.Text.RegularExpressions.Regex rEMail = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$");

        private bool isValidForm;

        public FicheContactForm(Template template)
        {
            InitializeComponent();
            this.template = template;
            this.InitialiseChamp();
            toolStripLabelInfo.Text = "";
        }

        private void InitialiseChamp()
        {
            if (template == null)
                return;

            int x=20, y=20;
            
            foreach (Champ c in template.getChamps())
            {
                Label l = new Label();
                l.Text = c.getLibelle();
                l.AutoSize = true;

                l.Location = new Point(x, y);
                l.Parent = this;

                Control ctrl=null;

                if (c.getDatatype().getLibelle()=="DATE")
                {
                    ctrl = new DateTimePicker();
                }
                else if (c.getDatatype().getLibelle() == "EMAIL")
                {
                    ctrl = new TextBox();
                    ctrl.Leave += Ctrl_Leave;                  
                    ctrl.LostFocus += Ctrl_Leave;
                    ctrl.KeyPress += Ctrl_KeyPress;
                }
                else if (c.getPreselectionsize() > 0)
                {
                    ctrl = new ComboBox();

                    foreach (string s in c.getPreselection())
                    {
                        ((ComboBox)ctrl).Items.Add(s);
                    }                    

                }else
                {
                    ctrl = new TextBox();
                    ctrl.KeyPress += Ctrl_KeyPress;
                }

                ctrl.Width = 200;
                ctrl.Location = new Point(120, y);
                ctrl.Parent = this;

                y = y + ctrl.Height + 10;

            }

            Button btnAjouter = new Button();
            btnAjouter.Text = "Ajouter";
            btnAjouter.Width = 100;
            btnAjouter.Location = new Point(x, y);
            btnAjouter.Parent = this;
            btnAjouter.Click += BtnAjouter_Click;

            Button btnQuitter = new Button();
            btnQuitter.Text = "Quitter";
            btnQuitter.Width = 100;
            btnQuitter.Location = new Point(btnAjouter.Width+40, y);
            btnQuitter.Parent = this;
            btnQuitter.Click += BtnQuitter_Click;
        }

        private void Ctrl_KeyPress(object sender, KeyPressEventArgs e)
        {
            toolStripLabelInfo.Text = "";
        }

    
        private void Ctrl_Leave(object sender, EventArgs e)
        {
            TextBox txtb=(TextBox) sender;

            if (!rEMail.IsMatch(txtb.Text) && !string.IsNullOrEmpty(txtb.Text))
            {
                isValidForm = false;
                toolStripLabelInfo.Text = "Erreur sur la saisie de l'email";
            }
        }

        private void BtnQuitter_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnAjouter_Click(object sender, EventArgs e)
        {
            return;
        }
    }
}
