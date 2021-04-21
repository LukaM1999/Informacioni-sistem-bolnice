using System;
using System.Linq;
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

        public void Premestanje(Prostorija izProstorije, Prostorija uProstoriju, DinamickaOprema dinamickaOprema, int kolicina)
        {
            


        }

        public Repozitorijum.StatickaOprema magacin;
        public Repozitorijum.Prostorije prostorije;

    }
}