
using System;

namespace TuristickaAgencija.WinUI
{
    partial class frmIndex
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
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.Korisnici = new System.Windows.Forms.ToolStripMenuItem();
            this.pretragaKorisnika = new System.Windows.Forms.ToolStripMenuItem();
            this.noviKorisnik = new System.Windows.Forms.ToolStripMenuItem();
            this.Putovanja = new System.Windows.Forms.ToolStripMenuItem();
            this.pretragaPutovanja = new System.Windows.Forms.ToolStripMenuItem();
            this.novoPutovanje = new System.Windows.Forms.ToolStripMenuItem();
            this.Rezervacije = new System.Windows.Forms.ToolStripMenuItem();
            this.PregledRezervacija = new System.Windows.Forms.ToolStripMenuItem();
            this.Uplate = new System.Windows.Forms.ToolStripMenuItem();
            this.PregledUplata = new System.Windows.Forms.ToolStripMenuItem();
            this.Komentari = new System.Windows.Forms.ToolStripMenuItem();
            this.PregledKomentara = new System.Windows.Forms.ToolStripMenuItem();
            this.Ocjene = new System.Windows.Forms.ToolStripMenuItem();
            this.PregledOcjena = new System.Windows.Forms.ToolStripMenuItem();
            this.Obavijesti = new System.Windows.Forms.ToolStripMenuItem();
            this.PregledObavijesti = new System.Windows.Forms.ToolStripMenuItem();
            this.NovaObavijest = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.Vodici = new System.Windows.Forms.ToolStripMenuItem();
            this.PretragaVodica = new System.Windows.Forms.ToolStripMenuItem();
            this.DodajVodica = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Korisnici,
            this.Putovanja,
            this.Rezervacije,
            this.Uplate,
            this.Komentari,
            this.Ocjene,
            this.Obavijesti,
            this.Vodici});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menuStrip.Size = new System.Drawing.Size(737, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "MenuStrip";
            // 
            // Korisnici
            // 
            this.Korisnici.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pretragaKorisnika,
            this.noviKorisnik});
            this.Korisnici.ImageTransparentColor = System.Drawing.SystemColors.ActiveBorder;
            this.Korisnici.Name = "Korisnici";
            this.Korisnici.Size = new System.Drawing.Size(64, 20);
            this.Korisnici.Text = "Korisnici";
            // 
            // pretragaKorisnika
            // 
            this.pretragaKorisnika.Name = "pretragaKorisnika";
            this.pretragaKorisnika.Size = new System.Drawing.Size(168, 22);
            this.pretragaKorisnika.Text = "Pretraga korisnika";
            this.pretragaKorisnika.Click += new System.EventHandler(this.pretragaKorisnikaToolStripMenuItem_Click);
            // 
            // noviKorisnik
            // 
            this.noviKorisnik.Name = "noviKorisnik";
            this.noviKorisnik.Size = new System.Drawing.Size(168, 22);
            this.noviKorisnik.Text = "Novi korisnik";
            this.noviKorisnik.Click += new System.EventHandler(this.noviKorisnikToolStripMenuItem_Click);
            // 
            // Putovanja
            // 
            this.Putovanja.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pretragaPutovanja,
            this.novoPutovanje});
            this.Putovanja.Name = "Putovanja";
            this.Putovanja.Size = new System.Drawing.Size(72, 20);
            this.Putovanja.Text = "Putovanja";
            // 
            // pretragaPutovanja
            // 
            this.pretragaPutovanja.Name = "pretragaPutovanja";
            this.pretragaPutovanja.Size = new System.Drawing.Size(174, 22);
            this.pretragaPutovanja.Text = "Pretraga putovanja";
            this.pretragaPutovanja.Click += new System.EventHandler(this.pretragaPutovanjaToolStripMenuItem_Click);
            // 
            // novoPutovanje
            // 
            this.novoPutovanje.Name = "novoPutovanje";
            this.novoPutovanje.Size = new System.Drawing.Size(174, 22);
            this.novoPutovanje.Text = "Novo putovanje";
            this.novoPutovanje.Click += new System.EventHandler(this.novoPutovanjeToolStripMenuItem_Click);
            // 
            // Rezervacije
            // 
            this.Rezervacije.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PregledRezervacija});
            this.Rezervacije.Name = "Rezervacije";
            this.Rezervacije.Size = new System.Drawing.Size(77, 20);
            this.Rezervacije.Text = "Rezervacije";
            // 
            // PregledRezervacija
            // 
            this.PregledRezervacija.Name = "PregledRezervacija";
            this.PregledRezervacija.Size = new System.Drawing.Size(172, 22);
            this.PregledRezervacija.Text = "Pregled rezervacija";
            this.PregledRezervacija.Click += new System.EventHandler(this.pregledRezervacijaToolStripMenuItem_Click);
            // 
            // Uplate
            // 
            this.Uplate.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PregledUplata});
            this.Uplate.Name = "Uplate";
            this.Uplate.Size = new System.Drawing.Size(53, 20);
            this.Uplate.Text = "Uplate";
            // 
            // PregledUplata
            // 
            this.PregledUplata.Name = "PregledUplata";
            this.PregledUplata.Size = new System.Drawing.Size(150, 22);
            this.PregledUplata.Text = "Pregled uplata";
            this.PregledUplata.Click += new System.EventHandler(this.pregledUplataToolStripMenuItem_Click);
            // 
            // Komentari
            // 
            this.Komentari.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PregledKomentara});
            this.Komentari.Name = "Komentari";
            this.Komentari.Size = new System.Drawing.Size(74, 20);
            this.Komentari.Text = "Komentari";
            // 
            // PregledKomentara
            // 
            this.PregledKomentara.Name = "PregledKomentara";
            this.PregledKomentara.Size = new System.Drawing.Size(174, 22);
            this.PregledKomentara.Text = "Pregled komentara";
            this.PregledKomentara.Click += new System.EventHandler(this.pregledKomentaraToolStripMenuItem_Click);
            // 
            // Ocjene
            // 
            this.Ocjene.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PregledOcjena});
            this.Ocjene.Name = "Ocjene";
            this.Ocjene.Size = new System.Drawing.Size(56, 20);
            this.Ocjene.Text = "Ocjene";
            // 
            // PregledOcjena
            // 
            this.PregledOcjena.Name = "PregledOcjena";
            this.PregledOcjena.Size = new System.Drawing.Size(152, 22);
            this.PregledOcjena.Text = "Pregled ocjena";
            this.PregledOcjena.Click += new System.EventHandler(this.pregledOcjenaToolStripMenuItem_Click);
            // 
            // Obavijesti
            // 
            this.Obavijesti.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PregledObavijesti,
            this.NovaObavijest});
            this.Obavijesti.Name = "Obavijesti";
            this.Obavijesti.Size = new System.Drawing.Size(71, 20);
            this.Obavijesti.Text = "Obavijesti";
            // 
            // PregledObavijesti
            // 
            this.PregledObavijesti.Name = "PregledObavijesti";
            this.PregledObavijesti.Size = new System.Drawing.Size(167, 22);
            this.PregledObavijesti.Text = "Pregled obavijesti";
            this.PregledObavijesti.Click += new System.EventHandler(this.pregledObavijestiToolStripMenuItem_Click);
            // 
            // NovaObavijest
            // 
            this.NovaObavijest.Name = "NovaObavijest";
            this.NovaObavijest.Size = new System.Drawing.Size(167, 22);
            this.NovaObavijest.Text = "Nova obavijest";
            this.NovaObavijest.Click += new System.EventHandler(this.novaObavijestToolStripMenuItem_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 501);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.statusStrip.Size = new System.Drawing.Size(737, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "StatusStrip";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(39, 17);
            this.toolStripStatusLabel.Text = "Status";
            // 
            // Vodici
            // 
            this.Vodici.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PretragaVodica,
            this.DodajVodica});
            this.Vodici.Name = "Vodici";
            this.Vodici.Size = new System.Drawing.Size(51, 20);
            this.Vodici.Text = "Vodici";
            // 
            // PretragaVodica
            // 
            this.PretragaVodica.Name = "PretragaVodica";
            this.PretragaVodica.Size = new System.Drawing.Size(156, 22);
            this.PretragaVodica.Text = "Pretraga vodica";
            this.PretragaVodica.Click += new System.EventHandler(this.pretragaVodicaToolStripMenuItem_Click);
            // 
            // DodajVodica
            // 
            this.DodajVodica.Name = "DodajVodica";
            this.DodajVodica.Size = new System.Drawing.Size(156, 22);
            this.DodajVodica.Text = "Dodaj vodica";
            this.DodajVodica.Click += new System.EventHandler(this.noviVodicToolStripMenuItem_Click);
            // 
            // frmIndex
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(737, 523);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "frmIndex";
            this.Text = "frmIndex";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

       


        #endregion


        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolStripMenuItem Korisnici;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ToolStripMenuItem pretragaKorisnika;
        private System.Windows.Forms.ToolStripMenuItem noviKorisnik;
        private System.Windows.Forms.ToolStripMenuItem Putovanja;
        private System.Windows.Forms.ToolStripMenuItem pretragaPutovanja;
        private System.Windows.Forms.ToolStripMenuItem novoPutovanje;
        private System.Windows.Forms.ToolStripMenuItem Rezervacije;
        private System.Windows.Forms.ToolStripMenuItem PregledRezervacija;
        private System.Windows.Forms.ToolStripMenuItem Uplate;
        private System.Windows.Forms.ToolStripMenuItem PregledUplata;
        private System.Windows.Forms.ToolStripMenuItem Komentari;
        private System.Windows.Forms.ToolStripMenuItem PregledKomentara;
        private System.Windows.Forms.ToolStripMenuItem Ocjene;
        private System.Windows.Forms.ToolStripMenuItem PregledOcjena;
        private System.Windows.Forms.ToolStripMenuItem Obavijesti;
        private System.Windows.Forms.ToolStripMenuItem PregledObavijesti;
        private System.Windows.Forms.ToolStripMenuItem NovaObavijest;
        private System.Windows.Forms.ToolStripMenuItem Vodici;
        private System.Windows.Forms.ToolStripMenuItem PretragaVodica;
        private System.Windows.Forms.ToolStripMenuItem DodajVodica;
    }
}



