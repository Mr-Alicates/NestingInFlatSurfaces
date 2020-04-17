namespace Nesting.ClassifierList
{
    partial class ClassifierList
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
            this.lstClassifierList = new System.Windows.Forms.ListView();
            this.clmClassifierName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmClassifierVersion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmAssemblyName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmDescription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // lstClassifierList
            // 
            this.lstClassifierList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmClassifierName,
            this.clmClassifierVersion,
            this.clmAssemblyName,
            this.clmDescription});
            this.lstClassifierList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstClassifierList.Location = new System.Drawing.Point(0, 0);
            this.lstClassifierList.Name = "lstClassifierList";
            this.lstClassifierList.ShowItemToolTips = true;
            this.lstClassifierList.Size = new System.Drawing.Size(1016, 512);
            this.lstClassifierList.TabIndex = 0;
            this.lstClassifierList.UseCompatibleStateImageBehavior = false;
            this.lstClassifierList.View = System.Windows.Forms.View.Details;
            // 
            // clmClassifierName
            // 
            this.clmClassifierName.Text = "Name";
            this.clmClassifierName.Width = 200;
            // 
            // clmClassifierVersion
            // 
            this.clmClassifierVersion.Text = "Version";
            this.clmClassifierVersion.Width = 50;
            // 
            // clmAssemblyName
            // 
            this.clmAssemblyName.Text = "AssemblyName";
            this.clmAssemblyName.Width = 300;
            // 
            // clmDescription
            // 
            this.clmDescription.Text = "Description";
            this.clmDescription.Width = 430;
            // 
            // ClassifierList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 512);
            this.Controls.Add(this.lstClassifierList);
            this.Name = "ClassifierList";
            this.Text = "ClassifierList";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lstClassifierList;
        private System.Windows.Forms.ColumnHeader clmClassifierName;
        private System.Windows.Forms.ColumnHeader clmClassifierVersion;
        private System.Windows.Forms.ColumnHeader clmAssemblyName;
        private System.Windows.Forms.ColumnHeader clmDescription;
    }
}