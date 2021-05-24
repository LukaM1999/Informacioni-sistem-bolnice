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
using Repozitorijum;
using Model;

namespace InformacioniSistemBolnice
{
    public partial class TerminInfoProzor : Window
    {
        public TerminInfoProzor(DataGrid listaZakazanih)
        {
            InitializeComponent();
            Termin t = (Termin)listaZakazanih.SelectedItem;
            datumLabela.Content = t.Vreme.ToString("MM/dd/yyyy");
            vremeLabela.Content = t.Vreme.ToString("HH:mm");
            tipTerminaLabela.Content = t.Tip.ToString();
            statusTerminaLabela.Content = t.Status.ToString();
            trajanjeLabela.Content = t.Trajanje.ToString();
            if (t.ProstorijaId != null)
            {
                salaLabela.Content = t.ProstorijaId;
            }
            else
            {
                salaLabela.Content = "Nepoznato";
            }
        }
    }
}
