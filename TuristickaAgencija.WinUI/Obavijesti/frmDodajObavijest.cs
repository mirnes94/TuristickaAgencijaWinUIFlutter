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

    public partial class frmDodajObavijest : Form
    {
        private readonly APIService _korisniciService = new APIService("Korisnici");
        private readonly APIService _obavijestiService = new APIService("Obavijesti");
        private int? _id = null;


        public frmDodajObavijest(int? obavijestId = null)
        {
            InitializeComponent();
            btnDodajObavijest.CausesValidation = true;
            _id = obavijestId;
        }
        private async Task LoadKorisnici()
        {

            var result = await _korisniciService.Get<List<Model.Korisnici>>(null);
            result.Insert(0, new Model.Korisnici());
            cmbKorisnik.DataSource = result;
            cmbKorisnik.DisplayMember = "KorisnickoIme";
            cmbKorisnik.ValueMember = "Id";
        }

        private async void frmDodajObavijest_Load(object sender, EventArgs e)
        {
            await LoadKorisnici();
            await LoadObavijesti();

        }

        private async Task LoadObavijesti()
        {
            if (_id.HasValue)
            {
                var obavijest = await _obavijestiService.GetById<Model.Obavijesti>((int)_id);
                txtNazivObavijesti.Text = obavijest.Naziv;
                txtSadrzajObavijesti.Text = obavijest.Sadrzaj;
                var korisnik = await _korisniciService.GetById<Model.Korisnici>((int)_id);
                cmbKorisnik.Text = korisnik.KorisnickoIme;
            }
        }
        private async void btnDodajObavijest_Click(object sender, EventArgs e)
        {
            
            if (isFormValidAll())
            {
                ObavijestiInsertUpdateRequest request = new ObavijestiInsertUpdateRequest();
                var idKorisnik = cmbKorisnik.SelectedValue;
                if (int.TryParse(idKorisnik.ToString(), out int KorisnikId))
                {
                    request.KorisnikId = KorisnikId;
                }


                
                request.Datum = DateTime.Now;
                request.Naziv = txtNazivObavijesti.Text;
                request.Sadrzaj = txtSadrzajObavijesti.Text;

                if (_id.HasValue)
                {
                    await _obavijestiService.Update<Model.Obavijesti>((int)_id, request);
                }
                else
                {
                    await _obavijestiService.Insert<Model.Obavijesti>(request);
                }

                errorProvider1.SetError(txtNazivObavijesti, null);
                errorProvider1.SetError(txtSadrzajObavijesti, null);
                errorProvider1.SetError(cmbKorisnik, null);


                MessageBox.Show("Operacija uspješna");
            }
            else
            {
                MessageBox.Show("Molimo Vas da ispravno popunite sva obavezna polja prije nego što nastavite.");
            }
        }
       

        private bool isFormValidAll()
        {
            bool valid = true;

            // Provjera txtNazivObavijesti
            if (string.IsNullOrWhiteSpace(txtNazivObavijesti.Text))
            {
                errorProvider1.SetError(txtNazivObavijesti, "Obavezno polje");
                valid = false;
            }

            // Provjera txtSadrzajObavijesti
            if (string.IsNullOrWhiteSpace(txtSadrzajObavijesti.Text))
            {
                errorProvider1.SetError(txtSadrzajObavijesti, "Obavezno polje");
                valid = false;
            }

            // Provjera cmbKorisnik
            if (string.IsNullOrEmpty(cmbKorisnik.Text))
            {
                errorProvider1.SetError(cmbKorisnik, "Obavezno polje");
                valid = false;
            }

            return valid;
        }



        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
            return;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
