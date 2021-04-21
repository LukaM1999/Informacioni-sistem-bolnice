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

        public void IzdavanjeRecepta(ReceptForma recept)
        {
            Terapija t = new Terapija((DateTime)recept.Pocetak.SelectedDate, (DateTime)recept.Kraj.SelectedDate, double.Parse(recept.Mera.Text), double.Parse(recept.Redovnost.Text));
            Recept r = new Recept(recept.Id.Text);
            r.terapija.Add(t);
            recept.p.zdravstveniKarton.recept = r;
            Pacijenti.Instance.Serijalizacija("../../../json/pacijenti.json");
            Pacijenti.Instance.Deserijalizacija("../../../json/pacijenti.json");
        }


        public void DodavanjeAnamneze(AnamnezaForma anamneza)
        {
            Anamneza a = new Anamneza(anamneza.prvi.Text, anamneza.drugi.Text, anamneza.treci.Text, anamneza.cetvrti.Text, anamneza.peti.Text);
            anamneza.p.zdravstveniKarton.anamneza = a;

            Pacijenti.Instance.Serijalizacija("../../../json/pacijenti.json");
            Pacijenti.Instance.Deserijalizacija("../../../json/pacijenti.json");
        }

        public Repozitorijum.Pacijenti pacijenti;

    }
}