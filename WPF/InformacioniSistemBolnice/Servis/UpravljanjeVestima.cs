using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using InformacioniSistemBolnice;
using Model;
using Repozitorijum;

namespace Servis
{
    class UpravljanjeVestima
    {
        private static readonly Lazy<UpravljanjeVestima>
          lazy =
          new Lazy<UpravljanjeVestima>
              (() => new UpravljanjeVestima());

        public static UpravljanjeVestima Instance { get { return lazy.Value; } }

        public void KreiranjeVesti(VestDto vestDto)
        {
            Vest vest = new Vest(vestDto.Sadrzaj, vestDto.Id, vestDto.VremeObjave);
            Vesti.Instance.DodajVest(vest);
        }

        public VestDto PregledVesti(Vest vest)
        {
            VestDto vestZaPrikazivanje = new(vest.Id, vest.Sadrzaj);
            vestZaPrikazivanje.VremeObjave = vest.VremeObjave;
            return vestZaPrikazivanje;
        }

        public void UklanjanjeVesti(Vest vest)
        {
            Vesti.Instance.ObrisiVest(vest);
            Vesti.Instance.SacuvajPromene();
        }

        public void IzmenaVesti(VestDto novaVest, Vest staraVest)
        {
            staraVest.Id = novaVest.Id;
            staraVest.Sadrzaj = novaVest.Sadrzaj;
            Vesti.Instance.SacuvajPromene();
        }
    }
}
