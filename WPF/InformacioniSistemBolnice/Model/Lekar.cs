using System;
using System.Collections.ObjectModel;
using PropertyChanged;
namespace Model
{
    [AddINotifyPropertyChangedInterface]
    public class Lekar : Osoba
    {
        public string specijalizacija { get; set; }
        public ObservableCollection<Termin> zauzetiTermini = new ObservableCollection<Termin>();

        public Lekar() {}

        public Lekar(Osoba o, string specijalizacija)
        {
            this.ime = o.ime;
            this.prezime = o.prezime;
            this.jmbg = o.jmbg;
            this.telefon = o.telefon;
            this.email = o.email;
            this.korisnik = o.korisnik;
            this.specijalizacija = specijalizacija;
            zauzetiTermini = new ObservableCollection<Termin>();
        }
        public override string ToString()
        {
            return ime + " " + prezime;
        }
    }
}