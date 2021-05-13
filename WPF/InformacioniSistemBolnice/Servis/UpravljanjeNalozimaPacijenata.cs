using System;
using Model;
using Repozitorijum;
using System.Windows.Controls;
using InformacioniSistemBolnice;
using System.Collections.ObjectModel;
using System.Linq;

namespace Servis
{
    public class UpravljanjeNalozimaPacijenata
    {
        private static readonly Lazy<UpravljanjeNalozimaPacijenata>
          lazy =
          new Lazy<UpravljanjeNalozimaPacijenata>
              (() => new UpravljanjeNalozimaPacijenata());

        public static UpravljanjeNalozimaPacijenata Instance { get { return lazy.Value; } }

        public void KreiranjeNaloga(RegistracijaPacijentaDto registracija)
        {
            ObservableCollection<Termin> zakazaniTermini = new ObservableCollection<Termin>();
            DateTime datum = DateTime.Parse(registracija.datumRodjenja.ToString());
            Korisnik korisnik = new Korisnik(registracija.korisnickoIme, registracija.lozinka, (Model.UlogaKorisnika)Enum.Parse(typeof(Model.UlogaKorisnika), "pacijent"));
            Adresa adresa = new Adresa(registracija.drzava, registracija.grad, registracija.ulica, registracija.broj);
            Pacijent p = new Pacijent(new Osoba(registracija.ime, registracija.prezime, registracija.jmbg, datum, registracija.telefon, registracija.email, korisnik, adresa));
            p.zakazaniTermini = zakazaniTermini;
            Pacijenti.Instance.ListaPacijenata.Add(p);
            Korisnici.Instance.listaKorisnika.Add(korisnik);
            Korisnici.Instance.Serijalizacija();
            Pacijenti.Instance.Serijalizacija();

        }

        public void UklanjanjeNaloga(ListView ListaPacijenata)
        {
            if (ListaPacijenata.SelectedValue != null)
            {
                Pacijent pacijent = (Pacijent)ListaPacijenata.SelectedValue;
                Pacijenti pacijenti = Pacijenti.Instance;
                Korisnici korisnici = Korisnici.Instance;
                Kartoni kartoni = Kartoni.Instance;
                foreach (Pacijent p in pacijenti.ListaPacijenata)
                {
                    if (p.jmbg.Equals(pacijent.jmbg))
                    {
                        pacijenti.ListaPacijenata.Remove(p);
                        korisnici.listaKorisnika.Remove(p.korisnik);
                        kartoni.listaKartona.Remove(p.zdravstveniKarton);


                        Pacijenti.Instance.Serijalizacija();
                        Korisnici.Instance.Serijalizacija(); Pacijenti.Instance.Serijalizacija();
                        Kartoni.Instance.Serijalizacija();
                        Pacijenti.Instance.Deserijalizacija();
                        Korisnici.Instance.Deserijalizacija();
                        Kartoni.Instance.Deserijalizacija();
                        //ListaPacijenata.ItemsSource = Pacijenti.Instance.ListaPacijenata;


                        break;
                    }
                }
            }
        }

        public void UklanjanjeGostujucegNaloga(ListView gostujuciNalozi)
        {
            if (gostujuciNalozi.SelectedValue != null)
            {
                Pacijent pacijent = (Pacijent)gostujuciNalozi.SelectedValue;
                Pacijenti pacijenti = Pacijenti.Instance;
                Korisnici korisnici = Korisnici.Instance;
                foreach (Pacijent p in pacijenti.ListaPacijenata)
                {
                    if (p.jmbg.Equals(pacijent.jmbg))
                    {
                        pacijenti.ListaPacijenata.Remove(p);
                        Pacijenti.Instance.Serijalizacija();
                        Pacijenti.Instance.Deserijalizacija();
                        break;
                    }
                }
                azurirajPrikazListeGostujucihNaloga(gostujuciNalozi);
            }
        }

        private static void azurirajPrikazListeGostujucihNaloga(ListView gostujuciNalozi)
        {
            ObservableCollection<Pacijent> gostujuciPacijenti = new ObservableCollection<Pacijent>();
            foreach (Pacijent gostujuciPacijent in Pacijenti.Instance.ListaPacijenata)
            {
                if (gostujuciPacijent.korisnik.korisnickoIme == null)
                {
                    gostujuciPacijenti.Add(gostujuciPacijent);
                }
            }
            gostujuciNalozi.ItemsSource = gostujuciPacijenti.ToList();
        }

