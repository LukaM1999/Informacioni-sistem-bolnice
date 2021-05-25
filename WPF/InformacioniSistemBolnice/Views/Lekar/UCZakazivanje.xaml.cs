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
using Model;
using InformacioniSistemBolnice;
using InformacioniSistemBolnice.DTO;
using Repozitorijum;

namespace InformacioniSistemBolnice.Views.Lekar
{
    /// <summary>
    /// Interaction logic for UCZakazivanje.xaml
    /// </summary>
    public partial class UCZakazivanje : UserControl
    {
        private GlavniProzorLekara glavniProzor;
        public UCZakazivanje(GlavniProzorLekara glavni)
        {
            InitializeComponent();
            PacijentRepo.Instance.Deserijalizacija();
            glavniProzor = glavni;
            ListaPacijenata.ItemsSource = PacijentRepo.Instance.Pacijenti;
        }

        private void Nazad_Click(object sender, RoutedEventArgs e)
        {
            glavniProzor.contentControl.Content = new UCRaspored(glavniProzor);
        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            if (pocetak.SelectedDate != null && kraj.SelectedDate != null && ListaPacijenata.SelectedIndex > -1 && tip.SelectedIndex > -1)
            {
                UputDto uputDto = new UputDto((DateTime)pocetak.SelectedDate, (DateTime)kraj.SelectedDate,
                    glavniProzor.ulogovanLekar, (Model.Pacijent)ListaPacijenata.SelectedItem,
                    (TipTermina)Enum.Parse(typeof(TipTermina), tip.SelectedItem.ToString()), (bool)hitno.IsChecked);
                IzborTerminaLekara izborTerminaLekara = new(uputDto);
                izborTerminaLekara.Show();
            }
        }
    }
}
