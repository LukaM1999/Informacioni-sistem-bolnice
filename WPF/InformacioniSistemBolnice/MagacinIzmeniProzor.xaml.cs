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
    /// Interaction logic for MagacinIzmeniProzor.xaml
    /// </summary>
    public partial class MagacinIzmeniProzor : Window
    {
        private DataGrid lista;
        public MagacinIzmeniProzor()
        {
            InitializeComponent();
        }

        public MagacinIzmeniProzor(DataGrid lv)
        {
            InitializeComponent();
            lista = lv;
        }

        public void postavljanjeVrednost()
        {
            Model.StatickaOprema oprema = (Model.StatickaOprema)lista.SelectedValue;
            tb1.Text = oprema.kolicina.ToString();
            cb1.Text = oprema.tip.ToString();
        }

        private void dugmePotvrdi_Click(object sender, RoutedEventArgs e)
        {
            Model.StatickaOprema oprema = (Model.StatickaOprema)lista.SelectedValue;
            UpravnikKontroler.Instance.IzmenaStatickeOpreme(oprema, this);
            lista.ItemsSource = Repozitorijum.StatickaOprema.Instance.listaOpreme;
            this.Close();
        }
    }
}
