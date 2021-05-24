using System;

namespace Model
{
    public class Korisnik
    {
        public string KorisnickoIme { get; set; }
        public string Lozinka { get; set; }
        public UlogaKorisnika Uloga { get; set; }
        public Korisnik(string korisnickoIme, string lozinka, UlogaKorisnika uloga)
        {
            this.KorisnickoIme = korisnickoIme;
            this.Lozinka = lozinka;
            this.Uloga = uloga;
        }
    }
}