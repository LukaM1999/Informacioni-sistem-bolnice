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

        public void KreiranjeAlergena(DefinisanjeAlergenaForma definisanjeAlergenaForma)
        {
            Alergen alergen = new Alergen(definisanjeAlergenaForma.nazivAlergenaUnos.Text);
            Alergeni.Instance.listaAlergena.Add(alergen);
            Alergeni.Instance.Serijalizacija();
           
        }

        public void UklanjanjeAlergena(ListView ListaAlergena)
        {
            if (ListaAlergena.SelectedValue != null)
            {
                Alergen alergen = (Alergen)ListaAlergena.SelectedValue;
                
                Alergeni alergeni = Alergeni.Instance;
                foreach (Alergen a in alergeni.listaAlergena)
                {
                    if (a.nazivAlergena.Equals(alergen.nazivAlergena))
                    {
                        alergeni.listaAlergena.Remove(a);
                        Alergeni.Instance.Serijalizacija();

                        break;
                    }
                }
            }
        }

        public void IzmenaAlergena(ListView ListaAlergena, IzmenaAlergenaForma izmenaAlergenaForma)
        {
            
            if (ListaAlergena.SelectedValue != null)
            {
                Alergen alergen = (Alergen)ListaAlergena.SelectedValue;
                alergen.nazivAlergena = izmenaAlergenaForma.nazivAlergenaUnos.Text;
                Alergeni.Instance.Serijalizacija();
                Alergeni.Instance.Deserijalizacija();
                ListaAlergena.ItemsSource = Alergeni.Instance.listaAlergena;

            }

        }

        public void PregledAlergena(ListView ListaAlergena)
        {
            if (ListaAlergena.SelectedIndex >= 0)
            {
                PregledAlergena pregledAlergena = new PregledAlergena(ListaAlergena);
                Alergen alergen = (Alergen)ListaAlergena.SelectedItem;
                pregledAlergena.labelaAlergen.Content = alergen.nazivAlergena;
                pregledAlergena.Show();

            }
        }

        public Repozitorijum.Alergeni alergeni;

    }
}