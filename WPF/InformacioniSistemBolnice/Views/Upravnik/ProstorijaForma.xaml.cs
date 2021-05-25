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
using InformacioniSistemBolnice.DTO;
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
            Inventar inventar = new();
            ProstorijaDto dto = new(Int32.Parse(SpratUnos.Text), (TipProstorije)Enum.Parse(typeof(TipProstorije), tipUnos.Text), idUnos.Text, false, inventar);
            UpravnikKontroler.Instance.KreiranjeProstorije(dto);
            listaProstorija.ItemsSource = ProstorijaRepo.Instance.Prostorije;
            this.Close();

        }
    }
}
