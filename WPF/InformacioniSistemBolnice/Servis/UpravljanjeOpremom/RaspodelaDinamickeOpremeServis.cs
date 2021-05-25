using System;
using System.Linq;
using InformacioniSistemBolnice.DTO;
using Model;
using Repozitorijum;

namespace Servis
{
    public class RaspodelaDinamickeOpremeServis
    {
        private static readonly Lazy<RaspodelaDinamickeOpremeServis> Lazy = new Lazy<RaspodelaDinamickeOpremeServis> 
            (() => new RaspodelaDinamickeOpremeServis());
        public static RaspodelaDinamickeOpremeServis Instance { get { return Lazy.Value; } }
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
            DinamickaOpremaRepo.Instance.NadjiPoTipu(dto.Oprema.Tip).Kolicina -= dto.Kolicina;
            DinamickaOpremaRepo.Instance.Serijalizacija();
            if (ProstorijaRepo.Instance.NadjiPoId(dto.UProstorijuId).Inventar.NadjiDinamickuOpremuPoTipu(dto.Oprema.Tip) != null)
            {
                ProstorijaRepo.Instance.NadjiPoId(dto.UProstorijuId).Inventar.NadjiDinamickuOpremuPoTipu(dto.Oprema.Tip).Kolicina += dto.Kolicina;
                ProstorijaRepo.Instance.Serijalizacija();
                return;
            }
            ProstorijaRepo.Instance.NadjiPoId(dto.UProstorijuId).Inventar.DinamickaOprema.Add(new(dto.Kolicina, dto.Oprema.Tip));
            ProstorijaRepo.Instance.Serijalizacija();
        }
        private void RaspodeliOpremuIzProstorijeUMagacin(RaspodelaDinamickeOpremeDto dto)
        {
            ProstorijaRepo.Instance.NadjiPoId(dto.IzProstorijeId).Inventar.NadjiDinamickuOpremuPoTipu(dto.Oprema.Tip).Kolicina -= dto.Kolicina;
            ProstorijaRepo.Instance.Serijalizacija();
            if (DinamickaOpremaRepo.Instance.NadjiPoTipu(dto.Oprema.Tip) != null)
            {
                DinamickaOpremaRepo.Instance.NadjiPoTipu(dto.Oprema.Tip).Kolicina += dto.Kolicina;
                DinamickaOpremaRepo.Instance.Serijalizacija();
                return;
            }
            DinamickaOpremaRepo.Instance.DinamickaOprema.Add(new(dto.Kolicina, dto.Oprema.Tip));
            DinamickaOpremaRepo.Instance.Serijalizacija();
        }
        private void RaspodeliOpremuIzProstorijeUProstoriju(RaspodelaDinamickeOpremeDto dto)
        {
            ProstorijaRepo.Instance.NadjiPoId(dto.IzProstorijeId).Inventar.NadjiDinamickuOpremuPoTipu(dto.Oprema.Tip).Kolicina -= dto.Kolicina;
            ProstorijaRepo.Instance.Serijalizacija();
            if (ProstorijaRepo.Instance.NadjiPoId(dto.UProstorijuId).Inventar.NadjiDinamickuOpremuPoTipu(dto.Oprema.Tip) != null)
            {
                ProstorijaRepo.Instance.NadjiPoId(dto.UProstorijuId).Inventar.NadjiDinamickuOpremuPoTipu(dto.Oprema.Tip).Kolicina += dto.Kolicina;
                ProstorijaRepo.Instance.Serijalizacija();
                return;
            }
            ProstorijaRepo.Instance.NadjiPoId(dto.UProstorijuId).Inventar.DinamickaOprema.Add(new(dto.Kolicina, dto.Oprema.Tip));
            ProstorijaRepo.Instance.Serijalizacija();
        }

    }
}