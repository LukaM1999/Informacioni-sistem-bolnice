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
using InformacioniSistemBolnice.Views.Lekar;
using Model;
using Repozitorijum;

namespace InformacioniSistemBolnice
{
    /// <summary>
    /// Interaction logic for UCPacijenti.xaml
    /// </summary>
    public partial class UCBolnickaLecenja : UserControl
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
                BolnickoLecenje lecenje = (BolnickoLecenje)BolnickaLecenja.SelectedItem;
                Pacijent naLecenju = PacijentRepo.Instance.NadjiPoJmbg(lecenje.JmbgPacijenta);
                IzmenaZdravstvenogKartonaLekar zk = new(naLecenju, glavniProzorLekara);
                zk.Show();
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
