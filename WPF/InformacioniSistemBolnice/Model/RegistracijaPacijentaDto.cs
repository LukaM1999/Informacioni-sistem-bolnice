using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class PacijentDto
    {
        public string Ime { get; set; }

        public string Prezime { get; set; }

        public string PacijentJmbg { get; set; }

        public DateTime DatumRodjenja { get; set; }

        public string Telefon { get; set; }

        public string Email { get; set; }

        public string Grad { get; set; }

        public string Drzava { get; set; }

        public string Ulica { get; set; }

        public string Broj { get; set; }

        public string KorisnickoIme { get; set; }

        public string Lozinka { get; set; }

        
        public PacijentDto(string ime, string prezime, string jmbg, DateTime datumRodjenja, string telefon, string email, string korisnickoIme,
            string lozinka, string drzava, string grad, string ulica, string broj)
        {
            this.Ime = ime;
            this.Prezime = prezime;
            this.PacijentJmbg = jmbg;
            this.DatumRodjenja = datumRodjenja;
            this.Telefon = telefon;
            this.Email = email;
            this.Drzava = drzava;
            this.Grad = grad;
            this.Ulica = ulica;
            this.Broj = broj;
            this.KorisnickoIme = korisnickoIme;
            this.Lozinka = lozinka;
        }
    }
}
