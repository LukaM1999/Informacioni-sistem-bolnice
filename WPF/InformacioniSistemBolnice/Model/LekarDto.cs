using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class LekarDto
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string LekarJmbg { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public string Drzava { get; set; }
        public string Grad { get; set; }
        public string Ulica { get; set; }
        public string Broj { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
        public string KorisnickoIme { get; set; }
        public string Lozinka { get; set; }
        public string Specijalizacija { get; set; }

        public LekarDto() {}
        public LekarDto(string ime, string prezime, string jmbg, DateTime datumRodjenja, 
                        string drzava, string grad, string ulica, string broj,
                        string telefon,string email, string korisnickoIme, string lozinka, 
                        string specijalizacija)
        {
            this.Ime = ime;
            this.Prezime = prezime;
            this.LekarJmbg = jmbg;
            this.DatumRodjenja = datumRodjenja;
            this.Drzava = drzava;
            this.Grad = grad;
            this.Ulica = ulica;
            this.Broj = broj;
            this.Telefon = telefon;
            this.Email = email;
            this.KorisnickoIme = korisnickoIme;
            this.Lozinka = lozinka;
            this.Specijalizacija = specijalizacija;
           
        }

        public LekarDto(string ime, string prezime, string jmbg, DateTime datumRodjenja,
                        string drzava, string grad, string ulica, string broj,
                        string telefon, string email, string korisnickoIme, string lozinka)
        {
            this.Ime = ime;
            this.Prezime = prezime;
            this.LekarJmbg = jmbg;
            this.DatumRodjenja = datumRodjenja;
            this.Drzava = drzava;
            this.Grad = grad;
            this.Ulica = ulica;
            this.Broj = broj;
            this.Telefon = telefon;
            this.Email = email;
            this.KorisnickoIme = korisnickoIme;
            this.Lozinka = lozinka;
            
        }
    }
}
