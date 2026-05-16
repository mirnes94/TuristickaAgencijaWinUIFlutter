using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace TuristickaAgencija.WinUI
{
    public partial class frmLogin : Form
    {
        APIService _service = new APIService("Korisnici");

        public frmLogin()
        {
            InitializeComponent();
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            APIService.Username = txtKorisnickoIme.Text;
            APIService.Password = txtLozinka.Text;

            var url = $"{Properties.Settings.Default.APIUrl}/Korisnici/Authenticiraj/{APIService.Username},{APIService.Password}";


            if (CheckConnection(url, APIService.Username, APIService.Password))
            {
                try
                {

                    if (string.IsNullOrEmpty(txtKorisnickoIme.Text) || string.IsNullOrEmpty(txtLozinka.Text))
                    {
                        MessageBox.Show("All fields are required! Try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    else
                    {

                        Model.Korisnici korisnik = await _service.Authentication<Model.Korisnici>(txtKorisnickoIme.Text, txtLozinka.Text);

                        MessageBox.Show("Welcome:\n " + korisnik.Ime + " " + korisnik.Prezime);
                        DialogResult = DialogResult.OK;
                        this.Hide();

                        frmIndex frm = new frmIndex();
                        frm.Show();
                    }


                }
                catch (Exception err)
                {
                    MessageBox.Show("Wrong username or password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            else
            {
                MessageBox.Show("Problem with connection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }


        }

        private bool CheckConnection(string URL, string username, string password)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
                request.Timeout = 5000;

                // Priprema Basic Auth headera
                string credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}"));
                request.Headers[HttpRequestHeader.Authorization] = "Basic " + credentials;

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.BadGateway ||
                    response.StatusCode == HttpStatusCode.BadRequest ||
                    response.StatusCode == HttpStatusCode.GatewayTimeout)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (WebException webEx)
            {
                Console.WriteLine($"WebException: {webEx.Message}");
                if (webEx.Response is HttpWebResponse errorResponse)
                {
                    Console.WriteLine($"Status code: {(int)errorResponse.StatusCode} - {errorResponse.StatusCode}");
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return false;
            }
        }


    }
}
