using System;
using System.Collections.ObjectModel;

namespace Model
{
    public class ZdravstveniKarton
    {
        private string brojKartona;
        private string brojKnjizice;
        private string imeRoditelja;
        private string liceZaZdravZastitu;
        private BracnoStanje bracnoStanje;
        private KategorijaZdravstveneZastite kategorijaZdravZastite;

        public ZdravstveniKarton()
        {

        }

        public PodaciOZaposlenjuIZanimanju[] podaciOZaposlenjuIZanimanju;
        public ObservableCollection<Recept> recept;

        public ObservableCollection<Recept> Recept
        {
            get
            {
                if (recept == null)
                    recept = new ObservableCollection<Recept>();
                return recept;
            }
            set
            {
                RemoveAllRecept();
                if (value != null)
                {
                    foreach (Recept oRecept in value)
                        AddRecept(oRecept);
                }
            }
        }

        public void AddRecept(Recept newRecept)
        {
            if (newRecept == null)
                return;
            if (this.recept == null)
                this.recept = new ObservableCollection<Recept>();
            if (!this.recept.Contains(newRecept))
                this.recept.Add(newRecept);
        }

        public void RemoveRecept(Recept oldRecept)
        {
            if (oldRecept == null)
                return;
            if (this.recept != null)
                if (this.recept.Contains(oldRecept))
                    this.recept.Remove(oldRecept);
        }

        public void RemoveAllRecept()
        {
            if (recept != null)
                recept.Clear();
        }
        public ObservableCollection<Anamneza> anamneza;

        public ObservableCollection<Anamneza> Anamneza
        {
            get
            {
                if (anamneza == null)
                    anamneza = new ObservableCollection<Anamneza>();
                return anamneza;
            }
            set
            {
                RemoveAllAnamneza();
                if (value != null)
                {
                    foreach (Anamneza oAnamneza in value)
                        AddAnamneza(oAnamneza);
                }
            }
        }

        public void AddAnamneza(Anamneza newAnamneza)
        {
            if (newAnamneza == null)
                return;
            if (this.anamneza == null)
                this.anamneza = new ObservableCollection<Anamneza>();
            if (!this.anamneza.Contains(newAnamneza))
                this.anamneza.Add(newAnamneza);
        }

        public void RemoveAnamneza(Anamneza oldAnamneza)
        {
            if (oldAnamneza == null)
                return;
            if (this.anamneza != null)
                if (this.anamneza.Contains(oldAnamneza))
                    this.anamneza.Remove(oldAnamneza);
        }

        public void RemoveAllAnamneza()
        {
            if (anamneza != null)
                anamneza.Clear();
        }
        public Pacijent pacijent;

    }
}