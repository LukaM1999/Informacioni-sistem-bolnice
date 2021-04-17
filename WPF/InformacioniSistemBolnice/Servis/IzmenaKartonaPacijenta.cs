using System;

namespace Servis
{
    public class IzmenaKartonaPacijenta
    {
        private static readonly Lazy<IzmenaKartonaPacijenta>
           lazy =
           new Lazy<IzmenaKartonaPacijenta>
               (() => new IzmenaKartonaPacijenta());

        public static IzmenaKartonaPacijenta Instance { get { return lazy.Value; } }

        public void IzdavanjeRecepta()
        {
            throw new NotImplementedException();
        }

        public void DodavanjeAnamneze()
        {
            throw new NotImplementedException();
        }

        public Repozitorijum.Pacijenti pacijenti;

    }
}