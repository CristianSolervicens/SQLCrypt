﻿namespace SQLCrypt
{
    partial class frmCommonTasks
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCommonTasks));
            this.lsTask = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btSalir = new System.Windows.Forms.Button();
            this.btOk = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lsTask
            // 
            this.lsTask.FormattingEnabled = true;
            this.lsTask.Location = new System.Drawing.Point(1, 0);
            this.lsTask.Name = "lsTask";
            this.lsTask.Size = new System.Drawing.Size(340, 485);
            this.lsTask.TabIndex = 0;
            this.lsTask.SelectedIndexChanged += new System.EventHandler(this.lsTask_SelectedIndexChanged);
            this.lsTask.DoubleClick += new System.EventHandler(this.lsTask_DoubleClick);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.btSalir);
            this.panel1.Controls.Add(this.btOk);
            this.panel1.Location = new System.Drawing.Point(6, 492);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(331, 37);
            this.panel1.TabIndex = 10;
            // 
            // btSalir
            // 
            this.btSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btSalir.Image = ((System.Drawing.Image)(resources.GetObject("btSalir.Image")));
            this.btSalir.Location = new System.Drawing.Point(273, 5);
            this.btSalir.Name = "btSalir";
            this.btSalir.Size = new System.Drawing.Size(50, 25);
            this.btSalir.TabIndex = 11;
            this.btSalir.UseVisualStyleBackColor = true;
            this.btSalir.Click += new System.EventHandler(this.btSalir_Click);
            // 
            // btOk
            // 
            this.btOk.Image = ((System.Drawing.Image)(resources.GetObject("btOk.Image")));
            this.btOk.Location = new System.Drawing.Point(5, 5);
            this.btOk.Name = "btOk";
            this.btOk.Size = new System.Drawing.Size(50, 25);
            this.btOk.TabIndex = 10;
            this.btOk.UseVisualStyleBackColor = true;
            this.btOk.Click += new System.EventHandler(this.btOk_Click);
            // 
            // frmCommonTasks
            // 
            this.AcceptButton = this.btOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btSalir;
            this.ClientSize = new System.Drawing.Size(342, 534);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lsTask);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCommonTasks";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmCommonTasks";
            this.Load += new System.EventHandler(this.frmCommonTasks_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lsTask;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btSalir;
        private System.Windows.Forms.Button btOk;
    }
}