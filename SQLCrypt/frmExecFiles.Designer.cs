namespace SQLCrypt
{
    partial class frmExecFiles
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmExecFiles));
            this.rtSalida = new System.Windows.Forms.RichTextBox();
            this.btSelFolders = new System.Windows.Forms.Button();
            this.btExecute = new System.Windows.Forms.Button();
            this.lstFiles = new System.Windows.Forms.ListBox();
            this.btSalir = new System.Windows.Forms.Button();
            this.btToMainWindow = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rtSalida
            // 
            this.rtSalida.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtSalida.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtSalida.Location = new System.Drawing.Point(3, 238);
            this.rtSalida.Name = "rtSalida";
            this.rtSalida.ReadOnly = true;
            this.rtSalida.Size = new System.Drawing.Size(681, 305);
            this.rtSalida.TabIndex = 1;
            this.rtSalida.Text = "";
            // 
            // btSelFolders
            // 
            this.btSelFolders.Image = ((System.Drawing.Image)(resources.GetObject("btSelFolders.Image")));
            this.btSelFolders.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btSelFolders.Location = new System.Drawing.Point(70, 2);
            this.btSelFolders.Name = "btSelFolders";
            this.btSelFolders.Size = new System.Drawing.Size(97, 30);
            this.btSelFolders.TabIndex = 2;
            this.btSelFolders.Text = "Select Folder";
            this.btSelFolders.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btSelFolders.UseVisualStyleBackColor = true;
            this.btSelFolders.Click += new System.EventHandler(this.btSelFolders_Click);
            // 
            // btExecute
            // 
            this.btExecute.Image = ((System.Drawing.Image)(resources.GetObject("btExecute.Image")));
            this.btExecute.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btExecute.Location = new System.Drawing.Point(169, 1);
            this.btExecute.Name = "btExecute";
            this.btExecute.Size = new System.Drawing.Size(88, 31);
            this.btExecute.TabIndex = 3;
            this.btExecute.Text = "Execute";
            this.btExecute.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btExecute.UseVisualStyleBackColor = true;
            this.btExecute.Click += new System.EventHandler(this.btExecute_Click);
            // 
            // lstFiles
            // 
            this.lstFiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstFiles.FormattingEnabled = true;
            this.lstFiles.Location = new System.Drawing.Point(4, 35);
            this.lstFiles.Name = "lstFiles";
            this.lstFiles.Size = new System.Drawing.Size(679, 199);
            this.lstFiles.TabIndex = 4;
            this.lstFiles.SelectedIndexChanged += new System.EventHandler(this.lstFiles_SelectedIndexChanged);
            // 
            // btSalir
            // 
            this.btSalir.Image = ((System.Drawing.Image)(resources.GetObject("btSalir.Image")));
            this.btSalir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btSalir.Location = new System.Drawing.Point(6, 1);
            this.btSalir.Name = "btSalir";
            this.btSalir.Size = new System.Drawing.Size(61, 31);
            this.btSalir.TabIndex = 5;
            this.btSalir.Text = "Salir";
            this.btSalir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btSalir.UseVisualStyleBackColor = true;
            this.btSalir.Click += new System.EventHandler(this.btSalir_Click);
            // 
            // btToMainWindow
            // 
            this.btToMainWindow.Image = ((System.Drawing.Image)(resources.GetObject("btToMainWindow.Image")));
            this.btToMainWindow.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btToMainWindow.Location = new System.Drawing.Point(260, 2);
            this.btToMainWindow.Name = "btToMainWindow";
            this.btToMainWindow.Size = new System.Drawing.Size(139, 31);
            this.btToMainWindow.TabIndex = 6;
            this.btToMainWindow.Text = "A Ventana Principal";
            this.btToMainWindow.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btToMainWindow.UseVisualStyleBackColor = true;
            this.btToMainWindow.Click += new System.EventHandler(this.btToMainWindow_Click);
            // 
            // frmExecFiles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(687, 545);
            this.Controls.Add(this.btToMainWindow);
            this.Controls.Add(this.btSalir);
            this.Controls.Add(this.lstFiles);
            this.Controls.Add(this.btExecute);
            this.Controls.Add(this.btSelFolders);
            this.Controls.Add(this.rtSalida);
            this.Name = "frmExecFiles";
            this.Text = "Ejecución de Archivos Batch";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.RichTextBox rtSalida;
        private System.Windows.Forms.Button btSelFolders;
        private System.Windows.Forms.Button btExecute;
        private System.Windows.Forms.ListBox lstFiles;
        private System.Windows.Forms.Button btSalir;
        private System.Windows.Forms.Button btToMainWindow;
    }
}