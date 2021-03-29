using System;

namespace Model
{
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
        public Pacijent pacijent
        {
            get;
            set;
        }
        public Lekar lekar
        {
            get;
            set;
        }
        public Prostorija prostorija
        {
            get;
            set;
        }
        public override string ToString()
        {
            return vreme.ToString("dd/MM/yyyy HH:mm");
        }
    }
}