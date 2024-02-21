namespace ACT2_archivos_con_tema
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnSequential = new System.Windows.Forms.Button();
            this.btnDirectAccess = new System.Windows.Forms.Button();
            this.textBoxOutput = new System.Windows.Forms.TextBox();
            this.btnIndexed = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnSequential
            // 
            this.btnSequential.Location = new System.Drawing.Point(606, 106);
            this.btnSequential.Name = "btnSequential";
            this.btnSequential.Size = new System.Drawing.Size(122, 23);
            this.btnSequential.TabIndex = 0;
            this.btnSequential.Text = "Sequential";
            this.btnSequential.UseVisualStyleBackColor = true;
            this.btnSequential.Click += new System.EventHandler(this.btnSequential_Click);
            // 
            // btnDirectAccess
            // 
            this.btnDirectAccess.Location = new System.Drawing.Point(606, 185);
            this.btnDirectAccess.Name = "btnDirectAccess";
            this.btnDirectAccess.Size = new System.Drawing.Size(122, 23);
            this.btnDirectAccess.TabIndex = 1;
            this.btnDirectAccess.Text = "DirectAccess";
            this.btnDirectAccess.UseVisualStyleBackColor = true;
            this.btnDirectAccess.Click += new System.EventHandler(this.btnDirectAccess_Click);
            // 
            // textBoxOutput
            // 
            this.textBoxOutput.Location = new System.Drawing.Point(215, 72);
            this.textBoxOutput.Multiline = true;
            this.textBoxOutput.Name = "textBoxOutput";
            this.textBoxOutput.Size = new System.Drawing.Size(354, 332);
            this.textBoxOutput.TabIndex = 2;
            // 
            // btnIndexed
            // 
            this.btnIndexed.Location = new System.Drawing.Point(606, 249);
            this.btnIndexed.Name = "btnIndexed";
            this.btnIndexed.Size = new System.Drawing.Size(122, 23);
            this.btnIndexed.TabIndex = 3;
            this.btnIndexed.Text = "Indexed";
            this.btnIndexed.UseVisualStyleBackColor = true;
            this.btnIndexed.Click += new System.EventHandler(this.btnIndexed_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(135, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "ID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(135, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "SALARY";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(135, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "NAME";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(414, 53);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 16);
            this.label4.TabIndex = 7;
            this.label4.Text = "SALARY";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(333, 53);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 16);
            this.label5.TabIndex = 8;
            this.label5.Text = "NAME";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(257, 53);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(20, 16);
            this.label6.TabIndex = 9;
            this.label6.Text = "ID";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnIndexed);
            this.Controls.Add(this.textBoxOutput);
            this.Controls.Add(this.btnDirectAccess);
            this.Controls.Add(this.btnSequential);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSequential;
        private System.Windows.Forms.Button btnDirectAccess;
        private System.Windows.Forms.TextBox textBoxOutput;
        private System.Windows.Forms.Button btnIndexed;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}

