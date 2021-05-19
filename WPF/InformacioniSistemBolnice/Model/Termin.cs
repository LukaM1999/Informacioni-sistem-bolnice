using System;
using PropertyChanged;

namespace Model
{
    [AddINotifyPropertyChangedInterface]
    public class Termin
    {
        public DateTime vreme { get; set; }
        public double trajanje { get; set; }
        public TipTermina tipTermina { get; set; }
        public StatusTermina status { get; set; }
        public AnketaOLekaru AnketaOLekaru { get; set; }
        public string pacijentJMBG { get; set; }
        public string lekarJMBG { get; set; }
        public string idProstorije { get; set; }
        public bool Hitan { get; set; }

        public Termin() { }
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

        public Termin(DateTime datum, double trajanje, TipTermina tip, StatusTermina status, string jmbgPacijenta, string jmbgLekara, string sifraProstorije, bool hitan)
        {
            vreme = datum;
            this.trajanje = trajanje;
            tipTermina = tip;
            this.status = status;
            pacijentJMBG = jmbgPacijenta;
            lekarJMBG = jmbgLekara;
            idProstorije = sifraProstorije;
            Hitan = hitan;
        }

        public Termin(DateTime datum, double trajanje, TipTermina tip, StatusTermina status)
        {
            vreme = datum;
            this.trajanje = trajanje;
            tipTermina = tip;
            this.status = status;
        }

        public override string ToString()
        {
            return vreme.ToString("MM/dd/yyyy HH:mm") + " " + lekarJMBG;
        }

        public void PopuniAnketuOLekaru(AnketaOLekaru popunjenaAnketa)
        {
            AnketaOLekaru = popunjenaAnketa;
        }
    }
}