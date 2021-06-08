using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Repozitorijum;
using Model;
using Servis;
using Kontroler;
using System.Collections.ObjectModel;
using InformacioniSistemBolnice.ViewModels.SekretarViewModel;

namespace InformacioniSistemBolnice
{
    public partial class PacijentiProzor : UserControl
    {
        public PocetnaStranicaSekretara pocetna;
        public ObservableCollection<Pacijent> pacijenti = new ObservableCollection<Pacijent>();
       

        public PacijentiProzor(PocetnaStranicaSekretara pocetnaStranicaSekretara)
        {
            InitializeComponent();
            pocetna = pocetnaStranicaSekretara;
            PrikaziPacijente();
        }

        private void PrikaziPacijente()
        {
            PacijentRepo.Instance.Deserijalizacija();
            foreach (Pacijent pacijent in PacijentRepo.Instance.Pacijenti)
                if (pacijent.Korisnik.KorisnickoIme != null) pacijenti.Add(pacijent);
            ListaPacijenata.ItemsSource = pacijenti.ToList();
        }

        private void RegistrujPacijenta_Click(object sender, RoutedEventArgs e)
        {
            pocetna.contentControl.Content = new RegistracijaPacijentaForma() 
            { DataContext = new RegistracijaPacijentaViewModel(this) }; 
        }

        private void ObrisiPacijenta_Click(object sender, RoutedEventArgs e)
        {
            if (ListaPacijenata.SelectedValue != null)
                NalogPacijentaKontroler.Instance.UklanjanjeNaloga((Pacijent)ListaPacijenata.SelectedItem);
            pocetna.contentControl.Content = new PacijentiProzor(pocetna);
        }

        private void IzmeniPacijenta_Click(object sender, RoutedEventArgs e)
        {
            Pacijent izabraniPacijent = (Pacijent)ListaPacijenata.SelectedValue;
            if (izabraniPacijent is not null)
                pocetna.contentControl.Content = new IzmenaNalogaPacijentaForma(this, izabraniPacijent); 
        }

        private void VidiPacijenta_Click(object sender, RoutedEventArgs e)
        {
            Pacijent izabraniPacijent = (Pacijent)ListaPacijenata.SelectedValue;
            if (izabraniPacijent is not null)
                pocetna.contentControl.Content = new PregledNalogaPacijenta()
                { DataContext = new PregledNalogaPacijentaViewModel(this, izabraniPacijent) }; ;
        }

        private void ZdravstveniKartonPacijenta_Click(object sender, RoutedEventArgs e)
        {
            Pacijent izabraniPacijent = (Pacijent)ListaPacijenata.SelectedValue;
            if (izabraniPacijent is not null && izabraniPacijent.zdravstveniKarton is not null)
                pocetna.contentControl.Content = new PregledZdravstvenogKartona()
                { DataContext = new PregledZdravstvenogKartonaViewModel(this, izabraniPacijent) };
            else MessageBox.Show("Nije kreiran zdravstveni karton za pacijenta");
        }

        private void IzmeniZdravstveniKarton_Click(object sender, RoutedEventArgs e)
        {
            Pacijent izabraniPacijent = (Pacijent)ListaPacijenata.SelectedValue;
            if (izabraniPacijent is not null && izabraniPacijent.zdravstveniKarton is not null)
                pocetna.contentControl.Content = new IzmenaZdravstvenogKartonaForma(this, pocetna, izabraniPacijent);
            else MessageBox.Show("Nije kreiran zdravstveni karton za pacijenta");
        }
    }
}
