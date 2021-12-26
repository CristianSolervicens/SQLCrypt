namespace SQLCrypt
{
    partial class frmObjects
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmObjects));
            this.lstObjetos = new System.Windows.Forms.ListBox();
            this.btRefreshType = new System.Windows.Forms.Button();
            this.cbObjType = new System.Windows.Forms.ComboBox();
            this.txFiltro = new System.Windows.Forms.TextBox();
            this.btRefreshFiltro = new System.Windows.Forms.Button();
            this.rchTxt = new System.Windows.Forms.RichTextBox();
            this.cbFiltro = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btFind = new System.Windows.Forms.Button();
            this.txFind = new System.Windows.Forms.TextBox();
            this.btSalir = new System.Windows.Forms.Button();
            this.laTextPosition = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txBuscaEnLista = new System.Windows.Forms.TextBox();
            this.btSaveSQL = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabelSel = new System.Windows.Forms.ToolStripStatusLabel();
            this.btLimpiaFiltro = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstObjetos
            // 
            this.lstObjetos.FormattingEnabled = true;
            this.lstObjetos.Location = new System.Drawing.Point(3, 149);
            this.lstObjetos.Name = "lstObjetos";
            this.lstObjetos.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstObjetos.Size = new System.Drawing.Size(480, 498);
            this.lstObjetos.TabIndex = 10;
            this.lstObjetos.SelectedIndexChanged += new System.EventHandler(this.lstObjetos_SelectedIndexChanged);
            // 
            // btRefreshType
            // 
            this.btRefreshType.Image = ((System.Drawing.Image)(resources.GetObject("btRefreshType.Image")));
            this.btRefreshType.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btRefreshType.Location = new System.Drawing.Point(418, 5);
            this.btRefreshType.Name = "btRefreshType";
            this.btRefreshType.Size = new System.Drawing.Size(70, 23);
            this.btRefreshType.TabIndex = 1;
            this.btRefreshType.Text = "Refresh";
            this.btRefreshType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btRefreshType.UseVisualStyleBackColor = true;
            this.btRefreshType.Click += new System.EventHandler(this.btRefreshType_Click);
            // 
            // cbObjType
            // 
            this.cbObjType.FormattingEnabled = true;
            this.cbObjType.Location = new System.Drawing.Point(91, 6);
            this.cbObjType.Name = "cbObjType";
            this.cbObjType.Size = new System.Drawing.Size(318, 21);
            this.cbObjType.TabIndex = 0;
            // 
            // txFiltro
            // 
            this.txFiltro.Location = new System.Drawing.Point(91, 56);
            this.txFiltro.Multiline = true;
            this.txFiltro.Name = "txFiltro";
            this.txFiltro.Size = new System.Drawing.Size(392, 61);
            this.txFiltro.TabIndex = 3;
            // 
            // btRefreshFiltro
            // 
            this.btRefreshFiltro.Location = new System.Drawing.Point(12, 57);
            this.btRefreshFiltro.Name = "btRefreshFiltro";
            this.btRefreshFiltro.Size = new System.Drawing.Size(71, 23);
            this.btRefreshFiltro.TabIndex = 4;
            this.btRefreshFiltro.Text = "Filtrar";
            this.btRefreshFiltro.UseVisualStyleBackColor = true;
            this.btRefreshFiltro.Click += new System.EventHandler(this.btRefreshFiltro_Click);
            // 
            // rchTxt
            // 
            this.rchTxt.Font = new System.Drawing.Font("Courier New", 10F);
            this.rchTxt.Location = new System.Drawing.Point(488, 34);
            this.rchTxt.Name = "rchTxt";
            this.rchTxt.RightMargin = 2048;
            this.rchTxt.Size = new System.Drawing.Size(690, 613);
            this.rchTxt.TabIndex = 11;
            this.rchTxt.Text = "";
            this.rchTxt.WordWrap = false;
            this.rchTxt.SelectionChanged += new System.EventHandler(this.rchTxt_SelectionChanged);
            this.rchTxt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rchTxt_KeyDown);
            // 
            // cbFiltro
            // 
            this.cbFiltro.FormattingEnabled = true;
            this.cbFiltro.Items.AddRange(new object[] {
            "Contiene el Texto",
            "Contiene la Columna"});
            this.cbFiltro.Location = new System.Drawing.Point(91, 31);
            this.cbFiltro.Name = "cbFiltro";
            this.cbFiltro.Size = new System.Drawing.Size(318, 21);
            this.cbFiltro.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Tipo Objeto";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Tipo Filtro";
            // 
            // btFind
            // 
            this.btFind.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btFind.Image = ((System.Drawing.Image)(resources.GetObject("btFind.Image")));
            this.btFind.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btFind.Location = new System.Drawing.Point(497, 7);
            this.btFind.Name = "btFind";
            this.btFind.Size = new System.Drawing.Size(65, 23);
            this.btFind.TabIndex = 7;
            this.btFind.Text = "&Buscar";
            this.btFind.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btFind.UseVisualStyleBackColor = true;
            this.btFind.Click += new System.EventHandler(this.btFind_Click);
            // 
            // txFind
            // 
            this.txFind.Location = new System.Drawing.Point(567, 9);
            this.txFind.Name = "txFind";
            this.txFind.Size = new System.Drawing.Size(209, 20);
            this.txFind.TabIndex = 6;
            this.txFind.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txFind_KeyUp);
            // 
            // btSalir
            // 
            this.btSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btSalir.Location = new System.Drawing.Point(791, 7);
            this.btSalir.Name = "btSalir";
            this.btSalir.Size = new System.Drawing.Size(41, 21);
            this.btSalir.TabIndex = 8;
            this.btSalir.Text = "Salir";
            this.btSalir.UseVisualStyleBackColor = true;
            this.btSalir.Click += new System.EventHandler(this.btSalir_Click);
            // 
            // laTextPosition
            // 
            this.laTextPosition.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.laTextPosition.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.laTextPosition.Location = new System.Drawing.Point(939, 8);
            this.laTextPosition.Name = "laTextPosition";
            this.laTextPosition.Size = new System.Drawing.Size(239, 19);
            this.laTextPosition.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 123);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Busca en Lista";
            // 
            // txBuscaEnLista
            // 
            this.txBuscaEnLista.Location = new System.Drawing.Point(91, 121);
            this.txBuscaEnLista.Name = "txBuscaEnLista";
            this.txBuscaEnLista.Size = new System.Drawing.Size(392, 20);
            this.txBuscaEnLista.TabIndex = 5;
            this.txBuscaEnLista.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txBuscaEnLista_KeyDown);
            // 
            // btSaveSQL
            // 
            this.btSaveSQL.Location = new System.Drawing.Point(842, 6);
            this.btSaveSQL.Name = "btSaveSQL";
            this.btSaveSQL.Size = new System.Drawing.Size(89, 23);
            this.btSaveSQL.TabIndex = 9;
            this.btSaveSQL.Text = "Guardar SQL";
            this.btSaveSQL.UseVisualStyleBackColor = true;
            this.btSaveSQL.Click += new System.EventHandler(this.btSaveSQL_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel,
            this.toolStripProgressBar1,
            this.toolStripStatusLabelSel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 650);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1183, 22);
            this.statusStrip1.TabIndex = 21;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(112, 17);
            this.toolStripStatusLabel.Text = "toolStripStatusLabel";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            // 
            // toolStripStatusLabelSel
            // 
            this.toolStripStatusLabelSel.Name = "toolStripStatusLabelSel";
            this.toolStripStatusLabelSel.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabelSel.Text = "toolStripStatusLabel1";
            // 
            // btLimpiaFiltro
            // 
            this.btLimpiaFiltro.Location = new System.Drawing.Point(12, 82);
            this.btLimpiaFiltro.Name = "btLimpiaFiltro";
            this.btLimpiaFiltro.Size = new System.Drawing.Size(71, 23);
            this.btLimpiaFiltro.TabIndex = 22;
            this.btLimpiaFiltro.Text = "Limpia Filtro";
            this.btLimpiaFiltro.UseVisualStyleBackColor = true;
            this.btLimpiaFiltro.Click += new System.EventHandler(this.btLimpiaFiltro_Click);
            // 
            // frmObjects
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btSalir;
            this.ClientSize = new System.Drawing.Size(1183, 672);
            this.Controls.Add(this.btLimpiaFiltro);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btSaveSQL);
            this.Controls.Add(this.txBuscaEnLista);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.laTextPosition);
            this.Controls.Add(this.btSalir);
            this.Controls.Add(this.txFind);
            this.Controls.Add(this.btFind);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbFiltro);
            this.Controls.Add(this.rchTxt);
            this.Controls.Add(this.btRefreshFiltro);
            this.Controls.Add(this.txFiltro);
            this.Controls.Add(this.cbObjType);
            this.Controls.Add(this.btRefreshType);
            this.Controls.Add(this.lstObjetos);
            this.KeyPreview = true;
            this.Name = "frmObjects";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Objetos";
            this.Resize += new System.EventHandler(this.frmObjects_Resize);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstObjetos;
        private System.Windows.Forms.Button btRefreshType;
        private System.Windows.Forms.ComboBox cbObjType;
        private System.Windows.Forms.TextBox txFiltro;
        private System.Windows.Forms.Button btRefreshFiltro;
        private System.Windows.Forms.RichTextBox rchTxt;
        private System.Windows.Forms.ComboBox cbFiltro;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btFind;
        private System.Windows.Forms.TextBox txFind;
        private System.Windows.Forms.Button btSalir;
        private System.Windows.Forms.Label laTextPosition;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txBuscaEnLista;
        private System.Windows.Forms.Button btSaveSQL;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelSel;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.Button btLimpiaFiltro;
    }
}