using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using InformacioniSistemBolnice.Servis;
using InformacioniSistemBolnice.Utilities;
using Kontroler;
using Model;
using Repozitorijum;
using Servis;

namespace InformacioniSistemBolnice
{
    public partial class IzborTerminaPacijentaView : Window
    {
        public IzborTerminaPacijentaView(ZakazivanjeTerminaPacijentaDTO zakazivanje)
        {
            InitializeComponent();
            ponudjeniTermini.ItemsSource = new PredlogSlobodnihTerminaServis(zakazivanje).PonudiSlobodneTermine();
        }

        private void zakaziDugme_Click(object sender, RoutedEventArgs e)
        {
            PacijentKontroler.Instance.Zakazivanje((Termin)ponudjeniTermini.SelectedValue);
            Owner?.Close();
            Close();
        }
    }
}
