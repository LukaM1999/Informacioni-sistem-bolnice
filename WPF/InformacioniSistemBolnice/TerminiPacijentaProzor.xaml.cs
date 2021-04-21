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
using System.Threading;

namespace InformacioniSistemBolnice
{
    public partial class TerminiPacijentaProzor : Window
    {
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


            if (ulogovanPacijent.zdravstveniKarton.recept != null)
            {
                Recept recept = ulogovanPacijent.zdravstveniKarton.recept;

                Thread th = new Thread(() =>
                {
                    while (DateTime.Now > recept.terapija[0].pocetakTerapije && DateTime.Now < recept.terapija[0].krajTerapije)
                    {
                        Thread.Sleep((int)recept.terapija[0].redovnost * 1000);
                        MessageBox.Show("Vreme je da popijete lek");
                    }
                });

                th.Start();
            }
            

        }

        private void pomeriDugme_Click(object sender, RoutedEventArgs e)
        {
            if (listaZakazanihTermina.SelectedIndex >= 0 && ((Termin)listaZakazanihTermina.SelectedItem).vreme > DateTime.Now.AddHours(24))
            {
                PomeranjeTerminaPacijentaProzor pomeranje = new PomeranjeTerminaPacijentaProzor(this);
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
