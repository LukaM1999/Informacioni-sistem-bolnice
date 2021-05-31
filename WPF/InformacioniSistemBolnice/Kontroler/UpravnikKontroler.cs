using System;
using System.Windows.Controls;
using InformacioniSistemBolnice;
using InformacioniSistemBolnice.DTO;
using Model;
using Servis;
using Repozitorijum;
using InformacioniSistemBolnice.Servis.UpravljanjeProstorijama;

namespace Kontroler
{
    public class UpravnikKontroler
    {
        private static readonly Lazy<UpravnikKontroler> Lazy = new(() => new UpravnikKontroler());
        public static UpravnikKontroler Instance { get { return Lazy.Value; } }

        public void KreiranjeProstorije(ProstorijaDto dto) => ProstorijaServis.Instance.KreiranjeProstorije(dto);

        public void UklanjanjeProstorije(ProstorijaDto dto) => ProstorijaServis.Instance.UklanjanjeProstorije(dto);

        public void IzmenaProstorije(ProstorijaDto dto) => ProstorijaServis.Instance.IzmenaProstorije(dto);

        public void KreiranjeStatickeOpreme(StatickaOpremaDto dto) { StatickaOpremaServis.Instance.KreiranjeOpreme(dto); }

        public void BrisanjeStatickeOpreme(StatickaOpremaDto dto) { StatickaOpremaServis.Instance.UklanjanjeOpreme(dto); }

        public void IzmenaStatickeOpreme(StatickaOpremaDto dto) { StatickaOpremaServis.Instance.IzmenaOpreme(dto); }

        public void KreiranjeDinamickeOpreme(DinamickaOpremaDto dto) 
        { 
            DinamickaOpremaServis.Instance.KreiranjeOpreme(dto); 
        }

        public void BrisanjeDinamickeOpreme(DinamickaOpremaDto dto) 
        { 
            DinamickaOpremaServis.Instance.UklanjanjeOpreme(dto); 
        }

        public void IzmenaDinamickeOpreme(DinamickaOpremaDto dto) { DinamickaOpremaServis.Instance.IzmenaOpreme(dto); }

        public void RasporedjivanjeDinamickeOpreme(RaspodelaDinamickeOpremeDto dto) 
        { 
            Servis.RaspodelaDinamickeOpremeServis.Instance.Premestanje(dto); 
        }

        public void RasporedjivanjeStatickeOpreme(RaspodelaStatickeOpremeDto dto) 
        { 
            Servis.RaspodelaStatickeOpremeServis.Instance.ZakazivanjePremestanja(dto); 
        }

        public void KreiranjeLeka(LekDto dto) { LekServis.Instance.KreiranjeLeka(dto); }

        public void BrisanjeLeka(LekDto dto) { LekServis.Instance.UklanjanjeLeka(dto); }

        public void IzmenaLeka(LekDto dto) { LekServis.Instance.IzmenaLeka(dto); }

        public void ZakazivanjeRenoviranja(ProstorijaRenoviranjeDto dto)
        {
            RenoviranjeServis.Instance.ZakazivanjeRenoviranja(dto);
        }

        public void BrisanjeZahtevaLeka(ZahtevDto dto)
        {
            ZahtevServis.Instance.ObrisiZahtev(dto);
        }

        public void TransformisiProstorije(TransformacijaProstorijaDto dto)
        {
            TransformacijaProstorijeServis.Instance.ZakaziTerminTransformacijeProstorija(dto);
        }
    }
}