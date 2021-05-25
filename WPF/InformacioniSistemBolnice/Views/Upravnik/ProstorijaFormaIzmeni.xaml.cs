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
using InformacioniSistemBolnice.DTO;
using Model;
using Servis;
using Repozitorijum;
using Kontroler;

namespace InformacioniSistemBolnice
{
    public partial class ProstorijaFormaIzmeni : Window
    {
        private DataGrid ListaProstorija;
        public ProstorijaFormaIzmeni()
        {
            InitializeComponent();
        }
        public ProstorijaFormaIzmeni(DataGrid listaProstorija)
        {
            InitializeComponent();
            ListaProstorija = listaProstorija;
            Prostorija izabranaProstorija = (Prostorija)listaProstorija.SelectedValue;
            tb1.Text = izabranaProstorija.Sprat.ToString();
            tipIzmena.Text = izabranaProstorija.Tip.ToString();
            tb2.Text = izabranaProstorija.Id.ToString();
            if (izabranaProstorija.JeZauzeta)
            {
                rb1.IsChecked = true;
            }
            else
            {
                rb2.IsChecked = true;
            }
            tb2.IsReadOnly = true;
        }
        private void potvrdaIzmeneDugme_Click(object sender, RoutedEventArgs e)
        {
            Prostorija izabranaProstorija = (Prostorija)ListaProstorija.SelectedItem;
            ProstorijaDto dto = new();
            if ((bool)rb1.IsChecked) dto.JeZauzeta = true;
            if((bool)rb2.IsChecked) dto.JeZauzeta = false;
            dto = new(Int32.Parse(tb1.Text), (TipProstorije)Enum.Parse(typeof(TipProstorije), tipIzmena.Text),
                    tb2.Text, dto.JeZauzeta, izabranaProstorija.Inventar);
            UpravnikKontroler.Instance.IzmenaProstorije(dto);
            ListaProstorija.ItemsSource = ProstorijaRepo.Instance.Prostorije;
            this.Close();
        }
    }
}
