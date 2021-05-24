using System;
using Model;
using Repozitorijum;
using InformacioniSistemBolnice;

namespace Servis
{
    public class IzmenaKartonaPacijenta
    {
        private static readonly Lazy<IzmenaKartonaPacijenta>
           lazy =
           new Lazy<IzmenaKartonaPacijenta>
               (() => new IzmenaKartonaPacijenta());

        public static IzmenaKartonaPacijenta Instance { get { return lazy.Value; } }

        public ReceptDto receptDto;

        public void IzdavanjeRecepta(ReceptDto dto)
        {
            receptDto = dto;
            foreach (Pacijent pacijent in PacijentRepo.Instance.Pacijenti)
            {
                if (PretragaPacijenta(pacijent, KreiranjeRecepta())) break;
            }
        }

        private bool PretragaPacijenta(Pacijent pacijent, Recept recept)
        {
            if (!pacijent.Jmbg.Equals(receptDto.Pacijent.Jmbg)) return false;
            pacijent.zdravstveniKarton.recepti.Add(recept);
            PacijentRepo.Instance.Serijalizacija();
            PacijentRepo.Instance.Deserijalizacija();
            return true;
        }

        private Recept KreiranjeRecepta()
        {
            Recept recept = new Recept(receptDto.Id);
            recept.terapije.Add(new Terapija(receptDto.PocetakTerapije, receptDto.KrajTerapije,
                receptDto.MeraLeka, receptDto.RedovnostUzimanjaLeka, receptDto.Lek));
            return recept;
        }


        public void DodavanjeAnamneze(AnamnezaForma anamneza)
        {
            Anamneza a = new Anamneza(anamneza.prvi.Text, anamneza.drugi.Text, anamneza.treci.Text, anamneza.peti.Text);
            foreach (Pacijent pacijent in PacijentRepo.Instance.Pacijenti)
            {
                if (pacijent.Jmbg.Equals(anamneza.p.Jmbg))
                {
                    pacijent.zdravstveniKarton.Anamneza = a;
                    PacijentRepo.Instance.Serijalizacija();
                    PacijentRepo.Instance.Deserijalizacija();
                    break;
                }
            }
        }

        public void DodajBeleske(Pacijent ulogovanPacijent, string beleske)
        {
            ZdravstveniKarton kartonPacijenta = ulogovanPacijent.zdravstveniKarton;
            Anamneza anamnezaPacijenta = kartonPacijenta.Anamneza;
            anamnezaPacijenta.BeleskePacijenta = beleske;
            PacijentRepo.Instance.Serijalizacija();
        }
    }
}