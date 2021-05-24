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
        public void IzmenaStatickeOpreme(StatickaOprema oprema, MagacinIzmeniProzor p)
        {
            UpravljanjeStatickomOpremom.Instance.IzmenaOpreme(oprema, p);
        }

        public void KreiranjeDinamickeOpreme(MagacinDodajDinamickuOpremu p)
        {
            UpravljanjeDinamickomOpremom.Instance.KreiranjeOpreme(p);
        }

        public void BrisanjeDinamickeOpreme(MagacinProzor p)
        {
            UpravljanjeDinamickomOpremom.Instance.UklanjanjeOpreme(p);
        }

        public void IzmenaDinamickeOpreme(DinamickaOprema oprema, MagacinIzmeniDinamickuOpremu p)
        {
            UpravljanjeDinamickomOpremom.Instance.IzmenaOpreme(oprema, p);
        }

        public void RasporedjivanjeDinamickeOpreme(RaspodelaDinamickeOpremeDto dto) 
        { 
            Servis.RasporedjivanjeDinamickeOpreme.Instance.Premestanje(dto);
        }

        public void RasporedjivanjeStatickeOpreme(RaspodelaStatickeOpremeDto dto)
        {
            Servis.RasporedjivanjeStatickeOpreme.Instance.ZakazivanjePremestanja(dto);
        }

        public void KreiranjeLeka(LekDto dto)
        {
            UpravljanjeLekovima.Instance.KreiranjeLeka(dto);
        }

        public void BrisanjeLeka(Lek lek)
        {
            UpravljanjeLekovima.Instance.UklanjanjeLeka(lek);
        }

        public void IzmenaLeka(LekDto dto, Lek lek)
        {
            UpravljanjeLekovima.Instance.IzmenaLeka(dto, lek);
        }

        public void ZakazivanjeRenoviranja(ProstorijaRenoviranjeDto dto)
        {
            Renoviranje.Instance.ZakazivanjeRenoviranja(dto);
        }
    }
}