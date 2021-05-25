using System;
using System.Windows;
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
            if (ProveriAlergene(pacijent)) return;
            pacijent.zdravstveniKarton.DodajRecept(KreirajRecept());
            PacijentRepo.Instance.Serijalizacija();
        }

        private bool ProveriAlergene(Pacijent pacijent)
        {
            foreach (Alergen alergenLeka in receptDto.Lek.Alergen)
            {
                foreach (Alergen alergenPacijenta in pacijent.zdravstveniKarton.Alergeni)
                {
                    if (UporediAlergene(alergenLeka, alergenPacijenta)) return true;
                }
            }
            return false;
        }

        private bool UporediAlergene(Alergen alergenLeka, Alergen alergenPacijenta)
        {
            if (alergenLeka.Naziv != alergenPacijenta.Naziv) return false;
            System.Diagnostics.Debug.WriteLine("Usao u if");
            MessageBox.Show("Nije moguce propisati recept, pacijent je alergican na " + alergenLeka.Naziv);
            return true;
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