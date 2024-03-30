using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using TuristickaAgencija.Model.Request;

namespace TuristickaAgencija.WinUI.Putovanja
{
    public partial class frmDodajPutovanje : Form
    {
        private readonly APIService _gradoviService = new APIService("Gradovi");
        private readonly APIService _smjestajService = new APIService("Smjestaj");
        private readonly APIService _prevozService = new APIService("Prevoz");
        private readonly APIService _putovanjaService = new APIService("Putovanja");
        private readonly APIService _firmaService = new APIService("Firma");
        private readonly APIService _vodiciService = new APIService("Vodic");
        private int? _id = null;
        private bool isFormValid = false;

        public frmDodajPutovanje(int? putovanjeId = null)
        {
            InitializeComponent();
            _id = putovanjeId;
        }

        private async void frmDodajPutovanje_Load(object sender, EventArgs e)
        {

            await LoadGradovi();
            await LoadSmjestaj();
            await LoadFirma();
            await LoadPutovanja();



        }

        private async Task LoadPutovanja()
        {
            var vodiciList = await _vodiciService.Get<List<Model.Vodic>>(null);
            clbVodici.DataSource = vodiciList;
            clbVodici.DisplayMember = "Ime";
            if (_id.HasValue)
            {
                var putovanje = await _putovanjaService.GetById<Model.Putovanja>((int)_id);
                txtBrojMjesta.Text = putovanje.BrojMjesta.ToString();
                txtCijena.Text = putovanje.CijenaPutovanja.ToString();
                txtNazivPutovanja.Text = putovanje.NazivPutovanja;
                txtOpisPutovanja.Text = putovanje.OpisPutovanja;
                dtDatumPolaska.Value = putovanje.DatumPolaska;
                dtDatumDolaska.Value = putovanje.DatumDolaska;
                var grad = await _gradoviService.GetById<Model.Gradovi>(putovanje.GradId);
                cmbGrad.Text = grad.NazivGrada;
                var prevoz = await _prevozService.GetById<Model.Prevoz>(putovanje.PrevozId);
                var firma = await _firmaService.GetById<Model.Firma>(prevoz.FirmaId);
                cmbPrevoz.Text = firma.Naziv.ToString();
                var smjestaj = await _smjestajService.GetById<Model.Smjestaj>(putovanje.SmjestajId);
                cmbSmjestaj.Text = smjestaj.NazivSmjestaja.ToString();
            }
        }

        private async Task LoadGradovi()
        {

            var result = await _gradoviService.Get<List<Model.Gradovi>>(null);
            result.Insert(0, new Model.Gradovi());
            cmbGrad.DataSource = result;
            cmbGrad.DisplayMember = "NazivGrada";
            cmbGrad.ValueMember = "Id";
        }
        private async Task LoadSmjestaj()
        {

            var result = await _smjestajService.Get<List<Model.Smjestaj>>(null);
            result.Insert(0, new Model.Smjestaj());
            cmbSmjestaj.DataSource = result;
            cmbSmjestaj.DisplayMember = "NazivSmjestaja";
            cmbSmjestaj.ValueMember = "Id";
        }

