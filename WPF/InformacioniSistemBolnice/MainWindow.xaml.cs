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
using RadSaDatotekama;


namespace InformacioniSistemBolnice
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OtvoriProzor(object sender, RoutedEventArgs e)
        {
            ProstorijeProzor prostorijeP = new ProstorijeProzor();
            // this.Visibility = Visibility.Hidden;
            prostorijeP.Show();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PacijentiProzor pacijentiP = new PacijentiProzor();
            pacijentiP.Show();
        }

        private void pacijentButton_Click(object sender, RoutedEventArgs e)
        {
            TerminiPacijentaProzor tpp = new TerminiPacijentaProzor();
            tpp.Show();


        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            TerminiLekaraProzor terminiLekara = new TerminiLekaraProzor();
            terminiLekara.Show();
        }
    }
}
