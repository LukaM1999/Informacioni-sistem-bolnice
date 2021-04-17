using System;

namespace Servis
{
    public class UpravljanjeStatickomOpremom
    {

        private static readonly Lazy<UpravljanjeStatickomOpremom>
           lazy =
           new Lazy<UpravljanjeStatickomOpremom>
               (() => new UpravljanjeStatickomOpremom());

        public static UpravljanjeStatickomOpremom Instance { get { return lazy.Value; } }

        public void KreiranjeOpreme()
        {
            throw new NotImplementedException();
        }

        public void UklanjanjeOpreme()
        {
            throw new NotImplementedException();
        }

        public void IzmenaOpreme()
        {
            throw new NotImplementedException();
        }

        public void PregledOpreme()
        {
            throw new NotImplementedException();
        }

        public Repozitorijum.Magacin magacin;

    }
}