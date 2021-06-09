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
using InformacioniSistemBolnice.DTO;
using Kontroler;

namespace InformacioniSistemBolnice.Views.UpravnikView
{
    public partial class FeedbackUpravnik : Page
    {
        public FeedbackUpravnik()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FeedbackKontroler.Instance.PosaljiFeedback(new FeedbackDto("upravnik1",
                "upravnik", TextBox.Text));
            NavigationService.Navigate(new MeniProzor());
        }

        private void VratiSe(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MeniProzor());
        }

    }
}
