using System;
using System.Linq;
using InformacioniSistemBolnice.DTO;
using Model;
using Repozitorijum;

namespace Servis
{
    public class RaspodelaDinamickeOpremeServis
    {
        private static readonly Lazy<RaspodelaDinamickeOpremeServis> Lazy = new(() => new RaspodelaDinamickeOpremeServis());
        public static RaspodelaDinamickeOpremeServis Instance => Lazy.Value;

        public void Premestanje(RaspodelaDinamickeOpremeDto dto)
        {
            if (dto.IzProstorijeId == null)
            {
                RaspodeliOpremuUIzMagacinaUProstoriju(dto);
                return;
            }
            if(dto.UProstorijuId == null)
            {
                RaspodeliOpremuIzProstorijeUMagacin(dto);
                return;
            }
            else
            {
                RaspodeliOpremuIzProstorijeUProstoriju(dto);
            }
            
        }

        private void RaspodeliOpremuUIzMagacinaUProstoriju(RaspodelaDinamickeOpremeDto dto)
        {
            DinamickaOprema izabranaOprema = DinamickaOpremaRepo.Instance.NadjiPoTipu(dto.Oprema.Tip);
            izabranaOprema.Kolicina -= dto.Kolicina;
            DinamickaOpremaRepo.Instance.Serijalizacija();
            Prostorija izabranaProstorija = null;
            if (ProstorijaRepo.Instance.NadjiPoId(dto.UProstorijuId).Inventar.NadjiDinamickuOpremuPoTipu(dto.Oprema.Tip) != null)
            {
                izabranaProstorija = ProstorijaRepo.Instance.NadjiPoId(dto.UProstorijuId);
                DinamickaOprema izabranaOpremaProstorije = izabranaProstorija.Inventar.NadjiDinamickuOpremuPoTipu(dto.Oprema.Tip);
                izabranaOpremaProstorije.Kolicina += dto.Kolicina;
                ProstorijaRepo.Instance.Serijalizacija();
                return;
            }
            izabranaProstorija = ProstorijaRepo.Instance.NadjiPoId(dto.UProstorijuId);
            izabranaProstorija.Inventar.DodajDinamickuOpremu(new(dto.Kolicina, dto.Oprema.Tip));
            ProstorijaRepo.Instance.Serijalizacija();
        }

        private void RaspodeliOpremuIzProstorijeUMagacin(RaspodelaDinamickeOpremeDto dto)
        {
            Prostorija izabranaProstorija = ProstorijaRepo.Instance.NadjiPoId(dto.IzProstorijeId);
            DinamickaOprema izabranaOpremaProstorije = izabranaProstorija.Inventar.NadjiDinamickuOpremuPoTipu(dto.Oprema.Tip);
            izabranaOpremaProstorije.Kolicina -= dto.Kolicina;
            ProstorijaRepo.Instance.Serijalizacija();
            if (DinamickaOpremaRepo.Instance.NadjiPoTipu(dto.Oprema.Tip) != null)
            {
                DinamickaOprema izabranaOprema = DinamickaOpremaRepo.Instance.NadjiPoTipu(dto.Oprema.Tip);
                izabranaOprema.Kolicina += dto.Kolicina;
                DinamickaOpremaRepo.Instance.Serijalizacija();
                return;
            }
            DinamickaOpremaRepo.Instance.DodajDinamickuOpremu(new(dto.Kolicina, dto.Oprema.Tip));
            DinamickaOpremaRepo.Instance.Serijalizacija();
        }

        private void RaspodeliOpremuIzProstorijeUProstoriju(RaspodelaDinamickeOpremeDto dto)
        {
            Prostorija izabranaProstorija = ProstorijaRepo.Instance.NadjiPoId(dto.IzProstorijeId);
            DinamickaOprema izabranaOpremaProstorije = izabranaProstorija.Inventar.NadjiDinamickuOpremuPoTipu(dto.Oprema.Tip);
            izabranaOpremaProstorije.Kolicina -= dto.Kolicina;
            ProstorijaRepo.Instance.Serijalizacija();
            Prostorija izabranaProstirijeOdrediste = null;
            if (ProstorijaRepo.Instance.NadjiPoId(dto.UProstorijuId).Inventar.NadjiDinamickuOpremuPoTipu(dto.Oprema.Tip) != null)
            {
                izabranaProstirijeOdrediste = ProstorijaRepo.Instance.NadjiPoId(dto.UProstorijuId);
                DinamickaOprema izabranaOprema = izabranaProstirijeOdrediste.Inventar
                    .NadjiDinamickuOpremuPoTipu(dto.Oprema.Tip);
                izabranaOprema.Kolicina += dto.Kolicina;
                ProstorijaRepo.Instance.Serijalizacija();
                return;
            }
            izabranaProstirijeOdrediste = ProstorijaRepo.Instance.NadjiPoId(dto.UProstorijuId);
            izabranaProstirijeOdrediste.Inventar.DodajDinamickuOpremu(new(dto.Kolicina, dto.Oprema.Tip));
            ProstorijaRepo.Instance.Serijalizacija();
        }
    }
}