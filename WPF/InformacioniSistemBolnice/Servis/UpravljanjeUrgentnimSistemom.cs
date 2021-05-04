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


namespace Servis
{
    class UpravljanjeUrgentnimSistemom
    {
        private static readonly Lazy<UpravljanjeUrgentnimSistemom>
           lazy =
           new Lazy<UpravljanjeUrgentnimSistemom>
               (() => new UpravljanjeUrgentnimSistemom());

        public static UpravljanjeUrgentnimSistemom Instance { get { return lazy.Value; } }

        private ObservableCollection<Termin> slobodniTermini = new ObservableCollection<Termin>();
        public HitnoZakazivanjeDto zakazivanjeHitnogTerminaDto;
        public TerminiLekaraZaPomeranjeDto terminiLekaraZaPomeranjeDto;
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
            hitan.status = StatusTermina.zakazan;
            Termini.Instance.listaTermina.Add(hitan);
            Termini.Instance.Serijalizacija();
            Termini.Instance.Deserijalizacija();
            SacuvajHitanTerminUListuZakazanihTerminaPacijenta(hitan);
            SacuvajHitanTerminUListuZakazanihTerminaLekara(hitan);
        }

        private static void SacuvajHitanTerminUListuZakazanihTerminaLekara(Termin hitan)
        {
            foreach (Lekar lekar in Lekari.Instance.listaLekara)
            {
                if (lekar.jmbg == hitan.lekarJMBG)
                {
                    lekar.zauzetiTermini.Add(hitan);
                    Lekari.Instance.Serijalizacija();
                    Lekari.Instance.Deserijalizacija();
                }
            }
        }
        private static void SacuvajHitanTerminUListuZakazanihTerminaPacijenta(Termin hitan)
        {
            foreach (Pacijent pacijent in Pacijenti.Instance.listaPacijenata)
            {
                if (pacijent.jmbg == hitan.pacijentJMBG)
                {
                    pacijent.zakazaniTermini.Add(hitan);
                    Pacijenti.Instance.Serijalizacija();
                    Pacijenti.Instance.Deserijalizacija();
                }
            }
        }

        private Termin UzmiNajbliziSlobodanTerminIzListeSlobodnihTermina()
        {
            Termin najbliziSlobodan = slobodniTermini.First();
            foreach (Termin t in slobodniTermini)
            {
                if (t.vreme < najbliziSlobodan.vreme)
                {
                    najbliziSlobodan = t;
                    break;
                }
            }
            return najbliziSlobodan;
        }


        private void GenerisanjeNajblizegSlobodnogTerminaZaOdredjenuSpecijalizaciju()
        {
            foreach (Lekar lekar in Lekari.Instance.listaLekara.ToList())
            {
                if (lekar.specijalizacija == zakazivanjeHitnogTerminaDto.specijalizacija)
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
                foreach (Termin postojeciTermin in lekar.zauzetiTermini)
                {
                    if (predlozenTermin.vreme == postojeciTermin.vreme)
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
                                      zakazivanjeHitnogTerminaDto.jmbgPacijenta, lekar.jmbg,
                                      zakazivanjeHitnogTerminaDto.idProstorije, true);
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
            DateTime staroVreme = ((Termin)terminiLekaraZaPomeranjeDto.terminZaPomeranje).vreme;
            Termin noviTermin = (Termin)terminiLekaraZaPomeranjeDto.noviTermin;
            PomeriTermin(staroVreme, noviTermin);
            SacuvajPromeneUTerminimaPacijenta(staroVreme, noviTermin);
            SacuvajPromeneUTerminimaLekara(staroVreme, noviTermin);

        }

        private void PomeriTermin(DateTime staroVreme, Termin noviTermin)
        {
            noviTermin.status = StatusTermina.pomeren;
            foreach (Termin stariTermin in Termini.Instance.listaTermina.ToList())
            {
                if (stariTermin.vreme == staroVreme && stariTermin.lekarJMBG == ((Termin)terminiLekaraZaPomeranjeDto.noviTermin).lekarJMBG)
                {
                    promeniParametreTermina(stariTermin);
                    Termini.Instance.listaTermina.Add(noviTermin);
                    Termini.Instance.Serijalizacija();
                    Termini.Instance.Deserijalizacija();

                    break;
                }
            }
        }

        private void SacuvajPromeneUTerminimaLekara(DateTime staroVreme, Termin noviTermin)
        {
            foreach (Lekar lekar in Lekari.Instance.listaLekara.ToList())
            {
                if (lekar.jmbg == noviTermin.lekarJMBG)
                {
                    foreach (Termin stariTermin in lekar.zauzetiTermini)
                    {
                        if (stariTermin.vreme == staroVreme)
                        {
                            promeniParametreTermina(stariTermin);
                            lekar.zauzetiTermini.Add(noviTermin);
                            Lekari.Instance.Serijalizacija();
                            Lekari.Instance.Deserijalizacija();
                            break;
                        }
                    }
                }
            }
        }

        private void SacuvajPromeneUTerminimaPacijenta(DateTime staroVreme, Termin noviTermin)
        {
            foreach (Pacijent pacijent in Pacijenti.Instance.listaPacijenata.ToList())
            {
                if (pacijent.jmbg == noviTermin.pacijentJMBG)
                {
                    foreach (Termin stariTermin in pacijent.zakazaniTermini)
                    {
                        if (stariTermin.vreme == staroVreme)
                        {
                            promeniParametreTermina(stariTermin);
                            pacijent.zakazaniTermini.Add(noviTermin);
                            Pacijenti.Instance.Serijalizacija();
                            Pacijenti.Instance.Deserijalizacija();
                            break;
                        }
                    }
                }
            }
        }

        private void promeniParametreTermina(Termin stariTermin)
        {
            stariTermin.status = StatusTermina.zakazan;
            stariTermin.Hitan = true;
            stariTermin.pacijentJMBG = terminiLekaraZaPomeranjeDto.jmbgPacijenta;
        }

        public void KreiranjeGostujcegPacijenta(KreiranjeGostujucegPacijentaProzor kreiranjeGostujucegPacijentaProzor)
        {
            DateTime datum = DateTime.Parse(kreiranjeGostujucegPacijentaProzor.datumUnos.Text);
            ObservableCollection<Termin> zakazaniTermini = new ObservableCollection<Termin>();
            Korisnik korisnik = new Korisnik(null, null, (Model.UlogaKorisnika)Enum.Parse(typeof(Model.UlogaKorisnika), "pacijent"));
            Pacijent p = new Pacijent(new Osoba(kreiranjeGostujucegPacijentaProzor.imeUnos.Text, kreiranjeGostujucegPacijentaProzor.prezimeUnos.Text, kreiranjeGostujucegPacijentaProzor.JMBGUnos.Text,
                datum, kreiranjeGostujucegPacijentaProzor.telUnos.Text, kreiranjeGostujucegPacijentaProzor.mailUnos.Text, korisnik, null));
            p.zakazaniTermini = zakazaniTermini;
            Pacijenti.Instance.listaPacijenata.Add(p);
            Korisnici.Instance.listaKorisnika.Add(korisnik);
            Korisnici.Instance.Serijalizacija();
            Korisnici.Instance.Deserijalizacija();
            Pacijenti.Instance.Serijalizacija();
            Pacijenti.Instance.Deserijalizacija();

        }





    }




}
