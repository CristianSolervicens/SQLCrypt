namespace SQLCrypt.frmUtiles
{
    partial class frmSnippets
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
            this.lsSnippets = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // lsSnippets
            // 
            this.lsSnippets.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsSnippets.FormattingEnabled = true;
            this.lsSnippets.Location = new System.Drawing.Point(0, 0);
            this.lsSnippets.Name = "lsSnippets";
            this.lsSnippets.Size = new System.Drawing.Size(343, 498);
            this.lsSnippets.Sorted = true;
            this.lsSnippets.TabIndex = 1;
            this.lsSnippets.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lsSnippets_MouseDoubleClick);
            this.lsSnippets.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.lsSnippets_PreviewKeyDown);
            // 
            // frmSnippets
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(343, 498);
            this.Controls.Add(this.lsSnippets);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmSnippets";
            this.Text = "frmSnippets";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lsSnippets;
    }
}