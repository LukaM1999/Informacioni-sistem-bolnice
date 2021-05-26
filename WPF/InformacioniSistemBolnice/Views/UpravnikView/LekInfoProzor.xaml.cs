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

namespace InformacioniSistemBolnice
{
    public partial class LekInfoProzor : Window
    {
        public LekInfoProzor(DataGrid listaLekova)
        {
            InitializeComponent();
            Lek izabraniLek = (Lek)listaLekova.SelectedValue;
            tbNaziv.Text = izabraniLek.Naziv;
            tbProizvodjac.Text = izabraniLek.Proizvodjac;
            tbSastojci.Text = izabraniLek.Sastojci;
            tbZamena.Text = izabraniLek.Zamena;
            listaAlergena.ItemsSource = izabraniLek.Alergen;
        }
    }
}
