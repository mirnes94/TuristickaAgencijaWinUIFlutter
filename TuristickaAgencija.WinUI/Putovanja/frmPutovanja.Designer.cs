
namespace TuristickaAgencija.WinUI.Putovanja
{
    partial class frmPutovanja
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvPutovanja = new System.Windows.Forms.DataGridView();
            this.cmbGradovi = new System.Windows.Forms.ComboBox();
            this.Destinacija = new System.Windows.Forms.Label();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPutovanja)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvPutovanja);
            this.groupBox1.Location = new System.Drawing.Point(12, 102);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(750, 247);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Putovanja";
            // 
            // dgvPutovanja
            // 
            this.dgvPutovanja.AllowUserToAddRows = false;
            this.dgvPutovanja.AllowUserToDeleteRows = false;
            this.dgvPutovanja.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPutovanja.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID});
            this.dgvPutovanja.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPutovanja.Location = new System.Drawing.Point(3, 19);
            this.dgvPutovanja.Name = "dgvPutovanja";
            this.dgvPutovanja.ReadOnly = true;
            this.dgvPutovanja.RowTemplate.Height = 25;
            this.dgvPutovanja.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPutovanja.Size = new System.Drawing.Size(744, 225);
            this.dgvPutovanja.TabIndex = 0;
            this.dgvPutovanja.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgvPutovanja_MouseDoubleClick);
            // 
            // cmbGradovi
            // 
            this.cmbGradovi.FormattingEnabled = true;
            this.cmbGradovi.Location = new System.Drawing.Point(15, 53);
            this.cmbGradovi.Name = "cmbGradovi";
            this.cmbGradovi.Size = new System.Drawing.Size(203, 23);
            this.cmbGradovi.TabIndex = 3;
            this.cmbGradovi.SelectedIndexChanged += new System.EventHandler(this.cmbGradovi_SelectedIndexChanged);
            // 
            // Destinacija
            // 
            this.Destinacija.AutoSize = true;
            this.Destinacija.Location = new System.Drawing.Point(15, 35);
            this.Destinacija.Name = "Destinacija";
            this.Destinacija.Size = new System.Drawing.Size(64, 15);
            this.Destinacija.TabIndex = 6;
            this.Destinacija.Text = "Destinacija";
            // 
            // ID
            // 
            this.ID.DataPropertyName = "ID";
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Visible = false;
            // 
            // frmPutovanja
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(771, 351);
            this.Controls.Add(this.Destinacija);
            this.Controls.Add(this.cmbGradovi);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmPutovanja";
            this.Text = "frmPutovanja";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmPutovanja_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPutovanja)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvPutovanja;
        private System.Windows.Forms.ComboBox cmbGradovi;
        private System.Windows.Forms.ComboBox cmb;
        private System.Windows.Forms.Label Destinacija;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
    }
}