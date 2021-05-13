using System;

namespace Model
{
    public class Korisnik
    {
        public string korisnickoIme { get; set; }
        public string lozinka { get; set; }
        public UlogaKorisnika uloga { get; set; }
        public Korisnik(string korisnickoIme, string lozinka, UlogaKorisnika uloga)
        {
            this.korisnickoIme = korisnickoIme;
            this.lozinka = lozinka;
            this.uloga = uloga;
        }
    }
}