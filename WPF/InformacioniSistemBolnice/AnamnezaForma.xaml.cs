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
            if (pacijent.zdravstveniKarton.anamneza != null)
            {
                this.prvi.Text = pacijent.zdravstveniKarton.anamneza.sadasnjaBolest;
                this.drugi.Text = pacijent.zdravstveniKarton.anamneza.ranijeBolesti;
                this.treci.Text = pacijent.zdravstveniKarton.anamneza.porodicneAnamneze;
                this.peti.Text = pacijent.zdravstveniKarton.anamneza.zakljucak;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LekarKontroler.Instance.DodavanjeAnamneze(this);
            this.Close();
        }
    }
}
