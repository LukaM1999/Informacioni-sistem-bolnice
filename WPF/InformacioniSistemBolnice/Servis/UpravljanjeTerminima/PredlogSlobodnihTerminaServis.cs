using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InformacioniSistemBolnice.DTO;
using InformacioniSistemBolnice.Utilities;
using Model;
using Repozitorijum;

namespace InformacioniSistemBolnice.Servis
{
    public class PredlogSlobodnihTerminaServis
    {
        private readonly ObservableCollection<Termin> slobodniTermini = new();
        private Lekar izabranLekar;
        private readonly TimeSpan intervalDana;
        private DateTime slobodanTermin;
        private readonly Termin terminZaPomeranje;
        private readonly ZakazivanjeTerminaPacijentaDto zakazivanjeInfo;

        public PredlogSlobodnihTerminaServis(ZakazivanjeTerminaPacijentaDto zakazivanje)
        {
            izabranLekar = LekarRepo.Instance.NadjiLekara(zakazivanje.LekarJmbg);
            intervalDana = zakazivanje.MaxDatum - zakazivanje.MinDatum;
            slobodanTermin = zakazivanje.MinDatum.AddHours(TerminUtility.PocetakRadnogVremenaSati);
            zakazivanjeInfo = zakazivanje;
        }

        public PredlogSlobodnihTerminaServis(Termin izabranTermin)
        {
            izabranLekar = LekarRepo.Instance.NadjiLekara(izabranTermin.LekarJmbg);
            terminZaPomeranje = izabranTermin;
            zakazivanjeInfo = new ZakazivanjeTerminaPacijentaDto(izabranTermin.PacijentJmbg);
        }

        public ObservableCollection<Termin> PonudiSlobodneTermine()
        {
            PronadjiSlobodneTermineZaViseDana(intervalDana.Days);
            IzbaciZauzetePredlozeneTermine();
            //slobodniTermini.Clear();
            if (slobodniTermini.Count is not 0) return slobodniTermini;
            return zakazivanjeInfo.VremePrioritet ? PonudiTermineDrugogLekara() : PonudiVremenskiIzmenjeneTermine();
        }

        public ObservableCollection<Termin> PonudiSlobodneTermineZaPomeranje()
        {
            slobodanTermin = TerminUtility.IzracunajPomeranjeUnazad(terminZaPomeranje);
            PronadjiSlobodneTermineZaViseDana(TerminUtility.DodatniDaniZaPomeranjeTermina);
            slobodanTermin = TerminUtility.IzracunajPomeranjeUnapred(terminZaPomeranje);
            PronadjiSlobodneTermineZaViseDana(TerminUtility.DodatniDaniZaPomeranjeTermina);
            IzbaciZauzetePredlozeneTermine();
            return slobodniTermini;
        }

        private ObservableCollection<Termin> PonudiVremenskiIzmenjeneTermine()
        {
            slobodanTermin = zakazivanjeInfo.MinDatum.Subtract(new TimeSpan(TerminUtility.DvaDanaUnazad, 0, 0));
            PronadjiSlobodneTermineZaViseDana(TerminUtility.DodatniDaniPredlaganjaTermina);
            slobodanTermin = zakazivanjeInfo.MaxDatum.AddHours(TerminUtility.PocetakRadnogVremenaSati);
            PronadjiSlobodneTermineZaViseDana(TerminUtility.DodatniDaniPredlaganjaTermina);
            foreach (Termin predlozenTermin in slobodniTermini) IzbaciPoklapajuce(predlozenTermin);
            return slobodniTermini;
        }

        private ObservableCollection<Termin> PonudiTermineDrugogLekara()
        {
            slobodanTermin = zakazivanjeInfo.MinDatum.AddHours(TerminUtility.PocetakRadnogVremenaSati);
            izabranLekar = LekarRepo.Instance.NadjiLekaraIsteSpecijalizacije(izabranLekar);
            if (izabranLekar is null) return null;
            PronadjiSlobodneTermineZaViseDana(intervalDana.Days);
            foreach (Termin predlozenTermin in slobodniTermini) IzbaciPoklapajuce(predlozenTermin);
            return slobodniTermini;
        }

        private void PronadjiSlobodneTermineZaViseDana(int danaZaPretragu)
        {
            for (int i = 0; i < danaZaPretragu; i++)
                slobodanTermin = PronadjiSlobodanTermin().AddHours(TerminUtility.SatiDoSledecegRadnogDana);
        }

        private DateTime PronadjiSlobodanTermin()
        {
            for (int j = 0; j < TerminUtility.PolucasovniTerminiRadnogDana; j++)
            {
                slobodniTermini.Add(new Termin(slobodanTermin, TerminUtility.PodrazumevanoTrajanjeTermina, 
                    TipTermina.pregled, StatusTermina.slobodan,
                    zakazivanjeInfo.PacijentJmbg, izabranLekar.Jmbg, zakazivanjeInfo.ProstorijaId));
                PostaviIdSlobodneProstorije();
                if (!JePronadjenaSlobodnaProstorija()) slobodniTermini.Remove(slobodniTermini.Last());
                slobodanTermin = slobodanTermin.AddMinutes(TerminUtility.PodrazumevanoTrajanjeTermina);
            }
            return slobodanTermin;
        }

        private bool JePronadjenaSlobodnaProstorija()
        {
            return slobodniTermini.Last().ProstorijaId is not null;
        }

        private void PostaviIdSlobodneProstorije()
        {
            Prostorija slobodnaProstorija = PronadjiSlobodnuProstoriju();
            if (slobodnaProstorija is not null) slobodniTermini.Last().ProstorijaId = slobodnaProstorija.Id;
        }

        private Prostorija PronadjiSlobodnuProstoriju()
        {
            foreach (Prostorija prostorija in ProstorijaRepo.Instance.Prostorije)
            {
                if (prostorija.Tip != TipProstorije.prostorijaZaPreglede || 
                    prostorija.NadjiTerminPoDatumu(slobodanTermin) is not null) continue;
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
            foreach (Termin postojeciTermin in izabranLekar.ZakazaniTermini)
                if (predlozenTermin.Vreme == postojeciTermin.Vreme) return slobodniTermini.Remove(predlozenTermin);
            return false;
        }
    }
}
