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

namespace InformacioniSistemBolnice
{
    /// <summary>
    /// Interaction logic for MagacinDodajDinamickuOpremu.xaml
    /// </summary>
    public partial class MagacinDodajDinamickuOpremu : Window
    {
        private DataGrid lista;
        public MagacinDodajDinamickuOpremu()
        {
            InitializeComponent();
        }

        public MagacinDodajDinamickuOpremu(DataGrid lv)
        {
            InitializeComponent();
            lista = lv;
        }

        private void dugmePotvrdi_Click(object sender, RoutedEventArgs e)
        {
            UpravnikKontroler.Instance.KreiranjeDinamickeOpreme(this);
            lista.ItemsSource = Repozitorijum.DinamickaOpremaRepo.Instance.ListaOpreme;
            this.Close();
        }
    }
}
