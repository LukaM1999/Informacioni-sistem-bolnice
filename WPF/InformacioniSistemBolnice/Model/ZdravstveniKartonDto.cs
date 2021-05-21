using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ZdravstveniKartonDto
    {
        public string BrojKartona { get; set; }
        public string BrojKnjizice { get; set; }
        public string Jmbg { get; set; }
        public string ImeJednogRoditelja { get; set; }
        public string LiceZaZdravstvenuZastitu { get; set; }
        public Pol PolPacijenta { get; set; }
        public BracnoStanje BracnoStanje { get; set; }
        public KategorijaZdravstveneZastite KategorijaZdravstveneZastite { get; set; }
        public Alergen Alergen { get; set; }



        public ZdravstveniKartonDto(String brojZdrKartona, String brojZdrKnjizice, String JMBG, String imeRoditelja, String liceZaZdravZastitu, Pol pol,
                                 BracnoStanje bracnoStanjee, KategorijaZdravstveneZastite kategorijaZdravZastite, Alergen alergen)
        {
            BrojKartona = brojZdrKartona;
            BrojKnjizice = brojZdrKnjizice;
            Jmbg = JMBG;
            ImeJednogRoditelja = imeRoditelja;
            LiceZaZdravstvenuZastitu = liceZaZdravZastitu;
            PolPacijenta = pol;
            BracnoStanje = bracnoStanjee;
            KategorijaZdravstveneZastite = kategorijaZdravZastite;
            Alergen = alergen;
            
        }
    }
}
