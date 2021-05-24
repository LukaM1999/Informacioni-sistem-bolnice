using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Repozitorijum;

namespace Servis
{
    public class UpravljanjeLekovima
    {
        private static readonly Lazy<UpravljanjeLekovima> lazy = new Lazy<UpravljanjeLekovima> (() => new UpravljanjeLekovima());
        public static UpravljanjeLekovima Instance { get { return lazy.Value; } }
        public void KreiranjeLeka(LekDto dto)
        {
            LekRepo.Instance.Lekovi.Add(new(dto.Naziv, dto.Proizvodjac, dto.Sastojci, dto.Zamena, dto.Alergeni));
            LekRepo.Instance.SacuvajPromene();
        }
        public void UklanjanjeLeka(LekDto dto)
        {
            LekRepo.Instance.BrisiPoNazivu(dto.Naziv);
            LekRepo.Instance.SacuvajPromene();
        }
        public void IzmenaLeka(LekDto dto)
        {
            LekRepo.Instance.NadjiPoNazivu(dto.Naziv).Proizvodjac = dto.Proizvodjac;
            LekRepo.Instance.NadjiPoNazivu(dto.Naziv).Zamena = dto.Zamena;
            LekRepo.Instance.NadjiPoNazivu(dto.Naziv).Sastojci = dto.Sastojci;
            LekRepo.Instance.NadjiPoNazivu(dto.Naziv).Alergen = dto.Alergeni;
            LekRepo.Instance.SacuvajPromene();
        }
    }
}
