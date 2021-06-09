using Kontroler;
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
    /// Interaction logic for OpremaDodaj.xaml
    /// </summary>
    public partial class OpremaDodaj : Page
    {
        public OpremaDodaj()
        {
            InitializeComponent();
            cbTip.ItemsSource = Enum.GetValues(typeof(TipProstorije));
            cbTip.SelectedIndex = 0;
        }

        private void VratiSe(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Magacin());
        }

        private void Potvrdi(object sender, RoutedEventArgs e)
        {
            if (tbKolicina.Text.All(char.IsDigit) && !string.IsNullOrWhiteSpace(tbKolicina.Text))
            {
                OpremaKontroler.Instance.KreiranjeStatickeOpreme(new(Int32.Parse(tbKolicina.Text),
                    (TipStatickeOpreme)Enum.Parse(typeof(TipStatickeOpreme), cbTip.Text)));
                this.NavigationService.Navigate(new Magacin());
            }
        }
    }
}
