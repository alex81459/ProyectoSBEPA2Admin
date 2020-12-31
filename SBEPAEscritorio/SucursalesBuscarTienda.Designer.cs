namespace SBEPAEscritorio
{
    partial class SucursalesBuscarTienda
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SucursalesBuscarTienda));
            this.label15 = new System.Windows.Forms.Label();
            this.dgbTiendas = new System.Windows.Forms.DataGridView();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.txtBuscarEn = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbBuscarEn = new System.Windows.Forms.ComboBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pbBuscarTienda = new System.Windows.Forms.PictureBox();
            this.Barra = new System.Windows.Forms.Panel();
            this.btnMinimizar = new System.Windows.Forms.PictureBox();
            this.pictureBox20 = new System.Windows.Forms.PictureBox();
            this.btnCerrar = new System.Windows.Forms.PictureBox();
            this.label7 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Idtienda = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdUsuario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.informacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Archivo_logo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CantidadVisita = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgbTiendas)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBuscarTienda)).BeginInit();
            this.Barra.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnMinimizar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox20)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCerrar)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.White;
            this.label15.Location = new System.Drawing.Point(29, 66);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(239, 12);
            this.label15.TabIndex = 66;
            this.label15.Text = "*Seleccione la tienda a la que estara vinculada la sucursal";
            // 
            // dgbTiendas
            // 
            this.dgbTiendas.AllowUserToAddRows = false;
            this.dgbTiendas.AllowUserToDeleteRows = false;
            this.dgbTiendas.AllowUserToResizeRows = false;
            this.dgbTiendas.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dgbTiendas.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(117)))), ((int)(((byte)(208)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Tai Le", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgbTiendas.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgbTiendas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgbTiendas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Idtienda,
            this.IdUsuario,
            this.nombre,
            this.informacion,
            this.Archivo_logo,
            this.CantidadVisita});
            this.dgbTiendas.EnableHeadersVisualStyles = false;
            this.dgbTiendas.Location = new System.Drawing.Point(7, 86);
            this.dgbTiendas.Name = "dgbTiendas";
            this.dgbTiendas.ReadOnly = true;
            this.dgbTiendas.RowHeadersVisible = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Cyan;
            this.dgbTiendas.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgbTiendas.Size = new System.Drawing.Size(547, 298);
            this.dgbTiendas.TabIndex = 70;
            this.dgbTiendas.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgbTiendas_CellDoubleClick);
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
            this.panel4.Location = new System.Drawing.Point(7, 5);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(548, 53);
            this.panel4.TabIndex = 71;
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
            "Idtienda",
            "IdUsuario",
            "nombre",
            "informacion"});
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
            // pbBuscarTienda
            // 
            this.pbBuscarTienda.Image = global::SBEPAEscritorio.Properties.Resources.info;
            this.pbBuscarTienda.Location = new System.Drawing.Point(7, 61);
            this.pbBuscarTienda.Name = "pbBuscarTienda";
            this.pbBuscarTienda.Size = new System.Drawing.Size(20, 20);
            this.pbBuscarTienda.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbBuscarTienda.TabIndex = 117;
            this.pbBuscarTienda.TabStop = false;
            // 
            // Barra
            // 
            this.Barra.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Barra.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Barra.Controls.Add(this.btnMinimizar);
            this.Barra.Controls.Add(this.pictureBox20);
            this.Barra.Controls.Add(this.btnCerrar);
            this.Barra.Controls.Add(this.label7);
            this.Barra.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.Barra.Dock = System.Windows.Forms.DockStyle.Top;
            this.Barra.Location = new System.Drawing.Point(0, 0);
            this.Barra.Name = "Barra";
            this.Barra.Size = new System.Drawing.Size(562, 22);
            this.Barra.TabIndex = 118;
            this.Barra.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Barra_MouseDown);
            this.Barra.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Barra_MouseMove);
            this.Barra.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Barra_MouseUp);
            // 
            // btnMinimizar
            // 
            this.btnMinimizar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMinimizar.Image = global::SBEPAEscritorio.Properties.Resources.icons8_minimizar_la_ventana_48;
            this.btnMinimizar.Location = new System.Drawing.Point(520, -1);
            this.btnMinimizar.Name = "btnMinimizar";
            this.btnMinimizar.Size = new System.Drawing.Size(20, 20);
            this.btnMinimizar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnMinimizar.TabIndex = 26;
            this.btnMinimizar.TabStop = false;
            this.btnMinimizar.Click += new System.EventHandler(this.btnMinimizar_Click);
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
            this.btnCerrar.Location = new System.Drawing.Point(540, -1);
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
            this.label7.Location = new System.Drawing.Point(184, 1);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(205, 18);
            this.label7.TabIndex = 22;
            this.label7.Text = "Sucursales Buscar Tienda";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Controls.Add(this.pbBuscarTienda);
            this.panel1.Controls.Add(this.dgbTiendas);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 23);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(562, 391);
            this.panel1.TabIndex = 119;
            // 
            // Idtienda
            // 
            this.Idtienda.DataPropertyName = "Idtienda";
            this.Idtienda.HeaderText = "Idtienda";
            this.Idtienda.Name = "Idtienda";
            this.Idtienda.ReadOnly = true;
            this.Idtienda.Width = 80;
            // 
            // IdUsuario
            // 
            this.IdUsuario.DataPropertyName = "IdUsuario";
            this.IdUsuario.HeaderText = "IdUsuario";
            this.IdUsuario.Name = "IdUsuario";
            this.IdUsuario.ReadOnly = true;
            this.IdUsuario.Width = 80;
            // 
            // nombre
            // 
            this.nombre.DataPropertyName = "nombre";
            this.nombre.HeaderText = "nombre";
            this.nombre.Name = "nombre";
            this.nombre.ReadOnly = true;
            this.nombre.Width = 150;
            // 
            // informacion
            // 
            this.informacion.DataPropertyName = "informacion";
            this.informacion.HeaderText = "informacion";
            this.informacion.Name = "informacion";
            this.informacion.ReadOnly = true;
            this.informacion.Width = 400;
            // 
            // Archivo_logo
            // 
            this.Archivo_logo.DataPropertyName = "Archivo_logo";
            this.Archivo_logo.HeaderText = "Archivo_logo";
            this.Archivo_logo.Name = "Archivo_logo";
            this.Archivo_logo.ReadOnly = true;
            this.Archivo_logo.Visible = false;
            // 
            // CantidadVisita
            // 
            this.CantidadVisita.DataPropertyName = "CantidadVisita";
            this.CantidadVisita.HeaderText = "CantidadVisita";
            this.CantidadVisita.Name = "CantidadVisita";
            this.CantidadVisita.ReadOnly = true;
            this.CantidadVisita.Visible = false;
            // 
            // SucursalesBuscarTienda
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.ClientSize = new System.Drawing.Size(562, 414);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Barra);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SucursalesBuscarTienda";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SBEPA Sucursales - Buscar Tienda";
            this.Load += new System.EventHandler(this.SucursalesBuscarTienda_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgbTiendas)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBuscarTienda)).EndInit();
            this.Barra.ResumeLayout(false);
            this.Barra.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnMinimizar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox20)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCerrar)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.DataGridView dgbTiendas;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pbBuscarTienda;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtBuscarEn;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmbBuscarEn;
        private System.Windows.Forms.Panel Barra;
        private System.Windows.Forms.PictureBox btnMinimizar;
        private System.Windows.Forms.PictureBox pictureBox20;
        private System.Windows.Forms.PictureBox btnCerrar;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Idtienda;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdUsuario;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn informacion;
        private System.Windows.Forms.DataGridViewTextBoxColumn Archivo_logo;
        private System.Windows.Forms.DataGridViewTextBoxColumn CantidadVisita;
    }
}