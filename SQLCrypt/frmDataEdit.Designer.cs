namespace SQLCrypt
{
    partial class frmDataEdit
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
            if (disposing && ( components != null ))
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
            this.dgv = new System.Windows.Forms.DataGridView();
            this.btTraeDatos = new System.Windows.Forms.Button();
            this.txFiltro = new System.Windows.Forms.TextBox();
            this.btAplicaCambios = new System.Windows.Forms.Button();
            this.btSalir = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.checkIncluyeColBinarias = new System.Windows.Forms.CheckBox();
            this.cbColumna = new System.Windows.Forms.ComboBox();
            this.cbOperador = new System.Windows.Forms.ComboBox();
            this.txFiltroAdd = new System.Windows.Forms.TextBox();
            this.cbAndOr = new System.Windows.Forms.ComboBox();
            this.btCleanFilter = new System.Windows.Forms.Button();
            this.btAddFilter = new System.Windows.Forms.Button();
            this.btBorrarActual = new System.Windows.Forms.Button();
            this.labelPK = new System.Windows.Forms.Label();
            this.laFilas = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv
            // 
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgv.Location = new System.Drawing.Point(0, 0);
            this.dgv.Name = "dgv";
            this.dgv.Size = new System.Drawing.Size(801, 369);
            this.dgv.TabIndex = 0;
            // 
            // btTraeDatos
            // 
            this.btTraeDatos.Location = new System.Drawing.Point(6, 379);
            this.btTraeDatos.Name = "btTraeDatos";
            this.btTraeDatos.Size = new System.Drawing.Size(107, 23);
            this.btTraeDatos.TabIndex = 1;
            this.btTraeDatos.Text = "Traer Datos";
            this.btTraeDatos.UseVisualStyleBackColor = true;
            this.btTraeDatos.Click += new System.EventHandler(this.btTraeDatos_Click);
            // 
            // txFiltro
            // 
            this.txFiltro.Location = new System.Drawing.Point(1, 463);
            this.txFiltro.Multiline = true;
            this.txFiltro.Name = "txFiltro";
            this.txFiltro.Size = new System.Drawing.Size(797, 90);
            this.txFiltro.TabIndex = 2;
            // 
            // btAplicaCambios
            // 
            this.btAplicaCambios.Location = new System.Drawing.Point(119, 379);
            this.btAplicaCambios.Name = "btAplicaCambios";
            this.btAplicaCambios.Size = new System.Drawing.Size(107, 23);
            this.btAplicaCambios.TabIndex = 3;
            this.btAplicaCambios.Text = "Aplicar Cambios";
            this.btAplicaCambios.UseVisualStyleBackColor = true;
            this.btAplicaCambios.Click += new System.EventHandler(this.btAplicaCambios_Click);
            // 
            // btSalir
            // 
            this.btSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btSalir.Location = new System.Drawing.Point(345, 379);
            this.btSalir.Name = "btSalir";
            this.btSalir.Size = new System.Drawing.Size(107, 23);
            this.btSalir.TabIndex = 4;
            this.btSalir.Text = "Salir";
            this.btSalir.UseVisualStyleBackColor = true;
            this.btSalir.Click += new System.EventHandler(this.btSalir_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 415);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Filtro";
            // 
            // checkIncluyeColBinarias
            // 
            this.checkIncluyeColBinarias.AutoSize = true;
            this.checkIncluyeColBinarias.Location = new System.Drawing.Point(649, 383);
            this.checkIncluyeColBinarias.Name = "checkIncluyeColBinarias";
            this.checkIncluyeColBinarias.Size = new System.Drawing.Size(143, 17);
            this.checkIncluyeColBinarias.TabIndex = 6;
            this.checkIncluyeColBinarias.Text = "Incluir Columnas Binarias";
            this.checkIncluyeColBinarias.UseVisualStyleBackColor = true;
            // 
            // cbColumna
            // 
            this.cbColumna.FormattingEnabled = true;
            this.cbColumna.Location = new System.Drawing.Point(168, 439);
            this.cbColumna.Name = "cbColumna";
            this.cbColumna.Size = new System.Drawing.Size(234, 21);
            this.cbColumna.TabIndex = 7;
            this.cbColumna.SelectedIndexChanged += new System.EventHandler(this.cbColumna_SelectedIndexChanged);
            // 
            // cbOperador
            // 
            this.cbOperador.FormattingEnabled = true;
            this.cbOperador.Items.AddRange(new object[] {
            "=",
            "Like",
            "in"});
            this.cbOperador.Location = new System.Drawing.Point(406, 439);
            this.cbOperador.Name = "cbOperador";
            this.cbOperador.Size = new System.Drawing.Size(92, 21);
            this.cbOperador.TabIndex = 8;
            // 
            // txFiltroAdd
            // 
            this.txFiltroAdd.Location = new System.Drawing.Point(501, 440);
            this.txFiltroAdd.Name = "txFiltroAdd";
            this.txFiltroAdd.Size = new System.Drawing.Size(297, 20);
            this.txFiltroAdd.TabIndex = 9;
            // 
            // cbAndOr
            // 
            this.cbAndOr.FormattingEnabled = true;
            this.cbAndOr.Items.AddRange(new object[] {
            "",
            "And",
            "Or"});
            this.cbAndOr.Location = new System.Drawing.Point(101, 439);
            this.cbAndOr.Name = "cbAndOr";
            this.cbAndOr.Size = new System.Drawing.Size(63, 21);
            this.cbAndOr.TabIndex = 10;
            // 
            // btCleanFilter
            // 
            this.btCleanFilter.Location = new System.Drawing.Point(3, 437);
            this.btCleanFilter.Name = "btCleanFilter";
            this.btCleanFilter.Size = new System.Drawing.Size(48, 23);
            this.btCleanFilter.TabIndex = 11;
            this.btCleanFilter.Text = "Limpia";
            this.btCleanFilter.UseVisualStyleBackColor = true;
            this.btCleanFilter.Click += new System.EventHandler(this.btCleanFilter_Click);
            // 
            // btAddFilter
            // 
            this.btAddFilter.Location = new System.Drawing.Point(54, 437);
            this.btAddFilter.Name = "btAddFilter";
            this.btAddFilter.Size = new System.Drawing.Size(42, 23);
            this.btAddFilter.TabIndex = 12;
            this.btAddFilter.Text = "Add";
            this.btAddFilter.UseVisualStyleBackColor = true;
            this.btAddFilter.Click += new System.EventHandler(this.btAddFilter_Click);
            // 
            // btBorrarActual
            // 
            this.btBorrarActual.Location = new System.Drawing.Point(232, 379);
            this.btBorrarActual.Name = "btBorrarActual";
            this.btBorrarActual.Size = new System.Drawing.Size(107, 23);
            this.btBorrarActual.TabIndex = 13;
            this.btBorrarActual.Text = "Borrar Actual";
            this.btBorrarActual.UseVisualStyleBackColor = true;
            this.btBorrarActual.Click += new System.EventHandler(this.btBorrarActual_Click);
            // 
            // labelPK
            // 
            this.labelPK.AutoSize = true;
            this.labelPK.Location = new System.Drawing.Point(4, 558);
            this.labelPK.Name = "labelPK";
            this.labelPK.Size = new System.Drawing.Size(110, 13);
            this.labelPK.TabIndex = 14;
            this.labelPK.Text = "Acá va la PK si la hay";
            // 
            // laFilas
            // 
            this.laFilas.AutoSize = true;
            this.laFilas.Location = new System.Drawing.Point(484, 384);
            this.laFilas.Name = "laFilas";
            this.laFilas.Size = new System.Drawing.Size(28, 13);
            this.laFilas.TabIndex = 15;
            this.laFilas.Text = "Filas";
            // 
            // frmDataEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btSalir;
            this.ClientSize = new System.Drawing.Size(801, 577);
            this.Controls.Add(this.laFilas);
            this.Controls.Add(this.labelPK);
            this.Controls.Add(this.btBorrarActual);
            this.Controls.Add(this.btAddFilter);
            this.Controls.Add(this.btCleanFilter);
            this.Controls.Add(this.cbAndOr);
            this.Controls.Add(this.txFiltroAdd);
            this.Controls.Add(this.cbOperador);
            this.Controls.Add(this.cbColumna);
            this.Controls.Add(this.checkIncluyeColBinarias);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btSalir);
            this.Controls.Add(this.btAplicaCambios);
            this.Controls.Add(this.txFiltro);
            this.Controls.Add(this.btTraeDatos);
            this.Controls.Add(this.dgv);
            this.Name = "frmDataEdit";
            this.Text = "frmDataEdit";
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Button btTraeDatos;
        private System.Windows.Forms.TextBox txFiltro;
        private System.Windows.Forms.Button btAplicaCambios;
        private System.Windows.Forms.Button btSalir;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkIncluyeColBinarias;
        private System.Windows.Forms.ComboBox cbColumna;
        private System.Windows.Forms.ComboBox cbOperador;
        private System.Windows.Forms.TextBox txFiltroAdd;
        private System.Windows.Forms.ComboBox cbAndOr;
        private System.Windows.Forms.Button btCleanFilter;
        private System.Windows.Forms.Button btAddFilter;
        private System.Windows.Forms.Button btBorrarActual;
        private System.Windows.Forms.Label labelPK;
        private System.Windows.Forms.Label laFilas;
    }
}