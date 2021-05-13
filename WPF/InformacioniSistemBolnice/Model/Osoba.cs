using System;

namespace Model
{
    public class Osoba
    {
        public string ime { get; set; }
        public string prezime { get; set; }
        public string jmbg { get; set; }
        public DateTime datumRodjenja { get; set; }
        public string telefon { get; set; }
        public string email { get; set; }
        public Adresa adresa { get; set; }
        public Korisnik korisnik { get; set; }
        public Osoba() {}
        
        public Osoba(string ime, string prezime, string jmbg, DateTime datumRodjenja, string telefon, string email, Korisnik korisnik, Adresa adresa)
        {
            this.ime = ime;
            this.prezime = prezime;
            this.jmbg = jmbg;
            this.datumRodjenja = datumRodjenja;
            this.telefon = telefon;
            this.email = email;
            this.korisnik = korisnik;
            this.adresa = adresa;
            
        }
    }
}