using InformacioniSistemBolnice.Views.UpravnikView;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InformacioniSistemBolnice.ViewModels.UpavnikViewModel
{
    public class LekInfoViewModel
    {
        public LekInfo Strana { get; set; }
        public ICommand Nazad { get; set; }
        public string Naziv { get; set; }
        public string Proizvodjac { get; set; }
        public string Zamena { get; set; }
        public string Sastojci { get; set; }
        public ObservableCollection<Alergen> ListaAlergena { get; set; }

        public LekInfoViewModel(LekInfo strana, Lek lek)
        {
            Strana = strana;
            Nazad = new Command(o => VratiSe());
            Naziv = lek.Naziv;
            Proizvodjac = lek.Proizvodjac;
            Zamena = lek.Zamena;
            Sastojci = lek.Sastojci;
            ListaAlergena = lek.Alergen;
        }

        public void VratiSe()
        {
            Strana.NavigationService.Navigate(new Lekovi());
        }
    }
}
