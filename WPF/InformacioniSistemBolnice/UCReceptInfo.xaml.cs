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
using System.Linq;

namespace InformacioniSistemBolnice
{
    /// <summary>
    /// Interaction logic for UCReceptInfo.xaml
    /// </summary>
    public partial class UCReceptInfo : UserControl
    {
        public UCListaRecepata listaRecepata;
        public UCReceptInfo(UCListaRecepata recepti)
        {
            InitializeComponent();
            Pacijenti.Instance.Deserijalizacija();
            Recept r = (Recept) recepti.listaRecepata.SelectedItem;
            listaTerapija.ItemsSource = r.Terapije;
            ID.Content = r.id;
            listaRecepata = recepti;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            listaRecepata.glavniProzorLekara.contentControl.Content = listaRecepata;
        }
        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            if (listaTerapija.SelectedIndex > -1)
            {
                Terapija t = (Terapija) listaTerapija.SelectedItem;
                if (t.Lek != null)
                {
                    naziv.Content = t.Lek.Naziv;
                    proizvodjac.Content = t.Lek.Proizvodjac;
                    sastojci.Content = t.Lek.Sastojci;
                    zamena.Content = t.Lek.Zamena;
                }
            }
        }
    }
}
