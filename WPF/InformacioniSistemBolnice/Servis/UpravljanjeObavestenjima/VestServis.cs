using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using InformacioniSistemBolnice;
using InformacioniSistemBolnice.DTO;
using Model;
using Repozitorijum;

namespace Servis
{
    public class VestServis
    {
        private static readonly Lazy<VestServis> Lazy = new(() => new VestServis());
        public static VestServis Instance => Lazy.Value;

        public void KreiranjeVesti(VestDto vestDto)
        {
            Vest vest = new Vest(vestDto.Sadrzaj, vestDto.Id, vestDto.VremeObjave);
            VestRepo.Instance.DodajVest(vest);
        }

        public VestDto PregledVesti(Vest vest)
        {
            VestDto vestZaPrikazivanje = new(vest.Id, vest.Sadrzaj);
            vestZaPrikazivanje.VremeObjave = vest.VremeObjave;
            return vestZaPrikazivanje;
        }

        public void UklanjanjeVesti(Vest vest)
        {
            VestRepo.Instance.ObrisiVest(vest);
            VestRepo.Instance.SacuvajPromene();
        }

        public void IzmenaVesti(VestDto novaVest, Vest staraVest)
        {
            staraVest.Id = novaVest.Id;
            staraVest.Sadrzaj = novaVest.Sadrzaj;
            VestRepo.Instance.SacuvajPromene();
        }
    }
}
