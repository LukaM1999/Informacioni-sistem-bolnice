using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Repozitorijum;
using Model;
using System.Collections.ObjectModel;
using Servis;
using InformacioniSistemBolnice;
using Kontroler;

namespace InformacioniSistemBolnice
{
    public partial class TerminiPacijentaProzor : Window
    {

        //public ObservableCollection<Termin> listaTerminaUlogovanog;
        public Pacijent ulogovanPacijent;

        public TerminiPacijentaProzor(string korisnickoIme, string lozinka)
        {
            InitializeComponent();

            Termini.Instance.Deserijalizacija("../../../json/zakazaniTermini.json");
            Pacijenti.Instance.Deserijalizacija("../../../json/pacijenti.json");
            Lekari.Instance.Deserijalizacija("../../../json/lekari.json");
            
            foreach (Pacijent pacijent in Pacijenti.Instance.listaPacijenata)
            {
                if (pacijent.korisnik.korisnickoIme.Equals(korisnickoIme) && pacijent.korisnik.lozinka.Equals(lozinka))
                {
                    ulogovanPacijent = pacijent;
                    break;
                }
            }

            listaZakazanihTermina.ItemsSource = ulogovanPacijent.zakazaniTermini;


        }

        /*
        public void NadjiTermine(string korisnickoIme, string lozinka)
        {
            foreach (Termin termin in Termini.Instance.listaTermina)
            {
                if (termin.pacijentJMBG != null)
                {
                    if (ulogovanPacijent.jmbg.Equals(termin.pacijentJMBG))
                    {
                        if (korisnickoIme.Equals(ulogovanPacijent.korisnik.korisnickoIme) && lozinka.Equals(ulogovanPacijent.korisnik.lozinka))
                        {
                            listaTerminaUlogovanog.Add(termin);
                        }
                    }

                }
            }
        }
        */

        private void pomeriDugme_Click(object sender, RoutedEventArgs e)
        {
            if (listaZakazanihTermina.SelectedIndex >= 0)
            {
                PomeranjeTerminaPacijentaProzor pomeranje = new PomeranjeTerminaPacijentaProzor(listaZakazanihTermina);
                pomeranje.Show();
            }
        }


        private void zakaziDugme_Click(object sender, RoutedEventArgs e)
        {
            ZakazivanjeTerminaPacijentaProzor zakazivanjeProzor = new ZakazivanjeTerminaPacijentaProzor(ulogovanPacijent.jmbg, this);
            zakazivanjeProzor.Show();
        }

        private void otkaziDugme_Click(object sender, RoutedEventArgs e)
        {
            PacijentKontroler.Instance.Otkazivanje(listaZakazanihTermina);
        }

        private void infoDugme_Click(object sender, RoutedEventArgs e)
        {
            if (listaZakazanihTermina.SelectedIndex >= 0)
            {
                PacijentKontroler.Instance.Uvid(listaZakazanihTermina);
            }
        }
    }
}
