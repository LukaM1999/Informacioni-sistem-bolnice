﻿using System;
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
using DinamickaOprema = Model.DinamickaOprema;
using StatickaOprema = Model.StatickaOprema;


namespace InformacioniSistemBolnice
{
    public partial class MagacinProzor : Window
    {
        private DataGrid ListaProstorija;

        public MagacinProzor() { }
        public MagacinProzor(DataGrid listaProstorija)
        {
            InitializeComponent();
            ListaProstorija = listaProstorija;
            StatickaOpremaRepo.Instance.Deserijalizacija();
            DinamickaOpremaRepo.Instance.Deserijalizacija();
            Prostorije.Instance.Deserijalizacija();
            listViewStatOpreme.ItemsSource = StatickaOpremaRepo.Instance.ListaOpreme;
            listViewDinamOpreme.ItemsSource = DinamickaOpremaRepo.Instance.ListaOpreme;
            this.listaProstorija.ItemsSource = Prostorije.Instance.ListaProstorija;
            listaProstorijaS.ItemsSource = Prostorije.Instance.ListaProstorija;
        }

        private void dugmeKreirajOpemu_Click(object sender, RoutedEventArgs e)
        {
            MagacinDodajProzor p = new MagacinDodajProzor(listViewStatOpreme);
            p.Show();
        }

        private void dugmeObrisiStatOpremu_Click(object sender, RoutedEventArgs e)
        {
            if (listViewStatOpreme.SelectedValue != null)
            {
                StatickaOprema oprema = (StatickaOprema)listViewStatOpreme.SelectedItem;
                UpravnikKontroler.Instance.BrisanjeStatickeOpreme(new(oprema.Kolicina, oprema.Tip));
                listViewStatOpreme.ItemsSource = StatickaOpremaRepo.Instance.ListaOpreme;
            }
        }

        private void dugmeIzmeniStatOpremu_Click(object sender, RoutedEventArgs e)
        {
            if (listViewStatOpreme.SelectedValue != null)
            {
                MagacinIzmeniProzor p = new MagacinIzmeniProzor(listViewStatOpreme);
                p.Show();
            }
        }

        private void dugmeKreirajDinamickuOpremu_Click(object sender, RoutedEventArgs e)
        {
            MagacinDodajDinamickuOpremu p = new MagacinDodajDinamickuOpremu(listViewDinamOpreme);
            p.Show();
        }

        private void dugmeObrisiDinamOpremu_Click(object sender, RoutedEventArgs e)
        {
            UpravnikKontroler.Instance.BrisanjeDinamickeOpreme(this);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (listViewDinamOpreme.SelectedValue != null)
            {
                MagacinIzmeniDinamickuOpremu p = new MagacinIzmeniDinamickuOpremu(listViewDinamOpreme);
                p.postavljanjeVrednost();
                p.Show();
            }
        }

        private void dugmeRaspodeliDinamicku_Click(object sender, RoutedEventArgs e)
        {
            if (listViewDinamOpreme.SelectedValue != null && listaProstorija.SelectedValue != null)
            {
                Prostorija uProstoriju = (Prostorija)listaProstorija.SelectedItem;
                RaspodelaDinamickeOpremeDto dto = new(null, uProstoriju.Id, (DinamickaOprema)listViewDinamOpreme.SelectedValue, 
                    Int32.Parse(tbKolDin.Text));
                UpravnikKontroler.Instance.RasporedjivanjeDinamickeOpreme(dto);
                listViewDinamOpreme.ItemsSource = Repozitorijum.DinamickaOpremaRepo.Instance.ListaOpreme;
                ListaProstorija.ItemsSource = Prostorije.Instance.ListaProstorija;
                ListaProstorija.ItemsSource = Prostorije.Instance.ListaProstorija;
                
            }


        }

        private void dugmeRaspodeliStaticku_Click(object sender, RoutedEventArgs e)
        {
            if (listViewStatOpreme.SelectedValue != null && listaProstorijaS.SelectedValue != null && datumStat.SelectedDate != null)
            {
                Prostorija prostorija = (Prostorija)listaProstorijaS.SelectedItem;
                UpravnikKontroler.Instance.RasporedjivanjeStatickeOpreme(new(null, prostorija.Id, 
                    (StatickaOprema)listViewStatOpreme.SelectedItem, Int32.Parse(tbKolStat.Text), (DateTime)datumStat.SelectedDate));
                listViewStatOpreme.ItemsSource = Repozitorijum.StatickaOpremaRepo.Instance.ListaOpreme;
                ListaProstorija.ItemsSource = Prostorije.Instance.ListaProstorija;
            }
        }
    }
}
