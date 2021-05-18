using System;
using System.Collections.ObjectModel;
using PropertyChanged;
namespace Model
{
    [AddINotifyPropertyChangedInterface]
    public class Lekar : Osoba
    {
        public Specijalizacija specijalizacija { get; set; }
        public ObservableCollection<Termin> zauzetiTermini = new ObservableCollection<Termin>();

        public Lekar() {}

        public Lekar(Osoba o, Specijalizacija specijalizacija)
        {
            this.ime = o.ime;
            this.prezime = o.prezime;
            this.jmbg = o.jmbg;
            this.datumRodjenja = o.datumRodjenja;
            this.adresa = o.adresa;
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
        public bool ObrisiTermin(Termin terminZaBrisanje)
        {
            return zauzetiTermini.Remove(NadjiTerminPoDatumu(terminZaBrisanje.vreme));
        }

        public bool DodajTermin(Termin terminZaDodavanje)
        {
            if (NadjiTerminPoDatumu(terminZaDodavanje.vreme) != null) return false;
            zauzetiTermini.Add(terminZaDodavanje);
            return true;
        }

        public Termin NadjiTerminPoDatumu(DateTime vremeTermina)
        {
            foreach (Termin pronadjen in zauzetiTermini)
                if (pronadjen.vreme == vremeTermina) return pronadjen;
            return null;
        }
    }
}