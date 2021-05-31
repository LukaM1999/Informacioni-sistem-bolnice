using InformacioniSistemBolnice.DTO;
using Kontroler;
using Model;
using System;
using System.Windows.Input;

namespace InformacioniSistemBolnice.ViewModels.SekretarViewModel
{
    public class KreiranjeVestiViewModel
    {
        public PocetnaStranicaSekretara pocetna;
        public VestiProzor vesti;
        public UCMenuSekretara menu;
        public Vest NovaVest { get; set; }
        public ICommand Nazad { get; set; }
        public ICommand KreirajVest { get; set; }

        public KreiranjeVestiViewModel(PocetnaStranicaSekretara pocetnaStranicaSekretara, UCMenuSekretara menuSekretara,
                                 VestiProzor vestiProzor)
        {
            pocetna = pocetnaStranicaSekretara;
            vesti = vestiProzor;
            menu = menuSekretara;
            NovaVest = new Vest();
            KreirajVest = new Command(o => KreiranjeVesti());
            Nazad = new Command(o => PovratakNazad());
        }
        private void KreiranjeVesti()
        {
            VestDto vestDto = new VestDto(NovaVest.Id, NovaVest.Sadrzaj);
            vestDto.VremeObjave = (DateTime.Now);
            SekretarKontroler.Instance.KreiranjeVesti(vestDto);
            pocetna.contentControl.Content = new VestiProzor(menu);
        }

        private void PovratakNazad()
        {
            pocetna.contentControl.Content = vesti.Content;
        }
    }
}

