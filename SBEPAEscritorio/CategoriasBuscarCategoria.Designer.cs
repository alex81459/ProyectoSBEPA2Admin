﻿namespace SBEPAEscritorio
{
    partial class CategoriasBuscarCategoria
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CategoriasBuscarCategoria));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.Barra = new System.Windows.Forms.Panel();
            this.pictureBox20 = new System.Windows.Forms.PictureBox();
            this.btnCerrar = new System.Windows.Forms.PictureBox();
            this.label7 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.txtBuscarEn = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbBuscarEn = new System.Windows.Forms.ComboBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label15 = new System.Windows.Forms.Label();
            this.pbBuscarTienda = new System.Windows.Forms.PictureBox();
            this.dgbCategorias = new System.Windows.Forms.DataGridView();
            this.idcategoria = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Barra.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox20)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCerrar)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBuscarTienda)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgbCategorias)).BeginInit();
            this.SuspendLayout();
            // 
            // Barra
            // 
            this.Barra.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Barra.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Barra.Controls.Add(this.pictureBox20);
            this.Barra.Controls.Add(this.btnCerrar);
            this.Barra.Controls.Add(this.label7);
            this.Barra.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.Barra.Dock = System.Windows.Forms.DockStyle.Top;
            this.Barra.Location = new System.Drawing.Point(0, 0);
            this.Barra.Name = "Barra";
            this.Barra.Size = new System.Drawing.Size(577, 22);
            this.Barra.TabIndex = 119;
            // 
            // pictureBox20
            // 
            this.pictureBox20.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.pictureBox20.Image = global::SBEPAEscritorio.Properties.Resources.LogoPequeño;
            this.pictureBox20.Location = new System.Drawing.Point(1, -1);
            this.pictureBox20.Name = "pictureBox20";
            this.pictureBox20.Size = new System.Drawing.Size(21, 21);
            this.pictureBox20.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox20.TabIndex = 25;
            this.pictureBox20.TabStop = false;
            // 
            // btnCerrar
            // 
            this.btnCerrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrar.Image = global::SBEPAEscritorio.Properties.Resources.icons8_cerrar_ventana_48;
            this.btnCerrar.Location = new System.Drawing.Point(556, 0);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(20, 20);
            this.btnCerrar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnCerrar.TabIndex = 23;
            this.btnCerrar.TabStop = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(127, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(309, 18);
            this.label7.TabIndex = 22;
            this.label7.Text = "Categorias Productos Buscar Categoria";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Controls.Add(this.pbBuscarTienda);
            this.panel1.Controls.Add(this.dgbCategorias);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 22);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(577, 409);
            this.panel1.TabIndex = 120;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.label8);
            this.panel4.Controls.Add(this.txtBuscarEn);
            this.panel4.Controls.Add(this.label9);
            this.panel4.Controls.Add(this.cmbBuscarEn);
            this.panel4.Controls.Add(this.pictureBox1);
            this.panel4.Location = new System.Drawing.Point(11, 7);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(548, 53);
            this.panel4.TabIndex = 120;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(316, 7);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(108, 13);
            this.label8.TabIndex = 64;
            this.label8.Text = "Paremetros a Buscar;";
            // 
            // txtBuscarEn
            // 
            this.txtBuscarEn.Location = new System.Drawing.Point(292, 23);
            this.txtBuscarEn.Name = "txtBuscarEn";
            this.txtBuscarEn.Size = new System.Drawing.Size(157, 20);
            this.txtBuscarEn.TabIndex = 63;
            this.txtBuscarEn.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBuscarEn_KeyPress);
            this.txtBuscarEn.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtBuscarEn_KeyUp);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(127, 7);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(62, 13);
            this.label9.TabIndex = 61;
            this.label9.Text = "Buscar Por:";
            // 
            // cmbBuscarEn
            // 
            this.cmbBuscarEn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cmbBuscarEn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmbBuscarEn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBuscarEn.ForeColor = System.Drawing.Color.White;
            this.cmbBuscarEn.FormattingEnabled = true;
            this.cmbBuscarEn.Items.AddRange(new object[] {
            "idcategoria",
            "nombre"});
            this.cmbBuscarEn.Location = new System.Drawing.Point(97, 23);
            this.cmbBuscarEn.Name = "cmbBuscarEn";
            this.cmbBuscarEn.Size = new System.Drawing.Size(121, 21);
            this.cmbBuscarEn.TabIndex = 62;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(5, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(46, 46);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 56;
            this.pictureBox1.TabStop = false;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.White;
            this.label15.Location = new System.Drawing.Point(33, 68);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(150, 12);
            this.label15.TabIndex = 118;
            this.label15.Text = "*Seleccione Categoria que asignara";
            // 
            // pbBuscarTienda
            // 
            this.pbBuscarTienda.Image = global::SBEPAEscritorio.Properties.Resources.info;
            this.pbBuscarTienda.Location = new System.Drawing.Point(11, 63);
            this.pbBuscarTienda.Name = "pbBuscarTienda";
            this.pbBuscarTienda.Size = new System.Drawing.Size(20, 20);
            this.pbBuscarTienda.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbBuscarTienda.TabIndex = 121;
            this.pbBuscarTienda.TabStop = false;
            // 
            // dgbCategorias
            // 
            this.dgbCategorias.AllowUserToAddRows = false;
            this.dgbCategorias.AllowUserToDeleteRows = false;
            this.dgbCategorias.AllowUserToResizeRows = false;
            this.dgbCategorias.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dgbCategorias.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(117)))), ((int)(((byte)(208)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Tai Le", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgbCategorias.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgbCategorias.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgbCategorias.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idcategoria,
            this.nombre});
            this.dgbCategorias.EnableHeadersVisualStyles = false;
            this.dgbCategorias.Location = new System.Drawing.Point(11, 88);
            this.dgbCategorias.Name = "dgbCategorias";
            this.dgbCategorias.ReadOnly = true;
            this.dgbCategorias.RowHeadersVisible = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Cyan;
            this.dgbCategorias.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgbCategorias.Size = new System.Drawing.Size(547, 308);
            this.dgbCategorias.TabIndex = 119;
            this.dgbCategorias.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgbTiendas_CellContentDoubleClick);
            // 
            // idcategoria
            // 
            this.idcategoria.DataPropertyName = "idcategoria";
            this.idcategoria.HeaderText = "idcategoria";
            this.idcategoria.Name = "idcategoria";
            this.idcategoria.ReadOnly = true;
            // 
            // nombre
            // 
            this.nombre.DataPropertyName = "nombre";
            this.nombre.HeaderText = "nombre";
            this.nombre.Name = "nombre";
            this.nombre.ReadOnly = true;
            this.nombre.Width = 300;
            // 
            // CategoriaBuscarCategoria
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.ClientSize = new System.Drawing.Size(577, 431);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Barra);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CategoriaBuscarCategoria";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Categorias Buscar Categoria";
            this.Load += new System.EventHandler(this.CategoriaBuscarCategoria_Load);
            this.Barra.ResumeLayout(false);
            this.Barra.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox20)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCerrar)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBuscarTienda)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgbCategorias)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Barra;
        private System.Windows.Forms.PictureBox pictureBox20;
        private System.Windows.Forms.PictureBox btnCerrar;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtBuscarEn;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmbBuscarEn;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.PictureBox pbBuscarTienda;
        private System.Windows.Forms.DataGridView dgbCategorias;
        private System.Windows.Forms.DataGridViewTextBoxColumn idcategoria;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombre;
    }
}