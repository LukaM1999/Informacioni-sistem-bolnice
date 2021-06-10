using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using InformacioniSistemBolnice.Views.PacijentView;
using Model;

namespace InformacioniSistemBolnice.ViewModels.PacijentViewModel
{
    public class ProfilViewModel
    {
        public Model.Pacijent UlogovanPacijent { get; set; } = GlavniProzorPacijentaView.ulogovanPacijent;

        public string Adresa { get; set; } = GlavniProzorPacijentaView.ulogovanPacijent.AdresaStanovanja.ToString();
        public string Title { get; } = "Profil";
        public ICommand OtvaranjeAnamneze { get; set; }
        public string[] Gradovi { get; set; }

        public ProfilViewModel()
        {
            Gradovi = new[] {"Novi Sad", "Beograd", "Subotica"};
            OtvaranjeAnamneze = new Command(o => OtvoriAnamnezu());
        }

        private void OtvoriAnamnezu()
        {
            new AnamnezaView().ShowDialog();
        }
    }

}
