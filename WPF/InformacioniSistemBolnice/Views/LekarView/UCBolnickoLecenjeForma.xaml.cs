﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Repozitorijum;
using Model;
using InformacioniSistemBolnice.DTO;
using InformacioniSistemBolnice.ViewModels.LekarViewModel;
using Kontroler;
using MahApps.Metro.Controls;


namespace InformacioniSistemBolnice
{
    public partial class UCBolnickoLecenjeForma : MetroContentControl
    {
        private GlavniProzorLekara glavniProzor;
        public ObservableCollection<Prostorija> sobe = new();
        public Pacijent pacijent;
        public object prethodniProzor;
        public UCBolnickoLecenjeForma(Pacijent izabran, GlavniProzorLekara glavni, object prethodni)
        {
            InitializeComponent();
            BolnickoLecenjeRepo.Instance.Deserijalizacija();
            ProstorijaRepo.Instance.Deserijalizacija();
            prethodniProzor = prethodni;
            glavniProzor = glavni;
            pacijent = izabran;
            PronalaziProstorijeZaHospitalizaciju();
        }

        private void PronalaziProstorijeZaHospitalizaciju()
        {
            foreach (Prostorija prostorija in ProstorijaRepo.Instance.Prostorije)
            {
                DodajProstoriju(prostorija);
            }
            ProstorijeZaHospitalizaciju.ItemsSource = sobe;
        }

        private void DodajProstoriju(Prostorija prostorija)
        {
            if (prostorija.Tip == TipProstorije.prostorijaZaHospitalizaciju) sobe.Add(prostorija);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            glavniProzor.contentControl.Content = new Karton(){DataContext = new KartonViewModel(glavniProzor, pacijent.Jmbg)};
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (Pocetak.SelectedDate != null && Zavrsetak.SelectedDate != null && ProstorijeZaHospitalizaciju.SelectedIndex > -1)
            {
                BolnickoLecenjeKontroler.Instance.KreirajBolnickoLecenje(new((DateTime)Pocetak.SelectedDate, 
                    (DateTime)Zavrsetak.SelectedDate, pacijent, (Prostorija)ProstorijeZaHospitalizaciju.SelectedItem));
                UCBolnickaLecenja lecenja = new UCBolnickaLecenja(glavniProzor);
                glavniProzor.contentControl.Content = lecenja;
            }
        }
    }
}
