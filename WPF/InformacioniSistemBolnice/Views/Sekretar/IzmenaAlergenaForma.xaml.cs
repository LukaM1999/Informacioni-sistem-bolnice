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
       
        public IzmenaAlergenaForma(AlergeniProzor alergeniProzor)
        {
            InitializeComponent();
            alergeni = alergeniProzor;
            pocetna = alergeniProzor.pocetna;
            listaAlergena = alergeniProzor.ListaAlergena;
        }

        private void PotvrdiIzmene_Click(object sender, RoutedEventArgs e)
        {
            if (listaAlergena.SelectedValue != null)
            {
                SekretarKontroler.Instance.IzmenaAlergena
                    (new AlergenDto(nazivAlergenaUnos.Text), (Alergen)listaAlergena.SelectedItem);
                AzurirajIzgled();
            }
        }

        private void AzurirajIzgled()
        {
            pocetna.Content = new AlergeniProzor(pocetna);
            alergeni.ListaAlergena.ItemsSource = AlergenRepo.Instance.Alergeni;
        }

        private void NazadBtn_Click(object sender, RoutedEventArgs e)
        {
            this.pocetna.contentControl.Content = new AlergeniProzor(this.pocetna);
        }
    }
}
