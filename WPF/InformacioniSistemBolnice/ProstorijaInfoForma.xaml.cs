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

namespace InformacioniSistemBolnice
{
    /// <summary>
    /// Interaction logic for ProstorijaInfoForma.xaml
    /// </summary>
    public partial class ProstorijaInfoForma : Window
    {
        public ProstorijaInfoForma()
        {
            InitializeComponent();
        }

        public ProstorijaInfoForma(Prostorija p)
        {
            InitializeComponent();
            labId2.Content = p.id;
            labSprat2.Content = p.sprat;
            labTip2.Content = p.tip;
            if (p.jeZauzeta)
            {
                labZauzetost.Content = "Prostorija je zauzeta";
            }
            else
            {
                labZauzetost.Content = "Prostorija nije zauzeta";
            }
            

        }
    }
}
