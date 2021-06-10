using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using InformacioniSistemBolnice.DTO;
using InformacioniSistemBolnice.Utilities;
using InformacioniSistemBolnice.ViewModels.PacijentViewModel.PacijentKomande;
using InformacioniSistemBolnice.Views.PacijentView;
using Kontroler;
using MahApps.Metro.Controls;
using Model;
using Repozitorijum;

namespace InformacioniSistemBolnice.ViewModels.PacijentViewModel
{
    [AddINotifyPropertyChangedInterface]
    public class KalendarViewModel
    {
        public string Title => "Kalendar";
        public static ObservableCollection<TerminDto> Appointments { get; set; }
        public TerminDto IzabranTermin { get; set; }
        public ICommand ZakazivanjeTermina { get; set; }
        public ICommand OtkazivanjeTermina { get; set; }
        public ICommand PomeranjeTermina { get; set; }
        public ICommand StatusObavestenja { get; set; }
        public bool IzabranaObavestenja { get; set; }
        public DateTime PrikazanDatum { get; set; } = DateTime.Today;
        public string TipPogleda { get; set; } = "Nedeljni pogled";
        private readonly Model.Pacijent ulogovanPacijent = GlavniProzorPacijentaView.ulogovanPacijent;

        public KalendarViewModel()
        {
            Appointments = new ObservableCollection<TerminDto>(ulogovanPacijent.ZakazaniTermini.
                Where(termin => termin.Status != StatusTermina.zavrsen).Select(termin =>
                    new TerminDto(TerminUtility.DobaviFormatiranPrikazTermina(termin.Tip, termin.LekarJmbg,
                            termin.ProstorijaId, termin.Status), termin.Vreme, termin.Vreme.AddMinutes(termin.Trajanje),
                        new SolidColorBrush(Colors.Red), new SolidColorBrush(Colors.White), termin.LekarJmbg,
                        termin.PacijentJmbg, termin.ProstorijaId, termin.Hitan, termin.Tip)));
            ZakazivanjeTermina = new Command(o => ZakaziTermin());
            OtkazivanjeTermina = new Command(o => OtkaziTermin(), o => IzabranTermin is not null);
            PomeranjeTermina = new Command(o => PomeriTermin(), o => IzabranTermin is not null &&
                                                                     IzabranTermin.Pocetak > DateTime.Now.AddHours(24) && !IzabranTermin.Naziv.Contains("pomeren"));
            StatusObavestenja = new Command(o => PromeniStatusObavestenja());
        }

        public void ZakaziTermin()
        {
            new ZakazivanjeTerminaPacijentaView(ulogovanPacijent.Jmbg).ShowDialog();
        }

        public void OtkaziTermin()
        {
            MessageBoxResult odluka = MessageBox.Show("Da li ste sigurni da želite da otkažete izabrani termin?",
                "Potvrda otkazivanja", MessageBoxButton.YesNo);
            if (odluka == MessageBoxResult.No) return;
            TerminKontroler.Instance.OtkaziTermin(TerminRepo.Instance.
                NadjiTermin(IzabranTermin.Pocetak, IzabranTermin.PacijentJmbg, IzabranTermin.LekarJmbg));
            Appointments.Remove(IzabranTermin);
            MessageBox.Show("Uspešno ste otkazali termin!");
            IzabranTermin = null;
        }

        public void PomeriTermin()
        {
            new PomeranjeTerminaPacijentaView(TerminRepo.Instance.
                NadjiTermin(IzabranTermin.Pocetak, IzabranTermin.PacijentJmbg, IzabranTermin.LekarJmbg)).ShowDialog();
        }

        public void PromeniStatusObavestenja()
        {
            IzabranaObavestenja = !IzabranaObavestenja;
        }
    }
}
