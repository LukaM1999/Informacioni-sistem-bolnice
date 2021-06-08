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

        public void ZakaziTermin(Termin izabranTermin) => TerminServis.Instance.ZakaziTermin(izabranTermin);

        public void OtkaziTermin(Termin izabranTermin) => TerminServis.Instance.OtkaziTermin(izabranTermin);

        public void PomeriTermin(Termin terminZaPomeranje, Termin noviTermin)
            => TerminServis.Instance.PomeriTermin(terminZaPomeranje, noviTermin);

        public void PomeriVanredanTerminPacijenta(Termin stariTermin, Termin noviTermin)
            => UrgentniSistemServis.Instance.PomeranjeVanrednogTermina(stariTermin, noviTermin);

    }
}
