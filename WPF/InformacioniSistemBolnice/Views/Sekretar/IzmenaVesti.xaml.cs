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
using Repozitorijum;

namespace InformacioniSistemBolnice
{
    
    public partial class IzmenaVesti : UserControl
    {
        public PocetnaStranicaSekretara pocetna;
        public VestiProzor vesti;
        public IzmenaVesti(VestiProzor vestiProzor, PocetnaStranicaSekretara pocetnaStranicaSekretara)
        {
            InitializeComponent();
            vesti = vestiProzor;
            pocetna = pocetnaStranicaSekretara;
        }

        private void izmeniVest_Click(object sender, RoutedEventArgs e)
        {
            VestDto vestDto = new VestDto(naslovVesti.Text, sadrzajVesti.Text);
            SekretarKontroler.Instance.IzmenaVesti(vestDto, (Vest)vesti.ListaVesti.SelectedItem);
            vesti.ListaVesti.ItemsSource = VestRepo.Instance.Vesti;
            pocetna.contentControl.Content = vesti.Content;
            
        }

        private void NazadBtn_Click(object sender, RoutedEventArgs e)
        {
            pocetna.contentControl.Content = vesti.Content;
        }
    }
}
