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

            Prostorije.Instance.Deserijalizacija();
            Pacijenti.Instance.Deserijalizacija();
            listaSati.ItemsSource = listaDatuma;
            
            foreach(Prostorija p in Prostorije.Instance.listaProstorija)
            {
                prostorijeID.Add(p.id);
            }
            sala.ItemsSource = prostorijeID;

            foreach (Pacijent p in Pacijenti.Instance.ListaPacijenata)
            {
                jmbgPacijent.Add(p.jmbg);
            }

            jmbg = jmbgLekara;
            //jmbgPacijenta = pacijent.jmbg;
            pacijenti.ItemsSource = jmbgPacijent;
            listaZakazanihTermina = listaZakazanihTerminaLekara;
        }

        private void potvrdaZakazivanjaDugme_Click(object sender, RoutedEventArgs e)
        {
            LekarKontroler.Instance.Zakazivanje(this, jmbg);

        }
    }
}
