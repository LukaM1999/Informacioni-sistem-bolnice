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

        public void ZakazivanjeHitnogTermina(ZakazivanjeHitnogTermina zakazivanjeHitnogTermina) {

            DateTime slobodanTermin = DateTime.Today;

            int sat = DateTime.Now.Hour;

            int minuta = DateTime.Now.Minute;

            if (minuta >= 30)
            {
                slobodanTermin = slobodanTermin.AddHours(sat + 1);
            }
            else
            {
                slobodanTermin = slobodanTermin.AddHours(sat);
                slobodanTermin = slobodanTermin.AddMinutes(30);
            }

            DateTime pomocni = slobodanTermin;

            Lekari.Instance.Deserijalizacija();

            foreach (Lekar lekar in Lekari.Instance.listaLekara)
            {
                Pacijent p = (Pacijent)zakazivanjeHitnogTermina.pacijenti.SelectedItem;
                if (lekar.specijalizacija == zakazivanjeHitnogTermina.specijalizacijeLekara.SelectedItem.ToString())
                {
                    for (int j = 0; j < 3; j++)
                    {
                        Termin t = new Termin(slobodanTermin, 30.0, TipTermina.pregled, StatusTermina.slobodan,
                                                       p.jmbg, lekar.jmbg, "P1", true);
                        slobodniTermini.Add(t);
                        //System.Diagnostics.Debug.WriteLine(t);
                        slobodanTermin = slobodanTermin.AddMinutes(30);
                    }
                    slobodanTermin = pomocni;

                    //System.Diagnostics.Debug.WriteLine(lekar)
                    //int broj = slobodniTermini.Count();
                    //System.Diagnostics.Debug.WriteLine("UKUPNO: " + broj + " za ljekara " + lekar.ime);
                    //Pacijenti.Instance.Deserijalizacija();
                    //ponudjeniiTermini.ItemsSource = Pacijenti.Instance.listaPacijenata;
                }
                foreach (Termin predlozenTermin in slobodniTermini.ToList())
                {
                    //System.Diagnostics.Debug.WriteLine("PREDLOZENI:" + predlozenTermin.vreme + "\n");
                    foreach (Termin postojeciTermin in lekar.zauzetiTermini)
                    {
                        if (predlozenTermin.vreme.Equals(postojeciTermin.vreme))
                        {
                            slobodniTermini.Remove(predlozenTermin);
                            break;
                        }

                        //System.Diagnostics.Debug.WriteLine("PREDLOZENI:" + predlozenTermin.vreme + "\n");
                        // System.Diagnostics.Debug.WriteLine("POSTOJECI:" + postojeciTermin.vreme + "\n");

                    }
                }

            }

            if(slobodniTermini.Count == 0)
            {
                MessageBox.Show("Svi termini kod lekara izabrane specijalizacije su zakazani u narednom periodu(1h i 30 min).\n NAPOMENA: Postoji mogucnost pomeranja termina koji nije urgentan!");
                return;
            }

            Termin najblizi = slobodniTermini.First();
            foreach (Termin t in slobodniTermini)
            {
                System.Diagnostics.Debug.WriteLine(t.ToString() + "\n");

                if (t.vreme < najblizi.vreme)
                {
                    najblizi = t;
                }
            }

            Termini.Instance.Deserijalizacija();
            najblizi.status = StatusTermina.zakazan;
            Termini.Instance.listaTermina.Add(najblizi);
            Termini.Instance.Serijalizacija();
            Termini.Instance.Deserijalizacija();
            foreach (Pacijent pacijent in Pacijenti.Instance.listaPacijenata)
            {
                if (pacijent.jmbg == najblizi.pacijentJMBG)
                {
                    pacijent.zakazaniTermini.Add(najblizi);
                    Pacijenti.Instance.Serijalizacija();
                    Pacijenti.Instance.Deserijalizacija();
                }
            }

            string l = najblizi.lekarJMBG;

            foreach (Lekar lekar in Lekari.Instance.listaLekara)
            {
                if (lekar.jmbg == l)
                {
                    lekar.zauzetiTermini.Add(najblizi);
                    Lekari.Instance.Serijalizacija();
                    Pacijenti.Instance.Deserijalizacija();
                }
            }

        }




        public void PomeranjeTermina(IzborTerminaZaPomeranjeZakazanog izborTerminaZaPomeranjeZakazanog, IzborTerminaZaPomeranje izborTerminaZaPomeranje)
        {
            if (izborTerminaZaPomeranjeZakazanog.ponudjeniTermini.SelectedIndex >= 0)
            {
                DateTime staroVreme = ((Termin)izborTerminaZaPomeranje.ponudjeniTerminiZaPomeranje.SelectedItem).vreme;
                Termin noviTermin = (Termin)izborTerminaZaPomeranjeZakazanog.ponudjeniTermini.SelectedItem;
                noviTermin.status = StatusTermina.pomeren;

                foreach (Termin stariTermin in Termini.Instance.listaTermina.ToList())
                {
                    if (stariTermin.vreme == staroVreme)
                    {
                        //Termini.Instance.listaTermina.Remove(stariTermin);
                        stariTermin.status = StatusTermina.zakazan;
                        stariTermin.Hitan = true;
                        Pacijent pacijent = (Pacijent)izborTerminaZaPomeranje.zakazivanjeHitnogTermina.pacijenti.SelectedItem;
                        stariTermin.pacijentJMBG = pacijent.jmbg;
                        Termini.Instance.listaTermina.Add(noviTermin);
                        Termini.Instance.Serijalizacija();
                        Termini.Instance.Deserijalizacija();
                        izborTerminaZaPomeranje.zakazivanjeHitnogTermina.upravljanjeUrgentnimSistemomProzor.
                            ListaTermina.ItemsSource = Termini.Instance.listaTermina;
                        
                        //izborTerminaZaPomeranje.ponudjeniTerminiZaPomeranje.ItemsSource = Termini.Instance.listaTermina; 
                        
                        izborTerminaZaPomeranje.Close();
                        izborTerminaZaPomeranjeZakazanog.Close();
                        
                        break;
                    }
                }

                foreach (Pacijent pacijent in Pacijenti.Instance.listaPacijenata.ToList())
                {
                    if (pacijent.jmbg == noviTermin.pacijentJMBG)
                    {
                        foreach (Termin stariTermin in pacijent.zakazaniTermini)
                        {
                            if (stariTermin.vreme == staroVreme)
                            {
                                //pacijent.zakazaniTermini.Remove(stariTermin);
                                stariTermin.status = StatusTermina.zakazan;
                                stariTermin.Hitan = true;
                                Pacijent p = (Pacijent)izborTerminaZaPomeranje.zakazivanjeHitnogTermina.pacijenti.SelectedItem;
                                stariTermin.pacijentJMBG = p.jmbg;
                                pacijent.zakazaniTermini.Add(noviTermin);
                                //izborTerminaZaPomeranje.ponudjeniTerminiZaPomeranje.ItemsSource = pacijent.zakazaniTermini;
                                Pacijenti.Instance.Serijalizacija();
                                Pacijenti.Instance.Deserijalizacija();
                                break;
                            }
                        }
                    }
                }

                foreach (Lekar lekar in Lekari.Instance.listaLekara.ToList())
                {
                    if (lekar.jmbg == noviTermin.lekarJMBG)
                    {
                        foreach (Termin stariTermin in lekar.zauzetiTermini)
                        {
                            if (stariTermin.vreme == staroVreme)
                            {
                                //lekar.zauzetiTermini.Remove(stariTermin);
                                stariTermin.status = StatusTermina.zakazan;
                                stariTermin.Hitan = true;
                                Pacijent pacijent = (Pacijent)izborTerminaZaPomeranje.zakazivanjeHitnogTermina.pacijenti.SelectedItem;
                                stariTermin.pacijentJMBG = pacijent.jmbg;
                                lekar.zauzetiTermini.Add(noviTermin);
                                Lekari.Instance.Serijalizacija();
                                Lekari.Instance.Deserijalizacija();
                                break;
                            }
                        }
                    }
                }
                izborTerminaZaPomeranjeZakazanog.Close();
            }

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
