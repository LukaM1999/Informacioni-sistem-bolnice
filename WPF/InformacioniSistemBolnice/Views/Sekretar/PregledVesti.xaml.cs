using System.Windows;
using System.Windows.Controls;
using Model;

namespace InformacioniSistemBolnice
{
    public partial class PregledVesti : Window
    {
        public ListView vesti;
        public Vest Vest { get; set; }
        public PregledVesti(ListView vesti, Vest izabranaVest)
        {
            Vest = izabranaVest;
            InitializeComponent();
            this.vesti = vesti;
        }
    }
}
