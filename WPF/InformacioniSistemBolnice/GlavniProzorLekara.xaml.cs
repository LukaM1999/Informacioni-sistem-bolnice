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

namespace InformacioniSistemBolnice
{
    /// <summary>
    /// Interaction logic for GlavniProzorLekara.xaml
    /// </summary>
    public partial class GlavniProzorLekara : Window
    {
        public GlavniProzorLekara()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TerminiLekaraProzor terminiLekara = new TerminiLekaraProzor();
            terminiLekara.Show();
            this.Show();

        }
    }
}
