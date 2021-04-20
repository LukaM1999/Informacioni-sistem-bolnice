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
    /// Interaction logic for MagacinIzmeniDinamickuOpremu.xaml
    /// </summary>
    public partial class MagacinIzmeniDinamickuOpremu : Window
    {
        private ListView lista;
        public MagacinIzmeniDinamickuOpremu()
        {
            InitializeComponent();
        }

        public MagacinIzmeniDinamickuOpremu(ListView lv)
        {
            InitializeComponent();
            lista = lv;
        }

        public void postavljanjeVrednost()
        {
            Model.DinamickaOprema oprema = (Model.DinamickaOprema)lista.SelectedValue;
            tb1.Text = oprema.kolicina.ToString();
            cb1.Text = oprema.tip.ToString();
        }

        private void dugmePotvrdi_Click(object sender, RoutedEventArgs e)
        {
            Model.DinamickaOprema oprema = (Model.DinamickaOprema)lista.SelectedValue;
            UpravnikKontroler.Instance.IzmenaDinamickeOpreme(oprema, this);
            lista.ItemsSource = Repozitorijum.DinamickaOprema.Instance.listaOpreme;
            this.Close();
        }
    }
}
