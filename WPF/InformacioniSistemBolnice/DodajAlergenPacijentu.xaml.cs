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
using Servis;
using Kontroler;

namespace InformacioniSistemBolnice
{
    /// <summary>
    /// Interaction logic for DodajAlergenPacijentu.xaml
    /// </summary>
    public partial class DodajAlergenPacijentu : Window
    {
        //public string jmbg;
        public IzmjenaZdravstvenogKartonaForma IzmjenaZdravstvenogKartonaForma;
       


        public DodajAlergenPacijentu(IzmjenaZdravstvenogKartonaForma izmjenaZdravstvenogKartonaForma)
        {
            InitializeComponent();
            IzmjenaZdravstvenogKartonaForma = izmjenaZdravstvenogKartonaForma;
            Alergeni.Instance.Deserijalizacija();
            ListaAlergena.ItemsSource = Alergeni.Instance.listaAlergena;

        }

        private void dodajAlergenPacijentu_Click(object sender, RoutedEventArgs e)
        {
            SekretarKontroler.Instance.DodavanjeAlergenaIzZdravstvenogKartona(this, IzmjenaZdravstvenogKartonaForma);
            this.Close();
        }
    }
}
