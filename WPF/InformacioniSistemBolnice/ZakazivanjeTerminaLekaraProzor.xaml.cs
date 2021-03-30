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
using RadSaDatotekama;
using Model;

namespace InformacioniSistemBolnice
{
    /// <summary>
    /// Interaction logic for ZakazivanjeTerminaLekaraProzor.xaml
    /// </summary>
    public partial class ZakazivanjeTerminaLekaraProzor : Window
    {

        public List<string> listaDatuma = new List<string>();

        public ZakazivanjeTerminaLekaraProzor()
        {
            InitializeComponent();
            DateTime datum = DateTime.Parse("7:00");

            for (int j = 0; j < 27; j++)
            {
                listaDatuma.Add(datum.ToString("HH:mm"));
                datum = datum.AddMinutes(30);
            }

            listaSati.ItemsSource = listaDatuma;
           

        }

        private void potvrdaZakazivanjaDugme_Click(object sender, RoutedEventArgs e)
        {
            if (listaSati.SelectedIndex >= 0 && datumTermina.SelectedDate != null)
            {
                DateTime datumTermina = (DateTime)this.datumTermina.SelectedDate;
                string datumVrednost = (string)listaSati.SelectedValue;
                string[] satiMinuti = datumVrednost.Split(":");
                double sat = double.Parse(satiMinuti[0]);
                if (satiMinuti[1].Equals("30"))
                {
                    sat += 0.5;
                }

                datumTermina = datumTermina.AddHours(sat);
                foreach (Termin t in Termini.Instance.listaTermina)
                {
                    if (t.vreme == datumTermina)
                    {
                        return;
                    }
                }

                Termin zakazanTermin = new Termin(datumTermina, 30, TipTermina.pregled, StatusTermina.zakazan);
                Termini.Instance.listaTermina.Add(zakazanTermin);
                Termini.Instance.Serijalizacija("../../../json/zakazaniTermini.json");
                this.Close();
            }
        }
    }
}
