using System;
using System.Windows;
using System.Windows.Controls;
using InformacioniSistemBolnice.DTO;
using Kontroler;
using Model;

namespace InformacioniSistemBolnice
{
    public partial class KreiranjeGostujucegPacijentaProzor : UserControl
    {
        public GostujuciNaloziProzor gostujuciNaloziProzor;
        public PocetnaStranicaSekretara pocetna;
        public KreiranjeGostujucegPacijentaProzor(GostujuciNaloziProzor gostujuciNalozi)
        {
            InitializeComponent();
            gostujuciNaloziProzor = gostujuciNalozi;
            pocetna = gostujuciNaloziProzor.pocetna;
        }

        private void KreirajGostujuciNalog_Click(object sender, RoutedEventArgs e)
        {
            GostujuciNalogDto gostujuciNalogDto = new GostujuciNalogDto(this.imeUnos.Text, this.prezimeUnos.Text,
                                                    this.JMBGUnos.Text, DateTime.Parse(this.datumUnos.Text),
                                                    this.telUnos.Text, this.mailUnos.Text);
            SekretarKontroler.Instance.KreiranjeGostujucegPacijenta(gostujuciNalogDto);
            pocetna.contentControl.Content = new GostujuciNaloziProzor(gostujuciNaloziProzor.pocetna);
        }

        private void NazadBtn_Click(object sender, RoutedEventArgs e)
                 => pocetna.contentControl.Content = new GostujuciNaloziProzor(pocetna);
    }
}
