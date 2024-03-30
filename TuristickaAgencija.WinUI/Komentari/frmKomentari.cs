using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TuristickaAgencija.Model.Request;

namespace TuristickaAgencija.WinUI.Komentari
{
    public partial class frmKomentari : Form
    {
        private readonly APIService _komentariService = new APIService("Komentar");
        private readonly APIService _korisniciService = new APIService("Korisnici");
        public frmKomentari()
        {
            InitializeComponent();
        }

        private async void frmKomentari_Load(object sender, EventArgs e)
        {
           await LoadKorisnici();
        }
        private async Task LoadKomentari()
        {
            var result = await _komentariService.Get<List<Model.Komentar>>(null);
            dgvKomentari.DataSource = result;
        }
        private async Task LoadKomentari(int putovanjeId)
        {
            var result = await _komentariService.Get<List<Model.Komentar>>(new KomentarSearchRequest()
            {
                PutovanjeId=putovanjeId

            });
            dgvKomentari.DataSource = result;

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
                    await LoadKomentari(id);
                }
                else
                {
                    await LoadKomentari();
                }

            }
            else
            {
                await LoadKomentari();
            }
        }
    }
}
