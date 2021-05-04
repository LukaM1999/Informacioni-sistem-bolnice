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

        public void IzdavanjeRecepta(ReceptDto receptDto)
        {
            var recept = KreiranjeRecepta(receptDto);
            foreach (Pacijent pacijent in Pacijenti.Instance.listaPacijenata)
            {
                if (PretragaPacijenta(receptDto, pacijent, recept)) break;
            }
        }

        private static bool PretragaPacijenta(ReceptDto receptDto, Pacijent pacijent, Recept recept)
        {
            if (pacijent.jmbg.Equals(receptDto.Pacijent.jmbg))
            {
                pacijent.zdravstveniKarton.Recepti.Add(recept);
                Pacijenti.Instance.Serijalizacija();
                Pacijenti.Instance.Deserijalizacija();
                return true;
            }

            return false;
        }

        private static Recept KreiranjeRecepta(ReceptDto receptDto)
        {
            Recept recept = new Recept(receptDto.Id);
            recept.Terapije.Add(new Terapija(receptDto.PocetakTerapije, receptDto.KrajTerapije,
                receptDto.MeraLeka, receptDto.RedovnostUzimanjaLeka, receptDto.Lek));
            return recept;
        }


        public void DodavanjeAnamneze(AnamnezaForma anamneza)
        {
            Anamneza a = new Anamneza(anamneza.prvi.Text, anamneza.drugi.Text, anamneza.treci.Text, anamneza.peti.Text);
            foreach (Pacijent pacijent in Pacijenti.Instance.listaPacijenata)
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

        public Repozitorijum.Pacijenti pacijenti;
    }
}