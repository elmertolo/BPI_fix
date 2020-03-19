namespace BPI_fix
{
    partial class Form1
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
            this.btnBPI = new System.Windows.Forms.Button();
            this.btnFBPI = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnBPI
            // 
            this.btnBPI.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnBPI.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBPI.Location = new System.Drawing.Point(63, 26);
            this.btnBPI.Name = "btnBPI";
            this.btnBPI.Size = new System.Drawing.Size(200, 54);
            this.btnBPI.TabIndex = 0;
            this.btnBPI.Text = "BPI";
            this.btnBPI.UseVisualStyleBackColor = false;
            this.btnBPI.Click += new System.EventHandler(this.btnBPI_Click);
            // 
            // btnFBPI
            // 
            this.btnFBPI.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnFBPI.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFBPI.Location = new System.Drawing.Point(63, 86);
            this.btnFBPI.Name = "btnFBPI";
            this.btnFBPI.Size = new System.Drawing.Size(200, 54);
            this.btnFBPI.TabIndex = 1;
            this.btnFBPI.Text = "FBPI";
            this.btnFBPI.UseVisualStyleBackColor = false;
            this.btnFBPI.Click += new System.EventHandler(this.btnFBPI_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkRed;
            this.ClientSize = new System.Drawing.Size(324, 175);
            this.Controls.Add(this.btnFBPI);
            this.Controls.Add(this.btnBPI);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BPI/FBPI Fix 1.0";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnBPI;
        private System.Windows.Forms.Button btnFBPI;
    }
}

