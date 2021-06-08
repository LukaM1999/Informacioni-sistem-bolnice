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
using InformacioniSistemBolnice.DTO;
using Model;
using Kontroler;

namespace InformacioniSistemBolnice
{
    public partial class AnamnezaForma : Window
    {
        public Pacijent pacijent;
        public AnamnezaForma(Pacijent pacijent)
        {
            InitializeComponent();
            this.pacijent = pacijent;
            if (pacijent.zdravstveniKarton.Anamneza == null) return;
            SadasnjaBolest.Text = pacijent.zdravstveniKarton.Anamneza.SadasnjaBolest;
            IstorijaBolesti.Text = pacijent.zdravstveniKarton.Anamneza.RanijeBolesti;
            PorodicneBolesti.Text = pacijent.zdravstveniKarton.Anamneza.PorodicneAnamneze;
            Zakljucak.Text = pacijent.zdravstveniKarton.Anamneza.Zakljucak;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AnamnezaDto anamneza =
                new(pacijent.Jmbg, SadasnjaBolest.Text, IstorijaBolesti.Text, PorodicneBolesti.Text, Zakljucak.Text);
            ZdravstveniKartonKontroler.Instance.DodavanjeAnamneze(anamneza);
            this.Close();
        }
    }
}
