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
using Model;

namespace InformacioniSistemBolnice
{
    /// <summary>
    /// Interaction logic for KreiranjeGostujucegPacijentaProzor.xaml
    /// </summary>
    public partial class KreiranjeGostujucegPacijentaProzor : Window
    {
        public KreiranjeGostujucegPacijentaProzor()
        {
            InitializeComponent();
        }

        private void potvrdiDugme_Click(object sender, RoutedEventArgs e)
        {
            GostujuciNalogDto gostujuciNalogDto = new GostujuciNalogDto(this.imeUnos.Text, this.prezimeUnos.Text,
                                this.JMBGUnos.Text, DateTime.Parse(this.datumUnos.Text), this.telUnos.Text, this.mailUnos.Text);
            SekretarKontroler.Instance.KreiranjeGostujucegPacijenta(gostujuciNalogDto);
            this.Close();
        }
    }
}
