using Syncfusion.Pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformacioniSistemBolnice.Utilities
{
    public abstract class IzvestajUtility
    {
        public void GenerisiIzvestaj() 
        {
            KreirajDokument();
            PrikaziObavestenje();
        }

        public abstract void KreirajDokument();
        public abstract void PrikaziObavestenje();
    }
}
