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

namespace InformacioniSistemBolnice.Views.Upravnik
{
    public partial class ZahteviInfoProzor : Window
    {
        public ZahteviInfoProzor(DataGrid listaZahteva)
        {
            InitializeComponent();
            Zahtev izabraniZahtev = (Zahtev)listaZahteva.SelectedValue;
            tbKomentar.Text = izabraniZahtev.Komentar;
            tbPotpis.Text = izabraniZahtev.Potpis;
        }
    }
}
