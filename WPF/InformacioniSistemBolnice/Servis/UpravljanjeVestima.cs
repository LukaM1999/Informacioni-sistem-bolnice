using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Model;
using Repozitorijum;

namespace InformacioniSistemBolnice.Servis
{
    class UpravljanjeVestima
    {
        private static readonly Lazy<UpravljanjeVestima>
          lazy =
          new Lazy<UpravljanjeVestima>
              (() => new UpravljanjeVestima());

        public static UpravljanjeVestima Instance { get { return lazy.Value; } }

        public void KreiranjeVesti(KreirajVijestProzor kreirajVijestProzor)
        {
            Vest vest = new Vest(kreirajVijestProzor.sadrzajVesti.Text, kreirajVijestProzor.idVesti.Text);
            Vesti.Instance.listaVesti.Add(vest);
            Vesti.Instance.Serijalizacija();
            Vesti.Instance.Deserijalizacija();

        }

        public void PregledVesti(ListView listaVesti)
        {
            if (listaVesti.SelectedIndex >= 0)
            {
               PregledVesti pregledVesti = new PregledVesti(listaVesti);
               Vest vest = (Vest)listaVesti.SelectedItem;
                pregledVesti.labelaSadrzajVesti.Content = vest.Sadrzaj;
                pregledVesti.labelaNaslovVesti.Content = vest.Id;
                pregledVesti.Show();

            }
        }

        public void UklanjanjeVesti(ListView listaVesti)
        {
            if (listaVesti.SelectedValue != null)
            {
                Vest vest = (Vest)listaVesti.SelectedValue;

                Vesti vesti = Vesti.Instance;
                foreach (Vest v in vesti.listaVesti)
                {
                    if (v.Id.Equals(vest.Id))
                    {
                        vesti.listaVesti.Remove(v);
                        Vesti.Instance.Serijalizacija();
                        Vesti.Instance.Deserijalizacija();
                        listaVesti.ItemsSource = Vesti.Instance.listaVesti;

                        break;
                    }
                }
            }
        }


        public void IzmenaVesti(ListView listaVesti, IzmenaVesti izmenaVesti)
        {

            if (listaVesti.SelectedValue != null)
            {
                Vest vest = (Vest)listaVesti.SelectedValue;
                vest.Id = izmenaVesti.naslovVesti.Text;
                vest.Sadrzaj = izmenaVesti.sadrzajVesti.Text;

                Vesti.Instance.Serijalizacija();
                Vesti.Instance.Deserijalizacija();
                listaVesti.ItemsSource = Vesti.Instance.listaVesti;

            }

        }


    }
}
