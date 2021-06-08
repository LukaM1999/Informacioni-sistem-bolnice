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

namespace InformacioniSistemBolnice
{
    public partial class AnketaOBolniciFormaView : Window
    {
        private readonly string pacijentovJmbg;

        public AnketaOBolniciFormaView(string jmbgPacijenta)
        {
            InitializeComponent();
            pacijentovJmbg = jmbgPacijenta;
        }

        private void Potvrda(object sender, RoutedEventArgs e)
        {
            AnketaDto anketa = new(UBroj(IzabranoRadioDugme(Ljubaznost)),
                UBroj(IzabranoRadioDugme(Profesionalizam)), UBroj(IzabranoRadioDugme(Strpljenje)),
                UBroj(IzabranoRadioDugme(Komunikativnost)), UBroj(IzabranoRadioDugme(Azurnost)),
                UBroj(IzabranoRadioDugme(Korisnost)), Komentari.Text, pacijentovJmbg, DateTime.Now);
            AnketaKontroler.Instance.PosaljiAnketuOBolnici(anketa);
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
