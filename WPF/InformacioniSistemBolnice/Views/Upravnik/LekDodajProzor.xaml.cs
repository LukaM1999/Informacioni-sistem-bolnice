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
        private ObservableCollection<Alergen> ListaAlergenaLeka { get; set; }
        private DataGrid ListaLekova { get; set; }

        public LekDodajProzor(DataGrid listaLekova)
        {
            InitializeComponent();
            Alergeni.Instance.Deserijalizacija();
            listaAlergena.ItemsSource = Alergeni.Instance.listaAlergena;
            ListaAlergenaLeka = new ObservableCollection<Alergen>();
            ListaLekova = listaLekova;
        }
        private void btnPotvrdi_Click(object sender, RoutedEventArgs e)
        {
            UpravnikKontroler.Instance.KreiranjeLeka(new(tbNaziv.Text, tbProizvodjac.Text, tbSastojci.Text, tbZamena.Text, ListaAlergenaLeka));
            ListaLekova.ItemsSource = Lekovi.Instance.ListaLekova;
            this.Close();
        }

        private void btnDodajAlergen_Click(object sender, RoutedEventArgs e)
        {
            if (listaAlergena.SelectedValue != null)
            {
                Alergen izabranAlergen = (Alergen)listaAlergena.SelectedValue;
                ListaAlergenaLeka.Add(izabranAlergen);
                listaAlergenaLeka.ItemsSource = ListaAlergenaLeka;
            }
        }

        private void btnObrisiAlergen_Click(object sender, RoutedEventArgs e)
        {
            if(listaAlergenaLeka.SelectedValue != null)
            {
                Alergen izabraniAlergen = (Alergen)listaAlergenaLeka.SelectedValue;
                ListaAlergenaLeka.Remove(izabraniAlergen);
                listaAlergena.ItemsSource = ListaAlergenaLeka;
            }
        }
    }
}
