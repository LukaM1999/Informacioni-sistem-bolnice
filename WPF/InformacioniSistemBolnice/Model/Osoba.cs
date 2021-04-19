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


        public Osoba()
        {

        }

        public Osoba(string i, string prz, string matBr, DateTime dR, string tel, string mail, Korisnik k)
        {
            ime = i;
            prezime = prz;
            jmbg = matBr;
            datumRodjenja = dR;
            telefon = tel;
            email = mail;
            korisnik = k;

        }

        public Korisnik korisnik
        {
            get;
            set;
        }

    }
}