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
            this.label5 = new System.Windows.Forms.Label();
            this.btUpdate = new System.Windows.Forms.Button();
            this.chkSavePasswd = new System.Windows.Forms.CheckBox();
            this.btNew = new System.Windows.Forms.Button();
            this.cbDescripcion = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 55);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Server";
            // 
            // txServer
            // 
            this.txServer.Location = new System.Drawing.Point(129, 51);
            this.txServer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txServer.Name = "txServer";
            this.txServer.Size = new System.Drawing.Size(336, 26);
            this.txServer.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 95);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Data Base";
            // 
            // txUser
            // 
            this.txUser.Location = new System.Drawing.Point(129, 131);
            this.txUser.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txUser.Name = "txUser";
            this.txUser.Size = new System.Drawing.Size(336, 26);
            this.txUser.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 135);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "User";
            // 
            // txPass
            // 
            this.txPass.Location = new System.Drawing.Point(129, 171);
            this.txPass.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txPass.Name = "txPass";
            this.txPass.PasswordChar = '#';
            this.txPass.Size = new System.Drawing.Size(336, 26);
            this.txPass.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 175);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "Password";
            // 
            // btCancelar
            // 
            this.btCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btCancelar.Image")));
            this.btCancelar.Location = new System.Drawing.Point(388, 253);
            this.btCancelar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btCancelar.Name = "btCancelar";
            this.btCancelar.Size = new System.Drawing.Size(71, 54);
            this.btCancelar.TabIndex = 11;
            this.btCancelar.UseVisualStyleBackColor = true;
            this.btCancelar.Click += new System.EventHandler(this.btCancelar_Click);
            // 
            // btAceptar
            // 
            this.btAceptar.Image = ((System.Drawing.Image)(resources.GetObject("btAceptar.Image")));
            this.btAceptar.Location = new System.Drawing.Point(18, 253);
            this.btAceptar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btAceptar.Name = "btAceptar";
            this.btAceptar.Size = new System.Drawing.Size(71, 54);
            this.btAceptar.TabIndex = 6;
            this.btAceptar.UseVisualStyleBackColor = true;
            this.btAceptar.Click += new System.EventHandler(this.btAceptar_Click);
            // 
            // pbTest
            // 
            this.pbTest.Image = ((System.Drawing.Image)(resources.GetObject("pbTest.Image")));
            this.pbTest.Location = new System.Drawing.Point(240, 253);
            this.pbTest.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pbTest.Name = "pbTest";
            this.pbTest.Size = new System.Drawing.Size(71, 54);
            this.pbTest.TabIndex = 9;
            this.pbTest.UseVisualStyleBackColor = true;
            this.pbTest.Click += new System.EventHandler(this.pbTest_Click);
            // 
            // cbBases
            // 
            this.cbBases.FormattingEnabled = true;
            this.cbBases.Location = new System.Drawing.Point(129, 89);
            this.cbBases.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbBases.Name = "cbBases";
            this.cbBases.Size = new System.Drawing.Size(336, 28);
            this.cbBases.TabIndex = 2;
            // 
            // btDelConexion
            // 
            this.btDelConexion.Image = ((System.Drawing.Image)(resources.GetObject("btDelConexion.Image")));
            this.btDelConexion.Location = new System.Drawing.Point(314, 253);
            this.btDelConexion.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btDelConexion.Name = "btDelConexion";
            this.btDelConexion.Size = new System.Drawing.Size(71, 54);
            this.btDelConexion.TabIndex = 10;
            this.btDelConexion.UseVisualStyleBackColor = true;
            this.btDelConexion.Click += new System.EventHandler(this.btDelConexion_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 15);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(109, 20);
            this.label5.TabIndex = 13;
            this.label5.Text = "Description (*)";
            // 
            // btUpdate
            // 
            this.btUpdate.Image = ((System.Drawing.Image)(resources.GetObject("btUpdate.Image")));
            this.btUpdate.Location = new System.Drawing.Point(166, 253);
            this.btUpdate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btUpdate.Name = "btUpdate";
            this.btUpdate.Size = new System.Drawing.Size(71, 54);
            this.btUpdate.TabIndex = 8;
            this.btUpdate.UseVisualStyleBackColor = true;
            this.btUpdate.Click += new System.EventHandler(this.btUpdate_Click);
            // 
            // chkSavePasswd
            // 
            this.chkSavePasswd.AutoSize = true;
            this.chkSavePasswd.Location = new System.Drawing.Point(130, 212);
            this.chkSavePasswd.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkSavePasswd.Name = "chkSavePasswd";
            this.chkSavePasswd.Size = new System.Drawing.Size(144, 24);
            this.chkSavePasswd.TabIndex = 5;
            this.chkSavePasswd.Text = "Save Password";
            this.chkSavePasswd.UseVisualStyleBackColor = true;
            // 
            // btNew
            // 
            this.btNew.Image = ((System.Drawing.Image)(resources.GetObject("btNew.Image")));
            this.btNew.Location = new System.Drawing.Point(92, 253);
            this.btNew.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btNew.Name = "btNew";
            this.btNew.Size = new System.Drawing.Size(71, 54);
            this.btNew.TabIndex = 7;
            this.btNew.UseVisualStyleBackColor = true;
            this.btNew.Click += new System.EventHandler(this.btNew_Click);
            // 
            // cbDescripcion
            // 
            this.cbDescripcion.FormattingEnabled = true;
            this.cbDescripcion.Location = new System.Drawing.Point(129, 11);
            this.cbDescripcion.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbDescripcion.Name = "cbDescripcion";
            this.cbDescripcion.Size = new System.Drawing.Size(336, 28);
            this.cbDescripcion.TabIndex = 0;
            this.cbDescripcion.SelectedIndexChanged += new System.EventHandler(this.cbDescripcion_SelectedIndexChanged);
            // 
            // frmConexion
            // 
            this.AcceptButton = this.btAceptar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btCancelar;
            this.ClientSize = new System.Drawing.Size(488, 320);
            this.Controls.Add(this.cbDescripcion);
            this.Controls.Add(this.btNew);
            this.Controls.Add(this.chkSavePasswd);
            this.Controls.Add(this.btUpdate);
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
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmConexion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Conexión a Base de Datos";
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
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btUpdate;
        private System.Windows.Forms.CheckBox chkSavePasswd;
        private System.Windows.Forms.Button btNew;
        private System.Windows.Forms.ComboBox cbDescripcion;
    }
}