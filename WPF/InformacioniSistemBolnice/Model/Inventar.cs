using System;
using System.Collections.ObjectModel;

namespace Model
{
   public class Inventar
   {
      public ObservableCollection<Oprema> oprema;
      
      public ObservableCollection<Oprema> Oprema
      {
         get
         {
            if (oprema == null)
               oprema = new ObservableCollection<Oprema>();
            return oprema;
         }
         set
         {
            RemoveAllOprema();
            if (value != null)
            {
               foreach (Oprema oOprema in value)
                  AddOprema(oOprema);
            }
         }
      }
      
      public void AddOprema(Oprema newOprema)
      {
         if (newOprema == null)
            return;
         if (this.oprema == null)
            this.oprema = new ObservableCollection<Oprema>();
         if (!this.oprema.Contains(newOprema))
            this.oprema.Add(newOprema);
      }
      
      public void RemoveOprema(Oprema oldOprema)
      {
         if (oldOprema == null)
            return;
         if (this.oprema != null)
            if (this.oprema.Contains(oldOprema))
               this.oprema.Remove(oldOprema);
      }
      
      public void RemoveAllOprema()
      {
         if (oprema != null)
            oprema.Clear();
      }
   
   }
}