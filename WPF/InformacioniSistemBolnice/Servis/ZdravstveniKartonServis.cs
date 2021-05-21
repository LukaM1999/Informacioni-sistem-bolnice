using InformacioniSistemBolnice;
using Model;
using Repozitorijum;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Servis
{
    public class ZdravstveniKartonServis
    {
        private static readonly Lazy<ZdravstveniKartonServis>
          lazy =
          new Lazy<ZdravstveniKartonServis>
              (() => new ZdravstveniKartonServis());
        public static ZdravstveniKartonServis Instance { get { return lazy.Value; } }

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
            
            Alergen alergen = (Alergen)listaAlergena.SelectedItem;
            alergeni.Add(alergen);
           
            zdravstveniKarton.Alergeni = alergeni;
            Kartoni.Instance.listaKartona.Add(zdravstveniKarton);
            Kartoni.Instance.Serijalizacija();
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
                p.zdravstveniKarton.PodaciOZaposlenjuIZanimanjuPacijenta.RadnoMjesto = izmjenaZdravstvenogKartonaForma.radnoMjestoUnos.Text;
                p.zdravstveniKarton.PodaciOZaposlenjuIZanimanjuPacijenta.RegistarskiBroj = izmjenaZdravstvenogKartonaForma.registarskiBrojUnos.Text;
                p.zdravstveniKarton.PodaciOZaposlenjuIZanimanjuPacijenta.RadPodPosebnimUslovima = izmjenaZdravstvenogKartonaForma.radUPosebnimUslovimaUnos.Text;
                p.zdravstveniKarton.PodaciOZaposlenjuIZanimanjuPacijenta.Promjene = izmjenaZdravstvenogKartonaForma.promjene.Text;
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


    }
}
