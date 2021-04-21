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
        public Inventar()
        {
            dinamickaOprema = new ObservableCollection<DinamickaOprema>();
        }

        public DinamickaOprema getSelected(DinamickaOprema oprema)
        {
            return dinamickaOprema.ElementAt(dinamickaOprema.IndexOf(oprema));
        }

    }
}