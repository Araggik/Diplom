using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using TriadNSim.Ontology;

namespace TriadNSim.Modifications.Forms
{
    public partial class CreateRule : Form
    {
        private COWLOntologyManager ontologyManager;
        public const string sOntologyPath = "Ontologies\\ComputerNetwork.owl";
        private Dictionary<ListViewItem, Bitmap> ItemImages;

        private void LoadElements()
        {
            Dictionary<string, Bitmap> images = LoadImageList();
            var ontology_elements = ontologyManager.GetComputerNetworkElements(":ComputerNetworkNode");
            ImageList image_list = new ImageList();

            foreach(var el in ontology_elements)
            {
                var find_bitmap = images[el.Comment.ToLower()];
                if (find_bitmap != null)
                {
                    ListViewItem newitem = new ListViewItem();
                    newitem.Text = el.Comment;
                    image_list.Images.Add(find_bitmap);
                    newitem.ImageIndex = image_list.Images.Count - 1;
                    listView1.Items.Add(newitem);
                    ItemImages[newitem] = find_bitmap;
                }
            }

            Bitmap arrow_btm = new Bitmap("Arrow.png");
            image_list.Images.Add(arrow_btm);
            var arrow_class = ontologyManager.GetClass("Arrow");
            var arrows_classes = ontologyManager.GetSubClasses(arrow_class, false);
            foreach(var el in arrows_classes)
            {
                var individuals = ontologyManager.GetIndividuals(el, false);
                foreach(var individ in individuals)
                {
                    var str = individ.getIRI().getFragment();                  
                    ListViewItem newitem = new ListViewItem();
                    newitem.Text = str;
                    newitem.ImageIndex = image_list.Images.Count - 1;
                    listView1.Items.Add(newitem);
                    ItemImages[newitem] = arrow_btm;
                }
            }

            Bitmap process_btm = new Bitmap("Process.png");
            image_list.Images.Add(process_btm);
            var process_class = ontologyManager.GetClass("Process");
            var individs= ontologyManager.GetIndividuals(process_class, false);
            foreach (var individ in individs)
            {
                var str = individ.getIRI().getFragment();
                ListViewItem newitem = new ListViewItem();
                newitem.Text = str;
                newitem.ImageIndex = image_list.Images.Count - 1;
                listView1.Items.Add(newitem);
                ItemImages[newitem] = process_btm;
            }

            image_list.ImageSize = new Size(60, 40);
            listView1.LargeImageList = image_list;
        }

        private Dictionary<string, Bitmap> LoadImageList()
        {
            Dictionary<string, Bitmap> res = new Dictionary<string, Bitmap>();
            if (File.Exists("elements.xml"))
            {
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.IgnoreWhitespace = true;
                settings.IgnoreComments = true;
                XmlDocument doc = new XmlDocument();
                doc.Load("elements.xml");
                int nCount = doc.ChildNodes.Count;
                for (int i = 0; i < nCount; i++)
                {
                    XmlNode node = doc.ChildNodes[i];
                    if (node.Name == "elements")
                    {
                        foreach (XmlNode item in node.ChildNodes)
                        {
                            string sName = item.Attributes["name"].Value;
                            string sBase64 = item.InnerXml;
                            if (sBase64.Length > 0)
                            {
                                byte[] buffer = Convert.FromBase64String(sBase64);
                                MemoryStream ms = new MemoryStream();
                                ms.Write(buffer, 0, buffer.Length);
                                res[sName.ToLower()] = new Bitmap(ms);
                            }
                        }
                        break;
                    }
                }
            }
            return res;
        }

        public CreateRule()
        {
            InitializeComponent();
            ontologyManager = new COWLOntologyManager(sOntologyPath);
            ItemImages = new Dictionary<ListViewItem, Bitmap>();
            LoadElements();
        }
    }
}
