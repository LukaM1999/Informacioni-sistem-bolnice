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
using Repozitorijum;
using Model;
using Servis;
using Kontroler;
using PropertyChanged;

namespace InformacioniSistemBolnice
{
    [AddINotifyPropertyChangedInterface]
    public partial class ZakazivanjeTerminaPacijentaView : Window
    {
        public string pacijentJmbg;

        public DateTime MinDatum { get; set; } = DateTime.Today;

        public ZakazivanjeTerminaPacijentaView(string jmbgPacijenta)
        {
            InitializeComponent();
            lekari.ItemsSource = LekarRepo.Instance.Lekari;
            pacijentJmbg = jmbgPacijenta;
        }

        private void OtvoriIzborTermina(object sender, RoutedEventArgs e)
        {
            if (minDatumTermina.SelectedDate >= maxDatumTermina.SelectedDate && lekari.SelectedIndex is -1) return;
            ZakazivanjeTerminaDto zakazivanje = new((DateTime)minDatumTermina.SelectedDate,
            (DateTime)maxDatumTermina.SelectedDate, ((Lekar)lekari.SelectedItem).Jmbg, pacijentJmbg,
            vremePrioritet: (bool)vremeRadio.IsChecked);
            new IzborTerminaPacijentaView(zakazivanje) { Owner = this }.ShowDialog();
        }
    }
}
