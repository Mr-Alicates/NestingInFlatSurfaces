using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Core.Interfaces;
using Core.Plugins;
using Nesting.Core.Classes;
using Nesting.Core.Classes.Classification;
using Nesting.Core.Classes.Nesting;
using Nesting.Core.Drawing;
using Nesting.Core.Interfaces;
using Newtonsoft.Json;
using Point = Core.Nesting.Point;

namespace Nesting.Runner
{
    public partial class NestingRunner : Form
    {
        private DrawingService resultsDrawingService = new DrawingService();
        private DrawingService partDrawingService = new DrawingService();
        private DrawingService workingAreaDrawingService = new DrawingService();
        private ICore applicationCore;

        private int currentSortingColumn = 3;
        private bool currentSortingOrder = true;
        private int currentlyRunningClassifiers = 0;
        private int? currentlySelectedWorkArea;

        public NestingRunner(ICore applicationCore)
        {
            InitializeComponent();
            this.applicationCore = applicationCore;



            Init();
        }

        private void Init()
        {
            //Init the listview so it shows part images          
            lstExistingParts.LargeImageList = new ImageList();
            lstExistingParts.LargeImageList.ImageSize = new Size(100, 100);
            lstExistingParts.LargeImageList.ColorDepth = ColorDepth.Depth32Bit;


            partDrawingService.FigureFillColorBack = Color.CornflowerBlue;
            partDrawingService.FigureFillColorFront = Color.LightBlue;
            partDrawingService.EdgePen = new Pen(Color.DarkBlue);


            //Init the listview so it shows workingArea images          
            lstExistingWorkingAreas.LargeImageList = new ImageList();
            lstExistingWorkingAreas.LargeImageList.ImageSize = new Size(100, 100);
            lstExistingWorkingAreas.LargeImageList.ColorDepth = ColorDepth.Depth32Bit;

            workingAreaDrawingService.FigureFillColorBack = Color.OrangeRed;
            workingAreaDrawingService.FigureFillColorFront = Color.IndianRed;
            workingAreaDrawingService.EdgePen = new Pen(Color.DarkRed);
            workingAreaDrawingService.VertexPen = new Pen(Color.Red);

            resultsDrawingService.FigureFillColorBack = Color.LightSeaGreen;
            resultsDrawingService.FigureFillColorFront = Color.LightGreen;
            resultsDrawingService.EdgePen = new Pen(Color.DarkGreen);
            resultsDrawingService.VertexPen = new Pen(Color.Green);

            imgDrawArea.Image = resultsDrawingService.GenerateBitmapWithBorder(Math.Max(imgDrawArea.Size.Height, imgDrawArea.Size.Width), resultsDrawingService.BorderPen, resultsDrawingService.FillColor);
            
            ListParts();
            ListWorkingAreas();
        }

        #region Classifiers

        public void LoadClassifierFactories(List<INestingClassifierFactory> factories)
        {
            lstClassifiers.Items.Clear();
            
            foreach (INestingClassifierFactory factory in factories)
            {
                ClassifierInformation information = factory.ClassifierInformation;

                ListViewItem item = new ListViewItem(information.Name);
                item.ToolTipText = information.Description;
                item.Tag = factory;

                lstClassifiers.Items.Add(item);
            }
        }

        #endregion

        #region Parts

        private async Task ListParts()
        {
            lstExistingParts.Items.Clear();
            AddLoadingSymbol();

            int pageNumber = 1;
            List<Part> parts = await applicationCore.GetPersistenceService().QueryAsync<Part>(pageNumber, 10);
            lstExistingParts.Items.Clear();
            
            while (parts.Count > 0)
            {
                parts = await applicationCore.GetPersistenceService().QueryAsync<Part>(pageNumber, 10);

                foreach (Part part in parts)
                {
                    AddPartToList(part);
                }

                pageNumber++;
            }

        }

        private void AddLoadingSymbol()
        {
            Part loading = new Part();
            loading.Id = "LOADING";
            loading.Name = "LOADING";
            loading.Description = "LOADING";

            loading.Vertexes.Add(new global::Core.Nesting.Point(30, 10));
            loading.Vertexes.Add(new global::Core.Nesting.Point(70, 10));
            loading.Vertexes.Add(new global::Core.Nesting.Point(30, 90));
            loading.Vertexes.Add(new Point(70, 90));

            Image bitmap = partDrawingService.DrawThumbnail(loading, 100);

            lstExistingParts.LargeImageList.Images.Add(loading.Id, bitmap);

            ListViewItem item = new ListViewItem(loading.Name);
            item.ImageKey = loading.Id;

            lstExistingParts.Items.Add(item);

        }
        
