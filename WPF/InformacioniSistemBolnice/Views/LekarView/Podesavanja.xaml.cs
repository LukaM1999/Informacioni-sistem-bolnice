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
using MahApps.Metro.Controls;

namespace InformacioniSistemBolnice
{
    public partial class Podesavanja : MetroContentControl
    {
        private GlavniProzorLekara glavniProzor;
        public Podesavanja(GlavniProzorLekara glavni)
        {
            InitializeComponent();
            glavniProzor = glavni;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Jezik jezik = new Jezik(glavniProzor);
            glavniProzor.contentControl.Content = jezik;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Tema tema = new Tema(glavniProzor);
            glavniProzor.contentControl.Content = tema;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            glavniProzor.Close();
        }
    }
}
