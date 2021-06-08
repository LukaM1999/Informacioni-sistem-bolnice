using System;
using System.Collections.ObjectModel;
using PropertyChanged;

namespace Model
{
    [AddINotifyPropertyChangedInterface]
    public class Terapija
    {
        public DateTime PocetakTerapije { get; set; }
        public DateTime KrajTerapije { get; set; }
        public double MeraLeka { get; set; }
        public double RedovnostTerapije { get; set; }
        public Lek Lek { get; set; }

        public Terapija(DateTime pocetak, DateTime kraj, double m, double r, Lek lek)
        {
            PocetakTerapije = pocetak;
            KrajTerapije = kraj;
            MeraLeka = m;
            RedovnostTerapije = r;
            Lek = lek;
        }

        public int DobaviRedovnostTerapije()
        {
            return (int)RedovnostTerapije;
        }
    }
}