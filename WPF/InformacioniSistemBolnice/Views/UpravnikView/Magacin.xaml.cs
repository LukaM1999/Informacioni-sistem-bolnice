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
using Repozitorijum;
using Kontroler;

namespace InformacioniSistemBolnice.Views.UpravnikView
{
    public partial class Magacin : Page
    {
        public Magacin()
        {
            InitializeComponent();
            DinamickaOpremaRepo.Instance.Deserijalizacija();
            StatickaOpremaRepo.Instance.Deserijalizacija();
            dgListaOpreme.ItemsSource = DinamickaOpremaRepo.Instance.DinamickaOprema;
        }

        private void VratiNaPrethodniProzor(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MeniProzor());
        }

        private void DinamickaOprema(object sender, RoutedEventArgs e)
        {
            dgListaOpreme.ItemsSource = DinamickaOpremaRepo.Instance.DinamickaOprema;
        }

        private void StatickaOprema(object sender, RoutedEventArgs e)
        {
            dgListaOpreme.ItemsSource = StatickaOpremaRepo.Instance.StatickaOprema;
        }
    }
}
