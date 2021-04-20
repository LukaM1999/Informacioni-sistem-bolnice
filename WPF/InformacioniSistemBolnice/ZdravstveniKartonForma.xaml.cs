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
using Kontroler;
using Repozitorijum;
using Servis;


namespace InformacioniSistemBolnice
{
    /// <summary>
    /// Interaction logic for ZdravstveniKartonForma.xaml
    /// </summary>
    public partial class ZdravstveniKartonForma : Window
    {
        public ListView ListaPacijenata;
       
       public ZdravstveniKartonForma()
        {
            InitializeComponent();
            
            Alergeni.Instance.Deserijalizacija("../../../json/alergeni.json");
            ListaAlergena.ItemsSource = Alergeni.Instance.listaAlergena;
        }
        
       
        public ZdravstveniKartonForma(ListView lp)
        {
            InitializeComponent();
            ListaPacijenata = lp;
            Alergeni.Instance.Deserijalizacija("../../../json/alergeni.json");
            ListaAlergena.ItemsSource = Alergeni.Instance.listaAlergena;
          

        }

        private void kreirajZdravstveniKarton_Click(object sender, RoutedEventArgs e)
        {
            Pacijenti.Instance.Deserijalizacija("../../../json/pacijenti.json");

            //UpravljanjeNalozimaPacijenata.Instance.KreirajZdravstveniKarton(this, ListaPacijenata);
            SekretarKontroler.Instance.KreiranjeZdravstvenogKartona(this);
            this.Close();
            
            

        }
    }
}
