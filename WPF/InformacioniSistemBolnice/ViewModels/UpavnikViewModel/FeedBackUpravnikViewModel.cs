using InformacioniSistemBolnice.DTO;
using InformacioniSistemBolnice.Views.UpravnikView;
using Kontroler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace InformacioniSistemBolnice.ViewModels.UpavnikViewModel
{
    public class FeedBackUpravnikViewModel
    {
        public ICommand Potvrdi { get; set; }
        public ICommand Nazad { get; set; }
        public string Komentar { get; set; }
        public FeedbackUpravnik Strana { get; set; }

        public FeedBackUpravnikViewModel(FeedbackUpravnik strana)
        {
            Strana = strana;
            Potvrdi = new Command(o => PotvrdiFeedback());
            Nazad = new Command(o => VratiSe());
        }

        public void PotvrdiFeedback()
        {
            if(!string.IsNullOrWhiteSpace(Komentar))
            {
                FeedbackKontroler.Instance.PosaljiFeedback(new FeedbackDto("upravnik1",
                    "upravnik", Komentar));
                Strana.NavigationService.Navigate(new MeniProzor());
            }
        }

        public void VratiSe()
        {
            Strana.NavigationService.Navigate(new MeniProzor());
        }
    }
}
