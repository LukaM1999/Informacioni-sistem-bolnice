using InformacioniSistemBolnice.DTO;
using Servis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Kontroler
{
    public class NalogPacijentaKontroler
    {
        private static readonly Lazy<NalogPacijentaKontroler> Lazy = new(() => new NalogPacijentaKontroler());
        public static NalogPacijentaKontroler Instance => Lazy.Value;

        public bool KreiranjeNaloga(PacijentDto pacijentDto)
        {
            if (PacijentServis.Instance.KreirajNalog(pacijentDto)) return true;
            return false;
        }

        public void UklanjanjeNaloga(Pacijent pacijent) => PacijentServis.Instance.UkloniNalog(pacijent);

        public void UklanjanjeGostujucegNaloga(Pacijent pacijent)
                => PacijentServis.Instance.UkloniGostujuciNalog(pacijent);

        public void IzmenaNaloga(PacijentDto pacijentDto, Pacijent pacijent)
            => PacijentServis.Instance.IzmeniNalog(pacijentDto, pacijent);

        public void KreiranjeGostujucegPacijenta(GostujuciNalogDto gostujuciNalogDto)
              => PacijentServis.Instance.KreirajGostujuciNalog(gostujuciNalogDto);

    }
}
