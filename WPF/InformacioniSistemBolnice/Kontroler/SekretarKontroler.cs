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

        public void KreiranjeNalogaLekara(LekarDto lekarDto)
        {
            UpravljanjeNalozimaLekara.Instance.KreiranjeNaloga(lekarDto);
        }

        public void UklanjanjeNalogaLekara(Lekar lekar)
        {
            UpravljanjeNalozimaLekara.Instance.UklanjanjeNaloga(lekar);
        }

        public void IzmenaNalogaLekara(LekarDto lekarDto, Lekar lekar)
        {
            UpravljanjeNalozimaLekara.Instance.IzmenaNaloga(lekarDto, lekar);
        }
        public LekarDto PregledNalogaLekara(Lekar lekar)
        {
            return UpravljanjeNalozimaLekara.Instance.PregledNaloga(lekar);
        }

        public void KreiranjeNaloga(PacijentDto pacijentDto)
        {
            UpravljanjeNalozimaPacijenata.Instance.KreiranjeNaloga(pacijentDto);
        }

        public void KreiranjeZdravstvenogKartona(ZdravstveniKartonDto zdravstveniKartonDto,
                                                PodaciOZaposlenjuIZanimanjuDto podaciOZaposlenjuIZanimanjuDto)
        {
            ZdravstveniKartonServis.Instance.KreiranjeZdravstvenogKarton(zdravstveniKartonDto, podaciOZaposlenjuIZanimanjuDto);
        }

        public void DodjelaZdravstvenogKartonaPacijentu()
        {
            ZdravstveniKartonServis.Instance.DodjelaZdravstvenogKartonaPacijentu();
        }


        public void UklanjanjeNaloga(Pacijent pacijent)
        {
            UpravljanjeNalozimaPacijenata.Instance.UklanjanjeNaloga(pacijent);
        }

        public void UklanjanjeGostujucegNaloga(ListView listaPacijenata)
        {
            UpravljanjeUrgentnimSistemom.Instance.UklanjanjeGostujucegNaloga(listaPacijenata);
        }


        public void IzmenaNaloga(PacijentDto pacijentDto, Pacijent pacijent)
        {
            UpravljanjeNalozimaPacijenata.Instance.IzmenaNaloga(pacijentDto, pacijent);
        }

        public void IzmenaZdravstvenogKartona(ZdravstveniKartonDto zdravstveniKartonDto, ZdravstveniKarton zdravstveniKarton,
            PodaciOZaposlenjuIZanimanjuDto podaciOZaposlenjuIZanimanjuDto)
        {
            ZdravstveniKartonServis.Instance.IzmjenaZdravstvenogKartona(zdravstveniKartonDto, podaciOZaposlenjuIZanimanjuDto, zdravstveniKarton);
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

        public void PregledAlergena(AlergeniProzor alergeniProzor)
        {
            UpravljanjeAlergenima.Instance.PregledAlergena(alergeniProzor);
        }


        public void DodavanjeAlergenaIzZdravstvenogKartona(DodajAlergenPacijentu dodajAlergenPacijentu, IzmjenaZdravstvenogKartonaForma izmjenaZdravstvenogKartonaFormadod)
        {
            ZdravstveniKartonServis.Instance.DodavanjeAlergenaPacijentu(dodajAlergenPacijentu, izmjenaZdravstvenogKartonaFormadod);
        }

        public  void UklanjanjeAlergenaIzZdravstvenogKartona(IzmjenaZdravstvenogKartonaForma izmjenaZdravstvenogKartonaFormadod)
        {
            ZdravstveniKartonServis.Instance.BrisanjeAlergenaPacijentu(izmjenaZdravstvenogKartonaFormadod);
        }

        public void ZakazivanjeTermina(Termin termin)
        {
            UpravljanjeTerminima.Instance.Zakazivanje(termin);
        }

        public void PomjeranjeTerminaPacijenata(Termin terminZaPomeranje, Termin noviTermin)
        {
            UpravljanjeTerminima.Instance.Pomeranje(terminZaPomeranje, noviTermin);
        }

        public void Otkazivanje(Termin termin)
        {
            UpravljanjeTerminima.Instance.Otkazivanje(termin);
        }

        public void PomeranjeVanrednogTerminaPacijenta(IzborTerminaZaNovoZakazivanje izborTerminaZaNovoZakazivanje, IzborTerminaZaPomeranje izborTerminaZaPomeranje)
        {
            UpravljanjeUrgentnimSistemom.Instance.PomeranjeVanrednogTermina(izborTerminaZaNovoZakazivanje, izborTerminaZaPomeranje);
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

        public void IzmenaVesti(VestiProzor vestiProzor, IzmenaVesti izmenaVesti)
        {
            UpravljanjeVestima.Instance.IzmenaVesti(vestiProzor, izmenaVesti);
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
        public UpravljanjeTerminima upravljanjeTerminima;
        
    }
}