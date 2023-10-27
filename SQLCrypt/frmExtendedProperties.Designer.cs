namespace SQLCrypt
{
    partial class frmExtendedProperties
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmExtendedProperties));
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.dbObjs = new System.Windows.Forms.TreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.txtObjDescription = new System.Windows.Forms.TextBox();
            this.laObjDescription = new System.Windows.Forms.Label();
            this.btGrabar = new System.Windows.Forms.Button();
            this.panelDisplay = new System.Windows.Forms.Panel();
            this.dgObject = new System.Windows.Forms.DataGridView();
            this.btSalir = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.panelDisplay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgObject)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.dbObjs);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.label1);
            this.splitContainer.Panel2.Controls.Add(this.txtObjDescription);
            this.splitContainer.Panel2.Controls.Add(this.laObjDescription);
            this.splitContainer.Panel2.Controls.Add(this.btGrabar);
            this.splitContainer.Panel2.Controls.Add(this.panelDisplay);
            this.splitContainer.Panel2.Controls.Add(this.btSalir);
            this.splitContainer.Size = new System.Drawing.Size(1889, 944);
            this.splitContainer.SplitterDistance = 363;
            this.splitContainer.TabIndex = 0;
            // 
            // dbObjs
            // 
            this.dbObjs.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dbObjs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dbObjs.Location = new System.Drawing.Point(0, 0);
            this.dbObjs.Name = "dbObjs";
            this.dbObjs.Size = new System.Drawing.Size(363, 944);
            this.dbObjs.TabIndex = 0;
            this.dbObjs.DoubleClick += new System.EventHandler(this.dbObjs_DoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(304, 86);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "Descripción";
            // 
            // txtObjDescription
            // 
            this.txtObjDescription.Location = new System.Drawing.Point(410, 12);
            this.txtObjDescription.Multiline = true;
            this.txtObjDescription.Name = "txtObjDescription";
            this.txtObjDescription.Size = new System.Drawing.Size(1100, 102);
            this.txtObjDescription.TabIndex = 1;
            // 
            // laObjDescription
            // 
            this.laObjDescription.AutoSize = true;
            this.laObjDescription.Location = new System.Drawing.Point(17, 21);
            this.laObjDescription.Name = "laObjDescription";
            this.laObjDescription.Size = new System.Drawing.Size(56, 20);
            this.laObjDescription.TabIndex = 4;
            this.laObjDescription.Text = "Objeto";
            // 
            // btGrabar
            // 
            this.btGrabar.Location = new System.Drawing.Point(100, 62);
            this.btGrabar.Name = "btGrabar";
            this.btGrabar.Size = new System.Drawing.Size(89, 47);
            this.btGrabar.TabIndex = 3;
            this.btGrabar.Text = "Grabar";
            this.btGrabar.UseVisualStyleBackColor = true;
            this.btGrabar.Click += new System.EventHandler(this.btGrabar_Click);
            // 
            // panelDisplay
            // 
            this.panelDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelDisplay.Controls.Add(this.dgObject);
            this.panelDisplay.Location = new System.Drawing.Point(3, 120);
            this.panelDisplay.Name = "panelDisplay";
            this.panelDisplay.Padding = new System.Windows.Forms.Padding(0, 100, 0, 0);
            this.panelDisplay.Size = new System.Drawing.Size(1519, 796);
            this.panelDisplay.TabIndex = 2;
            // 
            // dgObject
            // 
            this.dgObject.AllowUserToAddRows = false;
            this.dgObject.AllowUserToDeleteRows = false;
            this.dgObject.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgObject.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgObject.Location = new System.Drawing.Point(0, 0);
            this.dgObject.Name = "dgObject";
            this.dgObject.RowHeadersWidth = 62;
            this.dgObject.RowTemplate.Height = 28;
            this.dgObject.Size = new System.Drawing.Size(1519, 796);
            this.dgObject.TabIndex = 2;
            // 
            // btSalir
            // 
            this.btSalir.AutoEllipsis = true;
            this.btSalir.Location = new System.Drawing.Point(8, 61);
            this.btSalir.Name = "btSalir";
            this.btSalir.Size = new System.Drawing.Size(89, 47);
            this.btSalir.TabIndex = 4;
            this.btSalir.Text = "Salir";
            this.btSalir.UseVisualStyleBackColor = true;
            this.btSalir.Click += new System.EventHandler(this.btSalir_Click);
            // 
            // frmExtendedProperties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1889, 944);
            this.Controls.Add(this.splitContainer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmExtendedProperties";
            this.Text = "Editor de Extended Properties";
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.panelDisplay.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgObject)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.TreeView dbObjs;
        private System.Windows.Forms.Button btSalir;
        private System.Windows.Forms.Panel panelDisplay;
        private System.Windows.Forms.DataGridView dgObject;
        private System.Windows.Forms.Button btGrabar;
        private System.Windows.Forms.TextBox txtObjDescription;
        private System.Windows.Forms.Label laObjDescription;
        private System.Windows.Forms.Label label1;
    }
}