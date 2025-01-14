using System.Windows.Forms;

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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.autoSizeColumnsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manualSizeColumnsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.findToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.ctrlNSiguienteResultSetCtrlPPrevioResultSetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.laMessages = new System.Windows.Forms.Label();
            this.btBuscar = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.txtMessages = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            this.dataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView.Location = new System.Drawing.Point(0, 27);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowHeadersWidth = 70;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            this.dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.SystemColors.Control;
            this.dataGridView.RowTemplate.Height = 24;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridView.ShowCellErrors = false;
            this.dataGridView.ShowRowErrors = false;
            this.dataGridView.Size = new System.Drawing.Size(1139, 594);
            this.dataGridView.TabIndex = 0;
            this.dataGridView.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridView_DataError);
            this.dataGridView.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridView_RowPostPaint);
            // 
            // btSalir
            // 
            this.btSalir.BackColor = System.Drawing.SystemColors.Control;
            this.btSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btSalir.Location = new System.Drawing.Point(12, 59);
            this.btSalir.Name = "btSalir";
            this.btSalir.Size = new System.Drawing.Size(41, 21);
            this.btSalir.TabIndex = 15;
            this.btSalir.Text = "Salir";
            this.btSalir.UseVisualStyleBackColor = false;
            this.btSalir.Click += new System.EventHandler(this.btSalir_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.Control;
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
            this.previoResultSetToolStripMenuItem,
            this.toolStripSeparator3,
            this.autoSizeColumnsToolStripMenuItem,
            this.manualSizeColumnsToolStripMenuItem,
            this.toolStripSeparator4,
            this.findToolStripMenuItem});
            this.archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            this.archivoToolStripMenuItem.Size = new System.Drawing.Size(37, 23);
            this.archivoToolStripMenuItem.Text = "File";
            // 
            // salirToolStripMenuItem
            // 
            this.salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            this.salirToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.salirToolStripMenuItem.Text = "Close";
            this.salirToolStripMenuItem.Click += new System.EventHandler(this.salirToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(226, 6);
            // 
            // grabarJSONToolStripMenuItem
            // 
            this.grabarJSONToolStripMenuItem.Name = "grabarJSONToolStripMenuItem";
            this.grabarJSONToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.grabarJSONToolStripMenuItem.Text = "Save Result as JSON";
            this.grabarJSONToolStripMenuItem.Click += new System.EventHandler(this.grabarJSONToolStripMenuItem_Click);
            // 
            // grabarExcellToolStripMenuItem
            // 
            this.grabarExcellToolStripMenuItem.Name = "grabarExcellToolStripMenuItem";
            this.grabarExcellToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.grabarExcellToolStripMenuItem.Text = "Save Result as Excell";
            this.grabarExcellToolStripMenuItem.Click += new System.EventHandler(this.grabarExcellToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(226, 6);
            // 
            // verMensajesToolStripMenuItem
            // 
            this.verMensajesToolStripMenuItem.Name = "verMensajesToolStripMenuItem";
            this.verMensajesToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
            this.verMensajesToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.verMensajesToolStripMenuItem.Text = "Show Messages";
            this.verMensajesToolStripMenuItem.Click += new System.EventHandler(this.verMensajesToolStripMenuItem_Click);
            // 
            // siguienteResultSetToolStripMenuItem
            // 
            this.siguienteResultSetToolStripMenuItem.Name = "siguienteResultSetToolStripMenuItem";
            this.siguienteResultSetToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.siguienteResultSetToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.siguienteResultSetToolStripMenuItem.Text = "Next Result-Set";
            this.siguienteResultSetToolStripMenuItem.Click += new System.EventHandler(this.siguienteResultSetToolStripMenuItem_Click);
            // 
            // previoResultSetToolStripMenuItem
            // 
            this.previoResultSetToolStripMenuItem.Name = "previoResultSetToolStripMenuItem";
            this.previoResultSetToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.previoResultSetToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.previoResultSetToolStripMenuItem.Text = "Previous Result-Set";
            this.previoResultSetToolStripMenuItem.Click += new System.EventHandler(this.previoResultSetToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(226, 6);
            // 
            // autoSizeColumnsToolStripMenuItem
            // 
            this.autoSizeColumnsToolStripMenuItem.Name = "autoSizeColumnsToolStripMenuItem";
            this.autoSizeColumnsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.autoSizeColumnsToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.autoSizeColumnsToolStripMenuItem.Text = "Auto Size Columns";
            this.autoSizeColumnsToolStripMenuItem.Click += new System.EventHandler(this.autoSizeColumnsToolStripMenuItem_Click);
            // 
            // manualSizeColumnsToolStripMenuItem
            // 
            this.manualSizeColumnsToolStripMenuItem.Name = "manualSizeColumnsToolStripMenuItem";
            this.manualSizeColumnsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.manualSizeColumnsToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.manualSizeColumnsToolStripMenuItem.Text = "Manual Size Columns";
            this.manualSizeColumnsToolStripMenuItem.Click += new System.EventHandler(this.manualSizeColumnsToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(226, 6);
            // 
            // findToolStripMenuItem
            // 
            this.findToolStripMenuItem.Name = "findToolStripMenuItem";
            this.findToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.findToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.findToolStripMenuItem.Text = "Find";
            this.findToolStripMenuItem.Click += new System.EventHandler(this.findToolStripMenuItem_Click);
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripTextBox1.Enabled = false;
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.Size = new System.Drawing.Size(240, 23);
            // 
            // ctrlNSiguienteResultSetCtrlPPrevioResultSetToolStripMenuItem
            // 
            this.ctrlNSiguienteResultSetCtrlPPrevioResultSetToolStripMenuItem.Name = "ctrlNSiguienteResultSetCtrlPPrevioResultSetToolStripMenuItem";
            this.ctrlNSiguienteResultSetCtrlPPrevioResultSetToolStripMenuItem.Size = new System.Drawing.Size(316, 23);
            this.ctrlNSiguienteResultSetCtrlPPrevioResultSetToolStripMenuItem.Text = "[Ctrl] [N] Next Result Set       [Ctrl] [P] Previous Result Set";
            // 
            // laMessages
            // 
            this.laMessages.AutoSize = true;
            this.laMessages.BackColor = System.Drawing.SystemColors.Control;
            this.laMessages.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.laMessages.Location = new System.Drawing.Point(922, 8);
            this.laMessages.Name = "laMessages";
            this.laMessages.Size = new System.Drawing.Size(35, 13);
            this.laMessages.TabIndex = 18;
            this.laMessages.Text = "label1";
            // 
            // btBuscar
            // 
            this.btBuscar.BackColor = System.Drawing.SystemColors.Control;
            this.btBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btBuscar.Location = new System.Drawing.Point(639, 3);
            this.btBuscar.Name = "btBuscar";
            this.btBuscar.Size = new System.Drawing.Size(61, 22);
            this.btBuscar.TabIndex = 19;
            this.btBuscar.Text = "Find (F3)";
            this.btBuscar.UseVisualStyleBackColor = false;
            this.btBuscar.Click += new System.EventHandler(this.btBuscar_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.BackColor = System.Drawing.SystemColors.Control;
            this.txtSearch.Location = new System.Drawing.Point(705, 4);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(210, 20);
            this.txtSearch.TabIndex = 20;
            // 
            // txtMessages
            // 
            this.txtMessages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMessages.BackColor = System.Drawing.Color.DimGray;
            this.txtMessages.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMessages.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMessages.ForeColor = System.Drawing.SystemColors.Info;
            this.txtMessages.Location = new System.Drawing.Point(199, 93);
            this.txtMessages.Multiline = true;
            this.txtMessages.Name = "txtMessages";
            this.txtMessages.ReadOnly = true;
            this.txtMessages.Size = new System.Drawing.Size(767, 442);
            this.txtMessages.TabIndex = 21;
            // 
            // frmDespliegue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btSalir;
            this.ClientSize = new System.Drawing.Size(1139, 621);
            this.Controls.Add(this.txtMessages);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.btBuscar);
            this.Controls.Add(this.laMessages);
            this.Controls.Add(this.btSalir);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmDespliegue";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Result Sets";
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
        private System.Windows.Forms.Label laMessages;
        private System.Windows.Forms.Button btBuscar;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.TextBox txtMessages;
        private ToolStripMenuItem autoSizeColumnsToolStripMenuItem;
        private ToolStripMenuItem manualSizeColumnsToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripMenuItem findToolStripMenuItem;
    }
   
}