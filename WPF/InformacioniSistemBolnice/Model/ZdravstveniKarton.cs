using System;
using System.Collections.ObjectModel;
using Repozitorijum;
using PropertyChanged;

namespace Model
{
    [AddINotifyPropertyChangedInterface]
    public class ZdravstveniKarton
    {
        public string BrojKartona { get; set;}
        public string BrojKnjizice { get; set;}
        public string Jmbg { get; set;}
        public string ImeJednogRoditelja { get; set;}
        public string LiceZaZdravstvenuZastitu { get; set;}
        public Pol PolPacijenta { get; set;}
        public BracnoStanje BracnoStanje { get; set;}
        public KategorijaZdravstveneZastite KategorijaZdravstveneZastite { get; set;}
        public PodaciOZaposlenjuIZanimanju PodaciOZaposlenjuIZanimanjuPacijenta { get; set;}
        public ObservableCollection<Alergen> alergeni;
        public ObservableCollection<Recept> Recepti = new ObservableCollection<Recept>();

        public Anamneza anamneza
        {
            get;
            set;
        }

        public ObservableCollection<Alergen> Alergeni
        {
            get
            {
                if (alergeni == null)
                    alergeni = new ObservableCollection<Alergen>();
                return alergeni;
            }
            set
            {
                RemoveAllAlergen();
                if (value != null)
                {
                    foreach (Alergen Oalergen in value)
                        AddAlergen(Oalergen);
                }
            }
        }

        public void AddAlergen(Alergen newAlergen)
        {
            if (newAlergen == null)
                return;
            if (this.alergeni == null)
                this.alergeni = new ObservableCollection<Alergen>();
            if (!this.alergeni.Contains(newAlergen))
                this.alergeni.Add(newAlergen);
        }

        public void RemoveAllAlergen()
        {
            if (alergeni != null)
                alergeni.Clear();
        }

        public ZdravstveniKarton(String brojZdrKartona, String brojZdrKnjizice, String JMBG, String imeRoditelja, String liceZaZdravZastitu, Pol pol,
                                 BracnoStanje bracnoStanjee, KategorijaZdravstveneZastite kategorijaZdravZastite, PodaciOZaposlenjuIZanimanju podaciOZaposlenjuiZanimanju)
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