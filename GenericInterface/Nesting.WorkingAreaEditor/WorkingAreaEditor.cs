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

namespace Nesting.WorkingAreaEditor
{
    public partial class WorkingAreaEditor : Form
    {
        private DrawingService drawingService = new DrawingService();
        private List<MenuItem> workingAreaMenus = new List<MenuItem>();
        private List<MenuItem> vertexMenus = new List<MenuItem>();
        private IPersistenceService workingAreasPersistence;

        public WorkingAreaEditor(ICore core)
        {
            //Initializing persistence in background
            this.workingAreasPersistence = core.GetPersistenceService();
            workingAreasPersistence.PreInitPersistence<WorkingArea>();
            
            InitializeComponent();
            Init();
            
        }

        private void Init()
        {
            //Build context menus for workingAreas
            MenuItem delete = new MenuItem();
            delete.Text = "Delete workingArea";
            delete.Click += DeleteOnClick;
            workingAreaMenus.Add(delete);

            MenuItem clone = new MenuItem();
            clone.Text = "Clone workingArea";
            clone.Click += CloneOnClick;
            workingAreaMenus.Add(clone);


            //Build context menus for vertexes
            MenuItem deleteVertex = new MenuItem();
            deleteVertex.Text = "Delete vertex";
            deleteVertex.Click += DeleteVertex;
            vertexMenus.Add(deleteVertex);
            

            //Init the listview so it shows workingArea images          
            lstExistingWorkingAreas.LargeImageList = new ImageList();
            lstExistingWorkingAreas.LargeImageList.ImageSize = new Size(100, 100);
            lstExistingWorkingAreas.LargeImageList.ColorDepth = ColorDepth.Depth32Bit;
            lstExistingWorkingAreas.ContextMenu = new ContextMenu(workingAreaMenus.ToArray());

            drawingService.FigureFillColorBack = Color.OrangeRed;
            drawingService.FigureFillColorFront = Color.IndianRed;
            drawingService.EdgePen = new Pen(Color.DarkRed);
            drawingService.VertexPen = new Pen(Color.Red);

            imgWorkingArea.Image = drawingService.GenerateBitmapWithBorder(Math.Max(imgWorkingArea.Size.Height, imgWorkingArea.Size.Width), drawingService.BorderPen, drawingService.FillColor);


            ListWorkingAreas();
        }

        private void DeleteVertex(object sender, EventArgs e)
        {
            if (lstWorkingAreaVertexes.SelectedIndices.Count == 0)
            {
                return;
            }

            int index = lstWorkingAreaVertexes.SelectedIndices[0];

            lstWorkingAreaVertexes.Items.RemoveAt(index);

            DrawSchematic();
        }

        private async Task ListWorkingAreas()
        {
            lstExistingWorkingAreas.Items.Clear();
            AddLoadingSymbol();

            int pageNumber = 1;
            List<WorkingArea> workingAreas = await workingAreasPersistence.QueryAsync<WorkingArea>(pageNumber, 10);
            lstExistingWorkingAreas.Items.Clear();

            AddNewWorkingAreaSymbol();

            while (workingAreas.Count > 0)
            {
                workingAreas = await workingAreasPersistence.QueryAsync<WorkingArea>(pageNumber, 10);

                foreach (WorkingArea workingArea in workingAreas)
                {
                    AddWorkingAreaToList(workingArea);
                }

                pageNumber++;
            }

        }

        private void CloneOnClick(object sender, EventArgs eventArgs)
        {
            if (lstExistingWorkingAreas.SelectedItems.Count == 0)
            {
                return;
            }

            ListViewItem listviewItem = lstExistingWorkingAreas.SelectedItems[0];

            WorkingArea workingArea = (WorkingArea)listviewItem.Tag;

            //WorkingArea to be cloned!

            if (DialogResult.Yes == MessageBox.Show("Are you really sure you want to clone this workingArea?", "Clone workingArea?", MessageBoxButtons.YesNo))
            {
                lstExistingWorkingAreas.SelectedItems.Clear();
                lstExistingWorkingAreas.Items.Remove(listviewItem);

                WorkingArea clone = workingArea.Clone();
                clone.Id = null;
                clone.Name = workingArea.Name + "_CLONE";

                workingAreasPersistence.AddOrUpdate(clone.Id,clone);

                ListWorkingAreas();
            }
        }

        private void DeleteOnClick(object sender, EventArgs e)
        {
            if (lstExistingWorkingAreas.SelectedItems.Count == 0)
            {
                return;
            }

            ListViewItem listviewItem = lstExistingWorkingAreas.SelectedItems[0];

            WorkingArea workingArea = (WorkingArea)listviewItem.Tag;

            //WorkingArea to be deleted!

            if (DialogResult.Yes == MessageBox.Show("Are you really sure you want to delete this workingArea?", "Delete workingArea?",MessageBoxButtons.YesNo))
            {
                lstExistingWorkingAreas.SelectedItems.Clear();
                lstExistingWorkingAreas.Items.Remove(listviewItem);

                workingAreasPersistence.Delete<WorkingArea>(workingArea.Id);
            }
        }

