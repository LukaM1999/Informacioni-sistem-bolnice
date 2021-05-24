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
            UpravljanjeNalozimaLekara.Instance.KreirajNalog(lekarDto);
        }

        public void UklanjanjeNalogaLekara(Lekar lekar)
        {
            UpravljanjeNalozimaLekara.Instance.UkloniNalog(lekar);
        }

        public void IzmenaNalogaLekara(LekarDto lekarDto, Lekar lekar)
        {
            UpravljanjeNalozimaLekara.Instance.IzmeniNalog(lekarDto, lekar);
        }
        public LekarDto PregledNalogaLekara(Lekar lekar)
        {
            return UpravljanjeNalozimaLekara.Instance.PregledajNalog(lekar);
        }

        public void KreiranjeNaloga(PacijentDto pacijentDto)
        {
            UpravljanjeNalozimaPacijenata.Instance.KreirajNalog(pacijentDto);
        }

        public void KreiranjeZdravstvenogKartona(ZdravstveniKartonDto zdravstveniKartonDto,
                                                PodaciOZaposlenjuIZanimanjuDto podaciOZaposlenjuIZanimanjuDto)
        {
            ZdravstveniKartonServis.Instance.KreirajZdravstveniKarton(zdravstveniKartonDto, podaciOZaposlenjuIZanimanjuDto);
        }

        public void DodelaZdravstvenogKartonaPacijentu()
        {
            ZdravstveniKartonServis.Instance.DodeliZdravstveniKartonPacijentu();
        }

        public void UklanjanjeNaloga(Pacijent pacijent)
        {
            UpravljanjeNalozimaPacijenata.Instance.UkloniNalog(pacijent);
        }

        public void UklanjanjeGostujucegNaloga(ListView listaPacijenata)
        {
            UpravljanjeUrgentnimSistemom.Instance.UklanjanjeGostujucegNaloga(listaPacijenata);
        }

        public void IzmenaNaloga(PacijentDto pacijentDto, Pacijent pacijent)
        {
            UpravljanjeNalozimaPacijenata.Instance.IzmeniNalog(pacijentDto, pacijent);
        }

        public void IzmenaZdravstvenogKartona(ZdravstveniKartonDto zdravstveniKartonDto, ZdravstveniKarton zdravstveniKarton,
            PodaciOZaposlenjuIZanimanjuDto podaciOZaposlenjuIZanimanjuDto)
        {
            ZdravstveniKartonServis.Instance.IzmeniZdravstveniKarton(zdravstveniKartonDto, podaciOZaposlenjuIZanimanjuDto, zdravstveniKarton);
        }

        public void PregledNaloga(ListView pacijenti)
        {
            //UpravljanjeNalozimaPacijenata.Instance.PregledNaloga(pacijenti);
        }

        public void DefinisanjeAlergena(AlergenDto alergen)
        {
            UpravljanjeAlergenima.Instance.KreiranjeAlergena(alergen);

        }

        public void IzmjenaAlergena(AlergenDto noviAlergen, Alergen stariAlergen)
        {
            UpravljanjeAlergenima.Instance.IzmenaAlergena(noviAlergen, stariAlergen);
        }

        public void UklanjanjeAlergena(Alergen alergen)
        {
            UpravljanjeAlergenima.Instance.UklanjanjeAlergena(alergen);
        }

        public AlergenDto PregledAlergena(Alergen alergen)
        {
            return UpravljanjeAlergenima.Instance.PregledAlergena(alergen);
        }


        public void DodavanjeAlergenaIzZdravstvenogKartona(Alergen alergen, string jmbg)
        {
            ZdravstveniKartonServis.Instance.DodajAlergenPacijentu(alergen, jmbg);
        }

        public void UklanjanjeAlergenaIzZdravstvenogKartona(Alergen alergen, string jmbg)
        {
            ZdravstveniKartonServis.Instance.ObrisiAlergenPacijentu(alergen, jmbg);
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

        public void KreiranjeVesti(VestDto vestDto)
        {
            UpravljanjeVestima.Instance.KreiranjeVesti(vestDto);
        }

        public VestDto PregledVesti(Vest vest)
        {
            return UpravljanjeVestima.Instance.PregledVesti(vest);
        }

        public void UklanjanjeVesti(Vest vest)
        {
            UpravljanjeVestima.Instance.UklanjanjeVesti(vest);
        }

        public void IzmenaVesti(VestDto novaVest, Vest staraVest)
        {
            UpravljanjeVestima.Instance.IzmenaVesti(novaVest, staraVest);
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