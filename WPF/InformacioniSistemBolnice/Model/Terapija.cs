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

        public ObservableCollection<Lek> lek;

        public ObservableCollection<Lek> Lek
        {
            get
            {
                if (lek == null)
                    lek = new ObservableCollection<Lek>();
                return lek;
            }
            set
            {
                RemoveAllLek();
                if (value != null)
                {
                    foreach (Lek oLek in value)
                        AddLek(oLek);
                }
            }
        }

        public void AddLek(Lek newLek)
        {
            if (newLek == null)
                return;
            if (this.lek == null)
                this.lek = new ObservableCollection<Lek>();
            if (!this.lek.Contains(newLek))
                this.lek.Add(newLek);
        }

        public void RemoveLek(Lek oldLek)
        {
            if (oldLek == null)
                return;
            if (this.lek != null)
                if (this.lek.Contains(oldLek))
                    this.lek.Remove(oldLek);
        }

        public void RemoveAllLek()
        {
            if (lek != null)
                lek.Clear();
        }

        public Terapija()
        {

        }

        public Terapija(DateTime pocetak, DateTime kraj, double m, double r)
        {
            pocetakTerapije = pocetak;
            krajTerapije = kraj;
            mera = m;
            redovnost = r;
        }

    }
}