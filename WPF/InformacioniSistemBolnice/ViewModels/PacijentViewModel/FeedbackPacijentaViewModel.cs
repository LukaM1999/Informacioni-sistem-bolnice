using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using InformacioniSistemBolnice.DTO;
using InformacioniSistemBolnice.Utilities;
using Kontroler;
using PropertyChanged;

namespace InformacioniSistemBolnice.ViewModels.PacijentViewModel
{
    [AddINotifyPropertyChangedInterface]
    public class FeedbackPacijentaViewModel
    {
        public ICommand SlanjeFeedbacka { get; set; }
        public string Poruka { get; set; } = "";

        public FeedbackPacijentaViewModel()
        {
            SlanjeFeedbacka = new Command(o => PosaljiFeedback(), o => Poruka is not "");
        }

        public void PosaljiFeedback()
        {
            Model.Pacijent ulogovan = GlavniProzorPacijentaView.ulogovanPacijent;
            FeedbackKontroler.Instance.PosaljiFeedback(new FeedbackDto(ulogovan.ToString(),
                ulogovan.Korisnik.Uloga.ToString(), Poruka));
            Window prozor = PronadjiProzorUtility.PronadjiProzor(this);
            prozor.Close();
        }
    }
}
