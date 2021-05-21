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

        public void KreiranjeZdravstvenogKarton(ZdravstveniKartonDto zdravstveniKartonDto,
                                                PodaciOZaposlenjuIZanimanjuDto podaciOZaposlenjuIZanimanjuDto)
        {
            ZdravstveniKarton zdravstveniKarton = new(zdravstveniKartonDto.BrojKartona, zdravstveniKartonDto.BrojKnjizice,
                                                      zdravstveniKartonDto.Jmbg, zdravstveniKartonDto.ImeJednogRoditelja,
                                                      zdravstveniKartonDto.LiceZaZdravstvenuZastitu, zdravstveniKartonDto.PolPacijenta,
                                                      zdravstveniKartonDto.BracnoStanje, zdravstveniKartonDto.KategorijaZdravstveneZastite,
                                                      VratiPodatkeOZaposlenju(podaciOZaposlenjuIZanimanjuDto));
            zdravstveniKarton.Alergeni.Add(zdravstveniKartonDto.Alergen);
            Kartoni.Instance.DodajKarton(zdravstveniKarton);
        }

        private static PodaciOZaposlenjuIZanimanju VratiPodatkeOZaposlenju(PodaciOZaposlenjuIZanimanjuDto podaciOZaposlenjuDto)
        {
            return new(podaciOZaposlenjuDto.RadnoMjesto, podaciOZaposlenjuDto.RegistarskiBroj, podaciOZaposlenjuDto.SifraDelatnosti,
                        podaciOZaposlenjuDto.PosaoKojiObavlja, podaciOZaposlenjuDto.OSIZZdrZastite,
                        podaciOZaposlenjuDto.RadPodPosebnimUslovima, podaciOZaposlenjuDto.Promjene);
        }

        public void DodjelaZdravstvenogKartonaPacijentu()
        {
            foreach (ZdravstveniKarton zdravstveniKarton in Kartoni.Instance.listaKartona)
                Pacijenti.Instance.DodjelaZdravstvenogKartonaPacijentu(zdravstveniKarton);
        }

        public void IzmjenaZdravstvenogKartona(ZdravstveniKartonDto zdravstveniKartonDto, 
                                                PodaciOZaposlenjuIZanimanjuDto podaciOZaposlenjuIZanimanjuDto,      
                                                ZdravstveniKarton zdravstveniKarton)
        {
            IzmeniOsnovnePodatke(zdravstveniKartonDto, zdravstveniKarton);
            IzmeniPodatkeOZanimanju(podaciOZaposlenjuIZanimanjuDto, zdravstveniKarton);
            Pacijenti.Instance.SacuvajPromene();
        }

        private static void IzmeniOsnovnePodatke(ZdravstveniKartonDto zdravstveniKartonDto, ZdravstveniKarton zdravstveniKarton)
        {
            zdravstveniKarton.BrojKartona = zdravstveniKartonDto.BrojKartona;
            zdravstveniKarton.BrojKnjizice = zdravstveniKartonDto.BrojKnjizice;
            zdravstveniKarton.Jmbg = zdravstveniKartonDto.Jmbg;
            zdravstveniKarton.ImeJednogRoditelja = zdravstveniKartonDto.ImeJednogRoditelja;
            zdravstveniKarton.LiceZaZdravstvenuZastitu = zdravstveniKartonDto.LiceZaZdravstvenuZastitu;
            zdravstveniKarton.BracnoStanje = zdravstveniKartonDto.BracnoStanje;
            zdravstveniKarton.PolPacijenta = zdravstveniKartonDto.PolPacijenta;
            zdravstveniKarton.KategorijaZdravstveneZastite = zdravstveniKartonDto.KategorijaZdravstveneZastite;
        }

        private static void IzmeniPodatkeOZanimanju(PodaciOZaposlenjuIZanimanjuDto podaciOZaposlenjuIZanimanjuDto, 
                                                    ZdravstveniKarton zdravstveniKarton)
        {
            zdravstveniKarton.PodaciOZaposlenjuIZanimanju.RadnoMjesto = podaciOZaposlenjuIZanimanjuDto.RadnoMjesto;
            zdravstveniKarton.PodaciOZaposlenjuIZanimanju.RegistarskiBroj = podaciOZaposlenjuIZanimanjuDto.RegistarskiBroj;
            zdravstveniKarton.PodaciOZaposlenjuIZanimanju.SifraDelatnosti = podaciOZaposlenjuIZanimanjuDto.SifraDelatnosti;
            zdravstveniKarton.PodaciOZaposlenjuIZanimanju.RadnoMjesto = podaciOZaposlenjuIZanimanjuDto.RadnoMjesto;
            zdravstveniKarton.PodaciOZaposlenjuIZanimanju.PosaoKojiObavlja = podaciOZaposlenjuIZanimanjuDto.PosaoKojiObavlja;
            zdravstveniKarton.PodaciOZaposlenjuIZanimanju.Promjene = podaciOZaposlenjuIZanimanjuDto.Promjene;
        }


        public void DodavanjeAlergenaPacijentu(DodajAlergenPacijentu dodajAlergenPacijentu, IzmjenaZdravstvenogKartonaForma izmjenaZdravstvenogKartonaForma)
        {
            Alergen a = (Alergen)dodajAlergenPacijentu.ListaAlergena.SelectedItem;

            Pacijent p = Pacijenti.Instance.NadjiPoJmbg(izmjenaZdravstvenogKartonaForma.JMBGLabela.Content.ToString());
                    foreach (Alergen alergen in Alergeni.Instance.listaAlergena)
                    {

                        if (a.nazivAlergena == alergen.nazivAlergena)
                        {
                           
                            p.zdravstveniKarton.AddAlergen(alergen);
                            Pacijenti.Instance.SacuvajPromene();
                            izmjenaZdravstvenogKartonaForma.ListaAlergena.ItemsSource = p.zdravstveniKarton.Alergeni;
                            break;
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
