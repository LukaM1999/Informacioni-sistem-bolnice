using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InformacioniSistemBolnice;
using Repozitorijum;
using Model;
using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace Servis
{
    public class UrgentniSistemServis
    {
        private static readonly Lazy<UrgentniSistemServis>
           Lazy =
           new Lazy<UrgentniSistemServis>
               (() => new UrgentniSistemServis());

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
            TerminRepo.Instance.Termini.Add(hitan);
            TerminRepo.Instance.Serijalizacija();
            TerminRepo.Instance.Deserijalizacija();
            SacuvajHitanTerminUListuZakazanihTerminaPacijenta(hitan);
            SacuvajHitanTerminUListuZakazanihTerminaLekara(hitan);
        }

        private static void SacuvajHitanTerminUListuZakazanihTerminaLekara(Termin hitan)
        {
            foreach (Lekar lekar in LekarRepo.Instance.Lekari)
            {
                if (lekar.Jmbg == hitan.LekarJmbg)
                {
                    lekar.ZauzetiTermini.Add(hitan);
                    LekarRepo.Instance.Serijalizacija();
                    LekarRepo.Instance.Deserijalizacija();
                }
            }
        }
        private static void SacuvajHitanTerminUListuZakazanihTerminaPacijenta(Termin hitan)
        {
            foreach (Pacijent pacijent in PacijentRepo.Instance.Pacijenti)
            {
                if (pacijent.Jmbg == hitan.PacijentJmbg)
                {
                    pacijent.zakazaniTermini.Add(hitan);
                    PacijentRepo.Instance.Serijalizacija();
                    PacijentRepo.Instance.Deserijalizacija();
                }
            }
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
                foreach (Termin postojeciTermin in lekar.ZauzetiTermini)
                {
                    if (predlozenTermin.Vreme == postojeciTermin.Vreme)
                    {
                        slobodniTermini.Remove(predlozenTermin);
                        break;
                    }
                }
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
            this.terminiLekaraZaPomeranjeDto = terminiLekaraZaPomeranjeDto;
            DateTime staroVreme = ((Termin)terminiLekaraZaPomeranjeDto.TerminZaPomeranje).Vreme;
            Termin noviTermin = (Termin)terminiLekaraZaPomeranjeDto.NoviTermin;
            PomeriTermin(staroVreme, noviTermin);
            PomeriTerminPacijentu(staroVreme, noviTermin);
            PomeriTerminLekaru(staroVreme, noviTermin);

        }

        private void PomeriTermin(DateTime staroVreme, Termin noviTermin)
        {
            noviTermin.Status = StatusTermina.pomeren;
            foreach (Termin stariTermin in TerminRepo.Instance.Termini.ToList())
            {
                if (stariTermin.Vreme == staroVreme && stariTermin.LekarJmbg == ((Termin)terminiLekaraZaPomeranjeDto.NoviTermin).LekarJmbg)
                {
                    PromeniParametreTermina(stariTermin);
                    TerminRepo.Instance.Termini.Add(noviTermin);
                    TerminRepo.Instance.Serijalizacija();
                    TerminRepo.Instance.Deserijalizacija();

                    break;
                }
            }
        }

        private void PomeriTerminLekaru(DateTime staroVreme, Termin noviTermin)
        {
            foreach (Lekar lekar in LekarRepo.Instance.Lekari.ToList())
            {
                if (lekar.Jmbg == noviTermin.LekarJmbg)
                    SacuvajPromeneUTerminimaLekara(staroVreme, noviTermin, lekar);
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


        public void KreiranjeGostujcegPacijenta(GostujuciNalogDto gostujuciNalog)
        {
            gostujuciNalogDto = gostujuciNalog;
            ObservableCollection<Termin> zakazaniTermini = new ObservableCollection<Termin>();
            Pacijent gostujuciPacijent = new Pacijent(new Osoba(gostujuciNalogDto.Ime, gostujuciNalogDto.Prezime, gostujuciNalogDto.Jmbg,
                gostujuciNalogDto.DatumRodjenja, gostujuciNalogDto.Telefon, gostujuciNalogDto.Email,
                new Korisnik(null, null, (Model.UlogaKorisnika)Enum.Parse(typeof(Model.UlogaKorisnika), "pacijent")), null));
            gostujuciPacijent.zakazaniTermini = zakazaniTermini;
            DodajGostujucegPacijenta(gostujuciPacijent);
        }

        private static void DodajGostujucegPacijenta(Pacijent gostujuciPacijent)
        {
            PacijentRepo.Instance.Pacijenti.Add(gostujuciPacijent);
            PacijentRepo.Instance.Serijalizacija();
            PacijentRepo.Instance.Deserijalizacija();
        }
        public void UklanjanjeGostujucegNaloga(ListView gostujuciNalozi)
        {
            if (gostujuciNalozi.SelectedValue != null)
            {
                Pacijent pacijent = (Pacijent)gostujuciNalozi.SelectedValue;
                PacijentRepo pacijenti = PacijentRepo.Instance;
                KorisnikRepo korisnici = KorisnikRepo.Instance;
                foreach (Pacijent p in pacijenti.Pacijenti)
                {
                    if (p.Jmbg.Equals(pacijent.Jmbg))
                    {
                        pacijenti.Pacijenti.Remove(p);
                        PacijentRepo.Instance.Serijalizacija();
                        PacijentRepo.Instance.Deserijalizacija();
                        break;
                    }
                }
                azurirajPrikazListeGostujucihNaloga(gostujuciNalozi);
            }
        }


        private static void azurirajPrikazListeGostujucihNaloga(ListView gostujuciNalozi)
        {
            ObservableCollection<Pacijent> gostujuciPacijenti = new ObservableCollection<Pacijent>();
            foreach (Pacijent gostujuciPacijent in PacijentRepo.Instance.Pacijenti)
            {
                if (gostujuciPacijent.Korisnik.KorisnickoIme == null)
                {
                    gostujuciPacijenti.Add(gostujuciPacijent);
                }
            }
            gostujuciNalozi.ItemsSource = gostujuciPacijenti.ToList();
        }


        public void PomeranjeVanrednogTermina(IzborTerminaZaNovoZakazivanje izborTerminaZaNovoZakazivanje, IzborTerminaZaPomeranje izborTerminaZaPomeranje)
        {
            if (izborTerminaZaNovoZakazivanje.ponudjeniTerminiZaNovoZakazivanje.SelectedIndex >= 0)
            {
                DateTime staroVreme = ((Termin)izborTerminaZaPomeranje.ponudjeniTerminiZaPomeranje.SelectedItem).Vreme;
                Termin noviTermin = (Termin)izborTerminaZaNovoZakazivanje.ponudjeniTerminiZaNovoZakazivanje.SelectedItem;
                noviTermin.Status = StatusTermina.pomeren;

                foreach (Termin stariTermin in TerminRepo.Instance.Termini.ToList())
                {
                    if (stariTermin.Vreme == staroVreme)
                    {
                        TerminRepo.Instance.Termini.Remove(stariTermin);
                        TerminRepo.Instance.Termini.Add(noviTermin);
                        TerminRepo.Instance.Serijalizacija();
                        TerminRepo.Instance.Deserijalizacija();
                        break;
                    }
                }

                foreach (Pacijent pacijent in PacijentRepo.Instance.Pacijenti.ToList())
                {
                    if (pacijent.Jmbg == noviTermin.PacijentJmbg)
                    {
                        foreach (Termin stariTermin in pacijent.zakazaniTermini)
                        {
                            if (stariTermin.Vreme == staroVreme)
                            {
                                pacijent.zakazaniTermini.Remove(stariTermin);
                                pacijent.zakazaniTermini.Add(noviTermin);
                                //izborTerminaZaNovoZakazivanje.terminiPacijenta.listaZakazanihTermina.ItemsSource = pacijent.zakazaniTermini;
                                PacijentRepo.Instance.Serijalizacija();
                                PacijentRepo.Instance.Deserijalizacija();
                                break;
                            }
                        }
                    }
                }

                foreach (Lekar lekar in LekarRepo.Instance.Lekari.ToList())
                {
                    if (lekar.Jmbg == noviTermin.LekarJmbg)
                    {
                        foreach (Termin stariTermin in lekar.ZauzetiTermini)
                        {
                            if (stariTermin.Vreme == staroVreme)
                            {
                                lekar.ZauzetiTermini.Remove(stariTermin);
                                lekar.ZauzetiTermini.Add(noviTermin);
                                LekarRepo.Instance.Serijalizacija();
                                LekarRepo.Instance.Deserijalizacija();
                                break;
                            }
                        }
                    }
                }
                izborTerminaZaNovoZakazivanje.Close();
            }
        }
    }
}
