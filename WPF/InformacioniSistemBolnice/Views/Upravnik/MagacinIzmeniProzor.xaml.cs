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
    /// Interaction logic for MagacinIzmeniProzor.xaml
    /// </summary>
    public partial class MagacinIzmeniProzor : Window
    {
        private DataGrid ListaStatickeOpreme;
        public MagacinIzmeniProzor()
        {
            InitializeComponent();
        }

        public MagacinIzmeniProzor(DataGrid listaStatickeOpreme)
        {
            InitializeComponent();
            ListaStatickeOpreme = listaStatickeOpreme;
            postavljanjeVrednost();
        }

        private void postavljanjeVrednost()
        {
            StatickaOprema oprema = (StatickaOprema)ListaStatickeOpreme.SelectedValue;
            tb1.Text = oprema.Kolicina.ToString();
            cb1.Text = oprema.Tip.ToString();
            cb1.IsEnabled = false;
        }

        private void dugmePotvrdi_Click(object sender, RoutedEventArgs e)
        {
            UpravnikKontroler.Instance.IzmenaStatickeOpreme(new(Int32.Parse(tb1.Text), (TipStatickeOpreme)Enum.Parse(typeof(TipStatickeOpreme), cb1.Text)));
            ListaStatickeOpreme.ItemsSource = StatickaOpremaRepo.Instance.StatickaOprema;
            this.Close();
        }
    }
}
