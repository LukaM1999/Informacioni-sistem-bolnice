﻿using System.Windows;
using System.Windows.Controls;
using Kontroler;

namespace InformacioniSistemBolnice
{
    /// <summary>
    /// Interaction logic for IzmenaAlergenaForma.xaml
    /// </summary>
    public partial class IzmenaAlergenaForma : Window
    {
        public DataGrid listaAlergena;
        public IzmenaAlergenaForma()
        {
            InitializeComponent();
        }


        public IzmenaAlergenaForma(DataGrid ListaAlergena)
        {
            InitializeComponent();
            listaAlergena = ListaAlergena;
        }

        private void izmjeniAlergenDugme_Click(object sender, RoutedEventArgs e)
        {
            SekretarKontroler.Instance.IzmjenaAlergena(listaAlergena, this);
            this.Close();
        }
    }
}