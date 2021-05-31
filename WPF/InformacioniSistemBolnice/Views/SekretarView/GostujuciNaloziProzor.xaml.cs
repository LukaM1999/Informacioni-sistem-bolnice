using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Repozitorijum;
using Model;
using Kontroler;
using InformacioniSistemBolnice.ViewModels.SekretarViewModel;

namespace InformacioniSistemBolnice
{
    public partial class GostujuciNaloziProzor : UserControl
    {
        public PocetnaStranicaSekretara pocetna;
        public ObservableCollection<Pacijent> gostujuciNalozi = new ObservableCollection<Pacijent>();

        public GostujuciNaloziProzor(PocetnaStranicaSekretara pocetnaStranicaSekretara)
        {
            InitializeComponent();
            PacijentRepo.Instance.Deserijalizacija();
            pocetna = pocetnaStranicaSekretara;
            GenerisiGostujuceNaloge();
        }

        private void GenerisiGostujuceNaloge()
        {
            foreach (Pacijent gostujuciPacijent in PacijentRepo.Instance.Pacijenti)
                if (gostujuciPacijent.Korisnik.KorisnickoIme == null) gostujuciNalozi.Add(gostujuciPacijent);
            listaGostujucihNaloga.ItemsSource = gostujuciNalozi.ToList();
        }

        private void UkloniGostujuciNalog_Click(object sender, RoutedEventArgs e)
        {
            SekretarKontroler.Instance.UklanjanjeGostujucegNaloga((Pacijent)listaGostujucihNaloga.SelectedItem);
            pocetna.contentControl.Content = new GostujuciNaloziProzor(pocetna);
        }

        private void KreirajGostujuciNalog_Click(object sender, RoutedEventArgs e)
        {
            this.pocetna.contentControl.Content = new KreiranjeGostujucegPacijentaProzor()
            { DataContext = new KreiranjeGostujucegNalogaViewModel(this)};
        }

        private void VidiGostujuciNalog_Click(object sender, RoutedEventArgs e)
        {
            if (listaGostujucihNaloga.SelectedValue != null)
                this.pocetna.contentControl.Content = new PregledGostujucegNaloga()
                { DataContext = new PregledGostujucegNalogaViewModel(this, (Pacijent)listaGostujucihNaloga.SelectedItem) };
        }
    }
}




