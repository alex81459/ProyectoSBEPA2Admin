namespace SBEPAEscritorio
{
    partial class ProductosFavoritos
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProductosFavoritos));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.Barra = new System.Windows.Forms.Panel();
            this.pictureBox20 = new System.Windows.Forms.PictureBox();
            this.label7 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtBuscarEn = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbBuscarEn = new System.Windows.Forms.ComboBox();
            this.dgbUsuarios = new System.Windows.Forms.DataGridView();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.picLupa = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dgbProductosFavoritos = new System.Windows.Forms.DataGridView();
            this.btnActualizar = new System.Windows.Forms.PictureBox();
            this.imgClientes = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBox18 = new System.Windows.Forms.PictureBox();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Usuario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Rut = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CorreoElectronico = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ciudad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Apellido = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.id_usuario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombre_usuario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_producto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fecha_favorito = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Barra.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox20)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgbUsuarios)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLupa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgbProductosFavoritos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnActualizar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgClientes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox18)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // Barra
            // 
            this.Barra.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Barra.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Barra.Controls.Add(this.pictureBox2);
            this.Barra.Controls.Add(this.pictureBox20);
            this.Barra.Controls.Add(this.label7);
            this.Barra.Controls.Add(this.pictureBox1);
            this.Barra.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.Barra.Dock = System.Windows.Forms.DockStyle.Top;
            this.Barra.Location = new System.Drawing.Point(0, 0);
            this.Barra.Name = "Barra";
            this.Barra.Size = new System.Drawing.Size(851, 22);
            this.Barra.TabIndex = 70;
            this.Barra.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Barra_MouseDown);
            this.Barra.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Barra_MouseMove);
            this.Barra.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Barra_MouseUp);
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
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(268, 3);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(235, 18);
            this.label7.TabIndex = 22;
            this.label7.Text = "Productos Favoritos Usuarios";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.imgClientes);
            this.panel2.Controls.Add(this.btnActualizar);
            this.panel2.Controls.Add(this.btnBuscar);
            this.panel2.Controls.Add(this.txtBuscarEn);
            this.panel2.Controls.Add(this.picLupa);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.cmbBuscarEn);
            this.panel2.Location = new System.Drawing.Point(12, 28);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(566, 60);
            this.panel2.TabIndex = 93;
            // 
            // txtBuscarEn
            // 
            this.txtBuscarEn.Location = new System.Drawing.Point(259, 26);
            this.txtBuscarEn.MaxLength = 50;
            this.txtBuscarEn.Name = "txtBuscarEn";
            this.txtBuscarEn.Size = new System.Drawing.Size(156, 20);
            this.txtBuscarEn.TabIndex = 10;
            this.txtBuscarEn.Visible = false;
            this.txtBuscarEn.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBuscarEn_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(149, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Buscar Por:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(276, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Paremetros a Buscar;";
            this.label1.Visible = false;
            // 
            // cmbBuscarEn
            // 
            this.cmbBuscarEn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cmbBuscarEn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmbBuscarEn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBuscarEn.ForeColor = System.Drawing.Color.White;
            this.cmbBuscarEn.FormattingEnabled = true;
            this.cmbBuscarEn.Items.AddRange(new object[] {
            "Usuario",
            "Rut",
            "Estado",
            "Correo Electronico",
            "Ciudad",
            "Apellido",
            "ID"});
            this.cmbBuscarEn.Location = new System.Drawing.Point(123, 25);
            this.cmbBuscarEn.Name = "cmbBuscarEn";
            this.cmbBuscarEn.Size = new System.Drawing.Size(121, 21);
            this.cmbBuscarEn.TabIndex = 8;
            this.cmbBuscarEn.SelectedValueChanged += new System.EventHandler(this.cmbBuscarEn_SelectedValueChanged);
            // 
            // dgbUsuarios
            // 
            this.dgbUsuarios.AllowUserToAddRows = false;
            this.dgbUsuarios.AllowUserToDeleteRows = false;
            this.dgbUsuarios.AllowUserToResizeRows = false;
            this.dgbUsuarios.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dgbUsuarios.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(117)))), ((int)(((byte)(208)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Tai Le", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgbUsuarios.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgbUsuarios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgbUsuarios.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.Usuario,
            this.Rut,
            this.Estado,
            this.CorreoElectronico,
            this.Ciudad,
            this.Apellido});
            this.dgbUsuarios.EnableHeadersVisualStyles = false;
            this.dgbUsuarios.Location = new System.Drawing.Point(12, 93);
            this.dgbUsuarios.Name = "dgbUsuarios";
            this.dgbUsuarios.ReadOnly = true;
            this.dgbUsuarios.RowHeadersVisible = false;
            this.dgbUsuarios.RowHeadersWidth = 40;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Cyan;
            this.dgbUsuarios.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgbUsuarios.Size = new System.Drawing.Size(830, 195);
            this.dgbUsuarios.TabIndex = 92;
            this.dgbUsuarios.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgbUsuarios_CellDoubleClick);
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(421, 26);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(83, 20);
            this.btnBuscar.TabIndex = 87;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Visible = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // picLupa
            // 
            this.picLupa.Image = ((System.Drawing.Image)(resources.GetObject("picLupa.Image")));
            this.picLupa.Location = new System.Drawing.Point(67, 3);
            this.picLupa.Name = "picLupa";
            this.picLupa.Size = new System.Drawing.Size(50, 47);
            this.picLupa.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLupa.TabIndex = 86;
            this.picLupa.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(584, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(227, 26);
            this.label3.TabIndex = 94;
            this.label3.Text = "*Seleccione un usuario para ver sus productos\r\nfavoritos realizando doble click";
            // 
            // dgbProductosFavoritos
            // 
            this.dgbProductosFavoritos.AllowUserToAddRows = false;
            this.dgbProductosFavoritos.AllowUserToDeleteRows = false;
            this.dgbProductosFavoritos.AllowUserToResizeRows = false;
            this.dgbProductosFavoritos.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dgbProductosFavoritos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(117)))), ((int)(((byte)(208)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Tai Le", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgbProductosFavoritos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgbProductosFavoritos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgbProductosFavoritos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id_usuario,
            this.nombre_usuario,
            this.id_producto,
            this.nombre,
            this.fecha_favorito});
            this.dgbProductosFavoritos.EnableHeadersVisualStyles = false;
            this.dgbProductosFavoritos.Location = new System.Drawing.Point(12, 327);
            this.dgbProductosFavoritos.Name = "dgbProductosFavoritos";
            this.dgbProductosFavoritos.ReadOnly = true;
            this.dgbProductosFavoritos.RowHeadersVisible = false;
            this.dgbProductosFavoritos.RowHeadersWidth = 40;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Cyan;
            this.dgbProductosFavoritos.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgbProductosFavoritos.Size = new System.Drawing.Size(830, 210);
            this.dgbProductosFavoritos.TabIndex = 95;
            // 
            // btnActualizar
            // 
            this.btnActualizar.Image = global::SBEPAEscritorio.Properties.Resources.actualizar;
            this.btnActualizar.Location = new System.Drawing.Point(510, 6);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(44, 40);
            this.btnActualizar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnActualizar.TabIndex = 100;
            this.btnActualizar.TabStop = false;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // imgClientes
            // 
            this.imgClientes.Image = global::SBEPAEscritorio.Properties.Resources.Clientes;
            this.imgClientes.Location = new System.Drawing.Point(14, 3);
            this.imgClientes.Name = "imgClientes";
            this.imgClientes.Size = new System.Drawing.Size(47, 47);
            this.imgClientes.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgClientes.TabIndex = 96;
            this.imgClientes.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(50, 304);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(188, 13);
            this.label4.TabIndex = 96;
            this.label4.Text = "Productos Favoritos del Usuario";
            // 
            // pictureBox18
            // 
            this.pictureBox18.Image = global::SBEPAEscritorio.Properties.Resources.favorito;
            this.pictureBox18.Location = new System.Drawing.Point(12, 294);
            this.pictureBox18.Name = "pictureBox18";
            this.pictureBox18.Size = new System.Drawing.Size(32, 30);
            this.pictureBox18.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox18.TabIndex = 97;
            this.pictureBox18.TabStop = false;
            // 
            // ID
            // 
            this.ID.DataPropertyName = "ID";
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Width = 50;
            // 
            // Usuario
            // 
            this.Usuario.DataPropertyName = "Usuario";
            this.Usuario.HeaderText = "Usuario";
            this.Usuario.Name = "Usuario";
            this.Usuario.ReadOnly = true;
            // 
            // Rut
            // 
            this.Rut.DataPropertyName = "Rut";
            this.Rut.HeaderText = "RUT";
            this.Rut.Name = "Rut";
            this.Rut.ReadOnly = true;
            // 
            // Estado
            // 
            this.Estado.DataPropertyName = "Estado";
            this.Estado.HeaderText = "Estado";
            this.Estado.Name = "Estado";
            this.Estado.ReadOnly = true;
            // 
            // CorreoElectronico
            // 
            this.CorreoElectronico.DataPropertyName = "Correo Electronico";
            this.CorreoElectronico.HeaderText = "Correo Electronico";
            this.CorreoElectronico.Name = "CorreoElectronico";
            this.CorreoElectronico.ReadOnly = true;
            this.CorreoElectronico.Width = 170;
            // 
            // Ciudad
            // 
            this.Ciudad.DataPropertyName = "Ciudad";
            this.Ciudad.HeaderText = "Ciudad";
            this.Ciudad.Name = "Ciudad";
            this.Ciudad.ReadOnly = true;
            this.Ciudad.Width = 150;
            // 
            // Apellido
            // 
            this.Apellido.DataPropertyName = "Apellido";
            this.Apellido.HeaderText = "Apellido";
            this.Apellido.Name = "Apellido";
            this.Apellido.ReadOnly = true;
            this.Apellido.Width = 130;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Image = global::SBEPAEscritorio.Properties.Resources.icons8_cerrar_ventana_48;
            this.pictureBox1.Location = new System.Drawing.Point(830, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(20, 20);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 23;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox2.Image = global::SBEPAEscritorio.Properties.Resources.icons8_minimizar_la_ventana_48;
            this.pictureBox2.Location = new System.Drawing.Point(810, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(20, 20);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 26;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.btnMinimizar_Click);
            // 
            // id_usuario
            // 
            this.id_usuario.DataPropertyName = "id_usuario";
            this.id_usuario.HeaderText = "id_usuario";
            this.id_usuario.Name = "id_usuario";
            this.id_usuario.ReadOnly = true;
            this.id_usuario.Width = 80;
            // 
            // nombre_usuario
            // 
            this.nombre_usuario.DataPropertyName = "nombre_usuario";
            this.nombre_usuario.HeaderText = "nombre_usuario";
            this.nombre_usuario.Name = "nombre_usuario";
            this.nombre_usuario.ReadOnly = true;
            this.nombre_usuario.Width = 150;
            // 
            // id_producto
            // 
            this.id_producto.DataPropertyName = "id_producto";
            this.id_producto.HeaderText = "id_producto";
            this.id_producto.Name = "id_producto";
            this.id_producto.ReadOnly = true;
            this.id_producto.Width = 80;
            // 
            // nombre
            // 
            this.nombre.DataPropertyName = "nombre";
            this.nombre.HeaderText = "nombre";
            this.nombre.Name = "nombre";
            this.nombre.ReadOnly = true;
            this.nombre.Width = 300;
            // 
            // fecha_favorito
            // 
            this.fecha_favorito.DataPropertyName = "fecha_favorito";
            this.fecha_favorito.HeaderText = "fecha_favorito";
            this.fecha_favorito.Name = "fecha_favorito";
            this.fecha_favorito.ReadOnly = true;
            this.fecha_favorito.Width = 140;
            // 
            // ProductosFavoritos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.ClientSize = new System.Drawing.Size(851, 549);
            this.Controls.Add(this.pictureBox18);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dgbProductosFavoritos);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.dgbUsuarios);
            this.Controls.Add(this.Barra);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ProductosFavoritos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Productos Favoritos";
            this.Load += new System.EventHandler(this.ProductosFavoritos_Load);
            this.Barra.ResumeLayout(false);
            this.Barra.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox20)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgbUsuarios)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLupa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgbProductosFavoritos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnActualizar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgClientes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox18)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel Barra;
        private System.Windows.Forms.PictureBox pictureBox20;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtBuscarEn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbBuscarEn;
        private System.Windows.Forms.DataGridView dgbUsuarios;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.PictureBox picLupa;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgbProductosFavoritos;
        private System.Windows.Forms.PictureBox btnActualizar;
        private System.Windows.Forms.PictureBox imgClientes;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox18;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Usuario;
        private System.Windows.Forms.DataGridViewTextBoxColumn Rut;
        private System.Windows.Forms.DataGridViewTextBoxColumn Estado;
        private System.Windows.Forms.DataGridViewTextBoxColumn CorreoElectronico;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ciudad;
        private System.Windows.Forms.DataGridViewTextBoxColumn Apellido;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_usuario;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombre_usuario;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_producto;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn fecha_favorito;
    }
}