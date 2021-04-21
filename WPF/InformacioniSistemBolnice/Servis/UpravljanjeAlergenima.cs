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
            Alergeni.Instance.Serijalizacija("../../../json/alergeni.json");
           
        }

        public void UklanjanjeAlergena(DataGrid ListaAlergena)
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
                        Alergeni.Instance.Serijalizacija("../../../json/alergeni.json");

                        break;
                    }
                }
            }



        }

        public void IzmenaAlergena(DataGrid ListaAlergena, IzmenaAlergenaForma izmenaAlergenaForma)
        {
            
            if (ListaAlergena.SelectedValue != null)
            {
                Alergen alergen = (Alergen)ListaAlergena.SelectedValue;
                alergen.nazivAlergena = izmenaAlergenaForma.nazivAlergenaUnos.Text;
                Alergeni.Instance.Serijalizacija("../../../json/alergeni.json");
                Alergeni.Instance.Deserijalizacija("../../../json/alergeni.json");
                ListaAlergena.ItemsSource = Alergeni.Instance.listaAlergena;

            }

        }

        public void PregledAlergena(DataGrid ListaAlergena)
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