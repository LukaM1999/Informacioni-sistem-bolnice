using System;
using Model;
using Repozitorijum;
using InformacioniSistemBolnice;

namespace Servis
{
    public class AnamnezaServis
    {
        private static readonly Lazy<AnamnezaServis> Lazy = new(() => new AnamnezaServis());
        public static AnamnezaServis Instance => Lazy.Value;

        public void DodajAnamnezu(AnamnezaDto anamneza)
        {
            Anamneza novaAnamneza = new Anamneza(anamneza.SadasnjaBolest, anamneza.IstorijaBolesti,
                anamneza.PorodicneBolesti, anamneza.Zakljucak);
            Pacijent pacijent = PacijentRepo.Instance.NadjiPoJmbg(anamneza.PacijentJmbg);
            pacijent.zdravstveniKarton.Anamneza = novaAnamneza;
            PacijentRepo.Instance.Serijalizacija();
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