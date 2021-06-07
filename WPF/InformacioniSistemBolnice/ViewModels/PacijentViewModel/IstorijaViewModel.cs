using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Repozitorijum;

namespace InformacioniSistemBolnice.ViewModels.PacijentViewModel
{
    public class IstorijaViewModel
    {
        public string Title { get; } = "Istorija";
        public ObservableCollection<Termin> ZavrseniTermini { get; set; }

        public IstorijaViewModel()
        {
            ZavrseniTermini = new ObservableCollection<Termin>(GlavniProzorPacijentaView.ulogovanPacijent.ZakazaniTermini.
                Where(termin => termin.Status == StatusTermina.zavrsen));
        }
    }
}
