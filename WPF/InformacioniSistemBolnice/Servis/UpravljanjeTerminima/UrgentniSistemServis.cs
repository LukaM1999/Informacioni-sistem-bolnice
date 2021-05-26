using System;
using System.Linq;
using Repozitorijum;
using Model;
using System.Windows;
using System.Collections.ObjectModel;
using InformacioniSistemBolnice.DTO;

namespace Servis
{
    public class UrgentniSistemServis
    {
        private static readonly Lazy<UrgentniSistemServis> Lazy = new Lazy<UrgentniSistemServis>(()
            => new UrgentniSistemServis());

        public static UrgentniSistemServis Instance { get { return Lazy.Value; } }

        private ObservableCollection<Termin> slobodniTermini = new ObservableCollection<Termin>();
        public HitnoZakazivanjeDto zakazivanjeHitnogTerminaDto;
        public TerminiLekaraZaPomeranjeDto terminiLekaraZaPomeranjeDto;
        public GostujuciNalogDto gostujuciNalogDto;
        public DateTime pomocni;
        public DateTime slobodanTermin = IzgenerisiPrviNaredniTermin(DateTime.Today, DateTime.Now.Hour, DateTime.Now.Minute);

        public void ZakazivanjeHitnogTermina(HitnoZakazivanjeDto zakazivanjeHitnogTerminaDto)
        {
            pomocni = slobodanTermin;
            this.zakazivanjeHitnogTerminaDto = zakazivanjeHitnogTerminaDto;
            GenerisanjeNajblizegSlobodnogTerminaZaOdredjenuSpecijalizaciju();
            if (slobodniTermini.Count == 0)
            {
                MessageBox.Show("Svi termini kod lekara izabrane specijalizacije su zakazani u narednom periodu(1h). \n" +
                    "NAPOMENA: Postoji mogucnost pomeranja termina koji nije urgentan!");
                return;
            }
            ZakaziHitanTermin();
            slobodniTermini.Clear();
        }

        private void ZakaziHitanTermin()
        {
            Termin hitan = UzmiNajbliziSlobodanTerminIzListeSlobodnihTermina();
            hitan.Status = StatusTermina.zakazan;
            TerminServis.Instance.Zakazivanje(hitan);
        }

        private Termin UzmiNajbliziSlobodanTerminIzListeSlobodnihTermina()
        {
            Termin najbliziSlobodan = slobodniTermini.First();
            foreach (Termin t in slobodniTermini)
            {
                if (t.Vreme < najbliziSlobodan.Vreme)
                {
                    najbliziSlobodan = t;
                    break;
                }
            }
            return najbliziSlobodan;
        }


        private void GenerisanjeNajblizegSlobodnogTerminaZaOdredjenuSpecijalizaciju()
        {
            LekarRepo.Instance.Deserijalizacija();
            foreach (Lekar lekar in LekarRepo.Instance.Lekari)
            {
                if (lekar.Specijalizacija.Naziv == zakazivanjeHitnogTerminaDto.Specijalizacija)
                {
                    IzgenerisiSlobodneTermine(lekar);
                    ProveriStatusTermina(lekar);
                }
            }
        }

        private void ProveriStatusTermina(Lekar lekar)
        {
            foreach (Termin predlozenTermin in slobodniTermini.ToList())
            {
                TerminRepo.Instance.NadjiTermin(predlozenTermin.Vreme, predlozenTermin.LekarJmbg,
                                                 predlozenTermin.PacijentJmbg);
                slobodniTermini.Remove(predlozenTermin);
                break;
            }
        }

        private void IzgenerisiSlobodneTermine(Lekar lekar)
        {
            for (int j = 0; j < 2; j++)
            {
                Termin t = new Termin(slobodanTermin, 30.0, TipTermina.pregled, StatusTermina.slobodan,
                                      zakazivanjeHitnogTerminaDto.PacijentJmbg, lekar.Jmbg,
                                      zakazivanjeHitnogTerminaDto.ProstorijaId, true);
                slobodniTermini.Add(t);
                slobodanTermin = slobodanTermin.AddMinutes(30);
            }
            slobodanTermin = pomocni;
        }

