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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Model;

namespace InformacioniSistemBolnice
{
    /// <summary>
    /// Interaction logic for PregledGostujucegNaloga.xaml
    /// </summary>
    public partial class PregledGostujucegNaloga : UserControl
    {
        public PocetnaStranicaSekretara pocetna;
        public GostujuciNaloziProzor gostujuciNalozi;
        public PregledGostujucegNaloga(GostujuciNaloziProzor gostujuciNaloziProzor, ListView listaGostujucihNaloga)
        {
            InitializeComponent();
            gostujuciNalozi = gostujuciNaloziProzor;
            pocetna = gostujuciNaloziProzor.pocetna;
            if (listaGostujucihNaloga.SelectedValue != null)
            {
                Pacijent p = (Pacijent)listaGostujucihNaloga.SelectedItem;
                this.imeUnos.Content = p.Ime;
                this.prezimeUnos.Content = p.Prezime;
                this.datumUnos.Content = p.DatumRodjenja.ToString("dd/MM/yyyy");
                this.JMBGUnos.Content = p.Jmbg;
                this.telUnos.Content = p.Telefon;
                this.mailUnos.Content = p.Email;

            } 
        }

        private void NazadBtn_Click(object sender, RoutedEventArgs e)
        {
            this.pocetna.contentControl.Content = new GostujuciNaloziProzor(this.pocetna);
        }
    }
}
