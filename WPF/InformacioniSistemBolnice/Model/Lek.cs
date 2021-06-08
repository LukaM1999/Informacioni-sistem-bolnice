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
        public ObservableCollection<Alergen> Alergen { get; set; }

        public Lek() { }

        public Lek(String naziv, String proizvodjac, String sastojci, String zamena, ObservableCollection<Alergen> alergeni)
        {
            Naziv = naziv;
            Proizvodjac = proizvodjac;
            Sastojci = sastojci;
            Zamena = zamena;
            Alergen = alergeni;
        }

        public override string ToString()
        {
            return Naziv;
        }
    }
}