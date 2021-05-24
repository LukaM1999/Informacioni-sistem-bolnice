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
using Repozitorijum;
using Model;
using Kontroler;


namespace InformacioniSistemBolnice
{
    /// <summary>
    /// Interaction logic for GostujuciNaloziProzor.xaml
    /// </summary>
    public partial class GostujuciNaloziProzor : UserControl
    {
        public PocetnaStranicaSekretara pocetna;
        public ObservableCollection<Pacijent> gostujuciNalozi = new ObservableCollection<Pacijent>();
        public GostujuciNaloziProzor(PocetnaStranicaSekretara pocetnaStranicaSekretara)
        {
            InitializeComponent();
            PacijentRepo.Instance.Deserijalizacija();
            pocetna = pocetnaStranicaSekretara;
            
            foreach (Pacijent gostujuciPacijent in PacijentRepo.Instance.Pacijenti)
            {
                if (gostujuciPacijent.Korisnik.KorisnickoIme == null)
                {
                    gostujuciNalozi.Add(gostujuciPacijent);
                   
                }
            }


            listaGostujucihNaloga.ItemsSource = gostujuciNalozi.ToList();
        }

        private void ukloniGostujuciNalog(object sender, RoutedEventArgs e)
        {
            SekretarKontroler.Instance.UklanjanjeGostujucegNaloga(this.listaGostujucihNaloga);
        }

        private void kreirajGostujucegPacijenta_click(object sender, RoutedEventArgs e)
        {
            this.pocetna.contentControl.Content = new KreiranjeGostujucegPacijentaProzor(this);
           
        }

        private void InfoBtn_Click(object sender, RoutedEventArgs e)
        {
            if (listaGostujucihNaloga.SelectedValue != null)
            {
                this.pocetna.contentControl.Content = new PregledGostujucegNaloga(this, this.listaGostujucihNaloga);

            } else
            {
                MessageBox.Show("Morate selektovati pacijenta!");
                this.pocetna.contentControl.Content = this;
            }
        }
    }

}




