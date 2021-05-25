using System;
using System.Windows.Controls;
using InformacioniSistemBolnice;
using InformacioniSistemBolnice.DTO;
using Servis;
using Model;

namespace Kontroler
{
    public class SekretarKontroler
    {
        private static readonly Lazy<SekretarKontroler> Lazy = new(() => new SekretarKontroler());
        public static SekretarKontroler Instance => Lazy.Value;

        public void KreiranjeNalogaLekara(LekarDto lekarDto)
        {
            LekarServis.Instance.KreirajNalog(lekarDto);
        }

        public void UklanjanjeNalogaLekara(Lekar lekar)
        {
            LekarServis.Instance.UkloniNalog(lekar);
        }

        public void IzmenaNalogaLekara(LekarDto lekarDto, Lekar lekar)
        {
            LekarServis.Instance.IzmeniNalog(lekarDto, lekar);
        }
        public LekarDto PregledNalogaLekara(Lekar lekar)
        {
            return LekarServis.Instance.PregledajNalog(lekar);
        }

        public void KreiranjeNaloga(PacijentDto pacijentDto)
        {
            PacijentServis.Instance.KreirajNalog(pacijentDto);
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
            PacijentServis.Instance.UkloniNalog(pacijent);
        }

        public void UklanjanjeGostujucegNaloga(ListView listaPacijenata)
        {
            UrgentniSistemServis.Instance.UklanjanjeGostujucegNaloga(listaPacijenata);
        }

        public void IzmenaNaloga(PacijentDto pacijentDto, Pacijent pacijent)
        {
            PacijentServis.Instance.IzmeniNalog(pacijentDto, pacijent);
        }

        public void IzmenaZdravstvenogKartona(ZdravstveniKartonDto zdravstveniKartonDto, ZdravstveniKarton zdravstveniKarton,
            PodaciOZaposlenjuIZanimanjuDto podaciOZaposlenjuIZanimanjuDto)
        {
            ZdravstveniKartonServis.Instance.IzmeniZdravstveniKarton(zdravstveniKartonDto, podaciOZaposlenjuIZanimanjuDto, zdravstveniKarton);
        }

        public void DefinisanjeAlergena(AlergenDto alergen)
        {
            AlergenServis.Instance.KreiranjeAlergena(alergen);

        }

        public void IzmjenaAlergena(AlergenDto noviAlergen, Alergen stariAlergen)
        {
            AlergenServis.Instance.IzmenaAlergena(noviAlergen, stariAlergen);
        }

        public void UklanjanjeAlergena(Alergen alergen)
        {
            AlergenServis.Instance.UklanjanjeAlergena(alergen);
        }

        public AlergenDto PregledAlergena(Alergen alergen)
        {
            return AlergenServis.Instance.PregledAlergena(alergen);
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
            TerminServis.Instance.Zakazivanje(termin);
        }

        public void PomjeranjeTerminaPacijenata(Termin terminZaPomeranje, Termin noviTermin)
        {
            TerminServis.Instance.Pomeranje(terminZaPomeranje, noviTermin);
        }

        public void Otkazivanje(Termin termin)
        {
            TerminServis.Instance.Otkazivanje(termin);
        }

        public void PomeranjeVanrednogTerminaPacijenta(IzborTerminaZaNovoZakazivanje izborTerminaZaNovoZakazivanje, IzborTerminaZaPomeranje izborTerminaZaPomeranje)
        {
            UrgentniSistemServis.Instance.PomeranjeVanrednogTermina(izborTerminaZaNovoZakazivanje, izborTerminaZaPomeranje);
        }

        public void KreiranjeVesti(VestDto vestDto)
        {
            VestServis.Instance.KreiranjeVesti(vestDto);
        }

        public VestDto PregledVesti(Vest vest)
        {
            return VestServis.Instance.PregledVesti(vest);
        }

        public void UklanjanjeVesti(Vest vest)
        {
            VestServis.Instance.UklanjanjeVesti(vest);
        }

        public void IzmenaVesti(VestDto novaVest, Vest staraVest)
        {
            VestServis.Instance.IzmenaVesti(novaVest, staraVest);
        }

        public void KreiranjeGostujucegPacijenta(GostujuciNalogDto gostujuciNalogDto)
        {
            UrgentniSistemServis.Instance.KreiranjeGostujcegPacijenta(gostujuciNalogDto);

        }

        public void ZakazivanjeHitnogTermina(HitnoZakazivanjeDto hitnoZakazivanjeDto)
        {
            UrgentniSistemServis.Instance.ZakazivanjeHitnogTermina(hitnoZakazivanjeDto);
        }

        public void PomeranjeTermina(TerminiLekaraZaPomeranjeDto terminiLekaraZaPomeranjeDto)
        {
            UrgentniSistemServis.Instance.PomeranjeTermina(terminiLekaraZaPomeranjeDto);
        }
    }
}