        private void AddPartToList(Part part)
        {
            Image bitmap = partDrawingService.DrawThumbnail(part, 100);

            lstExistingParts.LargeImageList.Images.Add(part.Id, bitmap);

            ListViewItem item = new ListViewItem(part.Name);
            item.ImageKey = part.Id;
            item.Tag = part;

            lstExistingParts.Items.Add(item);
        }

        #endregion Parts

        #region WorkingAreas


        private async Task ListWorkingAreas()
        {
            lstExistingWorkingAreas.Items.Clear();
            AddLoadingSymbolWorkingAreas();

            int pageNumber = 1;
            List<WorkingArea> workingAreas = await applicationCore.GetPersistenceService().QueryAsync<WorkingArea>(pageNumber, 10);
            lstExistingWorkingAreas.Items.Clear();
            
            while (workingAreas.Count > 0)
            {
                workingAreas = await applicationCore.GetPersistenceService().QueryAsync<WorkingArea>(pageNumber, 10);

                foreach (WorkingArea workingArea in workingAreas)
                {
                    AddWorkingAreaToList(workingArea);
                }

                pageNumber++;
            }

        }


        private void AddLoadingSymbolWorkingAreas()
        {
            WorkingArea loading = new WorkingArea();
            loading.Id = "LOADING";
            loading.Name = "LOADING";
            loading.Description = "LOADING";

            loading.Vertexes.Add(new Point(30, 10));
            loading.Vertexes.Add(new Point(70, 10));
            loading.Vertexes.Add(new Point(30, 90));
            loading.Vertexes.Add(new Point(70, 90));

            Image bitmap = workingAreaDrawingService.DrawThumbnail(loading, 100);

            lstExistingWorkingAreas.LargeImageList.Images.Add(loading.Id, bitmap);

            ListViewItem item = new ListViewItem(loading.Name);
            item.ImageKey = loading.Id;

            lstExistingWorkingAreas.Items.Add(item);

        }


        private void AddWorkingAreaToList(WorkingArea workingArea)
        {
            Image bitmap = workingAreaDrawingService.DrawThumbnail(workingArea, 100);

            lstExistingWorkingAreas.LargeImageList.Images.Add(workingArea.Id, bitmap);

            ListViewItem item = new ListViewItem(workingArea.Name);
            item.ImageKey = workingArea.Id;
            item.Tag = workingArea;

            lstExistingWorkingAreas.Items.Add(item);
        }

        #endregion WorkginAreas

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ListParts();
            ListWorkingAreas();
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            this.results = new List<ClassificationResult>();

            //Extraemos el area de trabajo
            
            ListViewItem selectedItem = lstExistingWorkingAreas.CheckedItems[0];
            WorkingArea area = (WorkingArea) selectedItem.Tag;
            
            List<Part> parts = new List<Part>();

            foreach (ListViewItem item in lstExistingParts.CheckedItems)
            {
                Part part = ((Part) item.Tag).Clone();
                part.NormalizeVertexes();
                parts.Add(part);
            }
            
            List<ClassifierWrapper> classifiers = new List<ClassifierWrapper>();

            foreach (ListViewItem item in lstClassifiers.CheckedItems)
            {
                INestingClassifierFactory factory = (INestingClassifierFactory) item.Tag;

                INestingClassifier classifier = factory.Create();

                classifier.SetNestingManager(new NestingManager());
                classifier.SetClassificationParameters(new ClassificationParameters(area, parts));

                classifiers.Add(new ClassifierWrapper(classifier));
            }

            currentlyRunningClassifiers = classifiers.Count;
            btnRun.Enabled = false;
            btnRun.Text = $"{currentlyRunningClassifiers} classifiers running";

            foreach (ClassifierWrapper wrapper in classifiers)
            {
                wrapper.ClassifyAllCompleted += ReceiveResult;

                wrapper.ClassifyAllAsync();
            }
            
            PaintResults();
        }

        //Codigo relativo a la clasifiación

        private List<ClassificationResult> results = new List<ClassificationResult>();

        private void ReceiveResult(object sender, AsyncCompletedEventArgs e)
        {
            currentlyRunningClassifiers--;

            if (currentlyRunningClassifiers == 0)
            {
                btnRun.Enabled = true;
                btnRun.Text = "Run";
            }
            else
            {
                btnRun.Text = $"{currentlyRunningClassifiers} classifiers running";
            }

            List<ClassificationResult> partialResult = e.UserState as List<ClassificationResult>;

            if (partialResult != null && partialResult.Count > 0)
            {
                results.AddRange(partialResult);
                PaintResults();
            }

        }

