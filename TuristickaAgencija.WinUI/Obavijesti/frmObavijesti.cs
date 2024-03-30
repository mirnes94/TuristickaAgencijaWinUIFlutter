using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TuristickaAgencija.Model.Request;

namespace TuristickaAgencija.WinUI.Obavijesti
{
    public partial class frmObavijesti : Form
    {
        private readonly APIService _obavijestiService = new APIService("Obavijesti");
        private readonly APIService _korisniciService = new APIService("Korisnici");
        public frmObavijesti()
        {
            InitializeComponent();
        }

        private async void frmObavijesti_Load(object sender, EventArgs e)
        {
            await LoadKorisnici();
        }

        private async Task LoadObavijesti()
        {
            var result = await _obavijestiService.Get<List<Model.Obavijesti>>(null);
            dgvObavijesti.DataSource = result;
        }
        private async Task LoadObavijesti(int korisnikId)
        {
            var result = await _obavijestiService.Get<List<Model.Obavijesti>>(new ObavijestiSearchRequest()
            {
                KorisnikId = korisnikId

            });
            dgvObavijesti.DataSource = result;

        }
        private async Task LoadKorisnici()
        {

            var result = await _korisniciService.Get<List<Model.Korisnici>>(null);
            result.Insert(0, new Model.Korisnici());
            cmbKorisnik.DataSource = result;
            cmbKorisnik.DisplayMember = "KorisnickoIme";
            cmbKorisnik.ValueMember = "Id";
        }

        private async void cmbKorisnik_SelectedIndexChanged(object sender, EventArgs e)
        {
            var idObj = cmbKorisnik.SelectedValue;
            if (int.TryParse(idObj.ToString(), out int id))
            {
                if (id != 0)
                {
                    await LoadObavijesti(id);
                }
                else
                {
                    await LoadObavijesti();
                }

            }
            else
            {
                await LoadObavijesti();
            }
        }

        private void dgvObavijesti_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var id = dgvObavijesti.SelectedRows[0].Cells[0].Value;

            frmDodajObavijest frm = new frmDodajObavijest(int.Parse(id.ToString()));
            frm.Show();
        }
    }
}
