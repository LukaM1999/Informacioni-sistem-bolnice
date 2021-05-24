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
using Kontroler;

namespace InformacioniSistemBolnice
{
    /// <summary>
    /// Interaction logic for ProstorijaRenoviranjeProzor.xaml
    /// </summary>
    public partial class ProstorijaRenoviranjeProzor : Window
    {
        private Prostorija prostorija;
        public ProstorijaRenoviranjeProzor(DataGrid listaProstorija)
        {
            InitializeComponent();
            prostorija = (Prostorija)listaProstorija.SelectedItem;
            datumPocetkaRenoviranja.DisplayDateStart = DateTime.Today;
            datumKrajaRenoviranja.DisplayDateStart = DateTime.Today;
        }

        private void btnPorvrda_Click(object sender, RoutedEventArgs e)
        {
            if (proveraDatumaRenoviranja())
            {
                proveraZauzetostiProstorije();
            }
            else
            {
                labelaPoruka.Visibility = Visibility.Visible;
            }
        }

        public bool proveraDatumaRenoviranja()
        {
            return datumPocetkaRenoviranja.SelectedDate < datumKrajaRenoviranja.SelectedDate;
        }

        public void proveraZauzetostiProstorije()
        {
            foreach(Termin t in prostorija.TerminiProstorije.ToList())
            {
                if(daLiPostojiTerminUIzabranomOpseguVremena(t))
                {
                    labelaPorukaOZauzetosti.Visibility = Visibility.Visible;
                    return;
                }
            }
            ProstorijaRenoviranjeDto dto = new ProstorijaRenoviranjeDto((DateTime)datumPocetkaRenoviranja.SelectedDate,
                    (DateTime)datumKrajaRenoviranja.SelectedDate, prostorija);
            UpravnikKontroler.Instance.ZakazivanjeRenoviranja(dto);
            this.Close();
        }

        public bool daLiPostojiTerminUIzabranomOpseguVremena(Termin zakazaniTermin)
        {
            return zakazaniTermin.Vreme >= datumPocetkaRenoviranja.SelectedDate && zakazaniTermin.Vreme < datumKrajaRenoviranja.SelectedDate;
        }
    }
}
