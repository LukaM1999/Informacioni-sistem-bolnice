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
using Repozitorijum;
using Model;

namespace InformacioniSistemBolnice
{
    /// <summary>
    /// Interaction logic for PomeranjeZakazanogTermina.xaml
    /// </summary>
    public partial class PomeranjeZakazanogTermina : Window
    {
        public PomeranjeZakazanogTermina(IzborTerminaZaPomeranje izborTerminaZaPomeranje)
        {
            InitializeComponent();
            Termini.Instance.Deserijalizacija();


            if(izborTerminaZaPomeranje.ponudjeniTerminiZaPomeranje.SelectedItem != null)
            {
                Termin selektovaniTerminZaPomeranje = (Termin)izborTerminaZaPomeranje.ponudjeniTerminiZaPomeranje.SelectedItem;

                foreach (Termin zakazaniTermin in Termini.Instance.listaTermina) 
                { 
                    if(selektovaniTerminZaPomeranje.vreme == zakazaniTermin.vreme)
                    {
                        pacijent.Content = zakazaniTermin.pacijentJMBG;
                        lekar.Content = zakazaniTermin.lekarJMBG;
                        tipTermina.Content = zakazaniTermin.tipTermina.ToString();
                        prostorija.Content = zakazaniTermin.idProstorije;
                    }
                }

            }
        }

        private void izaberiTerminZaPomeranje_Click(object sender, RoutedEventArgs e)
        {
            IzborTerminaZaNovoZakazivanje izborTerminaZaNovoZakazivanje = new IzborTerminaZaNovoZakazivanje(this);
            izborTerminaZaNovoZakazivanje.Show();
        }
    }
}
