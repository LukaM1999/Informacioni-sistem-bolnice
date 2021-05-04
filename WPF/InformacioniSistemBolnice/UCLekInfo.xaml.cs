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

namespace InformacioniSistemBolnice
{
    /// <summary>
    /// Interaction logic for UCLekInfo.xaml
    /// </summary>
    public partial class UCLekInfo : UserControl
    {
        public GlavniProzorLekara glavniProzorLekara;
        public UCLekInfo(GlavniProzorLekara glavni)
        {
            InitializeComponent();
            glavniProzorLekara = glavni;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UCLekovi lekovi = new UCLekovi(glavniProzorLekara);
            glavniProzorLekara.contentControl.Content = lekovi;
        }
    }
}
