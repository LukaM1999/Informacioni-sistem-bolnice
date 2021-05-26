using System.Windows;
using System.Windows.Controls;
using InformacioniSistemBolnice.DTO;
using Kontroler;
using Model;
using Repozitorijum;

namespace InformacioniSistemBolnice
{
    public partial class IzmenaNalogaLekaraForma : UserControl
    {
        public PocetnaStranicaSekretara pocetna;
        public Lekari lekari;
        public ListView listaLekara;
        public Lekar Lekar { get; set; }
        public IzmenaNalogaLekaraForma(PocetnaStranicaSekretara pocetnaStranicaSekretara, 
                                        Lekari uCLekari, Lekar izabraniLekar)
        {
            Lekar = izabraniLekar;
            InitializeComponent();
            pocetna = pocetnaStranicaSekretara;
            lekari = uCLekari;
            listaLekara = lekari.ListaLekara;
            SpecijalizacijaRepo.Instance.Deserijalizacija();
            specijalizacije.ItemsSource = SpecijalizacijaRepo.Instance.Specijalizacije;
        }

        private void PotvrdiDugme_Click(object sender, RoutedEventArgs e)
        {
            Adresa adresa = Lekar.AdresaStanovanja;
            Korisnik korisnik = Lekar.Korisnik;
            LekarDto lekarDto = new(Lekar.Ime, Lekar.Prezime, Lekar.Jmbg, Lekar.DatumRodjenja, adresa.Drzava, adresa.Grad,
                                    adresa.Ulica, adresa.Broj, Lekar.Telefon, Lekar.Email, korisnik.KorisnickoIme,
                                    korisnik.Lozinka, Lekar.Specijalizacija.Naziv);
            SekretarKontroler.Instance.IzmenaNalogaLekara(lekarDto, (Lekar)listaLekara.SelectedValue);
            pocetna.contentControl.Content = new Lekari(pocetna);
        }

        private void NazadBtn_Click(object sender, RoutedEventArgs e)
            => pocetna.contentControl.Content = new Lekari(pocetna);

    }
}
