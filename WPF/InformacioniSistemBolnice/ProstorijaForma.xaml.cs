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
using Model;
using Logika;
using RadSaDatotekama;

namespace InformacioniSistemBolnice
{
    /// <summary>
    /// Interaction logic for ProstorijaForma.xaml
    /// </summary>
    public partial class ProstorijaForma : Window
    {
        public ProstorijaForma()
        {
            InitializeComponent();
        }

        private void potvrdiDugme_Click(object sender, RoutedEventArgs e)
        {

            UpravljanjeProstorijama.Instance.KreiranjeProstorije(this);
            this.Close();

        }
    }
}
