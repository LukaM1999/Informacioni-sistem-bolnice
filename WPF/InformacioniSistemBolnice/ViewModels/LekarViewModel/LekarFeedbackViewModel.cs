using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using InformacioniSistemBolnice.DTO;
using Kontroler;
using PropertyChanged;

namespace InformacioniSistemBolnice.ViewModels.LekarViewModel
{
    [AddINotifyPropertyChangedInterface]
    class LekarFeedbackViewModel
    {
        private GlavniProzorLekara pocetni { get; set; }
        public ICommand SlanjeFeedbacka { get; set; }
        public ICommand Nazad { get; set; }
        public string Poruka { get; set; }

        public LekarFeedbackViewModel(GlavniProzorLekara glavni)
        {
            pocetni = glavni;
            SlanjeFeedbacka = new Command(PosaljiFeedback);
            Nazad = new Command(o => PovratakNazad());
        }
        public void PosaljiFeedback(object o)
        {
            FeedbackKontroler.Instance.PosaljiFeedback(new FeedbackDto(pocetni.ulogovanLekar.ToString(), 
                pocetni.ulogovanLekar.Korisnik.Uloga.ToString(), Poruka));
            PovratakNazad();
        }

        public void PovratakNazad()
        {
            pocetni.contentControl.Content = new Podesavanja(pocetni);
        }
    }
}
