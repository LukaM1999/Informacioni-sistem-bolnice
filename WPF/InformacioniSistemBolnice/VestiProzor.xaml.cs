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
    public partial class VestiProzor : Window
    {
        public VestiProzor()
        {
            InitializeComponent();
            Vesti.Instance.Deserijalizacija("../../../json/vesti.json");
            ListaVesti.ItemsSource = Vesti.Instance.listaVesti;
        }

        private void kreirajVest_Clik(object sender, RoutedEventArgs e)
        {
            KreirajVijestProzor kreirajVijestProzor = new KreirajVijestProzor();
            kreirajVijestProzor.Show();
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
            if (ListaVesti.SelectedValue != null)
            {
               Vest vest = (Vest)ListaVesti.SelectedItem;
                IzmenaVesti izmenaAVesti = new IzmenaVesti(ListaVesti);
                izmenaAVesti.naslovVesti.Text = vest.Id;
                izmenaAVesti.sadrzajVesti.Text = vest.Sadrzaj;
                izmenaAVesti.Show();
            }
        }
    }
}