        private void AddWorkingAreaToList(WorkingArea workingArea)
        {
            Image bitmap = drawingService.DrawThumbnail(workingArea,100);
            
            lstExistingWorkingAreas.LargeImageList.Images.Add(workingArea.Id, bitmap);

            ListViewItem item = new ListViewItem(workingArea.Name);
            item.ImageKey = workingArea.Id;
            item.Tag = workingArea;

            lstExistingWorkingAreas.Items.Add(item);
        }

        private void AddLoadingSymbol()
        {
            WorkingArea loading = new WorkingArea();
            loading.Id = "LOADING";
            loading.Name = "LOADING";
            loading.Description = "LOADING";

            loading.Vertexes.Add(new Point(30, 10));
            loading.Vertexes.Add(new Point(70, 10));
            loading.Vertexes.Add(new Point(30, 90));
            loading.Vertexes.Add(new Point(70, 90));

            Image bitmap = drawingService.DrawThumbnail(loading, 100);

            lstExistingWorkingAreas.LargeImageList.Images.Add(loading.Id, bitmap);

            ListViewItem item = new ListViewItem(loading.Name);
            item.ImageKey = loading.Id;

            lstExistingWorkingAreas.Items.Add(item);

        }

        private void AddNewWorkingAreaSymbol()
        {
            WorkingArea newWorkingArea = new WorkingArea();
            newWorkingArea.Id = "#ADDNEWWORKINGAREA";
            newWorkingArea.Name = "ADDNEWWORKINGAREA";
            newWorkingArea.Description = "ADDNEWWORKINGAREA";

            newWorkingArea.Vertexes.Add(new Point(40, 10));
            newWorkingArea.Vertexes.Add(new Point(60, 10));
            newWorkingArea.Vertexes.Add(new Point(60, 40));

            newWorkingArea.Vertexes.Add(new Point(90, 40));
            newWorkingArea.Vertexes.Add(new Point(90, 60));
            newWorkingArea.Vertexes.Add(new Point(60, 60));

            newWorkingArea.Vertexes.Add(new Point(60, 90));
            newWorkingArea.Vertexes.Add(new Point(40, 90));
            newWorkingArea.Vertexes.Add(new Point(40, 60));

            newWorkingArea.Vertexes.Add(new Point(10, 60));
            newWorkingArea.Vertexes.Add(new Point(10, 40));
            newWorkingArea.Vertexes.Add(new Point(40, 40));

            AddWorkingAreaToList(newWorkingArea);

        }
        
        private void LstWorkingAreasItemSelected(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            lstWorkingAreaVertexes.Items.Clear();
            lstWorkingAreaVertexes.ContextMenu = new ContextMenu(vertexMenus.ToArray());
            
            WorkingArea workingArea = e.Item.Tag as WorkingArea;

            if (workingArea != null && workingArea.Id == "#ADDNEWWORKINGAREA")
            {
                workingArea = null;
            }

            bool selected = e.IsSelected && workingArea != null;
            if (selected)
            {
                txtId.Text = workingArea.Id;
                txtName.Text = workingArea.Name;
                txtDescription.Text = workingArea.Description;

                foreach (Point point in workingArea.Vertexes)
                {
                    AddVertex(point);
                }

                DrawSchematic();
            }
            else
            {
                imgWorkingArea.Image = null;

                txtId.Text = "";
                txtName.Text = "";
                txtDescription.Text = "";
            }
        }

        private void DrawSchematic()
        {
            List<Point> points = new List<Point>();


            foreach (ListViewItem item in lstWorkingAreaVertexes.Items)
            {
                points.Add((Point)item.Tag);
            }


            Image bitmap = drawingService.DrawSchematic(points, Math.Min(imgWorkingArea.Width, imgWorkingArea.Height));

            imgWorkingArea.Image = bitmap;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Are you really sure you want to save this workingArea?", "Save changes?", MessageBoxButtons.YesNo))
            {
                WorkingArea newWorkingArea = new WorkingArea();

                newWorkingArea.Id = txtId.Text;
                newWorkingArea.Name = txtName.Text;
                newWorkingArea.Description = txtDescription.Text;

                foreach (ListViewItem item in lstWorkingAreaVertexes.Items)
                {
                    newWorkingArea.Vertexes.Add((Point)item.Tag);
                }

                workingAreasPersistence.AddOrUpdate(newWorkingArea.Id,newWorkingArea);
                ListWorkingAreas();
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (lstExistingWorkingAreas.Items.Count == 0)
            {
                return;
            }

            if (DialogResult.Yes == MessageBox.Show("Are you really sure you want to discard changes to this workingArea?", "Discard changes?", MessageBoxButtons.YesNo))
            {
                int selectedIndex = lstExistingWorkingAreas.SelectedIndices[0];

                lstExistingWorkingAreas.SelectedItems.Clear();
                lstExistingWorkingAreas.SelectedIndices.Add(selectedIndex);
                
            }
        }

        private void AddVertexToWorkingArea(object sender, EventArgs e)
        {
            Point point = new Point((float)numXInternal.Value, (float)numYInternal.Value);

            AddVertex(point);
        }

        private void AddVertex(Point point)
        {
            ListViewItem item = new ListViewItem(new string[] { point.X + "", point.Y + "" });
            item.Tag = point;

            lstWorkingAreaVertexes.Items.Add(item);

            DrawSchematic();
        }

    }
}
