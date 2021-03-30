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
using RadSaDatotekama;

namespace InformacioniSistemBolnice
{
    /// <summary>
    /// Interaction logic for PomeranjeTerminaLekaraProzor.xaml
    /// </summary>
    public partial class PomeranjeTerminaLekaraProzor : Window
    {
        public Termin zakazanTermin;
        public List<string> listaDatuma = new List<string>();
        public ListView zakazaniTermini;
        public List<string> prostorijeID = new List<string>();
        public PomeranjeTerminaLekaraProzor(ListView zakazaniTermini)

        {
            InitializeComponent();
            DateTime datum = DateTime.Parse("7:00");

            for (int j = 0; j < 27; j++)
            {
                listaDatuma.Add(datum.ToString("HH:mm"));
                datum = datum.AddMinutes(30);
            }
            listaSati.ItemsSource = listaDatuma;
            this.zakazaniTermini = zakazaniTermini;
            zakazanTermin = (Termin)zakazaniTermini.SelectedItem;

            trajanjeTerminaUnos.Text = zakazanTermin.trajanje.ToString();
            tipTerminaUnos.Text = zakazanTermin.tipTermina.ToString();

            Prostorije.Instance.Deserijalizacija("../../../json/prostorije.json");
            listaSati.ItemsSource = listaDatuma;
            
            foreach (Prostorija p in Prostorije.Instance.listaProstorija)
            {
                prostorijeID.Add(p.id);
            }
            sala.ItemsSource = prostorijeID;

            if (zakazanTermin.prostorija != null)
            {
                sala.Text = zakazanTermin.prostorija.id;
            }

            datumTermina.Text = zakazanTermin.vreme.ToString("MM/dd/yyyy");

            string sat = zakazanTermin.vreme.ToString("HH:mm");
            for (int i = 0; i < listaDatuma.Count; i++)
            {
                if (sat.Equals(listaDatuma[i]))
                {
                    listaSati.SelectedIndex = i;
                }
            }
      
        }

        private void potvrdaPomeranjaDugme_Click(object sender, RoutedEventArgs e)
        {
            if (listaSati.SelectedIndex >= 0 && datumTermina.Text != null && tipTerminaUnos.SelectedIndex >= 0)
            {
                DateTime datumTermina = (DateTime)this.datumTermina.SelectedDate;
                string datumVrednost = (string)listaSati.SelectedValue;
                string[] satiMinuti = datumVrednost.Split(":");
                double sat = double.Parse(satiMinuti[0]);
                if (satiMinuti[1].Equals("30"))
                {
                    sat += 0.5;
                }

                datumTermina = datumTermina.AddHours(sat);

                if (!datumTermina.Equals(zakazanTermin.vreme))
                {
                    zakazanTermin.status = StatusTermina.pomeren;
                }
                
                zakazanTermin.vreme = datumTermina;
                zakazanTermin.trajanje = double.Parse(trajanjeTerminaUnos.Text);
                zakazanTermin.tipTermina = (TipTermina)Enum.Parse(typeof(TipTermina), tipTerminaUnos.Text);
                foreach (Prostorija p in Prostorije.Instance.listaProstorija)
                {
                    if (p.id.Equals((string)sala.SelectedItem))
                    {
                        zakazanTermin.prostorija = p;
                        break;
                    }
                }


                Termini.Instance.Serijalizacija("../../../json/zakazaniTermini.json");
                Termini.Instance.Deserijalizacija("../../../json/zakazaniTermini.json");
           
                zakazaniTermini.ItemsSource = Termini.Instance.listaTermina;
                this.Close();

            }
        }
    }
}
