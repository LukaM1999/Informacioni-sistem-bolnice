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

        private static readonly Lazy<RaspodelaStatickeOpremeServis>
           Lazy =
           new Lazy<RaspodelaStatickeOpremeServis>
               (() => new RaspodelaStatickeOpremeServis());

        public static RaspodelaStatickeOpremeServis Instance { get { return Lazy.Value; } }

        public void ZakazivanjePremestanja(RaspodelaStatickeOpremeDto dto)
        {
            PremestanjeStatickeOpremeRepo.Instance.TerminiPremestanja.Add(new(dto.IzProstorijeId, dto.UProstorijuId,
                    dto.Oprema, dto.Kolicina, dto.Datum));
            PremestanjeStatickeOpremeRepo.Instance.SacuvajPromene();
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
            PremestanjeStatickeOpremeRepo.Instance.TerminiPremestanja.Remove(termin);
            PremestanjeStatickeOpremeRepo.Instance.SacuvajPromene();
        }
        private void DodajOpremuUProstoriju(StatickaOpremaTermin termin)
        {
            if (ProstorijaRepo.Instance.NadjiPoId(termin.UProstorijuId).Inventar.NadjiStatickuOpremuPoTipu(termin.Oprema.Tip) == null)
            {
                ProstorijaRepo.Instance.NadjiPoId(termin.UProstorijuId).Inventar.StatickaOprema.Add(new(termin.Kolicina, termin.Oprema.Tip));
                ProstorijaRepo.Instance.SacuvajPromene();
                return;
            }
            ProstorijaRepo.Instance.NadjiPoId(termin.UProstorijuId).Inventar.NadjiStatickuOpremuPoTipu(termin.Oprema.Tip).Kolicina += termin.Kolicina;
            ProstorijaRepo.Instance.SacuvajPromene();
        }
        private void SmanjiKolicinuOpremeUProstoriji(StatickaOpremaTermin termin)
        {
            ProstorijaRepo.Instance.NadjiPoId(termin.IzProstorijeId).Inventar.NadjiStatickuOpremuPoTipu(termin.Oprema.Tip)
                        .Kolicina -= termin.Kolicina;
            ProstorijaRepo.Instance.SacuvajPromene();
        }
        private void DodajOpremuUMagacin(StatickaOpremaTermin termin)
        {
            if (StatickaOpremaRepo.Instance.NadjiPoTipu(termin.Oprema.Tip) == null)
            {
                StatickaOpremaRepo.Instance.StatickaOprema.Add(new(termin.Kolicina, termin.Oprema.Tip));
                StatickaOpremaRepo.Instance.SacuvajPromene();
                return;
            }
            StatickaOpremaRepo.Instance.NadjiPoTipu(termin.Oprema.Tip).Kolicina += termin.Kolicina;
            StatickaOpremaRepo.Instance.SacuvajPromene();
        }
        private void SmanjiKolicinuOpremeUMagacinu(StatickaOpremaTermin termin)
        {
            StatickaOpremaRepo.Instance.NadjiPoTipu(termin.Oprema.Tip).Kolicina -= termin.Kolicina;
            StatickaOpremaRepo.Instance.SacuvajPromene();
        }
    }
}