using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Core.Interfaces;
using Core.Nesting;
using Core.Persistence;
using Nesting.Core.Classes.Nesting;
using Nesting.Core.Drawing;
using Point = Core.Nesting.Point;

namespace Nesting.PartEditor
{
    public partial class PartEditor : Form
    {
        private DrawingService drawingService = new DrawingService();
        private List<MenuItem> partMenus = new List<MenuItem>();
        private List<MenuItem> vertexMenus = new List<MenuItem>();
        private IPersistenceService persistenceService;

        public PartEditor(ICore core)
        {
            this.persistenceService = core.GetPersistenceService();
            persistenceService.PreInitPersistence<Part>();
            
            InitializeComponent();
            Init();
            
        }

        private void Init()
        {
            //Build context menus for parts
            MenuItem delete = new MenuItem();
            delete.Text = "Delete part";
            delete.Click += DeleteOnClick;
            partMenus.Add(delete);

            MenuItem clone = new MenuItem();
            clone.Text = "Clone part";
            clone.Click += CloneOnClick;
            partMenus.Add(clone);


            //Build context menus for vertexes
            MenuItem deleteVertex = new MenuItem();
            deleteVertex.Text = "Delete vertex";
            deleteVertex.Click += DeleteVertex;
            vertexMenus.Add(deleteVertex);
            

            //Init the listview so it shows part images          
            lstExistingParts.LargeImageList = new ImageList();
            lstExistingParts.LargeImageList.ImageSize = new Size(100, 100);
            lstExistingParts.LargeImageList.ColorDepth = ColorDepth.Depth32Bit;
            lstExistingParts.ContextMenu = new ContextMenu(partMenus.ToArray());


            drawingService.FigureFillColorBack = Color.CornflowerBlue;
            drawingService.FigureFillColorFront = Color.LightBlue;
            drawingService.EdgePen = new Pen(Color.DarkBlue);

            imgPart.Image = drawingService.GenerateBitmapWithBorder(Math.Max(imgPart.Size.Height,imgPart.Size.Width),drawingService.BorderPen,drawingService.FillColor);

            ListParts();
        }

        private void DeleteVertex(object sender, EventArgs e)
        {
            if (lstPartVertexes.SelectedIndices.Count == 0)
            {
                return;
            }

            int index = lstPartVertexes.SelectedIndices[0];

            lstPartVertexes.Items.RemoveAt(index);

            DrawSchematic();
        }

        private async Task ListParts()
        {
            lstExistingParts.Items.Clear();
            AddLoadingSymbol();

            int pageNumber = 1;
            List<Part> parts = await persistenceService.QueryAsync<Part>(pageNumber, 10);
            lstExistingParts.Items.Clear();

            AddNewPartSymbol();

            while (parts.Count > 0)
            {
                parts = await persistenceService.QueryAsync<Part>(pageNumber, 10);

                foreach (Part part in parts)
                {
                    AddPartToList(part);
                }

                pageNumber++;
            }

        }

        private void CloneOnClick(object sender, EventArgs eventArgs)
        {
            if (lstExistingParts.SelectedItems.Count == 0)
            {
                return;
            }

            ListViewItem listviewItem = lstExistingParts.SelectedItems[0];

            Part part = (Part)listviewItem.Tag;

            //Part to be cloned!

            if (DialogResult.Yes == MessageBox.Show("Are you really sure you want to clone this part?", "Clone part?", MessageBoxButtons.YesNo))
            {
                lstExistingParts.SelectedItems.Clear();
                lstExistingParts.Items.Remove(listviewItem);

                Part clone = part.Clone();
                clone.Id = null;
                clone.Name = part.Name + "_CLONE";

                persistenceService.AddOrUpdate<Part>(clone.Id,clone);

                ListParts();
            }
        }

        private void DeleteOnClick(object sender, EventArgs e)
        {
            if (lstExistingParts.SelectedItems.Count == 0)
            {
                return;
            }

            ListViewItem listviewItem = lstExistingParts.SelectedItems[0];

            Part part = (Part)listviewItem.Tag;

            //Part to be deleted!

            if (DialogResult.Yes == MessageBox.Show("Are you really sure you want to delete this part?", "Delete part?",MessageBoxButtons.YesNo))
            {
                lstExistingParts.SelectedItems.Clear();
                lstExistingParts.Items.Remove(listviewItem);

                persistenceService.Delete<Part>(part.Id);
            }
        }

