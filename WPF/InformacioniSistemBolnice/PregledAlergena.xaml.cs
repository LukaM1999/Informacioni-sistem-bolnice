using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace InformacioniSistemBolnice
{
    /// <summary>
    /// Interaction logic for PregledAlergena.xaml
    /// </summary>
    public partial class PregledAlergena : Window
    {
        public ListView listaAlergena;
        public PregledAlergena()
        {
            InitializeComponent();
        }

        public PregledAlergena(ListView ListaAlergena)
        {
            InitializeComponent();
            listaAlergena = ListaAlergena;
        }
    }
}
