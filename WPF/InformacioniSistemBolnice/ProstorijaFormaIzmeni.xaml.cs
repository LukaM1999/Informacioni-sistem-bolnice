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
using Logika;
using RadSaDatotekama;

namespace InformacioniSistemBolnice
{
    /// <summary>
    /// Interaction logic for ProstorijaFormaIzmeni.xaml
    /// </summary>
    public partial class ProstorijaFormaIzmeni : Window
    {
        private ListView l;
        public ProstorijaFormaIzmeni()
        {
            InitializeComponent();
        }
        public ProstorijaFormaIzmeni(ListView p)
        {
            InitializeComponent();
            l = p;
        }

        public void SetTextBoxValue(Prostorija p)
        {
            tb1.Text = p.sprat.ToString();
            tipIzmena.Text = p.tip.ToString();
            tb2.Text = p.id.ToString();
            if (p.jeZauzeta)
            {
                rb1.IsChecked = true;
            }
            else
            {
                rb2.IsChecked = true;
            }

        }

        private void potvrdaIzmeneDugme_Click(object sender, RoutedEventArgs e)
        {
            Prostorija p = (Prostorija)l.SelectedValue;
            UpravljanjeProstorijama.Instance.IzmenaProstorije(this, p);
            l.ItemsSource = Prostorije.Instance.listaProstorija;
            this.Close();

        }
    }
}
