using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Model;
using Repozitorijum;

namespace InformacioniSistemBolnice
{
    public partial class UCLekovi : MetroContentControl
    {
        private GlavniProzorLekara glavniProzorLekara;
        public UCLekovi(GlavniProzorLekara glavni)
        {
            InitializeComponent();
            LekRepo.Instance.Deserijalizacija();
            listaLekova.ItemsSource = LekRepo.Instance.Lekovi;
            glavniProzorLekara = glavni;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (listaLekova.SelectedIndex > -1)
            {
                UCLekInfo ucLek = new UCLekInfo(glavniProzorLekara, (Lek)listaLekova.SelectedItem, this);
                glavniProzorLekara.contentControl.Content = ucLek;
            }
        }

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            ObservableCollection<Lek> Filter = new ObservableCollection<Lek>();
            foreach (var lek in LekRepo.Instance.Lekovi)
            {
                if (Nadji(searchBox.Text.ToLower(), lek))
                {
                    Filter.Add(lek);
                }
            }

            listaLekova.ItemsSource = Filter;
        }

        private bool Nadji(string text, Lek p)
        {
            return p.Naziv.ToLower().Contains(text) || p.Proizvodjac.ToLower().Contains(text);
        }
    }
}
