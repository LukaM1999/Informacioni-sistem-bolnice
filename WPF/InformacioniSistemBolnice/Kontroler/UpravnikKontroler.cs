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
        public void KreiranjeStatickeOpreme(MagacinDodajProzor p)
        {
            UpravljanjeStatickomOpremom.Instance.KreiranjeOpreme(p);
        }

        public void BrisanjeStatickeOpreme(MagacinProzor p)
        {
            UpravljanjeStatickomOpremom.Instance.UklanjanjeOpreme(p);
        }

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

        public void RasporedjivanjeDinamickeOpreme(Prostorija izProstorije, Prostorija uProstoriju, DinamickaOprema dinamickaOprema, int kolicina)
        {
            Servis.RasporedjivanjeDinamickeOpreme.Instance.Premestanje(izProstorije, uProstoriju, dinamickaOprema, kolicina);
        }

        public void RasporedjivanjeStatickeOpreme(Prostorija izProstorije, Prostorija uProstoriju, StatickaOprema statickaOprema, int kolicina, DateTime datum)
        {
            Servis.RasporedjivanjeStatickeOpreme.Instance.ZakazivanjePremestanja(izProstorije, uProstoriju, statickaOprema, kolicina, datum);
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