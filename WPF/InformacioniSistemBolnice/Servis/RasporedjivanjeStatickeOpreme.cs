using System;
using Model;
using Repozitorijum;
using System.Linq;
using System.Collections.ObjectModel;
using System.Threading;
using InformacioniSistemBolnice;

namespace Servis
{
    public class RasporedjivanjeStatickeOpreme
    {

        private static readonly Lazy<RasporedjivanjeStatickeOpreme>
           lazy =
           new Lazy<RasporedjivanjeStatickeOpreme>
               (() => new RasporedjivanjeStatickeOpreme());

        public static RasporedjivanjeStatickeOpreme Instance { get { return lazy.Value; } }

        public void ZakazivanjePremestanja(RaspodelaStatickeOpremeDto dto)
        {
            StatickaOpremaTermini.Instance.listaTermina.Add(new(dto.IzProstorijeId, dto.UProstorijuId,
                    dto.Oprema, dto.Kolicina, dto.Datum));
            StatickaOpremaTermini.Instance.SacuvajPromene();
        }

        public void ProveraPremestajaOpreme()
        {
            while (true)
            {
                foreach(StatickaOpremaTermin termin in StatickaOpremaTermini.Instance.listaTermina.ToList())
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
            StatickaOpremaTermini.Instance.listaTermina.Remove(termin);
            StatickaOpremaTermini.Instance.SacuvajPromene();
        }
        private void DodajOpremuUProstoriju(StatickaOpremaTermin termin)
        {
            if (Prostorije.Instance.NadjiPoId(termin.UProstorijuId).Inventar.NadjiStatickuOpremuPoTipu(termin.Oprema.Tip) == null)
            {
                Prostorije.Instance.NadjiPoId(termin.UProstorijuId).Inventar.StatickaOprema.Add(new(termin.Kolicina, termin.Oprema.Tip));
                Prostorije.Instance.SacuvajPromene();
                return;
            }
            Prostorije.Instance.NadjiPoId(termin.UProstorijuId).Inventar.NadjiStatickuOpremuPoTipu(termin.Oprema.Tip).Kolicina += termin.Kolicina;
            Prostorije.Instance.SacuvajPromene();
        }
        private void SmanjiKolicinuOpremeUProstoriji(StatickaOpremaTermin termin)
        {
            Prostorije.Instance.NadjiPoId(termin.IzProstorijeId).Inventar.NadjiStatickuOpremuPoTipu(termin.Oprema.Tip).
                                Kolicina -= termin.Kolicina;
            Prostorije.Instance.SacuvajPromene();
        }
        private void DodajOpremuUMagacin(StatickaOpremaTermin termin)
        {
            if (StatickaOpremaRepo.Instance.NadjiPoTipu(termin.Oprema.Tip) == null)
            {
                StatickaOpremaRepo.Instance.ListaOpreme.Add(new(termin.Kolicina, termin.Oprema.Tip));
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