using System;

namespace Model
{
    public class Osoba
    {

        public string ime;

        public string ImeOsobe
        {
            get
            {
                return ime;
            }
            set
            {
                ime = value;
            }
           
        }

        public string prezime;

        public string PrezimeOsobe
        {
            get
            {
                return prezime;
            }
            set
            {
                prezime = value;
            }
        }

        public string jmbg;

        public string JMBGOsobe
        {
            get
            {
                return jmbg;
            }
            set
            {
                jmbg = value;
            }
        }

        public DateTime datumRodjenja;

        public DateTime DatumRodjenjaOsobe
        {
            get
            {
                return datumRodjenja;
            }
            set
            {
                datumRodjenja = value;
            }
        }

        public string telefon;

        public string TelefonOsobe
        {
            get
            {
                return telefon;
            }
            set
            {
                telefon = value;
            }
        }

        public string email;

        public string EmailOsobe
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
            }
        }

        public Adresa adresa;
        public Adresa AdresaOsobe
        {
            get
            {
                return adresa;
            }
            set
            {
                adresa = value;
            }
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

        public Korisnik korisnik;

    }
}