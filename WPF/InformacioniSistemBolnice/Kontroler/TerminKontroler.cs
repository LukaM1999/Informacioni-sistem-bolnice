using Model;
using Servis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kontroler
{
    public class TerminKontroler
    {
        private static readonly Lazy<TerminKontroler> Lazy = new(() => new TerminKontroler());
        public static TerminKontroler Instance => Lazy.Value;

        public void ZakazivanjeTermina(Termin termin) => TerminServis.Instance.ZakaziTermin(termin);

        public void PomjeranjeTerminaPacijenata(Termin terminZaPomeranje, Termin noviTermin)
                => TerminServis.Instance.PomeriTermin(terminZaPomeranje, noviTermin);

        public void Otkazivanje(Termin termin) => TerminServis.Instance.OtkaziTermin(termin);

        public void PomeranjeVanrednogTerminaPacijenta(Termin stariTermin, Termin noviTermin)
                => UrgentniSistemServis.Instance.PomeranjeVanrednogTermina(stariTermin, noviTermin);

    }
}
