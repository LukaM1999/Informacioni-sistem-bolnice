using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using InformacioniSistemBolnice.Views.PacijentView;
using Kontroler;
using Model;
using Syncfusion.Windows.PdfViewer;

namespace InformacioniSistemBolnice.ViewModels.PacijentViewModel
{
    public class TerapijaViewModel
    {
        public ObservableCollection<Terapija> Terapije { get; set; } = new();
        public ICommand KreiranjeIzvestaja { get; set; }

        public TerapijaViewModel()
        {
            KreiranjeIzvestaja = new Command(o => KreirajIzvestaj());
            PronadjiTerapije();
        }

        private void PronadjiTerapije()
        {
            foreach (Recept recept in GlavniProzorPacijentaView.ulogovanPacijent.zdravstveniKarton.recepti)
                foreach (Terapija terapija in recept.terapije)
                    Terapije.Add(terapija);
        }

        public void KreirajIzvestaj()
        {
            IzvestajKontroler.Instance.GenerisiPacijentovIzvestaj();
            new IzvestajView().Show();
        }
    }
}
