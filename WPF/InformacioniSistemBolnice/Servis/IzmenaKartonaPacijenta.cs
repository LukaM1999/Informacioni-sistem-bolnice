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
            Recept recept = new Recept(receptDto.Id);
            recept.terapije.Add(new Terapija(receptDto.PocetakTerapije, receptDto.KrajTerapije, receptDto.MeraLeka, receptDto.RedovnostUzimanjaLeka));
            receptDto.Pacijent.zdravstveniKarton.Recepti.Add(recept);
            Pacijenti.Instance.Serijalizacija();
            Pacijenti.Instance.Deserijalizacija();
        }


        public void DodavanjeAnamneze(AnamnezaForma anamneza)
        {
            Anamneza a = new Anamneza(anamneza.prvi.Text, anamneza.drugi.Text, anamneza.treci.Text, anamneza.cetvrti.Text, anamneza.peti.Text);
            anamneza.p.zdravstveniKarton.anamneza = a;

            Pacijenti.Instance.Serijalizacija();
            Pacijenti.Instance.Deserijalizacija();
        }

        public Repozitorijum.Pacijenti pacijenti;
    }
}