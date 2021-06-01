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
using TriadNSim;

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

        private void lv_DragOver(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(typeof(ListViewItem)))
            {
                e.Effect = DragDropEffects.None;
                return;
            }
        }

        private void lv_ItemDrag(object sender, ItemDragEventArgs e)
        {
            listView1.DoDragDrop(e.Item, DragDropEffects.Move);
        }


        private void dp_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(ListViewItem)))
                e.Effect = DragDropEffects.Move;
        }

        private void dp_DragDrop(object sender, DragEventArgs e)
        {
            DrawingPanel.DrawingPanel dp = (DrawingPanel.DrawingPanel)sender;
            ListViewItem li = (ListViewItem)e.Data.GetData(typeof(ListViewItem));
            Point pt = dp.PointToClient(new Point(e.X, e.Y));

            float fZoom = dp.Zoom;
            int delta = 100;
            int X = (int)((pt.X / fZoom - dp.dx) - delta / 2);
            int Y = (int)((pt.Y / fZoom - dp.dx) - delta / 2);
            NetworkObject shape = new NetworkObject(dp);
            shape.Rect = new Rectangle(X, Y, delta, delta);
            shape.Name = li.Text;
            shape.img = new Bitmap(ItemImages[li]);
            shape.showBorder = false;
            dp.ShapeCollection.AddShape(shape);

            dp.Focus();
        }

        private void Add_functions()
        {
            listView1.DragOver += new DragEventHandler(lv_DragOver);
            listView1.ItemDrag += new ItemDragEventHandler(lv_ItemDrag);
            drawingPanel1.DragDrop+= new DragEventHandler(dp_DragDrop);
            drawingPanel1.DragEnter+= new DragEventHandler(dp_DragEnter);
        }

        public CreateRule()
        {
            InitializeComponent();
            ontologyManager = new COWLOntologyManager(sOntologyPath);
            ItemImages = new Dictionary<ListViewItem, Bitmap>();
            LoadElements();
            Add_functions();
        }
    }
}
