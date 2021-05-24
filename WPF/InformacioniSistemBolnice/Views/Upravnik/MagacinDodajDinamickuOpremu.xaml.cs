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
using Repozitorijum;

namespace InformacioniSistemBolnice
{
    /// <summary>
    /// Interaction logic for MagacinDodajDinamickuOpremu.xaml
    /// </summary>
    public partial class MagacinDodajDinamickuOpremu : Window
    {
        private DataGrid ListaDinamickeOpreme;
        public MagacinDodajDinamickuOpremu()
        {
            InitializeComponent();
        }

        public MagacinDodajDinamickuOpremu(DataGrid listaDinamickeOpreme)
        {
            InitializeComponent();
            ListaDinamickeOpreme = listaDinamickeOpreme;
        }

        private void dugmePotvrdi_Click(object sender, RoutedEventArgs e)
        {
            UpravnikKontroler.Instance.KreiranjeDinamickeOpreme(new(Int32.Parse(tbKol.Text), (TipDinamickeOpreme)Enum.Parse(typeof(TipDinamickeOpreme), cb1.Text)));
            ListaDinamickeOpreme.ItemsSource = Repozitorijum.DinamickaOpremaRepo.Instance.ListaOpreme;
            this.Close();
        }
    }
}
