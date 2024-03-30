
namespace TuristickaAgencija.WinUI.Putovanja
{
    partial class frmDodajPutovanje
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
            this.txtBrojMjesta = new System.Windows.Forms.TextBox();
            this.txtCijena = new System.Windows.Forms.TextBox();
            this.txtOpisPutovanja = new System.Windows.Forms.TextBox();
            this.txtNazivPutovanja = new System.Windows.Forms.TextBox();
            this.dtDatumPolaska = new System.Windows.Forms.DateTimePicker();
            this.dtDatumDolaska = new System.Windows.Forms.DateTimePicker();
            this.cmbGrad = new System.Windows.Forms.ComboBox();
            this.cmbSmjestaj = new System.Windows.Forms.ComboBox();
            this.cmbPrevoz = new System.Windows.Forms.ComboBox();
            this.btnSacuvaj = new System.Windows.Forms.Button();
            this.NazivPutovanja = new System.Windows.Forms.Label();
            this.OpisPutovanja = new System.Windows.Forms.Label();
            this.Cijena = new System.Windows.Forms.Label();
            this.BrojMjesta = new System.Windows.Forms.Label();
            this.DatumPolaska = new System.Windows.Forms.Label();
            this.DatumDolaska = new System.Windows.Forms.Label();
            this.Destinacija = new System.Windows.Forms.Label();
            this.Smjestaj = new System.Windows.Forms.Label();
            this.Prevoz = new System.Windows.Forms.Label();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.btnUploadPicture = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.txtSlikaInput = new System.Windows.Forms.TextBox();
            this.clbVodici = new System.Windows.Forms.CheckedListBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // txtBrojMjesta
            // 
            this.txtBrojMjesta.Location = new System.Drawing.Point(135, 183);
            this.txtBrojMjesta.Name = "txtBrojMjesta";
            this.txtBrojMjesta.Size = new System.Drawing.Size(124, 23);
            this.txtBrojMjesta.TabIndex = 10;
            this.txtBrojMjesta.Validating += new System.ComponentModel.CancelEventHandler(this.txtBrojMjesta_Validating);
            // 
            // txtCijena
            // 
            this.txtCijena.Location = new System.Drawing.Point(34, 183);
            this.txtCijena.Name = "txtCijena";
            this.txtCijena.Size = new System.Drawing.Size(79, 23);
            this.txtCijena.TabIndex = 9;
            this.txtCijena.Validating += new System.ComponentModel.CancelEventHandler(this.txtCijena_Validating);
            // 
            // txtOpisPutovanja
            // 
            this.txtOpisPutovanja.Location = new System.Drawing.Point(30, 86);
            this.txtOpisPutovanja.Multiline = true;
            this.txtOpisPutovanja.Name = "txtOpisPutovanja";
            this.txtOpisPutovanja.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtOpisPutovanja.Size = new System.Drawing.Size(229, 70);
            this.txtOpisPutovanja.TabIndex = 8;
            this.txtOpisPutovanja.Validating += new System.ComponentModel.CancelEventHandler(this.txtOpisPutovanja_Validating);
            // 
            // txtNazivPutovanja
            // 
            this.txtNazivPutovanja.Location = new System.Drawing.Point(31, 42);
            this.txtNazivPutovanja.Name = "txtNazivPutovanja";
            this.txtNazivPutovanja.Size = new System.Drawing.Size(228, 23);
            this.txtNazivPutovanja.TabIndex = 7;
            this.txtNazivPutovanja.Validating += new System.ComponentModel.CancelEventHandler(this.txtNazivPutovanja_Validating);
            // 
            // dtDatumPolaska
            // 
            this.dtDatumPolaska.Location = new System.Drawing.Point(30, 234);
            this.dtDatumPolaska.Name = "dtDatumPolaska";
            this.dtDatumPolaska.Size = new System.Drawing.Size(229, 23);
            this.dtDatumPolaska.TabIndex = 11;
            // 
            // dtDatumDolaska
            // 
            this.dtDatumDolaska.Location = new System.Drawing.Point(31, 282);
            this.dtDatumDolaska.Name = "dtDatumDolaska";
            this.dtDatumDolaska.Size = new System.Drawing.Size(229, 23);
            this.dtDatumDolaska.TabIndex = 12;
            // 
            // cmbGrad
            // 
            this.cmbGrad.FormattingEnabled = true;
            this.cmbGrad.Location = new System.Drawing.Point(30, 329);
            this.cmbGrad.Name = "cmbGrad";
            this.cmbGrad.Size = new System.Drawing.Size(79, 23);
            this.cmbGrad.TabIndex = 14;
            // 
            // cmbSmjestaj
            // 
            this.cmbSmjestaj.FormattingEnabled = true;
            this.cmbSmjestaj.Location = new System.Drawing.Point(115, 329);
            this.cmbSmjestaj.Name = "cmbSmjestaj";
            this.cmbSmjestaj.Size = new System.Drawing.Size(68, 23);
            this.cmbSmjestaj.TabIndex = 15;
            // 
            // cmbPrevoz
            // 
            this.cmbPrevoz.FormattingEnabled = true;
            this.cmbPrevoz.Location = new System.Drawing.Point(195, 329);
            this.cmbPrevoz.Name = "cmbPrevoz";
            this.cmbPrevoz.Size = new System.Drawing.Size(70, 23);
            this.cmbPrevoz.TabIndex = 16;
            // 
            // btnSacuvaj
            // 
            this.btnSacuvaj.Location = new System.Drawing.Point(457, 323);
            this.btnSacuvaj.Name = "btnSacuvaj";
            this.btnSacuvaj.Size = new System.Drawing.Size(75, 23);
            this.btnSacuvaj.TabIndex = 17;
            this.btnSacuvaj.Text = "Sacuvaj";
            this.btnSacuvaj.UseVisualStyleBackColor = true;
            this.btnSacuvaj.Click += new System.EventHandler(this.btnSacuvaj_Click);
            // 
            // NazivPutovanja
            // 
            this.NazivPutovanja.AutoSize = true;
            this.NazivPutovanja.Location = new System.Drawing.Point(31, 27);
            this.NazivPutovanja.Name = "NazivPutovanja";
            this.NazivPutovanja.Size = new System.Drawing.Size(92, 15);
            this.NazivPutovanja.TabIndex = 18;
            this.NazivPutovanja.Text = "Naziv putovanja";
            // 
            // OpisPutovanja
            // 
            this.OpisPutovanja.AutoSize = true;
            this.OpisPutovanja.Location = new System.Drawing.Point(31, 68);
            this.OpisPutovanja.Name = "OpisPutovanja";
            this.OpisPutovanja.Size = new System.Drawing.Size(87, 15);
            this.OpisPutovanja.TabIndex = 19;
            this.OpisPutovanja.Text = "Opis putovanja";
            // 
            // Cijena
            // 
            this.Cijena.AutoSize = true;
            this.Cijena.Location = new System.Drawing.Point(34, 159);
            this.Cijena.Name = "Cijena";
            this.Cijena.Size = new System.Drawing.Size(40, 15);
            this.Cijena.TabIndex = 20;
            this.Cijena.Text = "Cijena";
            // 
            // BrojMjesta
            // 
            this.BrojMjesta.AutoSize = true;
            this.BrojMjesta.Location = new System.Drawing.Point(135, 157);
            this.BrojMjesta.Name = "BrojMjesta";
            this.BrojMjesta.Size = new System.Drawing.Size(66, 15);
            this.BrojMjesta.TabIndex = 21;
            this.BrojMjesta.Text = "Broj mjesta";
            // 
            // DatumPolaska
            // 
            this.DatumPolaska.AutoSize = true;
            this.DatumPolaska.Location = new System.Drawing.Point(31, 216);
            this.DatumPolaska.Name = "DatumPolaska";
            this.DatumPolaska.Size = new System.Drawing.Size(86, 15);
            this.DatumPolaska.TabIndex = 22;
            this.DatumPolaska.Text = "Datum polaska";
            // 
            // DatumDolaska
            // 
            this.DatumDolaska.AutoSize = true;
            this.DatumDolaska.Location = new System.Drawing.Point(32, 264);
            this.DatumDolaska.Name = "DatumDolaska";
            this.DatumDolaska.Size = new System.Drawing.Size(86, 15);
            this.DatumDolaska.TabIndex = 23;
            this.DatumDolaska.Text = "Datum dolaska";
            // 
            // Destinacija
            // 
            this.Destinacija.AutoSize = true;
            this.Destinacija.Location = new System.Drawing.Point(31, 311);
            this.Destinacija.Name = "Destinacija";
            this.Destinacija.Size = new System.Drawing.Size(64, 15);
            this.Destinacija.TabIndex = 24;
            this.Destinacija.Text = "Destinacija";
            // 
            // Smjestaj
            // 
            this.Smjestaj.AutoSize = true;
            this.Smjestaj.Location = new System.Drawing.Point(115, 311);
            this.Smjestaj.Name = "Smjestaj";
            this.Smjestaj.Size = new System.Drawing.Size(51, 15);
            this.Smjestaj.TabIndex = 25;
            this.Smjestaj.Text = "Smjestaj";
            // 
            // Prevoz
            // 
            this.Prevoz.AutoSize = true;
            this.Prevoz.Location = new System.Drawing.Point(195, 311);
            this.Prevoz.Name = "Prevoz";
            this.Prevoz.Size = new System.Drawing.Size(42, 15);
            this.Prevoz.TabIndex = 26;
            this.Prevoz.Text = "Prevoz";
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // pictureBox
            // 
            this.pictureBox.Location = new System.Drawing.Point(282, 42);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(250, 70);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox.TabIndex = 27;
            this.pictureBox.TabStop = false;
            // 
            // btnUploadPicture
            // 
            this.btnUploadPicture.Location = new System.Drawing.Point(457, 133);
            this.btnUploadPicture.Name = "btnUploadPicture";
            this.btnUploadPicture.Size = new System.Drawing.Size(75, 23);
            this.btnUploadPicture.TabIndex = 28;
            this.btnUploadPicture.Text = "Upload";
            this.btnUploadPicture.UseVisualStyleBackColor = true;
            this.btnUploadPicture.Click += new System.EventHandler(this.btnUploadPicture_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // txtSlikaInput
            // 
            this.txtSlikaInput.Location = new System.Drawing.Point(282, 133);
            this.txtSlikaInput.Name = "txtSlikaInput";
            this.txtSlikaInput.Size = new System.Drawing.Size(153, 23);
            this.txtSlikaInput.TabIndex = 29;
            this.txtSlikaInput.Validating += new System.ComponentModel.CancelEventHandler(this.txtSlikaInput_Validating);
            // 
            // clbVodici
            // 
            this.clbVodici.FormattingEnabled = true;
            this.clbVodici.Location = new System.Drawing.Point(282, 183);
            this.clbVodici.Name = "clbVodici";
            this.clbVodici.Size = new System.Drawing.Size(250, 130);
            this.clbVodici.TabIndex = 30;
            // 
            // frmDodajPutovanje
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(554, 358);
            this.Controls.Add(this.clbVodici);
            this.Controls.Add(this.txtSlikaInput);
            this.Controls.Add(this.btnUploadPicture);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.Prevoz);
            this.Controls.Add(this.Smjestaj);
            this.Controls.Add(this.Destinacija);
            this.Controls.Add(this.DatumDolaska);
            this.Controls.Add(this.DatumPolaska);
            this.Controls.Add(this.BrojMjesta);
            this.Controls.Add(this.Cijena);
            this.Controls.Add(this.OpisPutovanja);
            this.Controls.Add(this.NazivPutovanja);
            this.Controls.Add(this.btnSacuvaj);
            this.Controls.Add(this.cmbPrevoz);
            this.Controls.Add(this.cmbSmjestaj);
            this.Controls.Add(this.cmbGrad);
            this.Controls.Add(this.dtDatumDolaska);
            this.Controls.Add(this.dtDatumPolaska);
            this.Controls.Add(this.txtBrojMjesta);
            this.Controls.Add(this.txtCijena);
            this.Controls.Add(this.txtOpisPutovanja);
            this.Controls.Add(this.txtNazivPutovanja);
            this.Name = "frmDodajPutovanje";
            this.Text = "frmDodajPutovanje";
            this.Load += new System.EventHandler(this.frmDodajPutovanje_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtBrojMjesta;
        private System.Windows.Forms.TextBox txtCijena;
        private System.Windows.Forms.TextBox txtOpisPutovanja;
        private System.Windows.Forms.TextBox txtNazivPutovanja;
        private System.Windows.Forms.DateTimePicker dtDatumPolaska;
        private System.Windows.Forms.DateTimePicker dtDatumDolaska;
        private System.Windows.Forms.ComboBox cmbGrad;
        private System.Windows.Forms.ComboBox cmbSmjestaj;
        private System.Windows.Forms.ComboBox cmbPrevoz;
        private System.Windows.Forms.Button btnSacuvaj;
        private System.Windows.Forms.Label NazivPutovanja;
        private System.Windows.Forms.Label OpisPutovanja;
        private System.Windows.Forms.Label Cijena;
        private System.Windows.Forms.Label BrojMjesta;
        private System.Windows.Forms.Label DatumPolaska;
        private System.Windows.Forms.Label DatumDolaska;
        private System.Windows.Forms.Label Destinacija;
        private System.Windows.Forms.Label Smjestaj;
        private System.Windows.Forms.Label Prevoz;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.Button btnUploadPicture;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox txtSlikaInput;
        private System.Windows.Forms.CheckedListBox clbVodici;
    }
}