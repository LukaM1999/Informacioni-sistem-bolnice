﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InformacioniSistemBolnice.Servis.UpravljanjeIzvestajima;
using InformacioniSistemBolnice.Utilities;

namespace Kontroler
{
    public class IzvestajKontroler
    {
        private static readonly Lazy<IzvestajKontroler> Lazy = new(() => new IzvestajKontroler());
        public static IzvestajKontroler Instance => Lazy.Value;

        public void GenerisiPacijentovIzvestaj()
        {
            IzvestajPacijentaServis.Instance.GenerisiIzvestaj();
        }

        public void GenerisiLekarovIzvestaj()
        {
            IzvestajUtility izvestaj = new IzvestajLekaraServis();
            izvestaj.GenerisiIzvestaj();
        }

        public void GenerisiSekretarovIzvestaj()
        {
            IzvestajSekretaraServis.Instance.GenerisiIzvestaj();
        }

        public void GenerisiUpravnikovIzvestaj()
        {
            IzvestajUpravnikaServis.Instance.GenerisiIzvestaj();
        }
    }
}
