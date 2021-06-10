using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using InformacioniSistemBolnice.DTO;
using Model;
using PropertyChanged;
using Repozitorijum;

namespace InformacioniSistemBolnice.ViewModels.PacijentViewModel
{
    [AddINotifyPropertyChangedInterface]
    public class IstorijaViewModel
    {
        public string Title { get; } = "Istorija";
        public ObservableCollection<TerminPacijentaDto> ZavrseniTermini { get; set; }
        public TerminPacijentaDto IzabranTermin { get; set; }
        public ICommand OtvaranjeAnkete { get; set; }

        public IstorijaViewModel()
        {
            PostaviPacijentoveTermine();
            OtvaranjeAnkete = new Command(o => OtvoriAnketu(), 
                o => IzabranTermin is not null && IzabranTermin.Anketa is null);
        }

        private void PostaviPacijentoveTermine()
        {
            ObservableCollection<Termin> pacijentoviTermini = GlavniProzorPacijentaView.ulogovanPacijent.ZakazaniTermini;
            ZavrseniTermini = new ObservableCollection<TerminPacijentaDto>(pacijentoviTermini
                .Where(termin => termin.Status == StatusTermina.zavrsen).Select(termin =>
                    new TerminPacijentaDto(termin.Tip, termin.Vreme, termin.Trajanje + " minuta",
                        LekarRepo.Instance.NadjiLekara(termin.LekarJmbg), termin.ProstorijaId, termin.Hitan, termin.AnketaOLekaru)));
        }

        private void OtvoriAnketu()
        {
            new AnketaOLekaruFormaView(TerminRepo.Instance.NadjiTermin(IzabranTermin.Pocetak,
                GlavniProzorPacijentaView.ulogovanPacijent.Jmbg, IzabranTermin.Lekar.Jmbg)).ShowDialog();
        }
    }
}
