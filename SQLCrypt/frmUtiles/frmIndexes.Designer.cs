namespace SQLCrypt.frmUtiles
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmIndexes));
            this.txtIndex = new System.Windows.Forms.TextBox();
            this.btParse = new System.Windows.Forms.Button();
            this.btPaste = new System.Windows.Forms.Button();
            this.laStatus = new System.Windows.Forms.Label();
            this.lsCurrentIndex = new System.Windows.Forms.ListBox();
            this.lsExistingIndexes = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btCreateIndex = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTableName = new System.Windows.Forms.TextBox();
            this.btGetIndexes = new System.Windows.Forms.Button();
            this.btSalir = new System.Windows.Forms.Button();
            this.pnlHead = new System.Windows.Forms.Panel();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.pnlHead.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtIndex
            // 
            this.txtIndex.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtIndex.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIndex.Location = new System.Drawing.Point(6, 36);
            this.txtIndex.Multiline = true;
            this.txtIndex.Name = "txtIndex";
            this.txtIndex.Size = new System.Drawing.Size(976, 44);
            this.txtIndex.TabIndex = 1;
            // 
            // btParse
            // 
            this.btParse.Location = new System.Drawing.Point(313, 6);
            this.btParse.Name = "btParse";
            this.btParse.Size = new System.Drawing.Size(115, 23);
            this.btParse.TabIndex = 2;
            this.btParse.Text = "Parse and Check";
            this.btParse.UseVisualStyleBackColor = true;
            this.btParse.Click += new System.EventHandler(this.btParse_Click);
            // 
            // btPaste
            // 
            this.btPaste.Location = new System.Drawing.Point(8, 6);
            this.btPaste.Name = "btPaste";
            this.btPaste.Size = new System.Drawing.Size(297, 24);
            this.btPaste.TabIndex = 3;
            this.btPaste.Text = "Create Index Statement (Clean Field and Paste Statement)";
            this.btPaste.UseVisualStyleBackColor = true;
            this.btPaste.Click += new System.EventHandler(this.btPaste_Click);
            // 
            // laStatus
            // 
            this.laStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.laStatus.AutoSize = true;
            this.laStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.laStatus.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.laStatus.Location = new System.Drawing.Point(9, 370);
            this.laStatus.Name = "laStatus";
            this.laStatus.Size = new System.Drawing.Size(50, 16);
            this.laStatus.TabIndex = 4;
            this.laStatus.Text = "label1";
            // 
            // lsCurrentIndex
            // 
            this.lsCurrentIndex.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lsCurrentIndex.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lsCurrentIndex.FormattingEnabled = true;
            this.lsCurrentIndex.HorizontalScrollbar = true;
            this.lsCurrentIndex.ItemHeight = 15;
            this.lsCurrentIndex.Location = new System.Drawing.Point(8, 103);
            this.lsCurrentIndex.Name = "lsCurrentIndex";
            this.lsCurrentIndex.Size = new System.Drawing.Size(974, 109);
            this.lsCurrentIndex.TabIndex = 5;
            // 
            // lsExistingIndexes
            // 
            this.lsExistingIndexes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lsExistingIndexes.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lsExistingIndexes.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lsExistingIndexes.FormattingEnabled = true;
            this.lsExistingIndexes.HorizontalScrollbar = true;
            this.lsExistingIndexes.ItemHeight = 18;
            this.lsExistingIndexes.Location = new System.Drawing.Point(9, 36);
            this.lsExistingIndexes.Name = "lsExistingIndexes";
            this.lsExistingIndexes.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lsExistingIndexes.Size = new System.Drawing.Size(973, 292);
            this.lsExistingIndexes.TabIndex = 6;
            this.lsExistingIndexes.SelectedIndexChanged += new System.EventHandler(this.lsExistingIndexes_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Index Characteristics";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Existing Indexes";
            // 
            // btCreateIndex
            // 
            this.btCreateIndex.Location = new System.Drawing.Point(434, 6);
            this.btCreateIndex.Name = "btCreateIndex";
            this.btCreateIndex.Size = new System.Drawing.Size(115, 23);
            this.btCreateIndex.TabIndex = 9;
            this.btCreateIndex.Text = "CREATE INDEX";
            this.btCreateIndex.UseVisualStyleBackColor = true;
            this.btCreateIndex.Click += new System.EventHandler(this.btCreateIndex_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(229, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(133, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Table Name (with schema)";
            // 
            // txtTableName
            // 
            this.txtTableName.Location = new System.Drawing.Point(369, 10);
            this.txtTableName.Name = "txtTableName";
            this.txtTableName.Size = new System.Drawing.Size(248, 20);
            this.txtTableName.TabIndex = 11;
            // 
            // btGetIndexes
            // 
            this.btGetIndexes.Location = new System.Drawing.Point(656, 8);
            this.btGetIndexes.Name = "btGetIndexes";
            this.btGetIndexes.Size = new System.Drawing.Size(115, 23);
            this.btGetIndexes.TabIndex = 12;
            this.btGetIndexes.Text = "Get Indexes";
            this.btGetIndexes.UseVisualStyleBackColor = true;
            this.btGetIndexes.Click += new System.EventHandler(this.btGetIndexes_Click);
            // 
            // btSalir
            // 
            this.btSalir.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btSalir.Location = new System.Drawing.Point(866, 8);
            this.btSalir.Name = "btSalir";
            this.btSalir.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.btSalir.Size = new System.Drawing.Size(115, 23);
            this.btSalir.TabIndex = 13;
            this.btSalir.Text = "Salir";
            this.btSalir.UseVisualStyleBackColor = true;
            this.btSalir.Click += new System.EventHandler(this.btSalir_Click);
            // 
            // pnlHead
            // 
            this.pnlHead.Controls.Add(this.btPaste);
            this.pnlHead.Controls.Add(this.btParse);
            this.pnlHead.Controls.Add(this.txtIndex);
            this.pnlHead.Controls.Add(this.btCreateIndex);
            this.pnlHead.Controls.Add(this.btSalir);
            this.pnlHead.Controls.Add(this.label1);
            this.pnlHead.Controls.Add(this.lsCurrentIndex);
            this.pnlHead.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHead.Location = new System.Drawing.Point(0, 0);
            this.pnlHead.Name = "pnlHead";
            this.pnlHead.Size = new System.Drawing.Size(989, 217);
            this.pnlHead.TabIndex = 14;
            // 
            // pnlBottom
            // 
            this.pnlBottom.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlBottom.Controls.Add(this.btGetIndexes);
            this.pnlBottom.Controls.Add(this.txtTableName);
            this.pnlBottom.Controls.Add(this.label3);
            this.pnlBottom.Controls.Add(this.label2);
            this.pnlBottom.Controls.Add(this.lsExistingIndexes);
            this.pnlBottom.Controls.Add(this.laStatus);
            this.pnlBottom.Location = new System.Drawing.Point(0, 218);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(989, 398);
            this.pnlBottom.TabIndex = 15;
            // 
            // frmIndexes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btSalir;
            this.ClientSize = new System.Drawing.Size(989, 616);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.pnlHead);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "frmIndexes";
            this.Text = "Indexes Analysis";
            this.pnlHead.ResumeLayout(false);
            this.pnlHead.PerformLayout();
            this.pnlBottom.ResumeLayout(false);
            this.pnlBottom.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox txtIndex;
        private System.Windows.Forms.Button btParse;
        private System.Windows.Forms.Button btPaste;
        private System.Windows.Forms.Label laStatus;
        private System.Windows.Forms.ListBox lsCurrentIndex;
        private System.Windows.Forms.ListBox lsExistingIndexes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btCreateIndex;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTableName;
        private System.Windows.Forms.Button btGetIndexes;
        private System.Windows.Forms.Button btSalir;
        private System.Windows.Forms.Panel pnlHead;
        private System.Windows.Forms.Panel pnlBottom;
    }
}