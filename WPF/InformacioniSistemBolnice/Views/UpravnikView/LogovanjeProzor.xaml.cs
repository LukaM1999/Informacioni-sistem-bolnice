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
using InformacioniSistemBolnice.ViewModels.UpavnikViewModel;

namespace InformacioniSistemBolnice.Views.UpravnikView
{
    public partial class LogovanjeProzor : Page
    {
        public LogovanjeProzor()
        {
            InitializeComponent();    
        }

        private void PrijaviSe(object sender, RoutedEventArgs e)
        {
            labKorImeGreska.Visibility = Visibility.Hidden;
            labLozGreska.Visibility = Visibility.Hidden;
            labPraznoPolje.Visibility = Visibility.Hidden;
            if (!tbKorisnickoIme.Text.Equals("upravnik") && !tbKorisnickoIme.Text.Equals(""))
            {
                labKorImeGreska.Visibility = Visibility.Visible;
                return;
            }
            if (!tbLozinka.Password.Equals("ftn") && !tbLozinka.Password.Equals(""))
            {
                labLozGreska.Visibility = Visibility.Visible;
                return;
            }
            if (tbKorisnickoIme.Text.Equals("") || tbLozinka.Password.Equals(""))
            {
                labPraznoPolje.Visibility = Visibility.Visible;
                return;
            }
            this.NavigationService.Navigate(new MeniProzor());
        }
    }
}