        public void IzmenaNaloga(IzmenaNalogaPacijentaForma izmena, ListView ListaPacijenata)
        {
            if (ListaPacijenata.SelectedValue != null)
            {
                Pacijent p = (Pacijent)ListaPacijenata.SelectedItem;
                p.ime = izmena.imeUnos.Text;
                p.prezime = izmena.prezimeUnos.Text;
                p.jmbg = izmena.JMBGUnos.Text;
                p.adresa.Drzava = izmena.drzavaUnos.Text;
                p.adresa.Grad = izmena.gradUnos.Text;
                p.adresa.Ulica = izmena.ulicaUnos.Text;
                p.adresa.Broj = izmena.brojUnos.Text;
                p.datumRodjenja = DateTime.Parse(izmena.datumUnos.Text);
                p.telefon = izmena.telUnos.Text;
                p.email = izmena.mailUnos.Text;
                p.korisnik.korisnickoIme = izmena.korisnikUnos.Text;
                p.korisnik.lozinka = izmena.lozinkaUnos.Password;
                Korisnik korisnik = p.korisnik;
                Adresa adresa = p.adresa;
                Korisnici.Instance.Serijalizacija();
                Pacijenti.Instance.Serijalizacija();
                Pacijenti.Instance.Deserijalizacija();
            }
        }


        public void IzmjenaZdravstvenogKartona(IzmjenaZdravstvenogKartonaForma izmjenaZdravstvenogKartonaForma, ListView ListaPacijenata)
        {
            if (ListaPacijenata.SelectedValue != null)
            {
                Pacijent p = (Pacijent)ListaPacijenata.SelectedItem;
                p.zdravstveniKarton.BrojKartona = izmjenaZdravstvenogKartonaForma.brojKartona.Text;
                p.zdravstveniKarton.BrojKnjizice = izmjenaZdravstvenogKartonaForma.brojKnjizice.Text;
                p.zdravstveniKarton.Jmbg = izmjenaZdravstvenogKartonaForma.ToString();
                p.zdravstveniKarton.ImeJednogRoditelja = izmjenaZdravstvenogKartonaForma.imeRoditelja.Text;
                p.zdravstveniKarton.LiceZaZdravstvenuZastitu = izmjenaZdravstvenogKartonaForma.liceZdrZastita.Text;
                p.zdravstveniKarton.BracnoStanje = (Model.BracnoStanje)Enum.Parse(typeof(Model.BracnoStanje), izmjenaZdravstvenogKartonaForma.bracnoStanjeUnos.Text);
                p.zdravstveniKarton.PolPacijenta = (Model.Pol)Enum.Parse(typeof(Model.Pol), izmjenaZdravstvenogKartonaForma.polUnos.Text);
                p.zdravstveniKarton.KategorijaZdravstveneZastite = (Model.KategorijaZdravstveneZastite)Enum.Parse(typeof(Model.KategorijaZdravstveneZastite), izmjenaZdravstvenogKartonaForma.kategorijaZdrZastiteUnos.Text);
                p.zdravstveniKarton.PodaciOZaposlenjuIZanimanjuPacijenta.PosaoKojiObavlja = izmjenaZdravstvenogKartonaForma.posaoUnos.Text;
                p.zdravstveniKarton.PodaciOZaposlenjuIZanimanjuPacijenta.MestoRada = izmjenaZdravstvenogKartonaForma.radnoMjestoUnos.Text;
                p.zdravstveniKarton.PodaciOZaposlenjuIZanimanjuPacijenta.RegBroj = izmjenaZdravstvenogKartonaForma.registarskiBrojUnos.Text;
                p.zdravstveniKarton.PodaciOZaposlenjuIZanimanjuPacijenta.RadPodPosebnimUslovima = izmjenaZdravstvenogKartonaForma.radUPosebnimUslovimaUnos.Text;
                p.zdravstveniKarton.PodaciOZaposlenjuIZanimanjuPacijenta.Promene = izmjenaZdravstvenogKartonaForma.promjene.Text;
                p.zdravstveniKarton.PodaciOZaposlenjuIZanimanjuPacijenta.OSIZZdrZastite = izmjenaZdravstvenogKartonaForma.OSIZ.Text;

                Kartoni zdravstveniKartoni = Kartoni.Instance;
                foreach (ZdravstveniKarton zdravstveniKarton in zdravstveniKartoni.listaKartona)
                {
                    if (p.jmbg.Equals(zdravstveniKarton.Jmbg))
                    {
                        zdravstveniKarton.Equals(p.zdravstveniKarton);
                        Kartoni.Instance.Serijalizacija();

                    }
                }
                Pacijenti.Instance.Serijalizacija();
                Pacijenti.Instance.Deserijalizacija();
            }
        }



