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
        private static readonly Lazy<UpravljanjeLekovima>
           lazy =
           new Lazy<UpravljanjeLekovima>
               (() => new UpravljanjeLekovima());

        public static UpravljanjeLekovima Instance { get { return lazy.Value; } }

        public void KreiranjeLeka(LekDto dto)
        {
            Lek noviLek = new Lek(dto.naziv, dto.proizvodjac, dto.sastojci);
            Lekovi.Instance.listaLekova.Add(noviLek);
            Lekovi.Instance.Serijalizacija();
            Lekovi.Instance.Deserijalizacija();
        }

        public void UklanjanjeLeka(Lek lek)
        {
            Lekovi.Instance.listaLekova.Remove(lek);
            Lekovi.Instance.Serijalizacija();
            Lekovi.Instance.Deserijalizacija();
        }

        public void IzmenaLeka(LekDto dto)
        {
            
        }

        public void PregledLeka()
        {
            throw new NotImplementedException();
        }
    }
}
