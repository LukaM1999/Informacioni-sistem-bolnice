using System;

namespace Servis
{
    public class UpravljanjeAlergenima
    {

        private static readonly Lazy<UpravljanjeAlergenima>
           lazy =
           new Lazy<UpravljanjeAlergenima>
               (() => new UpravljanjeAlergenima());

        public static UpravljanjeAlergenima Instance { get { return lazy.Value; } }

        public void KreiranjeAlergena()
        {
            throw new NotImplementedException();
        }

        public void UklanjanjeAlergena()
        {
            throw new NotImplementedException();
        }

        public void IzmenaAlergena()
        {
            throw new NotImplementedException();
        }

        public void PregledAlergena()
        {
            throw new NotImplementedException();
        }

        public Repozitorijum.Alergeni alergeni;

    }
}