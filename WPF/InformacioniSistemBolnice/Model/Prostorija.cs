using System;
using System.Collections.ObjectModel;

namespace Model
{
    public class Prostorija
    {
        public int Sprat { get; set; }
        public string Id { get; set; }
        public bool JeZauzeta { get; set; }
        public TipProstorije Tip { get; set; }
        public Inventar Inventar { get; set; }
        public RenoviranjeTermin Renoviranje { get; set; }
        public ObservableCollection<Termin> TerminiProstorije { get; set; }

        public Prostorija() { JeZauzeta = false; }

        public Prostorija(int sprat, TipProstorije tip, string sifra, bool zauzeta, Inventar inventar)
        {
            this.Sprat = sprat;
            this.Tip = tip;
            this.Id = sifra;
            JeZauzeta = zauzeta;
            this.Inventar = inventar;
            TerminiProstorije = new ObservableCollection<Termin>();
        }

        public override string ToString()
        {
            return "ID: " + this.Id;
        }

        public bool ObrisiTermin(Termin terminZaBrisanje)
        {
            return TerminiProstorije.Remove(NadjiTerminPoDatumu(terminZaBrisanje.Vreme));
        }

        public bool DodajTermin(Termin terminZaDodavanje)
        {
            if (NadjiTerminPoDatumu(terminZaDodavanje.Vreme) != null) return false;
            TerminiProstorije.Add(terminZaDodavanje);
            return true;
        }

        public Termin NadjiTerminPoDatumu(DateTime vremeTermina)
        {
            foreach (Termin pronadjen in TerminiProstorije)
                if (pronadjen.Vreme == vremeTermina) return pronadjen;
            return null;
        }
    }
}