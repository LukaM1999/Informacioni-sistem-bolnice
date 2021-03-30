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
using RadSaDatotekama;

namespace InformacioniSistemBolnice
{
    /// <summary>
    /// Interaction logic for PomeranjeTerminaLekaraProzor.xaml
    /// </summary>
    public partial class PomeranjeTerminaLekaraProzor : Window
    {
        public Termin zakazanTermin;
        public List<string> listaDatuma = new List<string>();
        public ListView zakazaniTermini;
        public PomeranjeTerminaLekaraProzor(ListView zakazaniTermini)

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
        }

        private void potvrdaPomeranjaDugme_Click(object sender, RoutedEventArgs e)
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

                zakazanTermin.vreme = datumTermina;
                zakazanTermin.status = StatusTermina.pomeren;
                
              
                Termini.Instance.Serijalizacija("../../../json/zakazaniTermini.json");
                Termini.Instance.Deserijalizacija("../../../json/zakazaniTermini.json");
           
                zakazaniTermini.ItemsSource = Termini.Instance.listaTermina;
                this.Close();

            }
        }
    }
}
