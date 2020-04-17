namespace Nesting.Runner
{
    partial class NestingRunner
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabPageResults = new System.Windows.Forms.TabPage();
            this.btnExportResultJson = new System.Windows.Forms.Button();
            this.btnSavePicture = new System.Windows.Forms.Button();
            this.lstResults = new System.Windows.Forms.ListView();
            this.clmClassifierName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmClassifierVersion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmTimeTaken = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmRemainingArea = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmParts = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imgDrawArea = new System.Windows.Forms.PictureBox();
            this.tabNewClassification = new System.Windows.Forms.TabPage();
            this.chkPartsSelected = new System.Windows.Forms.CheckBox();
            this.chkWorkareaSelected = new System.Windows.Forms.CheckBox();
            this.chkAlgorithmSelected = new System.Windows.Forms.CheckBox();
            this.btnRun = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.spltSub1 = new System.Windows.Forms.SplitContainer();
            this.spltSub2 = new System.Windows.Forms.SplitContainer();
            this.lstClassifiers = new System.Windows.Forms.ListView();
            this.clmClassifiers = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lstExistingWorkingAreas = new System.Windows.Forms.ListView();
            this.lstExistingParts = new System.Windows.Forms.ListView();
            this.tabNestingRunner = new System.Windows.Forms.TabControl();
            this.tabPageResults.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgDrawArea)).BeginInit();
            this.tabNewClassification.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spltSub1)).BeginInit();
            this.spltSub1.Panel1.SuspendLayout();
            this.spltSub1.Panel2.SuspendLayout();
            this.spltSub1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spltSub2)).BeginInit();
            this.spltSub2.Panel1.SuspendLayout();
            this.spltSub2.Panel2.SuspendLayout();
            this.spltSub2.SuspendLayout();
            this.tabNestingRunner.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPageResults
            // 
            this.tabPageResults.Controls.Add(this.btnExportResultJson);
            this.tabPageResults.Controls.Add(this.btnSavePicture);
            this.tabPageResults.Controls.Add(this.lstResults);
            this.tabPageResults.Controls.Add(this.imgDrawArea);
            this.tabPageResults.Location = new System.Drawing.Point(4, 22);
            this.tabPageResults.Name = "tabPageResults";
            this.tabPageResults.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageResults.Size = new System.Drawing.Size(1008, 486);
            this.tabPageResults.TabIndex = 1;
            this.tabPageResults.Text = "Results";
            this.tabPageResults.UseVisualStyleBackColor = true;
            // 
            // btnExportResultJson
            // 
            this.btnExportResultJson.Location = new System.Drawing.Point(6, 6);
            this.btnExportResultJson.Name = "btnExportResultJson";
            this.btnExportResultJson.Size = new System.Drawing.Size(125, 23);
            this.btnExportResultJson.TabIndex = 6;
            this.btnExportResultJson.Text = "Export result to JSON";
            this.btnExportResultJson.UseVisualStyleBackColor = true;
            this.btnExportResultJson.Click += new System.EventHandler(this.btnExportResultJson_Click);
            // 
            // btnSavePicture
            // 
            this.btnSavePicture.Location = new System.Drawing.Point(137, 6);
            this.btnSavePicture.Name = "btnSavePicture";
            this.btnSavePicture.Size = new System.Drawing.Size(92, 23);
            this.btnSavePicture.TabIndex = 5;
            this.btnSavePicture.Text = "Export Picture";
            this.btnSavePicture.UseVisualStyleBackColor = true;
            this.btnSavePicture.Click += new System.EventHandler(this.btnSavePicture_Click);
            // 
            // lstResults
            // 
            this.lstResults.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lstResults.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmClassifierName,
            this.clmClassifierVersion,
            this.clmTimeTaken,
            this.clmRemainingArea,
            this.clmParts});
            this.lstResults.Location = new System.Drawing.Point(0, 35);
            this.lstResults.MultiSelect = false;
            this.lstResults.Name = "lstResults";
            this.lstResults.ShowItemToolTips = true;
            this.lstResults.Size = new System.Drawing.Size(513, 451);
            this.lstResults.TabIndex = 4;
            this.lstResults.UseCompatibleStateImageBehavior = false;
            this.lstResults.View = System.Windows.Forms.View.Details;
            this.lstResults.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lstResults_ColumnClick);
            this.lstResults.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lstResults_ItemSelectionChanged);
            // 
            // clmClassifierName
            // 
            this.clmClassifierName.Text = "Classifier Name";
            this.clmClassifierName.Width = 194;
            // 
            // clmClassifierVersion
            // 
            this.clmClassifierVersion.Text = "Classifier Version";
            this.clmClassifierVersion.Width = 74;
            // 
            // clmTimeTaken
            // 
            this.clmTimeTaken.Text = "Time Taken";
            this.clmTimeTaken.Width = 74;
            // 
            // clmRemainingArea
            // 
            this.clmRemainingArea.Text = "Remaining Area";
            this.clmRemainingArea.Width = 91;
            // 
            // clmParts
            // 
            this.clmParts.Text = "Parts";
            // 
            // imgDrawArea
            // 
            this.imgDrawArea.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.imgDrawArea.Location = new System.Drawing.Point(519, 0);
            this.imgDrawArea.Name = "imgDrawArea";
            this.imgDrawArea.Size = new System.Drawing.Size(486, 486);
            this.imgDrawArea.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgDrawArea.TabIndex = 3;
            this.imgDrawArea.TabStop = false;
            // 
            // tabNewClassification
            // 
            this.tabNewClassification.Controls.Add(this.chkPartsSelected);
            this.tabNewClassification.Controls.Add(this.chkWorkareaSelected);
            this.tabNewClassification.Controls.Add(this.chkAlgorithmSelected);
            this.tabNewClassification.Controls.Add(this.btnRun);
            this.tabNewClassification.Controls.Add(this.btnRefresh);
            this.tabNewClassification.Controls.Add(this.panel1);
            this.tabNewClassification.Location = new System.Drawing.Point(4, 22);
            this.tabNewClassification.Name = "tabNewClassification";
            this.tabNewClassification.Padding = new System.Windows.Forms.Padding(3);
            this.tabNewClassification.Size = new System.Drawing.Size(1008, 486);
            this.tabNewClassification.TabIndex = 0;
            this.tabNewClassification.Text = "Solve new nesting problem";
            this.tabNewClassification.UseVisualStyleBackColor = true;
            // 
            // chkPartsSelected
            // 
            this.chkPartsSelected.AutoSize = true;
            this.chkPartsSelected.Enabled = false;
            this.chkPartsSelected.Location = new System.Drawing.Point(8, 52);
            this.chkPartsSelected.Name = "chkPartsSelected";
            this.chkPartsSelected.Size = new System.Drawing.Size(83, 17);
            this.chkPartsSelected.TabIndex = 61;
            this.chkPartsSelected.Text = "Select Parts";
            this.chkPartsSelected.UseVisualStyleBackColor = true;
            // 
            // chkWorkareaSelected
            // 
            this.chkWorkareaSelected.AutoSize = true;
            this.chkWorkareaSelected.Enabled = false;
            this.chkWorkareaSelected.Location = new System.Drawing.Point(8, 29);
            this.chkWorkareaSelected.Name = "chkWorkareaSelected";
            this.chkWorkareaSelected.Size = new System.Drawing.Size(119, 17);
            this.chkWorkareaSelected.TabIndex = 61;
            this.chkWorkareaSelected.Text = "Select a Work Area";
            this.chkWorkareaSelected.UseVisualStyleBackColor = true;
            // 
            // chkAlgorithmSelected
            // 
            this.chkAlgorithmSelected.AutoSize = true;
            this.chkAlgorithmSelected.Enabled = false;
            this.chkAlgorithmSelected.Location = new System.Drawing.Point(8, 6);
            this.chkAlgorithmSelected.Name = "chkAlgorithmSelected";
            this.chkAlgorithmSelected.Size = new System.Drawing.Size(116, 17);
            this.chkAlgorithmSelected.TabIndex = 61;
            this.chkAlgorithmSelected.Text = "Select an algorithm";
            this.chkAlgorithmSelected.UseVisualStyleBackColor = true;
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(8, 75);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(113, 23);
            this.btnRun.TabIndex = 60;
            this.btnRun.Text = "Run";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(8, 104);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(113, 23);
            this.btnRefresh.TabIndex = 59;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.spltSub1);
            this.panel1.Location = new System.Drawing.Point(127, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(881, 486);
            this.panel1.TabIndex = 0;
            // 
            // spltSub1
            // 
            this.spltSub1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spltSub1.Location = new System.Drawing.Point(0, 0);
            this.spltSub1.Name = "spltSub1";
            this.spltSub1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // spltSub1.Panel1
            // 
            this.spltSub1.Panel1.Controls.Add(this.spltSub2);
            // 
            // spltSub1.Panel2
            // 
            this.spltSub1.Panel2.Controls.Add(this.lstExistingParts);
            this.spltSub1.Size = new System.Drawing.Size(881, 486);
            this.spltSub1.SplitterDistance = 263;
            this.spltSub1.TabIndex = 58;
            // 
            // spltSub2
            // 
            this.spltSub2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spltSub2.Location = new System.Drawing.Point(0, 0);
            this.spltSub2.Name = "spltSub2";
            // 
            // spltSub2.Panel1
            // 
            this.spltSub2.Panel1.Controls.Add(this.lstClassifiers);
            // 
            // spltSub2.Panel2
            // 
            this.spltSub2.Panel2.Controls.Add(this.lstExistingWorkingAreas);
            this.spltSub2.Size = new System.Drawing.Size(881, 263);
            this.spltSub2.SplitterDistance = 213;
            this.spltSub2.TabIndex = 0;
            // 
            // lstClassifiers
            // 
            this.lstClassifiers.CheckBoxes = true;
            this.lstClassifiers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmClassifiers});
            this.lstClassifiers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstClassifiers.Location = new System.Drawing.Point(0, 0);
            this.lstClassifiers.Name = "lstClassifiers";
            this.lstClassifiers.Size = new System.Drawing.Size(213, 263);
            this.lstClassifiers.TabIndex = 54;
            this.lstClassifiers.UseCompatibleStateImageBehavior = false;
            this.lstClassifiers.View = System.Windows.Forms.View.List;
            this.lstClassifiers.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.SelectionChanged);
            // 
            // clmClassifiers
            // 
            this.clmClassifiers.Text = "Select Classifier";
            this.clmClassifiers.Width = 209;
            // 
            // lstExistingWorkingAreas
            // 
            this.lstExistingWorkingAreas.BackColor = System.Drawing.SystemColors.Window;
            this.lstExistingWorkingAreas.CheckBoxes = true;
            this.lstExistingWorkingAreas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstExistingWorkingAreas.Location = new System.Drawing.Point(0, 0);
            this.lstExistingWorkingAreas.Name = "lstExistingWorkingAreas";
            this.lstExistingWorkingAreas.Size = new System.Drawing.Size(664, 263);
            this.lstExistingWorkingAreas.TabIndex = 51;
            this.lstExistingWorkingAreas.UseCompatibleStateImageBehavior = false;
            this.lstExistingWorkingAreas.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.SelectionChanged);
            // 
            // lstExistingParts
            // 
            this.lstExistingParts.BackColor = System.Drawing.SystemColors.Window;
            this.lstExistingParts.CheckBoxes = true;
            this.lstExistingParts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstExistingParts.Location = new System.Drawing.Point(0, 0);
            this.lstExistingParts.Name = "lstExistingParts";
            this.lstExistingParts.Size = new System.Drawing.Size(881, 219);
            this.lstExistingParts.TabIndex = 49;
            this.lstExistingParts.UseCompatibleStateImageBehavior = false;
            this.lstExistingParts.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.SelectionChanged);
            // 
            // tabNestingRunner
            // 
            this.tabNestingRunner.Controls.Add(this.tabNewClassification);
            this.tabNestingRunner.Controls.Add(this.tabPageResults);
            this.tabNestingRunner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabNestingRunner.Location = new System.Drawing.Point(0, 0);
            this.tabNestingRunner.Name = "tabNestingRunner";
            this.tabNestingRunner.SelectedIndex = 0;
            this.tabNestingRunner.Size = new System.Drawing.Size(1016, 512);
            this.tabNestingRunner.TabIndex = 0;
            // 
            // NestingRunner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 512);
            this.Controls.Add(this.tabNestingRunner);
            this.Name = "NestingRunner";
            this.Text = "NestingRunner";
            this.tabPageResults.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imgDrawArea)).EndInit();
            this.tabNewClassification.ResumeLayout(false);
            this.tabNewClassification.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.spltSub1.Panel1.ResumeLayout(false);
            this.spltSub1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spltSub1)).EndInit();
            this.spltSub1.ResumeLayout(false);
            this.spltSub2.Panel1.ResumeLayout(false);
            this.spltSub2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spltSub2)).EndInit();
            this.spltSub2.ResumeLayout(false);
            this.tabNestingRunner.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabPageResults;
        private System.Windows.Forms.ListView lstResults;
        private System.Windows.Forms.ColumnHeader clmClassifierName;
        private System.Windows.Forms.ColumnHeader clmClassifierVersion;
        private System.Windows.Forms.ColumnHeader clmTimeTaken;
        private System.Windows.Forms.ColumnHeader clmRemainingArea;
        private System.Windows.Forms.PictureBox imgDrawArea;
        private System.Windows.Forms.TabPage tabNewClassification;
        private System.Windows.Forms.TabControl tabNestingRunner;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer spltSub1;
        private System.Windows.Forms.SplitContainer spltSub2;
        private System.Windows.Forms.ListView lstClassifiers;
        private System.Windows.Forms.ColumnHeader clmClassifiers;
        private System.Windows.Forms.ListView lstExistingWorkingAreas;
        private System.Windows.Forms.ListView lstExistingParts;
        private System.Windows.Forms.CheckBox chkPartsSelected;
        private System.Windows.Forms.CheckBox chkWorkareaSelected;
        private System.Windows.Forms.CheckBox chkAlgorithmSelected;
        private System.Windows.Forms.ColumnHeader clmParts;
        private System.Windows.Forms.Button btnExportResultJson;
        private System.Windows.Forms.Button btnSavePicture;
    }
}