        private async Task LoadFirma()
        {

            var result = await _firmaService.Get<List<Model.Firma>>(null);
            result.Insert(0, new Model.Firma());
            cmbPrevoz.DataSource = result;
            cmbPrevoz.DisplayMember = "Naziv";
            cmbPrevoz.ValueMember = "Id";
        }
        private async Task LoadPrevoz()
        {

            var result = await _prevozService.Get<List<Model.Prevoz>>(null);
            
            result.Insert(0, new Model.Prevoz());
            cmbPrevoz.DataSource = result;
            //var firme= await _firmaService.Get<List<Model.Firma>>(null);
            cmbPrevoz.DisplayMember = "FirmaId";
            cmbPrevoz.ValueMember = "Id";
        }
        PutovanjaInsertUpdateRequest request = new PutovanjaInsertUpdateRequest();
        private async void btnSacuvaj_Click(object sender, EventArgs e)
        {
            if (isFormValid)
            {
                if (string.IsNullOrWhiteSpace(txtSlikaInput.Text))
                {
                    errorProvider.SetError(txtSlikaInput, "Morate odabrati sliku");
                    isFormValid = false;
                }
                else
                {


                    var vodiciList = clbVodici.CheckedItems.Cast<Model.Vodic>().Select(x => x.Id).ToList();
                    var idGrad = cmbGrad.SelectedValue;
                    if (int.TryParse(idGrad.ToString(), out int GradId))
                    {
                        request.GradId = GradId;
                    }
                    var idSmjestaj = cmbSmjestaj.SelectedValue;
                    if (int.TryParse(idSmjestaj.ToString(), out int SmjestajId))
                    {
                        request.SmjestajId = SmjestajId;
                    }
                    var idPrevoz = cmbPrevoz.SelectedValue;
                    if (int.TryParse(idPrevoz.ToString(), out int PrevozId))
                    {
                        request.PrevozId = PrevozId;
                    }
                    request.BrojMjesta = int.Parse(txtBrojMjesta.Text);
                    request.CijenaPutovanja = float.Parse(txtCijena.Text);
                    request.DatumPolaska = dtDatumPolaska.Value;
                    request.DatumDolaska = dtDatumDolaska.Value;
                    request.NazivPutovanja = txtNazivPutovanja.Text;
                    request.OpisPutovanja = txtOpisPutovanja.Text;
                    request.Vodici = vodiciList;
                    if (_id.HasValue)
                    {
                        await _putovanjaService.Update<Model.Putovanja>((int)_id, request);
                    }
                    else
                    {
                        await _putovanjaService.Insert<Model.Putovanja>(request);

                    }

                    MessageBox.Show("Operacija uspješna");
                }
            }
            else
            {

                MessageBox.Show("Molimo Vas da ispravno popunite sva obavezna polja prije nego što nastavite.");
            }
        }

        private void txtNazivPutovanja_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNazivPutovanja.Text))
            {
                errorProvider.SetError(txtNazivPutovanja, "Obavezno polje");
                isFormValid = false;
            }
            else
            {
                isFormValid = true;
                errorProvider.SetError(txtNazivPutovanja, null);
            }
        }

        private void txtOpisPutovanja_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtOpisPutovanja.Text))
            {
                errorProvider.SetError(txtOpisPutovanja, "Obavezno polje");
                isFormValid = false;
            }
            else
            {
                isFormValid = true;
                errorProvider.SetError(txtOpisPutovanja, null);
            }
        }

        private void txtCijena_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCijena.Text))
            {
                errorProvider.SetError(txtCijena, "Obavezno polje");
                isFormValid = false;
            }
            else if (!IsDecimalNumber(txtCijena.Text))
            {
                errorProvider.SetError(txtCijena, "Cijena mora biti pozitivan broj");
                isFormValid = false;
            }
            else
            {
                isFormValid = true;
                errorProvider.SetError(txtCijena, null);
            }
        }

        private void txtBrojMjesta_Validating(object sender, CancelEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtBrojMjesta.Text))
            {
                errorProvider.SetError(txtBrojMjesta, "Obavezno polje");
                isFormValid = false;
            }
            else if (!IsNumber(txtBrojMjesta.Text))
            {
                errorProvider.SetError(txtBrojMjesta, "Broj mjesta nije u ispravnom formatu");
                isFormValid = false;
            }
            else
            {
                isFormValid = true;
                errorProvider.SetError(txtBrojMjesta, null);
            }
        }

        private void btnUploadPicture_Click(object sender, EventArgs e)
        {
            var result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                var fileName = openFileDialog1.FileName;

                var file = File.ReadAllBytes(fileName);

                request.Slika = file;

                txtSlikaInput.Text = fileName;

                Image image = Image.FromFile(fileName);

                pictureBox.Image = image;

                isFormValid = true;
            }
            else
            {
                isFormValid = false;
            }

        }

        private void txtSlikaInput_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSlikaInput.Text))
            {
                errorProvider.SetError(txtSlikaInput, "Obavezno polje");
                isFormValid = false;
            }
            else
            {
                isFormValid = true;
                errorProvider.SetError(txtSlikaInput, null);
            }
        }

        static bool IsNumber(string input)
        {
            string pattern = @"^\+?\d+$";
            return Regex.IsMatch(input, pattern);
        }

        static bool IsDecimalNumber(string input)
        {
            string pattern = @"^?\d+(\.\d+)?$";
            bool isDecimalNumber = Regex.IsMatch(input, pattern);
            return isDecimalNumber;
        }

      
    }
}
