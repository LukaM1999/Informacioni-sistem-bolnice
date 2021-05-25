using System.Windows;
using System.Windows.Controls;

namespace InformacioniSistemBolnice
{
    public partial class PregledVesti : Window
    {
        public ListView vesti;

        public PregledVesti(ListView vesti)
        {
            InitializeComponent();
            this.vesti = vesti;
        }
    }
}
