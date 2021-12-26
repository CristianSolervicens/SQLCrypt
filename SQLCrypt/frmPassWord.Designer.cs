namespace SQLCrypt
{
    partial class frmPassWord
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPassWord));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.txClaveEncriptada = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txClaveClara = new System.Windows.Forms.TextBox();
            this.btSalir = new System.Windows.Forms.Button();
            this.btOk = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txClaveEncriptada);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txClaveClara);
            this.panel1.Location = new System.Drawing.Point(5, 13);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(312, 104);
            this.panel1.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(200, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Clave a Encriptada (Queda en Clipboard)";
            // 
            // txClaveEncriptada
            // 
            this.txClaveEncriptada.Location = new System.Drawing.Point(7, 70);
            this.txClaveEncriptada.Name = "txClaveEncriptada";
            this.txClaveEncriptada.ReadOnly = true;
            this.txClaveEncriptada.Size = new System.Drawing.Size(296, 20);
            this.txClaveEncriptada.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Clave a Encriptar";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // txClaveClara
            // 
            this.txClaveClara.Location = new System.Drawing.Point(7, 24);
            this.txClaveClara.Name = "txClaveClara";
            this.txClaveClara.Size = new System.Drawing.Size(296, 20);
            this.txClaveClara.TabIndex = 4;
            // 
            // btSalir
            // 
            this.btSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btSalir.Image = ((System.Drawing.Image)(resources.GetObject("btSalir.Image")));
            this.btSalir.Location = new System.Drawing.Point(233, 124);
            this.btSalir.Name = "btSalir";
            this.btSalir.Size = new System.Drawing.Size(58, 28);
            this.btSalir.TabIndex = 9;
            this.btSalir.UseVisualStyleBackColor = true;
            this.btSalir.Click += new System.EventHandler(this.btSalir_Click);
            // 
            // btOk
            // 
            this.btOk.Image = ((System.Drawing.Image)(resources.GetObject("btOk.Image")));
            this.btOk.Location = new System.Drawing.Point(30, 124);
            this.btOk.Name = "btOk";
            this.btOk.Size = new System.Drawing.Size(58, 28);
            this.btOk.TabIndex = 8;
            this.btOk.UseVisualStyleBackColor = true;
            this.btOk.Click += new System.EventHandler(this.btOk_Click);
            // 
            // frmPassWord
            // 
            this.AcceptButton = this.btOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btSalir;
            this.ClientSize = new System.Drawing.Size(324, 160);
            this.Controls.Add(this.btSalir);
            this.Controls.Add(this.btOk);
            this.Controls.Add(this.panel1);
            this.Name = "frmPassWord";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Encriptación de Claves";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txClaveEncriptada;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txClaveClara;
        private System.Windows.Forms.Button btSalir;
        private System.Windows.Forms.Button btOk;
    }
}