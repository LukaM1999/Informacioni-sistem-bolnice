using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using InformacioniSistemBolnice.Views.PacijentView;
using Kontroler;
using Syncfusion.Windows.PdfViewer;

namespace InformacioniSistemBolnice.ViewModels.PacijentViewModel
{
    public class TerapijaViewModel
    {
        private Model.Pacijent _ulogovanPacijent = GlavniProzorPacijentaView.ulogovanPacijent;

        public ICommand KreiranjeIzvestaja { get; set; }

        public TerapijaViewModel()
        {
            KreiranjeIzvestaja = new Command(o => KreirajIzvestaj());
        }

        public void KreirajIzvestaj()
        {
            IzvestajKontroler.Instance.GenerisiPacijentovIzvestaj();
            new IzvestajView().Show();
        }
    }
}
