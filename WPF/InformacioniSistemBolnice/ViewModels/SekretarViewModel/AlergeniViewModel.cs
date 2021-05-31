using InformacioniSistemBolnice.Views.SekretarView;
using Kontroler;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repozitorijum;
using System.Windows.Controls;
using System.Windows.Input;

namespace InformacioniSistemBolnice.ViewModels.SekretarViewModel
{
    public class AlergeniViewModel
    {
        public PocetnaStranicaSekretara pocetna;
        public AlergeniProzor alergeni;
        public ICommand DefinisiAlergen { get; set; }
        public ICommand VidiAlergen { get; set; }
        public ICommand IzmeniAlergen { get; set; }
        public ICommand ObrisiAlergen { get; set; }

        public AlergeniViewModel(PocetnaStranicaSekretara pocetnaStranicaSekretara, AlergeniProzor alergeniProzor)
        {
            pocetna = pocetnaStranicaSekretara;
            alergeni = alergeniProzor;
            DefinisiAlergen = new Command(o => DefinisanjeAlergena());
            VidiAlergen = new Command(o => PregledAlergena());
            IzmeniAlergen = new Command(o => IzmenaAlergena());
            ObrisiAlergen = new Command(o => BrisanjeAlergena());
        }

        private void DefinisanjeAlergena()
        {
            pocetna.contentControl.Content = new DefinisanjeAlergenaForma()
            { DataContext = new DefinisanjeAlergenaViewModel(alergeni) };
        }

        private void PregledAlergena()
        {
            Alergen izabraniAlergen = (Alergen)alergeni.ListaAlergena.SelectedItem;
            if (izabraniAlergen is not null)
                pocetna.contentControl.Content = new PregledAlergena()
                { DataContext = new PregledAlergenaViewModel(alergeni, izabraniAlergen) };
        }
        private void IzmenaAlergena()
        {
            Alergen izabraniAlergen = (Alergen)alergeni.ListaAlergena.SelectedItem;
            if (izabraniAlergen is not null)
                pocetna.contentControl.Content = new IzmenaAlergenaForma()
                { DataContext = new IzmenaAlergenaViewModel(alergeni, izabraniAlergen) };
        }

        private void BrisanjeAlergena()
        {
            SekretarKontroler.Instance.UklanjanjeAlergena((Alergen)alergeni.ListaAlergena.SelectedValue);
            pocetna.contentControl.Content = new AlergeniProzor(pocetna);
        }

    }
}
