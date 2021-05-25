using System;
using System.Windows.Controls;
using InformacioniSistemBolnice;
using InformacioniSistemBolnice.DTO;
using Servis;
using Model;


namespace Kontroler
{
    public class LekarKontroler
    {
        private static readonly Lazy<LekarKontroler>
             Lazy =
             new Lazy<LekarKontroler>
                 (() => new LekarKontroler());

        public static LekarKontroler Instance { get { return Lazy.Value; } }

        public void Zakazivanje(Termin izabranTermin) => TerminServis.Instance.Zakazivanje(izabranTermin);

        public void Otkazivanje(Termin izabranTermin) => TerminServis.Instance.Otkazivanje(izabranTermin);
        
        public void Pomeranje(Termin terminZaPomeranje, Termin noviTermin) 
            => TerminServis.Instance.Pomeranje(terminZaPomeranje, noviTermin);

        public void IzdavanjeRecepta(ReceptDto recept) => ReceptServis.Instance.IzdajRecept(recept);

        public void DodavanjeAnamneze(AnamnezaDto anamneza) => AnamnezaServis.Instance.DodajAnamnezu(anamneza);
        
        public void IzmenaLeka(LekDto lek) => LekServis.Instance.IzmenaLeka(lek);

        public void KreirajZahtev(ZahtevDto zahtev) => ZahtevServis.Instance.KreirajZahtev(zahtev);

        public void KreirajBolnickoLecenje(BolnickoLecenjeDto lecenje)
            => BolnickoLecenjeServis.Instance.KreirajBolnickoLecenje(lecenje);

        public void IzmeniBolnickoLecenje(BolnickoLecenjeDto lecenje)
            => BolnickoLecenjeServis.Instance.IzmeniBolnickoLecenje(lecenje);
    }
}