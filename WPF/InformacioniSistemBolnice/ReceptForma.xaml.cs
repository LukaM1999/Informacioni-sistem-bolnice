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
using Kontroler;

namespace InformacioniSistemBolnice
{
    /// <summary>
    /// Interaction logic for ReceptForma.xaml
    /// </summary>
    public partial class ReceptForma : Window
    {
        public Pacijent pacijent;
        public ReceptForma(Pacijent pacijent)
        {
            InitializeComponent();
            this.pacijent = pacijent;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ReceptDto dtoRecept = new ReceptDto((DateTime)Pocetak.SelectedDate, (DateTime)Kraj.SelectedDate, 
                double.Parse(Mera.Text), double.Parse(Redovnost.Text), Id.Text, pacijent);
            LekarKontroler.Instance.IzdavanjeRecepta(dtoRecept);
            this.Close();
        }
    }
}
