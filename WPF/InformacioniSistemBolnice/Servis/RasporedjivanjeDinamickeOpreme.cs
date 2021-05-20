using System;
using System.Linq;
using Model;
using Repozitorijum;

namespace Servis
{
    public class RasporedjivanjeDinamickeOpreme
    {
        private static readonly Lazy<RasporedjivanjeDinamickeOpreme> lazy = new Lazy<RasporedjivanjeDinamickeOpreme> 
            (() => new RasporedjivanjeDinamickeOpreme());
        public static RasporedjivanjeDinamickeOpreme Instance { get { return lazy.Value; } }
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
            DinamickaOpremaRepo.Instance.SacuvajPromene();
            if (Prostorije.Instance.NadjiPoId(dto.UProstorijuId).Inventar.NadjiDinamickuOpremuPoTipu(dto.Oprema.Tip) != null)
            {
                Prostorije.Instance.NadjiPoId(dto.UProstorijuId).Inventar.NadjiDinamickuOpremuPoTipu(dto.Oprema.Tip).Kolicina += dto.Kolicina;
                Prostorije.Instance.SacuvajPromene();
                return;
            }
            Prostorije.Instance.NadjiPoId(dto.UProstorijuId).Inventar.DinamickaOprema.Add(new(dto.Kolicina, dto.Oprema.Tip));
            Prostorije.Instance.SacuvajPromene();
        }
        private void RaspodeliOpremuIzProstorijeUMagacin(RaspodelaDinamickeOpremeDto dto)
        {
            Prostorije.Instance.NadjiPoId(dto.IzProstorijeId).Inventar.NadjiDinamickuOpremuPoTipu(dto.Oprema.Tip).Kolicina -= dto.Kolicina;
            Prostorije.Instance.SacuvajPromene();
            if (DinamickaOpremaRepo.Instance.NadjiPoTipu(dto.Oprema.Tip) != null)
            {
                DinamickaOpremaRepo.Instance.NadjiPoTipu(dto.Oprema.Tip).Kolicina += dto.Kolicina;
                DinamickaOpremaRepo.Instance.SacuvajPromene();
                return;
            }
            DinamickaOpremaRepo.Instance.ListaOpreme.Add(new(dto.Kolicina, dto.Oprema.Tip));
            DinamickaOpremaRepo.Instance.SacuvajPromene();
        }
        private void RaspodeliOpremuIzProstorijeUProstoriju(RaspodelaDinamickeOpremeDto dto)
        {
            Prostorije.Instance.NadjiPoId(dto.IzProstorijeId).Inventar.NadjiDinamickuOpremuPoTipu(dto.Oprema.Tip).Kolicina -= dto.Kolicina;
            Prostorije.Instance.SacuvajPromene();
            if (Prostorije.Instance.NadjiPoId(dto.UProstorijuId).Inventar.NadjiDinamickuOpremuPoTipu(dto.Oprema.Tip) != null)
            {
                Prostorije.Instance.NadjiPoId(dto.UProstorijuId).Inventar.NadjiDinamickuOpremuPoTipu(dto.Oprema.Tip).Kolicina += dto.Kolicina;
                Prostorije.Instance.SacuvajPromene();
                return;
            }
            Prostorije.Instance.NadjiPoId(dto.UProstorijuId).Inventar.DinamickaOprema.Add(new(dto.Kolicina, dto.Oprema.Tip));
            Prostorije.Instance.SacuvajPromene();
        }

    }
}