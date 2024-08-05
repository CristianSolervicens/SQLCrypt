namespace SQLCrypt
{
   partial class frmDespliegue
   {
      /// <summary>
      /// Variable del diseñador requerida.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary>
      /// Limpiar los recursos que se estén utilizando.
      /// </summary>
      /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
      protected override void Dispose(bool disposing)
      {
         if (disposing && (components != null))
         {
            components.Dispose();
         }
         base.Dispose(disposing);
      }

      #region Código generado por el Diseñador de Windows Forms

      /// <summary>
      /// Método necesario para admitir el Diseñador. No se puede modificar
      /// el contenido del método con el editor de código.
      /// </summary>
      private void InitializeComponent()
      {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDespliegue));
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.btSalir = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.archivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.grabarJSONToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grabarExcellToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.verMensajesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.siguienteResultSetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.previoResultSetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.ctrlNSiguienteResultSetCtrlPPrevioResultSetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AllowUserToOrderColumns = true;
            this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView.Location = new System.Drawing.Point(0, 27);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowTemplate.Height = 24;
            this.dataGridView.Size = new System.Drawing.Size(1139, 594);
            this.dataGridView.TabIndex = 0;
            this.dataGridView.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridView_DataError);
            this.dataGridView.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridView_RowPostPaint);
            // 
            // btSalir
            // 
            this.btSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btSalir.Location = new System.Drawing.Point(12, 59);
            this.btSalir.Name = "btSalir";
            this.btSalir.Size = new System.Drawing.Size(41, 21);
            this.btSalir.TabIndex = 15;
            this.btSalir.Text = "Salir";
            this.btSalir.UseVisualStyleBackColor = true;
            this.btSalir.Click += new System.EventHandler(this.btSalir_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archivoToolStripMenuItem,
            this.toolStripTextBox1,
            this.ctrlNSiguienteResultSetCtrlPPrevioResultSetToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1139, 27);
            this.menuStrip1.TabIndex = 17;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // archivoToolStripMenuItem
            // 
            this.archivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.salirToolStripMenuItem,
            this.toolStripSeparator2,
            this.grabarJSONToolStripMenuItem,
            this.grabarExcellToolStripMenuItem,
            this.toolStripSeparator1,
            this.verMensajesToolStripMenuItem,
            this.siguienteResultSetToolStripMenuItem,
            this.previoResultSetToolStripMenuItem});
            this.archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            this.archivoToolStripMenuItem.Size = new System.Drawing.Size(60, 23);
            this.archivoToolStripMenuItem.Text = "Archivo";
            // 
            // salirToolStripMenuItem
            // 
            this.salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            this.salirToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.salirToolStripMenuItem.Text = "Salir";
            this.salirToolStripMenuItem.Click += new System.EventHandler(this.salirToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(219, 6);
            // 
            // grabarJSONToolStripMenuItem
            // 
            this.grabarJSONToolStripMenuItem.Name = "grabarJSONToolStripMenuItem";
            this.grabarJSONToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.grabarJSONToolStripMenuItem.Text = "Grabar JSON";
            this.grabarJSONToolStripMenuItem.Click += new System.EventHandler(this.grabarJSONToolStripMenuItem_Click);
            // 
            // grabarExcellToolStripMenuItem
            // 
            this.grabarExcellToolStripMenuItem.Name = "grabarExcellToolStripMenuItem";
            this.grabarExcellToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.grabarExcellToolStripMenuItem.Text = "Grabar Excell";
            this.grabarExcellToolStripMenuItem.Click += new System.EventHandler(this.grabarExcellToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(219, 6);
            // 
            // verMensajesToolStripMenuItem
            // 
            this.verMensajesToolStripMenuItem.Name = "verMensajesToolStripMenuItem";
            this.verMensajesToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
            this.verMensajesToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.verMensajesToolStripMenuItem.Text = "Ver Mensajes";
            this.verMensajesToolStripMenuItem.Click += new System.EventHandler(this.verMensajesToolStripMenuItem_Click);
            // 
            // siguienteResultSetToolStripMenuItem
            // 
            this.siguienteResultSetToolStripMenuItem.Name = "siguienteResultSetToolStripMenuItem";
            this.siguienteResultSetToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.siguienteResultSetToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.siguienteResultSetToolStripMenuItem.Text = "Siguiente Result-Set";
            this.siguienteResultSetToolStripMenuItem.Click += new System.EventHandler(this.siguienteResultSetToolStripMenuItem_Click);
            // 
            // previoResultSetToolStripMenuItem
            // 
            this.previoResultSetToolStripMenuItem.Name = "previoResultSetToolStripMenuItem";
            this.previoResultSetToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.previoResultSetToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.previoResultSetToolStripMenuItem.Text = "Previo Result-Set";
            this.previoResultSetToolStripMenuItem.Click += new System.EventHandler(this.previoResultSetToolStripMenuItem_Click);
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.Enabled = false;
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.Size = new System.Drawing.Size(500, 23);
            // 
            // ctrlNSiguienteResultSetCtrlPPrevioResultSetToolStripMenuItem
            // 
            this.ctrlNSiguienteResultSetCtrlPPrevioResultSetToolStripMenuItem.Name = "ctrlNSiguienteResultSetCtrlPPrevioResultSetToolStripMenuItem";
            this.ctrlNSiguienteResultSetCtrlPPrevioResultSetToolStripMenuItem.Size = new System.Drawing.Size(328, 23);
            this.ctrlNSiguienteResultSetCtrlPPrevioResultSetToolStripMenuItem.Text = "[Ctrl] [N] Siguiente Result Set       [Ctrl] [P] Previo Result Set";
            // 
            // frmDespliegue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btSalir;
            this.ClientSize = new System.Drawing.Size(1139, 621);
            this.Controls.Add(this.btSalir);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmDespliegue";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Despliegue de resultados";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmDespliegue_Closing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmDespliegue_Closed);
            this.Load += new System.EventHandler(this.frmDespliegue_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.DataGridView dataGridView;
      private System.Windows.Forms.Button btSalir;
      private System.Windows.Forms.MenuStrip menuStrip1;
      private System.Windows.Forms.ToolStripMenuItem archivoToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem salirToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem grabarExcellToolStripMenuItem;
      private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
        private System.Windows.Forms.ToolStripMenuItem verMensajesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem siguienteResultSetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem previoResultSetToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem grabarJSONToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem ctrlNSiguienteResultSetCtrlPPrevioResultSetToolStripMenuItem;
    }
   
}