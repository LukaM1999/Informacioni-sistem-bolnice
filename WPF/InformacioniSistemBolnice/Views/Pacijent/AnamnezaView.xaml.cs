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
using Model;

namespace InformacioniSistemBolnice.Views.Pacijent
{
    public partial class AnamnezaView : Window
    {
        private readonly Model.Pacijent ulogovanPacijent;

        public AnamnezaView(Model.Pacijent ulogovan)
        {
            InitializeComponent();
            ulogovanPacijent = ulogovan;
            ZdravstveniKarton kartonPacijenta = ulogovanPacijent.zdravstveniKarton;
            Anamneza anamneza = kartonPacijenta.anamneza;
            SadasnjaBolest.Text = anamneza.sadasnjaBolest;
            IstorijaBolesti.Text = anamneza.ranijeBolesti;
            PorodicneBolesti.Text = anamneza.porodicneAnamneze;
            Zakljucak.Text = anamneza.zakljucak;
            Beleske.Text = anamneza.BeleskePacijenta;
        }

        private void SacuvajIzmene(object sender, RoutedEventArgs e)
        {
            PacijentKontroler.Instance.DodajBeleske(ulogovanPacijent, Beleske.Text);
            Close();
        }
    }
}
