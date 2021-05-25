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

            LekarRepo.Instance.Deserijalizacija();
            
            foreach (Lekar lekar in LekarRepo.Instance.Lekari)
            {
                Pacijent p = (Pacijent)zakazivanje.pacijenti.SelectedItem;
                    if (lekar.Specijalizacija.Naziv == zakazivanjeHitnogTermina.specijalizacijeLekara.SelectedItem.ToString())
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            Termin t = new Termin(slobodanTermin, 30.0, TipTermina.pregled, StatusTermina.slobodan,
                                                           p.Jmbg, lekar.Jmbg, "P1", true);
                            slobodniTermini.Add(t);
                            //System.Diagnostics.Debug.WriteLine(t);
                            slobodanTermin = slobodanTermin.AddMinutes(30);
                        }
                        slobodanTermin = pomocni;

                        //System.Diagnostics.Debug.WriteLine(lekar)
                        //int broj = slobodniTermini.Count();
                        //System.Diagnostics.Debug.WriteLine("UKUPNO: " + broj + " za ljekara " + lekar.Ime);
                        //Pacijenti.Instance.Deserijalizacija();
                        //ponudjeniiTermini.ItemsSource = Pacijenti.Instance.ListaPacijenata;
                    }
                    foreach (Termin predlozenTermin in slobodniTermini.ToList())
                    {
                        //System.Diagnostics.Debug.WriteLine("PREDLOZENI:" + predlozenTermin.Vreme + "\n");
                        foreach (Termin postojeciTermin in lekar.ZakazaniTermini)
                        {
                            if (predlozenTermin.Vreme.Equals(postojeciTermin.Vreme))
                            {
                                slobodniTermini.Remove(predlozenTermin);
                                break;
                            }

                            //System.Diagnostics.Debug.WriteLine("PREDLOZENI:" + predlozenTermin.Vreme + "\n");
                            // System.Diagnostics.Debug.WriteLine("POSTOJECI:" + postojeciTermin.Vreme + "\n");

                        }
                    }

                }

                Termin najblizi = slobodniTermini.First();
                foreach (Termin t in slobodniTermini)
                {
                    System.Diagnostics.Debug.WriteLine(t.ToString() + "\n");

                    if (t.Vreme < najblizi.Vreme)
                    {
                        najblizi = t;
                    }


                }


                TerminRepo.Instance.Deserijalizacija();
                najblizi.Status = StatusTermina.zakazan;
                TerminRepo.Instance.Termini.Add(najblizi);
                TerminRepo.Instance.Serijalizacija();
                TerminRepo.Instance.Deserijalizacija();
            foreach (Pacijent pacijent in PacijentRepo.Instance.Pacijenti)
            {
                if (pacijent.Jmbg == najblizi.PacijentJmbg)
                {
                    pacijent.ZakazaniTermini.Add(najblizi);
                    PacijentRepo.Instance.Serijalizacija();
                    PacijentRepo.Instance.Deserijalizacija();
                }
            }

                string l = najblizi.LekarJmbg;

                foreach (Lekar lekar in LekarRepo.Instance.Lekari)
                {
                    if (lekar.Jmbg == l)
                    {
                        lekar.ZakazaniTermini.Add(najblizi);
                        LekarRepo.Instance.Serijalizacija();
                        PacijentRepo.Instance.Deserijalizacija();
                    }
                }
            }
            
        

        private void zakaziDugme_Click(object sender, RoutedEventArgs e)
        {

        }
    }

       

 }


    
          

        

    

