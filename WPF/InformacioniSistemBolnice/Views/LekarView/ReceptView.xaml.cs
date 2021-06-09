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
using InformacioniSistemBolnice.ViewModels.LekarViewModel;
using InformacioniSistemBolnice.Views.UpravnikView;
using Kontroler;
using MahApps.Metro.Controls;
using Model;
using Repozitorijum;

namespace InformacioniSistemBolnice.Views.LekarView
{
    public partial class ReceptView : MetroContentControl
    {
        private GlavniProzorLekara glavniProzor;
        public Model.Pacijent pacijent;
        public ReceptView(Model.Pacijent pacijent, GlavniProzorLekara glavni)
        {
            InitializeComponent();
            LekRepo.Instance.Deserijalizacija();
            glavniProzor = glavni;
            this.pacijent = pacijent;
            listaLekova.ItemsSource = LekRepo.Instance.Lekovi;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (listaLekova.SelectedIndex > -1)
            {
                ZdravstveniKartonKontroler.Instance.IzdavanjeRecepta(new((DateTime)Pocetak.SelectedDate, (DateTime)Kraj.SelectedDate,
                    double.Parse(Mera.Text), double.Parse(Redovnost.Text),
                    Id.Text, pacijent, (Lek)listaLekova.SelectedItem));
                glavniProzor.contentControl.Content = new Karton()
                    {DataContext = new KartonViewModel(glavniProzor, pacijent.Jmbg)};
            }
        }

        private void Nazad_Click(object sender, RoutedEventArgs e)
        {
            glavniProzor.contentControl.Content = new Karton()
                { DataContext = new KartonViewModel(glavniProzor, pacijent.Jmbg) };
        }
    }
}
