using System;
using System.Collections.ObjectModel;
namespace Model
{
    public class Recept
    {
        private string id;

        public ObservableCollection<Terapija> terapija;

        public ObservableCollection<Terapija> Terapija
        {
            get
            {
                if (terapija == null)
                    terapija = new ObservableCollection<Terapija>();
                return terapija;
            }
            set
            {
                RemoveAllTerapija();
                if (value != null)
                {
                    foreach (Terapija oTerapija in value)
                        AddTerapija(oTerapija);
                }
            }
        }

        public void AddTerapija(Terapija newTerapija)
        {
            if (newTerapija == null)
                return;
            if (this.terapija == null)
                this.terapija = new ObservableCollection<Terapija>();
            if (!this.terapija.Contains(newTerapija))
                this.terapija.Add(newTerapija);
        }

        public void RemoveTerapija(Terapija oldTerapija)
        {
            if (oldTerapija == null)
                return;
            if (this.terapija != null)
                if (this.terapija.Contains(oldTerapija))
                    this.terapija.Remove(oldTerapija);
        }

        public void RemoveAllTerapija()
        {
            if (terapija != null)
                terapija.Clear();
        }

    }
}