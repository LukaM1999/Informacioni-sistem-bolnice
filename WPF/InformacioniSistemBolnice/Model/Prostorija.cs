using System;
using System.Collections.ObjectModel;

namespace Model
{
    public class Prostorija
    {
        public int sprat { get; set; }
        public string id { get; set; }
        public bool jeZauzeta { get; set; }
        public TipProstorije tip { get; set; }
        public Inventar inventar { get; set; }
        public RenoviranjeTermin Renoviranje { get; set; }
        public ObservableCollection<Termin> TerminiProstorije { get; set; }
        public Prostorija()
        {
            jeZauzeta = false;
        }

        public Prostorija(int sprat, TipProstorije tip, string sifra, bool zauzeta, Inventar inventar)
        {
            this.sprat = sprat;
            this.tip = tip;
            this.id = sifra;
            jeZauzeta = zauzeta;
            this.inventar = inventar;
        }

        public override string ToString()
        {
            return "ID: " + this.id;
        }

        public bool ObrisiTermin(Termin terminZaBrisanje)
        {
            return TerminiProstorije.Remove(NadjiTerminPoDatumu(terminZaBrisanje.vreme));
        }

        public bool DodajTermin(Termin terminZaDodavanje)
        {
            if (NadjiTerminPoDatumu(terminZaDodavanje.vreme) != null) return false;
            TerminiProstorije.Add(terminZaDodavanje);
            return true;
        }

        public Termin NadjiTerminPoDatumu(DateTime vremeTermina)
        {
            foreach (Termin pronadjen in TerminiProstorije)
                if (pronadjen.vreme == vremeTermina) return pronadjen;
            return null;
        }
    }
}