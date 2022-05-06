namespace FORMULARIO_MANTENIMIENTO
{
    partial class frm_combobox
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
            this.label1 = new System.Windows.Forms.Label();
            this.cmbCursos = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCursoNuevo = new System.Windows.Forms.TextBox();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.txtDia = new System.Windows.Forms.TextBox();
            this.btnMostrar = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.txtConteo = new System.Windows.Forms.TextBox();
            this.txtIniciar = new System.Windows.Forms.Button();
            this.txtDetener = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(293, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Cursos:";
            // 
            // cmbCursos
            // 
            this.cmbCursos.FormattingEnabled = true;
            this.cmbCursos.Items.AddRange(new object[] {
            "Visual Studio 2020",
            "Visual Studi Code",
            "Pycharm",
            "Google Colab"});
            this.cmbCursos.Location = new System.Drawing.Point(363, 38);
            this.cmbCursos.Name = "cmbCursos";
            this.cmbCursos.Size = new System.Drawing.Size(121, 21);
            this.cmbCursos.TabIndex = 1;
            this.cmbCursos.Text = "Seleccione un curso";
            this.cmbCursos.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(29, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Curso Nuevo:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(293, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(146, 16);
            this.label3.TabIndex = 3;
            this.label3.Text = "Cursos Seleccionados:";
            // 
            // txtCursoNuevo
            // 
            this.txtCursoNuevo.Location = new System.Drawing.Point(134, 42);
            this.txtCursoNuevo.Name = "txtCursoNuevo";
            this.txtCursoNuevo.Size = new System.Drawing.Size(121, 20);
            this.txtCursoNuevo.TabIndex = 4;
            // 
            // txtResult
            // 
            this.txtResult.Location = new System.Drawing.Point(296, 126);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.ReadOnly = true;
            this.txtResult.Size = new System.Drawing.Size(188, 167);
            this.txtResult.TabIndex = 5;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(134, 96);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(121, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Registrar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(12, 204);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.ReadOnly = true;
            this.numericUpDown1.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown1.TabIndex = 7;
            this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // txtDia
            // 
            this.txtDia.Location = new System.Drawing.Point(138, 203);
            this.txtDia.Name = "txtDia";
            this.txtDia.Size = new System.Drawing.Size(117, 20);
            this.txtDia.TabIndex = 8;
            // 
            // btnMostrar
            // 
            this.btnMostrar.Location = new System.Drawing.Point(80, 239);
            this.btnMostrar.Name = "btnMostrar";
            this.btnMostrar.Size = new System.Drawing.Size(121, 23);
            this.btnMostrar.TabIndex = 9;
            this.btnMostrar.Text = "Mostrar";
            this.btnMostrar.UseVisualStyleBackColor = true;
            this.btnMostrar.Click += new System.EventHandler(this.button2_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // txtConteo
            // 
            this.txtConteo.Location = new System.Drawing.Point(138, 331);
            this.txtConteo.Name = "txtConteo";
            this.txtConteo.Size = new System.Drawing.Size(117, 20);
            this.txtConteo.TabIndex = 10;
            // 
            // txtIniciar
            // 
            this.txtIniciar.Location = new System.Drawing.Point(55, 374);
            this.txtIniciar.Name = "txtIniciar";
            this.txtIniciar.Size = new System.Drawing.Size(75, 23);
            this.txtIniciar.TabIndex = 12;
            this.txtIniciar.Text = "Iniciar";
            this.txtIniciar.UseVisualStyleBackColor = true;
            this.txtIniciar.Click += new System.EventHandler(this.txtIniciar_Click);
            // 
            // txtDetener
            // 
            this.txtDetener.Location = new System.Drawing.Point(153, 374);
            this.txtDetener.Name = "txtDetener";
            this.txtDetener.Size = new System.Drawing.Size(75, 23);
            this.txtDetener.TabIndex = 13;
            this.txtDetener.Text = "Detener";
            this.txtDetener.UseVisualStyleBackColor = true;
            this.txtDetener.Click += new System.EventHandler(this.txtDetener_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(52, 331);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 16);
            this.label4.TabIndex = 14;
            this.label4.Text = "Contador:";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(55, 302);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(200, 23);
            this.progressBar1.TabIndex = 15;
            // 
            // frm_combobox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(548, 441);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtDetener);
            this.Controls.Add(this.txtIniciar);
            this.Controls.Add(this.txtConteo);
            this.Controls.Add(this.btnMostrar);
            this.Controls.Add(this.txtDia);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.txtCursoNuevo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbCursos);
            this.Controls.Add(this.label1);
            this.Name = "frm_combobox";
            this.Text = "frm_combobox";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbCursos;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCursoNuevo;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.TextBox txtDia;
        private System.Windows.Forms.Button btnMostrar;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox txtConteo;
        private System.Windows.Forms.Button txtIniciar;
        private System.Windows.Forms.Button txtDetener;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}