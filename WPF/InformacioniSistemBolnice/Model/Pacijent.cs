using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

namespace Model
{
    public class Pacijent : Osoba
    {
       
        public Pacijent()
        {

        }

        public Pacijent(Osoba o) 
        {
            this.ime = o.ime;
            this.prezime = o.prezime;
            this.datumRodjenja = o.datumRodjenja;
            this.jmbg = o.jmbg;
            this.telefon = o.telefon;
            this.email = o.email;
            this.korisnik = o.korisnik;
            this.zakazaniTermini = new ObservableCollection<Termin>();
        }

        public ZdravstveniKarton zdravstveniKarton;
        public ObservableCollection<Termin> zakazaniTermini = new ObservableCollection<Termin>();

        public override string ToString()
        {
            return korisnik.korisnickoIme;
        }
    }
}