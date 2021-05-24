using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Repozitorijum;

namespace InformacioniSistemBolnice.Servis
{
    public class PredlogSlobodnihTerminaServis
    {
        private const int PocetakRadnogVremenaSati = 7;
        private const int DodatniDaniPredlaganjaTermina = 2;
        private const int DodatniDaniZaPomeranjeTermina = 2;
        private const int PolucasovniTerminiRadnogDana = 27;
        private const double SatiDoSledecegRadnogDana = 10.5;

        private readonly ObservableCollection<Termin> slobodniTermini = new();
        private Lekar izabranLekar;
        private readonly TimeSpan intervalDana;
        private DateTime slobodanTermin;
        private readonly Termin terminZaPomeranje;
        private readonly ZakazivanjeTerminaPacijentaDTO zakazivanjeInfo;

        public PredlogSlobodnihTerminaServis(ZakazivanjeTerminaPacijentaDTO zakazivanje)
        {
            izabranLekar = zakazivanje.IzabranLekar;
            intervalDana = zakazivanje.MaxDatum - zakazivanje.MinDatum;
            slobodanTermin = zakazivanje.MinDatum.AddHours(PocetakRadnogVremenaSati);
            zakazivanjeInfo = zakazivanje;
        }

        public PredlogSlobodnihTerminaServis(Termin izabranTermin)
        {
            izabranLekar = Lekari.Instance.NadjiLekara(izabranTermin.lekarJMBG);
            terminZaPomeranje = izabranTermin;
            zakazivanjeInfo = new ZakazivanjeTerminaPacijentaDTO(izabranTermin.pacijentJMBG);
        }

        public ObservableCollection<Termin> PonudiSlobodneTermine()
        {
            PronadjiSlobodneTermineZaViseDana(intervalDana.Days);
            IzbaciZauzetePredlozeneTermine();
            //slobodniTermini.Clear();
            if (slobodniTermini.Count != 0) return slobodniTermini;
            if (zakazivanjeInfo.VremePrioritet) PonudiTermineDrugogLekara();
            else PonudiVremenskiIzmenjeneTermine();
            return slobodniTermini;
        }

        public ObservableCollection<Termin> PonudiSlobodneTermineZaPomeranje()
        {
            slobodanTermin = terminZaPomeranje.vreme.
                Subtract(new TimeSpan(48 - 7 + terminZaPomeranje.vreme.Hour, 30, 0));
            PronadjiSlobodneTermineZaViseDana(DodatniDaniZaPomeranjeTermina);
            slobodanTermin = terminZaPomeranje.vreme.
                Subtract(new TimeSpan(terminZaPomeranje.vreme.Hour, 30, 0)).AddHours(24 + 7);
            PronadjiSlobodneTermineZaViseDana(DodatniDaniZaPomeranjeTermina);
            IzbaciZauzetePredlozeneTermine();
            return slobodniTermini;
        }

        private void PonudiVremenskiIzmenjeneTermine()
        {
            slobodanTermin = zakazivanjeInfo.MinDatum.Subtract(new TimeSpan(48 - PocetakRadnogVremenaSati, 0, 0));
            PronadjiSlobodneTermineZaViseDana(DodatniDaniPredlaganjaTermina);
            slobodanTermin = zakazivanjeInfo.MaxDatum.AddHours(PocetakRadnogVremenaSati);
            PronadjiSlobodneTermineZaViseDana(DodatniDaniPredlaganjaTermina);
            foreach (Termin predlozenTermin in slobodniTermini) IzbaciPoklapajuce(predlozenTermin);
        }

        private void PonudiTermineDrugogLekara()
        {
            slobodanTermin = zakazivanjeInfo.MinDatum.AddHours(PocetakRadnogVremenaSati);
            izabranLekar = Lekari.Instance.NadjiLekaraIsteSpecijalizacije(izabranLekar);
            if (izabranLekar is null) return;
            PronadjiSlobodneTermineZaViseDana(intervalDana.Days);
            foreach (Termin predlozenTermin in slobodniTermini) IzbaciPoklapajuce(predlozenTermin);
        }

        private void PronadjiSlobodneTermineZaViseDana(int danaZaPretragu)
        {
            for (int i = 0; i < danaZaPretragu; i++)
                slobodanTermin = PronadjiSlobodanTermin().AddHours(SatiDoSledecegRadnogDana);
        }

        private DateTime PronadjiSlobodanTermin()
        {
            for (int j = 0; j < PolucasovniTerminiRadnogDana; j++)
            {
                slobodniTermini.Add(new Termin(slobodanTermin, 30.0, TipTermina.pregled, StatusTermina.slobodan,
                    zakazivanjeInfo.PacijentovJmbg, izabranLekar.jmbg, null));
                PostaviIdSlobodneProstorije();
                if (!JePronadjenaSlobodnaProstorija()) slobodniTermini.Remove(slobodniTermini.Last());
                slobodanTermin = slobodanTermin.AddMinutes(30);
            }
            return slobodanTermin;
        }

        private bool JePronadjenaSlobodnaProstorija()
        {
            return slobodniTermini.Last().idProstorije is not null;
        }

        private void PostaviIdSlobodneProstorije()
        {
            Prostorija slobodnaProstorija = PronadjiSlobodnuProstoriju();
            if (slobodnaProstorija is not null) slobodniTermini.Last().idProstorije = slobodnaProstorija.Id;
        }

        private Prostorija PronadjiSlobodnuProstoriju()
        {
            foreach (Prostorija prostorija in Prostorije.Instance.ListaProstorija)
            {
                if (prostorija.Tip != TipProstorije.prostorijaZaPreglede || 
                    prostorija.NadjiTerminPoDatumu(slobodanTermin) != null) continue;
                if (prostorija.Renoviranje is null || !JeTerminUOpseguRenoviranja(prostorija)) return prostorija;
            }
            return null;
        }

        private bool JeTerminUOpseguRenoviranja(Prostorija prostorija)
        {
            return slobodanTermin >= prostorija.Renoviranje.PocetakRenoviranja &&
                   slobodanTermin <= prostorija.Renoviranje.KrajRenoviranja;
        }

        private void IzbaciZauzetePredlozeneTermine()
        {
            foreach (Termin predlozenTermin in slobodniTermini.ToList()) IzbaciPoklapajuce(predlozenTermin);
        }

        private bool IzbaciPoklapajuce(Termin predlozenTermin)
        {
            foreach (Termin postojeciTermin in izabranLekar.zauzetiTermini)
                if (predlozenTermin.vreme == postojeciTermin.vreme) return slobodniTermini.Remove(predlozenTermin);
            return false;
        }
    }
}
