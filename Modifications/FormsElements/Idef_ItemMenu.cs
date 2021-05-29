using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TriadNSim.Modifications.FormsElements
{
    public partial class Idef_ItemMenu : ToolStripMenuItem//UserControl
    {
        public void TestEvent(object sender, EventArgs e)
        {
           
        }

        public Idef_ItemMenu()
        {
            InitializeComponent();
            this.Text = "IDEF";

            ToolStripItem item1 = new ToolStripMenuItem();
            item1.Text = "Translate IDEF";

            this.DropDownItems.Add(item1);

            ToolStripItem item2 = new ToolStripMenuItem();
            item2.Text = "Create rules";

            this.DropDownItems.Add(item2);            
        }
    }
}
