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

namespace InformacioniSistemBolnice.Views.LekarView
{
    public partial class About : MetroContentControl
    {
        private GlavniProzorLekara glavniProzor;
        public About(GlavniProzorLekara glavni)
        {
            InitializeComponent();
            glavniProzor = glavni;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            glavniProzor.contentControl.Content = new Podesavanja(glavniProzor);
        }
    }
}
