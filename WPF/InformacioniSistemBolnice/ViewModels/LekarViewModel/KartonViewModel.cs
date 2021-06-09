using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using InformacioniSistemBolnice.Views.LekarView;
using Model;
using Repozitorijum;

namespace InformacioniSistemBolnice.ViewModels.LekarViewModel
{
    public class KartonViewModel
    {
        public GlavniProzorLekara pocetna;
        public ZdravstveniKarton ZdravstveniKarton { get; set; }
        public Model.Pacijent Pacijent { get; set; }
        public ICommand Uput { get; set; }
        public ICommand Recept { get; set; }
        public ICommand Recepti { get; set; }
        public ICommand BolnickoLecenje { get; set; }
        public ICommand Anamneza { get; set; }

        public KartonViewModel(GlavniProzorLekara glavni, string jmbg)
        {
            Pacijent = PacijentRepo.Instance.NadjiPoJmbg(jmbg);
            ZdravstveniKarton = Pacijent.zdravstveniKarton;
            pocetna = glavni;
            Uput = new Command(o => IzdajUput());
            Recept = new Command(o => PropisiRecept());
            Recepti = new Command(o => PrikaziRecepte());
            Anamneza = new Command(o => PopuniAnamnezu());
            BolnickoLecenje = new Command(o => PosaljiNaBolnicko());
        }

        private void PopuniAnamnezu()
        {
            LekarAnamnezaView anamneza = new LekarAnamnezaView() {DataContext = new LekarAnamnezaViewModel(Pacijent, pocetna) };
            pocetna.contentControl.Content = anamneza;
        }

        private void PropisiRecept()
        {
            ReceptView recept = new ReceptView(Pacijent, pocetna);
            pocetna.contentControl.Content = recept;
        }

        private void PrikaziRecepte()
        {
            UCListaRecepata recepti = new UCListaRecepata(Pacijent, pocetna);
            pocetna.contentControl.Content = recepti;
        }

        private void IzdajUput()
        {
            UputView uput = new(Pacijent, pocetna);
            pocetna.contentControl.Content = uput;
        }

        private void PosaljiNaBolnicko()
        {
            UCBolnickoLecenjeForma forma = new(Pacijent, pocetna, this);
            pocetna.contentControl.Content = forma;
        }

    }
}
