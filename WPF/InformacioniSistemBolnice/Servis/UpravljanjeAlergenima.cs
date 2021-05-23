using System;
using Model;
using Repozitorijum;
using InformacioniSistemBolnice;
using System.Windows.Controls;


namespace Servis
{
    public class UpravljanjeAlergenima
    {

        private static readonly Lazy<UpravljanjeAlergenima>
           lazy =
           new Lazy<UpravljanjeAlergenima>
               (() => new UpravljanjeAlergenima());

        public static UpravljanjeAlergenima Instance { get { return lazy.Value; } }

        public void KreiranjeAlergena(AlergenDto alergenDto)
        {
            Alergeni.Instance.DodajAlergen(new Alergen(alergenDto.Naziv));
        }

        public void UklanjanjeAlergena(Alergen alergen)
        {
            Alergeni.Instance.BrisiPoNazivu(alergen.Naziv);
            Alergeni.Instance.SacuvajPromene();
        }

        public void IzmenaAlergena(ListView ListaAlergena, IzmenaAlergenaForma izmenaAlergenaForma)
        {

            if (ListaAlergena.SelectedValue != null)
            {
                Alergen alergen = (Alergen)ListaAlergena.SelectedValue;
                alergen.Naziv = izmenaAlergenaForma.nazivAlergenaUnos.Text;
                Alergeni.Instance.Serijalizacija();
                Alergeni.Instance.Deserijalizacija();
                ListaAlergena.ItemsSource = Alergeni.Instance.listaAlergena;

            }

        }

        public void PregledAlergena(AlergeniProzor alergeni)
        {
            if (alergeni.ListaAlergena.SelectedIndex >= 0)
            {
                PregledAlergena pregledAlergena = new PregledAlergena(alergeni);
                Alergen alergen = (Alergen)alergeni.ListaAlergena.SelectedItem;
                pregledAlergena.labelaAlergen.Content = alergen.Naziv;
                pregledAlergena.pocetna.contentControl.Content = pregledAlergena.Content;

            }
        }

        public Repozitorijum.Alergeni alergeni;

    }
}