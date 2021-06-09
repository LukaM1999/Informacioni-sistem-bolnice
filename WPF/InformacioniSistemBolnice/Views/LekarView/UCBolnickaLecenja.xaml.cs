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
using InformacioniSistemBolnice.ViewModels.LekarViewModel;
using InformacioniSistemBolnice.Views;
using MahApps.Metro.Controls;
using Model;
using Repozitorijum;

namespace InformacioniSistemBolnice
{
    public partial class UCBolnickaLecenja : MetroContentControl
    {
        public GlavniProzorLekara glavniProzorLekara;
        public UCBolnickaLecenja(GlavniProzorLekara glavni)
        {
            InitializeComponent();
            BolnickoLecenjeRepo.Instance.Deserijalizacija();
            PacijentRepo.Instance.Deserijalizacija();
            BolnickaLecenja.ItemsSource = BolnickoLecenjeRepo.Instance.BolnickaLecenja;
            glavniProzorLekara = glavni;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (BolnickaLecenja.SelectedIndex > -1)
            {
                Karton karton = new Karton();
                karton.DataContext = new KartonViewModel(glavniProzorLekara,
                    ((BolnickoLecenje)BolnickaLecenja.SelectedItem).JmbgPacijenta);
                glavniProzorLekara.contentControl.Content = karton;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (BolnickaLecenja.SelectedIndex > -1)
            {
                UCBolnickoLecenjeIzmena lecenje = new((BolnickoLecenje)BolnickaLecenja.SelectedItem, glavniProzorLekara);
                glavniProzorLekara.contentControl.Content = lecenje;
            }
        }
    }
}
