
namespace TuristickaAgencija.WinUI.Vodici
{
    partial class frmVodici
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
            this.Vodici = new System.Windows.Forms.GroupBox();
            this.dgvVodici = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtPrezime = new System.Windows.Forms.TextBox();
            this.txtIme = new System.Windows.Forms.TextBox();
            this.btnPrikazi = new System.Windows.Forms.Button();
            this.Vodici.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVodici)).BeginInit();
            this.SuspendLayout();
            // 
            // Vodici
            // 
            this.Vodici.Controls.Add(this.dgvVodici);
            this.Vodici.Location = new System.Drawing.Point(17, 95);
            this.Vodici.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Vodici.Name = "Vodici";
            this.Vodici.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Vodici.Size = new System.Drawing.Size(757, 393);
            this.Vodici.TabIndex = 0;
            this.Vodici.TabStop = false;
            this.Vodici.Text = "Vodici";
            this.Vodici.Enter += new System.EventHandler(this.Vodici_Enter);
            // 
            // dgvVodici
            // 
            this.dgvVodici.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVodici.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id});
            this.dgvVodici.Location = new System.Drawing.Point(9, 23);
            this.dgvVodici.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dgvVodici.Name = "dgvVodici";
            this.dgvVodici.RowHeadersWidth = 62;
            this.dgvVodici.RowTemplate.Height = 25;
            this.dgvVodici.Size = new System.Drawing.Size(740, 360);
            this.dgvVodici.TabIndex = 0;
            this.dgvVodici.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvVodici_CellContentClick);
            this.dgvVodici.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgvVodici_MouseDoubleClick);
            // 
            // Id
            // 
            this.Id.DataPropertyName = "ID";
            this.Id.HeaderText = "ID";
            this.Id.MinimumWidth = 8;
            this.Id.Name = "Id";
            this.Id.Visible = false;
            this.Id.Width = 150;
            // 
            // txtPrezime
            // 
            this.txtPrezime.Location = new System.Drawing.Point(283, 47);
            this.txtPrezime.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtPrezime.Name = "txtPrezime";
            this.txtPrezime.PlaceholderText = "Prezime";
            this.txtPrezime.Size = new System.Drawing.Size(198, 31);
            this.txtPrezime.TabIndex = 6;
            // 
            // txtIme
            // 
            this.txtIme.Location = new System.Drawing.Point(26, 47);
            this.txtIme.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtIme.Name = "txtIme";
            this.txtIme.PlaceholderText = "Ime";
            this.txtIme.Size = new System.Drawing.Size(198, 31);
            this.txtIme.TabIndex = 5;
            // 
            // btnPrikazi
            // 
            this.btnPrikazi.Location = new System.Drawing.Point(643, 47);
            this.btnPrikazi.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnPrikazi.Name = "btnPrikazi";
            this.btnPrikazi.Size = new System.Drawing.Size(123, 38);
            this.btnPrikazi.TabIndex = 4;
            this.btnPrikazi.Text = "Prikazi";
            this.btnPrikazi.UseVisualStyleBackColor = true;
            this.btnPrikazi.Click += new System.EventHandler(this.btnPrikazi_Click);
            // 
            // frmVodici
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(801, 492);
            this.Controls.Add(this.txtPrezime);
            this.Controls.Add(this.txtIme);
            this.Controls.Add(this.btnPrikazi);
            this.Controls.Add(this.Vodici);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmVodici";
            this.Text = "frmVodici";
            this.Load += new System.EventHandler(this.frmVodici_Load);
            this.Vodici.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvVodici)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox Vodici;
        private System.Windows.Forms.DataGridView dgvVodici;
        private System.Windows.Forms.TextBox txtPrezime;
        private System.Windows.Forms.TextBox txtIme;
        private System.Windows.Forms.Button btnPrikazi;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
    }
}