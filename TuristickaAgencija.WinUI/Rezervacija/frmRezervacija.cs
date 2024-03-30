using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TuristickaAgencija.Model.Request;

namespace TuristickaAgencija.WinUI.Rezervacija
{
    public partial class frmRezervacija : Form
    {
        private readonly APIService _rezervacijaService = new APIService("Rezervacija");
        private readonly APIService _korisniciService = new APIService("Korisnici");
     
      
        public frmRezervacija()
        {
            InitializeComponent();
        }

        private async void frmRezervacija_Load(object sender, EventArgs e)
        {
            await LoadKorisnici();
            await LoadRezervacije();
        }

        private async Task LoadRezervacije(int korisnikId)
        {
            
         
            if(korisnikId != 0)
            {
                var result = await _rezervacijaService.Get<List<Model.Rezervacija>>(new RezervacijaSearchRequest()
                {
                    KorisnikId = korisnikId,
                   

                });
                dgvRezervacije.DataSource = result;
            }
            else
            {
                var result = await _rezervacijaService.Get<List<Model.Rezervacija>>(null);
                dgvRezervacije.DataSource = result;
            }
           




        }
        private async Task LoadRezervacije()
        {         
                var result = await _rezervacijaService.Get<List<Model.Rezervacija>>(null);
                dgvRezervacije.DataSource = result;         
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
                await LoadRezervacije(idKorisnik);

            }
         

          
        }

       
    }
}
