using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using InformacioniSistemBolnice.Utilities;
using InformacioniSistemBolnice.Views.Pacijent;
using Kontroler;
using Model;

namespace InformacioniSistemBolnice.ViewModels.PacijentViewModel
{
    public class PodsetnikViewModel
    {
        private readonly Model.Pacijent ulogovanPacijent = GlavniProzorPacijentaView.ulogovanPacijent;
        public ICommand KreirajPacijentovPodsetnik { get; set; }
        public ICommand KreiranjePodsetnika { get; set; }

        public PodsetnikViewModel()
        {
            KreirajPacijentovPodsetnik = new Command(o => KreirajPodsetnik());
            KreiranjePodsetnika = new Command(o => OtvoriKreiranjePodsetnika());
        }

        private void OtvoriKreiranjePodsetnika()
        {
            new KreiranjePodsetnikaView { DataContext = this }.Show();
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