        private static DateTime IzgenerisiPrviNaredniTermin(DateTime slobodanTermin, int sat, int minuta)
        {
            if (minuta >= 30) slobodanTermin = slobodanTermin.AddHours(sat + 1);
            else
            {
                slobodanTermin = slobodanTermin.AddHours(sat).AddMinutes(30);
            }
            return slobodanTermin;
        }

        public void PomeranjeTermina(TerminiLekaraZaPomeranjeDto terminiLekaraZaPomeranjeDto)
        {
            Termin staroVreme = (Termin)terminiLekaraZaPomeranjeDto.TerminZaPomeranje;
            Termin noviTermin = (Termin)terminiLekaraZaPomeranjeDto.NoviTermin;
            PomeriTermin(staroVreme, noviTermin);
            PomeriTerminPacijentu(staroVreme.Vreme, noviTermin);
            PomeriTerminLekaru(staroVreme, noviTermin);

        }

        private void PomeriTermin(Termin stariTermin, Termin noviTermin)
        {
            noviTermin.Status = StatusTermina.pomeren;
            TerminRepo.Instance.NadjiTermin(stariTermin.Vreme, stariTermin.LekarJmbg, stariTermin.PacijentJmbg);
            PromeniParametreTermina(stariTermin);
            TerminServis.Instance.Zakazivanje(stariTermin);
            TerminServis.Instance.Zakazivanje(noviTermin);
        }

        private void PomeriTerminLekaru(Termin staroVreme, Termin noviTermin)
        {
            foreach (Lekar lekar in LekarRepo.Instance.Lekari.ToList())
            {
                if (lekar.Jmbg == noviTermin.LekarJmbg)
                    SacuvajPromeneUTerminimaLekara(staroVreme.Vreme, noviTermin, lekar);
            }
        }

        private void SacuvajPromeneUTerminimaLekara(DateTime staroVreme, Termin noviTermin, Lekar lekar)
        {
            foreach (Termin stariTermin in lekar.ZauzetiTermini)
            {
                if (stariTermin.Vreme != staroVreme) continue;
                PromeniParametreTermina(stariTermin);
                lekar.ZauzetiTermini.Add(noviTermin);
                LekarRepo.Instance.Serijalizacija();
                LekarRepo.Instance.Deserijalizacija();
                break;

            }
        }

        private void PomeriTerminPacijentu(DateTime staroVreme, Termin noviTermin)
        {
            foreach (Pacijent pacijent in PacijentRepo.Instance.Pacijenti.ToList())
            {
                if (pacijent.Jmbg == noviTermin.PacijentJmbg)
                {
                    SacuvajPromeneUTerminimaPacijenta(staroVreme, noviTermin, pacijent);
                }
            }
        }

        private void SacuvajPromeneUTerminimaPacijenta(DateTime staroVreme, Termin noviTermin, Pacijent pacijent)
        {
            foreach (Termin stariTermin in pacijent.zakazaniTermini)
            {
                if (stariTermin.Vreme == staroVreme)
                {
                    PromeniParametreTermina(stariTermin);
                    pacijent.zakazaniTermini.Add(noviTermin);
                    PacijentRepo.Instance.Serijalizacija();
                    PacijentRepo.Instance.Deserijalizacija();
                    break;
                }
            }
        }

        private void PromeniParametreTermina(Termin stariTermin)
        {
            stariTermin.Status = StatusTermina.zakazan;
            stariTermin.Hitan = true;
            stariTermin.PacijentJmbg = terminiLekaraZaPomeranjeDto.PacijentJmbg;
        }


        public void PomeranjeVanrednogTermina(Termin terminZaPomeranje, Termin noviTermin)
        {
            noviTermin.Status = StatusTermina.pomeren;
            TerminServis.Instance.Otkazivanje(terminZaPomeranje);
            TerminServis.Instance.Zakazivanje(noviTermin);
        }
    }
}

