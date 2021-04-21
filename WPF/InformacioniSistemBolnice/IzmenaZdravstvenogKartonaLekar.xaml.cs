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
        public Pacijent p;
       

        public IzmenaZdravstvenogKartonaLekar(DataGrid listaPacijenata)
        {
            InitializeComponent();
            Alergeni.Instance.Deserijalizacija("../../../json/alergeni.json");
            Kartoni.Instance.Deserijalizacija("../../../json/alergeni.json");
            p = (Pacijent)listaPacijenata.SelectedItem;
        }

        public IzmenaZdravstvenogKartonaLekar(Termin termin)
        {
            InitializeComponent();
            Pacijenti.Instance.Deserijalizacija("../../../json/pacijenti.json");
            foreach (Pacijent pacijent in Pacijenti.Instance.listaPacijenata)
            {
                if (pacijent.jmbg.Equals(termin.pacijentJMBG))
                {
                    Alergeni.Instance.Deserijalizacija("../../../json/alergeni.json");
                    Kartoni.Instance.Deserijalizacija("../../../json/alergeni.json");
                    p = pacijent;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AnamnezaForma anamneza = new AnamnezaForma(p);
            anamneza.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ReceptForma recept = new ReceptForma(p);
            recept.Show();
        }
    }
}
