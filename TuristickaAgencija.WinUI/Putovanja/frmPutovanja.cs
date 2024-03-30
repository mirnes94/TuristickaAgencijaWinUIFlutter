using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TuristickaAgencija.Model.Request;

namespace TuristickaAgencija.WinUI.Putovanja
{
    public partial class frmPutovanja : Form
    {
        private readonly APIService _putovanjaService = new APIService("Putovanja");
        private readonly APIService _gradoviService = new APIService("Gradovi");
       
        public frmPutovanja()
        {
            InitializeComponent();
        }

        private async void frmPutovanja_Load(object sender, EventArgs e)
        {
            await LoadGradovi();
        }

        private async Task LoadPutovanja()
        {
            var result = await _putovanjaService.Get<List<Model.Putovanja>>(null);
            dgvPutovanja.DataSource = result;
        }
        private async Task LoadGradovi()
        {

            var result = await _gradoviService.Get<List<Model.Gradovi>>(null);
            result.Insert(0, new Model.Gradovi());
            cmbGradovi.DataSource = result;
            cmbGradovi.DisplayMember = "NazivGrada";
            cmbGradovi.ValueMember = "Id";
        }
       

        private async void cmbGradovi_SelectedIndexChanged(object sender, EventArgs e)
        {
            var idObj = cmbGradovi.SelectedValue;
            if (int.TryParse(idObj.ToString(), out int id))
            {
                if(id!=0)
                {
                    await LoadPutovanja(id);
                }
                else
                {
                    await LoadPutovanja();
                }
               
            }
            else
            {
                await LoadPutovanja();
            }
        }

        private async Task LoadPutovanja(int gradId)
        {
            var result = await _putovanjaService.Get<List<Model.Putovanja>>(new PutovanjaSearchRequest()
            {
                GradId = gradId,
               
            });
            dgvPutovanja.DataSource = result;

        }

        private void dgvPutovanja_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var id = dgvPutovanja.SelectedRows[0].Cells[0].Value;

            frmDodajPutovanje frm = new frmDodajPutovanje(int.Parse(id.ToString()));
            frm.Show();
        }
    }
}
