using InformacioniSistemBolnice.DTO;
using Kontroler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InformacioniSistemBolnice.ViewModels.SekretarViewModel
{
    public class FeedbackViewModel
    {
        public ICommand SlanjeFeedbacka { get; set; }
        public ICommand Nazad { get; set; }
        public string Poruka { get; set; }

        public PocetnaStranicaSekretara pocetna;

        public UCMenuSekretara menu;

        public FeedbackViewModel(PocetnaStranicaSekretara pocetnaStranicaSekretara, UCMenuSekretara menuSekretara)
        {
            SlanjeFeedbacka = new Command(o => PosaljiFeedback());
            Nazad = new Command(o => PovratakNazad());
            pocetna = pocetnaStranicaSekretara;
            menu = menuSekretara;
        }

        public void PosaljiFeedback()
        {
            FeedbackKontroler.Instance.PosaljiFeedback(new FeedbackDto("sekretar1", "2".ToString(), Poruka));
            pocetna.contentControl.Content = menu.Content;
        }

        public void PovratakNazad() 
        {
            pocetna.contentControl.Content = menu.Content;
        }
    }
}
