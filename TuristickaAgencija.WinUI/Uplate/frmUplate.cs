using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TuristickaAgencija.Model.Request;

namespace TuristickaAgencija.WinUI.Uplate
{
    public partial class frmUplate : Form
    {
        private readonly APIService _uplateService = new APIService("Uplate");
        private readonly APIService _korisniciService = new APIService("Korisnici");
        public frmUplate()
        {
            InitializeComponent();
        }

        private async void frmUplate_Load(object sender, EventArgs e)
        {
            await LoadKorisnici();
            await LoadUplate();
        }

        private async Task LoadUplate()
        {
            var result = await _uplateService.Get<List<Model.Uplate>>(null);
            dgvUplate.DataSource = result;
        }

        private async Task LoadUplate(int korisnikId)
        {


            if (korisnikId != 0)
            {
                var result = await _uplateService.Get<List<Model.Uplate>>(new UplateSearchRequest()
                {
                    KorisnikId = korisnikId,


                });
                dgvUplate.DataSource = result;
            }
            else
            {
                var result = await _uplateService.Get<List<Model.Uplate>>(null);
                dgvUplate.DataSource = result;
            }

        }
        private async Task LoadKorisnici()
        {

            var result = await _korisniciService.Get<List<Model.Korisnici>>(null);
            result.Insert(0, new Model.Korisnici());
            cmbKorisnici.DataSource = result;
            cmbKorisnici.DisplayMember = "KorisnickoIme";
            cmbKorisnici.ValueMember = "Id";
        }

        private async void cmbKorisnici_SelectedIndexChanged(object sender, EventArgs e)
        {
            var idKor = cmbKorisnici.SelectedValue;

            if (int.TryParse(idKor.ToString(), out int idKorisnik))
            {
                await LoadUplate(idKorisnik);

            }
        }
    }
}
