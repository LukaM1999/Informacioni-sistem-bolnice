using System.Linq;
using System.Windows;
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
                (izmenaZdravstvenogKartonaForma.ZdravstveniKarton.Jmbg);
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

        private void DodajAlergenPacijentu_Click(object sender, RoutedEventArgs e)
        {
            ZdravstveniKarton zdravstveniKarton = izmenaZdravstvenogKartonaForma.ZdravstveniKarton;
            Pacijent pacijent = izmenaZdravstvenogKartonaForma.Pacijent;
            SekretarKontroler.Instance.DodavanjeAlergenaIzZdravstvenogKartona
                ((Alergen)ListaAlergena.SelectedItem, pacijent.Jmbg);
            AzurirajPrikazAlergena();
            this.Close();
        }

        private void AzurirajPrikazAlergena()
        {
            Pacijent pacijent = PacijentRepo.Instance.NadjiPoJmbg(izmenaZdravstvenogKartonaForma.Pacijent.Jmbg);
            ZdravstveniKarton zdravstveniKarton = pacijent.zdravstveniKarton;
            izmenaZdravstvenogKartonaForma.AlergeniPacijenta.ItemsSource = zdravstveniKarton.Alergeni;
        }
    }
}
