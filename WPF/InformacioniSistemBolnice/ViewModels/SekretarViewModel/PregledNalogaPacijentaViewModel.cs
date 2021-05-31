using System.Windows.Input;

namespace InformacioniSistemBolnice.ViewModels.SekretarViewModel
{
    public class PregledNalogaPacijentaViewModel
    {
        public PocetnaStranicaSekretara pocetna;
        public PacijentiProzor pacijenti;
        public Model.Pacijent Pacijent { get; set; }
        public ICommand Nazad { get; set; }
       
        public PregledNalogaPacijentaViewModel(PacijentiProzor pacijentiProzor, Model.Pacijent izabraniPacijent)
        {
            Pacijent = izabraniPacijent;
            pacijenti = pacijentiProzor;
            pocetna = pacijentiProzor.pocetna;
            Nazad = new Command(o => PovratakNazad());
        }
       
        private void PovratakNazad()
        {
            pocetna.contentControl.Content = new PacijentiProzor(pocetna);
        }

    }
}