        public void KreiranjeZdravstvenogKarton(ZdravstveniKartonForma zdravstveniKartonForma)
        {
            PodaciOZaposlenjuIZanimanju podaciOZaposlenjuIZanimanju = new PodaciOZaposlenjuIZanimanju(zdravstveniKartonForma.radnoMjestoUnos.Text, zdravstveniKartonForma.registarskiBrojUnos.Text,
                zdravstveniKartonForma.sifraDjelatnostiUnos.Text, zdravstveniKartonForma.posaoUnos.Text, zdravstveniKartonForma.OSIZ.Text,
                zdravstveniKartonForma.radnoMjestoUnos.Text, zdravstveniKartonForma.promjene.Text);

            ZdravstveniKarton zdravstveniKarton = new ZdravstveniKarton(zdravstveniKartonForma.brojKartonaUnos.Text, zdravstveniKartonForma.brojKnjiziceUnos.Text, zdravstveniKartonForma.JMBGUnos.Text,
                zdravstveniKartonForma.imeRoditeljaUnos.Text, zdravstveniKartonForma.liceZdrZastitaUnos.Text,
                 (Model.Pol)Enum.Parse(typeof(Model.Pol), zdravstveniKartonForma.polUnos.Text), (Model.BracnoStanje)Enum.Parse(typeof(Model.BracnoStanje), zdravstveniKartonForma.bracnoStanjeUnos.Text),
                 (Model.KategorijaZdravstveneZastite)Enum.Parse(typeof(Model.KategorijaZdravstveneZastite), zdravstveniKartonForma.kategorijaZdrZastiteUnos.Text), podaciOZaposlenjuIZanimanju);

            ListView listaAlergena = zdravstveniKartonForma.ListaAlergena;
            ObservableCollection<Alergen> alergeni = new ObservableCollection<Alergen>();
            // for (int i = 0; i < listaAlergena.SelectedItems.Count; i++)
            //{
            Alergen alergen = (Alergen)listaAlergena.SelectedItem;
            alergeni.Add(alergen);
            //}

            zdravstveniKarton.Alergeni = alergeni;
            Kartoni.Instance.listaKartona.Add(zdravstveniKarton);
            Kartoni.Instance.Serijalizacija();
        }


        public void DodjelaZdravstvenogKartonaPacijentu(IzmenaNalogaPacijentaForma izmenaNalogaPacijentaForma)
        {
            Kartoni zdravstveniKartoni = Kartoni.Instance;
            Pacijenti pacijenti = Pacijenti.Instance;
            //string jmbg = NadjiPacijenta(pregledNalogaPacijenta);
            foreach (ZdravstveniKarton zdravstveniKarton in zdravstveniKartoni.listaKartona)
            {
                foreach (Pacijent pacijent in pacijenti.ListaPacijenata)
                {
                    if (pacijent.jmbg.Equals(zdravstveniKarton.Jmbg))
                    {
                        pacijent.zdravstveniKarton = zdravstveniKarton;

                        Pacijenti.Instance.Serijalizacija();
                        // Pacijenti.Instance.Deserijalizacija();



                    }
                }

            }
        }



        public void DodavanjeAlergenaPacijentu(DodajAlergenPacijentu dodajAlergenPacijentu, IzmjenaZdravstvenogKartonaForma izmjenaZdravstvenogKartonaForma)
        {
            Alergen a = (Alergen)dodajAlergenPacijentu.ListaAlergena.SelectedItem;

            foreach (Pacijent p in Pacijenti.Instance.ListaPacijenata)
            {
                if (p.jmbg.Equals(izmjenaZdravstvenogKartonaForma.JMBGLabela.Content))
                {
                    foreach (Alergen alergen in Alergeni.Instance.listaAlergena)
                    {

                        if (a.nazivAlergena == alergen.nazivAlergena)
                        {
                            System.Diagnostics.Debug.WriteLine(p.jmbg);
                            p.zdravstveniKarton.AddAlergen(alergen);
                            Pacijenti.Instance.Serijalizacija();
                            Pacijenti.Instance.Deserijalizacija();

                            izmjenaZdravstvenogKartonaForma.ListaAlergena.ItemsSource = p.zdravstveniKarton.Alergeni;
                            break;
                        }

                    }

                }
            }
        }




