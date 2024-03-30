
namespace TuristickaAgencija.WinUI.Komentari
{
    partial class frmKomentari
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
            this.Komentari = new System.Windows.Forms.GroupBox();
            this.dgvKomentari = new System.Windows.Forms.DataGridView();
            this.cmbKorisnik = new System.Windows.Forms.ComboBox();
            this.Korisnik = new System.Windows.Forms.Label();
            this.Komentari.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKomentari)).BeginInit();
            this.SuspendLayout();
            // 
            // Komentari
            // 
            this.Komentari.Controls.Add(this.dgvKomentari);
            this.Komentari.Location = new System.Drawing.Point(12, 65);
            this.Komentari.Name = "Komentari";
            this.Komentari.Size = new System.Drawing.Size(547, 221);
            this.Komentari.TabIndex = 0;
            this.Komentari.TabStop = false;
            this.Komentari.Text = "Komentari";
            // 
            // dgvKomentari
            // 
            this.dgvKomentari.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvKomentari.Location = new System.Drawing.Point(6, 22);
            this.dgvKomentari.Name = "dgvKomentari";
            this.dgvKomentari.RowTemplate.Height = 25;
            this.dgvKomentari.Size = new System.Drawing.Size(535, 193);
            this.dgvKomentari.TabIndex = 0;
            // 
            // cmbKorisnik
            // 
            this.cmbKorisnik.FormattingEnabled = true;
            this.cmbKorisnik.Location = new System.Drawing.Point(432, 20);
            this.cmbKorisnik.Name = "cmbKorisnik";
            this.cmbKorisnik.Size = new System.Drawing.Size(121, 23);
            this.cmbKorisnik.TabIndex = 1;
            this.cmbKorisnik.SelectedIndexChanged += new System.EventHandler(this.cmbKorisnik_SelectedIndexChanged);
            // 
            // Korisnik
            // 
            this.Korisnik.AutoSize = true;
            this.Korisnik.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Korisnik.Location = new System.Drawing.Point(365, 23);
            this.Korisnik.Name = "Korisnik";
            this.Korisnik.Size = new System.Drawing.Size(61, 20);
            this.Korisnik.TabIndex = 2;
            this.Korisnik.Text = "Korisnik";
            // 
            // frmKomentari
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(586, 298);
            this.Controls.Add(this.Korisnik);
            this.Controls.Add(this.cmbKorisnik);
            this.Controls.Add(this.Komentari);
            this.Name = "frmKomentari";
            this.Text = "frmKomentari";
            this.Load += new System.EventHandler(this.frmKomentari_Load);
            this.Komentari.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvKomentari)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox Komentari;
        private System.Windows.Forms.DataGridView dgvKomentari;
        private System.Windows.Forms.ComboBox cmbKorisnik;
        private System.Windows.Forms.Label Korisnik;
    }
}