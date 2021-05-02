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
    /// <summary>
    /// Interaction logic for LekInfoProzor.xaml
    /// </summary>
    public partial class LekInfoProzor : Window
    {
        public LekInfoProzor(Lek lek)
        {
            InitializeComponent();
            tbNaziv.Text = lek.naziv;
            tbProizvodjac.Text = lek.proizvodjac;
            tbSastojci.Text = lek.sastojci;
        }
    }
}
