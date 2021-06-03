using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformacioniSistemBolnice.ViewModels.PacijentViewModel
{
    public class ProfilViewModel
    {
        public string Title { get; } = "Profil";

        public Model.Pacijent UlogovanPacijent { get; set; } = GlavniProzorPacijentaView.ulogovanPacijent;

        public ProfilViewModel()
        {
            
        }
    }

}
