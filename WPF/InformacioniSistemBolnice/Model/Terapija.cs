using System;
using System.Collections.ObjectModel;
namespace Model
{
    public class Terapija
    {
        public DateTime pocetakTerapije { get; set; }
        public DateTime krajTerapije { get; set; }
        public double mera { get; set; }
        public double redovnost { get; set; }

        public Lek Lek { get; set; }

        public Terapija(DateTime pocetak, DateTime kraj, double m, double r, Lek lek)
        {
            pocetakTerapije = pocetak;
            krajTerapije = kraj;
            mera = m;
            redovnost = r;
            Lek = lek;
        }

    }
}