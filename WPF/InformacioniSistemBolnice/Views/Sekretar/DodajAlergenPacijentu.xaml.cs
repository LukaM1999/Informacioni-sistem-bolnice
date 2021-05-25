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
using Model;
using System.Collections.ObjectModel;

namespace InformacioniSistemBolnice
{
    public partial class DodajAlergenPacijentu : Window
    {
        public IzmenaZdravstvenogKartonaForma izmenaZdravstvenogKartonaForma;
        public ObservableCollection<Alergen> alergeniZaDodavanje = new ObservableCollection<Alergen>();

        public DodajAlergenPacijentu(IzmenaZdravstvenogKartonaForma izmenaZdravstvenogKartona)
        {
            InitializeComponent();
            izmenaZdravstvenogKartonaForma = izmenaZdravstvenogKartona;
            AlergenRepo.Instance.Deserijalizacija();
            alergeniZaDodavanje = AlergenRepo.Instance.Alergeni;
            ZdravstveniKarton zdravstveniKarton = PacijentRepo.Instance.PronadjiZdravstveniKarton
                (izmenaZdravstvenogKartonaForma.JMBGLabela.Content.ToString());
            GenerisiAlergeneZaDodavanje(zdravstveniKarton);
            ListaAlergena.ItemsSource = alergeniZaDodavanje;
        }

        private void GenerisiAlergeneZaDodavanje(ZdravstveniKarton zdravstveniKarton)
        {
            foreach (Alergen a in zdravstveniKarton.Alergeni.ToList())
            {
                foreach (Alergen alergen in alergeniZaDodavanje.ToList())
                    if (alergen.Naziv == a.Naziv) alergeniZaDodavanje.Remove(alergen);
            }
        }

        private void dodajAlergenPacijentu_Click(object sender, RoutedEventArgs e)
        {
            SekretarKontroler.Instance.DodavanjeAlergenaIzZdravstvenogKartona((Alergen)ListaAlergena.SelectedItem,
                izmenaZdravstvenogKartonaForma.JMBGLabela.Content.ToString());
            AzurirajPrikazAlergena();
            this.Close();
        }

        private void AzurirajPrikazAlergena()
        {
            izmenaZdravstvenogKartonaForma.AlergeniPacijenta.ItemsSource = PacijentRepo.Instance.NadjiPoJmbg
            (izmenaZdravstvenogKartonaForma.JMBGLabela.Content.ToString()).zdravstveniKarton.Alergeni;
        }
    }
}