        private void AddPartToList(Part part)
        {
            Image bitmap = drawingService.DrawThumbnail(part,100);
            
            lstExistingParts.LargeImageList.Images.Add(part.Id, bitmap);

            ListViewItem item = new ListViewItem(part.Name);
            item.ImageKey = part.Id;
            item.Tag = part;

            lstExistingParts.Items.Add(item);
        }

        private void AddLoadingSymbol()
        {
            Part loading = new Part();
            loading.Id = "LOADING";
            loading.Name = "LOADING";
            loading.Description = "LOADING";

            loading.Vertexes.Add(new Point(30, 10));
            loading.Vertexes.Add(new Point(70, 10));
            loading.Vertexes.Add(new Point(30, 90));
            loading.Vertexes.Add(new Point(70, 90));

            Image bitmap = drawingService.DrawThumbnail(loading, 100);

            lstExistingParts.LargeImageList.Images.Add(loading.Id, bitmap);

            ListViewItem item = new ListViewItem(loading.Name);
            item.ImageKey = loading.Id;

            lstExistingParts.Items.Add(item);

        }

        private void AddNewPartSymbol()
        {
            Part newPart = new Part();
            newPart.Id = "#ADDNEWPART";
            newPart.Name = "ADDNEWPART";
            newPart.Description = "ADDNEWPART";

            newPart.Vertexes.Add(new Point(40, 10));
            newPart.Vertexes.Add(new Point(60, 10));
            newPart.Vertexes.Add(new Point(60, 40));

            newPart.Vertexes.Add(new Point(90, 40));
            newPart.Vertexes.Add(new Point(90, 60));
            newPart.Vertexes.Add(new Point(60, 60));

            newPart.Vertexes.Add(new Point(60, 90));
            newPart.Vertexes.Add(new Point(40, 90));
            newPart.Vertexes.Add(new Point(40, 60));

            newPart.Vertexes.Add(new Point(10, 60));
            newPart.Vertexes.Add(new Point(10, 40));
            newPart.Vertexes.Add(new Point(40, 40));

            AddPartToList(newPart);

        }
        
        private void LstPartsItemSelected(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            lstPartVertexes.Items.Clear();
            lstPartVertexes.ContextMenu = new ContextMenu(vertexMenus.ToArray());
            
            Part part = e.Item.Tag as Part;

            if (part != null && part.Id == "#ADDNEWPART")
            {
                part = null;
            }

            bool selected = e.IsSelected && part != null;
            if (selected)
            {
                txtId.Text = part.Id;
                txtName.Text = part.Name;
                txtDescription.Text = part.Description;

                foreach (Point point in part.Vertexes)
                {
                    AddVertex(point);
                }

                DrawSchematic();
            }
            else
            {
                imgPart.Image = null;

                txtId.Text = "";
                txtName.Text = "";
                txtDescription.Text = "";
            }
        }

        private void DrawSchematic()
        {
            List<Point> points = new List<Point>();


            foreach (ListViewItem item in lstPartVertexes.Items)
            {
                points.Add((Point)item.Tag);
            }


            Image bitmap = drawingService.DrawSchematic(points, Math.Min(imgPart.Width, imgPart.Height));

            imgPart.Image = bitmap;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Are you really sure you want to save this part?", "Save changes?", MessageBoxButtons.YesNo))
            {
                Part newPart = new Part();

                newPart.Id = txtId.Text;
                newPart.Name = txtName.Text;
                newPart.Description = txtDescription.Text;

                foreach (ListViewItem item in lstPartVertexes.Items)
                {
                    newPart.Vertexes.Add((Point)item.Tag);
                }

                persistenceService.AddOrUpdate<Part>(newPart.Id, newPart);
                ListParts();
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (lstExistingParts.Items.Count == 0)
            {
                return;
            }

            if (DialogResult.Yes == MessageBox.Show("Are you really sure you want to discard changes to this part?", "Discard changes?", MessageBoxButtons.YesNo))
            {
                int selectedIndex = lstExistingParts.SelectedIndices[0];

                lstExistingParts.SelectedItems.Clear();
                lstExistingParts.SelectedIndices.Add(selectedIndex);
                
            }
        }

        private void AddVertexToPart(object sender, EventArgs e)
        {
            Point point = new Point((float)numXInternal.Value, (float)numYInternal.Value);

            AddVertex(point);
        }

        private void AddVertex(Point point)
        {
            ListViewItem item = new ListViewItem(new string[] { point.X + "", point.Y + "" });
            item.Tag = point;

            lstPartVertexes.Items.Add(item);

            DrawSchematic();
        }

    }
}
