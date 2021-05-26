﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using InformacioniSistemBolnice.Utilities;
using InformacioniSistemBolnice.ViewModels;
using InformacioniSistemBolnice.Views.Pacijent;
using Kontroler;
using Model;

namespace InformacioniSistemBolnice
{
    public class KreiranjePodsetnikaViewModel
    {
        private readonly Pacijent ulogovanPacijent;
        public ICommand KreirajPacijentovPodsetnik { get; set; }

        public KreiranjePodsetnikaViewModel(Pacijent ulogovan)
        {
            ulogovanPacijent = ulogovan;
            KreirajPacijentovPodsetnik = new Command(o => KreirajPodsetnik());
        }

        private void KreirajPodsetnik()
        {
            KreiranjePodsetnikaView podsetnikForma = (KreiranjePodsetnikaView)PronadjiProzorUtility.PronadjiProzor(this);
            PacijentKontroler.Instance.UkljuciNoviPodsetnik(new Podsetnik(podsetnikForma.Sadrzaj.Text, 
                    podsetnikForma.Vreme.Text, ulogovanPacijent.Jmbg));
            podsetnikForma.Close();
        }
    }
}