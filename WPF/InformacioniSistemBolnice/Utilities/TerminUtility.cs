using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace InformacioniSistemBolnice.Utilities
{
    public class TerminUtility
    {
        public const int PocetakRadnogVremenaSati = 7;
        public const int DodatniDaniPredlaganjaTermina = 2;
        public const int DodatniDaniZaPomeranjeTermina = 2;
        public const int PolucasovniTerminiRadnogDana = 27;
        public const double SatiDoSledecegRadnogDana = 10.5;
        public const int PodrazumevanoTrajanjeTermina = 30;
        public const int DanSati = 24;
        public const int DvaDanaUnazad = 2 * DanSati - PocetakRadnogVremenaSati;

        public static DateTime IzracunajPomeranjeUnazad(Termin terminZaPomeranje)
        {
            return terminZaPomeranje.Vreme.Subtract(new TimeSpan(DvaDanaUnazad + terminZaPomeranje.Vreme.Hour, 0, 0));
        }

        public static DateTime IzracunajPomeranjeUnapred(Termin terminZaPomeranje)
        {
            return terminZaPomeranje.Vreme.
                Subtract(new TimeSpan(terminZaPomeranje.Vreme.Hour - DanSati - PocetakRadnogVremenaSati, 0 , 0));
        }
    }
}
