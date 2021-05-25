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
    public partial class LekIzmeniProzor : Window
    {
        private ObservableCollection<Alergen> ListaAlergenaLeka { get; set; }
        private DataGrid ListaLekova { get; set; }
        public LekIzmeniProzor(DataGrid listaLekova)
        {
            InitializeComponent();
            AlergenRepo.Instance.Deserijalizacija();
            ListaLekova = listaLekova;
            Lek izabraniLek = (Lek)ListaLekova.SelectedValue;
            ListaAlergenaLeka = izabraniLek.Alergen;
            listaAlergena.ItemsSource = AlergenRepo.Instance.Alergeni;
            listaAlergenaLeka.ItemsSource = izabraniLek.Alergen;
            PostaviText(izabraniLek);
        }
        public void PostaviText(Lek izabraniLek)
        {
            tbNaziv.Text = izabraniLek.Naziv;
            tbNaziv.IsReadOnly = true;
            tbProizvodjac.Text = izabraniLek.Proizvodjac;
            tbSastojci.Text = izabraniLek.Sastojci;
            tbZamena.Text = izabraniLek.Zamena;
        }
        private void btnPotvrdi_Click(object sender, RoutedEventArgs e)
        {
            UpravnikKontroler.Instance.IzmenaLeka(new(tbNaziv.Text, tbProizvodjac.Text, tbSastojci.Text, 
                tbZamena.Text, ListaAlergenaLeka));
            ListaLekova.ItemsSource = LekRepo.Instance.Lekovi;
            Close();
        }
        private void btnDodajAlergen_Click(object sender, RoutedEventArgs e)
        {
            Alergen izabraniAlergen = (Alergen)listaAlergena.SelectedValue;
            ListaAlergenaLeka.Add(izabraniAlergen);
            listaAlergenaLeka.ItemsSource = ListaAlergenaLeka;
        }
        private void btnObrisiAlergen_Click(object sender, RoutedEventArgs e)
        {
            if (listaAlergenaLeka.SelectedValue != null)
            {
                Alergen izabraniAlergen = (Alergen)listaAlergenaLeka.SelectedValue;
                ListaAlergenaLeka.Remove(izabraniAlergen);
                listaAlergenaLeka.ItemsSource = ListaAlergenaLeka;
            }
        }
    }
}
