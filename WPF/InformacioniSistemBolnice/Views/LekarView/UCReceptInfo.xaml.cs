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
using Model;
using Repozitorijum;

namespace InformacioniSistemBolnice
{
    /// <summary>
    /// Interaction logic for UCReceptInfo.xaml
    /// </summary>
    public partial class UCReceptInfo : MetroContentControl
    {
        public GlavniProzorLekara glavniProzor;
        public Recept recept;
        public object prethodniProzor;
        public UCReceptInfo(GlavniProzorLekara glavni, Recept izabran, object prethodni)
        {
            InitializeComponent();
            PacijentRepo.Instance.Deserijalizacija();
            glavniProzor = glavni;
            recept = izabran;
            listaTerapija.ItemsSource = recept.terapije;
            prethodniProzor = prethodni;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            glavniProzor.contentControl.Content = prethodniProzor;
        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            if (listaTerapija.SelectedIndex > -1)
            {
                Terapija terapija = (Terapija)listaTerapija.SelectedItem;
                UCLekInfo lek = new UCLekInfo(glavniProzor, terapija.Lek, this);
                glavniProzor.contentControl.Content = lek;
            }
        }
    }
}
