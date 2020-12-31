namespace SBEPAEscritorio
{
    partial class BaneoUsuarios
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BaneoUsuarios));
            this.Barra = new System.Windows.Forms.Panel();
            this.btnMinimizar = new System.Windows.Forms.PictureBox();
            this.pictureBox20 = new System.Windows.Forms.PictureBox();
            this.btnCerrar = new System.Windows.Forms.PictureBox();
            this.label7 = new System.Windows.Forms.Label();
            this.dgbUsuariosBaneados = new System.Windows.Forms.DataGridView();
            this.id_baneo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombre_usuario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.razon_baneo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dias_baneo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_usuario_baneado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pBActualizar = new System.Windows.Forms.PictureBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtBuscarEn = new System.Windows.Forms.TextBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cmbBuscarEn = new System.Windows.Forms.ComboBox();
            this.gbUsuarioBaneado = new System.Windows.Forms.GroupBox();
            this.pbDias = new System.Windows.Forms.PictureBox();
            this.pbFecha = new System.Windows.Forms.PictureBox();
            this.pbRazonDelBane = new System.Windows.Forms.PictureBox();
            this.pbNombreUsuario = new System.Windows.Forms.PictureBox();
            this.pbIDUsuario = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnLimpiarCampos = new System.Windows.Forms.Button();
            this.btnGuardarBaneo = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.nudDias = new System.Windows.Forms.NumericUpDown();
            this.lblFechaBaneo = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtRazonBaneo = new System.Windows.Forms.RichTextBox();
            this.btnBuscarUsuario = new System.Windows.Forms.Button();
            this.lblNombreUsuario = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblIDUsuario = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox14 = new System.Windows.Forms.PictureBox();
            this.label10 = new System.Windows.Forms.Label();
            this.lblIDBaneo = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.Barra.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnMinimizar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox20)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCerrar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgbUsuariosBaneados)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pBActualizar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.gbUsuarioBaneado.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbDias)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbFecha)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbRazonDelBane)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbNombreUsuario)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbIDUsuario)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDias)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox14)).BeginInit();
            this.SuspendLayout();
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
            this.Barra.Size = new System.Drawing.Size(934, 22);
            this.Barra.TabIndex = 70;
            this.Barra.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Barra_MouseDown);
            this.Barra.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Barra_MouseMove);
            this.Barra.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Barra_MouseUp);
            // 
            // btnMinimizar
            // 
            this.btnMinimizar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMinimizar.Image = global::SBEPAEscritorio.Properties.Resources.icons8_minimizar_la_ventana_48;
            this.btnMinimizar.Location = new System.Drawing.Point(893, 0);
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
            this.btnCerrar.Location = new System.Drawing.Point(913, 0);
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
            this.label7.Location = new System.Drawing.Point(378, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(129, 18);
            this.label7.TabIndex = 22;
            this.label7.Text = "Baneo Usuarios";
            // 
            // dgbUsuariosBaneados
            // 
            this.dgbUsuariosBaneados.AllowUserToAddRows = false;
            this.dgbUsuariosBaneados.AllowUserToDeleteRows = false;
            this.dgbUsuariosBaneados.AllowUserToResizeRows = false;
            this.dgbUsuariosBaneados.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dgbUsuariosBaneados.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(117)))), ((int)(((byte)(208)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Tai Le", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgbUsuariosBaneados.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgbUsuariosBaneados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgbUsuariosBaneados.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id_baneo,
            this.nombre_usuario,
            this.razon_baneo,
            this.fecha,
            this.dias_baneo,
            this.id_usuario_baneado});
            this.dgbUsuariosBaneados.EnableHeadersVisualStyles = false;
            this.dgbUsuariosBaneados.Location = new System.Drawing.Point(10, 88);
            this.dgbUsuariosBaneados.Name = "dgbUsuariosBaneados";
            this.dgbUsuariosBaneados.ReadOnly = true;
            this.dgbUsuariosBaneados.RowHeadersVisible = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Cyan;
            this.dgbUsuariosBaneados.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgbUsuariosBaneados.Size = new System.Drawing.Size(658, 335);
            this.dgbUsuariosBaneados.TabIndex = 71;
            this.dgbUsuariosBaneados.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgbUsuariosBaneados_CellContentDoubleClick);
            // 
            // id_baneo
            // 
            this.id_baneo.DataPropertyName = "id_baneo";
            this.id_baneo.HeaderText = "id_baneo";
            this.id_baneo.Name = "id_baneo";
            this.id_baneo.ReadOnly = true;
            this.id_baneo.Width = 75;
            // 
            // nombre_usuario
            // 
            this.nombre_usuario.DataPropertyName = "nombre_usuario";
            this.nombre_usuario.HeaderText = "nombre_usuario";
            this.nombre_usuario.Name = "nombre_usuario";
            this.nombre_usuario.ReadOnly = true;
            // 
            // razon_baneo
            // 
            this.razon_baneo.DataPropertyName = "razon_baneo";
            this.razon_baneo.HeaderText = "razon_baneo";
            this.razon_baneo.Name = "razon_baneo";
            this.razon_baneo.ReadOnly = true;
            this.razon_baneo.Width = 250;
            // 
            // fecha
            // 
            this.fecha.DataPropertyName = "fecha";
            this.fecha.HeaderText = "fecha";
            this.fecha.Name = "fecha";
            this.fecha.ReadOnly = true;
            // 
            // dias_baneo
            // 
            this.dias_baneo.DataPropertyName = "dias_baneo";
            this.dias_baneo.HeaderText = "dias_baneo";
            this.dias_baneo.Name = "dias_baneo";
            this.dias_baneo.ReadOnly = true;
            this.dias_baneo.Width = 75;
            // 
            // id_usuario_baneado
            // 
            this.id_usuario_baneado.DataPropertyName = "id_usuario_baneado";
            this.id_usuario_baneado.HeaderText = "id_usuario_baneado";
            this.id_usuario_baneado.Name = "id_usuario_baneado";
            this.id_usuario_baneado.ReadOnly = true;
            this.id_usuario_baneado.Width = 130;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.gbUsuarioBaneado);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.dgbUsuariosBaneados);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 22);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(934, 428);
            this.panel1.TabIndex = 72;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.pBActualizar);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.txtBuscarEn);
            this.panel2.Controls.Add(this.pictureBox3);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.cmbBuscarEn);
            this.panel2.Location = new System.Drawing.Point(86, 5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(508, 62);
            this.panel2.TabIndex = 74;
            // 
            // pBActualizar
            // 
            this.pBActualizar.Image = global::SBEPAEscritorio.Properties.Resources.actualizar;
            this.pBActualizar.Location = new System.Drawing.Point(409, 8);
            this.pBActualizar.Name = "pBActualizar";
            this.pBActualizar.Size = new System.Drawing.Size(46, 46);
            this.pBActualizar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pBActualizar.TabIndex = 61;
            this.pBActualizar.TabStop = false;
            this.pBActualizar.Click += new System.EventHandler(this.pBActualizar_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(270, 13);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(108, 13);
            this.label9.TabIndex = 60;
            this.label9.Text = "Paremetros a Buscar;";
            // 
            // txtBuscarEn
            // 
            this.txtBuscarEn.Location = new System.Drawing.Point(246, 29);
            this.txtBuscarEn.Name = "txtBuscarEn";
            this.txtBuscarEn.Size = new System.Drawing.Size(157, 20);
            this.txtBuscarEn.TabIndex = 59;
            this.txtBuscarEn.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBuscarEn_KeyPress);
            this.txtBuscarEn.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtBuscarEn_KeyUp);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(35, 8);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(46, 46);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 56;
            this.pictureBox3.TabStop = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(117, 13);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(62, 13);
            this.label11.TabIndex = 57;
            this.label11.Text = "Buscar Por:";
            // 
            // cmbBuscarEn
            // 
            this.cmbBuscarEn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cmbBuscarEn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmbBuscarEn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBuscarEn.ForeColor = System.Drawing.Color.White;
            this.cmbBuscarEn.FormattingEnabled = true;
            this.cmbBuscarEn.Items.AddRange(new object[] {
            "id_baneo",
            "razon_baneo",
            "dias_baneo",
            "id_usuario_baneado"});
            this.cmbBuscarEn.Location = new System.Drawing.Point(87, 29);
            this.cmbBuscarEn.Name = "cmbBuscarEn";
            this.cmbBuscarEn.Size = new System.Drawing.Size(121, 21);
            this.cmbBuscarEn.TabIndex = 58;
            // 
            // gbUsuarioBaneado
            // 
            this.gbUsuarioBaneado.Controls.Add(this.lblIDBaneo);
            this.gbUsuarioBaneado.Controls.Add(this.label12);
            this.gbUsuarioBaneado.Controls.Add(this.pbDias);
            this.gbUsuarioBaneado.Controls.Add(this.pbFecha);
            this.gbUsuarioBaneado.Controls.Add(this.pbRazonDelBane);
            this.gbUsuarioBaneado.Controls.Add(this.pbNombreUsuario);
            this.gbUsuarioBaneado.Controls.Add(this.pbIDUsuario);
            this.gbUsuarioBaneado.Controls.Add(this.pictureBox2);
            this.gbUsuarioBaneado.Controls.Add(this.pictureBox1);
            this.gbUsuarioBaneado.Controls.Add(this.btnLimpiarCampos);
            this.gbUsuarioBaneado.Controls.Add(this.btnGuardarBaneo);
            this.gbUsuarioBaneado.Controls.Add(this.label6);
            this.gbUsuarioBaneado.Controls.Add(this.nudDias);
            this.gbUsuarioBaneado.Controls.Add(this.lblFechaBaneo);
            this.gbUsuarioBaneado.Controls.Add(this.label8);
            this.gbUsuarioBaneado.Controls.Add(this.label5);
            this.gbUsuarioBaneado.Controls.Add(this.txtRazonBaneo);
            this.gbUsuarioBaneado.Controls.Add(this.btnBuscarUsuario);
            this.gbUsuarioBaneado.Controls.Add(this.lblNombreUsuario);
            this.gbUsuarioBaneado.Controls.Add(this.label3);
            this.gbUsuarioBaneado.Controls.Add(this.label1);
            this.gbUsuarioBaneado.Controls.Add(this.lblIDUsuario);
            this.gbUsuarioBaneado.Controls.Add(this.label2);
            this.gbUsuarioBaneado.Controls.Add(this.pictureBox14);
            this.gbUsuarioBaneado.ForeColor = System.Drawing.Color.White;
            this.gbUsuarioBaneado.Location = new System.Drawing.Point(676, -1);
            this.gbUsuarioBaneado.Name = "gbUsuarioBaneado";
            this.gbUsuarioBaneado.Size = new System.Drawing.Size(253, 424);
            this.gbUsuarioBaneado.TabIndex = 73;
            this.gbUsuarioBaneado.TabStop = false;
            // 
            // pbDias
            // 
            this.pbDias.Cursor = System.Windows.Forms.Cursors.Help;
            this.pbDias.Image = global::SBEPAEscritorio.Properties.Resources.info;
            this.pbDias.Location = new System.Drawing.Point(6, 280);
            this.pbDias.Name = "pbDias";
            this.pbDias.Size = new System.Drawing.Size(20, 20);
            this.pbDias.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbDias.TabIndex = 142;
            this.pbDias.TabStop = false;
            // 
            // pbFecha
            // 
            this.pbFecha.Cursor = System.Windows.Forms.Cursors.Help;
            this.pbFecha.Image = global::SBEPAEscritorio.Properties.Resources.info;
            this.pbFecha.Location = new System.Drawing.Point(6, 256);
            this.pbFecha.Name = "pbFecha";
            this.pbFecha.Size = new System.Drawing.Size(20, 20);
            this.pbFecha.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbFecha.TabIndex = 141;
            this.pbFecha.TabStop = false;
            // 
            // pbRazonDelBane
            // 
            this.pbRazonDelBane.Cursor = System.Windows.Forms.Cursors.Help;
            this.pbRazonDelBane.Image = global::SBEPAEscritorio.Properties.Resources.info;
            this.pbRazonDelBane.Location = new System.Drawing.Point(6, 160);
            this.pbRazonDelBane.Name = "pbRazonDelBane";
            this.pbRazonDelBane.Size = new System.Drawing.Size(20, 20);
            this.pbRazonDelBane.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbRazonDelBane.TabIndex = 140;
            this.pbRazonDelBane.TabStop = false;
            // 
            // pbNombreUsuario
            // 
            this.pbNombreUsuario.Cursor = System.Windows.Forms.Cursors.Help;
            this.pbNombreUsuario.Image = global::SBEPAEscritorio.Properties.Resources.info;
            this.pbNombreUsuario.Location = new System.Drawing.Point(4, 96);
            this.pbNombreUsuario.Name = "pbNombreUsuario";
            this.pbNombreUsuario.Size = new System.Drawing.Size(20, 20);
            this.pbNombreUsuario.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbNombreUsuario.TabIndex = 139;
            this.pbNombreUsuario.TabStop = false;
            // 
            // pbIDUsuario
            // 
            this.pbIDUsuario.Cursor = System.Windows.Forms.Cursors.Help;
            this.pbIDUsuario.Image = global::SBEPAEscritorio.Properties.Resources.info;
            this.pbIDUsuario.Location = new System.Drawing.Point(4, 75);
            this.pbIDUsuario.Name = "pbIDUsuario";
            this.pbIDUsuario.Size = new System.Drawing.Size(20, 20);
            this.pbIDUsuario.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbIDUsuario.TabIndex = 138;
            this.pbIDUsuario.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::SBEPAEscritorio.Properties.Resources.limpiar1;
            this.pictureBox2.Location = new System.Drawing.Point(158, 334);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(52, 53);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 137;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::SBEPAEscritorio.Properties.Resources.guardar;
            this.pictureBox1.Location = new System.Drawing.Point(36, 334);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(52, 53);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 136;
            this.pictureBox1.TabStop = false;
            // 
            // btnLimpiarCampos
            // 
            this.btnLimpiarCampos.ForeColor = System.Drawing.Color.Black;
            this.btnLimpiarCampos.Location = new System.Drawing.Point(134, 393);
            this.btnLimpiarCampos.Name = "btnLimpiarCampos";
            this.btnLimpiarCampos.Size = new System.Drawing.Size(97, 23);
            this.btnLimpiarCampos.TabIndex = 135;
            this.btnLimpiarCampos.Text = "Limpiar Campos";
            this.btnLimpiarCampos.UseVisualStyleBackColor = true;
            this.btnLimpiarCampos.Click += new System.EventHandler(this.btnLimpiarCampos_Click);
            // 
            // btnGuardarBaneo
            // 
            this.btnGuardarBaneo.ForeColor = System.Drawing.Color.Black;
            this.btnGuardarBaneo.Location = new System.Drawing.Point(15, 393);
            this.btnGuardarBaneo.Name = "btnGuardarBaneo";
            this.btnGuardarBaneo.Size = new System.Drawing.Size(97, 23);
            this.btnGuardarBaneo.TabIndex = 134;
            this.btnGuardarBaneo.Text = "Guardar Baneo";
            this.btnGuardarBaneo.UseVisualStyleBackColor = true;
            this.btnGuardarBaneo.Click += new System.EventHandler(this.btnGuardarBaneo_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(30, 282);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(36, 13);
            this.label6.TabIndex = 133;
            this.label6.Text = "Dias:";
            // 
            // nudDias
            // 
            this.nudDias.Cursor = System.Windows.Forms.Cursors.Hand;
            this.nudDias.Location = new System.Drawing.Point(68, 280);
            this.nudDias.Maximum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.nudDias.Name = "nudDias";
            this.nudDias.Size = new System.Drawing.Size(44, 20);
            this.nudDias.TabIndex = 132;
            this.nudDias.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblFechaBaneo
            // 
            this.lblFechaBaneo.AutoSize = true;
            this.lblFechaBaneo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFechaBaneo.ForeColor = System.Drawing.Color.White;
            this.lblFechaBaneo.Location = new System.Drawing.Point(75, 260);
            this.lblFechaBaneo.Name = "lblFechaBaneo";
            this.lblFechaBaneo.Size = new System.Drawing.Size(19, 13);
            this.lblFechaBaneo.TabIndex = 88;
            this.lblFechaBaneo.Text = "¿?";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(26, 260);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(50, 13);
            this.label8.TabIndex = 87;
            this.label8.Text = "Fecha: ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(30, 165);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(108, 13);
            this.label5.TabIndex = 86;
            this.label5.Text = "Razon del Baneo:";
            // 
            // txtRazonBaneo
            // 
            this.txtRazonBaneo.Location = new System.Drawing.Point(6, 182);
            this.txtRazonBaneo.MaxLength = 250;
            this.txtRazonBaneo.Name = "txtRazonBaneo";
            this.txtRazonBaneo.Size = new System.Drawing.Size(239, 69);
            this.txtRazonBaneo.TabIndex = 85;
            this.txtRazonBaneo.Text = "";
            this.txtRazonBaneo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRazonBaneo_KeyPress);
            // 
            // btnBuscarUsuario
            // 
            this.btnBuscarUsuario.ForeColor = System.Drawing.Color.Black;
            this.btnBuscarUsuario.Location = new System.Drawing.Point(75, 123);
            this.btnBuscarUsuario.Name = "btnBuscarUsuario";
            this.btnBuscarUsuario.Size = new System.Drawing.Size(97, 23);
            this.btnBuscarUsuario.TabIndex = 84;
            this.btnBuscarUsuario.Text = "Buscar Usuario...";
            this.btnBuscarUsuario.UseVisualStyleBackColor = true;
            this.btnBuscarUsuario.Click += new System.EventHandler(this.btnBuscarUsuario_Click);
            // 
            // lblNombreUsuario
            // 
            this.lblNombreUsuario.AutoSize = true;
            this.lblNombreUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombreUsuario.ForeColor = System.Drawing.Color.White;
            this.lblNombreUsuario.Location = new System.Drawing.Point(119, 100);
            this.lblNombreUsuario.Name = "lblNombreUsuario";
            this.lblNombreUsuario.Size = new System.Drawing.Size(19, 13);
            this.lblNombreUsuario.TabIndex = 83;
            this.lblNombreUsuario.Text = "¿?";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(23, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 13);
            this.label3.TabIndex = 82;
            this.label3.Text = "Nombre Usuario:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(64, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(170, 24);
            this.label1.TabIndex = 81;
            this.label1.Text = "Usuario a Banear";
            // 
            // lblIDUsuario
            // 
            this.lblIDUsuario.AutoSize = true;
            this.lblIDUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIDUsuario.ForeColor = System.Drawing.Color.White;
            this.lblIDUsuario.Location = new System.Drawing.Point(89, 78);
            this.lblIDUsuario.Name = "lblIDUsuario";
            this.lblIDUsuario.Size = new System.Drawing.Size(19, 13);
            this.lblIDUsuario.TabIndex = 80;
            this.lblIDUsuario.Text = "¿?";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(23, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 79;
            this.label2.Text = "ID Usuario:";
            // 
            // pictureBox14
            // 
            this.pictureBox14.Image = global::SBEPAEscritorio.Properties.Resources.ban;
            this.pictureBox14.Location = new System.Drawing.Point(6, 22);
            this.pictureBox14.Name = "pictureBox14";
            this.pictureBox14.Size = new System.Drawing.Size(52, 53);
            this.pictureBox14.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox14.TabIndex = 78;
            this.pictureBox14.TabStop = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(9, 73);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(329, 12);
            this.label10.TabIndex = 72;
            this.label10.Text = "*Seleccione un Usario Baneado haciendo doble click sobre sus datos en la Tabla";
            // 
            // lblIDBaneo
            // 
            this.lblIDBaneo.AutoSize = true;
            this.lblIDBaneo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIDBaneo.ForeColor = System.Drawing.Color.White;
            this.lblIDBaneo.Location = new System.Drawing.Point(129, 20);
            this.lblIDBaneo.Name = "lblIDBaneo";
            this.lblIDBaneo.Size = new System.Drawing.Size(19, 13);
            this.lblIDBaneo.TabIndex = 144;
            this.lblIDBaneo.Text = "¿?";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(67, 20);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(64, 13);
            this.label12.TabIndex = 143;
            this.label12.Text = "ID Baneo:";
            // 
            // BaneoUsuarios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.ClientSize = new System.Drawing.Size(934, 450);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Barra);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BaneoUsuarios";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Baneo Usuarios";
            this.Barra.ResumeLayout(false);
            this.Barra.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnMinimizar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox20)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCerrar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgbUsuariosBaneados)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pBActualizar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.gbUsuarioBaneado.ResumeLayout(false);
            this.gbUsuarioBaneado.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbDias)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbFecha)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbRazonDelBane)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbNombreUsuario)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbIDUsuario)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDias)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox14)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Barra;
        private System.Windows.Forms.PictureBox btnMinimizar;
        private System.Windows.Forms.PictureBox pictureBox20;
        private System.Windows.Forms.PictureBox btnCerrar;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridView dgbUsuariosBaneados;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox gbUsuarioBaneado;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox14;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.PictureBox pbDias;
        private System.Windows.Forms.PictureBox pbFecha;
        private System.Windows.Forms.PictureBox pbRazonDelBane;
        private System.Windows.Forms.PictureBox pbNombreUsuario;
        private System.Windows.Forms.PictureBox pbIDUsuario;
        public System.Windows.Forms.Label lblNombreUsuario;
        public System.Windows.Forms.Label lblIDUsuario;
        public System.Windows.Forms.Button btnBuscarUsuario;
        public System.Windows.Forms.RichTextBox txtRazonBaneo;
        public System.Windows.Forms.Label lblFechaBaneo;
        public System.Windows.Forms.NumericUpDown nudDias;
        public System.Windows.Forms.Button btnLimpiarCampos;
        public System.Windows.Forms.Button btnGuardarBaneo;
        public System.Windows.Forms.TextBox txtBuscarEn;
        public System.Windows.Forms.ComboBox cmbBuscarEn;
        public System.Windows.Forms.PictureBox pBActualizar;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_baneo;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombre_usuario;
        private System.Windows.Forms.DataGridViewTextBoxColumn razon_baneo;
        private System.Windows.Forms.DataGridViewTextBoxColumn fecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn dias_baneo;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_usuario_baneado;
        public System.Windows.Forms.Label lblIDBaneo;
        private System.Windows.Forms.Label label12;
    }
}