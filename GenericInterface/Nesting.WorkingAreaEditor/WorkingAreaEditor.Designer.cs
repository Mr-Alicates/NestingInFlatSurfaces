namespace Nesting.WorkingAreaEditor
{
    partial class WorkingAreaEditor
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
            this.lstExistingWorkingAreas = new System.Windows.Forms.ListView();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtId = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.lblId = new System.Windows.Forms.Label();
            this.numYInternal = new System.Windows.Forms.NumericUpDown();
            this.numXInternal = new System.Windows.Forms.NumericUpDown();
            this.btnAddInternalVertex = new System.Windows.Forms.Button();
            this.lstWorkingAreaVertexes = new System.Windows.Forms.ListView();
            this.clmXInt = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmYInt = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label2 = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.imgWorkingArea = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.numYInternal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numXInternal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgWorkingArea)).BeginInit();
            this.SuspendLayout();
            // 
            // lstExistingWorkingAreas
            // 
            this.lstExistingWorkingAreas.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lstExistingWorkingAreas.BackColor = System.Drawing.SystemColors.Window;
            this.lstExistingWorkingAreas.Location = new System.Drawing.Point(0, 0);
            this.lstExistingWorkingAreas.Name = "lstExistingWorkingAreas";
            this.lstExistingWorkingAreas.Size = new System.Drawing.Size(185, 512);
            this.lstExistingWorkingAreas.TabIndex = 39;
            this.lstExistingWorkingAreas.UseCompatibleStateImageBehavior = false;
            this.lstExistingWorkingAreas.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.LstWorkingAreasItemSelected);
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(257, 61);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(243, 54);
            this.txtDescription.TabIndex = 58;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(191, 61);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(60, 13);
            this.lblDescription.TabIndex = 57;
            this.lblDescription.Text = "Description";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(257, 35);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(243, 20);
            this.txtName.TabIndex = 56;
            // 
            // txtId
            // 
            this.txtId.Enabled = false;
            this.txtId.Location = new System.Drawing.Point(257, 5);
            this.txtId.Name = "txtId";
            this.txtId.Size = new System.Drawing.Size(243, 20);
            this.txtId.TabIndex = 55;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(191, 35);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(35, 13);
            this.lblName.TabIndex = 54;
            this.lblName.Text = "Name";
            // 
            // lblId
            // 
            this.lblId.AutoSize = true;
            this.lblId.Location = new System.Drawing.Point(191, 8);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(16, 13);
            this.lblId.TabIndex = 53;
            this.lblId.Text = "Id";
            // 
            // numYInternal
            // 
            this.numYInternal.DecimalPlaces = 2;
            this.numYInternal.Location = new System.Drawing.Point(348, 371);
            this.numYInternal.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.numYInternal.Name = "numYInternal";
            this.numYInternal.Size = new System.Drawing.Size(150, 20);
            this.numYInternal.TabIndex = 52;
            // 
            // numXInternal
            // 
            this.numXInternal.DecimalPlaces = 2;
            this.numXInternal.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numXInternal.Location = new System.Drawing.Point(194, 371);
            this.numXInternal.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.numXInternal.Name = "numXInternal";
            this.numXInternal.Size = new System.Drawing.Size(150, 20);
            this.numXInternal.TabIndex = 51;
            // 
            // btnAddInternalVertex
            // 
            this.btnAddInternalVertex.Location = new System.Drawing.Point(191, 397);
            this.btnAddInternalVertex.Name = "btnAddInternalVertex";
            this.btnAddInternalVertex.Size = new System.Drawing.Size(307, 23);
            this.btnAddInternalVertex.TabIndex = 50;
            this.btnAddInternalVertex.Text = "AddVertex";
            this.btnAddInternalVertex.UseVisualStyleBackColor = true;
            this.btnAddInternalVertex.Click += new System.EventHandler(this.AddVertexToWorkingArea);
            // 
            // lstWorkingAreaVertexes
            // 
            this.lstWorkingAreaVertexes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmXInt,
            this.clmYInt});
            this.lstWorkingAreaVertexes.Location = new System.Drawing.Point(194, 144);
            this.lstWorkingAreaVertexes.Name = "lstWorkingAreaVertexes";
            this.lstWorkingAreaVertexes.Size = new System.Drawing.Size(304, 221);
            this.lstWorkingAreaVertexes.TabIndex = 49;
            this.lstWorkingAreaVertexes.UseCompatibleStateImageBehavior = false;
            this.lstWorkingAreaVertexes.View = System.Windows.Forms.View.Details;
            // 
            // clmXInt
            // 
            this.clmXInt.Text = "X";
            this.clmXInt.Width = 140;
            // 
            // clmYInt
            // 
            this.clmYInt.Text = "Y";
            this.clmYInt.Width = 140;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(191, 128);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 13);
            this.label2.TabIndex = 48;
            this.label2.Text = "WorkingArea vertexes";
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(191, 442);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 58);
            this.btnReset.TabIndex = 45;
            this.btnReset.Text = "Discard changes";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(423, 442);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 58);
            this.btnOK.TabIndex = 44;
            this.btnOK.Text = "Save";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // imgWorkingArea
            // 
            this.imgWorkingArea.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.imgWorkingArea.Location = new System.Drawing.Point(504, 0);
            this.imgWorkingArea.Name = "imgWorkingArea";
            this.imgWorkingArea.Size = new System.Drawing.Size(512, 512);
            this.imgWorkingArea.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgWorkingArea.TabIndex = 40;
            this.imgWorkingArea.TabStop = false;
            // 
            // WorkingAreaEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 512);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtId);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.lblId);
            this.Controls.Add(this.numYInternal);
            this.Controls.Add(this.numXInternal);
            this.Controls.Add(this.btnAddInternalVertex);
            this.Controls.Add(this.lstWorkingAreaVertexes);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.imgWorkingArea);
            this.Controls.Add(this.lstExistingWorkingAreas);
            this.Name = "WorkingAreaEditor";
            this.Text = "WorkingAreaEditor";
            ((System.ComponentModel.ISupportInitialize)(this.numYInternal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numXInternal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgWorkingArea)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lstExistingWorkingAreas;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.NumericUpDown numYInternal;
        private System.Windows.Forms.NumericUpDown numXInternal;
        private System.Windows.Forms.Button btnAddInternalVertex;
        private System.Windows.Forms.ListView lstWorkingAreaVertexes;
        private System.Windows.Forms.ColumnHeader clmXInt;
        private System.Windows.Forms.ColumnHeader clmYInt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.PictureBox imgWorkingArea;
    }
}