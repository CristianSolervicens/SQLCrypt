namespace SQLCrypt
{
    partial class frmDespliegueTxt
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDespliegueTxt));
            this.rtchSalida = new System.Windows.Forms.RichTextBox();
            this.btSalir = new System.Windows.Forms.Button();
            this.txFind = new System.Windows.Forms.TextBox();
            this.btFind = new System.Windows.Forms.Button();
            this.btCancelar = new System.Windows.Forms.Button();
            this.laInfo = new System.Windows.Forms.Label();
            this.btGrabar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rtchSalida
            // 
            this.rtchSalida.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.rtchSalida.Font = new System.Drawing.Font("Courier New", 10F);
            this.rtchSalida.Location = new System.Drawing.Point(0, 29);
            this.rtchSalida.Name = "rtchSalida";
            this.rtchSalida.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
            this.rtchSalida.Size = new System.Drawing.Size(1180, 619);
            this.rtchSalida.TabIndex = 0;
            this.rtchSalida.Text = "";
            this.rtchSalida.WordWrap = false;
            this.rtchSalida.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rtchSalida_KeyDown);
            this.rtchSalida.KeyUp += new System.Windows.Forms.KeyEventHandler(this.rtchSalida_KeyUp);
            // 
            // btSalir
            // 
            this.btSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btSalir.Location = new System.Drawing.Point(185, 3);
            this.btSalir.Name = "btSalir";
            this.btSalir.Size = new System.Drawing.Size(41, 23);
            this.btSalir.TabIndex = 16;
            this.btSalir.Text = "Salir";
            this.btSalir.UseVisualStyleBackColor = true;
            this.btSalir.Click += new System.EventHandler(this.btSalir_Click);
            // 
            // txFind
            // 
            this.txFind.Location = new System.Drawing.Point(307, 5);
            this.txFind.Name = "txFind";
            this.txFind.Size = new System.Drawing.Size(198, 20);
            this.txFind.TabIndex = 18;
            this.txFind.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txFind_KeyUp);
            // 
            // btFind
            // 
            this.btFind.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btFind.Image = ((System.Drawing.Image)(resources.GetObject("btFind.Image")));
            this.btFind.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btFind.Location = new System.Drawing.Point(238, 4);
            this.btFind.Name = "btFind";
            this.btFind.Size = new System.Drawing.Size(65, 23);
            this.btFind.TabIndex = 17;
            this.btFind.Text = "&Buscar";
            this.btFind.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btFind.UseVisualStyleBackColor = true;
            this.btFind.Click += new System.EventHandler(this.btFind_Click);
            // 
            // btCancelar
            // 
            this.btCancelar.Location = new System.Drawing.Point(98, 3);
            this.btCancelar.Name = "btCancelar";
            this.btCancelar.Size = new System.Drawing.Size(75, 23);
            this.btCancelar.TabIndex = 19;
            this.btCancelar.Text = "Cancelar";
            this.btCancelar.UseVisualStyleBackColor = true;
            this.btCancelar.Click += new System.EventHandler(this.btCancelar_Click);
            // 
            // laInfo
            // 
            this.laInfo.AutoSize = true;
            this.laInfo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.laInfo.Location = new System.Drawing.Point(562, 8);
            this.laInfo.Name = "laInfo";
            this.laInfo.Size = new System.Drawing.Size(33, 13);
            this.laInfo.TabIndex = 20;
            this.laInfo.Text = "Label";
            // 
            // btGrabar
            // 
            this.btGrabar.Location = new System.Drawing.Point(4, 3);
            this.btGrabar.Name = "btGrabar";
            this.btGrabar.Size = new System.Drawing.Size(89, 23);
            this.btGrabar.TabIndex = 21;
            this.btGrabar.Text = "Grabar Archivo";
            this.btGrabar.UseVisualStyleBackColor = true;
            this.btGrabar.Click += new System.EventHandler(this.btGrabar_Click);
            // 
            // frmDespliegueTxt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btSalir;
            this.ClientSize = new System.Drawing.Size(1180, 648);
            this.Controls.Add(this.btGrabar);
            this.Controls.Add(this.laInfo);
            this.Controls.Add(this.btCancelar);
            this.Controls.Add(this.txFind);
            this.Controls.Add(this.btFind);
            this.Controls.Add(this.btSalir);
            this.Controls.Add(this.rtchSalida);
            this.Name = "frmDespliegueTxt";
            this.Text = "Despliegue";
            this.Resize += new System.EventHandler(this.frmDespliegueTxt_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtchSalida;
        private System.Windows.Forms.Button btSalir;
        private System.Windows.Forms.TextBox txFind;
        private System.Windows.Forms.Button btFind;
        private System.Windows.Forms.Button btCancelar;
        private System.Windows.Forms.Label laInfo;
        private System.Windows.Forms.Button btGrabar;
    }
}