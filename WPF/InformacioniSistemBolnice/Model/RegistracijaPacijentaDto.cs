using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class PacijentDto
    {
        public string ime { get; set; }

        public string prezime { get; set; }

        public string jmbg { get; set; }

        public DateTime datumRodjenja { get; set; }

        public string telefon { get; set; }

        public string email { get; set; }

        public string grad { get; set; }

        public string drzava { get; set; }

        public string ulica { get; set; }

        public string broj { get; set; }

        public string korisnickoIme { get; set; }

        public string lozinka { get; set; }

        
        public PacijentDto(string ime, string prezime, string jmbg, DateTime datumRodjenja, string telefon, string email, string korisnickoIme,
            string lozinka, string drzava, string grad, string ulica, string broj)
        {
            this.ime = ime;
            this.prezime = prezime;
            this.jmbg = jmbg;
            this.datumRodjenja = datumRodjenja;
            this.telefon = telefon;
            this.email = email;
            this.drzava = drzava;
            this.grad = grad;
            this.ulica = ulica;
            this.broj = broj;
            this.korisnickoIme = korisnickoIme;
            this.lozinka = lozinka;
        }
    }
}
