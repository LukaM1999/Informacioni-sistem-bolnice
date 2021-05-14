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
    /// <summary>
    /// Interaction logic for DodajAlergenPacijentu.xaml
    /// </summary>
    public partial class DodajAlergenPacijentu : Window
    {
        
        public IzmjenaZdravstvenogKartonaForma IzmjenaZdravstvenogKartonaForma;
        public ObservableCollection<Alergen> listaAlergena = new ObservableCollection<Alergen>();
        public DodajAlergenPacijentu(IzmjenaZdravstvenogKartonaForma izmjenaZdravstvenogKartonaForma)
        {
            InitializeComponent();
            IzmjenaZdravstvenogKartonaForma = izmjenaZdravstvenogKartonaForma;
            Alergeni.Instance.Deserijalizacija();
            listaAlergena = Alergeni.Instance.listaAlergena;
            foreach (Pacijent p in Pacijenti.Instance.ListaPacijenata)
            {
                if(p.zdravstveniKarton.Jmbg.Equals(izmjenaZdravstvenogKartonaForma.JMBGLabela.Content))
                {
                   foreach(Alergen a in p.zdravstveniKarton.Alergeni.ToList())
                    {
                        foreach(Alergen alergen in listaAlergena.ToList())
                        {
                            if(alergen.nazivAlergena == a.nazivAlergena)
                            {
                                listaAlergena.Remove(alergen);
                            }
                        }
                    }
                }
            }
            ListaAlergena.ItemsSource = listaAlergena;

        }

        private void dodajAlergenPacijentu_Click(object sender, RoutedEventArgs e)
        {
            SekretarKontroler.Instance.DodavanjeAlergenaIzZdravstvenogKartona(this, IzmjenaZdravstvenogKartonaForma);
            this.Close();
        }
    }
}