        public void BrisanjeAlergenaPacijentu(IzmjenaZdravstvenogKartonaForma izmjenaZdravstvenogKartonaForma)
        {
            if (izmjenaZdravstvenogKartonaForma.ListaAlergena.SelectedItem != null)
            {
                Alergen a = (Alergen)izmjenaZdravstvenogKartonaForma.ListaAlergena.SelectedItem;
                System.Diagnostics.Debug.WriteLine(a);
                foreach (Pacijent p in Pacijenti.Instance.ListaPacijenata)
                {
                    if (p.jmbg.Equals(izmjenaZdravstvenogKartonaForma.JMBGLabela.Content))
                    {
                        System.Diagnostics.Debug.WriteLine(p.jmbg);
                        System.Diagnostics.Debug.WriteLine(izmjenaZdravstvenogKartonaForma.JMBGLabela.Content);
                        foreach (Alergen alergen in p.zdravstveniKarton.Alergeni)
                        {
                            if (a.nazivAlergena == alergen.nazivAlergena)
                            {
                                System.Diagnostics.Debug.WriteLine(p.jmbg);
                                p.zdravstveniKarton.Alergeni.Remove(alergen);
                                Pacijenti.Instance.Serijalizacija();
                                Pacijenti.Instance.Deserijalizacija();

                                izmjenaZdravstvenogKartonaForma.ListaAlergena.ItemsSource = p.zdravstveniKarton.Alergeni;
                                break;
                            }
                        }
                    }
                }

            }

        }


        public void ZakazivanjeTermina(IzborTerminaPacijenta izborTermina, ZakazivanjeTerminaSekretara zakazivanjeTerminaSekretara)
        {
            foreach (Pacijent pacijent in Pacijenti.Instance.ListaPacijenata)
            {
                Pacijent p = (Pacijent)zakazivanjeTerminaSekretara.pacijenti.SelectedItem;
                if (pacijent.jmbg == p.jmbg)
                {
                    foreach (Termin vecZakazan in pacijent.zakazaniTermini)
                    {
                        if (vecZakazan == (Termin)izborTermina.ponudjeniTermini.SelectedItem)
                        {
                            return;
                        }
                    }
                    foreach (Lekar lekar in Lekari.Instance.listaLekara)
                    {
                        if (lekar.jmbg == ((Termin)izborTermina.ponudjeniTermini.SelectedItem).lekarJMBG)
                        {
                            Termin t = (Termin)izborTermina.ponudjeniTermini.SelectedItem;
                            t.status = StatusTermina.zakazan;
                            pacijent.zakazaniTermini.Add((Termin)izborTermina.ponudjeniTermini.SelectedItem);
                            lekar.zauzetiTermini.Add((Termin)izborTermina.ponudjeniTermini.SelectedItem);
                            Termini.Instance.listaTermina.Add((Termin)izborTermina.ponudjeniTermini.SelectedItem);
                            Lekari.Instance.Serijalizacija();
                            Pacijenti.Instance.Serijalizacija();
                            Termini.Instance.Serijalizacija();
                            Pacijenti.Instance.Deserijalizacija();
                            Lekari.Instance.Deserijalizacija();
                            Termini.Instance.Deserijalizacija();
                            zakazivanjeTerminaSekretara.terminiPacijentaProzorSekretara.listaZakazanihTermina.ItemsSource
                                = Termini.Instance.listaTermina;

                            izborTermina.zakazivanjeTerminaPacijenta.Close();
                            izborTermina.Close();
                            break;
                        }
                    }
                }
            }
        }



        public void Pomeranje(PomjeranjeTerminaProzorSekretara pomeranje)
        {
            if (pomeranje.ponudjeniTermini.SelectedIndex >= 0)
            {
                DateTime staroVreme = ((Termin)pomeranje.terminiPacijenta.listaZakazanihTermina.SelectedItem).vreme;
                Termin noviTermin = (Termin)pomeranje.ponudjeniTermini.SelectedItem;
                noviTermin.status = StatusTermina.pomeren;

                foreach (Termin stariTermin in Termini.Instance.listaTermina.ToList())
                {
                    if (stariTermin.vreme == staroVreme)
                    {
                        Termini.Instance.listaTermina.Remove(stariTermin);
                        Termini.Instance.listaTermina.Add(noviTermin);
                        Termini.Instance.Serijalizacija();
                        Termini.Instance.Deserijalizacija();
                        break;
                    }
                }

                foreach (Pacijent pacijent in Pacijenti.Instance.ListaPacijenata.ToList())
                {
                    if (pacijent.jmbg == noviTermin.pacijentJMBG)
                    {
                        foreach (Termin stariTermin in pacijent.zakazaniTermini)
                        {
                            if (stariTermin.vreme == staroVreme)
                            {
                                pacijent.zakazaniTermini.Remove(stariTermin);
                                pacijent.zakazaniTermini.Add(noviTermin);
                                pomeranje.terminiPacijenta.listaZakazanihTermina.ItemsSource = pacijent.zakazaniTermini;
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
                                lekar.zauzetiTermini.Remove(stariTermin);
                                lekar.zauzetiTermini.Add(noviTermin);
                                Lekari.Instance.Serijalizacija();
                                Lekari.Instance.Deserijalizacija();
                                break;
                            }
                        }
                    }
                }
                pomeranje.Close();
            }
        }


