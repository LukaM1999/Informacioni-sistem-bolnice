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

namespace InformacioniSistemBolnice
{
    
    public partial class IzmenaVesti : Window
    {
        public ListView listaVesti;
        public IzmenaVesti(ListView lista)
        {
            InitializeComponent();
            listaVesti = lista;
        }

        private void izmeniVest_Click(object sender, RoutedEventArgs e)
        {
            SekretarKontroler.Instance.IzmenaVesti(this.listaVesti, this);
            this.Close();
        }
    }
}
