using System;
using System.Collections.ObjectModel;

namespace Model
{
    public class Lek : DinamickaOprema
    {
        public string Naziv { get; set; }
        public string Proizvodjac { get; set; }

        public ObservableCollection<Alergen> alergen;

        public ObservableCollection<Alergen> Alergen
        {
            get
            {
                if (alergen == null)
                    alergen = new ObservableCollection<Alergen>();
                return alergen;
            }
            set
            {
                RemoveAllAlergen();
                if (value != null)
                {
                    foreach (Alergen oAlergen in value)
                        AddAlergen(oAlergen);
                }
            }
        }

        public void AddAlergen(Alergen newAlergen)
        {
            if (newAlergen == null)
                return;
            if (this.alergen == null)
                this.alergen = new ObservableCollection<Alergen>();
            if (!this.alergen.Contains(newAlergen))
                this.alergen.Add(newAlergen);
        }

        public void RemoveAlergen(Alergen oldAlergen)
        {
            if (oldAlergen == null)
                return;
            if (this.alergen != null)
                if (this.alergen.Contains(oldAlergen))
                    this.alergen.Remove(oldAlergen);
        }

        public void RemoveAllAlergen()
        {
            if (alergen != null)
                alergen.Clear();
        }

    }
}