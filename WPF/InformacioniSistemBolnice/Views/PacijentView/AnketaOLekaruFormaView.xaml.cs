using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class AnketaOLekaruFormaView : Window
    {
        private readonly Termin izabranTermin;

        public AnketaOLekaruFormaView(Termin izabranTermin)
        {
            InitializeComponent();
            this.izabranTermin = izabranTermin;
        }

        private void Potvrda(object sender, RoutedEventArgs e)
        {
            AnketaDto novaAnketa = new(UBroj(IzabranoRadioDugme(Ljubaznost)),
                UBroj(IzabranoRadioDugme(Profesionalizam)), UBroj(IzabranoRadioDugme(Strpljenje)),
                UBroj(IzabranoRadioDugme(Komunikativnost)), UBroj(IzabranoRadioDugme(Azurnost)),
                UBroj(IzabranoRadioDugme(Korisnost)), Komentari.Text);
            AnketaKontroler.Instance.PosaljiAnketuOLekaru(izabranTermin, novaAnketa);
            Close();
        }

        private static RadioButton IzabranoRadioDugme(Panel grupaZadovoljstva)
        {
            return grupaZadovoljstva.Children.OfType<RadioButton>().FirstOrDefault(r => r.IsChecked == true);
        }

        private static int UBroj(RadioButton meraZadovoljstva)
        {
            return meraZadovoljstva?.Name[^1] - '0' ?? 0;
        }
    }
}
