using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media;
using InformacioniSistemBolnice.DTO;
using InformacioniSistemBolnice.Utilities;
using InformacioniSistemBolnice.ViewModels.PacijentViewModel.PacijentKomande;
using Model;
using Repozitorijum;

namespace InformacioniSistemBolnice.ViewModels.PacijentViewModel
{
    [AddINotifyPropertyChangedInterface]
    public class KalendarViewModel
    {
        public string Title => "Kalendar";
        public ObservableCollection<TerminDto> Appointments { get; set; }
        public TerminDto IzabranTermin { get; set; }
        public ICommand ZakazivanjeTermina { get; set; }
        public ICommand OtkazivanjeTermina { get; set; }
        public ICommand PomeranjeTermina { get; set; }
        public ICommand PregledTermina { get; set; }
        private readonly Model.Pacijent ulogovanPacijent = GlavniProzorPacijentaView.ulogovanPacijent;

        public KalendarViewModel()
        {
            Appointments = new ObservableCollection<TerminDto>(ulogovanPacijent.ZakazaniTermini.Select(termin =>
                    new TerminDto(TerminUtility.DobaviFormatiranPrikazTermina(termin.Tip, termin.LekarJmbg,
                            termin.ProstorijaId, termin.Status), termin.Vreme, termin.Vreme.AddMinutes(termin.Trajanje),
                        new SolidColorBrush(Colors.Red), new SolidColorBrush(Colors.White), termin.LekarJmbg,
                        termin.PacijentJmbg, termin.ProstorijaId, termin.Hitan, termin.Tip)));
            PregledTermina = new Command(o => PregledajTermin(), o => IzabranTermin is not null);
            ZakazivanjeTermina = new Command(o => ZakaziTermin());
            OtkazivanjeTermina = new Command(o => OtkaziTermin(), o => IzabranTermin is not null);
            PomeranjeTermina = new Command(o => PomeriTermin(), o => IzabranTermin is not null);
        }

        public void PregledajTermin()
        {

        }

        public void ZakaziTermin()
        {
            new ZakazivanjeTerminaPacijentaView(ulogovanPacijent.Jmbg).Show();
        }

        public void OtkaziTermin()
        {

        }

        public void PomeriTermin()
        {

        }
    }
}
