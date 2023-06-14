namespace SQLCrypt
{
   partial class frmSqlCrypt
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSqlCrypt));
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
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.crearArchivoDeConexiónToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.abrirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grabarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grabarComoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.salidaATextoGrillaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cerrarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ayudToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.acercaDeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buscarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.reemplazarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.replaceToolStripMenuItem = new System.Windows.Forms.ToolStripTextBox();
            this.buscarEnBaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.databasesToolStripMenuItem = new System.Windows.Forms.ToolStripComboBox();
            this.chkToText = new System.Windows.Forms.CheckBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
            this.tssLaFile = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssLaPath = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssLaPos = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssLaStat = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitC = new System.Windows.Forms.SplitContainer();
            this.btProcs = new System.Windows.Forms.Button();
            this.laBuscarTablas = new System.Windows.Forms.Label();
            this.laTablas = new System.Windows.Forms.Label();
            this.txBuscaEnLista = new System.Windows.Forms.TextBox();
            this.btRefresh = new System.Windows.Forms.Button();
            this.panObjetos = new System.Windows.Forms.Panel();
            this.lstObjetos = new System.Windows.Forms.ListBox();
            this.panColumnas = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lsColumnas = new System.Windows.Forms.ListBox();
            this.txtSql = new System.Windows.Forms.RichTextBox();
            this.txTextLimit = new System.Windows.Forms.MyTextBox();
            this.label3 = new System.Windows.Forms.MyLabel();
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
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archivoToolStripMenuItem,
            this.ayudToolStripMenuItem,
            this.buscarToolStripMenuItem,
            this.toolStripTextBox1,
            this.reemplazarToolStripMenuItem,
            this.replaceToolStripMenuItem,
            this.buscarEnBaseToolStripMenuItem,
            this.databasesToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1797, 37);
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
            this.toolStripSeparator1,
            this.crearArchivoDeConexiónToolStripMenuItem,
            this.abrirToolStripMenuItem,
            this.grabarToolStripMenuItem,
            this.grabarComoToolStripMenuItem,
            this.toolStripSeparator2,
            this.salidaATextoGrillaToolStripMenuItem,
            this.cerrarToolStripMenuItem,
            this.salirToolStripMenuItem});
            this.archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            this.archivoToolStripMenuItem.Size = new System.Drawing.Size(88, 33);
            this.archivoToolStripMenuItem.Text = "Archivo";
            // 
            // archivoDeConexiónToolStripMenuItem
            // 
            this.archivoDeConexiónToolStripMenuItem.Name = "archivoDeConexiónToolStripMenuItem";
            this.archivoDeConexiónToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.archivoDeConexiónToolStripMenuItem.Size = new System.Drawing.Size(399, 34);
            this.archivoDeConexiónToolStripMenuItem.Text = "Archivo de Conexión";
            this.archivoDeConexiónToolStripMenuItem.Click += new System.EventHandler(this.archivoDeConexiónToolStripMenuItem_Click);
            // 
            // ConectTSM
            // 
            this.ConectTSM.Name = "ConectTSM";
            this.ConectTSM.ShortcutKeys = System.Windows.Forms.Keys.F7;
            this.ConectTSM.Size = new System.Drawing.Size(399, 34);
            this.ConectTSM.Text = "Conectar";
            this.ConectTSM.Click += new System.EventHandler(this.ConectTSM_Click);
            // 
            // directorioDeTrabajoToolStripMenuItem
            // 
            this.directorioDeTrabajoToolStripMenuItem.Name = "directorioDeTrabajoToolStripMenuItem";
            this.directorioDeTrabajoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
            this.directorioDeTrabajoToolStripMenuItem.Size = new System.Drawing.Size(399, 34);
            this.directorioDeTrabajoToolStripMenuItem.Text = "Directorio de Trabajo";
            this.directorioDeTrabajoToolStripMenuItem.Click += new System.EventHandler(this.directorioDeTrabajoToolStripMenuItem_Click);
            // 
            // ejecutarComandoToolStripMenuItem
            // 
            this.ejecutarComandoToolStripMenuItem.Name = "ejecutarComandoToolStripMenuItem";
            this.ejecutarComandoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.ejecutarComandoToolStripMenuItem.Size = new System.Drawing.Size(399, 34);
            this.ejecutarComandoToolStripMenuItem.Text = "Ejecutar Comando/Selección";
            this.ejecutarComandoToolStripMenuItem.Click += new System.EventHandler(this.ejecutarComandoToolStripMenuItem_Click);
            // 
            // ejecutarTodasLasBasesToolStripMenuItem
            // 
            this.ejecutarTodasLasBasesToolStripMenuItem.Name = "ejecutarTodasLasBasesToolStripMenuItem";
            this.ejecutarTodasLasBasesToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
            this.ejecutarTodasLasBasesToolStripMenuItem.Size = new System.Drawing.Size(399, 34);
            this.ejecutarTodasLasBasesToolStripMenuItem.Text = "Ejecutar En Todas Las Bases";
            this.ejecutarTodasLasBasesToolStripMenuItem.Click += new System.EventHandler(this.ejecutarTodasLasBasesToolStripMenuItem_Click);
            // 
            // comandosInmediatosToolStripMenuItem1
            // 
            this.comandosInmediatosToolStripMenuItem1.Name = "comandosInmediatosToolStripMenuItem1";
            this.comandosInmediatosToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.comandosInmediatosToolStripMenuItem1.Size = new System.Drawing.Size(399, 34);
            this.comandosInmediatosToolStripMenuItem1.Text = "Comandos Inmediatos";
            this.comandosInmediatosToolStripMenuItem1.Click += new System.EventHandler(this.comandosInmediatosToolStripMenuItem1_Click);
            // 
            // encriptarClavesToolStripMenuItem
            // 
            this.encriptarClavesToolStripMenuItem.Name = "encriptarClavesToolStripMenuItem";
            this.encriptarClavesToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.encriptarClavesToolStripMenuItem.Size = new System.Drawing.Size(399, 34);
            this.encriptarClavesToolStripMenuItem.Text = "Encriptar Claves";
            this.encriptarClavesToolStripMenuItem.Click += new System.EventHandler(this.encriptarClavesToolStripMenuItem_Click);
            // 
            // seleccionarTodoToolStripMenuItem
            // 
            this.seleccionarTodoToolStripMenuItem.Name = "seleccionarTodoToolStripMenuItem";
            this.seleccionarTodoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.K)));
            this.seleccionarTodoToolStripMenuItem.Size = new System.Drawing.Size(399, 34);
            this.seleccionarTodoToolStripMenuItem.Text = "Seleccionar Todo";
            this.seleccionarTodoToolStripMenuItem.Click += new System.EventHandler(this.seleccionarTodoToolStripMenuItem_Click);
            // 
            // IndexacionToolStripMenuItem
            // 
            this.IndexacionToolStripMenuItem.Name = "IndexacionToolStripMenuItem";
            this.IndexacionToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.IndexacionToolStripMenuItem.Size = new System.Drawing.Size(399, 34);
            this.IndexacionToolStripMenuItem.Text = "Indexación";
            this.IndexacionToolStripMenuItem.Click += new System.EventHandler(this.IndexacionToolStripMenuItem_Click);
            // 
            // verPanelDeObjetosToolStripMenuItem
            // 
            this.verPanelDeObjetosToolStripMenuItem.Name = "verPanelDeObjetosToolStripMenuItem";
            this.verPanelDeObjetosToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.J)));
            this.verPanelDeObjetosToolStripMenuItem.Size = new System.Drawing.Size(399, 34);
            this.verPanelDeObjetosToolStripMenuItem.Text = "Ver Panel de Tablas";
            this.verPanelDeObjetosToolStripMenuItem.Click += new System.EventHandler(this.verPanelDeObjetosToolStripMenuItem_Click);
            // 
            // buscaPaginaSQLToolStripMenuItem
            // 
            this.buscaPaginaSQLToolStripMenuItem.Name = "buscaPaginaSQLToolStripMenuItem";
            this.buscaPaginaSQLToolStripMenuItem.Size = new System.Drawing.Size(399, 34);
            this.buscaPaginaSQLToolStripMenuItem.Text = "Busca Pagina SQL";
            this.buscaPaginaSQLToolStripMenuItem.Click += new System.EventHandler(this.buscaPaginaSQLToolStripMenuItem_Click);
            // 
            // analizarDeadlocksToolStripMenuItem
            // 
            this.analizarDeadlocksToolStripMenuItem.Name = "analizarDeadlocksToolStripMenuItem";
            this.analizarDeadlocksToolStripMenuItem.Size = new System.Drawing.Size(399, 34);
            this.analizarDeadlocksToolStripMenuItem.Text = "Analizar Deadlocks";
            this.analizarDeadlocksToolStripMenuItem.Click += new System.EventHandler(this.analizarDeadlocksToolStripMenuItem_Click);
            // 
            // ejecutarArchivosEnBatchToolStripMenuItem
            // 
            this.ejecutarArchivosEnBatchToolStripMenuItem.Name = "ejecutarArchivosEnBatchToolStripMenuItem";
            this.ejecutarArchivosEnBatchToolStripMenuItem.Size = new System.Drawing.Size(399, 34);
            this.ejecutarArchivosEnBatchToolStripMenuItem.Text = "Ejecutar Archivos en Batch";
            this.ejecutarArchivosEnBatchToolStripMenuItem.Click += new System.EventHandler(this.ejecutarArchivosEnBatchToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(396, 6);
            // 
            // crearArchivoDeConexiónToolStripMenuItem
            // 
            this.crearArchivoDeConexiónToolStripMenuItem.Name = "crearArchivoDeConexiónToolStripMenuItem";
            this.crearArchivoDeConexiónToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.crearArchivoDeConexiónToolStripMenuItem.Size = new System.Drawing.Size(399, 34);
            this.crearArchivoDeConexiónToolStripMenuItem.Text = "Crear Archivo de Conexión";
            this.crearArchivoDeConexiónToolStripMenuItem.Click += new System.EventHandler(this.crearArchivoDeConexiónToolStripMenuItem_Click);
            // 
            // abrirToolStripMenuItem
            // 
            this.abrirToolStripMenuItem.Name = "abrirToolStripMenuItem";
            this.abrirToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.abrirToolStripMenuItem.Size = new System.Drawing.Size(399, 34);
            this.abrirToolStripMenuItem.Text = "Abrir Archivo";
            this.abrirToolStripMenuItem.Click += new System.EventHandler(this.abrirToolStripMenuItem_Click);
            // 
            // grabarToolStripMenuItem
            // 
            this.grabarToolStripMenuItem.Name = "grabarToolStripMenuItem";
            this.grabarToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.grabarToolStripMenuItem.Size = new System.Drawing.Size(399, 34);
            this.grabarToolStripMenuItem.Text = "&Grabar";
            this.grabarToolStripMenuItem.Click += new System.EventHandler(this.grabarToolStripMenuItem_Click);
            // 
            // grabarComoToolStripMenuItem
            // 
            this.grabarComoToolStripMenuItem.Name = "grabarComoToolStripMenuItem";
            this.grabarComoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.grabarComoToolStripMenuItem.Size = new System.Drawing.Size(399, 34);
            this.grabarComoToolStripMenuItem.Text = "Grabar como...";
            this.grabarComoToolStripMenuItem.Click += new System.EventHandler(this.grabarComoToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(396, 6);
            // 
            // salidaATextoGrillaToolStripMenuItem
            // 
            this.salidaATextoGrillaToolStripMenuItem.Name = "salidaATextoGrillaToolStripMenuItem";
            this.salidaATextoGrillaToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.salidaATextoGrillaToolStripMenuItem.Size = new System.Drawing.Size(399, 34);
            this.salidaATextoGrillaToolStripMenuItem.Text = "Salida a Texto/Grilla";
            this.salidaATextoGrillaToolStripMenuItem.Click += new System.EventHandler(this.salidaATextoGrillaToolStripMenuItem_Click);
            // 
            // cerrarToolStripMenuItem
            // 
            this.cerrarToolStripMenuItem.Name = "cerrarToolStripMenuItem";
            this.cerrarToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.cerrarToolStripMenuItem.Size = new System.Drawing.Size(399, 34);
            this.cerrarToolStripMenuItem.Text = "Cerrar Archivo (Limpiar)";
            this.cerrarToolStripMenuItem.Click += new System.EventHandler(this.cerrarToolStripMenuItem_Click);
            // 
            // salirToolStripMenuItem
            // 
            this.salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            this.salirToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.salirToolStripMenuItem.Size = new System.Drawing.Size(399, 34);
            this.salirToolStripMenuItem.Text = "&Salir";
            this.salirToolStripMenuItem.Click += new System.EventHandler(this.salirToolStripMenuItem_Click);
            // 
            // ayudToolStripMenuItem
            // 
            this.ayudToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.acercaDeToolStripMenuItem});
            this.ayudToolStripMenuItem.Name = "ayudToolStripMenuItem";
            this.ayudToolStripMenuItem.Size = new System.Drawing.Size(79, 33);
            this.ayudToolStripMenuItem.Text = "Ayuda";
            // 
            // acercaDeToolStripMenuItem
            // 
            this.acercaDeToolStripMenuItem.Name = "acercaDeToolStripMenuItem";
            this.acercaDeToolStripMenuItem.Size = new System.Drawing.Size(203, 34);
            this.acercaDeToolStripMenuItem.Text = "Acerca de...";
            this.acercaDeToolStripMenuItem.Click += new System.EventHandler(this.acercaDeToolStripMenuItem_Click);
            // 
            // buscarToolStripMenuItem
            // 
            this.buscarToolStripMenuItem.Image = global::SQLCrypt.Properties.Resources.Binoculr_x16;
            this.buscarToolStripMenuItem.Name = "buscarToolStripMenuItem";
            this.buscarToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.buscarToolStripMenuItem.Size = new System.Drawing.Size(103, 33);
            this.buscarToolStripMenuItem.Text = "Buscar";
            this.buscarToolStripMenuItem.ToolTipText = "F3";
            this.buscarToolStripMenuItem.Click += new System.EventHandler(this.buscarToolStripMenuItem_Click);
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.Size = new System.Drawing.Size(178, 33);
            this.toolStripTextBox1.ToolTipText = "Cadena a Buscar";
            // 
            // reemplazarToolStripMenuItem
            // 
            this.reemplazarToolStripMenuItem.Image = global::SQLCrypt.Properties.Resources._0RecycleRed;
            this.reemplazarToolStripMenuItem.Name = "reemplazarToolStripMenuItem";
            this.reemplazarToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F3)));
            this.reemplazarToolStripMenuItem.Size = new System.Drawing.Size(143, 33);
            this.reemplazarToolStripMenuItem.Text = "Reemplazar";
            this.reemplazarToolStripMenuItem.ToolTipText = "Ctrl + F3";
            this.reemplazarToolStripMenuItem.Click += new System.EventHandler(this.reemplazarToolStripMenuItem_Click);
            // 
            // replaceToolStripMenuItem
            // 
            this.replaceToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.replaceToolStripMenuItem.Name = "replaceToolStripMenuItem";
            this.replaceToolStripMenuItem.Size = new System.Drawing.Size(178, 33);
            this.replaceToolStripMenuItem.ToolTipText = "Cadena de Reemplazo";
            // 
            // buscarEnBaseToolStripMenuItem
            // 
            this.buscarEnBaseToolStripMenuItem.Name = "buscarEnBaseToolStripMenuItem";
            this.buscarEnBaseToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+B";
            this.buscarEnBaseToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B)));
            this.buscarEnBaseToolStripMenuItem.Size = new System.Drawing.Size(198, 33);
            this.buscarEnBaseToolStripMenuItem.Text = "Buscar en Base Ctrl+B";
            this.buscarEnBaseToolStripMenuItem.Click += new System.EventHandler(this.buscarEnBaseToolStripMenuItem_Click);
            // 
            // databasesToolStripMenuItem
            // 
            this.databasesToolStripMenuItem.AutoSize = false;
            this.databasesToolStripMenuItem.Name = "databasesToolStripMenuItem";
            this.databasesToolStripMenuItem.Size = new System.Drawing.Size(260, 33);
            this.databasesToolStripMenuItem.Text = "Databases";
            this.databasesToolStripMenuItem.SelectedIndexChanged += new System.EventHandler(this.databasesToolStripMenuItem_SelectedIndexChanged);
            // 
            // chkToText
            // 
            this.chkToText.AutoSize = true;
            this.chkToText.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.chkToText.FlatAppearance.BorderSize = 2;
            this.chkToText.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.chkToText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkToText.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.chkToText.Location = new System.Drawing.Point(1570, 7);
            this.chkToText.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkToText.Name = "chkToText";
            this.chkToText.Size = new System.Drawing.Size(129, 24);
            this.chkToText.TabIndex = 8;
            this.chkToText.Text = "Out to Text";
            this.chkToText.UseVisualStyleBackColor = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSplitButton1,
            this.tssLaFile,
            this.tssLaPath,
            this.tssLaPos,
            this.tssLaStat});
            this.statusStrip1.Location = new System.Drawing.Point(0, 913);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(2, 0, 21, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1797, 31);
            this.statusStrip1.TabIndex = 14;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripSplitButton1
            // 
            this.toolStripSplitButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripSplitButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSplitButton1.Image")));
            this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton1.Name = "toolStripSplitButton1";
            this.toolStripSplitButton1.Size = new System.Drawing.Size(45, 28);
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
            this.tssLaFile.Size = new System.Drawing.Size(240, 24);
            this.tssLaFile.Text = "tssLaFile";
            // 
            // tssLaPath
            // 
            this.tssLaPath.AutoSize = false;
            this.tssLaPath.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tssLaPath.Name = "tssLaPath";
            this.tssLaPath.Size = new System.Drawing.Size(460, 24);
            this.tssLaPath.Text = "tssLaPath";
            // 
            // tssLaPos
            // 
            this.tssLaPos.AutoSize = false;
            this.tssLaPos.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tssLaPos.Name = "tssLaPos";
            this.tssLaPos.Size = new System.Drawing.Size(180, 24);
            this.tssLaPos.Text = "tssLaPos";
            // 
            // tssLaStat
            // 
            this.tssLaStat.AutoSize = false;
            this.tssLaStat.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tssLaStat.Name = "tssLaStat";
            this.tssLaStat.Size = new System.Drawing.Size(210, 24);
            this.tssLaStat.Text = "tssLaStat";
            // 
            // splitC
            // 
            this.splitC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitC.Location = new System.Drawing.Point(0, 37);
            this.splitC.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.splitC.Name = "splitC";
            // 
            // splitC.Panel1
            // 
            this.splitC.Panel1.Controls.Add(this.btProcs);
            this.splitC.Panel1.Controls.Add(this.laBuscarTablas);
            this.splitC.Panel1.Controls.Add(this.laTablas);
            this.splitC.Panel1.Controls.Add(this.txBuscaEnLista);
            this.splitC.Panel1.Controls.Add(this.btRefresh);
            this.splitC.Panel1.Controls.Add(this.panObjetos);
            this.splitC.Panel1.Controls.Add(this.panColumnas);
            this.splitC.Panel1.Margin = new System.Windows.Forms.Padding(1);
            this.splitC.Panel1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.splitC.Panel1MinSize = 10;
            // 
            // splitC.Panel2
            // 
            this.splitC.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.splitC.Panel2.Controls.Add(this.txtSql);
            this.splitC.Panel2.Padding = new System.Windows.Forms.Padding(4);
            this.splitC.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.splitC.Size = new System.Drawing.Size(1797, 876);
            this.splitC.SplitterDistance = 318;
            this.splitC.SplitterWidth = 6;
            this.splitC.TabIndex = 15;
            // 
            // btProcs
            // 
            this.btProcs.Location = new System.Drawing.Point(74, 393);
            this.btProcs.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btProcs.Name = "btProcs";
            this.btProcs.Size = new System.Drawing.Size(68, 35);
            this.btProcs.TabIndex = 29;
            this.btProcs.Text = "Procs.";
            this.btProcs.UseVisualStyleBackColor = true;
            this.btProcs.Click += new System.EventHandler(this.btProcs_Click);
            // 
            // laBuscarTablas
            // 
            this.laBuscarTablas.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.laBuscarTablas.AutoSize = true;
            this.laBuscarTablas.Location = new System.Drawing.Point(12, 436);
            this.laBuscarTablas.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.laBuscarTablas.Name = "laBuscarTablas";
            this.laBuscarTablas.Size = new System.Drawing.Size(102, 20);
            this.laBuscarTablas.TabIndex = 28;
            this.laBuscarTablas.Text = "Buscar Tabla";
            // 
            // laTablas
            // 
            this.laTablas.AutoSize = true;
            this.laTablas.Location = new System.Drawing.Point(146, 402);
            this.laTablas.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.laTablas.Name = "laTablas";
            this.laTablas.Size = new System.Drawing.Size(51, 20);
            this.laTablas.TabIndex = 27;
            this.laTablas.Text = "label1";
            // 
            // txBuscaEnLista
            // 
            this.txBuscaEnLista.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txBuscaEnLista.Location = new System.Drawing.Point(8, 459);
            this.txBuscaEnLista.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txBuscaEnLista.Name = "txBuscaEnLista";
            this.txBuscaEnLista.Size = new System.Drawing.Size(306, 26);
            this.txBuscaEnLista.TabIndex = 26;
            this.txBuscaEnLista.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txBuscaEnLista_KeyDown);
            // 
            // btRefresh
            // 
            this.btRefresh.Location = new System.Drawing.Point(5, 392);
            this.btRefresh.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btRefresh.Name = "btRefresh";
            this.btRefresh.Size = new System.Drawing.Size(70, 35);
            this.btRefresh.TabIndex = 25;
            this.btRefresh.Text = "Tablas";
            this.btRefresh.UseVisualStyleBackColor = true;
            this.btRefresh.Click += new System.EventHandler(this.btRefresh_Click);
            // 
            // panObjetos
            // 
            this.panObjetos.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panObjetos.Controls.Add(this.lstObjetos);
            this.panObjetos.Dock = System.Windows.Forms.DockStyle.Top;
            this.panObjetos.Location = new System.Drawing.Point(0, 0);
            this.panObjetos.Name = "panObjetos";
            this.panObjetos.Size = new System.Drawing.Size(318, 495);
            this.panObjetos.TabIndex = 21;
            // 
            // lstObjetos
            // 
            this.lstObjetos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstObjetos.Dock = System.Windows.Forms.DockStyle.Top;
            this.lstObjetos.FormattingEnabled = true;
            this.lstObjetos.HorizontalScrollbar = true;
            this.lstObjetos.ItemHeight = 20;
            this.lstObjetos.Location = new System.Drawing.Point(0, 0);
            this.lstObjetos.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lstObjetos.Name = "lstObjetos";
            this.lstObjetos.ScrollAlwaysVisible = true;
            this.lstObjetos.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstObjetos.Size = new System.Drawing.Size(318, 382);
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
            this.panColumnas.Controls.Add(this.label1);
            this.panColumnas.Controls.Add(this.lsColumnas);
            this.panColumnas.Location = new System.Drawing.Point(0, 501);
            this.panColumnas.Name = "panColumnas";
            this.panColumnas.Size = new System.Drawing.Size(318, 370);
            this.panColumnas.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 20);
            this.label1.TabIndex = 32;
            this.label1.Text = "Columnas";
            // 
            // lsColumnas
            // 
            this.lsColumnas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lsColumnas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lsColumnas.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lsColumnas.FormattingEnabled = true;
            this.lsColumnas.HorizontalScrollbar = true;
            this.lsColumnas.IntegralHeight = false;
            this.lsColumnas.ItemHeight = 20;
            this.lsColumnas.Location = new System.Drawing.Point(0, 31);
            this.lsColumnas.Margin = new System.Windows.Forms.Padding(4);
            this.lsColumnas.Name = "lsColumnas";
            this.lsColumnas.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lsColumnas.Size = new System.Drawing.Size(318, 339);
            this.lsColumnas.TabIndex = 20;
            this.lsColumnas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lsColumnas_MouseDown);
            // 
            // txtSql
            // 
            this.txtSql.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSql.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSql.Location = new System.Drawing.Point(4, 4);
            this.txtSql.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtSql.Name = "txtSql";
            this.txtSql.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtSql.Size = new System.Drawing.Size(1465, 868);
            this.txtSql.TabIndex = 2;
            this.txtSql.Text = "";
            this.txtSql.WordWrap = false;
            this.txtSql.SelectionChanged += new System.EventHandler(this.txtSql_SelectionChanged);
            this.txtSql.TextChanged += new System.EventHandler(this.txtSql_TextChanged);
            this.txtSql.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSql_KeyDown);
            // 
            // txTextLimit
            // 
            this.txTextLimit.ini_Left = 0;
            this.txTextLimit.ini_Top = 0;
            this.txTextLimit.Location = new System.Drawing.Point(1371, 6);
            this.txTextLimit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txTextLimit.Name = "txTextLimit";
            this.txTextLimit.Size = new System.Drawing.Size(148, 26);
            this.txTextLimit.TabIndex = 10;
            this.txTextLimit.Text = "0";
            this.txTextLimit.Leave += new System.EventHandler(this.txTextLimit_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ini_Left = 0;
            this.label3.ini_Top = 0;
            this.label3.Location = new System.Drawing.Point(1282, 12);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 20);
            this.label3.TabIndex = 9;
            this.label3.Text = "Text Limit";
            // 
            // frmSqlCrypt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1797, 944);
            this.Controls.Add(this.splitC);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.txTextLimit);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.chkToText);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1790, 1000);
            this.Name = "frmSqlCrypt";
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
            this.panColumnas.ResumeLayout(false);
            this.panColumnas.PerformLayout();
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
      private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
      private System.Windows.Forms.ToolStripMenuItem buscarToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem reemplazarToolStripMenuItem;
      private System.Windows.Forms.ToolStripTextBox replaceToolStripMenuItem;
      private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
      private System.Windows.Forms.ToolStripMenuItem ConectTSM;
      private System.Windows.Forms.ToolStripMenuItem buscarEnBaseToolStripMenuItem;
      private System.Windows.Forms.ToolStripComboBox databasesToolStripMenuItem;
      private System.Windows.Forms.CheckBox chkToText;
      private System.Windows.Forms.MyLabel label3;
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
        public System.Windows.Forms.RichTextBox txtSql;
        private System.Windows.Forms.Panel panColumnas;
        private System.Windows.Forms.ListBox lsColumnas;
        private System.Windows.Forms.Button btProcs;
        private System.Windows.Forms.Label laBuscarTablas;
        private System.Windows.Forms.Label laTablas;
        private System.Windows.Forms.TextBox txBuscaEnLista;
        private System.Windows.Forms.Button btRefresh;
        private System.Windows.Forms.Panel panObjetos;
        private System.Windows.Forms.ListBox lstObjetos;
        private System.Windows.Forms.Label label1;
    }
}

