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
    /// Interaction logic for TerminInfoProzor.xaml
    /// </summary>
    public partial class TerminInfoProzor : Window
    {
        public TerminInfoProzor(ListView listaZakazanih)
        {
            InitializeComponent();
            Termin t = (Termin)listaZakazanih.SelectedItem;
            datumLabela.Content = t.vreme.ToString("dd/MM/yyyy");
            vremeLabela.Content = t.vreme.ToString("HH:mm");
            tipTerminaLabela.Content = t.tipTermina.ToString();
            statusTerminaLabela.Content = t.status.ToString();
            trajanjeLabela.Content = t.trajanje.ToString();
        }
    }
}
