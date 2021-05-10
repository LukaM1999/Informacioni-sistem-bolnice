using System;
using System.Windows.Controls;
using InformacioniSistemBolnice;
using Servis;
using Model;
using InformacioniSistemBolnice.Servis;

namespace Kontroler
{
    public class SekretarKontroler
    {
        private static readonly Lazy<SekretarKontroler>
           lazy =
           new Lazy<SekretarKontroler>
               (() => new SekretarKontroler());

        public static SekretarKontroler Instance { get { return lazy.Value; } }

        public void KreiranjeNaloga(RegistracijaPacijentaDto registracija)
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

        public void UklanjanjeGostujucegNaloga(ListView listaPacijenata)
        {
            UpravljanjeNalozimaPacijenata.Instance.UklanjanjeGostujucegNaloga(listaPacijenata);
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
            //UpravljanjeNalozimaPacijenata.Instance.PregledNaloga(pacijenti);
        }


        public void DefinisanjeAlergena(DefinisanjeAlergenaForma definisanjeAlergenaForma)
        {
            UpravljanjeAlergenima.Instance.KreiranjeAlergena(definisanjeAlergenaForma);

        }

        public void IzmjenaAlergena(ListView ListaAlergena, IzmenaAlergenaForma izmenaAlergenaForma)
        {
            UpravljanjeAlergenima.Instance.IzmenaAlergena(ListaAlergena, izmenaAlergenaForma);
        }

        public void UklanjanjeAlergena(ListView ListaAlergena)
        {
            UpravljanjeAlergenima.Instance.UklanjanjeAlergena(ListaAlergena);
        }

        public void PregledAlergena(ListView ListaAlergena)
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

        public void OtkazivanjeTerminaPacijenta(DataGrid listaZakazanihTermina)
        {
            UpravljanjeNalozimaPacijenata.Instance.Otkazivanje(listaZakazanihTermina);
        }

        public void PomeranjeVanrednogTerminaPacijenta(IzborTerminaZaNovoZakazivanje izborTerminaZaNovoZakazivanje, IzborTerminaZaPomeranje izborTerminaZaPomeranje)
        {
            UpravljanjeNalozimaPacijenata.Instance.PomeranjeVanrednogTermina(izborTerminaZaNovoZakazivanje, izborTerminaZaPomeranje);
        }

        public void KreiranjeVesti(KreirajVijestProzor kreirajVijestProzor)
        {
            UpravljanjeVestima.Instance.KreiranjeVesti(kreirajVijestProzor);
        }

        public void PregledVesti(ListView listaVesti)
        {
            UpravljanjeVestima.Instance.PregledVesti(listaVesti);
        }

        public void UklanjanjeVesti(ListView listaVesti)
        {
            UpravljanjeVestima.Instance.UklanjanjeVesti(listaVesti);
        }

        public void IzmenaVesti(ListView listaVesti, IzmenaVesti izmenaVesti)
        {
            UpravljanjeVestima.Instance.IzmenaVesti(listaVesti, izmenaVesti);
        }

        public void KreiranjeGostujucegPacijenta(GostujuciNalogDto gostujuciNalogDto)
        {
            UpravljanjeUrgentnimSistemom.Instance.KreiranjeGostujcegPacijenta(gostujuciNalogDto);
            
        }

        
        public void ZakazivanjeHitnogTermina(HitnoZakazivanjeDto hitnoZakazivanjeDto)
        {
            UpravljanjeUrgentnimSistemom.Instance.ZakazivanjeHitnogTermina(hitnoZakazivanjeDto);
        }


        public void PomeranjeTermina(TerminiLekaraZaPomeranjeDto terminiLekaraZaPomeranjeDto)
        {
            UpravljanjeUrgentnimSistemom.Instance.PomeranjeTermina(terminiLekaraZaPomeranjeDto);
        }

        public UpravljanjeNalozimaPacijenata upravljanjeNalozimaPacijenata;
        public UpravljanjeAlergenima upravljanjeAlergenima;
        public UpravljanjeTerminimaPacijenata upravljanjeTerminimaPacijenata;
        

    }
}