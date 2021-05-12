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
using Model;
using Kontroler;

namespace InformacioniSistemBolnice
{
    /// <summary>
    /// Interaction logic for LekDodajProzor.xaml
    /// </summary>
    public partial class LekDodajProzor : Window
    {
        public LekDodajProzor()
        {
            InitializeComponent();
        }

        private void btnPotvrdi_Click(object sender, RoutedEventArgs e)
        {
            LekDto dto = new LekDto(tbNaziv.Text, tbProizvodjac1.Text, tbSastojci.Text);
            UpravnikKontroler.Instance.KreiranjeLeka(dto);
            this.Close();
        }
    }
}
