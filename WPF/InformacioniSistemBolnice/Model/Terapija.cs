using System;
using System.Collections.ObjectModel;
namespace Model
{
    public class Terapija
    {
        private DateTime pocetakTerapije;
        private DateTime krajTerapije;
        private double mera;
        private double redovnost;

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

    }
}