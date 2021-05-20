using System;
using System.Collections.ObjectModel;
using PropertyChanged;

namespace Model
{
    [AddINotifyPropertyChangedInterface]
    public class Lek : DinamickaOprema
    {
        public string Naziv { get; set; }
        public string Proizvodjac { get; set; }
		public string Sastojci { get; set; }
        public string Zamena { get; set; }

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

        public Lek() { }
        public Lek(String naziv, String proizvodjac, String sastojci)
        {
            this.Naziv = naziv;
            this.Proizvodjac = proizvodjac;
            this.Sastojci = sastojci;
        }
        public Lek(string naziv, string proizvodjac, string sastojci, string zamena)
        {
            this.Naziv = naziv;
            this.Proizvodjac = proizvodjac;
            this.Sastojci = sastojci;
            this.Zamena = zamena;
        }
    }
}