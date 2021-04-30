using System;
using PropertyChanged;

namespace Model
{
    [AddINotifyPropertyChangedInterface]
    public class Termin
    {
        public DateTime vreme
        {
            get;
            set;
        }
        public double trajanje
        {
            get;
            set;
        }
        public TipTermina tipTermina
        {
            get;
            set;
        }
        public StatusTermina status
        {
            get;
            set;
        }

        public AnketaOLekaru AnketaOLekaru { get; set; }

        public Termin(DateTime dt, double tr, TipTermina tip, StatusTermina s, string jmbgPacijenta, string jmbgLekara, string sifraProstorije)
        {
            vreme = dt;
            trajanje = tr;
            tipTermina = tip;
            status = s;
            pacijentJMBG = jmbgPacijenta;
            lekarJMBG = jmbgLekara;
            idProstorije = sifraProstorije;
        }

        public Termin(DateTime dt, double tr, TipTermina tip, StatusTermina s, string jmbgPacijenta, string jmbgLekara, string sifraProstorije, bool hitan)
        {
            vreme = dt;
            trajanje = tr;
            tipTermina = tip;
            status = s;
            pacijentJMBG = jmbgPacijenta;
            lekarJMBG = jmbgLekara;
            idProstorije = sifraProstorije;
            Hitan = hitan;
        }

        public Termin(DateTime dt, double tr, TipTermina tip, StatusTermina s)
        {
            vreme = dt;
            trajanje = tr;
            tipTermina = tip;
            status = s;
        }

        public Termin()
        {
        }
        public string pacijentJMBG
        {
            get;
            set;
        }
        public string lekarJMBG
        {
            get;
            set;
        }
        public string idProstorije
        {
            get;
            set;
        }

        public bool Hitan { get; set; }

        public override string ToString()
        {
            return vreme.ToString("MM/dd/yyyy HH:mm");
        }
    }
}