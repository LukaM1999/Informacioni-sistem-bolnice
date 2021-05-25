using System.Windows;
using System.Windows.Controls;
using Model;

namespace InformacioniSistemBolnice
{
    public partial class PregledGostujucegNaloga : UserControl
    {
        public PocetnaStranicaSekretara pocetna;
        public GostujuciNaloziProzor gostujuciNalozi;

        public PregledGostujucegNaloga(GostujuciNaloziProzor gostujuciNaloziProzor, ListView listaGostujucihNaloga)
        {
            InitializeComponent();
            gostujuciNalozi = gostujuciNaloziProzor;
            pocetna = gostujuciNaloziProzor.pocetna;
            if (listaGostujucihNaloga.SelectedValue != null)
                PrikaziPodatkeGostujucegPacijenta((Pacijent)listaGostujucihNaloga.SelectedItem);
        }

        private void PrikaziPodatkeGostujucegPacijenta(Pacijent pacijent)
        {
            ime.Content = pacijent.Ime;
            prezime.Content = pacijent.Prezime;
            datum.Content = pacijent.DatumRodjenja.ToString("dd/MM/yyyy");
            JMBG.Content = pacijent.Jmbg;
            tel.Content = pacijent.Telefon;
            mail.Content = pacijent.Email;
        }

        private void NazadBtn_Click(object sender, RoutedEventArgs e)
            => pocetna.contentControl.Content = new GostujuciNaloziProzor(this.pocetna);
    }
}
