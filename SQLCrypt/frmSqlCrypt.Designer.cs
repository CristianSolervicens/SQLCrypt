namespace SQLCrypt
{
   partial class FrmSqlCrypt
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSqlCrypt));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.archivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.archivoDeConexiónToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ConectTSM = new System.Windows.Forms.ToolStripMenuItem();
            this.directorioDeTrabajoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ejecutarComandoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ejecutarTodasLasBasesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.comandosInmediatosToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.encriptarClavesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.seleccionarTodoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.IndexacionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.verPanelDeObjetosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buscaPaginaSQLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.analizarDeadlocksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ejecutarArchivosEnBatchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.extendedPropertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buscarEnBDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.crearArchivoDeConexiónToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.abrirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grabarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grabarComoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.salidaATextoGrillaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findReplaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toUpperToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toLowerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cerrarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ayudToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.acercaDeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buscarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.databasesToolStripMenuItem = new System.Windows.Forms.ToolStripComboBox();
            this.chkToText = new System.Windows.Forms.CheckBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
            this.tssLaFile = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssLaPath = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssLaPos = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssLaStat = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitC = new System.Windows.Forms.SplitContainer();
            this.laBuscarTablas = new System.Windows.Forms.Label();
            this.txBuscaEnLista = new System.Windows.Forms.TextBox();
            this.panObjetos = new System.Windows.Forms.Panel();
            this.cbObjetos = new System.Windows.Forms.ComboBox();
            this.laTablas = new System.Windows.Forms.Label();
            this.lstObjetos = new System.Windows.Forms.ListBox();
            this.panColumnas = new System.Windows.Forms.Panel();
            this.lsColumnas = new System.Windows.Forms.ListView();
            this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colNullable = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.txtSql = new ScintillaNET.Scintilla();
            this.btIndentShow = new System.Windows.Forms.Button();
            this.btSpacesShow = new System.Windows.Forms.Button();
            this.btBuscarEnBd = new System.Windows.Forms.Button();
            this.btConnectToBd = new System.Windows.Forms.Button();
            this.txTextLimit = new System.Windows.Forms.MyTextBox();
            this.btCutTrailing = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitC)).BeginInit();
            this.splitC.Panel1.SuspendLayout();
            this.splitC.Panel2.SuspendLayout();
            this.splitC.SuspendLayout();
            this.panObjetos.SuspendLayout();
            this.panColumnas.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archivoToolStripMenuItem,
            this.ayudToolStripMenuItem,
            this.buscarToolStripMenuItem,
            this.databasesToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 1, 0, 1);
            this.menuStrip1.Size = new System.Drawing.Size(1531, 30);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // archivoToolStripMenuItem
            // 
            this.archivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archivoDeConexiónToolStripMenuItem,
            this.ConectTSM,
            this.directorioDeTrabajoToolStripMenuItem,
            this.ejecutarComandoToolStripMenuItem,
            this.ejecutarTodasLasBasesToolStripMenuItem,
            this.comandosInmediatosToolStripMenuItem1,
            this.encriptarClavesToolStripMenuItem,
            this.seleccionarTodoToolStripMenuItem,
            this.IndexacionToolStripMenuItem,
            this.verPanelDeObjetosToolStripMenuItem,
            this.buscaPaginaSQLToolStripMenuItem,
            this.analizarDeadlocksToolStripMenuItem,
            this.ejecutarArchivosEnBatchToolStripMenuItem,
            this.extendedPropertiesToolStripMenuItem,
            this.buscarEnBDToolStripMenuItem,
            this.toolStripSeparator1,
            this.crearArchivoDeConexiónToolStripMenuItem,
            this.abrirToolStripMenuItem,
            this.grabarToolStripMenuItem,
            this.grabarComoToolStripMenuItem,
            this.toolStripSeparator2,
            this.salidaATextoGrillaToolStripMenuItem,
            this.findReplaceToolStripMenuItem,
            this.toUpperToolStripMenuItem,
            this.toLowerToolStripMenuItem,
            this.cerrarToolStripMenuItem,
            this.salirToolStripMenuItem});
            this.archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            this.archivoToolStripMenuItem.Size = new System.Drawing.Size(73, 28);
            this.archivoToolStripMenuItem.Text = "Archivo";
            // 
            // archivoDeConexiónToolStripMenuItem
            // 
            this.archivoDeConexiónToolStripMenuItem.Name = "archivoDeConexiónToolStripMenuItem";
            this.archivoDeConexiónToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.archivoDeConexiónToolStripMenuItem.Size = new System.Drawing.Size(333, 26);
            this.archivoDeConexiónToolStripMenuItem.Text = "Archivo de Conexión";
            this.archivoDeConexiónToolStripMenuItem.Click += new System.EventHandler(this.archivoDeConexiónToolStripMenuItem_Click);
            // 
            // ConectTSM
            // 
            this.ConectTSM.Name = "ConectTSM";
            this.ConectTSM.ShortcutKeys = System.Windows.Forms.Keys.F7;
            this.ConectTSM.Size = new System.Drawing.Size(333, 26);
            this.ConectTSM.Text = "Conectar";
            this.ConectTSM.Click += new System.EventHandler(this.ConectTSM_Click);
            // 
            // directorioDeTrabajoToolStripMenuItem
            // 
            this.directorioDeTrabajoToolStripMenuItem.Name = "directorioDeTrabajoToolStripMenuItem";
            this.directorioDeTrabajoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
            this.directorioDeTrabajoToolStripMenuItem.Size = new System.Drawing.Size(333, 26);
            this.directorioDeTrabajoToolStripMenuItem.Text = "Directorio de Trabajo";
            this.directorioDeTrabajoToolStripMenuItem.Click += new System.EventHandler(this.directorioDeTrabajoToolStripMenuItem_Click);
            // 
            // ejecutarComandoToolStripMenuItem
            // 
            this.ejecutarComandoToolStripMenuItem.Name = "ejecutarComandoToolStripMenuItem";
            this.ejecutarComandoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.ejecutarComandoToolStripMenuItem.Size = new System.Drawing.Size(333, 26);
            this.ejecutarComandoToolStripMenuItem.Text = "Ejecutar Comando/Selección";
            this.ejecutarComandoToolStripMenuItem.Click += new System.EventHandler(this.ejecutarComandoToolStripMenuItem_Click);
            // 
            // ejecutarTodasLasBasesToolStripMenuItem
            // 
            this.ejecutarTodasLasBasesToolStripMenuItem.Name = "ejecutarTodasLasBasesToolStripMenuItem";
            this.ejecutarTodasLasBasesToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
            this.ejecutarTodasLasBasesToolStripMenuItem.Size = new System.Drawing.Size(333, 26);
            this.ejecutarTodasLasBasesToolStripMenuItem.Text = "Ejecutar En Todas Las Bases";
            this.ejecutarTodasLasBasesToolStripMenuItem.Click += new System.EventHandler(this.ejecutarTodasLasBasesToolStripMenuItem_Click);
            // 
            // comandosInmediatosToolStripMenuItem1
            // 
            this.comandosInmediatosToolStripMenuItem1.Name = "comandosInmediatosToolStripMenuItem1";
            this.comandosInmediatosToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.comandosInmediatosToolStripMenuItem1.Size = new System.Drawing.Size(333, 26);
            this.comandosInmediatosToolStripMenuItem1.Text = "Comandos Inmediatos";
            this.comandosInmediatosToolStripMenuItem1.Click += new System.EventHandler(this.comandosInmediatosToolStripMenuItem1_Click);
            // 
            // encriptarClavesToolStripMenuItem
            // 
            this.encriptarClavesToolStripMenuItem.Name = "encriptarClavesToolStripMenuItem";
            this.encriptarClavesToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.encriptarClavesToolStripMenuItem.Size = new System.Drawing.Size(333, 26);
            this.encriptarClavesToolStripMenuItem.Text = "Encriptar Claves";
            this.encriptarClavesToolStripMenuItem.Click += new System.EventHandler(this.encriptarClavesToolStripMenuItem_Click);
            // 
            // seleccionarTodoToolStripMenuItem
            // 
            this.seleccionarTodoToolStripMenuItem.Name = "seleccionarTodoToolStripMenuItem";
            this.seleccionarTodoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.K)));
            this.seleccionarTodoToolStripMenuItem.Size = new System.Drawing.Size(333, 26);
            this.seleccionarTodoToolStripMenuItem.Text = "Seleccionar Todo";
            this.seleccionarTodoToolStripMenuItem.Click += new System.EventHandler(this.seleccionarTodoToolStripMenuItem_Click);
            // 
            // IndexacionToolStripMenuItem
            // 
            this.IndexacionToolStripMenuItem.Name = "IndexacionToolStripMenuItem";
            this.IndexacionToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.IndexacionToolStripMenuItem.Size = new System.Drawing.Size(333, 26);
            this.IndexacionToolStripMenuItem.Text = "Indexación";
            this.IndexacionToolStripMenuItem.Click += new System.EventHandler(this.IndexacionToolStripMenuItem_Click);
            // 
            // verPanelDeObjetosToolStripMenuItem
            // 
            this.verPanelDeObjetosToolStripMenuItem.Name = "verPanelDeObjetosToolStripMenuItem";
            this.verPanelDeObjetosToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.J)));
            this.verPanelDeObjetosToolStripMenuItem.Size = new System.Drawing.Size(333, 26);
            this.verPanelDeObjetosToolStripMenuItem.Text = "Ver Panel de Tablas";
            this.verPanelDeObjetosToolStripMenuItem.Click += new System.EventHandler(this.verPanelDeObjetosToolStripMenuItem_Click);
            // 
            // buscaPaginaSQLToolStripMenuItem
            // 
            this.buscaPaginaSQLToolStripMenuItem.Name = "buscaPaginaSQLToolStripMenuItem";
            this.buscaPaginaSQLToolStripMenuItem.Size = new System.Drawing.Size(333, 26);
            this.buscaPaginaSQLToolStripMenuItem.Text = "Busca Pagina SQL";
            this.buscaPaginaSQLToolStripMenuItem.Click += new System.EventHandler(this.buscaPaginaSQLToolStripMenuItem_Click);
            // 
            // analizarDeadlocksToolStripMenuItem
            // 
            this.analizarDeadlocksToolStripMenuItem.Name = "analizarDeadlocksToolStripMenuItem";
            this.analizarDeadlocksToolStripMenuItem.Size = new System.Drawing.Size(333, 26);
            this.analizarDeadlocksToolStripMenuItem.Text = "Analizar Deadlocks";
            this.analizarDeadlocksToolStripMenuItem.Click += new System.EventHandler(this.analizarDeadlocksToolStripMenuItem_Click);
            // 
            // ejecutarArchivosEnBatchToolStripMenuItem
            // 
            this.ejecutarArchivosEnBatchToolStripMenuItem.Name = "ejecutarArchivosEnBatchToolStripMenuItem";
            this.ejecutarArchivosEnBatchToolStripMenuItem.Size = new System.Drawing.Size(333, 26);
            this.ejecutarArchivosEnBatchToolStripMenuItem.Text = "Ejecutar Archivos en Batch";
            this.ejecutarArchivosEnBatchToolStripMenuItem.Click += new System.EventHandler(this.ejecutarArchivosEnBatchToolStripMenuItem_Click);
            // 
            // extendedPropertiesToolStripMenuItem
            // 
            this.extendedPropertiesToolStripMenuItem.Name = "extendedPropertiesToolStripMenuItem";
            this.extendedPropertiesToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.extendedPropertiesToolStripMenuItem.Size = new System.Drawing.Size(333, 26);
            this.extendedPropertiesToolStripMenuItem.Text = "Extended Properties";
            this.extendedPropertiesToolStripMenuItem.Click += new System.EventHandler(this.extendedPropertiesToolStripMenuItem_Click);
            // 
            // buscarEnBDToolStripMenuItem
            // 
            this.buscarEnBDToolStripMenuItem.Name = "buscarEnBDToolStripMenuItem";
            this.buscarEnBDToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B)));
            this.buscarEnBDToolStripMenuItem.Size = new System.Drawing.Size(333, 26);
            this.buscarEnBDToolStripMenuItem.Text = "Buscar en BD";
            this.buscarEnBDToolStripMenuItem.Click += new System.EventHandler(this.buscarEnBDToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(330, 6);
            // 
            // crearArchivoDeConexiónToolStripMenuItem
            // 
            this.crearArchivoDeConexiónToolStripMenuItem.Name = "crearArchivoDeConexiónToolStripMenuItem";
            this.crearArchivoDeConexiónToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.crearArchivoDeConexiónToolStripMenuItem.Size = new System.Drawing.Size(333, 26);
            this.crearArchivoDeConexiónToolStripMenuItem.Text = "Crear Archivo de Conexión";
            this.crearArchivoDeConexiónToolStripMenuItem.Click += new System.EventHandler(this.crearArchivoDeConexiónToolStripMenuItem_Click);
            // 
            // abrirToolStripMenuItem
            // 
            this.abrirToolStripMenuItem.Name = "abrirToolStripMenuItem";
            this.abrirToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.abrirToolStripMenuItem.Size = new System.Drawing.Size(333, 26);
            this.abrirToolStripMenuItem.Text = "Abrir Archivo";
            this.abrirToolStripMenuItem.Click += new System.EventHandler(this.abrirToolStripMenuItem_Click);
            // 
            // grabarToolStripMenuItem
            // 
            this.grabarToolStripMenuItem.Name = "grabarToolStripMenuItem";
            this.grabarToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.grabarToolStripMenuItem.Size = new System.Drawing.Size(333, 26);
            this.grabarToolStripMenuItem.Text = "&Grabar";
            this.grabarToolStripMenuItem.Click += new System.EventHandler(this.grabarToolStripMenuItem_Click);
            // 
            // grabarComoToolStripMenuItem
            // 
            this.grabarComoToolStripMenuItem.Name = "grabarComoToolStripMenuItem";
            this.grabarComoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.grabarComoToolStripMenuItem.Size = new System.Drawing.Size(333, 26);
            this.grabarComoToolStripMenuItem.Text = "Grabar como...";
            this.grabarComoToolStripMenuItem.Click += new System.EventHandler(this.grabarComoToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(330, 6);
            // 
            // salidaATextoGrillaToolStripMenuItem
            // 
            this.salidaATextoGrillaToolStripMenuItem.Name = "salidaATextoGrillaToolStripMenuItem";
            this.salidaATextoGrillaToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.salidaATextoGrillaToolStripMenuItem.Size = new System.Drawing.Size(333, 26);
            this.salidaATextoGrillaToolStripMenuItem.Text = "Salida a Texto/Grilla";
            this.salidaATextoGrillaToolStripMenuItem.Click += new System.EventHandler(this.salidaATextoGrillaToolStripMenuItem_Click);
            // 
            // findReplaceToolStripMenuItem
            // 
            this.findReplaceToolStripMenuItem.Name = "findReplaceToolStripMenuItem";
            this.findReplaceToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.findReplaceToolStripMenuItem.Size = new System.Drawing.Size(333, 26);
            this.findReplaceToolStripMenuItem.Text = "Find / Replace";
            this.findReplaceToolStripMenuItem.Click += new System.EventHandler(this.findReplaceToolStripMenuItem_Click);
            // 
            // toUpperToolStripMenuItem
            // 
            this.toUpperToolStripMenuItem.Name = "toUpperToolStripMenuItem";
            this.toUpperToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.U)));
            this.toUpperToolStripMenuItem.Size = new System.Drawing.Size(333, 26);
            this.toUpperToolStripMenuItem.Text = "ToUpper";
            this.toUpperToolStripMenuItem.Click += new System.EventHandler(this.toUpperToolStripMenuItem_Click);
            // 
            // toLowerToolStripMenuItem
            // 
            this.toLowerToolStripMenuItem.Name = "toLowerToolStripMenuItem";
            this.toLowerToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.U)));
            this.toLowerToolStripMenuItem.Size = new System.Drawing.Size(333, 26);
            this.toLowerToolStripMenuItem.Text = "ToLower";
            this.toLowerToolStripMenuItem.Click += new System.EventHandler(this.toLowerToolStripMenuItem_Click);
            // 
            // cerrarToolStripMenuItem
            // 
            this.cerrarToolStripMenuItem.Name = "cerrarToolStripMenuItem";
            this.cerrarToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.cerrarToolStripMenuItem.Size = new System.Drawing.Size(333, 26);
            this.cerrarToolStripMenuItem.Text = "Cerrar Archivo (Limpiar)";
            this.cerrarToolStripMenuItem.Click += new System.EventHandler(this.cerrarToolStripMenuItem_Click);
            // 
            // salirToolStripMenuItem
            // 
            this.salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            this.salirToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.salirToolStripMenuItem.Size = new System.Drawing.Size(333, 26);
            this.salirToolStripMenuItem.Text = "&Salir";
            this.salirToolStripMenuItem.Click += new System.EventHandler(this.salirToolStripMenuItem_Click);
            // 
            // ayudToolStripMenuItem
            // 
            this.ayudToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.acercaDeToolStripMenuItem});
            this.ayudToolStripMenuItem.Name = "ayudToolStripMenuItem";
            this.ayudToolStripMenuItem.Size = new System.Drawing.Size(65, 28);
            this.ayudToolStripMenuItem.Text = "Ayuda";
            // 
            // acercaDeToolStripMenuItem
            // 
            this.acercaDeToolStripMenuItem.Name = "acercaDeToolStripMenuItem";
            this.acercaDeToolStripMenuItem.Size = new System.Drawing.Size(167, 26);
            this.acercaDeToolStripMenuItem.Text = "Acerca de...";
            this.acercaDeToolStripMenuItem.Click += new System.EventHandler(this.acercaDeToolStripMenuItem_Click);
            // 
            // buscarToolStripMenuItem
            // 
            this.buscarToolStripMenuItem.Image = global::SQLCrypt.Properties.Resources.Binoculr_x16;
            this.buscarToolStripMenuItem.Name = "buscarToolStripMenuItem";
            this.buscarToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.buscarToolStripMenuItem.Size = new System.Drawing.Size(183, 28);
            this.buscarToolStripMenuItem.Text = "Buscar / Reemplazar";
            this.buscarToolStripMenuItem.ToolTipText = "F3";
            this.buscarToolStripMenuItem.Click += new System.EventHandler(this.buscarToolStripMenuItem_Click);
            // 
            // databasesToolStripMenuItem
            // 
            this.databasesToolStripMenuItem.AutoSize = false;
            this.databasesToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control;
            this.databasesToolStripMenuItem.Name = "databasesToolStripMenuItem";
            this.databasesToolStripMenuItem.Size = new System.Drawing.Size(232, 28);
            this.databasesToolStripMenuItem.SelectedIndexChanged += new System.EventHandler(this.databasesToolStripMenuItem_SelectedIndexChanged);
            // 
            // chkToText
            // 
            this.chkToText.AutoSize = true;
            this.chkToText.BackColor = System.Drawing.SystemColors.Control;
            this.chkToText.FlatAppearance.BorderSize = 2;
            this.chkToText.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.chkToText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkToText.ForeColor = System.Drawing.Color.Black;
            this.chkToText.Location = new System.Drawing.Point(1396, 9);
            this.chkToText.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkToText.Name = "chkToText";
            this.chkToText.Size = new System.Drawing.Size(100, 21);
            this.chkToText.TabIndex = 8;
            this.chkToText.Text = "Out to Text";
            this.chkToText.UseVisualStyleBackColor = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSplitButton1,
            this.tssLaFile,
            this.tssLaPath,
            this.tssLaPos,
            this.tssLaStat});
            this.statusStrip1.Location = new System.Drawing.Point(0, 738);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1531, 31);
            this.statusStrip1.TabIndex = 14;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripSplitButton1
            // 
            this.toolStripSplitButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripSplitButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSplitButton1.Image")));
            this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton1.Name = "toolStripSplitButton1";
            this.toolStripSplitButton1.Size = new System.Drawing.Size(43, 29);
            this.toolStripSplitButton1.Text = "toolStripSplitButton1";
            this.toolStripSplitButton1.ButtonClick += new System.EventHandler(this.toolStripSplitButton1_ButtonClick);
            // 
            // tssLaFile
            // 
            this.tssLaFile.AutoSize = false;
            this.tssLaFile.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tssLaFile.Name = "tssLaFile";
            this.tssLaFile.Size = new System.Drawing.Size(240, 25);
            this.tssLaFile.Text = "tssLaFile";
            // 
            // tssLaPath
            // 
            this.tssLaPath.AutoSize = false;
            this.tssLaPath.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tssLaPath.Name = "tssLaPath";
            this.tssLaPath.Size = new System.Drawing.Size(460, 25);
            this.tssLaPath.Text = "tssLaPath";
            // 
            // tssLaPos
            // 
            this.tssLaPos.AutoSize = false;
            this.tssLaPos.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tssLaPos.Name = "tssLaPos";
            this.tssLaPos.Size = new System.Drawing.Size(180, 25);
            this.tssLaPos.Text = "tssLaPos";
            // 
            // tssLaStat
            // 
            this.tssLaStat.AutoSize = false;
            this.tssLaStat.BackColor = System.Drawing.SystemColors.Control;
            this.tssLaStat.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tssLaStat.Name = "tssLaStat";
            this.tssLaStat.Size = new System.Drawing.Size(210, 25);
            this.tssLaStat.Text = "tssLaStat";
            // 
            // splitC
            // 
            this.splitC.BackColor = System.Drawing.SystemColors.Control;
            this.splitC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitC.Location = new System.Drawing.Point(0, 30);
            this.splitC.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.splitC.Name = "splitC";
            // 
            // splitC.Panel1
            // 
            this.splitC.Panel1.Controls.Add(this.laBuscarTablas);
            this.splitC.Panel1.Controls.Add(this.txBuscaEnLista);
            this.splitC.Panel1.Controls.Add(this.panObjetos);
            this.splitC.Panel1.Controls.Add(this.panColumnas);
            this.splitC.Panel1.Margin = new System.Windows.Forms.Padding(1);
            this.splitC.Panel1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.splitC.Panel1MinSize = 10;
            // 
            // splitC.Panel2
            // 
            this.splitC.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.splitC.Panel2.Controls.Add(this.txtSql);
            this.splitC.Panel2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.splitC.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.splitC.Size = new System.Drawing.Size(1531, 708);
            this.splitC.SplitterDistance = 310;
            this.splitC.SplitterWidth = 5;
            this.splitC.TabIndex = 15;
            // 
            // laBuscarTablas
            // 
            this.laBuscarTablas.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.laBuscarTablas.AutoSize = true;
            this.laBuscarTablas.BackColor = System.Drawing.SystemColors.Control;
            this.laBuscarTablas.Location = new System.Drawing.Point(11, 348);
            this.laBuscarTablas.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.laBuscarTablas.Name = "laBuscarTablas";
            this.laBuscarTablas.Size = new System.Drawing.Size(88, 16);
            this.laBuscarTablas.TabIndex = 28;
            this.laBuscarTablas.Text = "Buscar Tabla";
            // 
            // txBuscaEnLista
            // 
            this.txBuscaEnLista.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txBuscaEnLista.BackColor = System.Drawing.SystemColors.Control;
            this.txBuscaEnLista.ForeColor = System.Drawing.Color.Black;
            this.txBuscaEnLista.Location = new System.Drawing.Point(7, 367);
            this.txBuscaEnLista.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txBuscaEnLista.Name = "txBuscaEnLista";
            this.txBuscaEnLista.Size = new System.Drawing.Size(298, 22);
            this.txBuscaEnLista.TabIndex = 26;
            this.txBuscaEnLista.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txBuscaEnLista_KeyDown);
            // 
            // panObjetos
            // 
            this.panObjetos.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panObjetos.BackColor = System.Drawing.SystemColors.Control;
            this.panObjetos.Controls.Add(this.cbObjetos);
            this.panObjetos.Controls.Add(this.laTablas);
            this.panObjetos.Controls.Add(this.lstObjetos);
            this.panObjetos.Dock = System.Windows.Forms.DockStyle.Top;
            this.panObjetos.Location = new System.Drawing.Point(0, 0);
            this.panObjetos.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panObjetos.Name = "panObjetos";
            this.panObjetos.Size = new System.Drawing.Size(310, 396);
            this.panObjetos.TabIndex = 21;
            // 
            // cbObjetos
            // 
            this.cbObjetos.BackColor = System.Drawing.SystemColors.Control;
            this.cbObjetos.ForeColor = System.Drawing.Color.Black;
            this.cbObjetos.FormattingEnabled = true;
            this.cbObjetos.Location = new System.Drawing.Point(7, 311);
            this.cbObjetos.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbObjetos.Name = "cbObjetos";
            this.cbObjetos.Size = new System.Drawing.Size(235, 24);
            this.cbObjetos.TabIndex = 13;
            this.cbObjetos.SelectedValueChanged += new System.EventHandler(this.cbObjetos_SelectedValueChanged);
            // 
            // laTablas
            // 
            this.laTablas.AutoSize = true;
            this.laTablas.Location = new System.Drawing.Point(249, 319);
            this.laTablas.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.laTablas.Name = "laTablas";
            this.laTablas.Size = new System.Drawing.Size(61, 16);
            this.laTablas.TabIndex = 27;
            this.laTablas.Text = "laTablas";
            // 
            // lstObjetos
            // 
            this.lstObjetos.BackColor = System.Drawing.SystemColors.Control;
            this.lstObjetos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstObjetos.Dock = System.Windows.Forms.DockStyle.Top;
            this.lstObjetos.ForeColor = System.Drawing.Color.Black;
            this.lstObjetos.FormattingEnabled = true;
            this.lstObjetos.HorizontalScrollbar = true;
            this.lstObjetos.ItemHeight = 16;
            this.lstObjetos.Location = new System.Drawing.Point(0, 0);
            this.lstObjetos.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lstObjetos.Name = "lstObjetos";
            this.lstObjetos.ScrollAlwaysVisible = true;
            this.lstObjetos.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstObjetos.Size = new System.Drawing.Size(310, 306);
            this.lstObjetos.TabIndex = 12;
            this.lstObjetos.SelectedIndexChanged += new System.EventHandler(this.lstObjetos_SelectedIndexChanged);
            this.lstObjetos.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lstObjetos_MouseDown);
            // 
            // panColumnas
            // 
            this.panColumnas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panColumnas.BackColor = System.Drawing.SystemColors.Control;
            this.panColumnas.Controls.Add(this.lsColumnas);
            this.panColumnas.Location = new System.Drawing.Point(0, 401);
            this.panColumnas.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panColumnas.Name = "panColumnas";
            this.panColumnas.Size = new System.Drawing.Size(310, 303);
            this.panColumnas.TabIndex = 0;
            // 
            // lsColumnas
            // 
            this.lsColumnas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lsColumnas.BackColor = System.Drawing.SystemColors.Control;
            this.lsColumnas.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName,
            this.colType,
            this.colNullable});
            this.lsColumnas.ForeColor = System.Drawing.Color.Black;
            this.lsColumnas.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lsColumnas.HideSelection = false;
            this.lsColumnas.Location = new System.Drawing.Point(0, 0);
            this.lsColumnas.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lsColumnas.Name = "lsColumnas";
            this.lsColumnas.ShowGroups = false;
            this.lsColumnas.Size = new System.Drawing.Size(308, 302);
            this.lsColumnas.TabIndex = 33;
            this.lsColumnas.UseCompatibleStateImageBehavior = false;
            this.lsColumnas.View = System.Windows.Forms.View.Details;
            this.lsColumnas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lsColumnas_MouseDown);
            // 
            // colName
            // 
            this.colName.Text = "Columna";
            this.colName.Width = 150;
            // 
            // colType
            // 
            this.colType.Text = "Tipo";
            this.colType.Width = 100;
            // 
            // colNullable
            // 
            this.colNullable.Text = "Nullable";
            this.colNullable.Width = 80;
            // 
            // txtSql
            // 
            this.txtSql.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSql.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSql.Location = new System.Drawing.Point(4, 4);
            this.txtSql.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtSql.Name = "txtSql";
            this.txtSql.Size = new System.Drawing.Size(1208, 700);
            this.txtSql.TabIndex = 0;
            this.txtSql.UpdateUI += new System.EventHandler<ScintillaNET.UpdateUIEventArgs>(this.txtSql_SelectionChanged);
            this.txtSql.DragDrop += new System.Windows.Forms.DragEventHandler(this.txtSql_DragDrop);
            this.txtSql.DragEnter += new System.Windows.Forms.DragEventHandler(this.txtSql_DragEnter);
            // 
            // btIndentShow
            // 
            this.btIndentShow.Location = new System.Drawing.Point(1036, 2);
            this.btIndentShow.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btIndentShow.Name = "btIndentShow";
            this.btIndentShow.Size = new System.Drawing.Size(31, 32);
            this.btIndentShow.TabIndex = 16;
            this.btIndentShow.Text = "|";
            this.btIndentShow.UseVisualStyleBackColor = true;
            this.btIndentShow.Click += new System.EventHandler(this.btIndentShow_Click);
            // 
            // btSpacesShow
            // 
            this.btSpacesShow.Location = new System.Drawing.Point(1072, 2);
            this.btSpacesShow.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btSpacesShow.Name = "btSpacesShow";
            this.btSpacesShow.Size = new System.Drawing.Size(31, 32);
            this.btSpacesShow.TabIndex = 17;
            this.btSpacesShow.Text = "°";
            this.btSpacesShow.UseVisualStyleBackColor = true;
            this.btSpacesShow.Click += new System.EventHandler(this.btSpacesShow_Click);
            // 
            // btBuscarEnBd
            // 
            this.btBuscarEnBd.Location = new System.Drawing.Point(844, 2);
            this.btBuscarEnBd.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btBuscarEnBd.Name = "btBuscarEnBd";
            this.btBuscarEnBd.Size = new System.Drawing.Size(125, 31);
            this.btBuscarEnBd.TabIndex = 18;
            this.btBuscarEnBd.Text = "Buscar en BD";
            this.btBuscarEnBd.UseVisualStyleBackColor = true;
            this.btBuscarEnBd.Click += new System.EventHandler(this.btBuscarEnBd_Click);
            // 
            // btConnectToBd
            // 
            this.btConnectToBd.Image = global::SQLCrypt.Properties.Resources.Connect3;
            this.btConnectToBd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btConnectToBd.Location = new System.Drawing.Point(611, 2);
            this.btConnectToBd.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btConnectToBd.Name = "btConnectToBd";
            this.btConnectToBd.Size = new System.Drawing.Size(172, 31);
            this.btConnectToBd.TabIndex = 19;
            this.btConnectToBd.Text = "Connectar a BD";
            this.btConnectToBd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btConnectToBd.UseVisualStyleBackColor = true;
            this.btConnectToBd.Click += new System.EventHandler(this.btConnectToBd_Click);
            // 
            // txTextLimit
            // 
            this.txTextLimit.ini_Left = 0;
            this.txTextLimit.ini_Top = 0;
            this.txTextLimit.Location = new System.Drawing.Point(0, 0);
            this.txTextLimit.Name = "txTextLimit";
            this.txTextLimit.Size = new System.Drawing.Size(100, 22);
            this.txTextLimit.TabIndex = 0;
            // 
            // btCutTrailing
            // 
            this.btCutTrailing.Image = global::SQLCrypt.Properties.Resources.cut;
            this.btCutTrailing.Location = new System.Drawing.Point(1108, 2);
            this.btCutTrailing.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btCutTrailing.Name = "btCutTrailing";
            this.btCutTrailing.Size = new System.Drawing.Size(31, 32);
            this.btCutTrailing.TabIndex = 20;
            this.btCutTrailing.UseVisualStyleBackColor = true;
            this.btCutTrailing.Click += new System.EventHandler(this.btCutTrailing_Click);
            // 
            // FrmSqlCrypt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = global::SQLCrypt.Properties.Settings.Default.AppBackColor;
            this.ClientSize = new System.Drawing.Size(1531, 769);
            this.Controls.Add(this.btCutTrailing);
            this.Controls.Add(this.btConnectToBd);
            this.Controls.Add(this.btBuscarEnBd);
            this.Controls.Add(this.btSpacesShow);
            this.Controls.Add(this.btIndentShow);
            this.Controls.Add(this.splitC);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.chkToText);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MinimumSize = new System.Drawing.Size(1501, 762);
            this.Name = "FrmSqlCrypt";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sql Crypt";
            this.Load += new System.EventHandler(this.frmSqlCrypt_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitC.Panel1.ResumeLayout(false);
            this.splitC.Panel1.PerformLayout();
            this.splitC.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitC)).EndInit();
            this.splitC.ResumeLayout(false);
            this.panObjetos.ResumeLayout(false);
            this.panObjetos.PerformLayout();
            this.panColumnas.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

      }

      #endregion
      private System.Windows.Forms.MenuStrip menuStrip1;
      private System.Windows.Forms.ToolStripMenuItem archivoToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem grabarToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem salirToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem ayudToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem acercaDeToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem directorioDeTrabajoToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem cerrarToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem archivoDeConexiónToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem ejecutarComandoToolStripMenuItem;
      private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
      private System.Windows.Forms.ToolStripMenuItem grabarComoToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem abrirToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem crearArchivoDeConexiónToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem buscarToolStripMenuItem;
      private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
      private System.Windows.Forms.ToolStripMenuItem ConectTSM;
      private System.Windows.Forms.ToolStripComboBox databasesToolStripMenuItem;
      private System.Windows.Forms.CheckBox chkToText;
      private System.Windows.Forms.MyTextBox txTextLimit;
      private System.Windows.Forms.ToolStripMenuItem encriptarClavesToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem seleccionarTodoToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem comandosInmediatosToolStripMenuItem1;
      private System.Windows.Forms.StatusStrip statusStrip1;
      private System.Windows.Forms.ToolStripStatusLabel tssLaFile;
      private System.Windows.Forms.ToolStripStatusLabel tssLaPath;
      private System.Windows.Forms.ToolStripStatusLabel tssLaPos;
      private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton1;
      private System.Windows.Forms.ToolStripStatusLabel tssLaStat;
      private System.Windows.Forms.ToolStripMenuItem ejecutarTodasLasBasesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem IndexacionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem salidaATextoGrillaToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitC;
        private System.Windows.Forms.ToolStripMenuItem verPanelDeObjetosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem analizarDeadlocksToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem buscaPaginaSQLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ejecutarArchivosEnBatchToolStripMenuItem;
        private System.Windows.Forms.Panel panColumnas;
        private System.Windows.Forms.Label laBuscarTablas;
        private System.Windows.Forms.Label laTablas;
        private System.Windows.Forms.TextBox txBuscaEnLista;
        private System.Windows.Forms.Panel panObjetos;
        private System.Windows.Forms.ListBox lstObjetos;
        private System.Windows.Forms.ListView lsColumnas;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ColumnHeader colType;
        private System.Windows.Forms.ColumnHeader colNullable;
        private System.Windows.Forms.ComboBox cbObjetos;
        private System.Windows.Forms.ToolStripMenuItem extendedPropertiesToolStripMenuItem;
        private ScintillaNET.Scintilla txtSql;
        private System.Windows.Forms.ToolStripMenuItem findReplaceToolStripMenuItem;
        private System.Windows.Forms.Button btIndentShow;
        private System.Windows.Forms.ToolStripMenuItem toUpperToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toLowerToolStripMenuItem;
        private System.Windows.Forms.Button btSpacesShow;
        private System.Windows.Forms.ToolStripMenuItem buscarEnBDToolStripMenuItem;
        private System.Windows.Forms.Button btBuscarEnBd;
        private System.Windows.Forms.Button btConnectToBd;
        private System.Windows.Forms.Button btCutTrailing;
    }
}

