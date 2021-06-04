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
using DrawingPanel;
using System.Collections;
using TriadNSim.Modifications.Classes;

namespace TriadNSim.Modifications.Forms
{
    public partial class CreateRule : Form
    {
        private COWLOntologyManager ontologyManager;
        public const string sOntologyPath = "Ontologies\\ComputerNetwork.owl";
        private Dictionary<ListViewItem, Bitmap> ItemImages;
        protected DrawingPanel.BaseObject[] m_oSelectedObjects;
        public NetworkObject Input;
        public TranRule rule;

        private ENetworkObjectType GetElementTypeByName(string name)
        {
            ENetworkObjectType current_type = ENetworkObjectType.Undefined;
            switch(name)
            {
                case "Маршрутизатор":
                    current_type = ENetworkObjectType.Router;
                    break;
                case "Рабочая станция":
                    current_type = ENetworkObjectType.Client;
                    break;
                case "Сервер":
                    current_type = ENetworkObjectType.Server;
                    break;
                case "Узел":
                    current_type = ENetworkObjectType.SDNNode;
                    break;
            }
            return current_type;
        }

        private ENetworkObjectType GetElementTypeByClassName(string name)
        {
            ENetworkObjectType current_type = ENetworkObjectType.Undefined;
            switch (name)
            {
                case "Control":
                    current_type = ENetworkObjectType.ArrowControl;
                    break;
                case "Mechanism":
                    current_type = ENetworkObjectType.ArrowMechanism;
                    break;
                case "Input":
                    current_type = ENetworkObjectType.ArrowInput;
                    break;
                case "Output":
                    current_type = ENetworkObjectType.ArrowOutput;
                    break;
                case "Owner":
                    current_type = ENetworkObjectType.Owner;
                    break;
                case "BC_Client":
                    current_type = ENetworkObjectType.BC_client;
                    break;
                case "BC_Node":
                    current_type = ENetworkObjectType.BC_node;
                    break;

            }
            return current_type;
        }

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
                    newitem.Tag = new object[2] { el.Name, GetElementTypeByName(el.Comment)};
                    image_list.Images.Add(find_bitmap);
                    newitem.ImageIndex = image_list.Images.Count - 1;
                    listView1.Items.Add(newitem);
                    ItemImages[newitem] = find_bitmap;
                }
            }

            ontology_elements = ontologyManager.GetComputerNetworkElements(":BCNetworkNode");

            foreach (var el in ontology_elements)
            {
                Bitmap find_bitmap = null;
                if (el.Name == "Owner") find_bitmap = new Bitmap("1.png");
                else if (el.Name == "BC_Client") find_bitmap = new Bitmap("2.png");
                else if (el.Name == "BC_Node") find_bitmap = new Bitmap("3.png");
                ListViewItem newitem = new ListViewItem();
                newitem.Text = el.Comment;
                newitem.Tag = new object[2] { el.Name, GetElementTypeByClassName(el.Name) };
                image_list.Images.Add(find_bitmap);
                newitem.ImageIndex = image_list.Images.Count - 1;
                listView1.Items.Add(newitem);
                ItemImages[newitem] = find_bitmap;
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
                    newitem.Tag = new object[2] { el.Name, GetElementTypeByClassName(el.Name) };
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
                newitem.Tag = new object[2] { "Process", ENetworkObjectType.Process };
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

        private Dictionary<string, bool> GetShapeNames(ArrayList shapes)
        {
            Dictionary<string, bool> ShapeNames = new Dictionary<string, bool>();
            foreach (BaseObject obj in shapes)
            {
                if (obj.Name != null && obj.Name.Length > 0)
                    ShapeNames[obj.Name.ToLower()] = true;
            }
            return ShapeNames;
        }

        private string GetUniqueShapeName(string sName,ArrayList shapes)
        {
            int nIndex = 1;
            string sRes = sName.ToLower() + nIndex.ToString();
            Dictionary<string, bool> names = GetShapeNames(shapes);
            while (names.ContainsKey(sRes))
            {
                nIndex++;
                sRes = sName.ToLower() + nIndex.ToString();
            }
            return sName + nIndex.ToString();
        }

        private void dp_DragDrop(object sender, DragEventArgs e)
        {
            DrawingPanel.DrawingPanel dp = (DrawingPanel.DrawingPanel)sender;
            ListViewItem li = (ListViewItem)e.Data.GetData(typeof(ListViewItem));
            Point pt = dp.PointToClient(new Point(e.X, e.Y));
            object[] tag = li.Tag as object[];
            float fZoom = dp.Zoom;
            int delta = 100;
            int X = (int)((pt.X / fZoom - dp.dx) - delta / 2);
            int Y = (int)((pt.Y / fZoom - dp.dx) - delta / 2);
            NetworkObject shape = new NetworkObject(dp);
            shape.Rect = new Rectangle(X, Y, delta, delta);
            shape.Name = GetUniqueShapeName(li.Text,dp.Shapes);
            shape.Type = (ENetworkObjectType)tag[1];
            if (shape.Type != ENetworkObjectType.ArrowInput && shape.Type != ENetworkObjectType.ArrowControl
                && shape.Type != ENetworkObjectType.ArrowMechanism && shape.Type != ENetworkObjectType.ArrowOutput
                && shape.Type != ENetworkObjectType.Process)
                shape.SemanticType = ontologyManager.GetRoutineClass(ontologyManager.GetClass(tag[0] as string)).Name;
            else shape.SemanticType = null;
            shape.img = new Bitmap(ItemImages[li]);
            if(shape.Type == ENetworkObjectType.ArrowControl)
            {
                shape.Rotation = 90;
            }
            else if(shape.Type == ENetworkObjectType.ArrowMechanism)
            {
                shape.Rotation = 270;
            }
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
            drawingPanel2.DragDrop += new DragEventHandler(dp_DragDrop);
            drawingPanel2.DragEnter += new DragEventHandler(dp_DragEnter);
            drawingPanel2.MouseClick += new MouseEventHandler(rightPart_MouseUp);
            drawingPanel2.objectSelected += new ObjectSelected(rightPart_objectSelected);
            drawingPanel2.beforeAddLine += new BeforeAddLine(dp_beforeAddLine);
        }

        protected void miInput_Click(object sender, EventArgs e)
        {
            if (m_oSelectedObjects != null && m_oSelectedObjects.Length == 1)
            {
                NetworkObject obj = m_oSelectedObjects[0] as NetworkObject;
                if (obj != null)
                    Input = obj;
            }
        }

        private void rightPart_objectSelected(object sender, PropertyEventArgs e)
        {
            m_oSelectedObjects = e.ele;
        }

        private void rightPart_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Point pntShow = drawingPanel2.PointToScreen(e.Location);
                if (m_oSelectedObjects != null && m_oSelectedObjects.Length == 1)
                {
                    ContextMenuStrip menu = new ContextMenuStrip();
                    NetworkObject obj = m_oSelectedObjects[0] as NetworkObject;
                    if (obj != null)
                    {
                        menu.Items.Add("Входной полюс", null, miInput_Click);
                        menu.Show(pntShow, ToolStripDropDownDirection.AboveRight);
                    }
                }

            }
        }

        private void dp_beforeAddLine(object sender, BeforeAddLineEventArgs e)
        {
            DrawingPanel.DrawingPanel dp = (DrawingPanel.DrawingPanel)sender;
            e.cancel = true;
            NetworkObject objFrom = e.fromCP.Owner as NetworkObject;
            NetworkObject objTo = e.toCP.Owner as NetworkObject;
            if (dp.ShapeCollection.GetLine(objFrom, objTo) == null)
            {
                Link oLink =  new Link(dp, e.fromCP, e.toCP);
                dp.ShapeCollection.AddShape(oLink);
            }
        }

        private void copy_shapes(ArrayList dp, ArrayList rule)
        {
            foreach (BaseObject obj in dp)
                rule.Add(obj);
        }

        public CreateRule()
        {
            InitializeComponent();
            ontologyManager = new COWLOntologyManager(sOntologyPath);
            ItemImages = new Dictionary<ListViewItem, Bitmap>();
            rule = null;
            Input = null;
            LoadElements();
            Add_functions();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            drawingPanel2.CurrentTool = ToolType.ttSelect;
            toolStripButton1.Checked = true;
            toolStripButton2.Checked = false;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            drawingPanel2.CurrentTool = ToolType.ttLine;
            toolStripButton1.Checked = false;
            toolStripButton2.Checked = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            rule = new TranRule();
            copy_shapes(drawingPanel1.Shapes, rule.leftPart);
            copy_shapes(drawingPanel2.Shapes, rule.rightPart);
            rule.input = Input;
            rule.Name = textBox1.Text;
        }
    }
}
