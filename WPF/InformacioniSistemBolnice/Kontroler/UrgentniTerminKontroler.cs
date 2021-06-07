using InformacioniSistemBolnice.DTO;
using Servis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kontroler
{
    public class UrgentniTerminKontroler
    {
        private static readonly Lazy<UrgentniTerminKontroler> Lazy = new(() => new UrgentniTerminKontroler());
        public static UrgentniTerminKontroler Instance => Lazy.Value;

        public void ZakazivanjeHitnogTermina(HitnoZakazivanjeDto hitnoZakazivanjeDto)
               => UrgentniSistemServis.Instance.ZakazivanjeHitnogTermina(hitnoZakazivanjeDto);

        public void PomeranjeTermina(TerminiLekaraZaPomeranjeDto terminiLekaraZaPomeranjeDto)
               => UrgentniSistemServis.Instance.PomeranjeTermina(terminiLekaraZaPomeranjeDto);
    }
}
