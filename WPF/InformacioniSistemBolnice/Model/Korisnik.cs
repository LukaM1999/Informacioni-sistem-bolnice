using System;

namespace Model
{
    public class Korisnik
    {

        public string korisnickoIme
        {
            get; 
            set;
           

        }

        public string lozinka
        {
            get;
            set;

        }

        public UlogaKorisnika uloga
        {
            get;
            set;
        }

        public Korisnik()
        {

        }
        public Korisnik(string kI, string l, UlogaKorisnika u)
        {
            korisnickoIme = kI;
            lozinka = l;
            uloga = u;
        }


    }
}