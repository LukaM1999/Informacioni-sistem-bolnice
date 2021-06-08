using System.Windows;
using Repozitorijum;
using Model;
using Servis;
using System.Threading;
using InformacioniSistemBolnice.Views.Pacijent;
using System.Globalization;
using System.Windows.Controls.Primitives;
using InformacioniSistemBolnice.ViewModels.PacijentViewModel;
using InformacioniSistemBolnice.Views.PacijentView;
using PropertyChanged;

namespace InformacioniSistemBolnice
{
    [AddINotifyPropertyChangedInterface]
    public partial class GlavniProzorPacijentaView : Window
    {
        public static Pacijent ulogovanPacijent;
        public GlavniProzorPacijentaView(string korisnickoIme, string lozinka)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("sr-Latn-RS");
            Thread.CurrentThread.CurrentCulture = new CultureInfo("sr-Latn-RS");
            InitializeComponent();
            Application.Current.MainWindow = this;
            InicijalizujProzor(korisnickoIme, lozinka);
            PokreniNiti();
        }

        private void InicijalizujProzor(string korisnickoIme, string lozinka)
        {
            TerminRepo.Instance.Deserijalizacija();
            PacijentRepo.Instance.Deserijalizacija();
            LekarRepo.Instance.Deserijalizacija();
            AnketaOBolniciRepo.Instance.Deserijalizacija();
            ulogovanPacijent = PacijentRepo.Instance.PronadjiUlogovanogPacijenta(korisnickoIme, lozinka);
            PacijentRepo.Instance.PostaviTermineUlogovanogPacijenta(ulogovanPacijent);
        }

        private void PokreniNiti()
        {
            new Thread(() => new ZavrsenTerminServis<Pacijent>().PokreniProveruZavrsenostiTermina(ulogovanPacijent)).Start();
            new Thread(() => AntiTrollServis.Instance.ProveriMalicioznostPacijenta(ulogovanPacijent.Jmbg)).Start();
            PrikazBolnickeAnketeServis.Instance.OtvoriAnketuOBolnici(ulogovanPacijent);
            ObavestenjePacijentaServis.Instance.UkljuciPodsetnike(ulogovanPacijent.Jmbg);
            new Thread(() => ObavestenjeTerapijeServis.Instance.UkljuciObavestenja(ulogovanPacijent)).Start();
        }

        private void OtvoriAnketu(object sender, RoutedEventArgs e)
        {
            //Termin izabranTermin = (Termin)listaZakazanihTermina.SelectedValue;
            //if (JeIzabranZavrsenTermin(izabranTermin)) new AnketaOLekaruFormaView(izabranTermin).Show();
        }

        private bool JeIzabranZavrsenTermin(Termin izabranTermin)
        {
            return izabranTermin is { Status: StatusTermina.zavrsen, AnketaOLekaru: null };
        }

        private void OtvoriAnamnezu(object sender, RoutedEventArgs e)
        {
            if (ulogovanPacijent.zdravstveniKarton is { Anamneza: { } })
                new AnamnezaView { DataContext = new AnamnezaViewModel(ulogovanPacijent) }.Show();
        }

        private void IzaberiProfil(object sender, RoutedEventArgs e)
        {
            NavigationMenuListBox.SelectedItem = "Profil";
        }

        private void IzaberiKalendar(object sender, RoutedEventArgs e)
        {
            NavigationMenuListBox.SelectedItem = "Kalendar";
        }

        private void IzaberiObavestenja(object sender, RoutedEventArgs e)
        {
            NavigationMenuListBox.SelectedItem = "Obaveštenja";
        }

        private void IzaberiIstoriju(object sender, RoutedEventArgs e)
        {
            NavigationMenuListBox.SelectedItem = "Istorija";
        }

        private void IzaberiTerapiju(object sender, RoutedEventArgs e)
        {
            NavigationMenuListBox.SelectedItem = "Terapija";
        }

        private void IzaberiPomoc(object sender, RoutedEventArgs e)
        {
            NavigationMenuListBox.SelectedItem = "Pomoc";
        }

    }
}
