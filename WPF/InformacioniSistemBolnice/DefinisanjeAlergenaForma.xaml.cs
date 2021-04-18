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
using Kontroler;

namespace InformacioniSistemBolnice
{
    /// <summary>
    /// Interaction logic for DefinisanjeAlergenaForma.xaml
    /// </summary>
    public partial class DefinisanjeAlergenaForma : Window
    {
        public DefinisanjeAlergenaForma()
        {
            InitializeComponent();
        }

        private void definisiAlergenDugme_Click(object sender, RoutedEventArgs e)
        {
            SekretarKontroler.Instance.DefinisanjeAlergena(this);
            this.Close();
        }
    }
}
