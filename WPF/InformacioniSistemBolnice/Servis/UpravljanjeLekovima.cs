using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Repozitorijum;

namespace Servis
{
    public class UpravljanjeLekovima
    {
        private static readonly Lazy<UpravljanjeLekovima> lazy = new Lazy<UpravljanjeLekovima> (() => new UpravljanjeLekovima());
        public static UpravljanjeLekovima Instance { get { return lazy.Value; } }
        public void KreiranjeLeka(LekDto dto)
        {
            Lekovi.Instance.ListaLekova.Add(new(dto.Naziv, dto.Proizvodjac, dto.Sastojci, dto.Zamena, dto.Alergeni));
            Lekovi.Instance.SacuvajPromene();
        }
        public void UklanjanjeLeka(LekDto dto)
        {
            Lekovi.Instance.BrisiPoNazivu(dto.Naziv);
            Lekovi.Instance.SacuvajPromene();
        }
        public void IzmenaLeka(LekDto dto)
        {
            Lekovi.Instance.NadjiPoNazivu(dto.Naziv).Proizvodjac = dto.Proizvodjac;
            Lekovi.Instance.NadjiPoNazivu(dto.Naziv).Zamena = dto.Zamena;
            Lekovi.Instance.NadjiPoNazivu(dto.Naziv).Sastojci = dto.Sastojci;
            Lekovi.Instance.NadjiPoNazivu(dto.Naziv).Alergen = dto.Alergeni;
            Lekovi.Instance.SacuvajPromene();
        }
    }
}
