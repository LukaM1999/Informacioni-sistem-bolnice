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
                ProstorijaRenoviranjeDto dto = new ProstorijaRenoviranjeDto((DateTime)datumPocetkaRenoviranja.SelectedDate,
                    (DateTime)datumPocetkaRenoviranja.SelectedDate, prostorija);
                UpravnikKontroler.Instance.ZakazivanjeRenoviranja(dto);
                this.Close();
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
    }
}
