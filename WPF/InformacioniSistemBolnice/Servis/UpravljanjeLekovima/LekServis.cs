﻿using System;
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
            LekRepo.Instance.DodajLek(new(dto.Naziv, dto.Proizvodjac, dto.Sastojci, dto.Zamena, dto.Alergeni));
        }
        public void UklanjanjeLeka(LekDto dto)
        {
            LekRepo.Instance.BrisiPoNazivu(dto.Naziv);
            LekRepo.Instance.Serijalizacija();
        }
        public void IzmenaLeka(LekDto dto)
        {
            LekRepo.Instance.NadjiPoNazivu(dto.Naziv).Proizvodjac = dto.Proizvodjac;
            LekRepo.Instance.NadjiPoNazivu(dto.Naziv).Zamena = dto.Zamena;
            LekRepo.Instance.NadjiPoNazivu(dto.Naziv).Sastojci = dto.Sastojci;
            LekRepo.Instance.NadjiPoNazivu(dto.Naziv).Alergen = dto.Alergeni;
            LekRepo.Instance.Serijalizacija();
        }
    }
}