using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Model
{
   public class Inventar
   {
        public ObservableCollection<DinamickaOprema> DinamickaOprema { get; set; }
        public ObservableCollection<StatickaOprema> StatickaOprema { get; set; }
        public Inventar()
        {
            DinamickaOprema = new ObservableCollection<DinamickaOprema>();
            StatickaOprema = new ObservableCollection<StatickaOprema>();
        }
        public DinamickaOprema NadjiDinamickuOpremuPoTipu(TipDinamickeOpreme tipOpreme)
        {
            foreach (DinamickaOprema oprema in DinamickaOprema) if (oprema.Tip == tipOpreme) return oprema;
            return null;
        }
        public StatickaOprema getSelected(StatickaOprema oprema)
        {
            return StatickaOprema.ElementAt(StatickaOprema.IndexOf(oprema));
        }
        public DinamickaOprema getSelectedD(DinamickaOprema p)
        {
            DinamickaOprema prs = null;
            foreach (DinamickaOprema pr in DinamickaOprema)
            {
                if (pr.Tip.Equals(p.Tip))
                {
                    prs = pr;
                }
            }
            return DinamickaOprema.ElementAt(DinamickaOprema.IndexOf(prs));
        }
        public StatickaOprema getSelectedS(StatickaOprema p)
        {
            StatickaOprema prs = null;
            foreach (StatickaOprema pr in StatickaOprema)
            {
                if (pr.tip.Equals(p.tip))
                {
                    prs = pr;
                }
            }
            return StatickaOprema.ElementAt(StatickaOprema.IndexOf(prs));
        }
    }
}