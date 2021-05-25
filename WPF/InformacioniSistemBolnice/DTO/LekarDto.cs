using System;

namespace InformacioniSistemBolnice.DTO
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

        public LekarDto(string ime, string prezime, string jmbg, DateTime datumRodjenja, string drzava, string grad, 
                        string ulica, string broj, string telefon, string email, string korisnickoIme, string lozinka,
                        string specijalizacija)
        {
            Ime = ime;
            Prezime = prezime;
            LekarJmbg = jmbg;
            DatumRodjenja = datumRodjenja;
            Drzava = drzava;
            Grad = grad;
            Ulica = ulica;
            Broj = broj;
            Telefon = telefon;
            Email = email;
            KorisnickoIme = korisnickoIme;
            Lozinka = lozinka;
            Specijalizacija = specijalizacija;
           
        }
    }
}
