using Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InformacioniSistemBolnice.Views.UpravnikView
{
    /// <summary>
    /// Interaction logic for OpremaIzmeni.xaml
    /// </summary>
    public partial class OpremaIzmeniDinamicku : Page
    {
        private DinamickaOprema IzabranaOprema { get; set; }

        public OpremaIzmeniDinamicku(DinamickaOprema izabranaOprema)
        {
            InitializeComponent();
            IzabranaOprema = izabranaOprema;
            cbTip.ItemsSource = Enum.GetValues(typeof(TipProstorije));
            cbTip.SelectedItem = izabranaOprema.Tip;
        }
    }
}
