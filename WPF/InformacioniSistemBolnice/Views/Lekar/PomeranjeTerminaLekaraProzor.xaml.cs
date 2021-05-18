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
using Model;
using Repozitorijum;
using Servis;
using Kontroler;

namespace InformacioniSistemBolnice
{
    public partial class PomeranjeTerminaLekaraProzor : Window
    {
        public Termin zakazanTermin;
        public List<string> listaDatuma = new List<string>();
        public DataGrid zakazaniTermini;
        public List<string> prostorijeID = new List<string>();
        public PomeranjeTerminaLekaraProzor(DataGrid zakazaniTermini)

        {
            InitializeComponent();
            DateTime datum = DateTime.Parse("7:00");

            for (int j = 0; j < 27; j++)
            {
                listaDatuma.Add(datum.ToString("HH:mm"));
                datum = datum.AddMinutes(30);
            }
            listaSati.ItemsSource = listaDatuma;
            this.zakazaniTermini = zakazaniTermini;
            zakazanTermin = (Termin)zakazaniTermini.SelectedItem;

            trajanjeTerminaUnos.Text = zakazanTermin.trajanje.ToString();
            tipTerminaUnos.Text = zakazanTermin.tipTermina.ToString();

            Prostorije.Instance.Deserijalizacija();
            listaSati.ItemsSource = listaDatuma;
            
            foreach (Prostorija p in Prostorije.Instance.listaProstorija)
            {
                prostorijeID.Add(p.id);
            }
            sala.ItemsSource = prostorijeID;

            if (zakazanTermin.idProstorije != null)
            {
                sala.Text = zakazanTermin.idProstorije;
            }

            datumTermina.Text = zakazanTermin.vreme.ToString("MM/dd/yyyy");

            string sat = zakazanTermin.vreme.ToString("HH:mm");
            for (int i = 0; i < listaDatuma.Count; i++)
            {
                if (sat.Equals(listaDatuma[i]))
                {
                    listaSati.SelectedIndex = i;
                }
            }
      
        }

        private void potvrdaPomeranjaDugme_Click(object sender, RoutedEventArgs e)
        {
            //LekarKontroler.Instance.Pomeranje(zakazanTermin);
        }
    }
}
