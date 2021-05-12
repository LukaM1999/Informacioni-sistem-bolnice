using System.Windows;
using System.Windows.Controls;
using Kontroler;

namespace InformacioniSistemBolnice
{
    /// <summary>
    /// Interaction logic for IzmenaAlergenaForma.xaml
    /// </summary>
    public partial class IzmenaAlergenaForma : UserControl
    {
        public ListView listaAlergena;
        public PocetnaStranicaSekretara pocetna;
        public AlergeniProzor alergeni;
       
        public IzmenaAlergenaForma(AlergeniProzor alergeniProzor)
        {
            InitializeComponent();
            alergeni = alergeniProzor;
            pocetna = alergeniProzor.pocetna;
            listaAlergena = alergeniProzor.ListaAlergena;

        }

        private void potvrdiIzmeneDugme_Click(object sender, RoutedEventArgs e)
        {
            SekretarKontroler.Instance.IzmjenaAlergena(listaAlergena, this);
            pocetna.contentControl.Content = alergeni.Content;
        }

        private void NazadBtn_Click(object sender, RoutedEventArgs e)
        {
            this.pocetna.contentControl.Content = new AlergeniProzor(this.pocetna);
        }
    }
}
