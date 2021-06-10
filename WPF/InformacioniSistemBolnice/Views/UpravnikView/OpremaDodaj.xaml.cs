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
        private bool JeDinamicka { get; set; }
        public OpremaDodaj(bool jeDinamicka)
        {
            InitializeComponent();
            JeDinamicka = jeDinamicka;
            if (jeDinamicka)
            {
                cbTip.ItemsSource = Enum.GetValues(typeof(TipDinamickeOpreme));
                cbTip.SelectedIndex = 0;
            }
            else
            {
                cbTip.ItemsSource = Enum.GetValues(typeof(TipStatickeOpreme));
                cbTip.SelectedIndex = 0;
            }
        }

        private void VratiSe(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Magacin());
        }

        private void Potvrdi(object sender, RoutedEventArgs e)
        {
            if (tbKolicina.Text.All(char.IsDigit) && !string.IsNullOrWhiteSpace(tbKolicina.Text))
            {
                if (JeDinamicka)
                {
                    OpremaKontroler.Instance.KreiranjeDinamickeOpreme(new(Int32.Parse(tbKolicina.Text),
                        (TipDinamickeOpreme)Enum.Parse(typeof(TipDinamickeOpreme), cbTip.Text)));
                    this.NavigationService.Navigate(new Magacin());
                }
                else
                {
                    OpremaKontroler.Instance.KreiranjeStatickeOpreme(new(Int32.Parse(tbKolicina.Text),
                        (TipStatickeOpreme)Enum.Parse(typeof(TipStatickeOpreme), cbTip.Text)));
                    this.NavigationService.Navigate(new Magacin());
                }
            }
        }
    }
}
