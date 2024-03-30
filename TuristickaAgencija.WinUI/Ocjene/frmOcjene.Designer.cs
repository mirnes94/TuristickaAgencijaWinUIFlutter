
namespace TuristickaAgencija.WinUI.Ocjene
{
    partial class frmOcjene
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
            this.Ocjene = new System.Windows.Forms.GroupBox();
            this.dgvOcjene = new System.Windows.Forms.DataGridView();
            this.cmbPutovanje = new System.Windows.Forms.ComboBox();
            this.Putovanje = new System.Windows.Forms.Label();
            this.Ocjene.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOcjene)).BeginInit();
            this.SuspendLayout();
            // 
            // Ocjene
            // 
            this.Ocjene.Controls.Add(this.dgvOcjene);
            this.Ocjene.Location = new System.Drawing.Point(12, 68);
            this.Ocjene.Name = "Ocjene";
            this.Ocjene.Size = new System.Drawing.Size(531, 231);
            this.Ocjene.TabIndex = 0;
            this.Ocjene.TabStop = false;
            this.Ocjene.Text = "Ocjene";
            // 
            // dgvOcjene
            // 
            this.dgvOcjene.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOcjene.Location = new System.Drawing.Point(6, 22);
            this.dgvOcjene.Name = "dgvOcjene";
            this.dgvOcjene.RowTemplate.Height = 25;
            this.dgvOcjene.Size = new System.Drawing.Size(519, 203);
            this.dgvOcjene.TabIndex = 0;
            // 
            // cmbPutovanje
            // 
            this.cmbPutovanje.FormattingEnabled = true;
            this.cmbPutovanje.Location = new System.Drawing.Point(416, 29);
            this.cmbPutovanje.Name = "cmbPutovanje";
            this.cmbPutovanje.Size = new System.Drawing.Size(121, 23);
            this.cmbPutovanje.TabIndex = 1;
            this.cmbPutovanje.SelectedIndexChanged += new System.EventHandler(this.cmbPutovanje_SelectedIndexChanged);
            // 
            // Putovanje
            // 
            this.Putovanje.AutoSize = true;
            this.Putovanje.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Putovanje.Location = new System.Drawing.Point(336, 29);
            this.Putovanje.Name = "Putovanje";
            this.Putovanje.Size = new System.Drawing.Size(74, 20);
            this.Putovanje.TabIndex = 2;
            this.Putovanje.Text = "Putovanje";
            // 
            // frmOcjene
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(555, 311);
            this.Controls.Add(this.Putovanje);
            this.Controls.Add(this.cmbPutovanje);
            this.Controls.Add(this.Ocjene);
            this.Name = "frmOcjene";
            this.Text = "frmOcjene";
            this.Load += new System.EventHandler(this.frmOcjene_Load);
            this.Ocjene.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOcjene)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox Ocjene;
        private System.Windows.Forms.DataGridView dgvOcjene;
        private System.Windows.Forms.ComboBox cmbPutovanje;
        private System.Windows.Forms.Label Putovanje;
    }
}