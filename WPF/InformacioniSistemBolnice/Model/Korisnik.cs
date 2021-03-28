using System;

namespace Model
{
    public class Korisnik
    {

        

        public string korisnickoIme;

        public string KorisnickoIme
        {
            get
            {
                return korisnickoIme;
            }
            set
            {
                korisnickoIme = value;
            }

        }

        public string lozinka;

        public string Lozinka
        {
            get
            {
                return lozinka;
            }
            set
            {
                lozinka = value;
            }

        }

        public UlogaKorisnika uloga;

        public UlogaKorisnika Uloga
        {
            get
            {
                return uloga;
            }
            set
            {
                uloga = value;
            }
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