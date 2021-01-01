namespace SBEPAEscritorio
{
    partial class AjustesConexion
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AjustesConexion));
            this.btnVerificarConexion = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.txtPuertoServer = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDireccionServer = new System.Windows.Forms.TextBox();
            this.ttmensaje = new System.Windows.Forms.ToolTip(this.components);
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtClave = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Barra = new System.Windows.Forms.Panel();
            this.btnMinimizar = new System.Windows.Forms.PictureBox();
            this.pictureBox20 = new System.Windows.Forms.PictureBox();
            this.btnCerrar = new System.Windows.Forms.PictureBox();
            this.label7 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pbClave = new System.Windows.Forms.PictureBox();
            this.pbUsuario = new System.Windows.Forms.PictureBox();
            this.pictureBox9 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pbPuertoServidor = new System.Windows.Forms.PictureBox();
            this.pbServidorIP = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox11 = new System.Windows.Forms.PictureBox();
            this.Barra.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnMinimizar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox20)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCerrar)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbClave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbUsuario)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPuertoServidor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbServidorIP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox11)).BeginInit();
            this.SuspendLayout();
            // 
            // btnVerificarConexion
            // 
            this.btnVerificarConexion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVerificarConexion.Location = new System.Drawing.Point(107, 231);
            this.btnVerificarConexion.Name = "btnVerificarConexion";
            this.btnVerificarConexion.Size = new System.Drawing.Size(188, 24);
            this.btnVerificarConexion.TabIndex = 43;
            this.btnVerificarConexion.Text = "Verificar Conexion a Base de Datos";
            this.btnVerificarConexion.UseVisualStyleBackColor = true;
            this.btnVerificarConexion.Click += new System.EventHandler(this.btnVerificarConexion_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardar.Location = new System.Drawing.Point(127, 347);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(144, 23);
            this.btnGuardar.TabIndex = 39;
            this.btnGuardar.Text = "Guardar Configuracion";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // txtPuertoServer
            // 
            this.txtPuertoServer.Location = new System.Drawing.Point(127, 61);
            this.txtPuertoServer.MaxLength = 5;
            this.txtPuertoServer.Name = "txtPuertoServer";
            this.txtPuertoServer.Size = new System.Drawing.Size(77, 20);
            this.txtPuertoServer.TabIndex = 31;
            this.txtPuertoServer.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPuertoServer_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(41, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 30;
            this.label2.Text = "Puerto Servidor";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(27, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 29;
            this.label1.Text = "Direccion Servidor";
            // 
            // txtDireccionServer
            // 
            this.txtDireccionServer.Location = new System.Drawing.Point(127, 35);
            this.txtDireccionServer.MaxLength = 15;
            this.txtDireccionServer.Name = "txtDireccionServer";
            this.txtDireccionServer.Size = new System.Drawing.Size(114, 20);
            this.txtDireccionServer.TabIndex = 28;
            this.txtDireccionServer.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDireccionServer_KeyPress);
            // 
            // ttmensaje
            // 
            this.ttmensaje.AutoPopDelay = 30000;
            this.ttmensaje.InitialDelay = 1000;
            this.ttmensaje.ReshowDelay = 100;
            // 
            // txtUsuario
            // 
            this.txtUsuario.Location = new System.Drawing.Point(127, 87);
            this.txtUsuario.MaxLength = 50;
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.PasswordChar = '*';
            this.txtUsuario.Size = new System.Drawing.Size(168, 20);
            this.txtUsuario.TabIndex = 64;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.Control;
            this.label3.Location = new System.Drawing.Point(78, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 63;
            this.label3.Text = "Usuario";
            // 
            // txtClave
            // 
            this.txtClave.Location = new System.Drawing.Point(127, 113);
            this.txtClave.MaxLength = 50;
            this.txtClave.Name = "txtClave";
            this.txtClave.PasswordChar = '*';
            this.txtClave.Size = new System.Drawing.Size(168, 20);
            this.txtClave.TabIndex = 67;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.Control;
            this.label4.Location = new System.Drawing.Point(87, 116);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 66;
            this.label4.Text = "Clave";
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
            this.Barra.Size = new System.Drawing.Size(370, 22);
            this.Barra.TabIndex = 69;
            this.Barra.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Barra_MouseDown);
            this.Barra.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Barra_MouseMove);
            this.Barra.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Barra_MouseUp);
            // 
            // btnMinimizar
            // 
            this.btnMinimizar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMinimizar.Image = global::SBEPAEscritorio.Properties.Resources.icons8_minimizar_la_ventana_48;
            this.btnMinimizar.Location = new System.Drawing.Point(328, 0);
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
            this.btnCerrar.Location = new System.Drawing.Point(348, 0);
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
            this.label7.Location = new System.Drawing.Point(104, 2);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(139, 18);
            this.label7.TabIndex = 22;
            this.label7.Text = "Ajustes Conexion";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.pictureBox11);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.pictureBox4);
            this.panel1.Controls.Add(this.txtDireccionServer);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.pbClave);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtClave);
            this.panel1.Controls.Add(this.txtPuertoServer);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.pbUsuario);
            this.panel1.Controls.Add(this.btnGuardar);
            this.panel1.Controls.Add(this.txtUsuario);
            this.panel1.Controls.Add(this.pictureBox9);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.pbPuertoServidor);
            this.panel1.Controls.Add(this.pbServidorIP);
            this.panel1.Controls.Add(this.btnVerificarConexion);
            this.panel1.Controls.Add(this.pictureBox3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 22);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(370, 384);
            this.panel1.TabIndex = 76;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(81, 144);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(231, 39);
            this.label5.TabIndex = 73;
            this.label5.Text = "Usar SSL Seguridad de la capa de transporte\r\nEl cual permite cifrar la conexion c" +
    "on el servidor\r\n(Obligatoriamente siempre esta Activo)";
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.Location = new System.Drawing.Point(36, 142);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(39, 43);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 72;
            this.pictureBox4.TabStop = false;
            // 
            // pbClave
            // 
            this.pbClave.Cursor = System.Windows.Forms.Cursors.Help;
            this.pbClave.Image = global::SBEPAEscritorio.Properties.Resources.info;
            this.pbClave.Location = new System.Drawing.Point(301, 116);
            this.pbClave.Name = "pbClave";
            this.pbClave.Size = new System.Drawing.Size(20, 20);
            this.pbClave.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbClave.TabIndex = 68;
            this.pbClave.TabStop = false;
            // 
            // pbUsuario
            // 
            this.pbUsuario.Cursor = System.Windows.Forms.Cursors.Help;
            this.pbUsuario.Image = global::SBEPAEscritorio.Properties.Resources.info;
            this.pbUsuario.Location = new System.Drawing.Point(301, 90);
            this.pbUsuario.Name = "pbUsuario";
            this.pbUsuario.Size = new System.Drawing.Size(20, 20);
            this.pbUsuario.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbUsuario.TabIndex = 65;
            this.pbUsuario.TabStop = false;
            // 
            // pictureBox9
            // 
            this.pictureBox9.Image = global::SBEPAEscritorio.Properties.Resources.BaseDatosAjustes;
            this.pictureBox9.Location = new System.Drawing.Point(277, 5);
            this.pictureBox9.Name = "pictureBox9";
            this.pictureBox9.Size = new System.Drawing.Size(75, 72);
            this.pictureBox9.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox9.TabIndex = 40;
            this.pictureBox9.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::SBEPAEscritorio.Properties.Resources.baseDeDatosCheckGuardar;
            this.pictureBox1.Location = new System.Drawing.Point(164, 280);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(64, 61);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 41;
            this.pictureBox1.TabStop = false;
            // 
            // pbPuertoServidor
            // 
            this.pbPuertoServidor.Cursor = System.Windows.Forms.Cursors.Help;
            this.pbPuertoServidor.Image = global::SBEPAEscritorio.Properties.Resources.info;
            this.pbPuertoServidor.Location = new System.Drawing.Point(208, 63);
            this.pbPuertoServidor.Name = "pbPuertoServidor";
            this.pbPuertoServidor.Size = new System.Drawing.Size(20, 20);
            this.pbPuertoServidor.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbPuertoServidor.TabIndex = 62;
            this.pbPuertoServidor.TabStop = false;
            // 
            // pbServidorIP
            // 
            this.pbServidorIP.Cursor = System.Windows.Forms.Cursors.Help;
            this.pbServidorIP.Image = global::SBEPAEscritorio.Properties.Resources.info;
            this.pbServidorIP.Location = new System.Drawing.Point(243, 35);
            this.pbServidorIP.Name = "pbServidorIP";
            this.pbServidorIP.Size = new System.Drawing.Size(20, 20);
            this.pbServidorIP.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbServidorIP.TabIndex = 61;
            this.pbServidorIP.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::SBEPAEscritorio.Properties.Resources.check;
            this.pictureBox3.Location = new System.Drawing.Point(67, 226);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(34, 33);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 44;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox11
            // 
            this.pictureBox11.Image = global::SBEPAEscritorio.Properties.Resources.MiniLogo;
            this.pictureBox11.Location = new System.Drawing.Point(325, 332);
            this.pictureBox11.Name = "pictureBox11";
            this.pictureBox11.Size = new System.Drawing.Size(40, 47);
            this.pictureBox11.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox11.TabIndex = 166;
            this.pictureBox11.TabStop = false;
            // 
            // AjustesConexion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.ClientSize = new System.Drawing.Size(370, 406);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Barra);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AjustesConexion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ajustes Conexion";
            this.Load += new System.EventHandler(this.AjustesConexion_Load);
            this.Barra.ResumeLayout(false);
            this.Barra.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnMinimizar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox20)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCerrar)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbClave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbUsuario)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPuertoServidor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbServidorIP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox11)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Button btnVerificarConexion;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox9;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.TextBox txtPuertoServer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDireccionServer;
        private System.Windows.Forms.ToolTip ttmensaje;
        private System.Windows.Forms.PictureBox pbServidorIP;
        private System.Windows.Forms.PictureBox pbPuertoServidor;
        private System.Windows.Forms.PictureBox pbUsuario;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pbClave;
        private System.Windows.Forms.TextBox txtClave;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel Barra;
        private System.Windows.Forms.PictureBox btnMinimizar;
        private System.Windows.Forms.PictureBox pictureBox20;
        private System.Windows.Forms.PictureBox btnCerrar;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox pictureBox11;
    }
}