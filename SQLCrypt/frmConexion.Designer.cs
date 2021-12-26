namespace SQLCrypt
{
    partial class frmConexion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmConexion));
            this.label1 = new System.Windows.Forms.Label();
            this.txServer = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txUser = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txPass = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btCancelar = new System.Windows.Forms.Button();
            this.btAceptar = new System.Windows.Forms.Button();
            this.pbTest = new System.Windows.Forms.Button();
            this.cbBases = new System.Windows.Forms.ComboBox();
            this.btDelConexion = new System.Windows.Forms.Button();
            this.txDescripcion = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btUpdate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Servidor";
            // 
            // txServer
            // 
            this.txServer.Location = new System.Drawing.Point(82, 33);
            this.txServer.Name = "txServer";
            this.txServer.Size = new System.Drawing.Size(225, 20);
            this.txServer.TabIndex = 1;
            this.txServer.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txServer_KeyUp);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Base";
            // 
            // txUser
            // 
            this.txUser.Location = new System.Drawing.Point(82, 85);
            this.txUser.Name = "txUser";
            this.txUser.Size = new System.Drawing.Size(225, 20);
            this.txUser.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Usuario";
            // 
            // txPass
            // 
            this.txPass.Location = new System.Drawing.Point(82, 111);
            this.txPass.Name = "txPass";
            this.txPass.PasswordChar = '#';
            this.txPass.Size = new System.Drawing.Size(225, 20);
            this.txPass.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 114);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Clave";
            // 
            // btCancelar
            // 
            this.btCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btCancelar.Image")));
            this.btCancelar.Location = new System.Drawing.Point(253, 145);
            this.btCancelar.Name = "btCancelar";
            this.btCancelar.Size = new System.Drawing.Size(44, 30);
            this.btCancelar.TabIndex = 9;
            this.btCancelar.UseVisualStyleBackColor = true;
            this.btCancelar.Click += new System.EventHandler(this.btCancelar_Click);
            // 
            // btAceptar
            // 
            this.btAceptar.Image = ((System.Drawing.Image)(resources.GetObject("btAceptar.Image")));
            this.btAceptar.Location = new System.Drawing.Point(21, 145);
            this.btAceptar.Name = "btAceptar";
            this.btAceptar.Size = new System.Drawing.Size(44, 30);
            this.btAceptar.TabIndex = 5;
            this.btAceptar.UseVisualStyleBackColor = true;
            this.btAceptar.Click += new System.EventHandler(this.btAceptar_Click);
            // 
            // pbTest
            // 
            this.pbTest.Image = ((System.Drawing.Image)(resources.GetObject("pbTest.Image")));
            this.pbTest.Location = new System.Drawing.Point(137, 145);
            this.pbTest.Name = "pbTest";
            this.pbTest.Size = new System.Drawing.Size(44, 30);
            this.pbTest.TabIndex = 7;
            this.pbTest.UseVisualStyleBackColor = true;
            this.pbTest.Click += new System.EventHandler(this.pbTest_Click);
            // 
            // cbBases
            // 
            this.cbBases.FormattingEnabled = true;
            this.cbBases.Location = new System.Drawing.Point(82, 58);
            this.cbBases.Name = "cbBases";
            this.cbBases.Size = new System.Drawing.Size(225, 21);
            this.cbBases.TabIndex = 2;
            // 
            // btDelConexion
            // 
            this.btDelConexion.Image = ((System.Drawing.Image)(resources.GetObject("btDelConexion.Image")));
            this.btDelConexion.Location = new System.Drawing.Point(195, 145);
            this.btDelConexion.Name = "btDelConexion";
            this.btDelConexion.Size = new System.Drawing.Size(44, 30);
            this.btDelConexion.TabIndex = 8;
            this.btDelConexion.UseVisualStyleBackColor = true;
            this.btDelConexion.Click += new System.EventHandler(this.btDelConexion_Click);
            // 
            // txDescripcion
            // 
            this.txDescripcion.Location = new System.Drawing.Point(82, 7);
            this.txDescripcion.Name = "txDescripcion";
            this.txDescripcion.Size = new System.Drawing.Size(225, 20);
            this.txDescripcion.TabIndex = 0;
            this.txDescripcion.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txDescripcion_KeyUp);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Descripción";
            // 
            // btUpdate
            // 
            this.btUpdate.Image = ((System.Drawing.Image)(resources.GetObject("btUpdate.Image")));
            this.btUpdate.Location = new System.Drawing.Point(79, 145);
            this.btUpdate.Name = "btUpdate";
            this.btUpdate.Size = new System.Drawing.Size(44, 30);
            this.btUpdate.TabIndex = 6;
            this.btUpdate.UseVisualStyleBackColor = true;
            this.btUpdate.Click += new System.EventHandler(this.btUpdate_Click);
            // 
            // frmConexion
            // 
            this.AcceptButton = this.btAceptar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btCancelar;
            this.ClientSize = new System.Drawing.Size(319, 184);
            this.Controls.Add(this.btUpdate);
            this.Controls.Add(this.txDescripcion);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btDelConexion);
            this.Controls.Add(this.cbBases);
            this.Controls.Add(this.pbTest);
            this.Controls.Add(this.btCancelar);
            this.Controls.Add(this.btAceptar);
            this.Controls.Add(this.txPass);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txUser);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txServer);
            this.Controls.Add(this.label1);
            this.Name = "frmConexion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Conexión a Base de Datos";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmConexion_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txServer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txUser;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txPass;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btCancelar;
        private System.Windows.Forms.Button btAceptar;
        private System.Windows.Forms.Button pbTest;
        private System.Windows.Forms.ComboBox cbBases;
        private System.Windows.Forms.Button btDelConexion;
        private System.Windows.Forms.TextBox txDescripcion;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btUpdate;
    }
}