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
using ControlzEx.Theming;
using MahApps.Metro.Controls;

namespace InformacioniSistemBolnice
{
    public partial class Tema : MetroContentControl
    {
        private GlavniProzorLekara glavniProzor;

        public Tema(GlavniProzorLekara glavni)
        {
            InitializeComponent();
            glavniProzor = glavni;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Podesavanja podesavanja = new Podesavanja(glavniProzor);
            glavniProzor.contentControl.Content = podesavanja;
        }
        private void Svetla_Checked(object sender, RoutedEventArgs e)
        {
            ThemeManager.Current.ChangeTheme(Application.Current, "Light.Blue");
        }
        private void Tamna_Checked(object sender, RoutedEventArgs e)
        {
            ThemeManager.Current.ChangeTheme(Application.Current, "Dark.Blue");
        }
    }
}