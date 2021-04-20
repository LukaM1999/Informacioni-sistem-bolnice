using System;

namespace Model
{
    public class Osoba
    {

        public string ime
        {
            get;
            set;
        }

    public string prezime
        {
            get;
            set;
        }

        public string jmbg
        {
            get;
            set;
        }

        public DateTime datumRodjenja
        {
            get;
            set;
        }

        public string telefon
        {
            get;
            set;
        }

        public string email
        {
            get;
            set;
        }

        public Adresa adresa{
            get;
            set;
        }
       

        public Osoba()
        {

        }

        public Osoba(string i, string prz, string matBr, DateTime dR, string tel, string mail, Korisnik k, Adresa a)
        {
            ime = i;
            prezime = prz;
            jmbg = matBr;
            datumRodjenja = dR;
            telefon = tel;
            email = mail;
            korisnik = k;
            adresa = a;
            
        }

        public Korisnik korisnik
        {
            get;
            set;
        }

    }
}