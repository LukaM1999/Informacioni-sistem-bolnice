using InformacioniSistemBolnice.DTO;
using Servis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kontroler
{
    public class ZahtevKontroler
    {
        private static readonly Lazy<ZahtevKontroler> Lazy = new(() => new ZahtevKontroler());
        public static ZahtevKontroler Instance { get { return Lazy.Value; } }

        public void BrisanjeZahtevaLeka(ZahtevDto dto)
        {
            ZahtevServis.Instance.ObrisiZahtev(dto);
        }
    }
}
