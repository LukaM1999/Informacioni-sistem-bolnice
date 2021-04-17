using System;

namespace Model
{
    public class Lekar : Osoba
    {
        public Lekar()
        {
        }
        public Lekar(Osoba o)
        {
            this.ime = o.ime;
            this.prezime = o.prezime;
            this.jmbg = o.jmbg;
            this.telefon = o.telefon;
            this.email = o.email;
            this.korisnik = o.korisnik;
        }

        public Termin[] termin;

        public override string ToString()
        {
            return ime + " " + prezime;
        }
    }
}