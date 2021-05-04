using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Repozitorijum;

namespace Servis
{
    class Renoviranje
    {
        private static readonly Lazy<Renoviranje>
           lazy =
           new Lazy<Renoviranje>
               (() => new Renoviranje());

        public static Renoviranje Instance { get { return lazy.Value; } }

        public void ZakazivanjeRenoviranja(ProstorijaRenoviranjeDto dto)
        {
            RenoviranjeTermin novTermin = new RenoviranjeTermin(dto.PocetakRenoviranja, dto.KrajRenoviranja, dto.Prostorija.id);
            Prostorije.Instance.getSelected(dto.Prostorija).renoviranjeTermin = novTermin;
            Prostorije.Instance.Serijalizacija();
            Prostorije.Instance.Deserijalizacija();
            RenoviranjeTermini.Instance.listaTermina.Add(novTermin);
            RenoviranjeTermini.Instance.Serijalizacija();
            RenoviranjeTermini.Instance.Deserijalizacija();
        }

        public void ProveraRenoviranja()
        {
            while (true)
            {

            }
        }
    }
}
