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
using Repozitorijum;
using Model;
using Servis;
using Kontroler;

namespace InformacioniSistemBolnice
{
    public partial class ZakazivanjeTerminaPacijentaView : Window
    {
        public string pacijentJmbg;

        public ZakazivanjeTerminaPacijentaView(string jmbgPacijenta)
        {
            InitializeComponent();
            lekari.ItemsSource = Lekari.Instance.listaLekara;
            pacijentJmbg = jmbgPacijenta;
        }

        private void potvrdaZakazivanjaDugme_Click(object sender, RoutedEventArgs e)
        {
            ZakazivanjeTerminaPacijentaDTO zakazivanje = new((DateTime)minDatumTermina.SelectedDate,
                (DateTime)maxDatumTermina.SelectedDate, (Lekar)lekari.SelectedItem, (bool)vremeRadio.IsChecked, pacijentJmbg);
            if (zakazivanje.MinDatum < zakazivanje.MaxDatum && zakazivanje.IzabranLekar != null)
                new IzborTerminaPacijentaView(zakazivanje) { Owner = this }.Show();
        }
    }
}
