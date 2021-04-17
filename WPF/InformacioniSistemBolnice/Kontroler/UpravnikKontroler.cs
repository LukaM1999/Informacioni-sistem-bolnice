using System;
using System.Windows.Controls;
using InformacioniSistemBolnice;
using Model;
using Servis;

namespace Kontroler
{
    public class UpravnikKontroler
    {
        private static readonly Lazy<UpravnikKontroler>
           lazy =
           new Lazy<UpravnikKontroler>
               (() => new UpravnikKontroler());

        public static UpravnikKontroler Instance { get { return lazy.Value; } }

        public void KreiranjeProstorije(ProstorijaForma p)
        {
            UpravljanjeProstorijama.Instance.KreiranjeProstorije(p);
        }

        public void UklanjanjeProstorije(ListView listaProstorija)
        {
            UpravljanjeProstorijama.Instance.UklanjanjeProstorije(listaProstorija);
        }

        public void IzmenaProstorije(ProstorijaFormaIzmeni izmena, Prostorija p)
        {
            UpravljanjeProstorijama.Instance.IzmenaProstorije(izmena, p);
        }

        public void PregledProstorije(Prostorija pr)
        {
            UpravljanjeProstorijama.Instance.PregledProstorije(pr);
        }

        public UpravljanjeProstorijama upravljanjeProstorijama;
        public UpravljanjeStatickomOpremom upravljanjeStatickomOpremom;
        public UpravljanjeDinamickomOpremom upravljanjeDinamickomOpremom;
        public RasporedjivanjeDinamickeOpreme rasporedjivanjeDinamickeOpreme;
        public RasporedjivanjeStatickeOpreme rasporedjivanjeStatickeOpreme;

    }
}