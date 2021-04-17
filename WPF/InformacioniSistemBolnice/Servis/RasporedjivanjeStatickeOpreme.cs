using System;
using Model;

namespace Servis
{
    public class RasporedjivanjeStatickeOpreme
    {

        private static readonly Lazy<RasporedjivanjeStatickeOpreme>
           lazy =
           new Lazy<RasporedjivanjeStatickeOpreme>
               (() => new RasporedjivanjeStatickeOpreme());

        public static RasporedjivanjeStatickeOpreme Instance { get { return lazy.Value; } }

        public void ZakazivanjePremestanja(StatickaOprema statickaOprema, Prostorija izProstorije, Prostorija uProstoriju, int kolicina)
        {
            throw new NotImplementedException();
        }

        public Repozitorijum.Magacin magacin;
        public Repozitorijum.Prostorije prostorije;

    }
}