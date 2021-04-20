using System;

namespace Servis
{
    public class UpravljanjeDinamickomOpremom
    {
        private static readonly Lazy<UpravljanjeDinamickomOpremom>
           lazy =
           new Lazy<UpravljanjeDinamickomOpremom>
               (() => new UpravljanjeDinamickomOpremom());

        public static UpravljanjeDinamickomOpremom Instance { get { return lazy.Value; } }

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

        public Repozitorijum.StatickaOprema magacin;

    }
}