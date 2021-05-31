using System.Windows.Input;

namespace InformacioniSistemBolnice.ViewModels.SekretarViewModel
{
    public class PregledGostujucegNalogaViewModel
    {
        public PocetnaStranicaSekretara pocetna;
        public GostujuciNaloziProzor gostujuciNalozi;
        public Model.Pacijent GostujuciPacijent { get; set; }
        public ICommand Nazad { get; set; }

        public PregledGostujucegNalogaViewModel(GostujuciNaloziProzor gostujuciNaloziProzor, Model.Pacijent izabraniPacijent)
        {
            gostujuciNalozi = gostujuciNaloziProzor;
            pocetna = gostujuciNaloziProzor.pocetna;
            Nazad = new Command(o => PovratakNazad());
            GostujuciPacijent = izabraniPacijent;
        }

        private void PovratakNazad()
        {
            pocetna.contentControl.Content = new GostujuciNaloziProzor(this.pocetna);
        }
    }
}
