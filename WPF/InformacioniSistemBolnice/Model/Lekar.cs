using System;
using System.Collections.ObjectModel;
using PropertyChanged;
namespace Model
{
    [AddINotifyPropertyChangedInterface]
    public class Lekar : Osoba, IUcesnikPregleda
    {
        public Specijalizacija Specijalizacija { get; set; }
        public ObservableCollection<Termin> ZakazaniTermini { get; set; }

        public Lekar() { }
        
        public Lekar(Adresa adresa, Korisnik korisnik)
        {
            AdresaStanovanja = adresa;
            Korisnik = korisnik;
        }

        public Lekar(Osoba o, Specijalizacija specijalizacija)
        {
            this.Ime = o.Ime;
            this.Prezime = o.Prezime;
            this.Jmbg = o.Jmbg;
            this.DatumRodjenja = o.DatumRodjenja;
            this.AdresaStanovanja = o.AdresaStanovanja;
            this.Telefon = o.Telefon;
            this.Email = o.Email;
            this.Korisnik = o.Korisnik;
            this.Specijalizacija = specijalizacija;
            ZakazaniTermini = new ObservableCollection<Termin>();
        }
        public override string ToString()
        {
            return Ime + " " + Prezime;
        }
        public bool ObrisiTermin(Termin terminZaBrisanje)
        {
            return ZakazaniTermini.Remove(NadjiTerminPoDatumu(terminZaBrisanje.Vreme));
        }

        public bool DodajTermin(Termin terminZaDodavanje)
        {
            if (NadjiTerminPoDatumu(terminZaDodavanje.Vreme) != null) return false;
            ZakazaniTermini.Add(terminZaDodavanje);
            return true;
        }

        public Termin NadjiTerminPoDatumu(DateTime vremeTermina)
        {
            foreach (Termin pronadjen in ZakazaniTermini)
                if (pronadjen.Vreme == vremeTermina) return pronadjen;
            return null;
        }
    }
}