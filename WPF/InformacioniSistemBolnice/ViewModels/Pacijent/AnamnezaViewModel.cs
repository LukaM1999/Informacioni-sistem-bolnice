using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Kontroler;
using Model;
using PropertyChanged;

namespace InformacioniSistemBolnice.ViewModels.Pacijent
{
    public class AnamnezaViewModel
    {
        private readonly Model.Pacijent ulogovanPacijent;
        public Anamneza PacijentovaAnamneza { get; set; }
        public ICommand SacuvajIzmeneAnamneze { get; set; }

        public AnamnezaViewModel(Model.Pacijent ulogovan)
        {
            ulogovanPacijent = ulogovan;
            ZdravstveniKarton kartonPacijenta = ulogovanPacijent.zdravstveniKarton;
            PacijentovaAnamneza = kartonPacijenta.anamneza;
            SacuvajIzmeneAnamneze = new Command(o => SacuvajIzmene());
        }

        private void SacuvajIzmene()
        {
            PacijentKontroler.Instance.DodajBeleske(ulogovanPacijent, PacijentovaAnamneza.BeleskePacijenta);
            foreach (Window zaZatvaranje in Application.Current.Windows) 
                if (zaZatvaranje.DataContext == this) zaZatvaranje.Close();
        }
    }
}
