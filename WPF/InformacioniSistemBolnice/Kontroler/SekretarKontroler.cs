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

        public void DodavanjeAlergenaIzZdravstvenogKartona(Alergen alergen, string jmbg)
        {
            ZdravstveniKartonServis.Instance.DodavanjeAlergenaPacijentu(alergen, jmbg);
        }

        public void UklanjanjeAlergenaIzZdravstvenogKartona(Alergen alergen, string jmbg)
        {
            ZdravstveniKartonServis.Instance.BrisanjeAlergenaPacijentu(alergen, jmbg);
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