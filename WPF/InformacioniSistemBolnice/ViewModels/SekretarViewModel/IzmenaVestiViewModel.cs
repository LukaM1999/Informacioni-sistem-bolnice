using InformacioniSistemBolnice.DTO;
using Kontroler;
using Model;
using Repozitorijum;
using System.Windows.Input;

namespace InformacioniSistemBolnice.ViewModels.SekretarViewModel
{
    class IzmenaVestiViewModel
    {
        public PocetnaStranicaSekretara pocetna;
        public VestiProzor vesti;
        public Vest Vest { get; set; }
        public ICommand Nazad { get; set; }
        public ICommand IzmeniVest { get; set; }

        public IzmenaVestiViewModel(VestiProzor vestiProzor, PocetnaStranicaSekretara pocetnaStranicaSekretara, Vest izabranaVest)
        {
            Vest = izabranaVest;
            vesti = vestiProzor;
            pocetna = pocetnaStranicaSekretara;
            Vest = izabranaVest;
            IzmeniVest = new Command(o => IzmenaVesti());
            Nazad = new Command(o => PovratakNazad());
        }

        public void IzmenaVesti()
        {
            VestDto vestDto = new VestDto(Vest.Id, Vest.Sadrzaj);
            VestKontroler.Instance.IzmenaVesti(vestDto, (Vest)vesti.ListaVesti.SelectedItem);
            VestRepo.Instance.Deserijalizacija();
            vesti.ListaVesti.ItemsSource = VestRepo.Instance.Vesti;
            pocetna.contentControl.Content = vesti.Content;
        }

        public void PovratakNazad()
        {
            pocetna.contentControl.Content = vesti.Content;
        }
    }
}
