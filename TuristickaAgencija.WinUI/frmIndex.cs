using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TuristickaAgencija.WinUI.Komentari;
using TuristickaAgencija.WinUI.Korisnici;
using TuristickaAgencija.WinUI.Obavijesti;
using TuristickaAgencija.WinUI.Ocjene;
using TuristickaAgencija.WinUI.Putovanja;
using TuristickaAgencija.WinUI.Rezervacija;
using TuristickaAgencija.WinUI.Uplate;
using TuristickaAgencija.WinUI.Vodici;

namespace TuristickaAgencija.WinUI
{
    public partial class frmIndex : Form
    {
        private int childFormNumber = 0;

        public frmIndex()
        {
            InitializeComponent();
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Window " + childFormNumber++;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }
        /*
        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }*/

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

   

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }
        private void pretragaKorisnikaToolStripMenuItem_Click(object sender, EventArgs e)
        {         
            frmKorisnici frm = new frmKorisnici();
            frm.MdiParent = this;
            frm.Show();        
        }
        private void noviKorisnikToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmKorisniciDetalji frm = new frmKorisniciDetalji();
            frm.MdiParent = this;
            frm.Show();
        }
       
        private void pretragaPutovanjaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPutovanja frm = new frmPutovanja();
            frm.MdiParent = this;
            frm.Show();
        }
        private void novoPutovanjeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDodajPutovanje frm = new frmDodajPutovanje();
            frm.MdiParent = this;
            frm.Show();
        }
        private void pregledRezervacijaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRezervacija frm = new frmRezervacija();
            frm.MdiParent = this;
            frm.Show();
        }
        private void pregledUplataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUplate frm = new frmUplate();
            frm.MdiParent = this;
            frm.Show();
        }
        private void pregledKomentaraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmKomentari frm = new frmKomentari();
            frm.MdiParent = this;
            frm.Show();
        }
        private void pregledOcjenaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmOcjene frm = new frmOcjene();
            frm.MdiParent = this;
            frm.Show();
        }
        private void pregledObavijestiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmObavijesti frm = new frmObavijesti();
            frm.MdiParent = this;
            frm.Show();
        }
        private void novaObavijestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDodajObavijest frm = new frmDodajObavijest();
            frm.MdiParent = this;
            frm.Show();
        }
        private void pretragaVodicaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmVodici frm = new frmVodici();
            frm.MdiParent = this;
            frm.Show();
        }
        private void noviVodicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDodajVodica frm = new frmDodajVodica();
            frm.MdiParent = this;
            frm.Show();
        }

    }
}
