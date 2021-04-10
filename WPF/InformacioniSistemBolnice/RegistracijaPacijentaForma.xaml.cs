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
using Repozitorijum;
using Servis;
using Kontroler;

namespace InformacioniSistemBolnice
{
    public partial class RegistracijaPacijentaForma : Window
    {
        public RegistracijaPacijentaForma()
        {
            InitializeComponent();
        }

        private void potvrdiDugme_Click(object sender, RoutedEventArgs e)
        {
            SekretarKontroler.Instance.KreiranjeNaloga(this);
            this.Close();
        }

    }
}
