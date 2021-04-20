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
using Repozitorijum;

namespace InformacioniSistemBolnice
{
    /// <summary>
    /// Interaction logic for UCPacijenti.xaml
    /// </summary>
    public partial class UCPacijenti : UserControl
    {
        public UCPacijenti()
        {
            InitializeComponent();
            Pacijenti.Instance.Deserijalizacija("../../../json/pacijenti.json");
            listaPacijenata.ItemsSource = Pacijenti.Instance.listaPacijenata;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (listaPacijenata.SelectedIndex > -1)
            {
                IzmenaZdravstvenogKartonaLekar zk = new IzmenaZdravstvenogKartonaLekar(listaPacijenata);
                zk.Show();

            }
        }
    }
}
