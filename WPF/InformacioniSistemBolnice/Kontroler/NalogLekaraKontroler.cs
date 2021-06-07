using InformacioniSistemBolnice.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servis;
using Model;

namespace Kontroler
{
    public class NalogLekaraKontroler
    {
        private static readonly Lazy<NalogLekaraKontroler> Lazy = new(() => new NalogLekaraKontroler());
        public static NalogLekaraKontroler Instance => Lazy.Value;

        public bool KreiranjeNalogaLekara(LekarDto lekarDto)
        {
            if (LekarServis.Instance.KreirajNalog(lekarDto)) return true;
            else return false;
        }

        public void UklanjanjeNalogaLekara(Lekar lekar) => LekarServis.Instance.UkloniNalog(lekar);

        public void IzmenaNalogaLekara(LekarDto lekarDto, Lekar lekar) => LekarServis.Instance.IzmeniNalog(lekarDto, lekar);
    }
}
