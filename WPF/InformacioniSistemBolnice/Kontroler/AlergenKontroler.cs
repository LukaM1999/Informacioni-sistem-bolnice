using InformacioniSistemBolnice.DTO;
using Model;
using Servis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kontroler
{
    public class AlergenKontroler
    {
        private static readonly Lazy<AlergenKontroler> Lazy = new(() => new AlergenKontroler());
        public static AlergenKontroler Instance => Lazy.Value;

        public void DefinisanjeAlergena(AlergenDto alergen) => AlergenServis.Instance.KreiranjeAlergena(alergen);

        public void IzmenaAlergena(AlergenDto noviAlergen, Alergen stariAlergen)
                => AlergenServis.Instance.IzmenaAlergena(noviAlergen, stariAlergen);

        public void UklanjanjeAlergena(Alergen alergen) => AlergenServis.Instance.UklanjanjeAlergena(alergen);

    }
}
