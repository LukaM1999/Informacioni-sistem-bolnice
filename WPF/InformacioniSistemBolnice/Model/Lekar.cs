using System;
using System.Collections.ObjectModel;
using PropertyChanged;
namespace Model
{
    [AddINotifyPropertyChangedInterface]
    public class Lekar : Osoba
    {
        public Specijalizacija Specijalizacija { get; set; }
        public ObservableCollection<Termin> ZauzetiTermini { get; set; }

        public Lekar(Osoba osoba, Specijalizacija specijalizacija)
        {
            Ime = osoba.Ime;
            Prezime = osoba.Prezime;
            Jmbg = osoba.Jmbg;
            DatumRodjenja = osoba.DatumRodjenja;
            AdresaStanovanja = osoba.AdresaStanovanja;
            Telefon = osoba.Telefon;
            Email = osoba.Email;
            Korisnik = osoba.Korisnik;
            Specijalizacija = specijalizacija;
            ZauzetiTermini = new ObservableCollection<Termin>();
        }

        public override string ToString()
        {
            return Ime + " " + Prezime;
        }
        public bool ObrisiTermin(Termin terminZaBrisanje)
        {
            return ZauzetiTermini.Remove(NadjiTerminPoDatumu(terminZaBrisanje.Vreme));
        }

        public bool DodajTermin(Termin terminZaDodavanje)
        {
            if (NadjiTerminPoDatumu(terminZaDodavanje.Vreme) != null) return false;
            ZauzetiTermini.Add(terminZaDodavanje);
            return true;
        }

        public Termin NadjiTerminPoDatumu(DateTime vremeTermina)
        {
            foreach (Termin pronadjen in ZauzetiTermini)
                if (pronadjen.Vreme == vremeTermina) return pronadjen;
            return null;
        }
    }
}