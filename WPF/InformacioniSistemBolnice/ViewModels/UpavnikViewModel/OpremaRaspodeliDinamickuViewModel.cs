using InformacioniSistemBolnice.Views.UpravnikView;
using Kontroler;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Repozitorijum;

namespace InformacioniSistemBolnice.ViewModels.UpavnikViewModel
{
    public class OpremaRaspodeliDinamickuViewModel
    {
        public ICommand Nazad { get; set; }
        public ICommand Potvrdi { get; set; }
        public string Kolicina { get; set; }
        public Prostorija Prostorija { get; set; }
        public ObservableCollection<Prostorija> ListaProstorija = ProstorijaRepo.Instance.Prostorije;
        public OpremaRaspodeliDinamicku Strana { get; set; }
        public DinamickaOprema IzabranaOprema { get; set; }

        public OpremaRaspodeliDinamickuViewModel(OpremaRaspodeliDinamicku strana, DinamickaOprema oprema)
        {
            Strana = strana;
            IzabranaOprema = oprema;
            Nazad = new Command(o => VratiSe());
            Potvrdi = new Command(o => Potvrda());
            Kolicina = "0";
            Prostorija = ProstorijaRepo.Instance.Prostorije.First();
        }

        public void VratiSe()
        {
            Strana.NavigationService.Navigate(new Views.UpravnikView.Magacin());
        }

        public void Potvrda()
        {
            if (Kolicina.All(char.IsDigit) && !string.IsNullOrWhiteSpace(Kolicina)
                && Int32.Parse(Kolicina) <= IzabranaOprema.Kolicina)
            {
                Prostorija uProstoriju = Prostorija;
                OpremaKontroler.Instance.RasporedjivanjeDinamickeOpreme(new(null, uProstoriju.Id,
                    IzabranaOprema, Int32.Parse(Kolicina)));
                Strana.NavigationService.Navigate(new Views.UpravnikView.Magacin());
            }
        }
    }
}
