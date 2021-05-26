using System.Windows;
using Repozitorijum;
using Model;

namespace InformacioniSistemBolnice
{
    public partial class PomeranjeZakazanogTermina : Window
    {
        public PomeranjeZakazanogTermina(IzborTerminaZaPomeranje izborTerminaZaPomeranje)
        {
            InitializeComponent();
            Termin terminZaPomeranje = UzmiTerminZaPomeranje(izborTerminaZaPomeranje);
            Termin zakazaniTermin = TerminRepo.Instance.NadjiTermin(terminZaPomeranje.Vreme,
                terminZaPomeranje.PacijentJmbg, terminZaPomeranje.LekarJmbg);
            pacijent.Content = zakazaniTermin.PacijentJmbg;
            lekar.Content = zakazaniTermin.LekarJmbg;
            tipTermina.Content = zakazaniTermin.Tip.ToString();
            prostorija.Content = zakazaniTermin.ProstorijaId;
        }

        private Termin UzmiTerminZaPomeranje(IzborTerminaZaPomeranje izborTerminaZaPomeranje)
        {
            TerminRepo.Instance.Deserijalizacija();
            if (izborTerminaZaPomeranje.ponudjeniTerminiZaPomeranje.SelectedItem != null)
                return (Termin)izborTerminaZaPomeranje.ponudjeniTerminiZaPomeranje.SelectedItem;
            return null;

        }

        private void izaberiTerminZaPomeranje_Click(object sender, RoutedEventArgs e)
        {
            IzborTerminaZaNovoZakazivanje izborTerminaZaNovoZakazivanje = new IzborTerminaZaNovoZakazivanje(this);
            izborTerminaZaNovoZakazivanje.Show();
        }
    }
}

