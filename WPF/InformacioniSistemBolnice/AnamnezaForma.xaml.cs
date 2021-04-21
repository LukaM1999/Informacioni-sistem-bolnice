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
using Kontroler;

namespace InformacioniSistemBolnice
{
    /// <summary>
    /// Interaction logic for AnamnezaForma.xaml
    /// </summary>
    public partial class AnamnezaForma : Window
    {
        public Pacijent p;
        public AnamnezaForma(Pacijent pacijent)
        {
            InitializeComponent();
            p = pacijent;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LekarKontroler.Instance.DodavanjeAnamneze(this);
            this.Close();
        }
    }
}
