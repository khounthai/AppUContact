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

        public FicheContactForm(Template template)
        {
            InitializeComponent();
            this.template = template;
        }

        private void InitialiseChamp()
        {
            if (template == null)
                return;

            int x=5, y=5;
            
            foreach (Champ c in template.getChamps())
            {
                Label l = new Label();
                l.Text = c.getLibelle();
                l.AutoSize = true;

                l.Location = new Point(x, y);
                l.Parent = this;

                Control ctrl=null;

                if (c.getPreselectionsize() > 0)
                {
                    ctrl = new TextBox();
                }else
                {
                    ComboBox cb = new ComboBox();
                    foreach (string s in c.getPreselection())
                    {
                        cb.Items.Add(s);
                    }
                }

                ctrl.Width = 100;
                ctrl.Location = new Point(l.Location.X + 10, y);

                y = y + ctrl.Height + 5;

            }
        }
    }
}
