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
using Servis;
using Repozitorijum;
using Kontroler;

namespace InformacioniSistemBolnice
{
    public partial class ProstorijaForma : Window
    {
        DataGrid listaProstorija;
        public ProstorijaForma(DataGrid listaProstorija)
        {
            InitializeComponent();
            this.listaProstorija = listaProstorija;
        }

        private void potvrdiDugme_Click(object sender, RoutedEventArgs e)
        {

            UpravnikKontroler.Instance.KreiranjeProstorije(this);
            listaProstorija.ItemsSource = Prostorije.Instance.listaProstorija;
            this.Close();

        }
    }
}
