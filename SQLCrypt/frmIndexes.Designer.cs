namespace SQLCrypt
{
    partial class frmIndexes
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
            this.btProcesar = new System.Windows.Forms.Button();
            this.txTableName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txIndexName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txColumns = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txInclude = new System.Windows.Forms.TextBox();
            this.laDropped = new System.Windows.Forms.Label();
            this.laCreated = new System.Windows.Forms.Label();
            this.btCleanInclude = new System.Windows.Forms.Button();
            this.btCleanCols = new System.Windows.Forms.Button();
            this.btCleanIndex = new System.Windows.Forms.Button();
            this.btCleanTabla = new System.Windows.Forms.Button();
            this.btCleanAll = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btProcesar
            // 
            this.btProcesar.Location = new System.Drawing.Point(12, 153);
            this.btProcesar.Name = "btProcesar";
            this.btProcesar.Size = new System.Drawing.Size(85, 23);
            this.btProcesar.TabIndex = 0;
            this.btProcesar.Text = "Procesar";
            this.btProcesar.UseVisualStyleBackColor = true;
            this.btProcesar.Click += new System.EventHandler(this.btProcesar_Click);
            // 
            // txTableName
            // 
            this.txTableName.Location = new System.Drawing.Point(87, 34);
            this.txTableName.Name = "txTableName";
            this.txTableName.Size = new System.Drawing.Size(478, 20);
            this.txTableName.TabIndex = 1;
            this.txTableName.TextChanged += new System.EventHandler(this.txTableName_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Tabla";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Índice";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // txIndexName
            // 
            this.txIndexName.Location = new System.Drawing.Point(87, 60);
            this.txIndexName.Name = "txIndexName";
            this.txIndexName.Size = new System.Drawing.Size(478, 20);
            this.txIndexName.TabIndex = 3;
            this.txIndexName.TextChanged += new System.EventHandler(this.txIndexName_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Columnas";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // txColumns
            // 
            this.txColumns.Location = new System.Drawing.Point(87, 86);
            this.txColumns.Name = "txColumns";
            this.txColumns.Size = new System.Drawing.Size(478, 20);
            this.txColumns.TabIndex = 5;
            this.txColumns.TextChanged += new System.EventHandler(this.txColumns_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(28, 115);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Include";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // txInclude
            // 
            this.txInclude.Location = new System.Drawing.Point(87, 112);
            this.txInclude.Name = "txInclude";
            this.txInclude.Size = new System.Drawing.Size(478, 20);
            this.txInclude.TabIndex = 7;
            this.txInclude.TextChanged += new System.EventHandler(this.txInclude_TextChanged);
            // 
            // laDropped
            // 
            this.laDropped.AutoSize = true;
            this.laDropped.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.laDropped.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.laDropped.Location = new System.Drawing.Point(163, 145);
            this.laDropped.Name = "laDropped";
            this.laDropped.Size = new System.Drawing.Size(55, 13);
            this.laDropped.TabIndex = 9;
            this.laDropped.Text = "Dropped";
            // 
            // laCreated
            // 
            this.laCreated.AutoSize = true;
            this.laCreated.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.laCreated.ForeColor = System.Drawing.Color.Green;
            this.laCreated.Location = new System.Drawing.Point(163, 166);
            this.laCreated.Name = "laCreated";
            this.laCreated.Size = new System.Drawing.Size(51, 13);
            this.laCreated.TabIndex = 10;
            this.laCreated.Text = "Created";
            // 
            // btCleanInclude
            // 
            this.btCleanInclude.Location = new System.Drawing.Point(6, 112);
            this.btCleanInclude.Name = "btCleanInclude";
            this.btCleanInclude.Size = new System.Drawing.Size(15, 19);
            this.btCleanInclude.TabIndex = 11;
            this.btCleanInclude.UseVisualStyleBackColor = true;
            this.btCleanInclude.Click += new System.EventHandler(this.btCleanInclude_Click);
            // 
            // btCleanCols
            // 
            this.btCleanCols.Location = new System.Drawing.Point(6, 87);
            this.btCleanCols.Name = "btCleanCols";
            this.btCleanCols.Size = new System.Drawing.Size(15, 19);
            this.btCleanCols.TabIndex = 12;
            this.btCleanCols.UseVisualStyleBackColor = true;
            this.btCleanCols.Click += new System.EventHandler(this.btCleanCols_Click);
            // 
            // btCleanIndex
            // 
            this.btCleanIndex.Location = new System.Drawing.Point(6, 63);
            this.btCleanIndex.Name = "btCleanIndex";
            this.btCleanIndex.Size = new System.Drawing.Size(15, 19);
            this.btCleanIndex.TabIndex = 13;
            this.btCleanIndex.UseVisualStyleBackColor = true;
            this.btCleanIndex.Click += new System.EventHandler(this.btCleanIndex_Click);
            // 
            // btCleanTabla
            // 
            this.btCleanTabla.Location = new System.Drawing.Point(6, 35);
            this.btCleanTabla.Name = "btCleanTabla";
            this.btCleanTabla.Size = new System.Drawing.Size(15, 19);
            this.btCleanTabla.TabIndex = 14;
            this.btCleanTabla.UseVisualStyleBackColor = true;
            this.btCleanTabla.Click += new System.EventHandler(this.btCleanTabla_Click);
            // 
            // btCleanAll
            // 
            this.btCleanAll.Location = new System.Drawing.Point(6, 4);
            this.btCleanAll.Name = "btCleanAll";
            this.btCleanAll.Size = new System.Drawing.Size(82, 24);
            this.btCleanAll.TabIndex = 15;
            this.btCleanAll.Text = "Limpiar Todo";
            this.btCleanAll.UseVisualStyleBackColor = true;
            this.btCleanAll.Click += new System.EventHandler(this.btCleanAll_Click);
            // 
            // frmIndexes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(573, 189);
            this.Controls.Add(this.btCleanAll);
            this.Controls.Add(this.btCleanTabla);
            this.Controls.Add(this.btCleanIndex);
            this.Controls.Add(this.btCleanCols);
            this.Controls.Add(this.btCleanInclude);
            this.Controls.Add(this.laCreated);
            this.Controls.Add(this.laDropped);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txInclude);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txColumns);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txIndexName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txTableName);
            this.Controls.Add(this.btProcesar);
            this.Name = "frmIndexes";
            this.Text = "Índices";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btProcesar;
        private System.Windows.Forms.TextBox txTableName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txIndexName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txColumns;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txInclude;
        private System.Windows.Forms.Label laDropped;
        private System.Windows.Forms.Label laCreated;
        private System.Windows.Forms.Button btCleanInclude;
        private System.Windows.Forms.Button btCleanCols;
        private System.Windows.Forms.Button btCleanIndex;
        private System.Windows.Forms.Button btCleanTabla;
        private System.Windows.Forms.Button btCleanAll;
    }
}