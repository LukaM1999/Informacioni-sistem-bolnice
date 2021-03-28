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
using System.Collections.ObjectModel;

namespace InformacioniSistemBolnice
{
    /// <summary>
    /// Interaction logic for TerminiPacijentaProzor.xaml
    /// </summary>
    public partial class TerminiPacijentaProzor : Window
    {
        public ObservableCollection<Termin> slobodniTermini = new ObservableCollection<Termin>();
        public Dictionary<Termin, int> terminIndeks = new Dictionary<Termin, int>();

        public TerminiPacijentaProzor()
        {
            InitializeComponent();

            Termini.Instance.Deserijalizacija("../../../json/slobodniTerminiPacijenta.json");
            listaSlobodnihTermina.ItemsSource = Termini.Instance.listaTermina;
            
        }

        private void otkaziButton_Click(object sender, RoutedEventArgs e)
        {
            Termin t = (Termin)listaZakazanihTermina.SelectedValue;
            foreach (KeyValuePair<Termin, int> ti in terminIndeks)
            {
                if (ti.Key == t)
                {
                    Termini.Instance.listaTermina.Insert(ti.Value, t);
                    listaZakazanihTermina.Items.Remove(t);
                    terminIndeks.Remove(ti.Key);
                }
            }
        }
        private void pomeriButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void prikazButton_Click(object sender, RoutedEventArgs e)
        {
          
        }

        private void zakaziButton_Click(object sender, RoutedEventArgs e)
        {
            Termin t = (Termin)listaSlobodnihTermina.SelectedValue;
            terminIndeks.Add(t, listaSlobodnihTermina.SelectedIndex);
            listaZakazanihTermina.Items.Add(t);
            Termini.Instance.listaTermina.Remove(t);
        }

    }
}