        private void PaintResults()
        {
            lstResults.Items.Clear();

            switch (currentSortingColumn)
            {
                case 0:
                    results = results.OrderBy(x => x.Classifier.Name).ToList();
                    break;

                case 1:
                    results = results.OrderBy(x => x.Classifier.Version).ToList();
                    break;

                case 2:
                    results = results.OrderBy(x => x.TimeTaken).ToList();
                    break;

                case 3:
                    results = results.OrderBy(x => x.RemainingArea).ToList();
                    break;

                case 4:
                    results = results.OrderBy(x => x.Parts.Count).ToList();
                    break;

            }


            if (currentSortingOrder)
            {
                results.Reverse();
            }


            foreach (ClassificationResult result in results)
            {
                ListViewItem item = new ListViewItem(result.Classifier.Name);
                item.SubItems.Add(result.Classifier.Version);

                if (result.HasError)
                {
                    item.SubItems.Add("Error!");
                    item.ToolTipText = result.Error.ToString();
                }
                else
                {
                    item.SubItems.Add(result.TimeTaken.Value.TotalSeconds + "");
                    item.SubItems.Add(result.RemainingArea + "");
                    item.SubItems.Add(result.Parts.Count + "");
                }

                item.Tag = result;

                lstResults.Items.Add(item);

                if (lstResults.Items.Count == 1)
                {
                    item.Selected = true;
                }
            }


        }

        //Result selected
        private void lstResults_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            ListView item = sender as ListView;

            if (item == null || item.SelectedItems.Count == 0)
            {
                return;
            }

            ClassificationResult result = item.SelectedItems[0].Tag as ClassificationResult;

            if (result == null)
            {
                return;
            }

            Bitmap bitmap = resultsDrawingService.DrawResult(result, Math.Max(imgDrawArea.Size.Height, imgDrawArea.Size.Width));

            imgDrawArea.Image = bitmap;
        }

        private void SelectionChanged(object sender, ItemCheckedEventArgs e)
        {
            if (lstExistingWorkingAreas.CheckedIndices.Count == 0)
            {
                currentlySelectedWorkArea = null;
            }
            else if (lstExistingWorkingAreas.CheckedIndices.Count == 1)
            {
                currentlySelectedWorkArea = lstExistingWorkingAreas.CheckedIndices[0];
            }
            else
            {
                lstExistingWorkingAreas.Items[currentlySelectedWorkArea.Value].Checked = false;

                currentlySelectedWorkArea = lstExistingWorkingAreas.CheckedIndices[0];
            }

            chkAlgorithmSelected.Checked = lstClassifiers.CheckedItems.Count > 0;
            chkPartsSelected.Checked = lstExistingParts.CheckedItems.Count > 0;
            chkWorkareaSelected.Checked = lstExistingWorkingAreas.CheckedItems.Count > 0;

            btnRun.Enabled = chkAlgorithmSelected.Checked && chkPartsSelected.Checked && chkWorkareaSelected.Checked;
        }

        private void lstResults_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            int newSortingColumn = e.Column;

            if (currentSortingColumn == newSortingColumn)
            {
                currentSortingOrder = !currentSortingOrder;
            }

            currentSortingColumn = newSortingColumn;

            PaintResults();
        }

        private void btnSavePicture_Click(object sender, EventArgs e)
        {
            ClassificationResult result = lstResults.SelectedItems[0].Tag as ClassificationResult;

            if (result == null)
            {
                return;
            }

            SaveFileDialog savefile = new SaveFileDialog();
            // set a default file name
            savefile.FileName = result.Classifier.Name +"-"+DateTime.Now.ToFileTimeUtc()+".png";
            // set filters - this can be done in properties as well
            savefile.Filter = "Image files (*.png)|*.png|All files (*.*)|*.*";

            if (savefile.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            
            Bitmap bitmap = resultsDrawingService.DrawResult(result,1000);

            bitmap.Save(savefile.FileName,ImageFormat.Png);
        }

        private void btnExportResultJson_Click(object sender, EventArgs e)
        {
            ClassificationResult result = lstResults.SelectedItems[0].Tag as ClassificationResult;

            if (result == null)
            {
                return;
            }

            SaveFileDialog savefile = new SaveFileDialog();
            // set a default file name
            savefile.FileName = result.Classifier.Name + "-" + DateTime.Now.ToFileTimeUtc() + ".json";
            // set filters - this can be done in properties as well
            savefile.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";

            if (savefile.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            
            File.WriteAllText(savefile.FileName, JsonConvert.SerializeObject(result, Formatting.Indented));
        }
    }
}
