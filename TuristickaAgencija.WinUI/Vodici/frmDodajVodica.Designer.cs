
namespace TuristickaAgencija.WinUI.Vodici
{
    partial class frmDodajVodica
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
            this.Prezime = new System.Windows.Forms.Label();
            this.txtPrezime = new System.Windows.Forms.TextBox();
            this.Ime = new System.Windows.Forms.Label();
            this.txtIme = new System.Windows.Forms.TextBox();
            this.Kontakt = new System.Windows.Forms.Label();
            this.txtKontakt = new System.Windows.Forms.TextBox();
            this.Jmbg = new System.Windows.Forms.Label();
            this.txtJmbg = new System.Windows.Forms.TextBox();
            this.txtSlikaInput = new System.Windows.Forms.TextBox();
            this.btnUploadPicture = new System.Windows.Forms.Button();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.btnSacuvaj = new System.Windows.Forms.Button();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // Prezime
            // 
            this.Prezime.AutoSize = true;
            this.Prezime.Location = new System.Drawing.Point(17, 147);
            this.Prezime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Prezime.Name = "Prezime";
            this.Prezime.Size = new System.Drawing.Size(74, 25);
            this.Prezime.TabIndex = 7;
            this.Prezime.Text = "Prezime";
            // 
            // txtPrezime
            // 
            this.txtPrezime.Location = new System.Drawing.Point(16, 177);
            this.txtPrezime.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtPrezime.Name = "txtPrezime";
            this.txtPrezime.Size = new System.Drawing.Size(285, 31);
            this.txtPrezime.TabIndex = 6;
            // 
            // Ime
            // 
            this.Ime.AutoSize = true;
            this.Ime.Location = new System.Drawing.Point(19, 52);
            this.Ime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Ime.Name = "Ime";
            this.Ime.Size = new System.Drawing.Size(42, 25);
            this.Ime.TabIndex = 5;
            this.Ime.Text = "Ime";
            // 
            // txtIme
            // 
            this.txtIme.Location = new System.Drawing.Point(17, 87);
            this.txtIme.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtIme.Name = "txtIme";
            this.txtIme.Size = new System.Drawing.Size(284, 31);
            this.txtIme.TabIndex = 4;
            // 
            // Kontakt
            // 
            this.Kontakt.AutoSize = true;
            this.Kontakt.Location = new System.Drawing.Point(17, 242);
            this.Kontakt.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Kontakt.Name = "Kontakt";
            this.Kontakt.Size = new System.Drawing.Size(73, 25);
            this.Kontakt.TabIndex = 9;
            this.Kontakt.Text = "Kontakt";
            // 
            // txtKontakt
            // 
            this.txtKontakt.Location = new System.Drawing.Point(16, 272);
            this.txtKontakt.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtKontakt.Name = "txtKontakt";
            this.txtKontakt.Size = new System.Drawing.Size(285, 31);
            this.txtKontakt.TabIndex = 8;
            // 
            // Jmbg
            // 
            this.Jmbg.AutoSize = true;
            this.Jmbg.Location = new System.Drawing.Point(17, 337);
            this.Jmbg.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Jmbg.Name = "Jmbg";
            this.Jmbg.Size = new System.Drawing.Size(56, 25);
            this.Jmbg.TabIndex = 11;
            this.Jmbg.Text = "JMBG";
            // 
            // txtJmbg
            // 
            this.txtJmbg.Location = new System.Drawing.Point(16, 367);
            this.txtJmbg.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtJmbg.Name = "txtJmbg";
            this.txtJmbg.Size = new System.Drawing.Size(285, 31);
            this.txtJmbg.TabIndex = 10;
            // 
            // txtSlikaInput
            // 
            this.txtSlikaInput.Location = new System.Drawing.Point(359, 218);
            this.txtSlikaInput.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtSlikaInput.Name = "txtSlikaInput";
            this.txtSlikaInput.Size = new System.Drawing.Size(217, 31);
            this.txtSlikaInput.TabIndex = 32;
            // 
            // btnUploadPicture
            // 
            this.btnUploadPicture.Location = new System.Drawing.Point(594, 217);
            this.btnUploadPicture.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnUploadPicture.Name = "btnUploadPicture";
            this.btnUploadPicture.Size = new System.Drawing.Size(107, 38);
            this.btnUploadPicture.TabIndex = 31;
            this.btnUploadPicture.Text = "Upload";
            this.btnUploadPicture.UseVisualStyleBackColor = true;
            this.btnUploadPicture.Click += new System.EventHandler(this.btnUploadPicture_Click);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // btnSacuvaj
            // 
            this.btnSacuvaj.Location = new System.Drawing.Point(594, 367);
            this.btnSacuvaj.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSacuvaj.Name = "btnSacuvaj";
            this.btnSacuvaj.Size = new System.Drawing.Size(107, 38);
            this.btnSacuvaj.TabIndex = 33;
            this.btnSacuvaj.Text = "Sacuvaj";
            this.btnSacuvaj.UseVisualStyleBackColor = true;
            this.btnSacuvaj.Click += new System.EventHandler(this.btnSacuvaj_Click);
            // 
            // pictureBox
            // 
            this.pictureBox.Location = new System.Drawing.Point(359, 87);
            this.pictureBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(343, 120);
            this.pictureBox.TabIndex = 34;
            this.pictureBox.TabStop = false;
            // 
            // frmDodajVodica
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(736, 442);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.btnSacuvaj);
            this.Controls.Add(this.txtSlikaInput);
            this.Controls.Add(this.btnUploadPicture);
            this.Controls.Add(this.Jmbg);
            this.Controls.Add(this.txtJmbg);
            this.Controls.Add(this.Kontakt);
            this.Controls.Add(this.txtKontakt);
            this.Controls.Add(this.Prezime);
            this.Controls.Add(this.txtPrezime);
            this.Controls.Add(this.Ime);
            this.Controls.Add(this.txtIme);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmDodajVodica";
            this.Text = "frmDodajVodica";
            this.Load += new System.EventHandler(this.frmDodajVodica_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Prezime;
        private System.Windows.Forms.TextBox txtPrezime;
        private System.Windows.Forms.Label Ime;
        private System.Windows.Forms.TextBox txtIme;
        private System.Windows.Forms.Label Kontakt;
        private System.Windows.Forms.TextBox txtKontakt;
        private System.Windows.Forms.Label Jmbg;
        private System.Windows.Forms.TextBox txtJmbg;
        private System.Windows.Forms.TextBox txtSlikaInput;
        private System.Windows.Forms.Button btnUploadPicture;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button btnSacuvaj;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox;
    }
}