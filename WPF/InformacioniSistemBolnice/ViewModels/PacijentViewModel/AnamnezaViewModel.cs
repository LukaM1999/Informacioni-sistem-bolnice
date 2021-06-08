using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using InformacioniSistemBolnice.Utilities;
using InformacioniSistemBolnice.ViewModels;
using InformacioniSistemBolnice.Views.PacijentView;
using Kontroler;
using Model;
using PropertyChanged;

namespace InformacioniSistemBolnice.ViewModels.PacijentViewModel
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
            PacijentovaAnamneza = kartonPacijenta.Anamneza;
            SacuvajIzmeneAnamneze = new Command(o => SacuvajIzmene());
        }

        private void SacuvajIzmene()
        {
            ZdravstveniKartonKontroler.Instance.DodajAnamnezaBeleske(ulogovanPacijent, PacijentovaAnamneza.BeleskePacijenta);
            AnamnezaView anamnezaForma = (AnamnezaView)PronadjiProzorUtility.PronadjiProzor(this);
            anamnezaForma.Close();
        }
    }
}
