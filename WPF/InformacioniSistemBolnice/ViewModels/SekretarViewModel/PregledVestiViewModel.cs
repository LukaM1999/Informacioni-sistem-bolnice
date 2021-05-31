using Model;
using System.Windows.Controls;

namespace InformacioniSistemBolnice.ViewModels.SekretarViewModel
{
    public class PregledVestiViewModel
    {
        public ListView vesti;
        public Vest Vest { get; set; }

        public PregledVestiViewModel(ListView vesti, Vest izabranaVest)
        {
            Vest = izabranaVest;
            this.vesti = vesti;
        }
    }
}
