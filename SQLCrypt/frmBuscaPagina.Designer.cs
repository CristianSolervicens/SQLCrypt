﻿namespace SQLCrypt
{
    partial class frmBuscaPagina
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btConsultar = new System.Windows.Forms.Button();
            this.txBaseId = new System.Windows.Forms.TextBox();
            this.txFileId = new System.Windows.Forms.TextBox();
            this.txPageID = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Base Id";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "File Id";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Page";
            // 
            // btConsultar
            // 
            this.btConsultar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btConsultar.Location = new System.Drawing.Point(79, 95);
            this.btConsultar.Name = "btConsultar";
            this.btConsultar.Size = new System.Drawing.Size(75, 23);
            this.btConsultar.TabIndex = 3;
            this.btConsultar.Text = "Find";
            this.btConsultar.UseVisualStyleBackColor = true;
            this.btConsultar.Click += new System.EventHandler(this.btConsultar_Click);
            // 
            // txBaseId
            // 
            this.txBaseId.Location = new System.Drawing.Point(79, 13);
            this.txBaseId.Name = "txBaseId";
            this.txBaseId.Size = new System.Drawing.Size(100, 20);
            this.txBaseId.TabIndex = 0;
            // 
            // txFileId
            // 
            this.txFileId.Location = new System.Drawing.Point(79, 35);
            this.txFileId.Name = "txFileId";
            this.txFileId.Size = new System.Drawing.Size(100, 20);
            this.txFileId.TabIndex = 1;
            // 
            // txPageID
            // 
            this.txPageID.Location = new System.Drawing.Point(79, 58);
            this.txPageID.Name = "txPageID";
            this.txPageID.Size = new System.Drawing.Size(100, 20);
            this.txPageID.TabIndex = 2;
            // 
            // frmBuscaPagina
            // 
            this.AcceptButton = this.btConsultar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(232, 130);
            this.Controls.Add(this.txBaseId);
            this.Controls.Add(this.txPageID);
            this.Controls.Add(this.txFileId);
            this.Controls.Add(this.btConsultar);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "frmBuscaPagina";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Busca Página";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btConsultar;
        private System.Windows.Forms.TextBox txBaseId;
        private System.Windows.Forms.TextBox txFileId;
        private System.Windows.Forms.TextBox txPageID;
    }
}