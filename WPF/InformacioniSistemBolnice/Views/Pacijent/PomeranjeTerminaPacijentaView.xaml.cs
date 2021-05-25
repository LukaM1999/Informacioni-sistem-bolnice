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
using Repozitorijum;
using Servis;
using Kontroler;
using System.Collections.ObjectModel;
using InformacioniSistemBolnice.Servis;
using InformacioniSistemBolnice.Utilities;

namespace InformacioniSistemBolnice
{
    public partial class PomeranjeTerminaPacijentaView : Window
    {
        private readonly Termin terminZaPomeranje;

        public PomeranjeTerminaPacijentaView(Termin izabranTermin)
        {
            InitializeComponent();
            terminZaPomeranje = izabranTermin;
            ponudjeniTermini.ItemsSource =
                new PredlogSlobodnihTerminaServis(izabranTermin).PonudiSlobodneTermineZaPomeranje();
        }

        private void PomeriTermin(object sender, RoutedEventArgs e)
        {
            PacijentKontroler.Instance.Pomeranje(terminZaPomeranje, (Termin)ponudjeniTermini.SelectedValue);
            Close();
        }
    }
}
