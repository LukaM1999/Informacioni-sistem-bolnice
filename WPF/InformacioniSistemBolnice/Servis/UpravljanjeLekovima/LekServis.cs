using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InformacioniSistemBolnice.DTO;
using Model;
using Repozitorijum;

namespace Servis
{
    public class LekServis
    {
        private static readonly Lazy<LekServis> Lazy = new(() => new LekServis());
        public static LekServis Instance => Lazy.Value;

        public void KreiranjeLeka(LekDto dto)
        {
            LekRepo.Instance.Dodaj(new Lek(dto.Naziv, dto.Proizvodjac, dto.Sastojci, dto.Zamena, dto.Alergeni));
        }
        public void UklanjanjeLeka(LekDto dto)
        {
            LekRepo.Instance.ObrisiPoId(dto.Naziv);
            LekRepo.Instance.Serijalizacija();
        }
        public void IzmenaLeka(LekDto dto)
        {
            (LekRepo.Instance.NadjiPoId(dto.Naziv) as Lek).Proizvodjac = dto.Proizvodjac;
            (LekRepo.Instance.NadjiPoId(dto.Naziv) as Lek).Zamena = dto.Zamena;
            (LekRepo.Instance.NadjiPoId(dto.Naziv) as Lek).Sastojci = dto.Sastojci;
            (LekRepo.Instance.NadjiPoId(dto.Naziv) as Lek).Alergen = dto.Alergeni;
            LekRepo.Instance.Serijalizacija();
        }
    }
}
