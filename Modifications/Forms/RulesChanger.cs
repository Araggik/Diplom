using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TriadNSim.Modifications.Classes;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace TriadNSim.Modifications.Forms
{
    public partial class RulesChanger : Form
    {
        public List<TranRule> list_rules;

        public RulesChanger()
        {
            InitializeComponent();
            list_rules = new List<TranRule>();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            CreateRule newForm = new CreateRule();
            var res = newForm.ShowDialog();
            if(res == DialogResult.OK)
            {
                list_rules.Add(newForm.rule);
                listView1.Items.Add(newForm.rule.Name);
            }
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stream StreamWrite;
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.DefaultExt = "rls";
            fileDialog.Title = "Сохранить правила трансформации";
            fileDialog.Filter = "TransformationRule files (*.rls)|*.rls|All files (*.*)|*.*";
            var res = fileDialog.ShowDialog();
            if(res == DialogResult.OK && fileDialog.FileName!=null && fileDialog.FileName.Length>0)
            {
                StreamWrite = fileDialog.OpenFile();
                BinaryFormatter BinaryWrite = new BinaryFormatter();
                BinaryWrite.Serialize(StreamWrite, list_rules);
                StreamWrite.Close();
            }
        }
    }
}
