using System;
using System.Windows.Controls;
using InformacioniSistemBolnice;
using Model;
using Servis;

namespace Kontroler
{
    public class UpravnikKontroler
    {
        private static readonly Lazy<UpravnikKontroler> lazy = new(() => new UpravnikKontroler());
        public static UpravnikKontroler Instance { get { return lazy.Value; } }
        public void KreiranjeProstorije(ProstorijaDto dto) => UpravljanjeProstorijama.Instance.KreiranjeProstorije(dto);
        public void UklanjanjeProstorije(ProstorijaDto dto) => UpravljanjeProstorijama.Instance.UklanjanjeProstorije(dto);
        public void IzmenaProstorije(ProstorijaDto dto) => UpravljanjeProstorijama.Instance.IzmenaProstorije(dto);
        public void KreiranjeStatickeOpreme(StatickaOpremaDto dto) { UpravljanjeStatickomOpremom.Instance.KreiranjeOpreme(dto); }
        public void BrisanjeStatickeOpreme(StatickaOpremaDto dto) { UpravljanjeStatickomOpremom.Instance.UklanjanjeOpreme(dto); }
        public void IzmenaStatickeOpreme(StatickaOpremaDto dto) { UpravljanjeStatickomOpremom.Instance.IzmenaOpreme(dto); }
        public void KreiranjeDinamickeOpreme(DinamickaOpremaDto dto) { UpravljanjeDinamickomOpremom.Instance.KreiranjeOpreme(dto); }
        public void BrisanjeDinamickeOpreme(DinamickaOpremaDto dto) { UpravljanjeDinamickomOpremom.Instance.UklanjanjeOpreme(dto); }
        public void IzmenaDinamickeOpreme(DinamickaOpremaDto dto) { UpravljanjeDinamickomOpremom.Instance.IzmenaOpreme(dto); }
        public void RasporedjivanjeDinamickeOpreme(RaspodelaDinamickeOpremeDto dto) { Servis.RasporedjivanjeDinamickeOpreme.Instance.Premestanje(dto); }
        public void RasporedjivanjeStatickeOpreme(RaspodelaStatickeOpremeDto dto) { Servis.RasporedjivanjeStatickeOpreme.Instance.ZakazivanjePremestanja(dto); }
        public void KreiranjeLeka(LekDto dto) { UpravljanjeLekovima.Instance.KreiranjeLeka(dto); }
        public void BrisanjeLeka(LekDto dto) { UpravljanjeLekovima.Instance.UklanjanjeLeka(dto); }
        public void IzmenaLeka(LekDto dto)
        {
            UpravljanjeLekovima.Instance.IzmenaLeka(dto);
        }

        public void ZakazivanjeRenoviranja(ProstorijaRenoviranjeDto dto)
        {
            Renoviranje.Instance.ZakazivanjeRenoviranja(dto);
        }
    }
}