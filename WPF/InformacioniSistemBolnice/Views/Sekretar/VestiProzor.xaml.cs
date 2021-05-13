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
using Kontroler;
using Model;

namespace InformacioniSistemBolnice
{
    public partial class VestiProzor : UserControl
    {
        public UCMenuSekretara menu;
        public VestiProzor(UCMenuSekretara menuSekretara)
        {
            InitializeComponent();
            menu = menuSekretara;
            Vesti.Instance.Deserijalizacija();
            ListaVesti.ItemsSource = Vesti.Instance.listaVesti;
        }

        private void kreirajVest_Clik(object sender, RoutedEventArgs e)
        {
            menu.pocetna.contentControl.Content = new KreirajVijestProzor(menu.pocetna, menu, this);
        }

       
        private void pregledVesti_Click(object sender, RoutedEventArgs e)
        {
            SekretarKontroler.Instance.PregledVesti(ListaVesti);
        }

        private void obrisiVesti_Clik(object sender, RoutedEventArgs e)
        {
            SekretarKontroler.Instance.UklanjanjeVesti(this.ListaVesti);
        }

        private void izmeniVest_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListaVesti.SelectedValue != null)
            {
               Vest vest = (Vest)ListaVesti.SelectedItem;
                IzmenaVesti izmenaAVesti = new IzmenaVesti(this, this.menu.pocetna);
                izmenaAVesti.naslovVesti.Text = vest.Id;
                izmenaAVesti.sadrzajVesti.Text = vest.Sadrzaj;
                this.menu.pocetna.contentControl.Content = izmenaAVesti.Content;
            }
        }
    }
}
