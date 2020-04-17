namespace GenericInterface.Forms
{
    partial class About
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
            this.btnOk = new System.Windows.Forms.Button();
            this.lblApplicationName = new System.Windows.Forms.Label();
            this.lblApplicationReason = new System.Windows.Forms.Label();
            this.lblApplicationAuthor = new System.Windows.Forms.Label();
            this.lblBuildNumber = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(232, 80);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // lblApplicationName
            // 
            this.lblApplicationName.AutoSize = true;
            this.lblApplicationName.Location = new System.Drawing.Point(12, 9);
            this.lblApplicationName.Name = "lblApplicationName";
            this.lblApplicationName.Size = new System.Drawing.Size(220, 13);
            this.lblApplicationName.TabIndex = 1;
            this.lblApplicationName.Text = "Generic Interface for Generic Projects (GIGP)";
            // 
            // lblApplicationReason
            // 
            this.lblApplicationReason.AutoSize = true;
            this.lblApplicationReason.Location = new System.Drawing.Point(12, 36);
            this.lblApplicationReason.Name = "lblApplicationReason";
            this.lblApplicationReason.Size = new System.Drawing.Size(294, 13);
            this.lblApplicationReason.TabIndex = 4;
            this.lblApplicationReason.Text = "Developed as Final Degree Project for Computer Engineering";
            this.lblApplicationReason.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblApplicationAuthor
            // 
            this.lblApplicationAuthor.AutoSize = true;
            this.lblApplicationAuthor.Location = new System.Drawing.Point(12, 64);
            this.lblApplicationAuthor.Name = "lblApplicationAuthor";
            this.lblApplicationAuthor.Size = new System.Drawing.Size(229, 13);
            this.lblApplicationAuthor.TabIndex = 5;
            this.lblApplicationAuthor.Text = "Pablo Torrejón Cabello ( pabtorca@gmail.com )";
            // 
            // lblBuildNumber
            // 
            this.lblBuildNumber.AutoSize = true;
            this.lblBuildNumber.Location = new System.Drawing.Point(12, 90);
            this.lblBuildNumber.Name = "lblBuildNumber";
            this.lblBuildNumber.Size = new System.Drawing.Size(74, 13);
            this.lblBuildNumber.TabIndex = 6;
            this.lblBuildNumber.Text = "Build number: ";
            // 
            // About
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(319, 116);
            this.Controls.Add(this.lblBuildNumber);
            this.Controls.Add(this.lblApplicationAuthor);
            this.Controls.Add(this.lblApplicationReason);
            this.Controls.Add(this.lblApplicationName);
            this.Controls.Add(this.btnOk);
            this.Name = "About";
            this.Text = "About";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label lblApplicationName;
        private System.Windows.Forms.Label lblApplicationReason;
        private System.Windows.Forms.Label lblApplicationAuthor;
        private System.Windows.Forms.Label lblBuildNumber;
    }
}