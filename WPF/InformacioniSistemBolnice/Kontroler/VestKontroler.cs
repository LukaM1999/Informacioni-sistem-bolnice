using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InformacioniSistemBolnice.DTO;
using Servis;
using Model;


namespace Kontroler
{
    public class VestKontroler
    {
        private static readonly Lazy<VestKontroler> Lazy = new(() => new VestKontroler());
        public static VestKontroler Instance => Lazy.Value;

        public void KreiranjeVesti(VestDto vestDto) => VestServis.Instance.KreirajVesti(vestDto);

        public VestDto PregledVesti(Vest vest) { return VestServis.Instance.PregledajVest(vest); }

        public void UklanjanjeVesti(Vest vest) => VestServis.Instance.UkloniVest(vest);

        public void IzmenaVesti(VestDto novaVest, Vest staraVest) => VestServis.Instance.IzmeniVest(novaVest, staraVest);
    }
}
