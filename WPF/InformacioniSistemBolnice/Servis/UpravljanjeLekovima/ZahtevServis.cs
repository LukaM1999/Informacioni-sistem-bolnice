using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Repozitorijum;

namespace Servis
{
    public class ZahtevServis
    {
        private static readonly Lazy<ZahtevServis> Lazy = new(() => new ZahtevServis());
        public static ZahtevServis Instance => Lazy.Value;

        public void KreirajZahtev(ZahtevDto noviZahtev)
        {
            ZahtevRepo.Instance.DodajZahtev(new Zahtev(noviZahtev.Komentar, noviZahtev.Potpis));
            ZahtevRepo.Instance.Serijalizacija();
        }

        public void ObrisiZahtev(ZahtevDto dto)
        {
            Zahtev izabraniZahtev = new(dto.Komentar, dto.Potpis);
            ZahtevRepo.Instance.BrisiPoKomentaru(izabraniZahtev.Komentar);
            ZahtevRepo.Instance.Serijalizacija();
        }
    }
}
