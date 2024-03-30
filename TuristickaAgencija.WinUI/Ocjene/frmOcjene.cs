using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TuristickaAgencija.Model.Request;

namespace TuristickaAgencija.WinUI.Ocjene
{
    public partial class frmOcjene : Form
    {
        private readonly APIService _gradoviService = new APIService("Gradovi");
        private readonly APIService _ocjeneService = new APIService("Ocjene");
        private readonly APIService _putovanjaService = new APIService("Putovanja");
        public frmOcjene()
        {
            InitializeComponent();
        }

        private async void frmOcjene_Load(object sender, EventArgs e)
        {
           //await LoadGradovi();
           await LoadPutovanja();
        }

        private async Task LoadOcjene()
        {
            var result = await _ocjeneService.Get<List<Model.Ocjene>>(null);
            dgvOcjene.DataSource = result;
        }
        private async Task LoadOcjene(int putovanjeId)
        {
            var result = await _ocjeneService.Get<List<Model.Ocjene>>(new OcjeneSearchRequest()
            {
                PutovanjeId=putovanjeId

            });
            dgvOcjene.DataSource = result;

        }
        private async Task LoadGradovi()
        {

            var result = await _gradoviService.Get<List<Model.Gradovi>>(null);
            //result.Insert(0, new Model.Gradovi());
            cmbPutovanje.DataSource = result;
            cmbPutovanje.DisplayMember = "NazivGrada";
            cmbPutovanje.ValueMember = "Id";
        }
        private async Task LoadPutovanja()
        {

            var result = await _putovanjaService.Get<List<Model.Putovanja>>(null);
            result.Insert(0, new Model.Putovanja());
            cmbPutovanje.DataSource = result;
            cmbPutovanje.DisplayMember = "NazivPutovanja";
            cmbPutovanje.ValueMember = "Id";
        }

        private async void cmbPutovanje_SelectedIndexChanged(object sender, EventArgs e)
        {
            var idObj = cmbPutovanje.SelectedValue;
            if (int.TryParse(idObj.ToString(), out int id))
            {
                if (id != 0)
                {
                    await LoadOcjene(id);
                }
                else
                {
                    await LoadOcjene();
                }

            }
            else
            {
                await LoadOcjene();
            }
        }
    }
}
