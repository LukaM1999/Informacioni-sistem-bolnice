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
    public partial class ZdravstveniKartonForma : UserControl
    {
        public ListView listaPacijenata;
        public PocetnaStranicaSekretara pocetna;
        public IzmenaNalogaPacijentaForma izmenaNalogaPacijentaForma;
       
        
        public ZdravstveniKartonForma(ListView pacijenti, PocetnaStranicaSekretara pocetnaStranicaSekretara, IzmenaNalogaPacijentaForma izmenaNalogaPacijentaForma)
        {
            InitializeComponent();
            listaPacijenata = pacijenti;
            Alergeni.Instance.Deserijalizacija();
            ListaAlergena.ItemsSource = Alergeni.Instance.listaAlergena;
            pocetna = pocetnaStranicaSekretara;
            this.izmenaNalogaPacijentaForma = izmenaNalogaPacijentaForma;
          

        }

        private void kreirajZdravstveniKarton_Click(object sender, RoutedEventArgs e)
        {
            Pacijenti.Instance.Deserijalizacija();

            //UpravljanjeNalozimaPacijenata.Instance.KreirajZdravstveniKarton(this, ListaPacijenata);
            SekretarKontroler.Instance.KreiranjeZdravstvenogKartona(this);
            pocetna.contentControl.Content = izmenaNalogaPacijentaForma;
            
        }
    }
}
