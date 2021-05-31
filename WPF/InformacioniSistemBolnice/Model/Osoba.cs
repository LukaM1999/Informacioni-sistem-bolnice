using System;

namespace Model
{
    public class Osoba
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Jmbg { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
        public Adresa AdresaStanovanja { get; set; }
        public Korisnik Korisnik { get; set; }
        public Osoba() {}
        public Osoba(Adresa adresa, Korisnik korisnik)
        {
            AdresaStanovanja = adresa;
            Korisnik = korisnik;
        }

        public Osoba(string ime, string prezime, string jmbg, DateTime datumRodjenja, string telefon, string email, Korisnik korisnik, Adresa adresa)
        {
            this.Ime = ime;
            this.Prezime = prezime;
            this.Jmbg = jmbg;
            this.DatumRodjenja = datumRodjenja;
            this.Telefon = telefon;
            this.Email = email;
            this.Korisnik = korisnik;
            this.AdresaStanovanja = adresa;
            
        }
    }
}