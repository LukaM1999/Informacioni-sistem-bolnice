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

namespace InformacioniSistemBolnice
{
    /// <summary>
    /// Interaction logic for LekIzmeniProzor.xaml
    /// </summary>
    public partial class LekIzmeniProzor : Window
    {
        private Lek lek;
        private DataGrid listaLekova;
        public LekIzmeniProzor()
        {
            InitializeComponent();
        }

        public LekIzmeniProzor(DataGrid listaLekova)
        {
            InitializeComponent();
            lek = (Lek)listaLekova.SelectedItem;
            this.listaLekova = listaLekova;
        }

        public void PostaviText()
        {
            tbNaziv.Text = lek.naziv;
            tbProizvodjac.Text = lek.proizvodjac;
            tbSastojci.Text = lek.sastojci;
        }
        private void btnPotvrdi_Click(object sender, RoutedEventArgs e)
        {
            LekDto dto = new LekDto(tbNaziv.Text, tbProizvodjac.Text, tbSastojci.Text);
            UpravnikKontroler.Instance.IzmenaLeka(dto, lek);
            listaLekova.ItemsSource = Lekovi.Instance.listaLekova;
            this.Close();
        }
    }
}
