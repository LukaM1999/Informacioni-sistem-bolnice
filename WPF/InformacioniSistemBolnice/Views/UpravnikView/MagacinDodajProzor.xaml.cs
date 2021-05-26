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
using Kontroler;
using Model;

namespace InformacioniSistemBolnice
{
    public partial class MagacinDodajProzor : Window
    {
        private DataGrid ListaStatickeOpreme { get; set; }
        public MagacinDodajProzor()
        {
            InitializeComponent();
        }

        public MagacinDodajProzor(DataGrid listaStatickeOpreme)
        {
            InitializeComponent();
            ListaStatickeOpreme = listaStatickeOpreme;
        }

        private void dugmePotvrdi_Click(object sender, RoutedEventArgs e)
        {
            UpravnikKontroler.Instance.KreiranjeStatickeOpreme(new(Int32.Parse(tbKol.Text), (TipStatickeOpreme)Enum.Parse(typeof(TipStatickeOpreme), cb1.Text)));
            ListaStatickeOpreme.ItemsSource = Repozitorijum.StatickaOpremaRepo.Instance.StatickaOprema;
            this.Close();
        }
    }
}
