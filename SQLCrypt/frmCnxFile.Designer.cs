namespace SQLCrypt
{
   partial class frmCnxFile
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCnxFile));
            this.btCrearArchivo = new System.Windows.Forms.Button();
            this.btSalir = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.btTestConn = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.sqlServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mySqLRemotoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mySqlRemotoToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.mySqlLocalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oracleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mSOdbcAntiguoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mSOdbcNuevoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oracleODBCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oDBCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sqlServerNativoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btCrearArchivo
            // 
            this.btCrearArchivo.Location = new System.Drawing.Point(3, 145);
            this.btCrearArchivo.Name = "btCrearArchivo";
            this.btCrearArchivo.Size = new System.Drawing.Size(96, 23);
            this.btCrearArchivo.TabIndex = 4;
            this.btCrearArchivo.Text = "Crear Archivo";
            this.btCrearArchivo.UseVisualStyleBackColor = true;
            this.btCrearArchivo.Click += new System.EventHandler(this.btCrearArchivo_Click);
            // 
            // btSalir
            // 
            this.btSalir.Location = new System.Drawing.Point(535, 145);
            this.btSalir.Name = "btSalir";
            this.btSalir.Size = new System.Drawing.Size(96, 23);
            this.btSalir.TabIndex = 5;
            this.btSalir.Text = "Salir";
            this.btSalir.UseVisualStyleBackColor = true;
            this.btSalir.Click += new System.EventHandler(this.btSalir_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(4, 30);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(629, 110);
            this.textBox1.TabIndex = 2;
            // 
            // btTestConn
            // 
            this.btTestConn.Location = new System.Drawing.Point(257, 145);
            this.btTestConn.Name = "btTestConn";
            this.btTestConn.Size = new System.Drawing.Size(96, 23);
            this.btTestConn.TabIndex = 8;
            this.btTestConn.Text = "Probar Conexión";
            this.btTestConn.UseVisualStyleBackColor = true;
            this.btTestConn.Click += new System.EventHandler(this.btTestConn_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sqlServerToolStripMenuItem,
            this.mySqLRemotoToolStripMenuItem,
            this.oracleToolStripMenuItem,
            this.oDBCToolStripMenuItem,
            this.sqlServerNativoToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(638, 24);
            this.menuStrip1.TabIndex = 9;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // sqlServerToolStripMenuItem
            // 
            this.sqlServerToolStripMenuItem.Name = "sqlServerToolStripMenuItem";
            this.sqlServerToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.sqlServerToolStripMenuItem.Text = "SqlServer";
            this.sqlServerToolStripMenuItem.Click += new System.EventHandler(this.sqlServerToolStripMenuItem_Click);
            // 
            // mySqLRemotoToolStripMenuItem
            // 
            this.mySqLRemotoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mySqlRemotoToolStripMenuItem1,
            this.mySqlLocalToolStripMenuItem});
            this.mySqLRemotoToolStripMenuItem.Name = "mySqLRemotoToolStripMenuItem";
            this.mySqLRemotoToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
            this.mySqLRemotoToolStripMenuItem.Text = "MySq";
            // 
            // mySqlRemotoToolStripMenuItem1
            // 
            this.mySqlRemotoToolStripMenuItem1.Name = "mySqlRemotoToolStripMenuItem1";
            this.mySqlRemotoToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.mySqlRemotoToolStripMenuItem1.Text = "MySql Remoto";
            this.mySqlRemotoToolStripMenuItem1.Click += new System.EventHandler(this.mySqlRemotoToolStripMenuItem1_Click);
            // 
            // mySqlLocalToolStripMenuItem
            // 
            this.mySqlLocalToolStripMenuItem.Name = "mySqlLocalToolStripMenuItem";
            this.mySqlLocalToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.mySqlLocalToolStripMenuItem.Text = "MySql Local";
            this.mySqlLocalToolStripMenuItem.Click += new System.EventHandler(this.mySqlLocalToolStripMenuItem_Click);
            // 
            // oracleToolStripMenuItem
            // 
            this.oracleToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mSOdbcAntiguoToolStripMenuItem,
            this.mSOdbcNuevoToolStripMenuItem,
            this.oracleODBCToolStripMenuItem});
            this.oracleToolStripMenuItem.Name = "oracleToolStripMenuItem";
            this.oracleToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.oracleToolStripMenuItem.Text = "Oracle";
            // 
            // mSOdbcAntiguoToolStripMenuItem
            // 
            this.mSOdbcAntiguoToolStripMenuItem.Name = "mSOdbcAntiguoToolStripMenuItem";
            this.mSOdbcAntiguoToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.mSOdbcAntiguoToolStripMenuItem.Text = "MS Odbc Antiguo";
            this.mSOdbcAntiguoToolStripMenuItem.Click += new System.EventHandler(this.mSOdbcAntiguoToolStripMenuItem_Click);
            // 
            // mSOdbcNuevoToolStripMenuItem
            // 
            this.mSOdbcNuevoToolStripMenuItem.Name = "mSOdbcNuevoToolStripMenuItem";
            this.mSOdbcNuevoToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.mSOdbcNuevoToolStripMenuItem.Text = "MS Odbc Nuevo";
            this.mSOdbcNuevoToolStripMenuItem.Click += new System.EventHandler(this.mSOdbcNuevoToolStripMenuItem_Click);
            // 
            // oracleODBCToolStripMenuItem
            // 
            this.oracleODBCToolStripMenuItem.Name = "oracleODBCToolStripMenuItem";
            this.oracleODBCToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.oracleODBCToolStripMenuItem.Text = "Oracle ODBC";
            this.oracleODBCToolStripMenuItem.Click += new System.EventHandler(this.oracleODBCToolStripMenuItem_Click);
            // 
            // oDBCToolStripMenuItem
            // 
            this.oDBCToolStripMenuItem.Name = "oDBCToolStripMenuItem";
            this.oDBCToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
            this.oDBCToolStripMenuItem.Text = "ODBC";
            this.oDBCToolStripMenuItem.Click += new System.EventHandler(this.oDBCToolStripMenuItem_Click);
            // 
            // sqlServerNativoToolStripMenuItem
            // 
            this.sqlServerNativoToolStripMenuItem.Name = "sqlServerNativoToolStripMenuItem";
            this.sqlServerNativoToolStripMenuItem.Size = new System.Drawing.Size(102, 20);
            this.sqlServerNativoToolStripMenuItem.Text = "SqlServerNativo";
            this.sqlServerNativoToolStripMenuItem.Click += new System.EventHandler(this.sqlServerNativoToolStripMenuItem_Click);
            // 
            // frmCnxFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(638, 173);
            this.Controls.Add(this.btTestConn);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btSalir);
            this.Controls.Add(this.btCrearArchivo);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmCnxFile";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Crea Archivo de Conexión";
            this.Load += new System.EventHandler(this.frmCnxFile_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.Button btCrearArchivo;
      private System.Windows.Forms.Button btSalir;
      private System.Windows.Forms.TextBox textBox1;
      private System.Windows.Forms.SaveFileDialog saveFileDialog1;
      private System.Windows.Forms.Button btTestConn;
      private System.Windows.Forms.MenuStrip menuStrip1;
      private System.Windows.Forms.ToolStripMenuItem sqlServerToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem mySqLRemotoToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem mySqlRemotoToolStripMenuItem1;
      private System.Windows.Forms.ToolStripMenuItem mySqlLocalToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem oracleToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem mSOdbcAntiguoToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem mSOdbcNuevoToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem oracleODBCToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem oDBCToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sqlServerNativoToolStripMenuItem;
    }
}

