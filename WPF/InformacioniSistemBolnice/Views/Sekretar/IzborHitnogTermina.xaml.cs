using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace InformacioniSistemBolnice
{
    public partial class IzborHitnogTermina : Window
    {
        public ZakazivanjeHitnogTermina zakazivanjeHitnogTermina;
        private ObservableCollection<Termin> slobodniTermini = new ObservableCollection<Termin>();
        public IzborHitnogTermina(ZakazivanjeHitnogTermina zakazivanje)
        {
            zakazivanjeHitnogTermina = zakazivanje;
     
            DateTime slobodanTermin = DateTime.Today;
            
            int sat = DateTime.Now.Hour;
            
            int minuta = DateTime.Now.Minute;
            
            if (minuta > 30 || minuta == 30)
            {
                slobodanTermin = slobodanTermin.AddHours(sat+1);
            }
            else
            {
                slobodanTermin = slobodanTermin.AddHours(sat);
                slobodanTermin = slobodanTermin.AddMinutes(30);
            }

            DateTime pomocni = slobodanTermin;

            Lekari.Instance.Deserijalizacija();
            
            foreach (Lekar lekar in Lekari.Instance.listaLekara)
            {
                Pacijent p = (Pacijent)zakazivanje.pacijenti.SelectedItem;
                    if (lekar.specijalizacija == zakazivanjeHitnogTermina.specijalizacijeLekara.SelectedItem.ToString())
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            Termin t = new Termin(slobodanTermin, 30.0, TipTermina.pregled, StatusTermina.slobodan,
                                                           p.jmbg, lekar.jmbg, "P1", true);
                            slobodniTermini.Add(t);
                            //System.Diagnostics.Debug.WriteLine(t);
                            slobodanTermin = slobodanTermin.AddMinutes(30);
                        }
                        slobodanTermin = pomocni;

                        //System.Diagnostics.Debug.WriteLine(lekar)
                        //int broj = slobodniTermini.Count();
                        //System.Diagnostics.Debug.WriteLine("UKUPNO: " + broj + " za ljekara " + lekar.ime);
                        //Pacijenti.Instance.Deserijalizacija();
                        //ponudjeniiTermini.ItemsSource = Pacijenti.Instance.listaPacijenata;
                    }
                    foreach (Termin predlozenTermin in slobodniTermini.ToList())
                    {
                        //System.Diagnostics.Debug.WriteLine("PREDLOZENI:" + predlozenTermin.vreme + "\n");
                        foreach (Termin postojeciTermin in lekar.zauzetiTermini)
                        {
                            if (predlozenTermin.vreme.Equals(postojeciTermin.vreme))
                            {
                                slobodniTermini.Remove(predlozenTermin);
                                break;
                            }

                            //System.Diagnostics.Debug.WriteLine("PREDLOZENI:" + predlozenTermin.vreme + "\n");
                            // System.Diagnostics.Debug.WriteLine("POSTOJECI:" + postojeciTermin.vreme + "\n");

                        }
                    }

                }

                Termin najblizi = slobodniTermini.First();
                foreach (Termin t in slobodniTermini)
                {
                    System.Diagnostics.Debug.WriteLine(t.ToString() + "\n");

                    if (t.vreme < najblizi.vreme)
                    {
                        najblizi = t;
                    }


                }


                Termini.Instance.Deserijalizacija();
                najblizi.status = StatusTermina.zakazan;
                Termini.Instance.listaTermina.Add(najblizi);
                Termini.Instance.Serijalizacija();
                Termini.Instance.Deserijalizacija();
            foreach (Pacijent pacijent in Pacijenti.Instance.listaPacijenata)
            {
                if (pacijent.jmbg == najblizi.pacijentJMBG)
                {
                    pacijent.zakazaniTermini.Add(najblizi);
                    Pacijenti.Instance.Serijalizacija();
                    Pacijenti.Instance.Deserijalizacija();
                }
            }

                string l = najblizi.lekarJMBG;

                foreach (Lekar lekar in Lekari.Instance.listaLekara)
                {
                    if (lekar.jmbg == l)
                    {
                        lekar.zauzetiTermini.Add(najblizi);
                        Lekari.Instance.Serijalizacija();
                        Pacijenti.Instance.Deserijalizacija();
                    }
                }
            }
            
        

        private void zakaziDugme_Click(object sender, RoutedEventArgs e)
        {

        }
    }

       

 }


    
          

        

    

