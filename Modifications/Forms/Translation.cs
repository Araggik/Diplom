using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TriadNSim.Ontology;
using TriadNSim;
using DrawingPanel;
using System.Collections;
using TriadNSim.Modifications.Classes;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace TriadNSim.Modifications.Forms
{
    public partial class Translation : Form
    {
        private COWLOntologyManager ontologyManager;
        public const string sOntologyPath = "Ontologies\\ComputerNetwork.owl";
        private Dictionary<ListViewItem, Bitmap> ItemImages;
        public List<TranRule> list_rules;
        public DrawingPanel.DrawingPanel main_panel = null;

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

        public void LoadElements()
        {           
            ImageList image_list = new ImageList();

            Bitmap arrow_btm = new Bitmap("Arrow.png");
            image_list.Images.Add(arrow_btm);
            var arrow_class = ontologyManager.GetClass("Arrow");
            var arrows_classes = ontologyManager.GetSubClasses(arrow_class, false);
            foreach (var el in arrows_classes)
            {
                var individuals = ontologyManager.GetIndividuals(el, false);
                foreach (var individ in individuals)
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
            var individs = ontologyManager.GetIndividuals(process_class, false);
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

        private string GetUniqueShapeName(string sName, ArrayList shapes)
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
            shape.Name = GetUniqueShapeName(li.Text, dp.Shapes);
            shape.Type = (ENetworkObjectType)tag[1];
            shape.SemanticType = null;
            shape.img = new Bitmap(ItemImages[li]);
            if (shape.Type == ENetworkObjectType.ArrowControl)
            {
                shape.Rotation = 90;
            }
            else if (shape.Type == ENetworkObjectType.ArrowMechanism)
            {
                shape.Rotation = 270;
            }
            shape.showBorder = false;
            dp.ShapeCollection.AddShape(shape);
            dp.Focus();
        }

        public void AddFunctions()
        {
            listView1.DragOver += new DragEventHandler(lv_DragOver);
            listView1.ItemDrag += new ItemDragEventHandler(lv_ItemDrag);
            drawingPanel1.DragDrop += new DragEventHandler(dp_DragDrop);
            drawingPanel1.DragEnter += new DragEventHandler(dp_DragEnter);
        }

        public Translation()
        {
            InitializeComponent();
            ontologyManager = new COWLOntologyManager(sOntologyPath);
            ItemImages = new Dictionary<ListViewItem, Bitmap>();
            list_rules = null;
            LoadElements();
            AddFunctions();
        }

        private void Translate()
        {
            foreach(var rule in list_rules)
            {
                int n = rule.leftPart.Count;
                List<object> del_indices = new List<object>();
                int current_find_el_index = 0;
                int first_new_index = -1;                
                for(int i=0; i<drawingPanel1.Shapes.Count; i++)
                {
                    var obj = drawingPanel1.Shapes[i];
                    var curent_find_el = (BaseObject)rule.leftPart[current_find_el_index];
                    if (obj is NetworkObject && curent_find_el is NetworkObject )
                    {
                        var netobj = (NetworkObject)obj;
                        var curent_find_el_netobj = (NetworkObject)curent_find_el;
                        if(netobj.Type == curent_find_el_netobj.Type)
                        {
                            if(current_find_el_index == 0)
                            {
                                first_new_index = drawingPanel1.Shapes.Count;
                            }
                            del_indices.Add(obj);
                            current_find_el_index++;
                            if(current_find_el_index >=n)
                            {
                                foreach(var el in rule.rightPart)
                                {
                                    drawingPanel1.Shapes.Add(el);
                                }
                                foreach(var ind in del_indices)
                                {
                                    List<object> del_indices_line = new List<object>();
                                    foreach (var li in drawingPanel1.Shapes)
                                    {
                                        if(li is Line)
                                        {
                                            var line = (Line)li;
                                            if(ind == line.FromCP.Owner)
                                            {
                                                if(rule.input == null)
                                                {
                                                    NetworkObject nobj = (NetworkObject)drawingPanel1.Shapes[first_new_index];
                                                    line.FromCP =  nobj.ConnectionPoint;
                                                }
                                                else
                                                {
                                                    line.FromCP = rule.input.ConnectionPoint;
                                                }
                                                del_indices_line.Add(line);
                                            }
                                            else if (ind == line.ToCP.Owner)
                                            {
                                                if (rule.input == null)
                                                {
                                                    NetworkObject nobj = (NetworkObject)drawingPanel1.Shapes[first_new_index];
                                                    line.ToCP = nobj.ConnectionPoint;
                                                }
                                                else
                                                {
                                                    line.ToCP.Owner = rule.input.ConnectionPoint;
                                                }
                                                del_indices_line.Add(line);
                                            }
                                        }
                                    }
                                    foreach(var li in del_indices_line)
                                    {
                                        var line = (Line)li;
                                        var newli = new Link(main_panel, line.FromCP,  line.ToCP);
                                        newli.bSelected = false;
                                        drawingPanel1.Shapes.Add(newli);
                                        drawingPanel1.Shapes.Remove(li);
                                    }
                                }
                                foreach(var ind in del_indices)
                                {
                                    drawingPanel1.Shapes.Remove(ind);
                                }
                                del_indices.Clear();
                                current_find_el_index = 0;
                                first_new_index = -1;
                            }
                        }

                    }
                }
            }
            foreach(var el in drawingPanel1.Shapes)
            {
                BaseObject obj = (BaseObject)el;
                obj.drawingPanel = main_panel;
                main_panel.ShapeCollection.AddShape(obj);
            }
            //foreach(var el in drawingPanel1.Shapes)
            //{
            //    if (el is NetworkObject)
            //    {
            //        NetworkObject obj = (NetworkObject)el;
            //        main_panel.ShapeCollection.AddShape(obj);
            //    }
            //}
            //foreach (var el in drawingPanel1.Shapes)
            //{
            //    if (el is Link)
            //    {
            //        Link replace_line = (Link)el;
            //        NetworkObject find_from = null;
            //        NetworkObject find_to = null;
            //        bool flag = false;
            //        int i = 0;
            //        while(!flag && i < main_panel.ShapeCollection.ShapeList.Count)
            //        {                        
            //            var current_obj = main_panel.ShapeCollection.ShapeList[i];
            //            if(current_obj is NetworkObject)
            //            {
            //                NetworkObject cur_network_obj = (NetworkObject)current_obj;
            //                if(replace_line.FromCP.Owner.Name == cur_network_obj.Name)
            //                {
            //                    find_from = cur_network_obj;
            //                }
            //                else if (replace_line.ToCP.Owner.Name == cur_network_obj.Name)
            //                {
            //                    find_to = cur_network_obj;
            //                }
            //            }
            //            flag = (find_from != null) && (find_to != null);
            //            i++;
            //        }
            //        if(flag)
            //        {
            //           Link newlink = new Link(main_panel, new CConnectionPoint(main_panel, find_from), new CConnectionPoint(main_panel, find_to));
            //            newlink.bSelected = false;
            //            main_panel.ShapeCollection.AddShape(newlink);
            //        }
            //    }
            //}
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Translate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Stream StreamRead;
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.DefaultExt = "rls";
            fileDialog.Title = "Открыть правила трансформации";
            fileDialog.Filter = "TransformationRule files (*.rls)|*.rls|All files (*.*)|*.*";
            var res = fileDialog.ShowDialog();
            if (res == DialogResult.OK && fileDialog.FileName != null && fileDialog.FileName.Length > 0)
            {
                StreamRead = fileDialog.OpenFile();
                BinaryFormatter BinaryRead = new BinaryFormatter();
                list_rules = (List<TranRule>)BinaryRead.Deserialize(StreamRead);
                StreamRead.Close();
            }
        }
    }
}
