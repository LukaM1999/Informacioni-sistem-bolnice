using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Linq;
using PropertyChanged;

namespace Model
{
    [AddINotifyPropertyChangedInterface]
    public class Pacijent : Osoba, IUcesnikPregleda
    {
        public bool Maliciozan { get; set; }
        public ZdravstveniKarton zdravstveniKarton { get; set; }
        public ObservableCollection<Termin> ZakazaniTermini { get; set; }
        public string SlikaPutanja { get; set; } = "";

        public Pacijent() {}
        public Pacijent(Osoba o) 
        {
            this.Ime = o.Ime;
            this.Prezime = o.Prezime;
            this.DatumRodjenja = o.DatumRodjenja;
            this.Jmbg = o.Jmbg;
            this.Telefon = o.Telefon;
            this.Email = o.Email;
            this.Korisnik = o.Korisnik;
            this.AdresaStanovanja = o.AdresaStanovanja;
            this.Maliciozan = false;
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
        public override string ToString()
        {
            return Ime + " " + Prezime;
        }

        private Termin NadjiTerminPoDatumu(DateTime vremeTermina)
        {
            foreach (Termin pronadjen in ZakazaniTermini)
                if (pronadjen.Vreme == vremeTermina) return pronadjen;
            return null;
        }

        public List<Termin> DobaviSortiraneTermine()
        {
            return ZakazaniTermini.OrderBy(termin => termin.Vreme).ToList();
        }

        public bool PacijentPosetioBolnicu(List<Termin> sortiraniTermini)
        {
            return sortiraniTermini.Count != 0 && sortiraniTermini[0].Status == StatusTermina.zavrsen;
        }
    }
}