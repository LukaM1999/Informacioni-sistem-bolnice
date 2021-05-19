using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ProstorijaDto
    {
        public int Sprat { get; set; }
        public string Id { get; set; }
        public bool JeZauzeta { get; set; }
        public TipProstorije Tip { get; set; }
        public Inventar Inventar { get; set; }
        public RenoviranjeTermin Renoviranje { get; set; }
        public ObservableCollection<Termin> TerminiProstorije { get; set; }
        public ProstorijaDto(int Sprat, TipProstorije tip, string sifra, bool zauzeta, Inventar inventar)
        {
            this.Sprat = Sprat;
            this.Tip = tip;
            this.Id = sifra;
            JeZauzeta = zauzeta;
            this.Inventar = inventar;
        }
    }
}
