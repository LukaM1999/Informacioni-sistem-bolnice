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
using Kontroler;
using Model;
using Repozitorijum;
using Servis;

namespace InformacioniSistemBolnice
{
    
    public partial class IzmenaZdravstvenogKartonaLekar : Window
    {
        public Pacijent pacijent;
       

        public IzmenaZdravstvenogKartonaLekar(Pacijent pacijent)
        {
            InitializeComponent();
            Alergeni.Instance.Deserijalizacija();
            Kartoni.Instance.Deserijalizacija();
            this.pacijent = pacijent;
        }

        public IzmenaZdravstvenogKartonaLekar(Termin termin)
        {
            InitializeComponent();
            Pacijenti.Instance.Deserijalizacija();
            foreach (Pacijent pacijent in Pacijenti.Instance.listaPacijenata)
            {
                if (pacijent.jmbg.Equals(termin.pacijentJMBG))
                {
                    Alergeni.Instance.Deserijalizacija();
                    Kartoni.Instance.Deserijalizacija();
                    this.pacijent = pacijent;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AnamnezaForma anamneza = new AnamnezaForma(pacijent);
            anamneza.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ReceptForma recept = new ReceptForma(pacijent);
            recept.Show();
        }
    }
}
