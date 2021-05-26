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
using Repozitorijum;

namespace InformacioniSistemBolnice
{
    /// <summary>
    /// Interaction logic for UCPacijenti.xaml
    /// </summary>
    public partial class UCPacijenti : UserControl
    {
        private GlavniProzorLekara glavniProzorLekara;
        public UCPacijenti(GlavniProzorLekara glavni)
        {
            InitializeComponent();
            PacijentRepo.Instance.Deserijalizacija();
            listaPacijenata.ItemsSource = PacijentRepo.Instance.Pacijenti;
            glavniProzorLekara = glavni;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (listaPacijenata.SelectedIndex > -1)
            {
                IzmenaZdravstvenogKartonaLekar zk = new((Pacijent)listaPacijenata.SelectedItem, glavniProzorLekara);
                zk.Show();

            }
        }
    }
}
