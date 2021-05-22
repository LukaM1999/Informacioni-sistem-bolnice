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
    public class Pacijent : Osoba
    {
        public bool maliciozan { get; set; }
        public ZdravstveniKarton zdravstveniKarton;
        public ObservableCollection<Termin> zakazaniTermini = new();

        public Pacijent() {}
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

        public bool ObrisiTermin(Termin terminZaBrisanje)
        {
            return zakazaniTermini.Remove(NadjiTerminPoDatumu(terminZaBrisanje.vreme));
        }

        public bool DodajTermin(Termin terminZaDodavanje)
        {
            if (NadjiTerminPoDatumu(terminZaDodavanje.vreme) != null) return false;
            zakazaniTermini.Add(terminZaDodavanje);
            return true;
        }
        public override string ToString()
        {
            return ime + " " + prezime;
        }

        private Termin NadjiTerminPoDatumu(DateTime vremeTermina)
        {
            foreach (Termin pronadjen in zakazaniTermini)
                if (pronadjen.vreme == vremeTermina) return pronadjen;
            return null;
        }

        public List<Termin> DobaviSortiraneTermine()
        {
            return zakazaniTermini.OrderBy(termin => termin.vreme).ToList();
        }

        public bool PacijentPosetioBolnicu(List<Termin> sortiraniTermini)
        {
            return sortiraniTermini.Count != 0 && sortiraniTermini[0].status == StatusTermina.zavrsen;
        }
    }
}