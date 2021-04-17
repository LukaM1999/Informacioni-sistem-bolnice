using System;
using Model;

namespace Servis
{
    public class RasporedjivanjeDinamickeOpreme
    {
        private static readonly Lazy<RasporedjivanjeDinamickeOpreme>
           lazy =
           new Lazy<RasporedjivanjeDinamickeOpreme>
               (() => new RasporedjivanjeDinamickeOpreme());

        public static RasporedjivanjeDinamickeOpreme Instance { get { return lazy.Value; } }

        public void Premestanje(Prostorija uProstoriju, DinamickaOprema dinamickaOprema, int kolicina)
        {
            throw new NotImplementedException();
        }

        public Repozitorijum.Magacin magacin;
        public Repozitorijum.Prostorije prostorije;

    }
}