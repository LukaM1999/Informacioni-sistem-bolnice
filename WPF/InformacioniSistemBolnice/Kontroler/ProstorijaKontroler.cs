using InformacioniSistemBolnice.DTO;
using InformacioniSistemBolnice.Servis.UpravljanjeProstorijama;
using Servis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kontroler
{
    public class ProstorijaKontroler
    {
        private static readonly Lazy<ProstorijaKontroler> Lazy = new(() => new ProstorijaKontroler());
        public static ProstorijaKontroler Instance => Lazy.Value;

        public void KreiranjeProstorije(ProstorijaDto dto) => ProstorijaServis.Instance.KreiranjeProstorije(dto);

        public void UklanjanjeProstorije(ProstorijaDto dto) => ProstorijaServis.Instance.UklanjanjeProstorije(dto);

        public void IzmenaProstorije(ProstorijaDto dto) => ProstorijaServis.Instance.IzmenaProstorije(dto);

        public void TransformisiProstorije(TransformacijaProstorijaDto dto)
        {
            TransformacijaProstorijeServis.Instance.ZakaziTerminTransformacijeProstorija(dto);
        }

        public void ZakazivanjeRenoviranja(ProstorijaRenoviranjeDto dto)
        {
            RenoviranjeServis.Instance.ZakazivanjeRenoviranja(dto);
        }
    }
}
