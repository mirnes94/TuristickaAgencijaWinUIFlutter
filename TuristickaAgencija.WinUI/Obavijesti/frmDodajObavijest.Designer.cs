
namespace TuristickaAgencija.WinUI.Obavijesti
{
    partial class frmDodajObavijest
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
            this.txtSadrzajObavijesti = new System.Windows.Forms.TextBox();
            this.txtNazivObavijesti = new System.Windows.Forms.TextBox();
            this.cmbKorisnik = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnDodajObavijest = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtSadrzajObavijesti
            // 
            this.txtSadrzajObavijesti.Location = new System.Drawing.Point(29, 123);
            this.txtSadrzajObavijesti.Multiline = true;
            this.txtSadrzajObavijesti.Name = "txtSadrzajObavijesti";
            this.txtSadrzajObavijesti.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSadrzajObavijesti.Size = new System.Drawing.Size(229, 77);
            this.txtSadrzajObavijesti.TabIndex = 13;
            //this.txtSadrzajObavijesti.Validating += new System.ComponentModel.CancelEventHandler(this.txtSadrzajObavijesti_Validating);
            // 
            // txtNazivObavijesti
            // 
            this.txtNazivObavijesti.Location = new System.Drawing.Point(30, 72);
            this.txtNazivObavijesti.Name = "txtNazivObavijesti";
            this.txtNazivObavijesti.Size = new System.Drawing.Size(228, 23);
            this.txtNazivObavijesti.TabIndex = 12;
            //this.txtNazivObavijesti.Validating += new System.ComponentModel.CancelEventHandler(this.txtNazivObavijesti_Validating);
            // 
            // cmbKorisnik
            // 
            this.cmbKorisnik.FormattingEnabled = true;
            this.cmbKorisnik.Location = new System.Drawing.Point(30, 232);
            this.cmbKorisnik.Name = "cmbKorisnik";
            this.cmbKorisnik.Size = new System.Drawing.Size(228, 23);
            this.cmbKorisnik.TabIndex = 15;
            //this.cmbKorisnik.Validating += new System.ComponentModel.CancelEventHandler(this.cmbKorisnik_Validating);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 15);
            this.label1.TabIndex = 16;
            this.label1.Text = "Naziv obavijesti";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 105);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 15);
            this.label2.TabIndex = 17;
            this.label2.Text = "Sadržaj obavijesti";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 214);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 15);
            this.label3.TabIndex = 18;
            this.label3.Text = "Korisnik";
            // 
            // btnDodajObavijest
            // 
            this.btnDodajObavijest.Location = new System.Drawing.Point(79, 274);
            this.btnDodajObavijest.Name = "btnDodajObavijest";
            this.btnDodajObavijest.Size = new System.Drawing.Size(127, 23);
            this.btnDodajObavijest.TabIndex = 19;
            this.btnDodajObavijest.Text = "Dodaj obavijest";
            this.btnDodajObavijest.UseVisualStyleBackColor = true;
            this.btnDodajObavijest.Click += new System.EventHandler(this.btnDodajObavijest_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // frmDodajObavijest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(296, 317);
            this.Controls.Add(this.btnDodajObavijest);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbKorisnik);
            this.Controls.Add(this.txtSadrzajObavijesti);
            this.Controls.Add(this.txtNazivObavijesti);
            this.Name = "frmDodajObavijest";
            this.Text = "frmDodajObavijest";
            this.Load += new System.EventHandler(this.frmDodajObavijest_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtSadrzajObavijesti;
        private System.Windows.Forms.TextBox txtNazivObavijesti;
        private System.Windows.Forms.ComboBox cmbKorisnik;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnDodajObavijest;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}