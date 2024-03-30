
namespace TuristickaAgencija.WinUI.Obavijesti
{
    partial class frmObavijesti
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
            this.Obavijesti = new System.Windows.Forms.GroupBox();
            this.dgvObavijesti = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmbKorisnik = new System.Windows.Forms.ComboBox();
            this.Korisnik = new System.Windows.Forms.Label();
            this.Obavijesti.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvObavijesti)).BeginInit();
            this.SuspendLayout();
            // 
            // Obavijesti
            // 
            this.Obavijesti.Controls.Add(this.dgvObavijesti);
            this.Obavijesti.Location = new System.Drawing.Point(12, 65);
            this.Obavijesti.Name = "Obavijesti";
            this.Obavijesti.Size = new System.Drawing.Size(454, 240);
            this.Obavijesti.TabIndex = 0;
            this.Obavijesti.TabStop = false;
            this.Obavijesti.Text = "Obavijesti";
            // 
            // dgvObavijesti
            // 
            this.dgvObavijesti.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvObavijesti.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id});
            this.dgvObavijesti.Location = new System.Drawing.Point(6, 22);
            this.dgvObavijesti.Name = "dgvObavijesti";
            this.dgvObavijesti.RowTemplate.Height = 25;
            this.dgvObavijesti.Size = new System.Drawing.Size(448, 212);
            this.dgvObavijesti.TabIndex = 0;
            this.dgvObavijesti.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgvObavijesti_MouseDoubleClick);
            // 
            // Id
            // 
            this.Id.DataPropertyName = "ID";
            this.Id.HeaderText = "ID";
            this.Id.Name = "Id";
            this.Id.Visible = false;
            // 
            // cmbKorisnik
            // 
            this.cmbKorisnik.FormattingEnabled = true;
            this.cmbKorisnik.Location = new System.Drawing.Point(345, 26);
            this.cmbKorisnik.Name = "cmbKorisnik";
            this.cmbKorisnik.Size = new System.Drawing.Size(121, 23);
            this.cmbKorisnik.TabIndex = 1;
            this.cmbKorisnik.SelectedIndexChanged += new System.EventHandler(this.cmbKorisnik_SelectedIndexChanged);
            // 
            // Korisnik
            // 
            this.Korisnik.AutoSize = true;
            this.Korisnik.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Korisnik.Location = new System.Drawing.Point(275, 25);
            this.Korisnik.Name = "Korisnik";
            this.Korisnik.Size = new System.Drawing.Size(61, 20);
            this.Korisnik.TabIndex = 2;
            this.Korisnik.Text = "Korisnik";
            // 
            // frmObavijesti
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(474, 317);
            this.Controls.Add(this.Korisnik);
            this.Controls.Add(this.cmbKorisnik);
            this.Controls.Add(this.Obavijesti);
            this.Name = "frmObavijesti";
            this.Text = "frmObavijesti";
            this.Load += new System.EventHandler(this.frmObavijesti_Load);
            this.Obavijesti.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvObavijesti)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox Obavijesti;
        private System.Windows.Forms.DataGridView dgvObavijesti;
        private System.Windows.Forms.ComboBox cmbKorisnik;
        private System.Windows.Forms.Label Korisnik;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
    }
}