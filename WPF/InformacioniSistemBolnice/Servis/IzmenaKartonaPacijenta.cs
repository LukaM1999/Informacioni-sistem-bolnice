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
            foreach (Pacijent pacijent in Pacijenti.Instance.ListaPacijenata)
            {
                if (PretragaPacijenta(pacijent, KreiranjeRecepta())) break;
            }
        }

        private bool PretragaPacijenta(Pacijent pacijent, Recept recept)
        {
            if (!pacijent.jmbg.Equals(receptDto.Pacijent.jmbg)) return false;
            pacijent.zdravstveniKarton.Recepti.Add(recept);
            Pacijenti.Instance.Serijalizacija();
            Pacijenti.Instance.Deserijalizacija();
            return true;
        }

        private Recept KreiranjeRecepta()
        {
            Recept recept = new Recept(receptDto.Id);
            recept.Terapije.Add(new Terapija(receptDto.PocetakTerapije, receptDto.KrajTerapije,
                receptDto.MeraLeka, receptDto.RedovnostUzimanjaLeka, receptDto.Lek));
            return recept;
        }


        public void DodavanjeAnamneze(AnamnezaForma anamneza)
        {
            Anamneza a = new Anamneza(anamneza.prvi.Text, anamneza.drugi.Text, anamneza.treci.Text, anamneza.peti.Text);
            foreach (Pacijent pacijent in Pacijenti.Instance.ListaPacijenata)
            {
                if (pacijent.jmbg.Equals(anamneza.p.jmbg))
                {
                    pacijent.zdravstveniKarton.anamneza = a;
                    Pacijenti.Instance.Serijalizacija();
                    Pacijenti.Instance.Deserijalizacija();
                    break;
                }
            }
        }

        public void DodajBeleske(Pacijent ulogovanPacijent, string beleske)
        {
            ZdravstveniKarton kartonPacijenta = ulogovanPacijent.zdravstveniKarton;
            Anamneza anamnezaPacijenta = kartonPacijenta.anamneza;
            anamnezaPacijenta.BeleskePacijenta = beleske;
            Pacijenti.Instance.Serijalizacija();
        }

        public Repozitorijum.Pacijenti pacijenti;
    }
}