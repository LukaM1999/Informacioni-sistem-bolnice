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

namespace InformacioniSistemBolnice
{
    /// <summary>
    /// Interaction logic for UCListaRecepata.xaml
    /// </summary>
    public partial class UCListaRecepata : UserControl
    {
        public GlavniProzorLekara glavniProzorLekara;
        public Pacijent pacijent;

        public UCListaRecepata(Pacijent pacijent, GlavniProzorLekara glavni)
        {
            InitializeComponent();
            glavniProzorLekara = glavni;
            this.pacijent = pacijent;
            listaRecepata.ItemsSource = pacijent.zdravstveniKarton.recepti;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UCPacijenti pacijenti = new UCPacijenti(glavniProzorLekara);
            glavniProzorLekara.contentControl.Content = pacijenti;
        }
        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            if (listaRecepata.SelectedIndex > -1)
            {
                UCReceptInfo recept = new UCReceptInfo(this);
                glavniProzorLekara.contentControl.Content = recept;
            }
        }

    }
}
