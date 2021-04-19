using System;
using System.Collections.ObjectModel;
namespace Model
{
    public class Lekar : Osoba
    {
        public Lekar()
        {
        }
        public Lekar(Osoba o, string spec)
        {
            this.ime = o.ime;
            this.prezime = o.prezime;
            this.jmbg = o.jmbg;
            this.telefon = o.telefon;
            this.email = o.email;
            this.korisnik = o.korisnik;
            this.specijalizacija = spec;
            zauzetiTermini = new ObservableCollection<Termin>();
        }

        public string specijalizacija
        {
            get;
            set;
        }

        public ObservableCollection<Termin> zauzetiTermini;

        public override string ToString()
        {
            return ime + " " + prezime;
        }
    }
}