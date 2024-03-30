using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using TuristickaAgencija.Model.Request;

namespace TuristickaAgencija.WinUI.Korisnici
{
    public partial class frmKorisniciDetalji : Form
    {
        private readonly APIService _service = new APIService("Korisnici");
        private readonly APIService _ulogeService = new APIService("Uloge");
        private int? _id = null;
        private bool isFormValid = false;

        public frmKorisniciDetalji(int? korisnikId=null)
        {
            InitializeComponent();
            _id = korisnikId;
        }

        private async  void btnSacuvaj_ClickAsync(object sender, EventArgs e)
        {
           
            if (isFormValid)
            {
                //var roleList = clbRole.CheckedItems.Cast<Model.Uloge>().ToList();
                var roleList = clbRole.CheckedItems.Cast<Model.Uloge>().Select(x => x.Id).ToList();
                var request = new KorisniciInsertUpdateRequest()
                {
                    Email = txtEmail.Text,
                    Ime = txtIme.Text,
                    KorisnickoIme = txtKorisnickoIme.Text,
                    Password = txtPassword.Text,
                    PasswordConfirmation = txtPasswordPotvrda.Text,
                    Prezime = txtPrezime.Text,
                    Telefon = txtTelefon.Text,
                    Status = cbxStatus.Checked,
                    Uloge = roleList
                    
                };

                if (_id.HasValue)
                {
                    await _service.Update<Model.Korisnici>((int)_id, request);
                }
                else
                {
                    await _service.Insert<Model.Korisnici>(request);

                }
                MessageBox.Show("Operacija uspješna");
            }
            else
            {
                MessageBox.Show("Molimo Vas da ispravno popunite sva obavezna polja prije nego što nastavite.");
            }


        }

        private async void frmKorisniciDetalji_Load(object sender, EventArgs e)
        {
            var ulogeList = await _ulogeService.Get<List<Model.Uloge>>(null);
            clbRole.DataSource = ulogeList;
            clbRole.DisplayMember = "Naziv";
            if (_id.HasValue)
            {
                var korisnik = await _service.GetById<Model.Korisnici>((int)_id);
                txtEmail.Text = korisnik.Email;
                txtIme.Text = korisnik.Ime;
                txtKorisnickoIme.Text = korisnik.KorisnickoIme;
                txtPrezime.Text = korisnik.Prezime;
                txtTelefon.Text = korisnik.Telefon;
                cbxStatus.Checked = korisnik.Status;
               

            }
        }

        private void txtIme_Validating(object sender, CancelEventArgs e)
        {
         
            if (string.IsNullOrWhiteSpace(txtIme.Text))
            {
                //errorProvider.SetError(txtIme,Properties.Resources.Validation_RequiredField);
                errorProvider.SetError(txtIme, "Obavezno polje");
                isFormValid = false;
            }
            else
            {
                isFormValid = true;
                errorProvider.SetError(txtIme, null);
            }
        }

        private void txtPrezime_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPrezime.Text))
            {
                errorProvider.SetError(txtPrezime, "Obavezno polje");
                isFormValid = false;
            }
            else
            {
                isFormValid = true;
                errorProvider.SetError(txtPrezime, null);
            }
        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                errorProvider.SetError(txtEmail, "Obavezno polje");
                isFormValid = false;
            }
            else if (!IsValidEmail(txtEmail.Text))
            {
                errorProvider.SetError(txtEmail, "Email nije u ispravnom formatu");
                isFormValid = false;
            }
            else
            {
                isFormValid = true;
                errorProvider.SetError(txtEmail, null);
            }
        }

        private void txtTelefon_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTelefon.Text))
            {
                errorProvider.SetError(txtTelefon, "Obavezno polje");
                isFormValid = false;
            }
            else if (!IsValidPhoneNumber(txtTelefon.Text))
            {
                errorProvider.SetError(txtTelefon, "Broj telefona nije u ispravnom formatu");
                isFormValid = false;
            }
            else
            {
                isFormValid = true;
                errorProvider.SetError(txtTelefon, null);
            }
        }


        private void txtKorisnickoIme_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtKorisnickoIme.Text) && txtKorisnickoIme.Text.Length<3)
            {
                errorProvider.SetError(txtKorisnickoIme, "Obavezno polje");
                isFormValid = false;
            }
            else
            {
                isFormValid = true;
                errorProvider.SetError(txtKorisnickoIme, null);
            }
        }

        private void txtKorisnickoIme_TextChanged(object sender, EventArgs e)
        {

        }

        static bool IsValidEmail(string email)
        {
           
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, pattern);
        }

        static bool IsValidPhoneNumber(string phoneNumber)
        {
            string pattern = @"^\+?\d+$";
            return Regex.IsMatch(phoneNumber, pattern);
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

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        } 
    }


}
