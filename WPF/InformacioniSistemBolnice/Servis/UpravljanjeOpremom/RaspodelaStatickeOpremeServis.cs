using System;
using Model;
using Repozitorijum;
using System.Linq;
using System.Collections.ObjectModel;
using System.Threading;
using InformacioniSistemBolnice;
using InformacioniSistemBolnice.DTO;

namespace Servis
{
    public class RaspodelaStatickeOpremeServis
    {

        private static readonly Lazy<RaspodelaStatickeOpremeServis> Lazy = new(() => new RaspodelaStatickeOpremeServis());
        public static RaspodelaStatickeOpremeServis Instance { get { return Lazy.Value; } }

        public void ZakazivanjePremestanja(RaspodelaStatickeOpremeDto dto)
        {
            PremestanjeStatickeOpremeRepo.Instance.DodajStatickuOpremu(new(dto.IzProstorijeId, dto.UProstorijuId,
                    dto.Oprema, dto.Kolicina, dto.Datum));
            PremestanjeStatickeOpremeRepo.Instance.Serijalizacija();
        }

        public void ProveraPremestajaOpreme()
        {
            while (true)
            {
                foreach(StatickaOpremaTermin termin in PremestanjeStatickeOpremeRepo.Instance.TerminiPremestanja.ToList())
                {
                    if (JeProsaoDatumRaspodeleStatickeOpreme(termin))
                    {
                        if(termin.IzProstorijeId == null)
                        {
                            SmanjiKolicinuOpremeUMagacinu(termin);
                            DodajOpremuUProstoriju(termin);
                            ObrisiTerminKojiJeProsao(termin);
                            break;
                        }
                        if (termin.UProstorijuId == null)
                        {
                            SmanjiKolicinuOpremeUProstoriji(termin);
                            DodajOpremuUMagacin(termin);
                            ObrisiTerminKojiJeProsao(termin);
                            break;
                        }
                        if (termin.UProstorijuId != null && termin.IzProstorijeId != null)
                        {
                            SmanjiKolicinuOpremeUProstoriji(termin);
                            DodajOpremuUProstoriju(termin);
                            ObrisiTerminKojiJeProsao(termin);
                            break;
                        }
                    }
                }
            }
        }

        private bool JeProsaoDatumRaspodeleStatickeOpreme(StatickaOpremaTermin termin)
        {
            return termin.DatumPremestaja.Year <= DateTime.Now.Year && termin.DatumPremestaja.Day <= DateTime.Now.Day 
                && termin.DatumPremestaja.Month <= DateTime.Now.Month;
        }

        private void ObrisiTerminKojiJeProsao(StatickaOpremaTermin termin)
        {
            PremestanjeStatickeOpremeRepo.Instance.BrisiTermin(termin);
            PremestanjeStatickeOpremeRepo.Instance.Serijalizacija();
        }

        private void DodajOpremuUProstoriju(StatickaOpremaTermin termin)
        {
            Prostorija izabranaProstorija = null;
            if (JeStatickaOpremaIzabraneProstorijeNull(termin))
            {
                izabranaProstorija = ProstorijaRepo.Instance.NadjiPoId(termin.UProstorijuId);
                izabranaProstorija.Inventar.DodajStatickuOpremu(new(termin.Kolicina, termin.Oprema.Tip));
                ProstorijaRepo.Instance.Serijalizacija();
                return;
            }
            izabranaProstorija = ProstorijaRepo.Instance.NadjiPoId(termin.UProstorijuId);
            StatickaOprema izabranaStatickaOprema = izabranaProstorija.Inventar.NadjiStatickuOpremuPoTipu(termin.Oprema.Tip);
            izabranaStatickaOprema.Kolicina += termin.Kolicina;
            ProstorijaRepo.Instance.Serijalizacija();
        }

        private bool JeStatickaOpremaIzabraneProstorijeNull(StatickaOpremaTermin termin)
        {
            return ProstorijaRepo.Instance.NadjiPoId(termin.UProstorijuId).Inventar.NadjiStatickuOpremuPoTipu(termin.Oprema.Tip) == null;
        }

        private void SmanjiKolicinuOpremeUProstoriji(StatickaOpremaTermin termin)
        {
            Prostorija izabranaProstorija = ProstorijaRepo.Instance.NadjiPoId(termin.IzProstorijeId);
            StatickaOprema izabranaStatickaOprema = izabranaProstorija.Inventar.NadjiStatickuOpremuPoTipu(termin.Oprema.Tip);
            izabranaStatickaOprema.Kolicina -= termin.Kolicina;
            ProstorijaRepo.Instance.Serijalizacija();
        }

        private void DodajOpremuUMagacin(StatickaOpremaTermin termin)
        {
            if (StatickaOpremaRepo.Instance.NadjiPoTipu(termin.Oprema.Tip) == null)
            {
                StatickaOpremaRepo.Instance.StatickaOprema.Add(new(termin.Kolicina, termin.Oprema.Tip));
                StatickaOpremaRepo.Instance.Serijalizacija();
                return;
            }
            StatickaOpremaRepo.Instance.NadjiPoTipu(termin.Oprema.Tip).Kolicina += termin.Kolicina;
            StatickaOpremaRepo.Instance.Serijalizacija();
        }

        private void SmanjiKolicinuOpremeUMagacinu(StatickaOpremaTermin termin)
        {
            StatickaOprema izabranaOprema = StatickaOpremaRepo.Instance.NadjiPoTipu(termin.Oprema.Tip);
            izabranaOprema.Kolicina -= termin.Kolicina;
            StatickaOpremaRepo.Instance.Serijalizacija();
        }
    }
}