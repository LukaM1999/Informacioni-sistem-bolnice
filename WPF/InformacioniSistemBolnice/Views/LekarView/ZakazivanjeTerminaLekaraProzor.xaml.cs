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
using Servis;
using Kontroler;

namespace InformacioniSistemBolnice
{
    public partial class ZakazivanjeTerminaLekaraProzor : Window
    {

        public List<string> listaDatuma = new List<string>();
        public List<string> prostorijeID = new List<string>();
        public List<string> jmbgPacijent = new List<string>();
        public DataGrid listaZakazanihTermina;
        public string jmbg;
        //string jmbgPacijenta;

        public ZakazivanjeTerminaLekaraProzor(string jmbgLekara, DataGrid listaZakazanihTerminaLekara)
        {
            InitializeComponent();
            DateTime datum = DateTime.Parse("7:00");

            for (int j = 0; j < 27; j++)
            {
                listaDatuma.Add(datum.ToString("HH:mm"));
                datum = datum.AddMinutes(30);
            }

            ProstorijaRepo.Instance.Deserijalizacija();
            PacijentRepo.Instance.Deserijalizacija();
            listaSati.ItemsSource = listaDatuma;
            
            foreach(Prostorija p in ProstorijaRepo.Instance.Prostorije)
            {
                prostorijeID.Add(p.Id);
            }
            sala.ItemsSource = prostorijeID;

            foreach (Pacijent p in PacijentRepo.Instance.Pacijenti)
            {
                jmbgPacijent.Add(p.Jmbg);
            }

            jmbg = jmbgLekara;
            //jmbgPacijenta = pacijent.jmbg;
            pacijenti.ItemsSource = jmbgPacijent;
            listaZakazanihTermina = listaZakazanihTerminaLekara;
        }

        private void potvrdaZakazivanjaDugme_Click(object sender, RoutedEventArgs e)
        {
            TerminKontroler.Instance.ZakaziTermin((Termin)listaZakazanihTermina.SelectedItem);
        }
    }
}
