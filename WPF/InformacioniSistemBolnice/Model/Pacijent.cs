using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using PropertyChanged;

namespace Model
{
    [AddINotifyPropertyChangedInterface]
    public class Pacijent : Osoba
    {

        public bool maliciozan
        {
            get;
            set;
        }
       
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
            this.adresa = o.adresa;
            this.maliciozan = false;
        }

        public ZdravstveniKarton zdravstveniKarton;
        public ObservableCollection<Termin> zakazaniTermini { get; set; }
        

        public override string ToString()
        {
            return korisnik.korisnickoIme;
        }
    }
}