using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using InformacioniSistemBolnice.DTO;
using InformacioniSistemBolnice.Views.PacijentView;
using Kontroler;

namespace InformacioniSistemBolnice.ViewModels.PacijentViewModel
{
    public class GlavniProzorPacijentaViewModel
    {
        public ICommand OtvaranjeFeedbacka { get; set; }

        public GlavniProzorPacijentaViewModel()
        {
            OtvaranjeFeedbacka = new Command(o => OtvoriFeedback());
        }

        public void OtvoriFeedback()
        {
            new FeedbackPacijentaView().ShowDialog();
        }
    }
}
