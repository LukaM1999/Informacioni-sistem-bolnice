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
    public partial class GostujuciNaloziProzor : Window
    {
        
        public GostujuciNaloziProzor()
        {
            InitializeComponent();
            Pacijenti.Instance.Deserijalizacija();
            ObservableCollection<Pacijent> gostujuciNalozi = new ObservableCollection<Pacijent>();
            foreach (Pacijent gostujuciPacijent in Pacijenti.Instance.listaPacijenata)
            {
                if (gostujuciPacijent.korisnik.korisnickoIme == null)
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
    }


}




