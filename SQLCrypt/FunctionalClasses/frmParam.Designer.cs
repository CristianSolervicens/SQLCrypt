namespace SQLCrypt
{
   partial class frmParam
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmParam));
            this.panel = new System.Windows.Forms.Panel();
            this.btCancelar = new System.Windows.Forms.Button();
            this.btAceptar = new System.Windows.Forms.Button();
            this.panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel.Controls.Add(this.btCancelar);
            this.panel.Controls.Add(this.btAceptar);
            this.panel.Location = new System.Drawing.Point(3, 358);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(327, 35);
            this.panel.TabIndex = 2;
            // 
            // btCancelar
            // 
            this.btCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btCancelar.Image")));
            this.btCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btCancelar.Location = new System.Drawing.Point(242, 3);
            this.btCancelar.Name = "btCancelar";
            this.btCancelar.Size = new System.Drawing.Size(77, 26);
            this.btCancelar.TabIndex = 3;
            this.btCancelar.Text = "Cancelar";
            this.btCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btCancelar.UseVisualStyleBackColor = true;
            this.btCancelar.Click += new System.EventHandler(this.btCancelar_Click);
            // 
            // btAceptar
            // 
            this.btAceptar.Image = ((System.Drawing.Image)(resources.GetObject("btAceptar.Image")));
            this.btAceptar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btAceptar.Location = new System.Drawing.Point(4, 3);
            this.btAceptar.Name = "btAceptar";
            this.btAceptar.Size = new System.Drawing.Size(77, 26);
            this.btAceptar.TabIndex = 2;
            this.btAceptar.Text = "Aceptar";
            this.btAceptar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btAceptar.UseVisualStyleBackColor = true;
            this.btAceptar.Click += new System.EventHandler(this.btAceptar_Click);
            // 
            // frmParam
            // 
            this.AcceptButton = this.btAceptar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btCancelar;
            this.ClientSize = new System.Drawing.Size(333, 397);
            this.Controls.Add(this.panel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmParam";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Parámetros de Consulta";
            this.Activated += new System.EventHandler(this.frmParam_Activated);
            this.Load += new System.EventHandler(this.frmParam_Load);
            this.panel.ResumeLayout(false);
            this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.Panel panel;
      private System.Windows.Forms.Button btCancelar;
      private System.Windows.Forms.Button btAceptar;

   }
}