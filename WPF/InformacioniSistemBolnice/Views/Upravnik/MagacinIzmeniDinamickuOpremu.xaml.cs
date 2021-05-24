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
    /// <summary>
    /// Interaction logic for MagacinIzmeniDinamickuOpremu.xaml
    /// </summary>
    public partial class MagacinIzmeniDinamickuOpremu : Window
    {
        private DataGrid ListaDinamickeOpreme;
        public MagacinIzmeniDinamickuOpremu()
        {
            InitializeComponent();
        }
        public MagacinIzmeniDinamickuOpremu(DataGrid listaDinamickeOpreme)
        {
            InitializeComponent();
            ListaDinamickeOpreme = listaDinamickeOpreme;
            postavljanjeVrednost();
        }
        private void postavljanjeVrednost()
        {
            DinamickaOprema oprema = (DinamickaOprema)ListaDinamickeOpreme.SelectedValue;
            tb1.Text = oprema.Kolicina.ToString();
            cb1.Text = oprema.Tip.ToString();
            cb1.IsEnabled = false;
        }
        private void dugmePotvrdi_Click(object sender, RoutedEventArgs e)
        {
            DinamickaOprema oprema = (DinamickaOprema)ListaDinamickeOpreme.SelectedValue;
            UpravnikKontroler.Instance.IzmenaDinamickeOpreme(new(Int32.Parse(tb1.Text), (TipDinamickeOpreme)Enum.Parse(typeof(TipDinamickeOpreme), cb1.Text)));
            ListaDinamickeOpreme.ItemsSource = Repozitorijum.DinamickaOpremaRepo.Instance.ListaOpreme;
            this.Close();
        }
    }
}
