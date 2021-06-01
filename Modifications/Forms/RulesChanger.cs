using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TriadNSim.Modifications.Forms
{
    public partial class RulesChanger : Form
    {
        public RulesChanger()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            CreateRule newForm = new CreateRule();
            var res = newForm.ShowDialog();
        }
    }
}
