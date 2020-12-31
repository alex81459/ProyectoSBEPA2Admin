namespace SBEPAEscritorio
{
    partial class ProductosBuscarCategoria
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProductosBuscarCategoria));
            this.Barra = new System.Windows.Forms.Panel();
            this.btnMinimizar = new System.Windows.Forms.PictureBox();
            this.pictureBox20 = new System.Windows.Forms.PictureBox();
            this.btnCerrar = new System.Windows.Forms.PictureBox();
            this.label21 = new System.Windows.Forms.Label();
            this.dgbCategoria = new System.Windows.Forms.DataGridView();
            this.Categoria = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CategoriaSimple = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SubCategoria = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDSubCategoria = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.txtBuscarEn = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbBuscarEn = new System.Windows.Forms.ComboBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Barra.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnMinimizar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox20)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCerrar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgbCategoria)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // Barra
            // 
            this.Barra.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Barra.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Barra.Controls.Add(this.btnMinimizar);
            this.Barra.Controls.Add(this.pictureBox20);
            this.Barra.Controls.Add(this.btnCerrar);
            this.Barra.Controls.Add(this.label21);
            this.Barra.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.Barra.Dock = System.Windows.Forms.DockStyle.Top;
            this.Barra.Location = new System.Drawing.Point(0, 0);
            this.Barra.Name = "Barra";
            this.Barra.Size = new System.Drawing.Size(737, 22);
            this.Barra.TabIndex = 71;
            // 
            // btnMinimizar
            // 
            this.btnMinimizar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMinimizar.Image = global::SBEPAEscritorio.Properties.Resources.icons8_minimizar_la_ventana_48;
            this.btnMinimizar.Location = new System.Drawing.Point(696, 0);
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
            this.btnCerrar.Location = new System.Drawing.Point(716, 0);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(20, 20);
            this.btnCerrar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnCerrar.TabIndex = 23;
            this.btnCerrar.TabStop = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.ForeColor = System.Drawing.Color.White;
            this.label21.Location = new System.Drawing.Point(185, 2);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(247, 18);
            this.label21.TabIndex = 22;
            this.label21.Text = "Producto Buscar Sub Categoria";
            // 
            // dgbCategoria
            // 
            this.dgbCategoria.AllowUserToAddRows = false;
            this.dgbCategoria.AllowUserToDeleteRows = false;
            this.dgbCategoria.AllowUserToResizeRows = false;
            this.dgbCategoria.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dgbCategoria.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(117)))), ((int)(((byte)(208)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Tai Le", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgbCategoria.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgbCategoria.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgbCategoria.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Categoria,
            this.CategoriaSimple,
            this.SubCategoria,
            this.IDSubCategoria});
            this.dgbCategoria.EnableHeadersVisualStyles = false;
            this.dgbCategoria.Location = new System.Drawing.Point(6, 99);
            this.dgbCategoria.Name = "dgbCategoria";
            this.dgbCategoria.ReadOnly = true;
            this.dgbCategoria.RowHeadersVisible = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Cyan;
            this.dgbCategoria.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgbCategoria.Size = new System.Drawing.Size(724, 295);
            this.dgbCategoria.TabIndex = 72;
            this.dgbCategoria.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgbCategoria_CellDoubleClick);
            // 
            // Categoria
            // 
            this.Categoria.DataPropertyName = "Categoria";
            this.Categoria.HeaderText = "Categoria";
            this.Categoria.Name = "Categoria";
            this.Categoria.ReadOnly = true;
            this.Categoria.Width = 200;
            // 
            // CategoriaSimple
            // 
            this.CategoriaSimple.DataPropertyName = "CategoriaSimple";
            this.CategoriaSimple.HeaderText = "CategoriaSimple";
            this.CategoriaSimple.Name = "CategoriaSimple";
            this.CategoriaSimple.ReadOnly = true;
            this.CategoriaSimple.Width = 200;
            // 
            // SubCategoria
            // 
            this.SubCategoria.DataPropertyName = "SubCategoria";
            this.SubCategoria.HeaderText = "SubCategoria";
            this.SubCategoria.Name = "SubCategoria";
            this.SubCategoria.ReadOnly = true;
            this.SubCategoria.Width = 200;
            // 
            // IDSubCategoria
            // 
            this.IDSubCategoria.DataPropertyName = "IDSubCategoria";
            this.IDSubCategoria.HeaderText = "IDSubCategoria";
            this.IDSubCategoria.Name = "IDSubCategoria";
            this.IDSubCategoria.ReadOnly = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 84);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(214, 12);
            this.label1.TabIndex = 73;
            this.label1.Text = "*Para seleccionar la sub Categoria Debe doble click";
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
            this.panel4.Location = new System.Drawing.Point(112, 28);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(548, 53);
            this.panel4.TabIndex = 74;
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
            this.txtBuscarEn.Location = new System.Drawing.Point(296, 23);
            this.txtBuscarEn.Name = "txtBuscarEn";
            this.txtBuscarEn.Size = new System.Drawing.Size(157, 20);
            this.txtBuscarEn.TabIndex = 63;
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
            "Categoria",
            "CategoriaSimple",
            "SubCategoria"});
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
            // ProductosBuscarCategoria
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.ClientSize = new System.Drawing.Size(737, 402);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgbCategoria);
            this.Controls.Add(this.Barra);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ProductosBuscarCategoria";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Productos Buscar Categoria";
            this.Load += new System.EventHandler(this.ProductosBuscarCategoria_Load);
            this.Barra.ResumeLayout(false);
            this.Barra.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnMinimizar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox20)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCerrar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgbCategoria)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel Barra;
        private System.Windows.Forms.PictureBox btnMinimizar;
        private System.Windows.Forms.PictureBox pictureBox20;
        private System.Windows.Forms.PictureBox btnCerrar;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.DataGridView dgbCategoria;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Categoria;
        private System.Windows.Forms.DataGridViewTextBoxColumn CategoriaSimple;
        private System.Windows.Forms.DataGridViewTextBoxColumn SubCategoria;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDSubCategoria;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtBuscarEn;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmbBuscarEn;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}