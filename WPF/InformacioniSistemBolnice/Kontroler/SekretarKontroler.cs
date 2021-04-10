using System;
using System.Windows.Controls;
using InformacioniSistemBolnice;
using Servis;

namespace Kontroler
{
    public class SekretarKontroler
    {
        private static readonly Lazy<SekretarKontroler>
           lazy =
           new Lazy<SekretarKontroler>
               (() => new SekretarKontroler());

        public static SekretarKontroler Instance { get { return lazy.Value; } }

        public void KreiranjeNaloga(RegistracijaPacijentaForma registracija)
        {
            UpravljanjeNalozimaPacijenata.Instance.KreiranjeNaloga(registracija);
        }

        public void UklanjanjeNaloga(ListView listaPacijenata)
        {
            UpravljanjeNalozimaPacijenata.Instance.UklanjanjeNaloga(listaPacijenata);
        }

        public void IzmenaNaloga(IzmenaNalogaPacijentaForma izmena, ListView listaPacijenata)
        {
            UpravljanjeNalozimaPacijenata.Instance.IzmenaNaloga(izmena, listaPacijenata);
        }

        public void PregledNaloga(ListView pacijenti)
        {
            UpravljanjeNalozimaPacijenata.Instance.PregledNaloga(pacijenti);
        }

        public UpravljanjeNalozimaPacijenata upravljanjeNalozimaPacijenata;

    }
}