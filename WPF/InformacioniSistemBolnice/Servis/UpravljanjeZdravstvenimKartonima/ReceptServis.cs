using System;
using Model;
using Repozitorijum;
using InformacioniSistemBolnice;
using InformacioniSistemBolnice.DTO;

namespace Servis
{
    public class ReceptServis
    {
        private static readonly Lazy<ReceptServis> Lazy = new(() => new ReceptServis());
        public static ReceptServis Instance => Lazy.Value;

        public ReceptDto receptDto;

        public void IzdajRecept(ReceptDto dto)
        {
            receptDto = dto;
            Pacijent pacijent = PacijentRepo.Instance.NadjiPoJmbg(receptDto.Pacijent.Jmbg);
            pacijent.zdravstveniKarton.DodajRecept(KreirajRecept());
            PacijentRepo.Instance.Serijalizacija();
        }

        private Recept KreirajRecept()
        {
            Recept recept = new Recept(receptDto.Id);
            recept.DodajTerapiju(new Terapija(receptDto.PocetakTerapije, receptDto.KrajTerapije,
                receptDto.MeraLeka, receptDto.RedovnostUzimanjaLeka, receptDto.Lek));
            return recept;
        }
    }
}