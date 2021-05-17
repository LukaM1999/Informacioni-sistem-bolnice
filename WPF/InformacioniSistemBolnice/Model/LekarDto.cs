using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class LekarDto
    {
        public string ime { get; set; }
        public string prezime { get; set; }
        public string jmbg { get; set; }
        public DateTime datumRodjenja { get; set; }
        public string drzava { get; set; }
        public string grad { get; set; }
        public string ulica { get; set; }
        public string broj { get; set; }
        public string telefon { get; set; }
        public string email { get; set; }
        public string korisnickoIme { get; set; }
        public string lozinka { get; set; }
        public string specijalizacija { get; set; }

        public LekarDto(string ime, string prezime, string jmbg, DateTime datumRodjenja, 
                        string drzava, string grad, string ulica, string broj,
                        string telefon,string email, string korisnickoIme, string lozinka, 
                        string specijalizacija)
        {
            this.ime = ime;
            this.prezime = prezime;
            this.jmbg = jmbg;
            this.datumRodjenja = datumRodjenja;
            this.drzava = drzava;
            this.grad = grad;
            this.ulica = ulica;
            this.broj = broj;
            this.telefon = telefon;
            this.email = email;
            this.korisnickoIme = korisnickoIme;
            this.lozinka = lozinka;
            this.specijalizacija = specijalizacija;
           
        }

    }
}
