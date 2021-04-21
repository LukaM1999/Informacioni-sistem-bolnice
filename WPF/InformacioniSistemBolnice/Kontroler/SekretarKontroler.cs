using System;
using System.Windows.Controls;
using InformacioniSistemBolnice;
using Servis;
using Model;

namespace Kontroler
{
    public class SekretarKontroler
    {
        private static readonly Lazy<SekretarKontroler>
           lazy =
           new Lazy<SekretarKontroler>
               (() => new SekretarKontroler());

        public static SekretarKontroler Instance { get { return lazy.Value; } }

        public void KreiranjeNaloga(RegistracijaPacijentaForma registracija)
        {
            UpravljanjeNalozimaPacijenata.Instance.KreiranjeNaloga(registracija);
        }

        public void KreiranjeZdravstvenogKartona(ZdravstveniKartonForma zdravstveniKartonForma)
        {
            UpravljanjeNalozimaPacijenata.Instance.KreiranjeZdravstvenogKarton(zdravstveniKartonForma);
        }

        public void DodjelaZdravstvenogKartonaPacijentu(IzmenaNalogaPacijentaForma izmenaNalogaPacijentaForma)
        {
            UpravljanjeNalozimaPacijenata.Instance.DodjelaZdravstvenogKartonaPacijentu(izmenaNalogaPacijentaForma);
        }


        public void UklanjanjeNaloga(ListView listaPacijenata)
        {
            UpravljanjeNalozimaPacijenata.Instance.UklanjanjeNaloga(listaPacijenata);
        }

        public void IzmenaNaloga(IzmenaNalogaPacijentaForma izmena, ListView listaPacijenata)
        {
            UpravljanjeNalozimaPacijenata.Instance.IzmenaNaloga(izmena, listaPacijenata);
        }

        public void IzmenaZdravstvenogKartona(IzmjenaZdravstvenogKartonaForma izmjenaZdravstvenogKartonaForma, ListView listaPacijenata)
        {
            UpravljanjeNalozimaPacijenata.Instance.IzmjenaZdravstvenogKartona(izmjenaZdravstvenogKartonaForma, listaPacijenata);
        }

        public void PregledNaloga(ListView pacijenti)
        {
            UpravljanjeNalozimaPacijenata.Instance.PregledNaloga(pacijenti);
        }


        public void DefinisanjeAlergena(DefinisanjeAlergenaForma definisanjeAlergenaForma)
        {
            UpravljanjeAlergenima.Instance.KreiranjeAlergena(definisanjeAlergenaForma);

        }

        public void IzmjenaAlergena(DataGrid ListaAlergena, IzmenaAlergenaForma izmenaAlergenaForma)
        {
            UpravljanjeAlergenima.Instance.IzmenaAlergena(ListaAlergena, izmenaAlergenaForma);
        }

        public void UklanjanjeAlergena(DataGrid ListaAlergena)
        {
            UpravljanjeAlergenima.Instance.UklanjanjeAlergena(ListaAlergena);
        }

        public void PregledAlergena(DataGrid ListaAlergena)
        {
            UpravljanjeAlergenima.Instance.PregledAlergena(ListaAlergena);
        }


        public void DodavanjeAlergenaIzZdravstvenogKartona(DodajAlergenPacijentu dodajAlergenPacijentu, IzmjenaZdravstvenogKartonaForma izmjenaZdravstvenogKartonaFormadod)
        {
            UpravljanjeNalozimaPacijenata.Instance.DodavanjeAlergenaPacijentu(dodajAlergenPacijentu, izmjenaZdravstvenogKartonaFormadod);
        }

        public  void UklanjanjeAlergenaIzZdravstvenogKartona(IzmjenaZdravstvenogKartonaForma izmjenaZdravstvenogKartonaFormadod)
        {
            UpravljanjeNalozimaPacijenata.Instance.BrisanjeAlergenaPacijentu(izmjenaZdravstvenogKartonaFormadod);
        }

        public void PomjeranjeTerminaPacijenata(PomjeranjeTerminaProzorSekretara pomjeranjeTerminaProzorSekretara)
        {
            UpravljanjeNalozimaPacijenata.Instance.Pomeranje(pomjeranjeTerminaProzorSekretara);
        }




        public UpravljanjeNalozimaPacijenata upravljanjeNalozimaPacijenata;
        public UpravljanjeAlergenima upravljanjeAlergenima;
        public UpravljanjeTerminimaPacijenata upravljanjeTerminimaPacijenata;

    }
}