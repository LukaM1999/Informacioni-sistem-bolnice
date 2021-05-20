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
        private DataGrid lista;
        public MagacinIzmeniDinamickuOpremu()
        {
            InitializeComponent();
        }

        public MagacinIzmeniDinamickuOpremu(DataGrid lv)
        {
            InitializeComponent();
            lista = lv;
        }

        public void postavljanjeVrednost()
        {
            DinamickaOprema oprema = (DinamickaOprema)lista.SelectedValue;
            tb1.Text = oprema.kolicina.ToString();
            cb1.Text = oprema.tip.ToString();
        }

        private void dugmePotvrdi_Click(object sender, RoutedEventArgs e)
        {
            DinamickaOprema oprema = (DinamickaOprema)lista.SelectedValue;
            UpravnikKontroler.Instance.IzmenaDinamickeOpreme(oprema, this);
            lista.ItemsSource = Repozitorijum.DinamickaOpremaRepo.Instance.listaOpreme;
            this.Close();
        }
    }
}
