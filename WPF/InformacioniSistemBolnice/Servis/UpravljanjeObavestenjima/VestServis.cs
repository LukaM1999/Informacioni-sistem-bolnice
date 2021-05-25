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

        public void KreirajVesti(VestDto vestDto)
        {
            Vest vest = new Vest(vestDto.Sadrzaj, vestDto.Id, vestDto.VremeObjave);
            VestRepo.Instance.DodajVest(vest);
        }

        public VestDto PregledajVest(Vest vest)
        {
            VestDto vestZaPrikazivanje = new(vest.Id, vest.Sadrzaj);
            vestZaPrikazivanje.VremeObjave = vest.VremeObjave;
            return vestZaPrikazivanje;
        }

        public void UkloniVest(Vest vest)
        {
            VestRepo.Instance.ObrisiVest(vest);
            VestRepo.Instance.Serijalizacija();
        }

        public void IzmeniVest(VestDto novaVest, Vest staraVest)
        {
            staraVest.Id = novaVest.Id;
            staraVest.Sadrzaj = novaVest.Sadrzaj;
            VestRepo.Instance.Serijalizacija();
        }
    }
}
