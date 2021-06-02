using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using InformacioniSistemBolnice.Views.UpravnikView;

namespace InformacioniSistemBolnice.ViewModels.UpavnikViewModel
{
    class LogovanjeViewModel
    {
        public ICommand UlogujSe { get; set; }
        public LogovanjeProzor Prozor { get; set; }

        public LogovanjeViewModel()
        {
            //Prozor = prozor;
            UlogujSe = new Command(o => Logovanje());
        }

        public void Logovanje()
        {
            Prozor.labKorImeGreska.Visibility = Visibility.Hidden;
            Prozor.labLozGreska.Visibility = Visibility.Hidden;
            Prozor.labPraznoPolje.Visibility = Visibility.Hidden;
            if (!Prozor.tbKorisnickoIme.Text.Equals("upravnik") && !Prozor.tbKorisnickoIme.Text.Equals(""))
            {
                Prozor.labKorImeGreska.Visibility = Visibility.Visible;
                return;
            }
            if (!Prozor.tbLozinka.Password.Equals("ftn") && !Prozor.tbLozinka.Password.Equals(""))
            {
                Prozor.labLozGreska.Visibility = Visibility.Visible;
                return;
            }
            if (Prozor.tbKorisnickoIme.Text.Equals("") || Prozor.tbLozinka.Password.Equals(""))
            {
                Prozor.labPraznoPolje.Visibility = Visibility.Visible;
                return;
            }
            Prozor.NavigationService.Navigate(new MeniProzor());
        }
    }
}
