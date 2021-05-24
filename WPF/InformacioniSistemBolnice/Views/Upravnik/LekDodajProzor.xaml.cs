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
using System.Windows.Shapes;
using Model;
using Kontroler;
using Repozitorijum;
using System.Collections.ObjectModel;

namespace InformacioniSistemBolnice
{
    public partial class LekDodajProzor : Window
    {
        private ObservableCollection<Alergen> ListaAlergena { get; set; }
        private DataGrid ListaLekova { get; set; }

        public LekDodajProzor(DataGrid listaLekova)
        {
            InitializeComponent();
            ListaAlergena = new ObservableCollection<Alergen>();
            ListaLekova = listaLekova;
        }
        private void btnPotvrdi_Click(object sender, RoutedEventArgs e)
        {
            UpravnikKontroler.Instance.KreiranjeLeka(new(tbNaziv.Text, tbProizvodjac.Text, tbSastojci.Text, tbZamena.Text, ListaAlergena));
            ListaLekova.ItemsSource = Lekovi.Instance.ListaLekova;
            this.Close();
        }

        private void btnDodajAlergen_Click(object sender, RoutedEventArgs e)
        {
            ListaAlergena.Add(new(tbNazivAlergena.Text));
            listaAlergena.ItemsSource = ListaAlergena;
        }

        private void btnObrisiAlergen_Click(object sender, RoutedEventArgs e)
        {
            if(listaAlergena.SelectedValue != null)
            {
                Alergen izabraniAlergen = (Alergen)listaAlergena.SelectedValue;
                ListaAlergena.Remove(izabraniAlergen);
                listaAlergena.ItemsSource = ListaAlergena;
            }
        }
    }
}
