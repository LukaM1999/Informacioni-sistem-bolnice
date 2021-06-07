using InformacioniSistemBolnice.DTO;
using Servis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kontroler
{
    public class OpremaKontroler
    {
        private static readonly Lazy<OpremaKontroler> Lazy = new(() => new OpremaKontroler());
        public static OpremaKontroler Instance { get { return Lazy.Value; } }

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
    }
}
