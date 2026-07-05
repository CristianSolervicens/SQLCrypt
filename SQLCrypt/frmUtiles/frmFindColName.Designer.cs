namespace SQLCrypt.frmUtiles
{
    partial class frmFindColName
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
            this.txtColumnName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtColumnName
            // 
            this.txtColumnName.Location = new System.Drawing.Point(5, 6);
            this.txtColumnName.Name = "txtColumnName";
            this.txtColumnName.Size = new System.Drawing.Size(191, 20);
            this.txtColumnName.TabIndex = 0;
            this.txtColumnName.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.txtColumnName_PreviewKeyDown);
            // 
            // frmFindColName
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(201, 31);
            this.Controls.Add(this.txtColumnName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmFindColName";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "frmFindColName";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtColumnName;
    }
}