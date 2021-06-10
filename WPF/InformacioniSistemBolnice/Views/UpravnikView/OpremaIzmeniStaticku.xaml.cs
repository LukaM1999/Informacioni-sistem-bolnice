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
    /// Interaction logic for OpremaIzmeniStaticku.xaml
    /// </summary>
    public partial class OpremaIzmeniStaticku : Page
    {
        public OpremaIzmeniStaticku(StatickaOprema oprema)
        {
            InitializeComponent();
            cbTip.ItemsSource = Enum.GetValues(typeof(TipStatickeOpreme));
            cbTip.SelectedItem = oprema.Tip;
            cbTip.IsHitTestVisible = false;
            tbKolicina.Text = oprema.Kolicina.ToString();
        }

        private void VratiSe(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Magacin());
        }

        private void Potvrdi(object sender, RoutedEventArgs e)
        {
            if (tbKolicina.Text.All(char.IsDigit) && !string.IsNullOrWhiteSpace(tbKolicina.Text))
            {
                OpremaKontroler.Instance.IzmenaStatickeOpreme(new(Int32.Parse(tbKolicina.Text),
                    (TipStatickeOpreme)Enum.Parse(typeof(TipStatickeOpreme), cbTip.Text)));
                this.NavigationService.Navigate(new Magacin());
            }
        }
    }
}
