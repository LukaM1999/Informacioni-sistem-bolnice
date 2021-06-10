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
using Model;
using Repozitorijum;
using Kontroler;
using InformacioniSistemBolnice.DTO;
using System.Text.RegularExpressions;

namespace InformacioniSistemBolnice.Views.UpravnikView
{
    /// <summary>
    /// Interaction logic for SalaDodaj.xaml
    /// </summary>
    public partial class SalaDodaj : Page
    {
        public SalaDodaj()
        {
            InitializeComponent();
            cbTipProstorije.ItemsSource = Enum.GetValues(typeof(TipProstorije));
            cbTipProstorije.SelectedIndex = 0;
        }

        private void Potvrdi(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(tbId.Text))
            {
                ProstorijaDto dto = new(Int32.Parse(tbSprat.Text), (TipProstorije)Enum.Parse(typeof(TipProstorije), cbTipProstorije.Text), tbId.Text, false, new());
                ProstorijaKontroler.Instance.KreiranjeProstorije(dto);
                this.NavigationService.Navigate(new Sale());
            }
        }

        private void VratiSe(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Sale());
        }

        private void ValidacijaBrojaUTextBoxu(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
