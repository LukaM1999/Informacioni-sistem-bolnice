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
            menu.pocetna.contentControl.Content = new KreirajVestProzor(menu.pocetna, menu, this);
        }

       
        private void pregledVesti_Click(object sender, RoutedEventArgs e)
        {
            PregledVesti pregledVesti = new PregledVesti(ListaVesti);
            VestDto vestDto = SekretarKontroler.Instance.PregledVesti((Vest)ListaVesti.SelectedItem);
            pregledVesti.sadrzaj.Text = vestDto.Sadrzaj;
            pregledVesti.naslov.Content = vestDto.Id;
            pregledVesti.vremeObjave.Content = vestDto.VremeObjave;
            pregledVesti.Show();
        }

        private void obrisiVesti_Clik(object sender, RoutedEventArgs e)
        {
            if (ListaVesti.SelectedValue != null)
            {
                SekretarKontroler.Instance.UklanjanjeVesti((Vest)ListaVesti.SelectedValue);
                ListaVesti.ItemsSource = Vesti.Instance.listaVesti;
            }
        }

        private void izmeniVest_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListaVesti.SelectedValue != null)
            {
                IzmenaVesti izmenaAVesti = new IzmenaVesti(this, this.menu.pocetna);
                izmenaAVesti.naslovVesti.Text = ((Vest)ListaVesti.SelectedItem).Id;
                izmenaAVesti.sadrzajVesti.Text = ((Vest)ListaVesti.SelectedItem).Sadrzaj;
                ListaVesti.ItemsSource = Vesti.Instance.listaVesti;
                this.menu.pocetna.contentControl.Content = izmenaAVesti.Content;
            }
        }
    }
}
