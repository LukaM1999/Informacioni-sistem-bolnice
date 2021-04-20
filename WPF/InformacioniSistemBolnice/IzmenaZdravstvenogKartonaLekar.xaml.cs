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
        public DataGrid ListaPacijenata;
        public Pacijent p;
       

        public IzmenaZdravstvenogKartonaLekar(DataGrid listaPacijenata)
        {
            InitializeComponent();
            ListaPacijenata = listaPacijenata;
            Alergeni.Instance.Deserijalizacija("../../../json/alergeni.json");
            Kartoni.Instance.Deserijalizacija("../../../json/alergeni.json");
            p = (Pacijent)listaPacijenata.SelectedItem;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ReceptForma recept = new ReceptForma(p);
            recept.Show();
        }
    }
}
