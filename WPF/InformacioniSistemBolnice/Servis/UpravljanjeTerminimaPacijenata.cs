using System;
using Model;
using Repozitorijum;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using InformacioniSistemBolnice;
using System.Linq;

namespace Servis
{
    public class UpravljanjeTerminimaPacijenata
    {
        private static readonly Lazy<UpravljanjeTerminimaPacijenata>
           Lazy = new(() => new UpravljanjeTerminimaPacijenata());
        public static UpravljanjeTerminimaPacijenata Instance => Lazy.Value;

        public void Zakazivanje(Termin terminZaZakazivanje)
        {
            Pacijent ulogovanPacijent = Pacijenti.Instance.NadjiPoJmbg(terminZaZakazivanje.pacijentJMBG);
            foreach (Termin vecZakazan in ulogovanPacijent.zakazaniTermini)
                if (vecZakazan.vreme == terminZaZakazivanje.vreme) return;
            Lekar izabranLekar = Lekari.Instance.NadjiPoJmbg(terminZaZakazivanje.lekarJMBG);
            Prostorija nadjenaProstorija = Prostorije.Instance.NadjiPoId(terminZaZakazivanje.idProstorije);
            terminZaZakazivanje.status = StatusTermina.zakazan;
            ulogovanPacijent.DodajTermin(terminZaZakazivanje);
            izabranLekar.zauzetiTermini.Add(terminZaZakazivanje);
            nadjenaProstorija.termin.Add(terminZaZakazivanje);
            Termini.Instance.listaTermina.Add(terminZaZakazivanje);
            Lekari.Instance.Serijalizacija();
            Pacijenti.Instance.Serijalizacija();
            Termini.Instance.Serijalizacija();
            Prostorije.Instance.Serijalizacija();
        }

        public void Otkazivanje(Termin terminZaOtkazivanje)
        {
            Pacijent ulogovanPacijent = Pacijenti.Instance.NadjiPoJmbg(terminZaOtkazivanje.pacijentJMBG);
            ulogovanPacijent.ObrisiTermin(Termini.Instance.NadjiPoVremenu(terminZaOtkazivanje.vreme));
            Pacijenti.Instance.Serijalizacija();

            foreach (Lekar lekar in Lekari.Instance.listaLekara.ToList())
            {
                if (lekar.jmbg != terminZaOtkazivanje.lekarJMBG) continue;
                foreach (Termin termin in lekar.zauzetiTermini.ToList())
                {
                    if (termin.vreme != terminZaOtkazivanje.vreme) continue;
                    lekar.zauzetiTermini.Remove(termin);
                    Lekari.Instance.Serijalizacija();
                    break;
                }
            }

            foreach (Termin termin in Termini.Instance.listaTermina.ToList())
            {
                if (termin.vreme != terminZaOtkazivanje.vreme) continue;
                Termini.Instance.listaTermina.Remove(termin);
                Termini.Instance.Serijalizacija();
                break;
            }

            foreach (Prostorija prostorija in Prostorije.Instance.listaProstorija.ToList())
            {
                if (prostorija.id != terminZaOtkazivanje.idProstorije) continue;
                prostorija.termin.Remove(terminZaOtkazivanje);
                Prostorije.Instance.Serijalizacija();
                break;
            }
        }

        public void Pomeranje(Termin terminZaPomeranje, Termin noviTermin)
        {
            noviTermin.status = StatusTermina.pomeren;
            foreach (Termin stariTermin in Termini.Instance.listaTermina.ToList())
            {
                if (stariTermin.vreme != terminZaPomeranje.vreme) continue;
                Termini.Instance.listaTermina.Remove(stariTermin);
                Termini.Instance.listaTermina.Add(noviTermin);
                Termini.Instance.Serijalizacija();
                break;
            }

            foreach (Pacijent pacijent in Pacijenti.Instance.ListaPacijenata.ToList())
            {
                if (pacijent.jmbg != noviTermin.pacijentJMBG) continue;
                foreach (Termin stariTermin in pacijent.zakazaniTermini)
                {
                    if (stariTermin.vreme != terminZaPomeranje.vreme) continue;
                    pacijent.ObrisiTermin(stariTermin);
                    pacijent.DodajTermin(noviTermin);
                    Pacijenti.Instance.Serijalizacija();
                    break;
                }
            }

            foreach (Lekar lekar in Lekari.Instance.listaLekara.ToList())
            {
                if (lekar.jmbg != noviTermin.lekarJMBG) continue;
                foreach (Termin stariTermin in lekar.zauzetiTermini)
                {
                    if (stariTermin.vreme != terminZaPomeranje.vreme) continue;
                    lekar.zauzetiTermini.Remove(stariTermin);
                    lekar.zauzetiTermini.Add(noviTermin);
                    Lekari.Instance.Serijalizacija();
                    break;
                }
            }

            foreach (Prostorija prostorija in Prostorije.Instance.listaProstorija.ToList())
            {
                if (prostorija.id != noviTermin.idProstorije) continue;
                foreach (Termin stariTermin in prostorija.termin)
                {
                    if (stariTermin.vreme != terminZaPomeranje.vreme) continue;
                    prostorija.termin.Remove(stariTermin);
                    prostorija.termin.Add(noviTermin);
                    Prostorije.Instance.Serijalizacija();
                    break;
                }
            }
        }

        public void Uvid(DataGrid listaZakazanihTermina)
        {
            new TerminInfoProzor(listaZakazanihTermina).Show();
        }

    }
}