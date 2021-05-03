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

        public void KreiranjeNaloga(RegistracijaPacijentaForma registracija)
        {
            ObservableCollection<Termin> zakazaniTermini = new ObservableCollection<Termin>();
            DateTime datum = DateTime.Parse(registracija.datumUnos.Text);
            Korisnik korisnik = new Korisnik(registracija.korisnikUnos.Text, registracija.lozinkaUnos.Password, (Model.UlogaKorisnika)Enum.Parse(typeof(Model.UlogaKorisnika), "pacijent"));
            Adresa adresa = new Adresa(registracija.drzavaUnos.Text, registracija.gradUnos.Text, registracija.ulicaUnos.Text, registracija.brojUnos.Text);
            Pacijent p = new Pacijent(new Osoba(registracija.imeUnos.Text, registracija.prezimeUnos.Text, registracija.JMBGUnos.Text, datum, registracija.telUnos.Text, registracija.mailUnos.Text, korisnik, adresa));
            p.zakazaniTermini = zakazaniTermini;
            Pacijenti.Instance.listaPacijenata.Add(p);
            Korisnici.Instance.listaKorisnika.Add(korisnik);
            Adrese.Instance.listaAdresa.Add(adresa);
            Adrese.Instance.Serijalizacija();
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
                foreach (Pacijent p in pacijenti.listaPacijenata)
                {
                    if (p.jmbg.Equals(pacijent.jmbg))
                    {
                        pacijenti.listaPacijenata.Remove(p);
                        korisnici.listaKorisnika.Remove(p.korisnik);
                        kartoni.listaKartona.Remove(p.zdravstveniKarton);


                        Pacijenti.Instance.Serijalizacija();
                        Korisnici.Instance.Serijalizacija(); Pacijenti.Instance.Serijalizacija();
                        Kartoni.Instance.Serijalizacija();
                        Pacijenti.Instance.Deserijalizacija();
                        Korisnici.Instance.Deserijalizacija();
                        Kartoni.Instance.Deserijalizacija();
                        ListaPacijenata.ItemsSource = Pacijenti.Instance.listaPacijenata;


                        break;
                    }
                }
            }
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
                Adrese.Instance.Serijalizacija();
                Korisnici.Instance.Serijalizacija();
                Pacijenti.Instance.Serijalizacija();
                Pacijenti.Instance.Deserijalizacija();
                ListaPacijenata.ItemsSource = Pacijenti.Instance.listaPacijenata;


            }
        }


        public void IzmjenaZdravstvenogKartona(IzmjenaZdravstvenogKartonaForma izmjenaZdravstvenogKartonaForma, ListView ListaPacijenata)
        {
            if (ListaPacijenata.SelectedValue != null)
            {
                Pacijent p = (Pacijent)ListaPacijenata.SelectedItem;
                p.zdravstveniKarton.BrojKartona = izmjenaZdravstvenogKartonaForma.brojKartonaUnos.Text;
                p.zdravstveniKarton.BrojKnjizice = izmjenaZdravstvenogKartonaForma.brojKnjiziceUnos.Text;
                p.zdravstveniKarton.Jmbg = izmjenaZdravstvenogKartonaForma.JMBGLabela.ToString();
                p.zdravstveniKarton.ImeJednogRoditelja = izmjenaZdravstvenogKartonaForma.imeRoditeljaUnos.Text;
                p.zdravstveniKarton.LiceZaZdravstvenuZastitu = izmjenaZdravstvenogKartonaForma.liceZdrZastitaUnos.Text;
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
            BazaPodatakaOZaposlenjuiZanimanjuPacijenata.Instance.bazaPodatakaOZaposlenjuIZanimanjuPacijenata.Add(podaciOZaposlenjuIZanimanju);
            BazaPodatakaOZaposlenjuiZanimanjuPacijenata.Instance.Serijalizacija();
            Kartoni.Instance.listaKartona.Add(zdravstveniKarton);
            Kartoni.Instance.Serijalizacija();
        }

        public void PregledZdravstvenogKartona(PregledNalogaPacijenta pregledNalogaPacijenta)
        {
            Kartoni zdravstveniKartoni = Kartoni.Instance;
            string jmbg = NadjiPacijenta(pregledNalogaPacijenta);
            foreach (ZdravstveniKarton zdravstveniKarton in zdravstveniKartoni.listaKartona)
            {

                if (jmbg.Equals(zdravstveniKarton.Jmbg))
                {
                    ZdravstveniKartonForma zdravstveniKartonForma = new ZdravstveniKartonForma();
                    zdravstveniKartonForma.brojKartonaUnos.Text = zdravstveniKarton.BrojKartona;
                    zdravstveniKartonForma.brojKnjiziceUnos.Text = zdravstveniKarton.BrojKnjizice;
                    zdravstveniKartonForma.imeRoditeljaUnos.Text = zdravstveniKarton.ImeJednogRoditelja;
                    zdravstveniKartonForma.liceZdrZastitaUnos.Text = zdravstveniKarton.LiceZaZdravstvenuZastitu;
                    zdravstveniKartonForma.polUnos.Text = zdravstveniKarton.PolPacijenta.ToString();
                    zdravstveniKartonForma.bracnoStanjeUnos.Text = zdravstveniKarton.BracnoStanje.ToString();
                    zdravstveniKartonForma.kategorijaZdrZastiteUnos.Text = zdravstveniKarton.KategorijaZdravstveneZastite.ToString();
                    zdravstveniKartonForma.radnoMjestoUnos.Text = zdravstveniKarton.PodaciOZaposlenjuIZanimanjuPacijenta.MestoRada.ToString();
                    zdravstveniKartonForma.radUPosebnimUslovimaUnos.Text = zdravstveniKarton.PodaciOZaposlenjuIZanimanjuPacijenta.RadPodPosebnimUslovima.ToString();
                    zdravstveniKartonForma.sifraDjelatnostiUnos.Text = zdravstveniKarton.PodaciOZaposlenjuIZanimanjuPacijenta.SifraDelatnosti.ToString();
                    zdravstveniKartonForma.registarskiBrojUnos.Text = zdravstveniKarton.PodaciOZaposlenjuIZanimanjuPacijenta.RegBroj.ToString();
                    zdravstveniKartonForma.OSIZ.Text = zdravstveniKarton.PodaciOZaposlenjuIZanimanjuPacijenta.OSIZZdrZastite.ToString();
                    zdravstveniKartonForma.Show();


                }


            }
        }



        public void DodjelaZdravstvenogKartonaPacijentu(IzmenaNalogaPacijentaForma izmenaNalogaPacijentaForma)
        {
            Kartoni zdravstveniKartoni = Kartoni.Instance;
            Pacijenti pacijenti = Pacijenti.Instance;
            //string jmbg = NadjiPacijenta(pregledNalogaPacijenta);
            foreach (ZdravstveniKarton zdravstveniKarton in zdravstveniKartoni.listaKartona)
            {
                foreach (Pacijent pacijent in pacijenti.listaPacijenata)
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

        public void PregledNaloga(ListView pacijenti)
        {
            if (pacijenti.SelectedIndex >= 0)
            {
                PregledNalogaPacijenta pregled = new PregledNalogaPacijenta(pacijenti);
                Pacijent p = (Pacijent)pacijenti.SelectedItem;
                pregled.imeLabela.Content = p.ime;
                pregled.prezimeLabela.Content = p.prezime;
                pregled.JMBGUnos.Text = p.jmbg;
                pregled.datumLabela.Content = p.datumRodjenja.ToString("MM/dd/yyyy");
                pregled.mailLabela.Content = p.email;
                pregled.telLabela.Content = p.telefon;
                pregled.korisnikLabela.Content = p.korisnik.korisnickoIme;
                pregled.Show();
            }
        }


        public string NadjiPacijenta(PregledNalogaPacijenta pregledNalogaPacijenta)
        {
            Pacijenti pacijenti = Pacijenti.Instance;
            string jmbg = pregledNalogaPacijenta.JMBGUnos.Text;

            foreach (Pacijent pacijent in pacijenti.listaPacijenata)
            {
                if (jmbg.Equals(pacijent.jmbg))
                {
                    return pacijent.jmbg;
                }
            }

            return "Nije nadjen";
        }



        public void DodavanjeAlergenaPacijentu(DodajAlergenPacijentu dodajAlergenPacijentu ,IzmjenaZdravstvenogKartonaForma izmjenaZdravstvenogKartonaForma)
        {
            Alergen a = (Alergen)dodajAlergenPacijentu.ListaAlergena.SelectedItem;
            
            foreach(Pacijent p in Pacijenti.Instance.listaPacijenata)
            {
                if(p.jmbg.Equals(izmjenaZdravstvenogKartonaForma.JMBGLabela.Content))
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
            if (izmjenaZdravstvenogKartonaForma.ListaAlergena.SelectedIndex > -1)
            {
                Alergen a = (Alergen)izmjenaZdravstvenogKartonaForma.ListaAlergena.SelectedItem;


                foreach (Pacijent p in Pacijenti.Instance.listaPacijenata)
                {
                    if (p.jmbg.Equals(izmjenaZdravstvenogKartonaForma.JMBGLabela.Content))
                    {
                        foreach (Alergen alergen in Alergeni.Instance.listaAlergena)
                        {
                            if (a.nazivAlergena == alergen.nazivAlergena)
                            {
                                System.Diagnostics.Debug.WriteLine(p.jmbg);
                                p.zdravstveniKarton.RemoveAlergen(alergen);
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
            foreach (Pacijent pacijent in Pacijenti.Instance.listaPacijenata)
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

                foreach (Pacijent pacijent in Pacijenti.Instance.listaPacijenata.ToList())
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

                foreach (Pacijent pacijent in Pacijenti.Instance.listaPacijenata.ToList())
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

                foreach (Pacijent pacijent in Pacijenti.Instance.listaPacijenata.ToList())
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


   
