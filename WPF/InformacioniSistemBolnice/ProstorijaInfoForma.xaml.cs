using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Model;
using Repozitorijum;
using Kontroler;

namespace InformacioniSistemBolnice
{
    public partial class ProstorijaInfoForma : Window
    {
        private ProstorijeProzor prostorProzor;
        private Prostorija prost;
        public ProstorijaInfoForma()
        {
            InitializeComponent();
        }

        public ProstorijaInfoForma(ProstorijeProzor p)
        {
            Prostorija pr = (Prostorija)p.ListaProstorija.SelectedValue;
            prostorProzor = p;
            prost = pr;
            InitializeComponent();
            labId2.Content = pr.id;
            labSprat2.Content = pr.sprat;
            labTip2.Content = pr.tip;
            if (pr.jeZauzeta)
            {
                labZauzetost.Content = "Prostorija je zauzeta";
            }
            else
            {
                labZauzetost.Content = "Prostorija nije zauzeta";
            }
            
            listaProstorija.ItemsSource = Prostorije.Instance.listaProstorija;
            listaDinamicke.ItemsSource = pr.inventar.dinamickaOprema;
            

            

        }

        private void dugmeRaspodeliDinamicku_Click(object sender, RoutedEventArgs e)
        {
            int kolicina = 0;
            if (rbMagacin.IsPressed)
            {
                Model.DinamickaOprema dinamickaOprema = (Model.DinamickaOprema)listaDinamicke.SelectedValue;
                Prostorija izProstorije = (Model.Prostorija)listaProstorija.SelectedValue;
                kolicina = Int32.Parse(tbKolicinaDinamicka.Text);

                //Prostorija uProstoriju = (Prostorija)listaProstorija.SelectedValue;
                //UpravnikKontroler.Instance.RasporedjivanjeDinamickeOpreme(prost, null, oprema, Int32.Parse(tbKolicinaDinamicka.Text));
                foreach (Model.DinamickaOprema d in izProstorije.inventar.dinamickaOprema)
                {
                    if (d.tip.Equals(dinamickaOprema.tip))
                    {
                        izProstorije.inventar.dinamickaOprema.ElementAt(izProstorije.inventar.dinamickaOprema.IndexOf(d)).kolicina -= kolicina;
                        foreach (Model.DinamickaOprema d2 in Repozitorijum.DinamickaOprema.Instance.listaOpreme)
                        {
                            if (d2.tip.Equals(izProstorije.inventar.dinamickaOprema.ElementAt(izProstorije.inventar.dinamickaOprema.IndexOf(d)).tip))
                            {
                                Repozitorijum.DinamickaOprema.Instance.listaOpreme.ElementAt(Repozitorijum.DinamickaOprema.Instance.listaOpreme.IndexOf(d2)).kolicina
                                    += kolicina;
                                Repozitorijum.DinamickaOprema.Instance.Serijalizacija("../../../json/dinamickaOprema.json");
                                Repozitorijum.DinamickaOprema.Instance.Deserijalizacija("../../../json/dinamickaOprema.json");
                                //Prostorije.Instance.Serijalizacija("../../../json/prostorije.json");
                                //Prostorije.Instance.Deserijalizacija("../../../json/prostorije.json");
                                return;
                            }
                        }
                        //Repozitorijum.DinamickaOprema.Instance.listaOpreme.Add(new Model.DinamickaOprema(kolicina, d.tip));
                        //Repozitorijum.DinamickaOprema.Instance.Serijalizacija("../../../json/dinamickaOprema.json");
                        //Repozitorijum.DinamickaOprema.Instance.Deserijalizacija("../../../json/dinamickaOprema.json");
                        return;
                    }
                }

            }
        }
    }
}
