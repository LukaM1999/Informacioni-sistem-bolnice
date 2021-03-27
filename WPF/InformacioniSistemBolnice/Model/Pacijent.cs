using System;

namespace Model
{
    public class Pacijent : Osoba
    {
       
        public Pacijent()
        {

        }

        public Pacijent(Osoba o) 
        {
            this.ime = o.ime;
            this.prezime = o.prezime;
            this.jmbg = o.jmbg;
            this.telefon = o.telefon;
            this.email = o.email;
            this.korisnik = o.korisnik;
        }

        public ZdravstveniKarton zdravstveniKarton;
        public Termin[] termin;

    }
}