using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// <summary>
    /// Interaction logic for Jezik.xaml
    /// </summary>
    public partial class Jezik : MetroContentControl
    {
        private GlavniProzorLekara glavniProzor;
        public Jezik(GlavniProzorLekara glavni)
        {
            InitializeComponent();
            glavniProzor = glavni;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Podesavanja podesavanja = new(glavniProzor);
            glavniProzor.contentControl.Content = podesavanja;
        }

        private void Srpski_Checked(object sender, RoutedEventArgs e)
        {
            TranslationSource.Instance.CurrentCulture = new System.Globalization.CultureInfo("sr-SR");
        }

        private void Engleski_Checked(object sender, RoutedEventArgs e)
        {
            TranslationSource.Instance.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        }

    }
}
