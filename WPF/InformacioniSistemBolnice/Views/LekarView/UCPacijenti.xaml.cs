using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using InformacioniSistemBolnice.ViewModels.LekarViewModel;
using MahApps.Metro.Controls;
using Model;
using Repozitorijum;

namespace InformacioniSistemBolnice
{
    public partial class UCPacijenti : MetroContentControl
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
                Karton karton = new Karton()
                    { DataContext = new KartonViewModel(glavniProzorLekara, ((Pacijent)listaPacijenata.SelectedItem).Jmbg) };
                glavniProzorLekara.contentControl.Content = karton;

            }
        }

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            ObservableCollection<Pacijent> Filter = new ObservableCollection<Pacijent>();
            foreach (var pacijent in PacijentRepo.Instance.Pacijenti)
            {
                if (Nadji(searchBox.Text.ToLower(), pacijent))
                {
                    Filter.Add(pacijent);
                }
            }

            listaPacijenata.ItemsSource = Filter;
        }

        private bool Nadji(string text, Pacijent p)
        {
            return p.Ime.ToLower().Contains(text) || p.Prezime.ToLower().Contains(text) ||
                   p.Jmbg.ToLower().StartsWith(text);
        }
    }
}