        public void Otkazivanje(DataGrid listaZakazanihTermina)
        {
            if (listaZakazanihTermina.SelectedIndex >= 0)
            {
                Termin t = (Termin)listaZakazanihTermina.SelectedItem;

                foreach (Pacijent pacijent in Pacijenti.Instance.ListaPacijenata.ToList())
                {
                    if (pacijent.jmbg == t.pacijentJMBG)
                    {
                        foreach (Termin termin in pacijent.zakazaniTermini)
                        {
                            if (termin.vreme == t.vreme)
                            {
                                pacijent.zakazaniTermini.Remove(termin);
                                Pacijenti.Instance.Serijalizacija();
                                Pacijenti.Instance.Deserijalizacija();
                                break;
                            }
                        }

                    }
                }

                foreach (Lekar lekar in Lekari.Instance.listaLekara.ToList())
                {
                    if (lekar.jmbg == t.lekarJMBG)
                    {
                        foreach (Termin termin in lekar.zauzetiTermini.ToList())
                        {
                            if (termin.vreme == t.vreme)
                            {
                                lekar.zauzetiTermini.Remove(termin);
                                Lekari.Instance.Serijalizacija();
                                Lekari.Instance.Deserijalizacija();
                                break;
                            }
                        }
                    }
                }

                foreach (Termin termin in Termini.Instance.listaTermina.ToList())
                {
                    if (termin.vreme == t.vreme)
                    {
                        Termini.Instance.listaTermina.Remove(termin);
                        Termini.Instance.Serijalizacija();
                        Termini.Instance.Deserijalizacija();
                        listaZakazanihTermina.ItemsSource = null;
                        listaZakazanihTermina.ItemsSource = Termini.Instance.listaTermina;
                    }
                }
            }
        }

        public void PomeranjeVanrednogTermina(IzborTerminaZaNovoZakazivanje izborTerminaZaNovoZakazivanje, IzborTerminaZaPomeranje izborTerminaZaPomeranje)
        {
            if (izborTerminaZaNovoZakazivanje.ponudjeniTerminiZaNovoZakazivanje.SelectedIndex >= 0)
            {
                DateTime staroVreme = ((Termin)izborTerminaZaPomeranje.ponudjeniTerminiZaPomeranje.SelectedItem).vreme;
                Termin noviTermin = (Termin)izborTerminaZaNovoZakazivanje.ponudjeniTerminiZaNovoZakazivanje.SelectedItem;
                noviTermin.status = StatusTermina.pomeren;

                foreach (Termin stariTermin in Termini.Instance.listaTermina.ToList())
                {
                    if (stariTermin.vreme == staroVreme)
                    {
                        Termini.Instance.listaTermina.Remove(stariTermin);
                        Termini.Instance.listaTermina.Add(noviTermin);
                        Termini.Instance.Serijalizacija();
                        Termini.Instance.Deserijalizacija();
                        break;
                    }
                }

                foreach (Pacijent pacijent in Pacijenti.Instance.ListaPacijenata.ToList())
                {
                    if (pacijent.jmbg == noviTermin.pacijentJMBG)
                    {
                        foreach (Termin stariTermin in pacijent.zakazaniTermini)
                        {
                            if (stariTermin.vreme == staroVreme)
                            {
                                pacijent.zakazaniTermini.Remove(stariTermin);
                                pacijent.zakazaniTermini.Add(noviTermin);
                                //izborTerminaZaNovoZakazivanje.terminiPacijenta.listaZakazanihTermina.ItemsSource = pacijent.zakazaniTermini;
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
                                lekar.zauzetiTermini.Remove(stariTermin);
                                lekar.zauzetiTermini.Add(noviTermin);
                                Lekari.Instance.Serijalizacija();
                                Lekari.Instance.Deserijalizacija();
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



