using System;
using System.Collections.ObjectModel;
using Repozitorijum;


namespace Model
{
    public class ZdravstveniKarton
    {
        private string brojKartona;
        private string brojKnjizice;
        private string jmbg;
        private string imeJednogRoditelja;
        private string liceZaZdravstvenuZastitu;
        private Pol polPacijenta;
        private BracnoStanje bracnoStanje;
        private KategorijaZdravstveneZastite kategorijaZdravstveneZastite;
        private PodaciOZaposlenjuIZanimanju podaciOZaposlenjuIZanimanjuPacijenta;
        private ObservableCollection<Alergen> alergeni; 
        private ObservableCollection<Recept> recepti;
       





        private BracnoStanje BracnoStanjePacijenta
        {
            get
            {
                return BracnoStanje;
            }
            set
            {
                BracnoStanje = value;
            }
        }
       

        private KategorijaZdravstveneZastite KategorijaZdravstveneZastitePacijenta
        {
            get
            {
                return KategorijaZdravstveneZastite;
            }
            set
            {
                KategorijaZdravstveneZastite = value;
            }
        }

        public string Jmbg { get => jmbg; set => jmbg = value; }
        public string LiceZaZdravstvenuZastitu { get => liceZaZdravstvenuZastitu; set => liceZaZdravstvenuZastitu = value; }
        public Pol PolPacijenta { get => polPacijenta; set => polPacijenta = value; }
        public string ImeJednogRoditelja { get => imeJednogRoditelja; set => imeJednogRoditelja = value; }
        public string BrojKnjizice { get => brojKnjizice; set => brojKnjizice = value; }
        public string BrojKartona { get => brojKartona; set => brojKartona = value; }
       
        public ObservableCollection<Recept> Recept { get => recepti; set => recepti = value; }
        public KategorijaZdravstveneZastite KategorijaZdravstveneZastite { get => kategorijaZdravstveneZastite; set => kategorijaZdravstveneZastite = value; }
        public BracnoStanje BracnoStanje { get => bracnoStanje; set => bracnoStanje = value; }
        public PodaciOZaposlenjuIZanimanju PodaciOZaposlenjuIZanimanjuPacijenta { get => podaciOZaposlenjuIZanimanjuPacijenta; set => podaciOZaposlenjuIZanimanjuPacijenta = value; }
        public ObservableCollection<Alergen> Alergeni { get => alergeni; set => alergeni = value; }
        

        public ZdravstveniKarton()
        {

        }



        public ZdravstveniKarton(String brojZdrKartona, String brojZdrKnjizice, String JMBG, String imeRoditelja, String liceZaZdravZastitu, Pol pol, BracnoStanje bracnoStanjee, KategorijaZdravstveneZastite kategorijaZdravZastite, PodaciOZaposlenjuIZanimanju podaciOZaposlenjuiZanimanju)
        {
            BrojKartona = brojZdrKartona;
            BrojKnjizice = brojZdrKnjizice;
            Jmbg = JMBG;
            ImeJednogRoditelja = imeRoditelja;
            LiceZaZdravstvenuZastitu = liceZaZdravZastitu;
            PolPacijenta = pol;
            BracnoStanje = bracnoStanjee;
            KategorijaZdravstveneZastite = kategorijaZdravZastite;
            PodaciOZaposlenjuIZanimanjuPacijenta = podaciOZaposlenjuiZanimanju;
       
        }



        

    }
}