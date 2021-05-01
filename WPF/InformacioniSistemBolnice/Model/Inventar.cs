using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Model
{
   public class Inventar
   {

        public ObservableCollection<DinamickaOprema> dinamickaOprema
        {
            get;
            set;
        }

        public ObservableCollection<StatickaOprema> statickaOprema
        {
            get;
            set;
        }
        public Inventar()
        {
            dinamickaOprema = new ObservableCollection<DinamickaOprema>();
            statickaOprema = new ObservableCollection<StatickaOprema>();
        }

        public DinamickaOprema getSelected(DinamickaOprema oprema)
        {
            return dinamickaOprema.ElementAt(dinamickaOprema.IndexOf(oprema));
        }

        public StatickaOprema getSelected(StatickaOprema oprema)
        {
            return statickaOprema.ElementAt(statickaOprema.IndexOf(oprema));
        }

        public DinamickaOprema getSelectedD(DinamickaOprema p)
        {
            DinamickaOprema prs = null;
            foreach (DinamickaOprema pr in dinamickaOprema)
            {
                if (pr.tip.Equals(p.tip))
                {
                    prs = pr;
                }
            }
            return dinamickaOprema.ElementAt(dinamickaOprema.IndexOf(prs));
        }

        public StatickaOprema getSelectedS(StatickaOprema p)
        {
            StatickaOprema prs = null;
            foreach (StatickaOprema pr in statickaOprema)
            {
                if (pr.tip.Equals(p.tip))
                {
                    prs = pr;
                }
            }
            return statickaOprema.ElementAt(statickaOprema.IndexOf(prs));
        }
    }
}