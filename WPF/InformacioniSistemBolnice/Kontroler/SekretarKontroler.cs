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

        public void KreiranjeZdravstvenogKartona(ZdravstveniKartonForma zdravstveniKartonForma)
        {
            UpravljanjeNalozimaPacijenata.Instance.KreirajZdravstveniKarton(zdravstveniKartonForma);
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

        public void PregledZdravstvenogKartona()
        {
            UpravljanjeNalozimaPacijenata.Instance.PregledZdravstvenogKartona();
        }

        public void DefinisanjeAlergena(DefinisanjeAlergenaForma definisanjeAlergenaForma)
        {
            UpravljanjeAlergenima.Instance.KreiranjeAlergena(definisanjeAlergenaForma);

        }

        public void IzmjenaAlergena(ListView ListaAlergena, IzmenaAlergenaForma izmenaAlergenaForma)
        {
            UpravljanjeAlergenima.Instance.IzmenaAlergena(ListaAlergena, izmenaAlergenaForma);
        }

        public void UklanjanjeAlergena(ListView ListaAlergena)
        {
            UpravljanjeAlergenima.Instance.UklanjanjeAlergena(ListaAlergena);
        }

        public void PregledAlergena(ListView ListaAlergena)
        {
            UpravljanjeAlergenima.Instance.PregledAlergena(ListaAlergena);
        }



        public UpravljanjeNalozimaPacijenata upravljanjeNalozimaPacijenata;
        public UpravljanjeAlergenima upravljanjeAlergenima;
        public UpravljanjeTerminimaPacijenata upravljanjeTerminimaPacijenata;

    }
}