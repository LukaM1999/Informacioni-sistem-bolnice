using Model;

namespace InformacioniSistemBolnice.DTO
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

        public ZdravstveniKartonDto(string brojZdrKartona, string brojZdrKnjizice, string jmbg, string imeRoditelja, 
                                    string liceZaZdravZastitu, Pol pol, BracnoStanje bracnoStanjee, 
                                    KategorijaZdravstveneZastite kategorijaZdravZastite, Alergen alergen)
        {
            BrojKartona = brojZdrKartona;
            BrojKnjizice = brojZdrKnjizice;
            Jmbg = jmbg;
            ImeJednogRoditelja = imeRoditelja;
            LiceZaZdravstvenuZastitu = liceZaZdravZastitu;
            PolPacijenta = pol;
            BracnoStanje = bracnoStanjee;
            KategorijaZdravstveneZastite = kategorijaZdravZastite;
            Alergen = alergen;
            
        }
    }
}
