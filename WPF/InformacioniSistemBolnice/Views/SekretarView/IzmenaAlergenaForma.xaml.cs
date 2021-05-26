using System.Windows;
using System.Windows.Controls;
using InformacioniSistemBolnice.DTO;
using Kontroler;
using Model;
using Repozitorijum;

namespace InformacioniSistemBolnice
{
    public partial class IzmenaAlergenaForma : UserControl
    {
        public ListView listaAlergena;
        public PocetnaStranicaSekretara pocetna;
        public AlergeniProzor alergeni;
        public Alergen Alergen { get; set; }

        public IzmenaAlergenaForma(AlergeniProzor alergeniProzor, Alergen izabraniAlergen)
        {
            Alergen = izabraniAlergen;
            InitializeComponent();
            alergeni = alergeniProzor;
            pocetna = alergeniProzor.pocetna;
            listaAlergena = alergeniProzor.ListaAlergena;
        }

        private void PotvrdiIzmene_Click(object sender, RoutedEventArgs e)
        {
            SekretarKontroler.Instance.IzmenaAlergena(new AlergenDto(Alergen.Naziv), (Alergen)listaAlergena.SelectedItem);
            pocetna.Content = new AlergeniProzor(pocetna);
        }


        private void NazadBtn_Click(object sender, RoutedEventArgs e)
        {
            this.pocetna.contentControl.Content = new AlergeniProzor(this.pocetna);
        }
    }
}
