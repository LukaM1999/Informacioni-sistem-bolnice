using System;
using PropertyChanged;

namespace Model
{
    [AddINotifyPropertyChangedInterface]
    public class Termin
    {
        public DateTime Vreme { get; set; }
        public double Trajanje { get; set; }
        public TipTermina Tip { get; set; }
        public StatusTermina Status { get; set; }
        public AnketaOLekaru AnketaOLekaru { get; set; }
        public string PacijentJmbg { get; set; }
        public string LekarJmbg { get; set; }
        public string ProstorijaId { get; set; }
        public bool Hitan { get; set; }

        public Termin() {}

        public Termin(DateTime dt, double tr, TipTermina tip, StatusTermina s, string jmbgPacijenta, string jmbgLekara, string sifraProstorije)
        {
            Vreme = dt;
            Trajanje = tr;
            Tip = tip;
            Status = s;
            PacijentJmbg = jmbgPacijenta;
            LekarJmbg = jmbgLekara;
            ProstorijaId = sifraProstorije;
        }

        public Termin(DateTime datum, double trajanje, TipTermina tip, StatusTermina status, string jmbgPacijenta, string jmbgLekara, string sifraProstorije, bool hitan)
        {
            Vreme = datum;
            Trajanje = trajanje;
            Tip = tip;
            Status = status;
            PacijentJmbg = jmbgPacijenta;
            LekarJmbg = jmbgLekara;
            ProstorijaId = sifraProstorije;
            Hitan = hitan;
        }

        public override string ToString()
        {
            return Vreme.ToString("MM/dd/yyyy HH:mm") + " " + LekarJmbg + " " + PacijentJmbg;
        }

        public void PopuniAnketuOLekaru(AnketaOLekaru popunjenaAnketa)
        {
            AnketaOLekaru = popunjenaAnketa;
        }
    }
}