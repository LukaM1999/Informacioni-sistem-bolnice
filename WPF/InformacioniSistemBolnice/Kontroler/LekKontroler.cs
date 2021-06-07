using InformacioniSistemBolnice.DTO;
using Servis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kontroler
{
    public class LekKontroler
    {
        private static readonly Lazy<LekKontroler> Lazy = new(() => new LekKontroler());
        public static LekKontroler Instance { get { return Lazy.Value; } }

        public void KreiranjeLeka(LekDto dto) { LekServis.Instance.KreiranjeLeka(dto); }

        public void BrisanjeLeka(LekDto dto) { LekServis.Instance.UklanjanjeLeka(dto); }

        public void IzmenaLeka(LekDto dto) { LekServis.Instance.IzmenaLeka(dto); }
    }
}
