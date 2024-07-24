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
            this.ejecutarComandoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ejecutarTodasLasBasesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.comandosInmediatosToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.encriptarClavesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.verPanelDeObjetosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buscaPaginaSQLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.analizarDeadlocksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ejecutarArchivosEnBatchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.extendedPropertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buscarEnBDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.indicesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.abrirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grabarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grabarComoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.salidaATextoGrillaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cerrarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findReplaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findNextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findPreviousToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.mostrarEspaciosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.guiaIndentacionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.numerosDeLíneaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.selecciónAMayúsculasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selecciónAMinúsculasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.commentSelectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uncommentSelectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.eliminarEspaciosFinDeLíneaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tABAEspaciosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoCompleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabSizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.baseDeDatoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.databasesToolStripMenuItem = new System.Windows.Forms.ToolStripComboBox();
            this.ayudToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.acercaDeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.btRefreshType = new System.Windows.Forms.Button();
            this.cbObjetos = new System.Windows.Forms.ComboBox();
            this.laTablas = new System.Windows.Forms.Label();
            this.lstObjetos = new System.Windows.Forms.ListBox();
            this.panColumnas = new System.Windows.Forms.Panel();
            this.lsColumnas = new System.Windows.Forms.ListView();
            this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colNullable = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.txtSql = new ScintillaNET.Scintilla();
            this.btBuscarEnBd = new System.Windows.Forms.Button();
            this.btConnectToBd = new System.Windows.Forms.Button();
            this.btReconnect = new System.Windows.Forms.Button();
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
            this.editToolStripMenuItem,
            this.tabSizeToolStripMenuItem,
            this.toolStripTextBox1,
            this.baseDeDatoToolStripMenuItem,
            this.databasesToolStripMenuItem,
            this.ayudToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 1, 0, 1);
            this.menuStrip1.Size = new System.Drawing.Size(1163, 25);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // archivoToolStripMenuItem
            // 
            this.archivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ejecutarComandoToolStripMenuItem,
            this.ejecutarTodasLasBasesToolStripMenuItem,
            this.comandosInmediatosToolStripMenuItem1,
            this.encriptarClavesToolStripMenuItem,
            this.verPanelDeObjetosToolStripMenuItem,
            this.buscaPaginaSQLToolStripMenuItem,
            this.analizarDeadlocksToolStripMenuItem,
            this.ejecutarArchivosEnBatchToolStripMenuItem,
            this.extendedPropertiesToolStripMenuItem,
            this.buscarEnBDToolStripMenuItem,
            this.indicesToolStripMenuItem,
            this.toolStripSeparator1,
            this.abrirToolStripMenuItem,
            this.grabarToolStripMenuItem,
            this.grabarComoToolStripMenuItem,
            this.toolStripSeparator2,
            this.salidaATextoGrillaToolStripMenuItem,
            this.cerrarToolStripMenuItem,
            this.salirToolStripMenuItem});
            this.archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            this.archivoToolStripMenuItem.Size = new System.Drawing.Size(60, 23);
            this.archivoToolStripMenuItem.Text = "&Archivo";
            // 
            // ejecutarComandoToolStripMenuItem
            // 
            this.ejecutarComandoToolStripMenuItem.Name = "ejecutarComandoToolStripMenuItem";
            this.ejecutarComandoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.ejecutarComandoToolStripMenuItem.Size = new System.Drawing.Size(267, 22);
            this.ejecutarComandoToolStripMenuItem.Text = "Ejecutar Comando/Selección";
            this.ejecutarComandoToolStripMenuItem.Click += new System.EventHandler(this.ejecutarComandoToolStripMenuItem_Click);
            // 
            // ejecutarTodasLasBasesToolStripMenuItem
            // 
            this.ejecutarTodasLasBasesToolStripMenuItem.Name = "ejecutarTodasLasBasesToolStripMenuItem";
            this.ejecutarTodasLasBasesToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
            this.ejecutarTodasLasBasesToolStripMenuItem.Size = new System.Drawing.Size(267, 22);
            this.ejecutarTodasLasBasesToolStripMenuItem.Text = "Ejecutar En Todas Las Bases";
            this.ejecutarTodasLasBasesToolStripMenuItem.Click += new System.EventHandler(this.ejecutarTodasLasBasesToolStripMenuItem_Click);
            // 
            // comandosInmediatosToolStripMenuItem1
            // 
            this.comandosInmediatosToolStripMenuItem1.Name = "comandosInmediatosToolStripMenuItem1";
            this.comandosInmediatosToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.comandosInmediatosToolStripMenuItem1.Size = new System.Drawing.Size(267, 22);
            this.comandosInmediatosToolStripMenuItem1.Text = "Comandos Inmediatos";
            this.comandosInmediatosToolStripMenuItem1.Click += new System.EventHandler(this.comandosInmediatosToolStripMenuItem1_Click);
            // 
            // encriptarClavesToolStripMenuItem
            // 
            this.encriptarClavesToolStripMenuItem.Name = "encriptarClavesToolStripMenuItem";
            this.encriptarClavesToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.encriptarClavesToolStripMenuItem.Size = new System.Drawing.Size(267, 22);
            this.encriptarClavesToolStripMenuItem.Text = "Encriptar Claves";
            this.encriptarClavesToolStripMenuItem.Click += new System.EventHandler(this.encriptarClavesToolStripMenuItem_Click);
            // 
            // verPanelDeObjetosToolStripMenuItem
            // 
            this.verPanelDeObjetosToolStripMenuItem.Name = "verPanelDeObjetosToolStripMenuItem";
            this.verPanelDeObjetosToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.J)));
            this.verPanelDeObjetosToolStripMenuItem.Size = new System.Drawing.Size(267, 22);
            this.verPanelDeObjetosToolStripMenuItem.Text = "Ver Panel de Tablas";
            this.verPanelDeObjetosToolStripMenuItem.Click += new System.EventHandler(this.verPanelDeObjetosToolStripMenuItem_Click);
            // 
            // buscaPaginaSQLToolStripMenuItem
            // 
            this.buscaPaginaSQLToolStripMenuItem.Name = "buscaPaginaSQLToolStripMenuItem";
            this.buscaPaginaSQLToolStripMenuItem.Size = new System.Drawing.Size(267, 22);
            this.buscaPaginaSQLToolStripMenuItem.Text = "Busca Pagina SQL";
            this.buscaPaginaSQLToolStripMenuItem.Click += new System.EventHandler(this.buscaPaginaSQLToolStripMenuItem_Click);
            // 
            // analizarDeadlocksToolStripMenuItem
            // 
            this.analizarDeadlocksToolStripMenuItem.Name = "analizarDeadlocksToolStripMenuItem";
            this.analizarDeadlocksToolStripMenuItem.Size = new System.Drawing.Size(267, 22);
            this.analizarDeadlocksToolStripMenuItem.Text = "Analizar Deadlocks";
            this.analizarDeadlocksToolStripMenuItem.Click += new System.EventHandler(this.analizarDeadlocksToolStripMenuItem_Click);
            // 
            // ejecutarArchivosEnBatchToolStripMenuItem
            // 
            this.ejecutarArchivosEnBatchToolStripMenuItem.Name = "ejecutarArchivosEnBatchToolStripMenuItem";
            this.ejecutarArchivosEnBatchToolStripMenuItem.Size = new System.Drawing.Size(267, 22);
            this.ejecutarArchivosEnBatchToolStripMenuItem.Text = "Ejecutar Archivos en Batch";
            this.ejecutarArchivosEnBatchToolStripMenuItem.Click += new System.EventHandler(this.ejecutarArchivosEnBatchToolStripMenuItem_Click);
            // 
            // extendedPropertiesToolStripMenuItem
            // 
            this.extendedPropertiesToolStripMenuItem.Name = "extendedPropertiesToolStripMenuItem";
            this.extendedPropertiesToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.extendedPropertiesToolStripMenuItem.Size = new System.Drawing.Size(267, 22);
            this.extendedPropertiesToolStripMenuItem.Text = "Extended Properties";
            this.extendedPropertiesToolStripMenuItem.Click += new System.EventHandler(this.extendedPropertiesToolStripMenuItem_Click);
            // 
            // buscarEnBDToolStripMenuItem
            // 
            this.buscarEnBDToolStripMenuItem.Name = "buscarEnBDToolStripMenuItem";
            this.buscarEnBDToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B)));
            this.buscarEnBDToolStripMenuItem.Size = new System.Drawing.Size(267, 22);
            this.buscarEnBDToolStripMenuItem.Text = "Buscar en BD";
            this.buscarEnBDToolStripMenuItem.Click += new System.EventHandler(this.buscarEnBDToolStripMenuItem_Click);
            // 
            // indicesToolStripMenuItem
            // 
            this.indicesToolStripMenuItem.Name = "indicesToolStripMenuItem";
            this.indicesToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.indicesToolStripMenuItem.Size = new System.Drawing.Size(267, 22);
            this.indicesToolStripMenuItem.Text = "Indices";
            this.indicesToolStripMenuItem.Click += new System.EventHandler(this.indicesToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(264, 6);
            // 
            // abrirToolStripMenuItem
            // 
            this.abrirToolStripMenuItem.Name = "abrirToolStripMenuItem";
            this.abrirToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.abrirToolStripMenuItem.Size = new System.Drawing.Size(267, 22);
            this.abrirToolStripMenuItem.Text = "Abrir Archivo";
            this.abrirToolStripMenuItem.Click += new System.EventHandler(this.abrirToolStripMenuItem_Click);
            // 
            // grabarToolStripMenuItem
            // 
            this.grabarToolStripMenuItem.Name = "grabarToolStripMenuItem";
            this.grabarToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.grabarToolStripMenuItem.Size = new System.Drawing.Size(267, 22);
            this.grabarToolStripMenuItem.Text = "&Grabar";
            this.grabarToolStripMenuItem.Click += new System.EventHandler(this.grabarToolStripMenuItem_Click);
            // 
            // grabarComoToolStripMenuItem
            // 
            this.grabarComoToolStripMenuItem.Name = "grabarComoToolStripMenuItem";
            this.grabarComoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.grabarComoToolStripMenuItem.Size = new System.Drawing.Size(267, 22);
            this.grabarComoToolStripMenuItem.Text = "Grabar como...";
            this.grabarComoToolStripMenuItem.Click += new System.EventHandler(this.grabarComoToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(264, 6);
            // 
            // salidaATextoGrillaToolStripMenuItem
            // 
            this.salidaATextoGrillaToolStripMenuItem.Name = "salidaATextoGrillaToolStripMenuItem";
            this.salidaATextoGrillaToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.salidaATextoGrillaToolStripMenuItem.Size = new System.Drawing.Size(267, 22);
            this.salidaATextoGrillaToolStripMenuItem.Text = "Salida a Texto/Grilla";
            this.salidaATextoGrillaToolStripMenuItem.Click += new System.EventHandler(this.salidaATextoGrillaToolStripMenuItem_Click);
            // 
            // cerrarToolStripMenuItem
            // 
            this.cerrarToolStripMenuItem.Name = "cerrarToolStripMenuItem";
            this.cerrarToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.cerrarToolStripMenuItem.Size = new System.Drawing.Size(267, 22);
            this.cerrarToolStripMenuItem.Text = "Cerrar Archivo (Limpiar)";
            this.cerrarToolStripMenuItem.Click += new System.EventHandler(this.cerrarToolStripMenuItem_Click);
            // 
            // salirToolStripMenuItem
            // 
            this.salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            this.salirToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.salirToolStripMenuItem.Size = new System.Drawing.Size(267, 22);
            this.salirToolStripMenuItem.Text = "&Salir";
            this.salirToolStripMenuItem.Click += new System.EventHandler(this.salirToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.findReplaceToolStripMenuItem,
            this.findNextToolStripMenuItem,
            this.findPreviousToolStripMenuItem,
            this.toolStripSeparator3,
            this.mostrarEspaciosToolStripMenuItem,
            this.guiaIndentacionToolStripMenuItem,
            this.numerosDeLíneaToolStripMenuItem,
            this.toolStripSeparator4,
            this.selecciónAMayúsculasToolStripMenuItem,
            this.selecciónAMinúsculasToolStripMenuItem,
            this.commentSelectionToolStripMenuItem,
            this.uncommentSelectionToolStripMenuItem,
            this.toolStripSeparator5,
            this.eliminarEspaciosFinDeLíneaToolStripMenuItem,
            this.tABAEspaciosToolStripMenuItem,
            this.autoCompleteToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 23);
            this.editToolStripMenuItem.Text = "&Edit";
            // 
            // findReplaceToolStripMenuItem
            // 
            this.findReplaceToolStripMenuItem.Name = "findReplaceToolStripMenuItem";
            this.findReplaceToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.findReplaceToolStripMenuItem.Size = new System.Drawing.Size(270, 22);
            this.findReplaceToolStripMenuItem.Text = "Find/Replace";
            this.findReplaceToolStripMenuItem.Click += new System.EventHandler(this.findReplaceToolStripMenuItem_Click);
            // 
            // findNextToolStripMenuItem
            // 
            this.findNextToolStripMenuItem.Name = "findNextToolStripMenuItem";
            this.findNextToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.findNextToolStripMenuItem.Size = new System.Drawing.Size(270, 22);
            this.findNextToolStripMenuItem.Text = "Find Next";
            this.findNextToolStripMenuItem.Click += new System.EventHandler(this.findNextToolStripMenuItem_Click);
            // 
            // findPreviousToolStripMenuItem
            // 
            this.findPreviousToolStripMenuItem.Name = "findPreviousToolStripMenuItem";
            this.findPreviousToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.F3)));
            this.findPreviousToolStripMenuItem.Size = new System.Drawing.Size(270, 22);
            this.findPreviousToolStripMenuItem.Text = "Find Previous";
            this.findPreviousToolStripMenuItem.Click += new System.EventHandler(this.findPreviousToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(267, 6);
            // 
            // mostrarEspaciosToolStripMenuItem
            // 
            this.mostrarEspaciosToolStripMenuItem.Name = "mostrarEspaciosToolStripMenuItem";
            this.mostrarEspaciosToolStripMenuItem.Size = new System.Drawing.Size(270, 22);
            this.mostrarEspaciosToolStripMenuItem.Text = "Mostrar Espacios";
            this.mostrarEspaciosToolStripMenuItem.Click += new System.EventHandler(this.mostrarEspaciosToolStripMenuItem_Click);
            // 
            // guiaIndentacionToolStripMenuItem
            // 
            this.guiaIndentacionToolStripMenuItem.Name = "guiaIndentacionToolStripMenuItem";
            this.guiaIndentacionToolStripMenuItem.Size = new System.Drawing.Size(270, 22);
            this.guiaIndentacionToolStripMenuItem.Text = "Guia Indentacion";
            this.guiaIndentacionToolStripMenuItem.Click += new System.EventHandler(this.guiaIndentacionToolStripMenuItem_Click);
            // 
            // numerosDeLíneaToolStripMenuItem
            // 
            this.numerosDeLíneaToolStripMenuItem.Name = "numerosDeLíneaToolStripMenuItem";
            this.numerosDeLíneaToolStripMenuItem.Size = new System.Drawing.Size(270, 22);
            this.numerosDeLíneaToolStripMenuItem.Text = "Numeros de Línea";
            this.numerosDeLíneaToolStripMenuItem.Click += new System.EventHandler(this.numerosDeLíneaToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(267, 6);
            // 
            // selecciónAMayúsculasToolStripMenuItem
            // 
            this.selecciónAMayúsculasToolStripMenuItem.Name = "selecciónAMayúsculasToolStripMenuItem";
            this.selecciónAMayúsculasToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.U)));
            this.selecciónAMayúsculasToolStripMenuItem.Size = new System.Drawing.Size(270, 22);
            this.selecciónAMayúsculasToolStripMenuItem.Text = "Selección a Mayúsculas";
            this.selecciónAMayúsculasToolStripMenuItem.Click += new System.EventHandler(this.selecciónAMayúsculasToolStripMenuItem_Click);
            // 
            // selecciónAMinúsculasToolStripMenuItem
            // 
            this.selecciónAMinúsculasToolStripMenuItem.Name = "selecciónAMinúsculasToolStripMenuItem";
            this.selecciónAMinúsculasToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.U)));
            this.selecciónAMinúsculasToolStripMenuItem.Size = new System.Drawing.Size(270, 22);
            this.selecciónAMinúsculasToolStripMenuItem.Text = "Selección a Minúsculas";
            this.selecciónAMinúsculasToolStripMenuItem.Click += new System.EventHandler(this.selecciónAMinúsculasToolStripMenuItem_Click);
            // 
            // commentSelectionToolStripMenuItem
            // 
            this.commentSelectionToolStripMenuItem.Name = "commentSelectionToolStripMenuItem";
            this.commentSelectionToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
            this.commentSelectionToolStripMenuItem.Size = new System.Drawing.Size(270, 22);
            this.commentSelectionToolStripMenuItem.Text = "Comment Selection";
            this.commentSelectionToolStripMenuItem.Click += new System.EventHandler(this.commentSelectionToolStripMenuItem_Click);
            // 
            // uncommentSelectionToolStripMenuItem
            // 
            this.uncommentSelectionToolStripMenuItem.Name = "uncommentSelectionToolStripMenuItem";
            this.uncommentSelectionToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.M)));
            this.uncommentSelectionToolStripMenuItem.Size = new System.Drawing.Size(270, 22);
            this.uncommentSelectionToolStripMenuItem.Text = "Uncomment Selection";
            this.uncommentSelectionToolStripMenuItem.Click += new System.EventHandler(this.uncommentSelectionToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(267, 6);
            // 
            // eliminarEspaciosFinDeLíneaToolStripMenuItem
            // 
            this.eliminarEspaciosFinDeLíneaToolStripMenuItem.Name = "eliminarEspaciosFinDeLíneaToolStripMenuItem";
            this.eliminarEspaciosFinDeLíneaToolStripMenuItem.Size = new System.Drawing.Size(270, 22);
            this.eliminarEspaciosFinDeLíneaToolStripMenuItem.Text = "Eliminar espacios fin de línea";
            this.eliminarEspaciosFinDeLíneaToolStripMenuItem.Click += new System.EventHandler(this.eliminarEspaciosFinDeLíneaToolStripMenuItem_Click);
            // 
            // tABAEspaciosToolStripMenuItem
            // 
            this.tABAEspaciosToolStripMenuItem.Name = "tABAEspaciosToolStripMenuItem";
            this.tABAEspaciosToolStripMenuItem.Size = new System.Drawing.Size(270, 22);
            this.tABAEspaciosToolStripMenuItem.Text = "TAB a Espacios";
            this.tABAEspaciosToolStripMenuItem.Click += new System.EventHandler(this.tABAEspaciosToolStripMenuItem_Click);
            // 
            // autoCompleteToolStripMenuItem
            // 
            this.autoCompleteToolStripMenuItem.Name = "autoCompleteToolStripMenuItem";
            this.autoCompleteToolStripMenuItem.Size = new System.Drawing.Size(270, 22);
            this.autoCompleteToolStripMenuItem.Text = "Auto Complete";
            this.autoCompleteToolStripMenuItem.Click += new System.EventHandler(this.autoCompleteToolStripMenuItem_Click);
            // 
            // tabSizeToolStripMenuItem
            // 
            this.tabSizeToolStripMenuItem.Name = "tabSizeToolStripMenuItem";
            this.tabSizeToolStripMenuItem.Size = new System.Drawing.Size(60, 23);
            this.tabSizeToolStripMenuItem.Text = "Tab Size";
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.Size = new System.Drawing.Size(20, 23);
            this.toolStripTextBox1.Text = "4";
            this.toolStripTextBox1.TextChanged += new System.EventHandler(this.toolStripTextBox1_TextChanged);
            // 
            // baseDeDatoToolStripMenuItem
            // 
            this.baseDeDatoToolStripMenuItem.Name = "baseDeDatoToolStripMenuItem";
            this.baseDeDatoToolStripMenuItem.Size = new System.Drawing.Size(87, 23);
            this.baseDeDatoToolStripMenuItem.Text = "Base de Dato";
            // 
            // databasesToolStripMenuItem
            // 
            this.databasesToolStripMenuItem.AutoSize = false;
            this.databasesToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control;
            this.databasesToolStripMenuItem.Name = "databasesToolStripMenuItem";
            this.databasesToolStripMenuItem.Size = new System.Drawing.Size(210, 23);
            this.databasesToolStripMenuItem.SelectedIndexChanged += new System.EventHandler(this.databasesToolStripMenuItem_SelectedIndexChanged);
            // 
            // ayudToolStripMenuItem
            // 
            this.ayudToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.acercaDeToolStripMenuItem});
            this.ayudToolStripMenuItem.Name = "ayudToolStripMenuItem";
            this.ayudToolStripMenuItem.Size = new System.Drawing.Size(53, 23);
            this.ayudToolStripMenuItem.Text = "Ay&uda";
            // 
            // acercaDeToolStripMenuItem
            // 
            this.acercaDeToolStripMenuItem.Name = "acercaDeToolStripMenuItem";
            this.acercaDeToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.acercaDeToolStripMenuItem.Text = "Acerca de...";
            this.acercaDeToolStripMenuItem.Click += new System.EventHandler(this.acercaDeToolStripMenuItem_Click);
            // 
            // chkToText
            // 
            this.chkToText.AutoSize = true;
            this.chkToText.BackColor = System.Drawing.SystemColors.Control;
            this.chkToText.FlatAppearance.BorderSize = 2;
            this.chkToText.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.chkToText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkToText.ForeColor = System.Drawing.Color.Black;
            this.chkToText.Location = new System.Drawing.Point(1047, 7);
            this.chkToText.Name = "chkToText";
            this.chkToText.Size = new System.Drawing.Size(79, 17);
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
            this.statusStrip1.Location = new System.Drawing.Point(0, 594);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1163, 31);
            this.statusStrip1.TabIndex = 14;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripSplitButton1
            // 
            this.toolStripSplitButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripSplitButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSplitButton1.Image")));
            this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton1.Name = "toolStripSplitButton1";
            this.toolStripSplitButton1.Size = new System.Drawing.Size(40, 29);
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
            this.tssLaFile.Size = new System.Drawing.Size(350, 26);
            this.tssLaFile.Text = "tssLaFile";
            // 
            // tssLaPath
            // 
            this.tssLaPath.AutoSize = false;
            this.tssLaPath.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tssLaPath.Name = "tssLaPath";
            this.tssLaPath.Size = new System.Drawing.Size(360, 26);
            this.tssLaPath.Text = "tssLaPath";
            // 
            // tssLaPos
            // 
            this.tssLaPos.AutoSize = false;
            this.tssLaPos.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tssLaPos.Name = "tssLaPos";
            this.tssLaPos.Size = new System.Drawing.Size(180, 26);
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
            this.tssLaStat.Size = new System.Drawing.Size(210, 26);
            this.tssLaStat.Text = "tssLaStat";
            // 
            // splitC
            // 
            this.splitC.BackColor = System.Drawing.SystemColors.Control;
            this.splitC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitC.Location = new System.Drawing.Point(0, 25);
            this.splitC.Name = "splitC";
            // 
            // splitC.Panel1
            // 
            this.splitC.Panel1.Controls.Add(this.laBuscarTablas);
            this.splitC.Panel1.Controls.Add(this.txBuscaEnLista);
            this.splitC.Panel1.Controls.Add(this.panObjetos);
            this.splitC.Panel1.Controls.Add(this.panColumnas);
            this.splitC.Panel1.Margin = new System.Windows.Forms.Padding(1);
            this.splitC.Panel1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.splitC.Panel1MinSize = 10;
            // 
            // splitC.Panel2
            // 
            this.splitC.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.splitC.Panel2.Controls.Add(this.txtSql);
            this.splitC.Panel2.Padding = new System.Windows.Forms.Padding(3);
            this.splitC.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.splitC.Size = new System.Drawing.Size(1163, 569);
            this.splitC.SplitterDistance = 232;
            this.splitC.TabIndex = 15;
            // 
            // laBuscarTablas
            // 
            this.laBuscarTablas.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.laBuscarTablas.AutoSize = true;
            this.laBuscarTablas.BackColor = System.Drawing.SystemColors.Control;
            this.laBuscarTablas.Location = new System.Drawing.Point(8, 283);
            this.laBuscarTablas.Name = "laBuscarTablas";
            this.laBuscarTablas.Size = new System.Drawing.Size(70, 13);
            this.laBuscarTablas.TabIndex = 28;
            this.laBuscarTablas.Text = "Buscar Tabla";
            // 
            // txBuscaEnLista
            // 
            this.txBuscaEnLista.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txBuscaEnLista.BackColor = System.Drawing.SystemColors.Control;
            this.txBuscaEnLista.ForeColor = System.Drawing.Color.Black;
            this.txBuscaEnLista.Location = new System.Drawing.Point(5, 298);
            this.txBuscaEnLista.Name = "txBuscaEnLista";
            this.txBuscaEnLista.Size = new System.Drawing.Size(224, 20);
            this.txBuscaEnLista.TabIndex = 26;
            this.txBuscaEnLista.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txBuscaEnLista_KeyDown);
            // 
            // panObjetos
            // 
            this.panObjetos.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panObjetos.BackColor = System.Drawing.SystemColors.Control;
            this.panObjetos.Controls.Add(this.btRefreshType);
            this.panObjetos.Controls.Add(this.cbObjetos);
            this.panObjetos.Controls.Add(this.laTablas);
            this.panObjetos.Controls.Add(this.lstObjetos);
            this.panObjetos.Dock = System.Windows.Forms.DockStyle.Top;
            this.panObjetos.Location = new System.Drawing.Point(0, 0);
            this.panObjetos.Margin = new System.Windows.Forms.Padding(2);
            this.panObjetos.Name = "panObjetos";
            this.panObjetos.Size = new System.Drawing.Size(232, 322);
            this.panObjetos.TabIndex = 21;
            // 
            // btRefreshType
            // 
            this.btRefreshType.Image = ((System.Drawing.Image)(resources.GetObject("btRefreshType.Image")));
            this.btRefreshType.Location = new System.Drawing.Point(192, 252);
            this.btRefreshType.Name = "btRefreshType";
            this.btRefreshType.Size = new System.Drawing.Size(27, 25);
            this.btRefreshType.TabIndex = 28;
            this.btRefreshType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btRefreshType.UseVisualStyleBackColor = true;
            this.btRefreshType.Click += new System.EventHandler(this.btRefreshType_Click);
            // 
            // cbObjetos
            // 
            this.cbObjetos.BackColor = System.Drawing.SystemColors.Control;
            this.cbObjetos.ForeColor = System.Drawing.Color.Black;
            this.cbObjetos.FormattingEnabled = true;
            this.cbObjetos.Location = new System.Drawing.Point(5, 253);
            this.cbObjetos.Margin = new System.Windows.Forms.Padding(2);
            this.cbObjetos.Name = "cbObjetos";
            this.cbObjetos.Size = new System.Drawing.Size(183, 21);
            this.cbObjetos.TabIndex = 13;
            this.cbObjetos.SelectedValueChanged += new System.EventHandler(this.cbObjetos_SelectedValueChanged);
            // 
            // laTablas
            // 
            this.laTablas.AutoSize = true;
            this.laTablas.Location = new System.Drawing.Point(130, 279);
            this.laTablas.Name = "laTablas";
            this.laTablas.Size = new System.Drawing.Size(47, 13);
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
            this.lstObjetos.Location = new System.Drawing.Point(0, 0);
            this.lstObjetos.Name = "lstObjetos";
            this.lstObjetos.ScrollAlwaysVisible = true;
            this.lstObjetos.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstObjetos.Size = new System.Drawing.Size(232, 249);
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
            this.panColumnas.Location = new System.Drawing.Point(0, 326);
            this.panColumnas.Margin = new System.Windows.Forms.Padding(2);
            this.panColumnas.Name = "panColumnas";
            this.panColumnas.Size = new System.Drawing.Size(232, 240);
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
            this.lsColumnas.Margin = new System.Windows.Forms.Padding(2);
            this.lsColumnas.Name = "lsColumnas";
            this.lsColumnas.ShowGroups = false;
            this.lsColumnas.Size = new System.Drawing.Size(232, 240);
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
            this.txtSql.Location = new System.Drawing.Point(3, 3);
            this.txtSql.Name = "txtSql";
            this.txtSql.Size = new System.Drawing.Size(921, 563);
            this.txtSql.TabIndex = 0;
            this.txtSql.CharAdded += new System.EventHandler<ScintillaNET.CharAddedEventArgs>(this.txtSql_CharAdded);
            this.txtSql.InsertCheck += new System.EventHandler<ScintillaNET.InsertCheckEventArgs>(this.txtSql_InsertCheck);
            this.txtSql.UpdateUI += new System.EventHandler<ScintillaNET.UpdateUIEventArgs>(this.txtSql_SelectionChanged);
            this.txtSql.TextChanged += new System.EventHandler(this.txtSql_TextChanged);
            this.txtSql.DragDrop += new System.Windows.Forms.DragEventHandler(this.txtSql_DragDrop);
            this.txtSql.DragEnter += new System.Windows.Forms.DragEventHandler(this.txtSql_DragEnter);
            this.txtSql.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSql_KeyPress);
            // 
            // btBuscarEnBd
            // 
            this.btBuscarEnBd.BackColor = System.Drawing.SystemColors.Control;
            this.btBuscarEnBd.Image = global::SQLCrypt.Properties.Resources.Lupa;
            this.btBuscarEnBd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btBuscarEnBd.Location = new System.Drawing.Point(756, 1);
            this.btBuscarEnBd.Name = "btBuscarEnBd";
            this.btBuscarEnBd.Size = new System.Drawing.Size(98, 25);
            this.btBuscarEnBd.TabIndex = 18;
            this.btBuscarEnBd.Text = "&Buscar en BD";
            this.btBuscarEnBd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btBuscarEnBd.UseVisualStyleBackColor = false;
            this.btBuscarEnBd.Click += new System.EventHandler(this.btBuscarEnBd_Click);
            // 
            // btConnectToBd
            // 
            this.btConnectToBd.BackColor = System.Drawing.SystemColors.Control;
            this.btConnectToBd.Image = global::SQLCrypt.Properties.Resources.Connect3;
            this.btConnectToBd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btConnectToBd.Location = new System.Drawing.Point(593, 1);
            this.btConnectToBd.Name = "btConnectToBd";
            this.btConnectToBd.Size = new System.Drawing.Size(118, 25);
            this.btConnectToBd.TabIndex = 19;
            this.btConnectToBd.Text = "&Conectar a BD";
            this.btConnectToBd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btConnectToBd.UseVisualStyleBackColor = false;
            this.btConnectToBd.Click += new System.EventHandler(this.btConnectToBd_Click);
            // 
            // btReconnect
            // 
            this.btReconnect.Image = ((System.Drawing.Image)(resources.GetObject("btReconnect.Image")));
            this.btReconnect.Location = new System.Drawing.Point(714, 1);
            this.btReconnect.Name = "btReconnect";
            this.btReconnect.Size = new System.Drawing.Size(26, 25);
            this.btReconnect.TabIndex = 29;
            this.btReconnect.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btReconnect.UseVisualStyleBackColor = true;
            this.btReconnect.Click += new System.EventHandler(this.btReconnect_Click);
            // 
            // FrmSqlCrypt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = global::SQLCrypt.Properties.Settings.Default.AppBackColor;
            this.ClientSize = new System.Drawing.Size(1163, 625);
            this.Controls.Add(this.btReconnect);
            this.Controls.Add(this.btConnectToBd);
            this.Controls.Add(this.btBuscarEnBd);
            this.Controls.Add(this.splitC);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.chkToText);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(1130, 623);
            this.Name = "FrmSqlCrypt";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sql Crypt";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmSqlCrypt_FormClosing);
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
      private System.Windows.Forms.ToolStripMenuItem cerrarToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem ejecutarComandoToolStripMenuItem;
      private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
      private System.Windows.Forms.ToolStripMenuItem grabarComoToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem abrirToolStripMenuItem;
      private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
      private System.Windows.Forms.ToolStripComboBox databasesToolStripMenuItem;
      private System.Windows.Forms.CheckBox chkToText;
      private System.Windows.Forms.MyTextBox txTextLimit;
      private System.Windows.Forms.ToolStripMenuItem encriptarClavesToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem comandosInmediatosToolStripMenuItem1;
      private System.Windows.Forms.StatusStrip statusStrip1;
      private System.Windows.Forms.ToolStripStatusLabel tssLaFile;
      private System.Windows.Forms.ToolStripStatusLabel tssLaPath;
      private System.Windows.Forms.ToolStripStatusLabel tssLaPos;
      private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton1;
      private System.Windows.Forms.ToolStripStatusLabel tssLaStat;
      private System.Windows.Forms.ToolStripMenuItem ejecutarTodasLasBasesToolStripMenuItem;
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
        private System.Windows.Forms.ToolStripMenuItem buscarEnBDToolStripMenuItem;
        private System.Windows.Forms.Button btBuscarEnBd;
        private System.Windows.Forms.Button btConnectToBd;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mostrarEspaciosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem guiaIndentacionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eliminarEspaciosFinDeLíneaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tabSizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
        private System.Windows.Forms.ToolStripMenuItem selecciónAMayúsculasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selecciónAMinúsculasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem findReplaceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem numerosDeLíneaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem commentSelectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uncommentSelectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem findNextToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem findPreviousToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem baseDeDatoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem indicesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tABAEspaciosToolStripMenuItem;
        private System.Windows.Forms.Button btRefreshType;
        private System.Windows.Forms.Button btReconnect;
        private System.Windows.Forms.ToolStripMenuItem autoCompleteToolStripMenuItem;
    }
}

