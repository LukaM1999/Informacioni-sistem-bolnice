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
using Repozitorijum;

namespace InformacioniSistemBolnice
{
    /// <summary>
    /// Interaction logic for UCLekovi.xaml
    /// </summary>
    public partial class UCLekovi : UserControl
    {
        private GlavniProzorLekara glavniProzorLekara;
        public UCLekovi(GlavniProzorLekara glavni)
        {
            InitializeComponent();
            Lekovi.Instance.Deserijalizacija();
            listaLekova.ItemsSource = Lekovi.Instance.ListaLekova;
            glavniProzorLekara = glavni;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (listaLekova.SelectedIndex > -1)
            {
                UCLekInfo ucLek = new UCLekInfo(glavniProzorLekara, (Lek)listaLekova.SelectedItem);
                glavniProzorLekara.contentControl.Content = ucLek;
            }
        }
    }
}
