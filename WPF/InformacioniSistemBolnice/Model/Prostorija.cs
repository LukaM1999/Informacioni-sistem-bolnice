using System;
using System.Collections.ObjectModel;

namespace Model
{
    public class Prostorija
    {
        public int sprat
        {
            get;
            set;

        }

        public string id
        {
            get;
            set;
        }

        public bool jeZauzeta
        {
            get;
            set;
        }

        public TipProstorije tip
        {
            get;
            set;
        }

        public Prostorija()
        {
            jeZauzeta = false;
        }

        public Prostorija(int sp, TipProstorije t, string sifra, bool zauzeta)
        {
            sprat = sp;
            tip = t;
            id = sifra;
            jeZauzeta = zauzeta;

        }
        public ObservableCollection<Termin> termin;
        public Inventar inventar
        {
            get;
            set;
        }

        public override string ToString()
        {
            return "ID: " + this.id;
        }

    }
}