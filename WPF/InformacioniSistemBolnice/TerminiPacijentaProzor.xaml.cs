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
using FluentScheduler;

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


            int mesecnihTermina = 0;
            Thread proveraMalicioznosti = new Thread(() =>
            { 

                    while (!ulogovanPacijent.maliciozan)
                    {
                        for (int i = 1; i < 13; i++)
                        {
                            foreach (Termin t in ulogovanPacijent.zakazaniTermini.ToList())
                            {
                                if (t.vreme > DateTime.Now.AddMonths(i - 1) && t.vreme < DateTime.Now.AddMonths(i))
                                {
                                    mesecnihTermina++;
                                }
                                if (mesecnihTermina > 3)
                                {
                                    ulogovanPacijent.maliciozan = true;
                                    System.Diagnostics.Debug.WriteLine("Maliciozan!");
                                    return;

                                }
                            }
                            mesecnihTermina = 0;
                        }

                    }
            });
            proveraMalicioznosti.Start();
            
            if (ulogovanPacijent.maliciozan)
            {
                proveraMalicioznosti.Interrupt();
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
    /*
    public class ScheduledJobRegistry : Registry
    {
        public ScheduledJobRegistry(int hours)
        {
            Schedule<MyJob>().ToRunEvery(hours).Seconds();
                    

        }
    }

    public class MyJob : IJob
    {
        public void Execute()
        {
            MessageBox.Show("Vreme je da popijete lek");
        }
    }
    */
}
